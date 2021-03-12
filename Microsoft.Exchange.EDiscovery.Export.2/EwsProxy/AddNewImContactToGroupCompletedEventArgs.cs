using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000452 RID: 1106
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class AddNewImContactToGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F4F RID: 8015 RVA: 0x0002B482 File Offset: 0x00029682
		internal AddNewImContactToGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x0002B495 File Offset: 0x00029695
		public AddNewImContactToGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddNewImContactToGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013C9 RID: 5065
		private object[] results;
	}
}
