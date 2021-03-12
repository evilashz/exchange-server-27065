using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200056D RID: 1389
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetUMCallSummaryCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001234 RID: 4660 RVA: 0x0002816E File Offset: 0x0002636E
		internal GetUMCallSummaryCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00028181 File Offset: 0x00026381
		public GetUMCallSummaryResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUMCallSummaryResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001838 RID: 6200
		private object[] results;
	}
}
