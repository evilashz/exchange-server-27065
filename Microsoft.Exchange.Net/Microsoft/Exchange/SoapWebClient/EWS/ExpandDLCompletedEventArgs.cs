using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000497 RID: 1175
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class ExpandDLCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x000270B6 File Offset: 0x000252B6
		internal ExpandDLCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000270C9 File Offset: 0x000252C9
		public ExpandDLResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExpandDLResponseType)this.results[0];
			}
		}

		// Token: 0x040017CD RID: 6093
		private object[] results;
	}
}
