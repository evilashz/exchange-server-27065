using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200056F RID: 1391
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class InitUMMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x00028196 File Offset: 0x00026396
		internal InitUMMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x000281A9 File Offset: 0x000263A9
		public InitUMMailboxResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (InitUMMailboxResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001839 RID: 6201
		private object[] results;
	}
}
