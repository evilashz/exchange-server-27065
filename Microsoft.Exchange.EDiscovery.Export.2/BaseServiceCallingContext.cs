using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Services.Protocols;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200003F RID: 63
	internal abstract class BaseServiceCallingContext<ServiceBindingType> : IServiceCallingContext<ServiceBindingType> where ServiceBindingType : HttpWebClientProtocol, IServiceBinding, new()
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000869C File Offset: 0x0000689C
		static BaseServiceCallingContext()
		{
			string arg = "UnknownVersion";
			try
			{
				arg = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
			}
			catch (Exception ex)
			{
				Tracer.TraceError("BaseServiceCallingContext..Ctor: Failed to get assembly file version. {0}", new object[]
				{
					ex
				});
			}
			BaseServiceCallingContext<ServiceBindingType>.userAgent = string.Format("Exchange\\{0}\\EDiscovery", arg);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008700 File Offset: 0x00006900
		public virtual ServiceBindingType CreateServiceBinding(Uri serviceUrl)
		{
			ServiceBindingType serviceBindingType = BaseServiceCallingContext<ServiceBindingType>.CreateDefaultServiceBinding();
			this.SetServiceUrl(serviceBindingType, serviceUrl);
			return serviceBindingType;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000871C File Offset: 0x0000691C
		public virtual bool AuthorizeServiceBinding(ServiceBindingType binding)
		{
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000871F File Offset: 0x0000691F
		public virtual void SetServiceApiContext(ServiceBindingType binding, string mailboxEmailAddress)
		{
			binding.HttpContext = BaseServiceCallingContext<ServiceBindingType>.CreateHttpContext(mailboxEmailAddress);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008734 File Offset: 0x00006934
		public virtual void SetServiceUrl(ServiceBindingType binding, Uri targetUrl)
		{
			binding.Url = targetUrl.AbsoluteUri;
			binding.PreAuthenticate = (targetUrl.Scheme == Uri.UriSchemeHttps);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008766 File Offset: 0x00006966
		public virtual void SetServiceUrlAffinity(ServiceBindingType binding, Uri targetUrl)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008768 File Offset: 0x00006968
		protected static ServiceHttpContext CreateHttpContext(string mailboxEmailAddress)
		{
			return new ServiceHttpContext
			{
				ClientRequestId = Guid.NewGuid(),
				AnchorMailbox = mailboxEmailAddress
			};
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008790 File Offset: 0x00006990
		private static ServiceBindingType CreateDefaultServiceBinding()
		{
			ServiceBindingType result = Activator.CreateInstance<ServiceBindingType>();
			result.Timeout = 600000;
			result.PreAuthenticate = true;
			result.UserAgent = BaseServiceCallingContext<ServiceBindingType>.userAgent;
			return result;
		}

		// Token: 0x040000D9 RID: 217
		private static readonly string userAgent;
	}
}
