using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004DD RID: 1245
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x0002762E File Offset: 0x0002582E
		internal GetDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00027641 File Offset: 0x00025841
		public GetDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017F0 RID: 6128
		private object[] results;
	}
}
