using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004A5 RID: 1189
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class ConvertIdCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x000271CE File Offset: 0x000253CE
		internal ConvertIdCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000271E1 File Offset: 0x000253E1
		public ConvertIdResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ConvertIdResponseType)this.results[0];
			}
		}

		// Token: 0x040017D4 RID: 6100
		private object[] results;
	}
}
