using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000424 RID: 1060
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class UnpinTeamMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x0002B0EA File Offset: 0x000292EA
		internal UnpinTeamMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0002B0FD File Offset: 0x000292FD
		public UnpinTeamMailboxResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UnpinTeamMailboxResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B2 RID: 5042
		private object[] results;
	}
}
