using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004C5 RID: 1221
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class CreateItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x0002744E File Offset: 0x0002564E
		internal CreateItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00027461 File Offset: 0x00025661
		public CreateItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateItemResponseType)this.results[0];
			}
		}

		// Token: 0x040017E4 RID: 6116
		private object[] results;
	}
}
