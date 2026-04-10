<!--
 * @Filename: 
 * @Author: Wang Xiao
 * @Date: 2026-04-09 21:00:51
 * @FilePath: /学习相关e:/CSharp_Project/Shopping/README.md
 * @Function: 
-->
# 购物模拟系统

## 项目概述

这是一个基于 C# 控制台实现的简单购物模拟系统。它模拟了一个拥有随机库存的“超市”，以及在此超市中进行随机购物的顾客。系统能够计算顾客购物车中商品的详细信息与总价。

## 项目结构与核心类说明

### 1. 数据模型层 (`Shopping.Model`)

此命名空间包含了系统的核心数据实体。

*   **`Person` (Person.cs)**
    *   **基础类**，定义了人员的基本属性：`Id`, `Name`, `Age`, `Birthday`。
    *   重写了 `ToString()` 方法以格式化输出。

*   **`Customer` (Customer.cs)**
    *   **继承自 `Person` 类**，代表顾客。
    *   核心属性：
        *   `public List<Good> GoodList`：存储顾客购买的商品列表。
*   **`Good` (Good.cs)**
    *   代表商品。
    *   核心属性：
        *   `public Dictionary<int, decimal> GoodInfo`：键值对，`Key` 为商品ID，`Value` 为商品单价。
        *   `public string? Name`：商品名称。
    *   重写了 `ToString()` 方法以格式化输出商品信息。

### 2. 数据仓库与工具层

*   **`GoodRepository` (GoodRepository.cs)**
    *   位于 `Shopping.Repository` 命名空间。
    *   职责：商品的数据仓库。在构造时，通过 `GenerateRandom` 工具生成指定数量的随机商品，并存储在 `MyShopGood` 列表属性中，作为“超市”的库存。

*   **`GenerateRandom` (GenerateRandom.cs)**
    *   位于 `Shopping.Tool` 命名空间。
    *   内部工具类，包含一个预设的商品名称列表。
    *   核心方法 `GenerateRandomGoods(int count)`：根据传入的数量 `count`，生成具有随机名称和价格（10-100之间）的商品列表。每个商品有唯一的ID。

### 3. 服务层 (`Shopping.Service`)

*   **`GoodService` (GoodService.cs)**
    *   系统的业务逻辑核心，直接依赖 `GoodRepository` 来获取库存数据。
    *   核心方法：
        1.  `QueryAllGoods()`：查询并打印超市库存中的所有商品。
        2.  `GetSomeGoods(int count)`：从超市库存中**随机抽取**指定数量 (`count`) 的商品，返回一个商品列表。这模拟了顾客的随机购买行为。
        3.  `GetGoodInfo(Customer customer)`：**服务方法**。接收一个 `Customer` 对象作为参数，统计并**格式化打印**该顾客购物车中所有商品的详细清单（包括名称、单价、数量、小计）以及最终的**购物总价**。这是系统的主要输出功能。
        4.  `GetTotalGoodsPrice(Customer customer)`：一个辅助方法，用于计算给定顾客购物车的总价格。

### 4. 程序入口 (`Program.cs`)

位于 `Shopping` 命名空间，是应用程序的启动点。

*   **流程**：
    1.  初始化 `GoodService`（它会自动创建包含100个随机商品的仓库）。
    2.  创建两个顾客：`Mike` 和 `John`。
    3.  **查询展示**：调用 `service.QueryAllGoods()` 打印超市所有商品。
    4.  **模拟购买**：为顾客 `Mike` 随机购买50件商品 (`P1.GoodList = service.GetSomeGoods(50)`)。
    5.  **生成账单**：调用 `service.GetGoodInfo(P1)` 为 `Mike` 生成并打印详细的购物清单和总价。

## 运行流程

1.  程序启动。
2.  控制台输出超市的100件商品库存。
3.  系统为顾客 `Mike` 随机选取50件商品加入其购物车 (`GoodList`)。
4.  系统详细列出 `Mike` 购买的商品，进行合并计算（相同商品合并数量），并输出最终的总金额。

## 类关系图

一个简化的核心类关系如下：

Program.cs (入口)
|
v
GoodService (业务逻辑核心)
|
|---依赖---> GoodRepository (数据仓库)
|               |
|               |---使用---> GenerateRandom (工具类)
|
|---操作---> Customer (模型)
|               |
|               |---继承---> Person (基类)
|
|---操作---> Good (模型)

## 如何使用

1.  此项目为一个标准的 C# 控制台应用程序。
2.  在 `Program.cs` 的 `Main` 方法中，你可以修改顾客名称、购买商品的数量 (`GetSomeGoods` 的参数) 来观察不同结果。
3.  运行程序后，请查看控制台输出，即可看到模拟的购物过程与账单。