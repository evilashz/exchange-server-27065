using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004B3 RID: 1203
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class CopyFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x000272E6 File Offset: 0x000254E6
		internal CopyFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x000272F9 File Offset: 0x000254F9
		public CopyFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CopyFolderResponseType)this.results[0];
			}
		}

		// Token: 0x040017DB RID: 6107
		private object[] results;
	}
}
