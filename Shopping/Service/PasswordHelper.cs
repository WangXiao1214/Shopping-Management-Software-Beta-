using System;
using System.Security.Cryptography;

namespace Shopping.Service  // 或者你自己的命名空间
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;        // 盐的长度（字节）
        private const int HashSize = 32;        // 哈希长度（字节）
        private const int Iterations = 600000;  // 2025/2026 推荐迭代次数（可根据性能调整为 310000~1000000）
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;


        /// <summary>
        /// 对密码进行安全哈希（注册时调用）
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("密码不能为空");

            // 生成随机盐
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // 使用 PBKDF2 + SHA256 进行哈希
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            // 把盐 + 哈希 合并后转成 Base64 存储
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// 验证密码（登录时调用）
        /// 支持账号不存在的情况（进行假验证，防止时序攻击）
        /// </summary>
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // 如果 storedHash 是 null 或空（账号不存在），也执行一次假的哈希计算
            if (string.IsNullOrEmpty(storedHash))
            {
                // 对一个固定假密码进行哈希（耗时接近真实验证）
                HashPassword("dummy_nonexistent_password_for_timing_attack_prevention");
                return false;
            }

            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedHash);

                // 取出盐（前16字节）
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // 取出原来的哈希值
                byte[] originalHash = new byte[HashSize];
                Array.Copy(hashBytes, SaltSize, originalHash, 0, HashSize);

                // 用相同的盐和迭代次数重新计算输入密码的哈希
                byte[] newhash = Rfc2898DeriveBytes.Pbkdf2(enteredPassword, salt, Iterations, Algorithm, HashSize);

                // 使用固定时间比较，防止时序攻击
                return CryptographicOperations.FixedTimeEquals(newhash, originalHash);
            }
            catch
            {
                // 如果 storedHash 格式错误，也当成验证失败
                return false;
            }
        }
    }
}