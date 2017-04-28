using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Sample_3.ORM;

namespace Sample_3
{
    class DbContextFactory
    {
        /// <summary>
        /// 实现对EF上下文实例进行管理，保证线程内唯一
        /// </summary>
        /// <returns></returns>
        public static ObjectContext GetCurrentDbContext()
        {
            //CallContext 是类似于方法调用的线程本地存储区的专用集合对象，并提供对每个逻辑执行线程都唯一的数据槽。 数据槽不在其他逻辑线程上的调用上下文之间共享。 当 CallContext 沿执行代码路径往返传播并且由该路径中的各个对象检查时，可将对象添加到其中。

            //当对另一个 AppDomain 中的对象进行远程方法调用时，CallContext 类将生成一个与该远程调用一起传播的 LogicalCallContext 实例。 只有公开 ILogicalThreadAffinative 接口并存储在 CallContext 中的对象被在 LogicalCallContext 中传播到 AppDomain 外部。 不支持此接口的对象不在 LogicalCallContext 实例中与远程方法调用一起传输。
            //暂时未考虑跨域情况
            ObjectContext context = (ObjectContext)CallContext.GetData("ReservationContent");

            if (context == null)
            {
                context = new ReservationContainer();
                context.Connection.Open();

                CallContext.SetData("ReservationContent", context);
            }

            return context;
        }
    }
}