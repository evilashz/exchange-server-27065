using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200051B RID: 1307
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class UpdateInboxRulesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600113E RID: 4414 RVA: 0x00027B06 File Offset: 0x00025D06
		internal UpdateInboxRulesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00027B19 File Offset: 0x00025D19
		public UpdateInboxRulesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateInboxRulesResponseType)this.results[0];
			}
		}

		// Token: 0x0400180F RID: 6159
		private object[] results;
	}
}
