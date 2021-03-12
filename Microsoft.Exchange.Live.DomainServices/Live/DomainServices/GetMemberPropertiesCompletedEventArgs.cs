using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000037 RID: 55
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetMemberPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00006D04 File Offset: 0x00004F04
		internal GetMemberPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006D17 File Offset: 0x00004F17
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x040000B0 RID: 176
		private object[] results;
	}
}
