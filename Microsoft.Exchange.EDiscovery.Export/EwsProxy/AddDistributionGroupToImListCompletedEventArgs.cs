using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200045C RID: 1116
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class AddDistributionGroupToImListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F6D RID: 8045 RVA: 0x0002B54A File Offset: 0x0002974A
		internal AddDistributionGroupToImListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0002B55D File Offset: 0x0002975D
		public AddDistributionGroupToImListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddDistributionGroupToImListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013CE RID: 5070
		private object[] results;
	}
}
