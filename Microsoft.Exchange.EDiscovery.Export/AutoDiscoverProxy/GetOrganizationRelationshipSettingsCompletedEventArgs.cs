using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x020000A4 RID: 164
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	public class GetOrganizationRelationshipSettingsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0001FA73 File Offset: 0x0001DC73
		internal GetOrganizationRelationshipSettingsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0001FA86 File Offset: 0x0001DC86
		public GetOrganizationRelationshipSettingsResponse Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (GetOrganizationRelationshipSettingsResponse)this.results[0];
			}
		}

		// Token: 0x0400035A RID: 858
		private object[] results;
	}
}
