using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200045E RID: 1118
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetImItemListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x0002B572 File Offset: 0x00029772
		internal GetImItemListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0002B585 File Offset: 0x00029785
		public GetImItemListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetImItemListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013CF RID: 5071
		private object[] results;
	}
}
