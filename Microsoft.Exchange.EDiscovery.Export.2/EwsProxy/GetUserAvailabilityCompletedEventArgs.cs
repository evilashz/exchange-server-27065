using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200040C RID: 1036
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class GetUserAvailabilityCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E7D RID: 7805 RVA: 0x0002AF0A File Offset: 0x0002910A
		internal GetUserAvailabilityCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0002AF1D File Offset: 0x0002911D
		public GetUserAvailabilityResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserAvailabilityResponseType)this.results[0];
			}
		}

		// Token: 0x040013A6 RID: 5030
		private object[] results;
	}
}
