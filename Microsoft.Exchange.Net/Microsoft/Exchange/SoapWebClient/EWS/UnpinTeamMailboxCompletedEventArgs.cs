using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000505 RID: 1285
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class UnpinTeamMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x0002794E File Offset: 0x00025B4E
		internal UnpinTeamMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00027961 File Offset: 0x00025B61
		public UnpinTeamMailboxResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UnpinTeamMailboxResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001804 RID: 6148
		private object[] results;
	}
}
