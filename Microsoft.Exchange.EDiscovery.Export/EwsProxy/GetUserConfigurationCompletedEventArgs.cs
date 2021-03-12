using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000408 RID: 1032
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E71 RID: 7793 RVA: 0x0002AEBA File Offset: 0x000290BA
		internal GetUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x0002AECD File Offset: 0x000290CD
		public GetUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040013A4 RID: 5028
		private object[] results;
	}
}
