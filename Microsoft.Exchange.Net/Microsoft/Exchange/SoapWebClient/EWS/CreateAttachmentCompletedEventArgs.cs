using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004D5 RID: 1237
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x0002758E File Offset: 0x0002578E
		internal CreateAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000275A1 File Offset: 0x000257A1
		public CreateAttachmentResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateAttachmentResponseType)this.results[0];
			}
		}

		// Token: 0x040017EC RID: 6124
		private object[] results;
	}
}
