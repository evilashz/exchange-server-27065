using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000136 RID: 310
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetOrganizationRelationshipSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x0001857F File Offset: 0x0001677F
		internal GetOrganizationRelationshipSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00018592 File Offset: 0x00016792
		public GetOrganizationRelationshipSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetOrganizationRelationshipSettingsResponse)this.results[0];
			}
		}

		// Token: 0x040005F1 RID: 1521
		private object[] results;
	}
}
