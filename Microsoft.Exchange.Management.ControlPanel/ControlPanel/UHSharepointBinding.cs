using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004A2 RID: 1186
	[DataContract]
	public class UHSharepointBinding
	{
		// Token: 0x06003AF5 RID: 15093 RVA: 0x000B26E4 File Offset: 0x000B08E4
		public UHSharepointBinding(BindingMetadata sharepointBinding)
		{
			ArgumentValidator.ThrowIfNull("sharepointBinding", sharepointBinding);
			this.siteUrl = sharepointBinding.Name;
		}

		// Token: 0x1700234C RID: 9036
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000B2703 File Offset: 0x000B0903
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x000B270B File Offset: 0x000B090B
		[DataMember]
		public string SiteUrl
		{
			get
			{
				return this.siteUrl;
			}
			set
			{
				this.siteUrl = value;
			}
		}

		// Token: 0x04002741 RID: 10049
		private string siteUrl;
	}
}
