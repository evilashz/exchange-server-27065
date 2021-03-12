using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000402 RID: 1026
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class UpdateDelegateCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E5F RID: 7775 RVA: 0x0002AE42 File Offset: 0x00029042
		internal UpdateDelegateCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0002AE55 File Offset: 0x00029055
		public UpdateDelegateResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UpdateDelegateResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013A1 RID: 5025
		private object[] results;
	}
}
