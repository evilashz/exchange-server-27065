using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003F8 RID: 1016
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x0002AD7A File Offset: 0x00028F7A
		internal GetAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0002AD8D File Offset: 0x00028F8D
		public GetAttachmentResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetAttachmentResponseType)this.results[0];
			}
		}

		// Token: 0x0400139C RID: 5020
		private object[] results;
	}
}
