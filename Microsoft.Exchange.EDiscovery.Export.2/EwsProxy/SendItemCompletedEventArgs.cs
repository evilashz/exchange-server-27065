using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003EC RID: 1004
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class SendItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x0002AC8A File Offset: 0x00028E8A
		internal SendItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0002AC9D File Offset: 0x00028E9D
		public SendItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SendItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001396 RID: 5014
		private object[] results;
	}
}
