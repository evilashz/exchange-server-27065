using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003F0 RID: 1008
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CopyItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E29 RID: 7721 RVA: 0x0002ACDA File Offset: 0x00028EDA
		internal CopyItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0002ACED File Offset: 0x00028EED
		public CopyItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CopyItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001398 RID: 5016
		private object[] results;
	}
}
