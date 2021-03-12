using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004BB RID: 1211
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetStreamingEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x00027386 File Offset: 0x00025586
		internal GetStreamingEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00027399 File Offset: 0x00025599
		public GetStreamingEventsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetStreamingEventsResponseType)this.results[0];
			}
		}

		// Token: 0x040017DF RID: 6111
		private object[] results;
	}
}
