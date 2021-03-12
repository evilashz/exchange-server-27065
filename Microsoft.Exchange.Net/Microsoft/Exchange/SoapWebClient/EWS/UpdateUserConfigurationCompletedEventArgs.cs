using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004EB RID: 1259
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class UpdateUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x00027746 File Offset: 0x00025946
		internal UpdateUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00027759 File Offset: 0x00025959
		public UpdateUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040017F7 RID: 6135
		private object[] results;
	}
}
