using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003B4 RID: 948
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ResolveNamesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001D75 RID: 7541 RVA: 0x0002A82A File Offset: 0x00028A2A
		internal ResolveNamesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0002A83D File Offset: 0x00028A3D
		public ResolveNamesResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ResolveNamesResponseType)this.results[0];
			}
		}

		// Token: 0x0400137A RID: 4986
		private object[] results;
	}
}
