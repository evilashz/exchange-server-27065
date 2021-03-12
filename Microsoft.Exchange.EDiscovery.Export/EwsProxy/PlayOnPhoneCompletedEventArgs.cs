using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000416 RID: 1046
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class PlayOnPhoneCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x0002AFD2 File Offset: 0x000291D2
		internal PlayOnPhoneCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0002AFE5 File Offset: 0x000291E5
		public PlayOnPhoneResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (PlayOnPhoneResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013AB RID: 5035
		private object[] results;
	}
}
