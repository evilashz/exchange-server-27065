using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000438 RID: 1080
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetInboxRulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F01 RID: 7937 RVA: 0x0002B27A File Offset: 0x0002947A
		internal GetInboxRulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0002B28D File Offset: 0x0002948D
		public GetInboxRulesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetInboxRulesResponseType)this.results[0];
			}
		}

		// Token: 0x040013BC RID: 5052
		private object[] results;
	}
}
