using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200054F RID: 1359
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class InstallAppCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x00027F16 File Offset: 0x00026116
		internal InstallAppCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00027F29 File Offset: 0x00026129
		public InstallAppResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (InstallAppResponseType)this.results[0];
			}
		}

		// Token: 0x04001829 RID: 6185
		private object[] results;
	}
}
