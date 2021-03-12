using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003D4 RID: 980
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SubscribeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001DD5 RID: 7637 RVA: 0x0002AAAA File Offset: 0x00028CAA
		internal SubscribeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0002AABD File Offset: 0x00028CBD
		public SubscribeResponseType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (SubscribeResponseType)this.results[0];
			}
		}

		// Token: 0x0400138A RID: 5002
		private object[] results;
	}
}
