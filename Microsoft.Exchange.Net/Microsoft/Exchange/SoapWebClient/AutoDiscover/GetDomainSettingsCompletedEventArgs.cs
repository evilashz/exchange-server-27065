using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000132 RID: 306
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetDomainSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001852F File Offset: 0x0001672F
		internal GetDomainSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00018542 File Offset: 0x00016742
		public GetDomainSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetDomainSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040005EF RID: 1519
		private object[] results;
	}
}
