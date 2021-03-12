using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200057F RID: 1407
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class UpdateGroupMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x000282D6 File Offset: 0x000264D6
		internal UpdateGroupMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x000282E9 File Offset: 0x000264E9
		public UpdateGroupMailboxResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateGroupMailboxResponseType)this.results[0];
			}
		}

		// Token: 0x04001841 RID: 6209
		private object[] results;
	}
}
