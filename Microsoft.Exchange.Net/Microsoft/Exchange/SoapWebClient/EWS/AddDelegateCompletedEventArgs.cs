using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004DF RID: 1247
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class AddDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x00027656 File Offset: 0x00025856
		internal AddDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00027669 File Offset: 0x00025869
		public AddDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040017F1 RID: 6129
		private object[] results;
	}
}
