using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200043C RID: 1084
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetPasswordExpirationDateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F0D RID: 7949 RVA: 0x0002B2CA File Offset: 0x000294CA
		internal GetPasswordExpirationDateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0002B2DD File Offset: 0x000294DD
		public GetPasswordExpirationDateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetPasswordExpirationDateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013BE RID: 5054
		private object[] results;
	}
}
