using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004A7 RID: 1191
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CreateFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x000271F6 File Offset: 0x000253F6
		internal CreateFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00027209 File Offset: 0x00025409
		public CreateFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017D5 RID: 6101
		private object[] results;
	}
}
