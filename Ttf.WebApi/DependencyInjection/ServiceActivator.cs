using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Ttf.WebApi.DependencyInjection
{
    /// <summary>
    /// Factory for creating controller instances using dependency injection.
    /// </summary>
    internal class ServiceActivator : IHttpControllerActivator
    {
        public ServiceActivator(HttpConfiguration configuration) { }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = IoC.Container.GetInstance(controllerType) as IHttpController;
            return controller;
        }
    }
}