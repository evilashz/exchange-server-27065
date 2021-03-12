using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003E0 RID: 992
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class CreateManagedFolderCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DF9 RID: 7673 RVA: 0x0002AB9A File Offset: 0x00028D9A
		internal CreateManagedFolderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0002ABAD File Offset: 0x00028DAD
		public CreateManagedFolderResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateManagedFolderResponseType)this.results[0];
			}
		}

		// Token: 0x04001390 RID: 5008
		private object[] results;
	}
}
