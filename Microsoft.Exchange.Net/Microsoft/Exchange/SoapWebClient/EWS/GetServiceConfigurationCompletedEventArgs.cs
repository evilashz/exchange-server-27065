using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004F3 RID: 1267
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetServiceConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x000277E6 File Offset: 0x000259E6
		internal GetServiceConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x000277F9 File Offset: 0x000259F9
		public GetServiceConfigurationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetServiceConfigurationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017FB RID: 6139
		private object[] results;
	}
}
