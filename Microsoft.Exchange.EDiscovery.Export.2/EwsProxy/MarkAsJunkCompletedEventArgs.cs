using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200044E RID: 1102
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class MarkAsJunkCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F43 RID: 8003 RVA: 0x0002B432 File Offset: 0x00029632
		internal MarkAsJunkCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0002B445 File Offset: 0x00029645
		public MarkAsJunkResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (MarkAsJunkResponseType)this.results[0];
			}
		}

		// Token: 0x040013C7 RID: 5063
		private object[] results;
	}
}
