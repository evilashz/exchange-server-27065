using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000545 RID: 1349
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RemoveDistributionGroupFromImListCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060011BC RID: 4540 RVA: 0x00027E4E File Offset: 0x0002604E
		internal RemoveDistributionGroupFromImListCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00027E61 File Offset: 0x00026061
		public RemoveDistributionGroupFromImListResponseMessageType Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (RemoveDistributionGroupFromImListResponseMessageType)this.results[0];
			}
		}

		// Token: 0x04001824 RID: 6180
		private object[] results;
	}
}
