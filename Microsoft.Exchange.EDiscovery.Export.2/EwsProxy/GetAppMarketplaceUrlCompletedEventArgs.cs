using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000474 RID: 1140
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetAppMarketplaceUrlCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001FB5 RID: 8117 RVA: 0x0002B72A File Offset: 0x0002992A
		internal GetAppMarketplaceUrlCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0002B73D File Offset: 0x0002993D
		public GetAppMarketplaceUrlResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetAppMarketplaceUrlResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013DA RID: 5082
		private object[] results;
	}
}
