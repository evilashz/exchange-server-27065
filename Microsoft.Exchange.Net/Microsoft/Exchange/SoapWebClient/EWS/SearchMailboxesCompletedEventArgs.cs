using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000521 RID: 1313
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class SearchMailboxesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x00027B7E File Offset: 0x00025D7E
		internal SearchMailboxesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00027B91 File Offset: 0x00025D91
		public SearchMailboxesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SearchMailboxesResponseType)this.results[0];
			}
		}

		// Token: 0x04001812 RID: 6162
		private object[] results;
	}
}
