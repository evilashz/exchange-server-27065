using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000555 RID: 1365
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetAppMarketplaceUrlCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011EC RID: 4588 RVA: 0x00027F8E File Offset: 0x0002618E
		internal GetAppMarketplaceUrlCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00027FA1 File Offset: 0x000261A1
		public GetAppMarketplaceUrlResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetAppMarketplaceUrlResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400182C RID: 6188
		private object[] results;
	}
}
