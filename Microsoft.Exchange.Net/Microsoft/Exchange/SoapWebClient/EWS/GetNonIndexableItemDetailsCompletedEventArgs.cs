using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200052B RID: 1323
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetNonIndexableItemDetailsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x00027C46 File Offset: 0x00025E46
		internal GetNonIndexableItemDetailsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x00027C59 File Offset: 0x00025E59
		public GetNonIndexableItemDetailsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetNonIndexableItemDetailsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001817 RID: 6167
		private object[] results;
	}
}
