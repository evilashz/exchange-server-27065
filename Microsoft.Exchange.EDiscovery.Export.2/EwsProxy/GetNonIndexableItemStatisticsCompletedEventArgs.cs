using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000448 RID: 1096
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetNonIndexableItemStatisticsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F31 RID: 7985 RVA: 0x0002B3BA File Offset: 0x000295BA
		internal GetNonIndexableItemStatisticsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x0002B3CD File Offset: 0x000295CD
		public GetNonIndexableItemStatisticsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetNonIndexableItemStatisticsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013C4 RID: 5060
		private object[] results;
	}
}
