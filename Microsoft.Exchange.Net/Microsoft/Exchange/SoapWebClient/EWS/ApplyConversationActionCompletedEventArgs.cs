using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000511 RID: 1297
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class ApplyConversationActionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x00027A3E File Offset: 0x00025C3E
		internal ApplyConversationActionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00027A51 File Offset: 0x00025C51
		public ApplyConversationActionResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ApplyConversationActionResponseType)this.results[0];
			}
		}

		// Token: 0x0400180A RID: 6154
		private object[] results;
	}
}
