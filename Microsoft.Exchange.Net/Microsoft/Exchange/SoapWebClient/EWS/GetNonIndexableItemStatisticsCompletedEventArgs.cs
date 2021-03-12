using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000529 RID: 1321
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetNonIndexableItemStatisticsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x00027C1E File Offset: 0x00025E1E
		internal GetNonIndexableItemStatisticsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00027C31 File Offset: 0x00025E31
		public GetNonIndexableItemStatisticsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetNonIndexableItemStatisticsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001816 RID: 6166
		private object[] results;
	}
}
