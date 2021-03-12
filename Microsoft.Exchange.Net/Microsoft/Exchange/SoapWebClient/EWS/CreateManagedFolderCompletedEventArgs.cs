using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004C1 RID: 1217
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CreateManagedFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x000273FE File Offset: 0x000255FE
		internal CreateManagedFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00027411 File Offset: 0x00025611
		public CreateManagedFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateManagedFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017E2 RID: 6114
		private object[] results;
	}
}
