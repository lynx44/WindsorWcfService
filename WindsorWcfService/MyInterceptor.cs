using Castle.DynamicProxy;

namespace WindsorWcfService
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            invocation.ReturnValue = "correct";
        }
    }
}