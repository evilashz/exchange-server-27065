using System;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConsumerMailboxData : IMailboxData, IMigrationSerializable
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000A7EF File Offset: 0x000089EF
		public ConsumerMailboxData(Guid mailboxGuid, Guid mailboxDatabaseGuid)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(mailboxGuid, "mailboxGuid");
			MigrationUtil.ThrowOnGuidEmptyArgument(mailboxDatabaseGuid, "mailboxDatabaseGuid");
			this.UserMailboxId = mailboxGuid;
			this.UserMailboxDatabaseId = mailboxDatabaseGuid;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A81B File Offset: 0x00008A1B
		internal ConsumerMailboxData()
		{
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A823 File Offset: 0x00008A23
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000A82B File Offset: 0x00008A2B
		public Guid UserMailboxId { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A834 File Offset: 0x00008A34
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000A83C File Offset: 0x00008A3C
		public Guid UserMailboxDatabaseId { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A845 File Offset: 0x00008A45
		public OrganizationId OrganizationId
		{
			get
			{
				return OrganizationId.ForestWideOrgId;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A84C File Offset: 0x00008A4C
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000A854 File Offset: 0x00008A54
		public string MailboxIdentifier { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A85D File Offset: 0x00008A5D
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A860 File Offset: 0x00008A60
		PropertyDefinition[] IMigrationSerializable.PropertyDefinitions
		{
			get
			{
				return ConsumerMailboxData.ConsumerMailboxDataPropertyDefinition;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A868 File Offset: 0x00008A68
		public TIdParameter GetIdParameter<TIdParameter>() where TIdParameter : IIdentityParameter
		{
			IIdentityParameter identityParameter;
			if (typeof(TIdParameter) == typeof(MailboxIdParameter))
			{
				identityParameter = new MailboxIdParameter(this.MailboxIdentifier);
			}
			else
			{
				if (!(typeof(TIdParameter) == typeof(MailboxOrMailUserIdParameter)))
				{
					throw new ArgumentException(string.Format("type not supported {0}", typeof(TIdParameter).Name), "TIdParameter");
				}
				identityParameter = new MailboxOrMailUserIdParameter(this.MailboxIdentifier);
			}
			return (TIdParameter)((object)identityParameter);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A8F1 File Offset: 0x00008AF1
		public void Update(string identifier, OrganizationId organizationId)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(identifier, "identifier");
			MigrationUtil.AssertOrThrow(organizationId == null || organizationId == OrganizationId.ForestWideOrgId, "We expect ConsumerMailboxes to always be in FirstOrg", new object[0]);
			this.MailboxIdentifier = identifier;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A92C File Offset: 0x00008B2C
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}", this.MailboxIdentifier, this.UserMailboxId, this.UserMailboxDatabaseId);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A954 File Offset: 0x00008B54
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.UserMailboxId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemMailboxId, false);
			this.UserMailboxDatabaseId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemMailboxDatabaseId, false);
			return true;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A97B File Offset: 0x00008B7B
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[MigrationBatchMessageSchema.MigrationJobItemMailboxDatabaseId] = this.UserMailboxDatabaseId;
			message[MigrationBatchMessageSchema.MigrationJobItemMailboxId] = this.UserMailboxId;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return new XElement("ConsumerMailboxData", new object[]
			{
				new XElement("mailboxId", this.UserMailboxId),
				new XElement("userMailboxDatabaseId", this.UserMailboxDatabaseId)
			});
		}

		// Token: 0x040000D6 RID: 214
		public static readonly PropertyDefinition[] ConsumerMailboxDataPropertyDefinition = new StorePropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemMailboxId,
			MigrationBatchMessageSchema.MigrationJobItemMailboxDatabaseId
		};
	}
}
