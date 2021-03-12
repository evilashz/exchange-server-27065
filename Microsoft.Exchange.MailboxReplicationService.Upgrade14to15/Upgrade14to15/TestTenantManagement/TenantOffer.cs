using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000AE RID: 174
	[DataContract(Name = "TenantOffer", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class TenantOffer : IExtensibleDataObject
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00008FC5 File Offset: 0x000071C5
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00008FCD File Offset: 0x000071CD
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00008FD6 File Offset: 0x000071D6
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x00008FDE File Offset: 0x000071DE
		[DataMember]
		public string Description
		{
			get
			{
				return this.DescriptionField;
			}
			set
			{
				this.DescriptionField = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00008FE7 File Offset: 0x000071E7
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x00008FEF File Offset: 0x000071EF
		[DataMember]
		public int LicenseCount
		{
			get
			{
				return this.LicenseCountField;
			}
			set
			{
				this.LicenseCountField = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00008FF8 File Offset: 0x000071F8
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00009000 File Offset: 0x00007200
		[DataMember]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00009009 File Offset: 0x00007209
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00009011 File Offset: 0x00007211
		[DataMember]
		public string OcpOffer
		{
			get
			{
				return this.OcpOfferField;
			}
			set
			{
				this.OcpOfferField = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000901A File Offset: 0x0000721A
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00009022 File Offset: 0x00007222
		[DataMember]
		public OfferType OfferType
		{
			get
			{
				return this.OfferTypeField;
			}
			set
			{
				this.OfferTypeField = value;
			}
		}

		// Token: 0x04000272 RID: 626
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000273 RID: 627
		private string DescriptionField;

		// Token: 0x04000274 RID: 628
		private int LicenseCountField;

		// Token: 0x04000275 RID: 629
		private string NameField;

		// Token: 0x04000276 RID: 630
		private string OcpOfferField;

		// Token: 0x04000277 RID: 631
		private OfferType OfferTypeField;
	}
}
