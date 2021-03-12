using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200040A RID: 1034
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UpdateUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E77 RID: 7799 RVA: 0x0002AEE2 File Offset: 0x000290E2
		internal UpdateUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0002AEF5 File Offset: 0x000290F5
		public UpdateUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040013A5 RID: 5029
		private object[] results;
	}
}
