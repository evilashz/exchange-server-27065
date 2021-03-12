using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200046C RID: 1132
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserRetentionPolicyTagsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001F9D RID: 8093 RVA: 0x0002B68A File Offset: 0x0002988A
		internal GetUserRetentionPolicyTagsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0002B69D File Offset: 0x0002989D
		public GetUserRetentionPolicyTagsResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetUserRetentionPolicyTagsResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013D6 RID: 5078
		private object[] results;
	}
}
