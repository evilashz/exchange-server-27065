using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000462 RID: 1122
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class RemoveContactFromImListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F7F RID: 8063 RVA: 0x0002B5C2 File Offset: 0x000297C2
		internal RemoveContactFromImListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0002B5D5 File Offset: 0x000297D5
		public RemoveContactFromImListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveContactFromImListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D1 RID: 5073
		private object[] results;
	}
}
