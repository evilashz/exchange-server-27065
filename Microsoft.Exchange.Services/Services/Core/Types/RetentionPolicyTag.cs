using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E2 RID: 994
	[XmlType(TypeName = "RetentionPolicyTagType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "RetentionPolicyTag", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RetentionPolicyTag
	{
		// Token: 0x06001BDE RID: 7134 RVA: 0x0009DA21 File Offset: 0x0009BC21
		public RetentionPolicyTag()
		{
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0009DA2C File Offset: 0x0009BC2C
		internal RetentionPolicyTag(PolicyTag policyTag) : this(policyTag.Name, policyTag.PolicyGuid, policyTag.TimeSpanForRetention.Days, (ElcFolderType)policyTag.Type, (RetentionActionType)policyTag.RetentionAction, policyTag.Description, policyTag.IsVisible, policyTag.OptedInto, policyTag.IsArchive)
		{
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0009DA80 File Offset: 0x0009BC80
		internal RetentionPolicyTag(string displayName, Guid retentionId, int retentionPeriod, ElcFolderType type, RetentionActionType retentionAction, string description, bool isVisible, bool optedInto, bool isArchive)
		{
			this.DisplayName = displayName;
			this.RetentionId = retentionId;
			this.RetentionPeriod = retentionPeriod;
			this.Type = type;
			this.RetentionAction = retentionAction;
			this.IsVisible = isVisible;
			this.OptedInto = optedInto;
			this.IsArchive = isArchive;
			if (!string.IsNullOrEmpty(description))
			{
				this.Description = description;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0009DAE1 File Offset: 0x0009BCE1
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x0009DAE9 File Offset: 0x0009BCE9
		[XmlElement("DisplayName")]
		[DataMember(Name = "DisplayName", IsRequired = true)]
		public string DisplayName { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0009DAF2 File Offset: 0x0009BCF2
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x0009DAFA File Offset: 0x0009BCFA
		[XmlElement("RetentionId")]
		[DataMember(Name = "RetentionId", IsRequired = true)]
		public Guid RetentionId { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0009DB03 File Offset: 0x0009BD03
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0009DB0B File Offset: 0x0009BD0B
		[DataMember(Name = "RetentionPeriod", IsRequired = true)]
		[XmlElement("RetentionPeriod")]
		public int RetentionPeriod { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0009DB14 File Offset: 0x0009BD14
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x0009DB1C File Offset: 0x0009BD1C
		[XmlElement("Type")]
		[DataMember(Name = "Type", IsRequired = true)]
		public ElcFolderType Type { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0009DB25 File Offset: 0x0009BD25
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x0009DB2D File Offset: 0x0009BD2D
		[XmlElement("RetentionAction")]
		[DataMember(Name = "RetentionAction", IsRequired = true)]
		public RetentionActionType RetentionAction { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0009DB36 File Offset: 0x0009BD36
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0009DB3E File Offset: 0x0009BD3E
		[DataMember(Name = "Description", IsRequired = false)]
		[XmlElement("Description")]
		public string Description { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0009DB47 File Offset: 0x0009BD47
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x0009DB4F File Offset: 0x0009BD4F
		[DataMember(Name = "IsVisible", IsRequired = true)]
		[XmlElement("IsVisible")]
		public bool IsVisible { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0009DB58 File Offset: 0x0009BD58
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x0009DB60 File Offset: 0x0009BD60
		[DataMember(Name = "OptedInto", IsRequired = true)]
		[XmlElement("OptedInto")]
		public bool OptedInto { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0009DB69 File Offset: 0x0009BD69
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x0009DB71 File Offset: 0x0009BD71
		[DataMember(Name = "IsArchive", IsRequired = true)]
		[XmlElement("IsArchive")]
		public bool IsArchive { get; set; }
	}
}
