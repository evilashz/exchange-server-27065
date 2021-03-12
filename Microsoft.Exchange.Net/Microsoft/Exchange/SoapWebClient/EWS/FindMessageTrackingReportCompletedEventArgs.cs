using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200050B RID: 1291
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class FindMessageTrackingReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x000279C6 File Offset: 0x00025BC6
		internal FindMessageTrackingReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x000279D9 File Offset: 0x00025BD9
		public FindMessageTrackingReportResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindMessageTrackingReportResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001807 RID: 6151
		private object[] results;
	}
}
