using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000412 RID: 1042
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetServiceConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x0002AF82 File Offset: 0x00029182
		internal GetServiceConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0002AF95 File Offset: 0x00029195
		public GetServiceConfigurationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetServiceConfigurationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013A9 RID: 5033
		private object[] results;
	}
}
