using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004ED RID: 1261
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetUserAvailabilityCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x0002776E File Offset: 0x0002596E
		internal GetUserAvailabilityCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00027781 File Offset: 0x00025981
		public GetUserAvailabilityResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserAvailabilityResponseType)this.results[0];
			}
		}

		// Token: 0x040017F8 RID: 6136
		private object[] results;
	}
}
