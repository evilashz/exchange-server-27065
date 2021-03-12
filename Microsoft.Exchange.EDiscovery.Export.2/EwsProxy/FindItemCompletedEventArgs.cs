using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003BC RID: 956
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class FindItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D8D RID: 7565 RVA: 0x0002A8CA File Offset: 0x00028ACA
		internal FindItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0002A8DD File Offset: 0x00028ADD
		public FindItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindItemResponseType)this.results[0];
			}
		}

		// Token: 0x0400137E RID: 4990
		private object[] results;
	}
}
