using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004E7 RID: 1255
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class DeleteUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010A2 RID: 4258 RVA: 0x000276F6 File Offset: 0x000258F6
		internal DeleteUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00027709 File Offset: 0x00025909
		public DeleteUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040017F5 RID: 6133
		private object[] results;
	}
}
