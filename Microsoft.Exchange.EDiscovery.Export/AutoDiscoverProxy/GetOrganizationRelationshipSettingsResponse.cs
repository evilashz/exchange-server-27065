using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200008F RID: 143
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetOrganizationRelationshipSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001F81D File Offset: 0x0001DA1D
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0001F825 File Offset: 0x0001DA25
		[XmlArray(IsNullable = true)]
		public OrganizationRelationshipSettings[] OrganizationRelationshipSettingsCollection
		{
			get
			{
				return this.organizationRelationshipSettingsCollectionField;
			}
			set
			{
				this.organizationRelationshipSettingsCollectionField = value;
			}
		}

		// Token: 0x0400033B RID: 827
		private OrganizationRelationshipSettings[] organizationRelationshipSettingsCollectionField;
	}
}
