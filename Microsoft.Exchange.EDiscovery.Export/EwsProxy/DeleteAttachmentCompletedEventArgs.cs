using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003F6 RID: 1014
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class DeleteAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x0002AD52 File Offset: 0x00028F52
		internal DeleteAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0002AD65 File Offset: 0x00028F65
		public DeleteAttachmentResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DeleteAttachmentResponseType)this.results[0];
			}
		}

		// Token: 0x0400139B RID: 5019
		private object[] results;
	}
}
