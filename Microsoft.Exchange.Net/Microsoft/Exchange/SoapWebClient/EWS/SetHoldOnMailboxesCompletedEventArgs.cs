using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000527 RID: 1319
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SetHoldOnMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x00027BF6 File Offset: 0x00025DF6
		internal SetHoldOnMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00027C09 File Offset: 0x00025E09
		public SetHoldOnMailboxesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetHoldOnMailboxesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001815 RID: 6165
		private object[] results;
	}
}
