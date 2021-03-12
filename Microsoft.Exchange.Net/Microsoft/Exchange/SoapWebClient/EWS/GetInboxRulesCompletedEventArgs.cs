using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000519 RID: 1305
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetInboxRulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001138 RID: 4408 RVA: 0x00027ADE File Offset: 0x00025CDE
		internal GetInboxRulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00027AF1 File Offset: 0x00025CF1
		public GetInboxRulesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetInboxRulesResponseType)this.results[0];
			}
		}

		// Token: 0x0400180E RID: 6158
		private object[] results;
	}
}
