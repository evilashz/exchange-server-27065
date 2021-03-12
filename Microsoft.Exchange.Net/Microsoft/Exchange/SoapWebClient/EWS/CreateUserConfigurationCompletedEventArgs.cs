using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020004E5 RID: 1253
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateUserConfigurationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600109C RID: 4252 RVA: 0x000276CE File Offset: 0x000258CE
		internal CreateUserConfigurationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x000276E1 File Offset: 0x000258E1
		public CreateUserConfigurationResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateUserConfigurationResponseType)this.results[0];
			}
		}

		// Token: 0x040017F4 RID: 6132
		private object[] results;
	}
}
