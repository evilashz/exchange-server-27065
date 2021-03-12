using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000559 RID: 1369
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CreateUMPromptCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x00027FDE File Offset: 0x000261DE
		internal CreateUMPromptCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00027FF1 File Offset: 0x000261F1
		public CreateUMPromptResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateUMPromptResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400182E RID: 6190
		private object[] results;
	}
}
