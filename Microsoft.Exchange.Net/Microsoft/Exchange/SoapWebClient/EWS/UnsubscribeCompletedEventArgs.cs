using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004B7 RID: 1207
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class UnsubscribeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x00027336 File Offset: 0x00025536
		internal UnsubscribeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00027349 File Offset: 0x00025549
		public UnsubscribeResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UnsubscribeResponseType)this.results[0];
			}
		}

		// Token: 0x040017DD RID: 6109
		private object[] results;
	}
}
