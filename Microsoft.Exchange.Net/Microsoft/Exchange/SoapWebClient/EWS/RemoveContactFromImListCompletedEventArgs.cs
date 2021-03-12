using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000543 RID: 1347
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class RemoveContactFromImListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011B6 RID: 4534 RVA: 0x00027E26 File Offset: 0x00026026
		internal RemoveContactFromImListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00027E39 File Offset: 0x00026039
		public RemoveContactFromImListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveContactFromImListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001823 RID: 6179
		private object[] results;
	}
}
