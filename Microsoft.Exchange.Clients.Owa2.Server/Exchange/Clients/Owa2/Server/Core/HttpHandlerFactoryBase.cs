using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200024C RID: 588
	public abstract class HttpHandlerFactoryBase<TService> : IHttpHandlerFactory
	{
		// Token: 0x06001620 RID: 5664 RVA: 0x00050F9C File Offset: 0x0004F19C
		private static IHttpHandlerFactory CreateWcfHttpHandlerFactory()
		{
			IHttpHandlerFactory result;
			try
			{
				result = (IHttpHandlerFactory)Activator.CreateInstance("System.ServiceModel.Activation, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "System.ServiceModel.Activation.ServiceHttpHandlerFactory").Unwrap();
			}
			catch (Exception arg)
			{
				ExTraceGlobals.CoreTracer.TraceError<string, Exception>(0L, "An exception occurred while trying to load HttpHandlerFactory {0}. Exception: {1}.", "System.ServiceModel.Activation.ServiceHttpHandlerFactory", arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00050FF4 File Offset: 0x0004F1F4
		private static IHttpHandler GetWcfHttpHandler(HttpContext httpContext, string requestType, string url, string pathTranslated)
		{
			if (HttpHandlerFactoryBase<TService>.wcfHttpHandlerFactory.Member != null)
			{
				return HttpHandlerFactoryBase<TService>.wcfHttpHandlerFactory.Member.GetHandler(httpContext, requestType, url, pathTranslated);
			}
			return null;
		}

		// Token: 0x06001622 RID: 5666
		internal abstract bool UseHttpHandlerFactory(HttpContext httpContext);

		// Token: 0x06001623 RID: 5667
		internal abstract bool TryGetServiceMethod(string actionName, out ServiceMethodInfo methodInfo);

		// Token: 0x06001624 RID: 5668
		internal abstract TService CreateServiceInstance();

		// Token: 0x06001625 RID: 5669
		internal abstract IHttpAsyncHandler CreateAsyncHttpHandler(HttpContext httpContext, TService service, ServiceMethodInfo methodInfo);

		// Token: 0x06001626 RID: 5670
		internal abstract IHttpHandler CreateHttpHandler(HttpContext httpContext, TService service, ServiceMethodInfo methodInfo);

		// Token: 0x06001627 RID: 5671
		internal abstract string SelectOperation(string url, HttpRequest httpRequest, string requestType);

		// Token: 0x06001628 RID: 5672 RVA: 0x00051018 File Offset: 0x0004F218
		public virtual IHttpHandler GetHandler(HttpContext httpContext, string requestType, string url, string pathTranslated)
		{
			if (this.UseHttpHandlerFactory(httpContext))
			{
				string text = this.SelectOperation(url, httpContext.Request, requestType);
				ServiceMethodInfo serviceMethodInfo;
				if (!string.IsNullOrEmpty(text) && this.TryGetServiceMethod(text, out serviceMethodInfo))
				{
					TService service = this.CreateServiceInstance();
					if (serviceMethodInfo.IsAsyncPattern)
					{
						return this.CreateAsyncHttpHandler(httpContext, service, serviceMethodInfo);
					}
					return this.CreateHttpHandler(httpContext, service, serviceMethodInfo);
				}
			}
			return HttpHandlerFactoryBase<TService>.GetWcfHttpHandler(httpContext, requestType, url, pathTranslated);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0005107D File Offset: 0x0004F27D
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}

		// Token: 0x04000C48 RID: 3144
		private const string wcfHttpHandlerAssemblyName = "System.ServiceModel.Activation, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04000C49 RID: 3145
		private const string wcfHttpHandlerTypeName = "System.ServiceModel.Activation.ServiceHttpHandlerFactory";

		// Token: 0x04000C4A RID: 3146
		private static LazyMember<IHttpHandlerFactory> wcfHttpHandlerFactory = new LazyMember<IHttpHandlerFactory>(() => HttpHandlerFactoryBase<TService>.CreateWcfHttpHandlerFactory());
	}
}
