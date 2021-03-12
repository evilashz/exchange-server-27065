using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000434 RID: 1076
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class FindPeopleCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001EF5 RID: 7925 RVA: 0x0002B22A File Offset: 0x0002942A
		internal FindPeopleCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0002B23D File Offset: 0x0002943D
		public FindPeopleResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindPeopleResponseMessageType)this.results[0];
			}
		}

		// Token: 0x040013BA RID: 5050
		private object[] results;
	}
}
