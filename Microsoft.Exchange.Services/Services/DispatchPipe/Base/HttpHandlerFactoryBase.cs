using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services.DispatchPipe.Base
{
	// Token: 0x02000DCC RID: 3532
	public abstract class HttpHandlerFactoryBase<TService> : IHttpHandlerFactory
	{
		// Token: 0x060059E9 RID: 23017 RVA: 0x001187D4 File Offset: 0x001169D4
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

		// Token: 0x060059EA RID: 23018 RVA: 0x0011882C File Offset: 0x00116A2C
		private static IHttpHandler GetWcfHttpHandler(HttpContext httpContext, string requestType, string url, string pathTranslated)
		{
			if (HttpHandlerFactoryBase<TService>.wcfHttpHandlerFactory.Member != null)
			{
				return HttpHandlerFactoryBase<TService>.wcfHttpHandlerFactory.Member.GetHandler(httpContext, requestType, url, pathTranslated);
			}
			return null;
		}

		// Token: 0x060059EB RID: 23019
		internal abstract bool TryGetServiceMethod(string actionName, out ServiceMethodInfo methodInfo);

		// Token: 0x060059EC RID: 23020
		internal abstract TService CreateServiceInstance();

		// Token: 0x060059ED RID: 23021
		internal abstract IHttpAsyncHandler CreateAsyncHttpHandler(HttpContext httpContext, TService service, ServiceMethodInfo methodInfo);

		// Token: 0x060059EE RID: 23022
		internal abstract IHttpHandler CreateHttpHandler(HttpContext httpContext, TService service, ServiceMethodInfo methodInfo);

		// Token: 0x060059EF RID: 23023
		internal abstract string SelectOperation(string url, HttpContext httpContext, string requestType);

		// Token: 0x060059F0 RID: 23024
		internal abstract bool UseHttpHandlerFactory(HttpContext httpContext);

		// Token: 0x060059F1 RID: 23025 RVA: 0x0011894C File Offset: 0x00116B4C
		public virtual IHttpHandler GetHandler(HttpContext httpContext, string requestType, string url, string pathTranslated)
		{
			IHttpHandler httpHandler = null;
			ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
			{
				if (this.UseHttpHandlerFactory(httpContext))
				{
					string text = this.SelectOperation(url, httpContext, requestType);
					ServiceMethodInfo serviceMethodInfo;
					if (!string.IsNullOrEmpty(text) && this.TryGetServiceMethod(text, out serviceMethodInfo))
					{
						TService service = this.CreateServiceInstance();
						if (serviceMethodInfo.IsAsyncPattern)
						{
							httpHandler = this.CreateAsyncHttpHandler(httpContext, service, serviceMethodInfo);
						}
						else
						{
							httpHandler = this.CreateHttpHandler(httpContext, service, serviceMethodInfo);
						}
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "RequestHandler", "Ews");
					}
				}
				if (httpHandler == null)
				{
					httpHandler = HttpHandlerFactoryBase<TService>.GetWcfHttpHandler(httpContext, requestType, url, pathTranslated);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "RequestHandler", "Wcf");
				}
			});
			return httpHandler;
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x001189A1 File Offset: 0x00116BA1
		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}

		// Token: 0x040031BD RID: 12733
		private const string wcfHttpHandlerAssemblyName = "System.ServiceModel.Activation, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x040031BE RID: 12734
		private const string wcfHttpHandlerTypeName = "System.ServiceModel.Activation.ServiceHttpHandlerFactory";

		// Token: 0x040031BF RID: 12735
		private static LazyMember<IHttpHandlerFactory> wcfHttpHandlerFactory = new LazyMember<IHttpHandlerFactory>(() => HttpHandlerFactoryBase<TService>.CreateWcfHttpHandlerFactory());
	}
}
