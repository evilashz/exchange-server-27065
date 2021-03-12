using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200004E RID: 78
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	public class EnumByodDomainsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00006DA4 File Offset: 0x00004FA4
		internal EnumByodDomainsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00006DB7 File Offset: 0x00004FB7
		public DomainInfoEx[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DomainInfoEx[])this.results[0];
			}
		}

		// Token: 0x040000B4 RID: 180
		private object[] results;
	}
}
