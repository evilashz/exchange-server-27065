using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000638 RID: 1592
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchParametersType
	{
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x000B73C2 File Offset: 0x000B55C2
		// (set) Token: 0x060031AA RID: 12714 RVA: 0x000B73CA File Offset: 0x000B55CA
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public RestrictionType Restriction { get; set; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000B73D3 File Offset: 0x000B55D3
		// (set) Token: 0x060031AC RID: 12716 RVA: 0x000B73DB File Offset: 0x000B55DB
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[XmlArrayItem("FolderId", typeof(FolderId), IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), IsNullable = false)]
		public BaseFolderId[] BaseFolderIds { get; set; }

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000B73E4 File Offset: 0x000B55E4
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x000B73EC File Offset: 0x000B55EC
		[IgnoreDataMember]
		[XmlAttribute]
		public SearchFolderTraversalType Traversal { get; set; }

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x000B73F5 File Offset: 0x000B55F5
		// (set) Token: 0x060031B0 RID: 12720 RVA: 0x000B7402 File Offset: 0x000B5602
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Name = "Traversal", Order = 0)]
		public string TraversalString
		{
			get
			{
				return EnumUtilities.ToString<SearchFolderTraversalType>(this.Traversal);
			}
			set
			{
				this.Traversal = EnumUtilities.Parse<SearchFolderTraversalType>(value);
				this.TraversalSpecified = true;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000B7417 File Offset: 0x000B5617
		// (set) Token: 0x060031B2 RID: 12722 RVA: 0x000B741F File Offset: 0x000B561F
		[IgnoreDataMember]
		[XmlIgnore]
		public bool TraversalSpecified { get; set; }
	}
}
