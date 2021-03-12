using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000432 RID: 1074
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class GetConversationItemsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EEF RID: 7919 RVA: 0x0002B202 File Offset: 0x00029402
		internal GetConversationItemsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0002B215 File Offset: 0x00029415
		public GetConversationItemsResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetConversationItemsResponseType)this.results[0];
			}
		}

		// Token: 0x040013B9 RID: 5049
		private object[] results;
	}
}
