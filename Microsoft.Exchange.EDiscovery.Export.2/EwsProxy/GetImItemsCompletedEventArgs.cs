using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000460 RID: 1120
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetImItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F79 RID: 8057 RVA: 0x0002B59A File Offset: 0x0002979A
		internal GetImItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0002B5AD File Offset: 0x000297AD
		public GetImItemsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetImItemsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D0 RID: 5072
		private object[] results;
	}
}
