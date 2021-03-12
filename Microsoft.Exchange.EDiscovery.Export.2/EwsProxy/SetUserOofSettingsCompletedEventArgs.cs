using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000410 RID: 1040
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class SetUserOofSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E89 RID: 7817 RVA: 0x0002AF5A File Offset: 0x0002915A
		internal SetUserOofSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0002AF6D File Offset: 0x0002916D
		public SetUserOofSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetUserOofSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040013A8 RID: 5032
		private object[] results;
	}
}
