using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000517 RID: 1303
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetPersonaCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x00027AB6 File Offset: 0x00025CB6
		internal GetPersonaCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00027AC9 File Offset: 0x00025CC9
		public GetPersonaResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetPersonaResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400180D RID: 6157
		private object[] results;
	}
}
