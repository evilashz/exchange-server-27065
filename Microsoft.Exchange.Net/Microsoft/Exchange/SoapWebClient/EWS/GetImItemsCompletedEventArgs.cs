using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000541 RID: 1345
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetImItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x00027DFE File Offset: 0x00025FFE
		internal GetImItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00027E11 File Offset: 0x00026011
		public GetImItemsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetImItemsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001822 RID: 6178
		private object[] results;
	}
}
