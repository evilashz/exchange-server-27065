using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004B1 RID: 1201
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class MoveFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x000272BE File Offset: 0x000254BE
		internal MoveFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x000272D1 File Offset: 0x000254D1
		public MoveFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MoveFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017DA RID: 6106
		private object[] results;
	}
}
