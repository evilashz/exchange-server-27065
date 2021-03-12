using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000418 RID: 1048
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetPhoneCallInformationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EA1 RID: 7841 RVA: 0x0002AFFA File Offset: 0x000291FA
		internal GetPhoneCallInformationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0002B00D File Offset: 0x0002920D
		public GetPhoneCallInformationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetPhoneCallInformationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013AC RID: 5036
		private object[] results;
	}
}
