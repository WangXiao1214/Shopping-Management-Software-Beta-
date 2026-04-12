# 购物系统项目 README

## 🎯 项目概述
这是一个基于C# .NET的控制台购物应用程序，实现了完整的电商后端系统核心功能，包括用户管理、商品管理、购物车操作和安全的身份验证机制。

## 📁 项目结构
```
Shopping/
├── Model/           # 数据模型
│   ├── Good.cs              # 商品实体
│   ├── Customer.cs          # 客户实体（继承Person）
│   ├── Person.cs            # 人员基类
│   ├── CustomerIdentity.cs  # 客户身份凭证
├── Interface/       # 接口定义
│   ├── IGoodService.cs      # 商品服务接口
│   ├── IBaseService.cs      # 基础泛型服务接口
│   ├── ICustomerService.cs  # 客户服务接口
├── Service/         # 业务逻辑层
│   ├── GoodService.cs       # 商品服务实现
│   ├── CustomerService.cs   # 客户服务实现
│   ├── CustomerAuthService.cs # 客户认证服务
│   ├── PasswordHelper.cs    # 密码安全工具
│   ├── ServiceSystem.cs     # 系统入口
├── Repository/     # 数据访问层
│   ├── GoodRepository.cs    # 商品仓储
│   ├── CustomerRepository.cs # 客户仓储
├── Tool/           # 工具类
│   └── GenerateRandom.cs    # 随机数据生成器
└── Program.cs      # 程序入口
```

## 🏗️ 核心功能模块

### 1. 用户管理模块
- **用户注册**：通过`CustomerAuthService.Register()`方法创建新账户
- **用户登录**：通过`CustomerAuthService.Login()`进行安全验证
- **客户管理**：通过`CustomerService`进行客户信息的CRUD操作
- **安全特性**：
  - PBKDF2 + SHA256密码哈希
  - 盐值加密存储
  - 时序攻击防护
  - 固定时间比较

### 2. 商品管理模块
- **商品查询**：`GoodService.QueryAllGoods()`查看所有商品
- **随机选购**：`GoodService.GetSomeGoods()`为顾客随机添加商品
- **购物车统计**：`GoodService.GetGoodInfo()`显示购物详情
- **价格计算**：`GoodService.GetTotalGoodsPrice()`计算总价
- **商品列表**：`GoodRepository`管理100个随机生成的商品

### 3. 数据模型
- **Good**：商品类，包含ID、名称、价格信息
- **Customer**：客户类，包含个人信息和购物车商品列表
- **Person**：人员基类，包含姓名、年龄、生日等基本信息
- **CustomerIdentity**：用户凭证，关联账户、密码哈希和客户ID

## 🔧 技术特性

### 安全特性
```csharp
// 密码哈希与验证
string hashedPassword = PasswordHelper.HashPassword("plainPassword");
bool isValid = PasswordHelper.VerifyPassword("inputPassword", storedHash);
```

### 架构设计
- **接口驱动设计**：通过接口定义服务契约
- **仓储模式**：数据访问与业务逻辑分离
- **服务层**：封装核心业务逻辑
- **依赖注入**：服务间解耦设计

### 代码质量
- 泛型基类接口`IBaseService<T, TId>`
- 统一的异常处理机制
- 类型安全的数据访问
- 清晰的命名约定

## 🚀 快速开始

### 1. 系统初始化
```csharp
ServiceSystem serviceSystem = new ServiceSystem();
```

### 2. 用户注册
```csharp
bool success = serviceSystem.SysCustomerAuthService.Register(
    account: "user123",
    password: "securePassword123",
    customerName: "张三"
);
```

### 3. 用户登录
```csharp
Customer customer = serviceSystem.SysCustomerAuthService.Login(
    account: "user123",
    password: "securePassword123"
);
```

### 4. 商品操作
```csharp
// 查看所有商品
serviceSystem.SysGoodService.QueryAllGoods();

// 随机选购20件商品
serviceSystem.SysGoodService.GetSomeGoods(customer, 20);

// 查看购物车详情
serviceSystem.SysGoodService.GetGoodInfo(customer);

// 计算总价
decimal totalPrice = serviceSystem.SysGoodService.GetTotalGoodsPrice(customer);
```

