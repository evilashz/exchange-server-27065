using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200052F RID: 1327
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class MarkAsJunkCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600117A RID: 4474 RVA: 0x00027C96 File Offset: 0x00025E96
		internal MarkAsJunkCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00027CA9 File Offset: 0x00025EA9
		public MarkAsJunkResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MarkAsJunkResponseType)this.results[0];
			}
		}

		// Token: 0x04001819 RID: 6169
		private object[] results;
	}
}
