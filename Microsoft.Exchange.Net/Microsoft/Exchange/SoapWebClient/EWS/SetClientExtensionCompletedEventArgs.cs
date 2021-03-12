using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000563 RID: 1379
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class SetClientExtensionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x000280A6 File Offset: 0x000262A6
		internal SetClientExtensionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000280B9 File Offset: 0x000262B9
		public SetClientExtensionResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetClientExtensionResponseType)this.results[0];
			}
		}

		// Token: 0x04001833 RID: 6195
		private object[] results;
	}
}
