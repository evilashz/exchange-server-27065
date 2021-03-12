using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200053D RID: 1341
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class AddDistributionGroupToImListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x00027DAE File Offset: 0x00025FAE
		internal AddDistributionGroupToImListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00027DC1 File Offset: 0x00025FC1
		public AddDistributionGroupToImListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AddDistributionGroupToImListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001820 RID: 6176
		private object[] results;
	}
}
