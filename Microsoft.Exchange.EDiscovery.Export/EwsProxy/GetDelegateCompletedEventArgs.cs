using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003FC RID: 1020
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x0002ADCA File Offset: 0x00028FCA
		internal GetDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x0002ADDD File Offset: 0x00028FDD
		public GetDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400139E RID: 5022
		private object[] results;
	}
}
