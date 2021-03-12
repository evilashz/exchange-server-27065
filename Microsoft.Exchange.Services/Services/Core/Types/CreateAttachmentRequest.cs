using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000404 RID: 1028
	[KnownType(typeof(ItemIdAttachmentType))]
	[KnownType(typeof(ItemAttachmentType))]
	[KnownType(typeof(ReferenceAttachmentType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(FileAttachmentType))]
	[XmlType("CreateAttachmentType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateAttachmentRequest : BaseRequest
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0009EF71 File Offset: 0x0009D171
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x0009EF79 File Offset: 0x0009D179
		[DataMember(EmitDefaultValue = false)]
		[XmlIgnore]
		public bool RequireImageType { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0009EF82 File Offset: 0x0009D182
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x0009EF8A File Offset: 0x0009D18A
		[DataMember(EmitDefaultValue = false)]
		[XmlIgnore]
		public bool IncludeContentIdInResponse { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0009EF93 File Offset: 0x0009D193
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0009EF9B File Offset: 0x0009D19B
		[XmlElement("ParentItemId")]
		[DataMember(Name = "ParentItemId", IsRequired = true, Order = 1)]
		public ItemId ParentItemId { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0009EFA4 File Offset: 0x0009D1A4
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0009EFAC File Offset: 0x0009D1AC
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		public AttachmentType[] Attachments { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x0009EFB5 File Offset: 0x0009D1B5
		// (set) Token: 0x06001D2E RID: 7470 RVA: 0x0009EFBD File Offset: 0x0009D1BD
		[DataMember(EmitDefaultValue = false)]
		[XmlIgnore]
		public bool ClientSupportsIrm { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0009EFC6 File Offset: 0x0009D1C6
		// (set) Token: 0x06001D30 RID: 7472 RVA: 0x0009EFCE File Offset: 0x0009D1CE
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false)]
		public string CancellationId { get; set; }

		// Token: 0x06001D31 RID: 7473 RVA: 0x0009EFD7 File Offset: 0x0009D1D7
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateAttachment(callContext, this);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0009EFE0 File Offset: 0x0009D1E0
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.ParentItemId);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0009EFEE File Offset: 0x0009D1EE
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			if (this.parentResourceKey == null)
			{
				this.parentResourceKey = base.GetResourceKeysForItemId(true, callContext, this.ParentItemId);
			}
			return this.parentResourceKey;
		}

		// Token: 0x04001309 RID: 4873
		internal const string ElementName = "CreateAttachment";

		// Token: 0x0400130A RID: 4874
		internal const string ParentItemIdElementName = "ParentItemId";

		// Token: 0x0400130B RID: 4875
		private ResourceKey[] parentResourceKey;
	}
}
