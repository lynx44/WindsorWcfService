using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Castle.DynamicProxy;
using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace WindsorWcfService
{
    public class Global : System.Web.HttpApplication
    {
        private static WindsorContainer container;

        protected void Application_Start(object sender, EventArgs e)
        {
            container = new WindsorContainer();

            container.Install(FromAssembly.This());

            container.AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero);
            var endpoint = WcfEndpoint.BoundTo(new BasicHttpBinding());
            //endpoint.Contract = typeof (IMyService);
            DefaultServiceModel serviceModel = new DefaultServiceModel();
                        serviceModel.AddEndpoints(endpoint);
            container.Register(Component.For<MyInterceptor>());
            container.Register(Component.For<IMyService>().ImplementedBy<MyService>().AsWcfService(serviceModel.Hosted()).IsDefault().LifestylePerWcfOperation().Interceptors<MyInterceptor>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}