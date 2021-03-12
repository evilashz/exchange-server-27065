using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200042A RID: 1066
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class FindMessageTrackingReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001ED7 RID: 7895 RVA: 0x0002B162 File Offset: 0x00029362
		internal FindMessageTrackingReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0002B175 File Offset: 0x00029375
		public FindMessageTrackingReportResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindMessageTrackingReportResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B5 RID: 5045
		private object[] results;
	}
}
