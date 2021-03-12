using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000430 RID: 1072
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ApplyConversationActionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EE9 RID: 7913 RVA: 0x0002B1DA File Offset: 0x000293DA
		internal ApplyConversationActionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0002B1ED File Offset: 0x000293ED
		public ApplyConversationActionResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ApplyConversationActionResponseType)this.results[0];
			}
		}

		// Token: 0x040013B8 RID: 5048
		private object[] results;
	}
}
