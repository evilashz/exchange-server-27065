using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200050D RID: 1293
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetMessageTrackingReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x000279EE File Offset: 0x00025BEE
		internal GetMessageTrackingReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00027A01 File Offset: 0x00025C01
		public GetMessageTrackingReportResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetMessageTrackingReportResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001808 RID: 6152
		private object[] results;
	}
}
