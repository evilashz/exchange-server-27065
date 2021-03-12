using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200043A RID: 1082
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class UpdateInboxRulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x0002B2A2 File Offset: 0x000294A2
		internal UpdateInboxRulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0002B2B5 File Offset: 0x000294B5
		public UpdateInboxRulesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateInboxRulesResponseType)this.results[0];
			}
		}

		// Token: 0x040013BD RID: 5053
		private object[] results;
	}
}
