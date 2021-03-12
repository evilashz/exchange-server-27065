using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003E4 RID: 996
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class CreateItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E05 RID: 7685 RVA: 0x0002ABEA File Offset: 0x00028DEA
		internal CreateItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0002ABFD File Offset: 0x00028DFD
		public CreateItemResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateItemResponseType)this.results[0];
			}
		}

		// Token: 0x04001392 RID: 5010
		private object[] results;
	}
}
