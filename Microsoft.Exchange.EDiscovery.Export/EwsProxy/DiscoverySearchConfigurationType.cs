using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200019A RID: 410
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DiscoverySearchConfigurationType
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000242CD File Offset: 0x000224CD
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000242D5 File Offset: 0x000224D5
		public string SearchId
		{
			get
			{
				return this.searchIdField;
			}
			set
			{
				this.searchIdField = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x000242DE File Offset: 0x000224DE
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x000242E6 File Offset: 0x000224E6
		public string SearchQuery
		{
			get
			{
				return this.searchQueryField;
			}
			set
			{
				this.searchQueryField = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x000242EF File Offset: 0x000224EF
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x000242F7 File Offset: 0x000224F7
		[XmlArrayItem("SearchableMailbox", IsNullable = false)]
		public SearchableMailboxType[] SearchableMailboxes
		{
			get
			{
				return this.searchableMailboxesField;
			}
			set
			{
				this.searchableMailboxesField = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00024300 File Offset: 0x00022500
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x00024308 File Offset: 0x00022508
		public string InPlaceHoldIdentity
		{
			get
			{
				return this.inPlaceHoldIdentityField;
			}
			set
			{
				this.inPlaceHoldIdentityField = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00024311 File Offset: 0x00022511
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x00024319 File Offset: 0x00022519
		public string ManagedByOrganization
		{
			get
			{
				return this.managedByOrganizationField;
			}
			set
			{
				this.managedByOrganizationField = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00024322 File Offset: 0x00022522
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x0002432A File Offset: 0x0002252A
		public string Language
		{
			get
			{
				return this.languageField;
			}
			set
			{
				this.languageField = value;
			}
		}

		// Token: 0x04000BFD RID: 3069
		private string searchIdField;

		// Token: 0x04000BFE RID: 3070
		private string searchQueryField;

		// Token: 0x04000BFF RID: 3071
		private SearchableMailboxType[] searchableMailboxesField;

		// Token: 0x04000C00 RID: 3072
		private string inPlaceHoldIdentityField;

		// Token: 0x04000C01 RID: 3073
		private string managedByOrganizationField;

		// Token: 0x04000C02 RID: 3074
		private string languageField;
	}
}
