using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x020000A0 RID: 160
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetDomainSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x0001FA23 File Offset: 0x0001DC23
		internal GetDomainSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001FA36 File Offset: 0x0001DC36
		public GetDomainSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetDomainSettingsResponse)this.results[0];
			}
		}

		// Token: 0x04000358 RID: 856
		private object[] results;
	}
}
