using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200045A RID: 1114
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class AddImGroupCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F67 RID: 8039 RVA: 0x0002B522 File Offset: 0x00029722
		internal AddImGroupCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0002B535 File Offset: 0x00029735
		public AddImGroupResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddImGroupResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013CD RID: 5069
		private object[] results;
	}
}
