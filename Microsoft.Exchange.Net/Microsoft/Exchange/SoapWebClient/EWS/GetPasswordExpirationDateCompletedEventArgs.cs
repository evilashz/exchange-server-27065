using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200051D RID: 1309
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetPasswordExpirationDateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001144 RID: 4420 RVA: 0x00027B2E File Offset: 0x00025D2E
		internal GetPasswordExpirationDateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00027B41 File Offset: 0x00025D41
		public GetPasswordExpirationDateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetPasswordExpirationDateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001810 RID: 6160
		private object[] results;
	}
}
