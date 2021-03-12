using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000515 RID: 1301
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class FindPeopleCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600112C RID: 4396 RVA: 0x00027A8E File Offset: 0x00025C8E
		internal FindPeopleCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00027AA1 File Offset: 0x00025CA1
		public FindPeopleResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (FindPeopleResponseMessageType)this.results[0];
			}
		}

		// Token: 0x0400180C RID: 6156
		private object[] results;
	}
}
