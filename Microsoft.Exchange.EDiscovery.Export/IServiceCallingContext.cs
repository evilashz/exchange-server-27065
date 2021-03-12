using System;
using System.Web.Services.Protocols;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200003E RID: 62
	internal interface IServiceCallingContext<ServiceBindingType> where ServiceBindingType : HttpWebClientProtocol, IServiceBinding, new()
	{
		// Token: 0x06000275 RID: 629
		ServiceBindingType CreateServiceBinding(Uri serviceUrl);

		// Token: 0x06000276 RID: 630
		bool AuthorizeServiceBinding(ServiceBindingType binding);

		// Token: 0x06000277 RID: 631
		void SetServiceApiContext(ServiceBindingType binding, string mailboxEmailAddress);

		// Token: 0x06000278 RID: 632
		void SetServiceUrl(ServiceBindingType binding, Uri targetUrl);

		// Token: 0x06000279 RID: 633
		void SetServiceUrlAffinity(ServiceBindingType binding, Uri targetUrl);
	}
}
