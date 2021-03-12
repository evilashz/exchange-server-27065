using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000553 RID: 1363
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class DisableAppCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011E6 RID: 4582 RVA: 0x00027F66 File Offset: 0x00026166
		internal DisableAppCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00027F79 File Offset: 0x00026179
		public DisableAppResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DisableAppResponseType)this.results[0];
			}
		}

		// Token: 0x0400182B RID: 6187
		private object[] results;
	}
}
