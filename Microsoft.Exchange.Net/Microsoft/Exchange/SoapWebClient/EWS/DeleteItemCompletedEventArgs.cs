using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004C7 RID: 1223
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class DeleteItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x00027476 File Offset: 0x00025676
		internal DeleteItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00027489 File Offset: 0x00025689
		public DeleteItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017E5 RID: 6117
		private object[] results;
	}
}
