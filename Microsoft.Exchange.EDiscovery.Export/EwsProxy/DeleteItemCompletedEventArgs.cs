using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003E6 RID: 998
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class DeleteItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E0B RID: 7691 RVA: 0x0002AC12 File Offset: 0x00028E12
		internal DeleteItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0002AC25 File Offset: 0x00028E25
		public DeleteItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001393 RID: 5011
		private object[] results;
	}
}
