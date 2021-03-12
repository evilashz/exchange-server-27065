using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000446 RID: 1094
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SetHoldOnMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x0002B392 File Offset: 0x00029592
		internal SetHoldOnMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0002B3A5 File Offset: 0x000295A5
		public SetHoldOnMailboxesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetHoldOnMailboxesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013C3 RID: 5059
		private object[] results;
	}
}
