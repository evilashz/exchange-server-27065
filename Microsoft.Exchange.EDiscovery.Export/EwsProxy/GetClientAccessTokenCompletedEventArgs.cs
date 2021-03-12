using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003FA RID: 1018
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetClientAccessTokenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E47 RID: 7751 RVA: 0x0002ADA2 File Offset: 0x00028FA2
		internal GetClientAccessTokenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0002ADB5 File Offset: 0x00028FB5
		public GetClientAccessTokenResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetClientAccessTokenResponseType)this.results[0];
			}
		}

		// Token: 0x0400139D RID: 5021
		private object[] results;
	}
}
