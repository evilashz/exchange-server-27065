using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003D6 RID: 982
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class UnsubscribeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DDB RID: 7643 RVA: 0x0002AAD2 File Offset: 0x00028CD2
		internal UnsubscribeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0002AAE5 File Offset: 0x00028CE5
		public UnsubscribeResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (UnsubscribeResponseType)this.results[0];
			}
		}

		// Token: 0x0400138B RID: 5003
		private object[] results;
	}
}
