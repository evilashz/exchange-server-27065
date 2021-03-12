using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000561 RID: 1377
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetClientExtensionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x0002807E File Offset: 0x0002627E
		internal GetClientExtensionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00028091 File Offset: 0x00026291
		public ClientExtensionResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ClientExtensionResponseType)this.results[0];
			}
		}

		// Token: 0x04001832 RID: 6194
		private object[] results;
	}
}
