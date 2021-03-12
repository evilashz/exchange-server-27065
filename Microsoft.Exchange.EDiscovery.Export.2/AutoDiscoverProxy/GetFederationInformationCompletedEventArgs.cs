using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x020000A2 RID: 162
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetFederationInformationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0001FA4B File Offset: 0x0001DC4B
		internal GetFederationInformationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0001FA5E File Offset: 0x0001DC5E
		public GetFederationInformationResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetFederationInformationResponse)this.results[0];
			}
		}

		// Token: 0x04000359 RID: 857
		private object[] results;
	}
}
