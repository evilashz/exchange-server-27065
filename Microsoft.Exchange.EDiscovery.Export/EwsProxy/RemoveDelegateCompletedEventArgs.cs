using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000400 RID: 1024
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class RemoveDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x0002AE1A File Offset: 0x0002901A
		internal RemoveDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0002AE2D File Offset: 0x0002902D
		public RemoveDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013A0 RID: 5024
		private object[] results;
	}
}
