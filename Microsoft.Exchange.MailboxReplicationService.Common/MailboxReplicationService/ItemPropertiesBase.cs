using System;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	[KnownType(typeof(AuditLogRecord))]
	[KnownType(typeof(MSInternal5))]
	[KnownType(typeof(OlcMessageProperties))]
	[KnownType(typeof(MSInternal1))]
	[KnownType(typeof(InboxRuleSettings))]
	[KnownType(typeof(FolderAcl))]
	[KnownType(typeof(CalendarItemProperties))]
	[KnownType(typeof(MsaSettings))]
	[KnownType(typeof(DeliverySettings))]
	[KnownType(typeof(UXSettings))]
	[KnownType(typeof(JunkEmailSettings))]
	[KnownType(typeof(PopAccountSettings))]
	[KnownType(typeof(CalendarUserSettings))]
	[KnownType(typeof(CalendarFolderSettings))]
	[KnownType(typeof(UnschematizedSettings))]
	[KnownType(typeof(SatchmoFolderSettings))]
	[KnownType(typeof(FeatureSetSettings))]
	[KnownType(typeof(OlcInboxRule))]
	[DataContract]
	[KnownType(typeof(AccountSettings))]
	internal abstract class ItemPropertiesBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public virtual void Apply(MrsPSHandler psHandler, MailboxSession mailboxSession)
		{
			throw new OlcSettingNotImplementedPermanentException("Mailbox", base.GetType().ToString());
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E7 File Offset: 0x000002E7
		public virtual void Apply(CoreFolder folder)
		{
			throw new OlcSettingNotImplementedPermanentException("Folder", base.GetType().ToString());
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020FE File Offset: 0x000002FE
		public virtual void Apply(MailboxSession session, Item item)
		{
			throw new OlcSettingNotImplementedPermanentException("Item", base.GetType().ToString());
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002115 File Offset: 0x00000315
		public virtual byte[] GetId()
		{
			return CommonUtils.GetSHA512Hash(this.ToString());
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = true,
				CheckCharacters = false
			};
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(ItemPropertiesBase));
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
			{
				dataContractSerializer.WriteObject(xmlWriter, this);
				xmlWriter.Flush();
			}
			return stringBuilder.ToString();
		}
	}
}
