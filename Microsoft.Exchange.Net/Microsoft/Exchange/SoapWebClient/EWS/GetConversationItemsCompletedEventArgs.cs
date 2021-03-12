using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000513 RID: 1299
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetConversationItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001126 RID: 4390 RVA: 0x00027A66 File Offset: 0x00025C66
		internal GetConversationItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00027A79 File Offset: 0x00025C79
		public GetConversationItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetConversationItemsResponseType)this.results[0];
			}
		}

		// Token: 0x0400180B RID: 6155
		private object[] results;
	}
}
