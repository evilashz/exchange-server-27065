using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000581 RID: 1409
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class PostModernGroupItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x000282FE File Offset: 0x000264FE
		internal PostModernGroupItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00028311 File Offset: 0x00026511
		public PostModernGroupItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (PostModernGroupItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001842 RID: 6210
		private object[] results;
	}
}
