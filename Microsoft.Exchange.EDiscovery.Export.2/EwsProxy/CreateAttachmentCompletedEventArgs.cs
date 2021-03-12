using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003F4 RID: 1012
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CreateAttachmentCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x0002AD2A File Offset: 0x00028F2A
		internal CreateAttachmentCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0002AD3D File Offset: 0x00028F3D
		public CreateAttachmentResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CreateAttachmentResponseType)this.results[0];
			}
		}

		// Token: 0x0400139A RID: 5018
		private object[] results;
	}
}
