using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004B5 RID: 1205
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class SubscribeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x0002730E File Offset: 0x0002550E
		internal SubscribeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00027321 File Offset: 0x00025521
		public SubscribeResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SubscribeResponseType)this.results[0];
			}
		}

		// Token: 0x040017DC RID: 6108
		private object[] results;
	}
}
