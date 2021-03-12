using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004B9 RID: 1209
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0002735E File Offset: 0x0002555E
		internal GetEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00027371 File Offset: 0x00025571
		public GetEventsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetEventsResponseType)this.results[0];
			}
		}

		// Token: 0x040017DE RID: 6110
		private object[] results;
	}
}
