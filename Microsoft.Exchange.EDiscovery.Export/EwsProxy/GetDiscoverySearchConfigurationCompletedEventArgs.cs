using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000442 RID: 1090
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetDiscoverySearchConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F1F RID: 7967 RVA: 0x0002B342 File Offset: 0x00029542
		internal GetDiscoverySearchConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0002B355 File Offset: 0x00029555
		public GetDiscoverySearchConfigurationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetDiscoverySearchConfigurationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013C1 RID: 5057
		private object[] results;
	}
}
