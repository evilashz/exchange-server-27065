using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200042C RID: 1068
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetMessageTrackingReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EDD RID: 7901 RVA: 0x0002B18A File Offset: 0x0002938A
		internal GetMessageTrackingReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0002B19D File Offset: 0x0002939D
		public GetMessageTrackingReportResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetMessageTrackingReportResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B6 RID: 5046
		private object[] results;
	}
}
