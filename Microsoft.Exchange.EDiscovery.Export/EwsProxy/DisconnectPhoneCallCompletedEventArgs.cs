using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200041A RID: 1050
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class DisconnectPhoneCallCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EA7 RID: 7847 RVA: 0x0002B022 File Offset: 0x00029222
		internal DisconnectPhoneCallCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0002B035 File Offset: 0x00029235
		public DisconnectPhoneCallResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DisconnectPhoneCallResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013AD RID: 5037
		private object[] results;
	}
}
