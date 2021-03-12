using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000422 RID: 1058
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class SetTeamMailboxCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EBF RID: 7871 RVA: 0x0002B0C2 File Offset: 0x000292C2
		internal SetTeamMailboxCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0002B0D5 File Offset: 0x000292D5
		public SetTeamMailboxResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SetTeamMailboxResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B1 RID: 5041
		private object[] results;
	}
}
