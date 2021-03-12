using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004AB RID: 1195
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class DeleteFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x00027246 File Offset: 0x00025446
		internal DeleteFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00027259 File Offset: 0x00025459
		public DeleteFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017D7 RID: 6103
		private object[] results;
	}
}
