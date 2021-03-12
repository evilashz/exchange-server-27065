using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003EE RID: 1006
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class MoveItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E23 RID: 7715 RVA: 0x0002ACB2 File Offset: 0x00028EB2
		internal MoveItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x0002ACC5 File Offset: 0x00028EC5
		public MoveItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MoveItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001397 RID: 5015
		private object[] results;
	}
}
