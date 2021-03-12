using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E7 RID: 999
	[XmlType(TypeName = "DistinguishedFolderIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "DistinguishedFolderId")]
	public class DistinguishedFolderId : BaseFolderId
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0009DC20 File Offset: 0x0009BE20
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0009DC28 File Offset: 0x0009BE28
		[XmlElement]
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public EmailAddressWrapper Mailbox { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0009DC31 File Offset: 0x0009BE31
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0009DC39 File Offset: 0x0009BE39
		[XmlAttribute("Id")]
		[IgnoreDataMember]
		public DistinguishedFolderIdName Id { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0009DC42 File Offset: 0x0009BE42
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0009DC4F File Offset: 0x0009BE4F
		[XmlIgnore]
		[DataMember(Name = "Id", IsRequired = true, Order = 1)]
		public string IdString
		{
			get
			{
				return EnumUtilities.ToString<DistinguishedFolderIdName>(this.Id);
			}
			set
			{
				this.Id = EnumUtilities.Parse<DistinguishedFolderIdName>(value);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0009DC5D File Offset: 0x0009BE5D
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0009DC65 File Offset: 0x0009BE65
		[XmlAttribute]
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string ChangeKey { get; set; }

		// Token: 0x06001C09 RID: 7177 RVA: 0x0009DC6E File Offset: 0x0009BE6E
		public DistinguishedFolderId()
		{
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0009DC76 File Offset: 0x0009BE76
		public DistinguishedFolderId(EmailAddressWrapper emailAddress, DistinguishedFolderIdName distinguishedFolderIdName, string changeKey)
		{
			this.Mailbox = emailAddress;
			this.Id = distinguishedFolderIdName;
			this.ChangeKey = changeKey;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0009DC93 File Offset: 0x0009BE93
		public override string GetId()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0009DC9A File Offset: 0x0009BE9A
		public override string GetChangeKey()
		{
			return this.ChangeKey;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0009DCA2 File Offset: 0x0009BEA2
		protected override void InitServerInfo(bool isHierarchicalOperation)
		{
			base.SetServerInfo(IdConverter.GetServerInfoForDistinguishedFolderId(CallContext.Current, this, isHierarchicalOperation));
		}
	}
}
