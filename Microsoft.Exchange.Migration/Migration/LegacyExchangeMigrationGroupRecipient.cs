using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A5 RID: 165
	internal sealed class LegacyExchangeMigrationGroupRecipient : ExchangeMigrationRecipient, IMigrationSerializable
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x00027AA6 File Offset: 0x00025CA6
		public LegacyExchangeMigrationGroupRecipient() : base(MigrationUserRecipientType.Group)
		{
			this.memberBatchSize = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationGroupMembersBatchSize");
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00027ABF File Offset: 0x00025CBF
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00027AC7 File Offset: 0x00025CC7
		public int CountOfProvisionedMembers { get; private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00027AD0 File Offset: 0x00025CD0
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00027AD8 File Offset: 0x00025CD8
		public int CountOfSkippedMembers { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00027AE1 File Offset: 0x00025CE1
		public override HashSet<PropTag> SupportedProperties
		{
			get
			{
				return LegacyExchangeMigrationGroupRecipient.supportedProperties;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00027AE8 File Offset: 0x00025CE8
		public override HashSet<PropTag> RequiredProperties
		{
			get
			{
				return LegacyExchangeMigrationGroupRecipient.requiredProperties;
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00027AEF File Offset: 0x00025CEF
		public bool IsMembersRetrieved()
		{
			return this.provisioningState != GroupMembershipProvisioningState.MemberNotRetrieved;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00027AFD File Offset: 0x00025CFD
		public bool IsMembersProvisioned()
		{
			return this.provisioningState == GroupMembershipProvisioningState.MemberRetrievedAndProvisioned;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00027B08 File Offset: 0x00025D08
		public List<string> GetNextBatchOfMembers(IMigrationDataProvider provider, IMigrationMessageItem message)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(message, "message");
			if (!this.IsMembersRetrieved())
			{
				string propertyValue = base.GetPropertyValue<string>(PropTag.SmtpAddress);
				throw new MigrationPermanentException(ServerStrings.MigrationGroupMembersNotAvailable(propertyValue));
			}
			if (this.IsMembersProvisioned())
			{
				return null;
			}
			List<string> list = new List<string>(this.memberBatchSize);
			using (IMigrationAttachment attachment = message.GetAttachment("GroupMembers.csv", PropertyOpenMode.ReadOnly))
			{
				try
				{
					int num = 1;
					int num2 = this.CountOfSkippedMembers + this.CountOfProvisionedMembers;
					foreach (string item in ExchangeMigrationGroupMembersCsvSchema.Read(attachment.Stream))
					{
						if (num > num2)
						{
							list.Add(item);
							if (list.Count == this.memberBatchSize)
							{
								break;
							}
						}
						num++;
					}
				}
				catch (CsvValidationException)
				{
					string propertyValue2 = base.GetPropertyValue<string>(PropTag.SmtpAddress);
					throw new MigrationPermanentException(ServerStrings.MigrationGroupMembersAttachmentCorrupted(propertyValue2));
				}
			}
			if (list.Count == 0)
			{
				this.provisioningState = GroupMembershipProvisioningState.MemberRetrievedAndProvisioned;
				this.WriteToMessageItem(message);
				return null;
			}
			return list;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00027C3C File Offset: 0x00025E3C
		public void SetGroupMembersInfo(IMigrationMessageItem message, string[] members)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			MigrationUtil.ThrowOnNullArgument(members, "members");
			if (this.IsMembersRetrieved())
			{
				string propertyValue = base.GetPropertyValue<string>(PropTag.SmtpAddress);
				throw new MigrationPermanentException(ServerStrings.MigrationGroupMembersAlreadyAvailable(propertyValue));
			}
			message.DeleteAttachment("GroupMembers.csv");
			using (IMigrationAttachment migrationAttachment = message.CreateAttachment("GroupMembers.csv"))
			{
				using (StreamWriter streamWriter = new StreamWriter(migrationAttachment.Stream))
				{
					ExchangeMigrationGroupMembersCsvSchema.Write(streamWriter, members);
				}
				migrationAttachment.Save(null);
				this.CountOfProvisionedMembers = 0;
				this.CountOfSkippedMembers = 0;
				if (members.Length > 0)
				{
					this.provisioningState = GroupMembershipProvisioningState.MemberRetrievedButNotProvisioned;
				}
				else
				{
					this.provisioningState = GroupMembershipProvisioningState.MemberRetrievedAndProvisioned;
				}
			}
			this.WriteToMessageItem(message);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00027D10 File Offset: 0x00025F10
		public void UpdateGroupMembersInfo(IMigrationMessageItem message, int membersProvisioned, int membersSkipped)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			this.CountOfSkippedMembers += membersSkipped;
			this.CountOfProvisionedMembers += membersProvisioned;
			this.WriteToMessageItem(message);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00027D40 File Offset: 0x00025F40
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			base.WriteToMessageItem(message, loaded);
			this.WriteToMessageItem(message);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00027D54 File Offset: 0x00025F54
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			if (!base.ReadFromMessageItem(message))
			{
				return false;
			}
			int valueOrDefault = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState, 0);
			if (!Enum.IsDefined(typeof(GroupMembershipProvisioningState), valueOrDefault))
			{
				throw new MigrationDataCorruptionException("Invalid MigrationJobItemGroupMemberProvisioningState. Message ID: " + message.Id);
			}
			this.provisioningState = (GroupMembershipProvisioningState)valueOrDefault;
			this.CountOfSkippedMembers = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemGroupMemberSkipped, 0);
			this.CountOfProvisionedMembers = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioned, 0);
			return true;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00027DD4 File Offset: 0x00025FD4
		private void WriteToMessageItem(IMigrationStoreObject message)
		{
			message[MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState] = (int)this.provisioningState;
			message[MigrationBatchMessageSchema.MigrationJobItemGroupMemberSkipped] = this.CountOfSkippedMembers;
			message[MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioned] = this.CountOfProvisionedMembers;
		}

		// Token: 0x04000394 RID: 916
		public static readonly PropertyDefinition[] GroupPropertyDefinitions = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioned,
			MigrationBatchMessageSchema.MigrationJobItemGroupMemberSkipped,
			MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState
		};

		// Token: 0x04000395 RID: 917
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

		// Token: 0x04000396 RID: 918
		private static HashSet<PropTag> requiredProperties = new HashSet<PropTag>(new PropTag[]
		{
			PropTag.DisplayType,
			PropTag.EmailAddress,
			PropTag.SmtpAddress,
			PropTag.DisplayName
		});

		// Token: 0x04000397 RID: 919
		private GroupMembershipProvisioningState provisioningState;

		// Token: 0x04000398 RID: 920
		private readonly int memberBatchSize;
	}
}
