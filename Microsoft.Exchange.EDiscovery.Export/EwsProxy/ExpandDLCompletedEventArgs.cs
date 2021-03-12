using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003B6 RID: 950
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class ExpandDLCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x0002A852 File Offset: 0x00028A52
		internal ExpandDLCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0002A865 File Offset: 0x00028A65
		public ExpandDLResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ExpandDLResponseType)this.results[0];
			}
		}

		// Token: 0x0400137B RID: 4987
		private object[] results;
	}
}
