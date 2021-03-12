using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000523 RID: 1315
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetDiscoverySearchConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00027BA6 File Offset: 0x00025DA6
		internal GetDiscoverySearchConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00027BB9 File Offset: 0x00025DB9
		public GetDiscoverySearchConfigurationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetDiscoverySearchConfigurationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001813 RID: 6163
		private object[] results;
	}
}
