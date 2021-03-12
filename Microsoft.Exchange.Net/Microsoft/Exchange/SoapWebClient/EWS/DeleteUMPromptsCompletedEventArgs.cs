using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200055B RID: 1371
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class DeleteUMPromptsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x00028006 File Offset: 0x00026206
		internal DeleteUMPromptsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00028019 File Offset: 0x00026219
		public DeleteUMPromptsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteUMPromptsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400182F RID: 6191
		private object[] results;
	}
}
