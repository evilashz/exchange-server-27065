using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000456 RID: 1110
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class AddImContactToGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F5B RID: 8027 RVA: 0x0002B4D2 File Offset: 0x000296D2
		internal AddImContactToGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x0002B4E5 File Offset: 0x000296E5
		public AddImContactToGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddImContactToGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013CB RID: 5067
		private object[] results;
	}
}
