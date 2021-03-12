using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxData : IMailboxData, IMigrationSerializable
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000B492 File Offset: 0x00009692
		public MailboxData(Guid mailboxGuid, Fqdn mailboxServer, string mailboxLegacyDN, ADObjectId mailboxADObjectId, Guid exchangeObjectId) : this(mailboxGuid, Guid.Empty, mailboxServer, mailboxLegacyDN, mailboxADObjectId, exchangeObjectId)
		{
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B4A8 File Offset: 0x000096A8
		public MailboxData(Guid mailboxGuid, Guid mailboxDatabaseGuid, Fqdn mailboxServer, string mailboxLegacyDN, ADObjectId mailboxADObjectId, Guid exchangeObjectId)
		{
			MigrationUtil.ThrowOnNullArgument(mailboxGuid, "mailboxGuid");
			MigrationUtil.ThrowOnNullArgument(mailboxServer, "mailboxServer");
			MigrationUtil.ThrowOnNullOrEmptyArgument(mailboxLegacyDN, "mailboxLegacyDN");
			MigrationUtil.ThrowOnGuidEmptyArgument(mailboxGuid, "mailboxGuid");
			MigrationUtil.ThrowOnNullArgument(mailboxADObjectId, "mailboxADObjectId");
			MigrationUtil.ThrowOnGuidEmptyArgument(exchangeObjectId, "exchangeObjectId");
			this.UserMailboxId = mailboxGuid;
			this.UserMailboxDatabaseId = mailboxDatabaseGuid;
			this.MailboxServer = mailboxServer;
			this.MailboxLegacyDN = mailboxLegacyDN;
			this.UserMailboxADObjectId = mailboxADObjectId;
			this.ExchangeObjectId = exchangeObjectId;
			this.recipientType = null;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B540 File Offset: 0x00009740
		public MailboxData(Guid mailboxGuid, string mailboxLegacyDn, ADObjectId mailboxADObjectId, Guid exchangeObjectId)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(mailboxGuid, "mailboxGuid");
			MigrationUtil.ThrowOnNullOrEmptyArgument(mailboxLegacyDn, "mailboxLegacyDn");
			MigrationUtil.ThrowOnNullArgument(mailboxADObjectId, "mailboxADObjectId");
			MigrationUtil.ThrowOnGuidEmptyArgument(exchangeObjectId, "exchangeObjectId");
			this.UserMailboxId = mailboxGuid;
			this.MailboxLegacyDN = mailboxLegacyDn;
			this.UserMailboxADObjectId = mailboxADObjectId;
			this.ExchangeObjectId = exchangeObjectId;
			this.recipientType = null;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B5A9 File Offset: 0x000097A9
		internal MailboxData()
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000B5B1 File Offset: 0x000097B1
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000B5B9 File Offset: 0x000097B9
		public Guid UserMailboxId { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000B5C2 File Offset: 0x000097C2
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000B5CA File Offset: 0x000097CA
		public Guid UserMailboxDatabaseId { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000B5D3 File Offset: 0x000097D3
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000B5DB File Offset: 0x000097DB
		public Fqdn MailboxServer { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000B5E4 File Offset: 0x000097E4
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000B5EC File Offset: 0x000097EC
		public string MailboxLegacyDN { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000B5F5 File Offset: 0x000097F5
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000B5FD File Offset: 0x000097FD
		public ADObjectId UserMailboxADObjectId { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B606 File Offset: 0x00009806
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000B60E File Offset: 0x0000980E
		public Guid ExchangeObjectId { get; private set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000B617 File Offset: 0x00009817
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000B61F File Offset: 0x0000981F
		public string Name { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000B628 File Offset: 0x00009828
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000B630 File Offset: 0x00009830
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B639 File Offset: 0x00009839
		public string MailboxIdentifier
		{
			get
			{
				if (this.ExchangeObjectId != Guid.Empty)
				{
					return MailboxData.CreateMailboxIdentifierString(this.OrganizationId, this.ExchangeObjectId);
				}
				return this.UserMailboxADObjectId.DistinguishedName;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000B66A File Offset: 0x0000986A
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				if (this.recipientType != null)
				{
					return this.recipientType.Value;
				}
				if (string.IsNullOrEmpty(this.MailboxServer))
				{
					return MigrationUserRecipientType.Mailuser;
				}
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B69A File Offset: 0x0000989A
		PropertyDefinition[] IMigrationSerializable.PropertyDefinitions
		{
			get
			{
				return MailboxData.MailboxDataPropertyDefinition;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B6A4 File Offset: 0x000098A4
		public static MailboxData CreateFromADUser(ADUser user)
		{
			return new MailboxData
			{
				ExchangeObjectId = user.ExchangeObjectId,
				MailboxLegacyDN = user.LegacyExchangeDN,
				MailboxServer = Fqdn.Parse(user.ServerName),
				Name = user.Name,
				OrganizationId = user.OrganizationId,
				UserMailboxADObjectId = user.Id,
				UserMailboxDatabaseId = user.Database.ObjectGuid,
				UserMailboxId = user.ExchangeGuid
			};
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B724 File Offset: 0x00009924
		public TIdParameter GetIdParameter<TIdParameter>() where TIdParameter : IIdentityParameter
		{
			string text = null;
			if (this.ExchangeObjectId != Guid.Empty)
			{
				text = MailboxData.CreateMailboxIdentifierString(this.OrganizationId, this.ExchangeObjectId);
			}
			IIdentityParameter identityParameter;
			if (typeof(TIdParameter) == typeof(MoveRequestIdParameter))
			{
				if (!string.IsNullOrEmpty(text))
				{
					identityParameter = new MoveRequestIdParameter(text);
				}
				else
				{
					identityParameter = new MoveRequestIdParameter(this.UserMailboxADObjectId);
				}
			}
			else if (typeof(TIdParameter) == typeof(MailboxIdParameter))
			{
				if (!string.IsNullOrEmpty(text))
				{
					identityParameter = new MailboxIdParameter(text);
				}
				else
				{
					identityParameter = new MailboxIdParameter(this.UserMailboxADObjectId);
				}
			}
			else
			{
				if (!(typeof(TIdParameter) == typeof(MailboxOrMailUserIdParameter)))
				{
					throw new ArgumentException(string.Format("type not supported {0}", typeof(TIdParameter).Name), "TIdParameter");
				}
				if (!string.IsNullOrEmpty(text))
				{
					identityParameter = new MailboxOrMailUserIdParameter(text);
				}
				else
				{
					identityParameter = new MailboxOrMailUserIdParameter(this.UserMailboxADObjectId);
				}
			}
			return (TIdParameter)((object)identityParameter);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B835 File Offset: 0x00009A35
		public void Update(string identifier, OrganizationId organizationId)
		{
			this.OrganizationId = organizationId;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B840 File Offset: 0x00009A40
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
			{
				this.UserMailboxId,
				this.UserMailboxDatabaseId,
				this.MailboxServer,
				this.Name,
				this.RecipientType
			});
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B89C File Offset: 0x00009A9C
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.MailboxLegacyDN = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN, null);
			if (string.IsNullOrEmpty(this.MailboxLegacyDN))
			{
				return false;
			}
			this.MailboxServer = MigrationHelper.GetFqdnProperty(message, MigrationBatchMessageSchema.MigrationJobItemMailboxServer, false);
			this.UserMailboxId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemMailboxId, false);
			this.UserMailboxDatabaseId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemMailboxDatabaseId, false);
			byte[] valueOrDefault = message.GetValueOrDefault<byte[]>(MigrationBatchMessageSchema.MigrationJobItemOwnerId, null);
			if (valueOrDefault != null)
			{
				this.UserMailboxADObjectId = new ADObjectId(valueOrDefault);
			}
			this.ExchangeObjectId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationExchangeObjectId, false);
			return true;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B930 File Offset: 0x00009B30
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			if (this.MailboxServer != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobItemMailboxServer] = this.MailboxServer.Domain;
				message[MigrationBatchMessageSchema.MigrationJobItemMailboxDatabaseId] = this.UserMailboxDatabaseId;
			}
			message[MigrationBatchMessageSchema.MigrationJobItemMailboxId] = this.UserMailboxId;
			message[MigrationBatchMessageSchema.MigrationJobItemOwnerId] = this.UserMailboxADObjectId.GetBytes();
			message[MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN] = this.MailboxLegacyDN;
			message[MigrationBatchMessageSchema.MigrationExchangeObjectId] = this.ExchangeObjectId;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return new XElement("MailboxData", new object[]
			{
				new XElement("mailboxServer", this.MailboxServer),
				new XElement("mailboxId", this.UserMailboxId),
				new XElement("mailboxOwner", this.UserMailboxADObjectId),
				new XElement("mailboxExchangeObjectId", this.ExchangeObjectId),
				new XElement("userMailboxDatabaseId", this.UserMailboxDatabaseId),
				new XElement("mailboxName", this.MailboxLegacyDN)
			});
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000BA88 File Offset: 0x00009C88
		internal static string CreateMailboxIdentifierString(OrganizationId organizationId, Guid exchangeObjectId)
		{
			if (organizationId != null && organizationId != OrganizationId.ForestWideOrgId)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
				{
					organizationId.OrganizationalUnit.Name,
					exchangeObjectId
				});
			}
			return exchangeObjectId.ToString();
		}

		// Token: 0x040000EA RID: 234
		public static readonly PropertyDefinition[] MailboxDataPropertyDefinition = new StorePropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemMailboxServer,
			MigrationBatchMessageSchema.MigrationJobItemMailboxId,
			MigrationBatchMessageSchema.MigrationJobItemMailboxDatabaseId,
			MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN,
			MigrationBatchMessageSchema.MigrationJobItemOwnerId,
			MigrationBatchMessageSchema.MigrationExchangeObjectId
		};

		// Token: 0x040000EB RID: 235
		private MigrationUserRecipientType? recipientType;
	}
}
