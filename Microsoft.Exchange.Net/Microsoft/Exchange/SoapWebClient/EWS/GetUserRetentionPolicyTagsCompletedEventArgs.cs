using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200054D RID: 1357
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetUserRetentionPolicyTagsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x00027EEE File Offset: 0x000260EE
		internal GetUserRetentionPolicyTagsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00027F01 File Offset: 0x00026101
		public GetUserRetentionPolicyTagsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserRetentionPolicyTagsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001828 RID: 6184
		private object[] results;
	}
}
