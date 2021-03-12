using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200009F RID: 159
	internal sealed class ExchangeMigrationGroupRecipient : ExchangeMigrationRecipient, IMigrationSerializable
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x00027139 File Offset: 0x00025339
		public ExchangeMigrationGroupRecipient() : base(MigrationUserRecipientType.Group)
		{
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00027142 File Offset: 0x00025342
		public override HashSet<PropTag> SupportedProperties
		{
			get
			{
				return ExchangeMigrationGroupRecipient.supportedProperties;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00027149 File Offset: 0x00025349
		public override HashSet<PropTag> RequiredProperties
		{
			get
			{
				return ExchangeMigrationGroupRecipient.requiredProperties;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00027150 File Offset: 0x00025350
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x00027158 File Offset: 0x00025358
		public string[] Members { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00027161 File Offset: 0x00025361
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x00027169 File Offset: 0x00025369
		public bool MembersChanged { get; set; }

		// Token: 0x06000912 RID: 2322 RVA: 0x00027174 File Offset: 0x00025374
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			if (this.MembersChanged)
			{
				((IMigrationMessageItem)message).DeleteAttachment("GroupMembers.csv");
				using (IMigrationAttachment migrationAttachment = ((IMigrationMessageItem)message).CreateAttachment("GroupMembers.csv"))
				{
					using (StreamWriter streamWriter = new StreamWriter(migrationAttachment.Stream))
					{
						ExchangeMigrationGroupMembersCsvSchema.Write(streamWriter, this.Members);
					}
					migrationAttachment.Save(null);
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00027208 File Offset: 0x00025408
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			if (!base.ReadFromMessageItem(message))
			{
				return false;
			}
			List<string> list = null;
			IMigrationAttachment migrationAttachment;
			if (((IMigrationMessageItem)message).TryGetAttachment("GroupMembers.csv", PropertyOpenMode.ReadOnly, out migrationAttachment))
			{
				using (migrationAttachment)
				{
					try
					{
						list = new List<string>(ExchangeMigrationGroupMembersCsvSchema.Read(migrationAttachment.Stream));
					}
					catch (CsvValidationException)
					{
						string propertyValue = base.GetPropertyValue<string>(PropTag.SmtpAddress);
						throw new MigrationPermanentException(ServerStrings.MigrationGroupMembersAttachmentCorrupted(propertyValue));
					}
				}
			}
			if (list != null)
			{
				this.Members = list.ToArray();
			}
			this.MembersChanged = false;
			return true;
		}

		// Token: 0x04000384 RID: 900
		private static HashSet<PropTag> supportedProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.DisplayName,
			PropTag.EmailAddress,
			PropTag.SmtpAddress,
			(PropTag)2148470815U,
			(PropTag)2148073485U,
			PropTag.Account,
			(PropTag)2148864031U,
			(PropTag)2148270111U
		});

		// Token: 0x04000385 RID: 901
		private static HashSet<PropTag> requiredProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.EmailAddress,
			PropTag.SmtpAddress,
			PropTag.DisplayName
		});
	}
}
