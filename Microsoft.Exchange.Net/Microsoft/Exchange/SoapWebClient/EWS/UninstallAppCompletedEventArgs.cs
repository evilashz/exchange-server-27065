using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000551 RID: 1361
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class UninstallAppCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x00027F3E File Offset: 0x0002613E
		internal UninstallAppCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00027F51 File Offset: 0x00026151
		public UninstallAppResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UninstallAppResponseType)this.results[0];
			}
		}

		// Token: 0x0400182A RID: 6186
		private object[] results;
	}
}
