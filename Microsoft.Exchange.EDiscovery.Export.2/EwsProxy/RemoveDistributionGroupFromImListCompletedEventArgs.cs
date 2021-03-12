using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000464 RID: 1124
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class RemoveDistributionGroupFromImListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F85 RID: 8069 RVA: 0x0002B5EA File Offset: 0x000297EA
		internal RemoveDistributionGroupFromImListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0002B5FD File Offset: 0x000297FD
		public RemoveDistributionGroupFromImListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveDistributionGroupFromImListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D2 RID: 5074
		private object[] results;
	}
}
