using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000571 RID: 1393
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class ResetUMMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001240 RID: 4672 RVA: 0x000281BE File Offset: 0x000263BE
		internal ResetUMMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000281D1 File Offset: 0x000263D1
		public ResetUMMailboxResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ResetUMMailboxResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400183A RID: 6202
		private object[] results;
	}
}
