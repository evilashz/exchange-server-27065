using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004AD RID: 1197
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class EmptyFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002726E File Offset: 0x0002546E
		internal EmptyFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00027281 File Offset: 0x00025481
		public EmptyFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (EmptyFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017D8 RID: 6104
		private object[] results;
	}
}
