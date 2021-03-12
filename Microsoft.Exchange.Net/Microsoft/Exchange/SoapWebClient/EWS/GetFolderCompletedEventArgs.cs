using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200049F RID: 1183
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00027156 File Offset: 0x00025356
		internal GetFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00027169 File Offset: 0x00025369
		public GetFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017D1 RID: 6097
		private object[] results;
	}
}
