using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200086E RID: 2158
	[DataContract(Name = "SearchableMailbox", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SearchableMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchableMailbox : IComparable
	{
		// Token: 0x06003DD4 RID: 15828 RVA: 0x000D7F9D File Offset: 0x000D619D
		public SearchableMailbox()
		{
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000D7FA5 File Offset: 0x000D61A5
		internal SearchableMailbox(Guid guid, string primarySmtpAddress, bool isExternalMailbox, string externalEmailAddress, string displayName, bool isMembershipGroup, string referenceId)
		{
			this.Guid = guid;
			this.PrimarySmtpAddress = primarySmtpAddress;
			this.IsExternalMailbox = isExternalMailbox;
			this.ExternalEmailAddress = externalEmailAddress;
			this.DisplayName = displayName;
			this.IsMembershipGroup = isMembershipGroup;
			this.ReferenceId = referenceId;
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003DD6 RID: 15830 RVA: 0x000D7FE2 File Offset: 0x000D61E2
		// (set) Token: 0x06003DD7 RID: 15831 RVA: 0x000D7FEA File Offset: 0x000D61EA
		[DataMember(Name = "Guid", IsRequired = true)]
		[XmlElement("Guid")]
		public Guid Guid { get; set; }

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003DD8 RID: 15832 RVA: 0x000D7FF3 File Offset: 0x000D61F3
		// (set) Token: 0x06003DD9 RID: 15833 RVA: 0x000D7FFB File Offset: 0x000D61FB
		[XmlElement("PrimarySmtpAddress")]
		[DataMember(Name = "PrimarySmtpAddress", IsRequired = true)]
		public string PrimarySmtpAddress { get; set; }

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x000D8004 File Offset: 0x000D6204
		// (set) Token: 0x06003DDB RID: 15835 RVA: 0x000D800C File Offset: 0x000D620C
		[XmlElement("IsExternalMailbox")]
		[DataMember(Name = "IsExternalMailbox", IsRequired = true)]
		public bool IsExternalMailbox { get; set; }

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x000D8015 File Offset: 0x000D6215
		// (set) Token: 0x06003DDD RID: 15837 RVA: 0x000D801D File Offset: 0x000D621D
		[DataMember(Name = "ExternalEmailAddress", IsRequired = true)]
		[XmlElement("ExternalEmailAddress")]
		public string ExternalEmailAddress { get; set; }

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003DDE RID: 15838 RVA: 0x000D8026 File Offset: 0x000D6226
		// (set) Token: 0x06003DDF RID: 15839 RVA: 0x000D802E File Offset: 0x000D622E
		[DataMember(Name = "DisplayName", IsRequired = true)]
		[XmlElement("DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003DE0 RID: 15840 RVA: 0x000D8037 File Offset: 0x000D6237
		// (set) Token: 0x06003DE1 RID: 15841 RVA: 0x000D803F File Offset: 0x000D623F
		[XmlElement("IsMembershipGroup")]
		[DataMember(Name = "IsMembershipGroup", IsRequired = true)]
		public bool IsMembershipGroup { get; set; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000D8048 File Offset: 0x000D6248
		// (set) Token: 0x06003DE3 RID: 15843 RVA: 0x000D8050 File Offset: 0x000D6250
		[XmlElement("ReferenceId")]
		[DataMember(Name = "ReferenceId", IsRequired = true)]
		public string ReferenceId { get; set; }

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000D805C File Offset: 0x000D625C
		public override bool Equals(object obj)
		{
			SearchableMailbox searchableMailbox = obj as SearchableMailbox;
			return searchableMailbox != null && string.Compare(this.ReferenceId, searchableMailbox.ReferenceId, StringComparison.CurrentCultureIgnoreCase) == 0;
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000D808A File Offset: 0x000D628A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000D8094 File Offset: 0x000D6294
		public int CompareTo(object obj)
		{
			SearchableMailbox searchableMailbox = (SearchableMailbox)obj;
			return string.Compare(this.DisplayName, searchableMailbox.DisplayName);
		}
	}
}
