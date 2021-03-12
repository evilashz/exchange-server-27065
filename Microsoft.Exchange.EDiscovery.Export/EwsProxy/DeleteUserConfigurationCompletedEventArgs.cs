using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000406 RID: 1030
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class DeleteUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E6B RID: 7787 RVA: 0x0002AE92 File Offset: 0x00029092
		internal DeleteUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x0002AEA5 File Offset: 0x000290A5
		public DeleteUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040013A3 RID: 5027
		private object[] results;
	}
}
