using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000409 RID: 1033
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "UpdateCreatePersonaRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateCreatePersonaRequestBase : BaseRequest
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x0009F449 File Offset: 0x0009D649
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x0009F451 File Offset: 0x0009D651
		[XmlElement("PropertyUpdates")]
		[DataMember(Name = "PropertyUpdates", IsRequired = false)]
		public PersonaPropertyUpdate[] PropertyUpdates
		{
			get
			{
				return this.propertyUpdates;
			}
			set
			{
				this.propertyUpdates = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0009F45A File Offset: 0x0009D65A
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x0009F462 File Offset: 0x0009D662
		[DataMember(Name = "PersonaId", IsRequired = false)]
		[XmlElement("PersonaId")]
		public ItemId PersonaId
		{
			get
			{
				return this.personaId;
			}
			set
			{
				this.personaId = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0009F46B File Offset: 0x0009D66B
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x0009F473 File Offset: 0x0009D673
		[DataMember(Name = "PersonTypeString", IsRequired = false)]
		[XmlElement("PersonTypeString")]
		public string PersonTypeString
		{
			get
			{
				return this.personTypeString;
			}
			set
			{
				this.personTypeString = value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0009F47C File Offset: 0x0009D67C
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x0009F484 File Offset: 0x0009D684
		[XmlElement("ParentFolderId")]
		[DataMember(Name = "ParentFolderId", IsRequired = false, EmitDefaultValue = false)]
		public TargetFolderId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
			set
			{
				this.parentFolderId = value;
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0009F48D File Offset: 0x0009D68D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0009F490 File Offset: 0x0009D690
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x04001338 RID: 4920
		private PersonaPropertyUpdate[] propertyUpdates;

		// Token: 0x04001339 RID: 4921
		private ItemId personaId;

		// Token: 0x0400133A RID: 4922
		private string personTypeString;

		// Token: 0x0400133B RID: 4923
		private TargetFolderId parentFolderId;
	}
}
