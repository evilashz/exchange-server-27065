using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200055F RID: 1375
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUMPromptNamesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x00028056 File Offset: 0x00026256
		internal GetUMPromptNamesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00028069 File Offset: 0x00026269
		public GetUMPromptNamesResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUMPromptNamesResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001831 RID: 6193
		private object[] results;
	}
}
