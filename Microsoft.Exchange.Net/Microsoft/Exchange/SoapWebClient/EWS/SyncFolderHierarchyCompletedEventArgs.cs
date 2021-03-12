using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004BD RID: 1213
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SyncFolderHierarchyCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x000273AE File Offset: 0x000255AE
		internal SyncFolderHierarchyCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x000273C1 File Offset: 0x000255C1
		public SyncFolderHierarchyResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SyncFolderHierarchyResponseType)this.results[0];
			}
		}

		// Token: 0x040017E0 RID: 6112
		private object[] results;
	}
}
