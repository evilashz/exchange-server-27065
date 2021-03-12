using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004E9 RID: 1257
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x0002771E File Offset: 0x0002591E
		internal GetUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00027731 File Offset: 0x00025931
		public GetUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040017F6 RID: 6134
		private object[] results;
	}
}
