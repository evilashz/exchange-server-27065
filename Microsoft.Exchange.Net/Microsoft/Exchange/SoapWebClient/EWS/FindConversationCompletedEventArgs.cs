using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200050F RID: 1295
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class FindConversationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x00027A16 File Offset: 0x00025C16
		internal FindConversationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00027A29 File Offset: 0x00025C29
		public FindConversationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindConversationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001809 RID: 6153
		private object[] results;
	}
}
