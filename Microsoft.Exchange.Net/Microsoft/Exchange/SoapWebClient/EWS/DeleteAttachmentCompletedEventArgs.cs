using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004D7 RID: 1239
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class DeleteAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x000275B6 File Offset: 0x000257B6
		internal DeleteAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000275C9 File Offset: 0x000257C9
		public DeleteAttachmentResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteAttachmentResponseType)this.results[0];
			}
		}

		// Token: 0x040017ED RID: 6125
		private object[] results;
	}
}
