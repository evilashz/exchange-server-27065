using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000414 RID: 1044
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetMailTipsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E95 RID: 7829 RVA: 0x0002AFAA File Offset: 0x000291AA
		internal GetMailTipsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0002AFBD File Offset: 0x000291BD
		public GetMailTipsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetMailTipsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013AA RID: 5034
		private object[] results;
	}
}
