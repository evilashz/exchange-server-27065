using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004BF RID: 1215
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SyncFolderItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600102A RID: 4138 RVA: 0x000273D6 File Offset: 0x000255D6
		internal SyncFolderItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x000273E9 File Offset: 0x000255E9
		public SyncFolderItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SyncFolderItemsResponseType)this.results[0];
			}
		}

		// Token: 0x040017E1 RID: 6113
		private object[] results;
	}
}
