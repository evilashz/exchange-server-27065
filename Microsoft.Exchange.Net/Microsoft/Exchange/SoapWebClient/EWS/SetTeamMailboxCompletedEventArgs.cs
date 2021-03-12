using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000503 RID: 1283
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class SetTeamMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00027926 File Offset: 0x00025B26
		internal SetTeamMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00027939 File Offset: 0x00025B39
		public SetTeamMailboxResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetTeamMailboxResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001803 RID: 6147
		private object[] results;
	}
}
