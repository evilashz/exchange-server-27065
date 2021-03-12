using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200051F RID: 1311
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetSearchableMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600114A RID: 4426 RVA: 0x00027B56 File Offset: 0x00025D56
		internal GetSearchableMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00027B69 File Offset: 0x00025D69
		public GetSearchableMailboxesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetSearchableMailboxesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001811 RID: 6161
		private object[] results;
	}
}
