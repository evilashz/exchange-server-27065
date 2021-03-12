using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004C3 RID: 1219
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x00027426 File Offset: 0x00025626
		internal GetItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00027439 File Offset: 0x00025639
		public GetItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017E3 RID: 6115
		private object[] results;
	}
}
