using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200042E RID: 1070
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class FindConversationCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EE3 RID: 7907 RVA: 0x0002B1B2 File Offset: 0x000293B2
		internal FindConversationCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0002B1C5 File Offset: 0x000293C5
		public FindConversationResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindConversationResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013B7 RID: 5047
		private object[] results;
	}
}
