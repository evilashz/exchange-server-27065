using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003E8 RID: 1000
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class UpdateItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E11 RID: 7697 RVA: 0x0002AC3A File Offset: 0x00028E3A
		internal UpdateItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0002AC4D File Offset: 0x00028E4D
		public UpdateItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001394 RID: 5012
		private object[] results;
	}
}
