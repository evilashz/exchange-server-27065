using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003E2 RID: 994
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DFF RID: 7679 RVA: 0x0002ABC2 File Offset: 0x00028DC2
		internal GetItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0002ABD5 File Offset: 0x00028DD5
		public GetItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001391 RID: 5009
		private object[] results;
	}
}