## 📊 核心接口说明

### IGoodService
```csharp
public interface IGoodService
{
    void QueryAllGoods();                          // 查询所有商品
    void GetSomeGoods(Customer customer, int count); // 随机获取商品
    void GetGoodInfo(Customer customer);          // 获取购物车信息
    decimal GetTotalGoodsPrice(Customer customer); // 计算总价格
    IEnumerable<Good> GetAllGoods();              // 获取所有商品列表
}
```

### IBaseService
```csharp
public interface IBaseService<T, TId> where T : class
{
    void QueryAll();                              // 查询所有实体
    T GetById(TId id);                           // 根据ID获取实体
    void Add(T entity);                          // 添加实体
    IEnumerable<T> GetAll();                     // 获取所有实体列表
}
```

## 🔐 安全设计

### 密码存储
- 使用PBKDF2算法进行密码哈希
- 16字节随机盐值
- 60万次迭代（SHA256）
- Base64编码存储

### 时序攻击防护
```csharp
// 统一验证时间，无论账号是否存在
string hashToVerify = identity?.PasswordHash 
    ?? PasswordHelper.HashPassword("dummy_nonexistent_password_123!@#");
bool result = CryptographicOperations.FixedTimeEquals(newHash, originalHash);
```

## 📈 数据处理

### 商品生成
- 随机生成100个商品
- 价格范围：10-100元
- 包含60种商品类型（水果、饮料、日用品等）

### 购物车统计
```csharp
// 数据结构：商品名称 → (单价 → 数量)
Dictionary<string, Dictionary<decimal, int>> shoppingCart = new Dictionary<string, Dictionary<decimal, int>>();

// 输出格式示例：
// 名称：Apple        单价：15 ￥         x 3       |  总价： 45.00
```

## 🎨 输出示例
```
==================================== 客户: 张三 ====================================
=================================== 商品信息 ===================================
名称：Apple            单价：15 ￥         x 3       |  总价： 45.00
名称：Milk            单价：25 ￥         x 2       |  总价： 50.00
=================================== 总价格为 95.00  ===================================
```

## ⚙️ 配置说明

### 密码哈希参数
```csharp
private const int SaltSize = 16;        // 盐的长度（字节）
private const int HashSize = 32;        // 哈希长度（字节）
private const int Iterations = 600000;  // 迭代次数
private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
```

### 商品生成参数
- 默认商品数量：100
- 价格范围：10-100
- 商品名称库：60个预设名称

## 🐛 常见问题

### 1. 用户不存在时的处理
```csharp
try
{
    Customer customer = serviceSystem.SysCustomerAuthService.Login("wrongAccount", "wrongPassword");
}
catch
{
    Console.WriteLine("登录失败，用户不存在或密码错误");
}
```

### 2. 空购物车处理
```csharp
if (customer.GoodList == null || customer.GoodList.Count == 0)
{
    Console.WriteLine($"顾客{customer.Name} 没有买东西");
    return;
}
```

## 📄 文件说明

| 文件名 | 功能 | 关键类/接口 |
|--------|------|-------------|
| Program.cs | 程序入口 | Main方法 |
| ServiceSystem.cs | 系统集成 | ServiceSystem |
| CustomerAuthService.cs | 用户认证 | CustomerAuthService |
| GoodService.cs | 商品服务 | GoodService |
| CustomerService.cs | 客户服务 | CustomerService |
| PasswordHelper.cs | 密码工具 | PasswordHelper |
| GoodRepository.cs | 商品仓储 | GoodRepository |

## 🔄 扩展建议

1. **数据持久化**：添加数据库支持
2. **订单管理**：添加订单处理模块
3. **支付集成**：集成支付接口
4. **Web API**：转换为RESTful API服务
5. **用户界面**：添加Web或桌面客户端

## 📄 许可证
MIT License

## 🤝 贡献指南
欢迎提交Issue和Pull Request

---

**版本**: 1.0.0  
**最后更新**: 2026-04-12  
**开发环境**: .NET 6+  
**主要开发者**: 您的团队名称