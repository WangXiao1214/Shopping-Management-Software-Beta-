using Shopping.Model;
using Shopping.Repository;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Shopping.Service;


namespace Shopping.Service
{
    public class CustomerAuthService
    {
        private readonly CustomerService _customerService = new CustomerService();
        private readonly Dictionary<string, CustomerIdentity> _identityMap = new Dictionary<string, CustomerIdentity>();
        private IReadOnlyDictionary<string, CustomerIdentity> IdentityMap => _identityMap;

        /// <summary>
        /// 用户注册方法
        /// 该方法用于创建新用户账号，包括创建客户信息、哈希密码和存储用户身份
        /// </summary>
        /// <param name="account">用户账号，必须是唯一的标识符</param>
        /// <param name="password">用户密码，将在存储前进行安全的加盐哈希处理</param>
        /// <param name="customerName">客户姓名，将用于创建客户信息</param>
        /// <returns>
        /// 返回注册结果：
        /// - true：注册成功
        /// - false：注册失败（通常是因为账号已存在）
        /// </returns>
        public bool Register(string account, string password, string customerName)
        {

            // 1. Check if the account already exists
            if (_identityMap.ContainsKey(account))
            {
                Console.WriteLine("Account already exists.");
                return false;
            }

            Console.WriteLine("Account isn't exist. Whether registering new account? ( Y/y continue or exit )");

            var inputInfo = Console.ReadLine().ToLower().Trim() ?? "no";

            if(inputInfo != "y")
            {
                Console.WriteLine("Exit ...");
                return false;
            }


            // 2. Create a new customer
            var newCustomer = new Customer(customerName);

            var hashedPwd = PasswordHelper.HashPassword(password);

            var identity = new CustomerIdentity(account, hashedPwd, newCustomer.Id);

            _customerService.AddCustomer(newCustomer);


            _identityMap[account] = identity;

            Console.WriteLine("Register successfully.");
            Console.WriteLine($"ID: {newCustomer.Id}\nAccount: {account}");
            return true;
        }

        /// <summary>
        /// 用户登录验证方法
        /// 该方法实现了安全的登录验证，包含防止时序攻击的保护措施
        /// 无论账号是否存在都会执行完整的密码验证流程，防止通过响应时间推测账号是否存在
        /// </summary>
        /// <param name="account">用户账号，用于查找用户身份信息</param>
        /// <param name="password">用户输入的密码，将与存储的哈希值进行验证</param>
        /// <returns>
        /// 返回登录验证结果：
        /// - Customer对象：登录成功，返回关联的客户信息
        /// - null：登录失败（不区分是账号不存在还是密码错误）
        /// </returns>
        public Customer Login(string account, string password)
        {
            // 1. 尝试获取身份信息（如果账号不存在，identity 会是 null）
            _identityMap.TryGetValue(account, out var identity);


            // 2. 准备要验证的哈希值
            string hashToVerify = identity?.PasswordHash
                            ?? PasswordHelper.HashPassword("dummy_nonexistent_password_123!@#");


            // 3. 使用 PasswordHasher 验证密码（这是关键！）
            var verificationResult = PasswordHelper.VerifyPassword(
                password,
                hashToVerify);

            // 4. 判断验证结果

            if (identity == null || !verificationResult)
            {
                Console.WriteLine("Login failed. Please check your account and password.");
                return null;
            }

            Console.WriteLine("Login successfully.");
            return _customerService.GetCustomerById(identity.CustomerId);
        }
    }

}