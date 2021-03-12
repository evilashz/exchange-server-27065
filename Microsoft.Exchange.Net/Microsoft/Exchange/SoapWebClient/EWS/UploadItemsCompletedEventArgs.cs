using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004A1 RID: 1185
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class UploadItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0002717E File Offset: 0x0002537E
		internal UploadItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00027191 File Offset: 0x00025391
		public UploadItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UploadItemsResponseType)this.results[0];
			}
		}

		// Token: 0x040017D2 RID: 6098
		private object[] results;
	}
}
