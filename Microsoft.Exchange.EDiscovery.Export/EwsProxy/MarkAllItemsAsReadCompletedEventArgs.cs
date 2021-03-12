using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200044C RID: 1100
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class MarkAllItemsAsReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F3D RID: 7997 RVA: 0x0002B40A File Offset: 0x0002960A
		internal MarkAllItemsAsReadCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0002B41D File Offset: 0x0002961D
		public MarkAllItemsAsReadResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MarkAllItemsAsReadResponseType)this.results[0];
			}
		}

		// Token: 0x040013C6 RID: 5062
		private object[] results;
	}
}
