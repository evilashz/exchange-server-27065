using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003DA RID: 986
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class GetStreamingEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0002AB22 File Offset: 0x00028D22
		internal GetStreamingEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0002AB35 File Offset: 0x00028D35
		public GetStreamingEventsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetStreamingEventsResponseType)this.results[0];
			}
		}

		// Token: 0x0400138D RID: 5005
		private object[] results;
	}
}
