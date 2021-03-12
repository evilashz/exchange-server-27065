using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000468 RID: 1128
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetImGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F91 RID: 8081 RVA: 0x0002B63A File Offset: 0x0002983A
		internal SetImGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x0002B64D File Offset: 0x0002984D
		public SetImGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetImGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D4 RID: 5076
		private object[] results;
	}
}
