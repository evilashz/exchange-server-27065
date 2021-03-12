using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000525 RID: 1317
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetHoldOnMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x00027BCE File Offset: 0x00025DCE
		internal GetHoldOnMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00027BE1 File Offset: 0x00025DE1
		public GetHoldOnMailboxesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetHoldOnMailboxesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001814 RID: 6164
		private object[] results;
	}
}
