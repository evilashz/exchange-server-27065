using System;
using System.Collections.Generic;
using System.Web.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000120 RID: 288
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ExchangeJobSyncInitializingProcessor : MigrationJobSyncInitializingProcessor
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003FDEC File Offset: 0x0003DFEC
		protected override bool IgnorePostCompleteSubmits
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0003FDEF File Offset: 0x0003DFEF
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0003FE06 File Offset: 0x0003E006
		protected override int UpdatesEncountered
		{
			get
			{
				if (base.Job.IsStaged)
				{
					return base.UpdatesEncountered;
				}
				return 0;
			}
			set
			{
				if (base.Job.IsStaged)
				{
					base.UpdatesEncountered = value;
				}
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003FF08 File Offset: 0x0003E108
		protected override LegacyMigrationJobProcessorResponse UpdateJobItems(bool scheduleNewWork)
		{
			LegacyMigrationJobProcessorResponse response = LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MaxRowsToProcessInOnePass");
			IEnumerable<MigrationJobItem> jobItemsByTypeAndGroupMemberProvisionedState = MigrationJobItem.GetJobItemsByTypeAndGroupMemberProvisionedState(base.DataProvider, base.Job, MigrationUserRecipientType.Group, MigrationUserStatus.Provisioning, GroupMembershipProvisioningState.MemberNotRetrieved, config);
			using (IEnumerator<MigrationJobItem> enumerator = jobItemsByTypeAndGroupMemberProvisionedState.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MigrationJobItem jobItem = enumerator.Current;
					response.Result = MigrationProcessorResult.Working;
					ItemStateTransitionHelper.RunJobItemOperation(base.Job, jobItem, base.DataProvider, MigrationUserStatus.Failed, delegate
					{
						ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = (ExchangeProvisioningDataStorage)jobItem.ProvisioningData;
						LegacyExchangeMigrationGroupRecipient legacyExchangeMigrationGroupRecipient = exchangeProvisioningDataStorage.ExchangeRecipient as LegacyExchangeMigrationGroupRecipient;
						MigrationUtil.AssertOrThrow(legacyExchangeMigrationGroupRecipient != null, "jobItem as expected to have an ExchangeMigrationGroupRecipient", new object[0]);
						if (!legacyExchangeMigrationGroupRecipient.IsMembersRetrieved())
						{
							NspiMigrationDataRowProvider nspiMigrationDataRowProvider = this.GetMigrationDataRowProvider() as NspiMigrationDataRowProvider;
							string[] members = nspiMigrationDataRowProvider.GetMembers(legacyExchangeMigrationGroupRecipient.Identifier);
							jobItem.SetGroupProperties(this.DataProvider, null, null, members, 0, 0);
							response.NumItemsProcessed++;
						}
					});
				}
			}
			return response;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003FFEC File Offset: 0x0003E1EC
		protected override IMigrationDataRowProvider GetMigrationDataRowProvider()
		{
			ExchangeOutlookAnywhereEndpoint endpoint = (ExchangeOutlookAnywhereEndpoint)base.Job.SourceEndpoint;
			if (base.Job.IsStaged)
			{
				return new NspiCsvMigrationDataRowProvider(base.Job, base.DataProvider, true);
			}
			return new NspiMigrationDataRowProvider(endpoint, base.Job, true);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00040038 File Offset: 0x0003E238
		protected override void CreateNewJobItem(IMigrationDataRow dataRow)
		{
			NspiMigrationDataRow nspiMigrationDataRow = dataRow as NspiMigrationDataRow;
			MigrationUserStatus status = this.DetermineInitialStatus();
			LocalizedString localizedErrorMessage;
			if (!nspiMigrationDataRow.Recipient.TryValidateRequiredProperties(out localizedErrorMessage))
			{
				base.CreateFailedJobItem(dataRow, new MigrationPermanentException(localizedErrorMessage));
				return;
			}
			if (base.Job.IsStaged)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "handling CSV attachment provisioning", new object[0]);
				this.CreateOrUpdateJobItemForStagedSEM(null, nspiMigrationDataRow);
				return;
			}
			if (nspiMigrationDataRow.Recipient.RecipientType == MigrationUserRecipientType.PublicFolder)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "found a public folder {0}", new object[]
				{
					dataRow
				});
				base.CreateNewJobItem(dataRow, null, MigrationUserStatus.Queued);
				return;
			}
			if (nspiMigrationDataRow.Recipient.RecipientType == MigrationUserRecipientType.Mailbox || nspiMigrationDataRow.Recipient.RecipientType == MigrationUserRecipientType.Mailuser)
			{
				nspiMigrationDataRow.EncryptedPassword = ExchangeJobSyncInitializingProcessor.GenerateEncryptedPassword();
			}
			string propertyValue = nspiMigrationDataRow.Recipient.GetPropertyValue<string>(PropTag.SmtpAddress);
			ADRecipient adrecipientByProxyAddress = base.DataProvider.ADProvider.GetADRecipientByProxyAddress(propertyValue);
			if (adrecipientByProxyAddress == null)
			{
				base.CreateNewJobItem(dataRow, null, status);
				return;
			}
			MigrationUserRecipientType recipientType = ExchangeJobSyncInitializingProcessor.GetRecipientType(adrecipientByProxyAddress.RecipientTypeDetails);
			if (nspiMigrationDataRow.Recipient.RecipientType != recipientType)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "found a recipient type that DOESNT match {0} found {1} expected {2}", new object[]
				{
					propertyValue,
					nspiMigrationDataRow.Recipient.RecipientType,
					recipientType
				});
				base.CreateFailedJobItem(dataRow, new MigrationUnexpectedExchangePrincipalFoundException(propertyValue));
				return;
			}
			nspiMigrationDataRow.Recipient.DoesADObjectExist = true;
			base.CreateNewJobItem(dataRow, null, status);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0004019C File Offset: 0x0003E39C
		protected override MigrationBatchError ProcessExistingJobItem(MigrationJobItem jobItem, IMigrationDataRow dataRow)
		{
			NspiMigrationDataRow nspiMigrationDataRow = dataRow as NspiMigrationDataRow;
			LocalizedString localizedErrorMessage;
			if (!nspiMigrationDataRow.Recipient.TryValidateRequiredProperties(out localizedErrorMessage))
			{
				bool flag = jobItem.UpdateDataRow(base.DataProvider, base.Job, dataRow);
				if (flag && base.Job.IsStaged)
				{
					this.UpdatesEncountered++;
				}
				MigrationPermanentException ex = new MigrationPermanentException(localizedErrorMessage);
				base.Job.ReportData.Append(Strings.MigrationReportJobItemFailed(jobItem.Identifier, ex.LocalizedString), ex, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
				jobItem.SetFailedStatus(base.DataProvider, MigrationUserStatus.Failed, ex, "all required properties are not available for type", true);
				return null;
			}
			ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = (ExchangeProvisioningDataStorage)jobItem.ProvisioningData;
			if ((nspiMigrationDataRow.Recipient.RecipientType == MigrationUserRecipientType.Mailbox || nspiMigrationDataRow.Recipient.RecipientType == MigrationUserRecipientType.Mailuser) && string.IsNullOrEmpty(nspiMigrationDataRow.EncryptedPassword))
			{
				if (!string.IsNullOrEmpty(exchangeProvisioningDataStorage.EncryptedPassword))
				{
					nspiMigrationDataRow.EncryptedPassword = exchangeProvisioningDataStorage.EncryptedPassword;
				}
				else if (!base.Job.IsStaged)
				{
					nspiMigrationDataRow.EncryptedPassword = ExchangeJobSyncInitializingProcessor.GenerateEncryptedPassword();
				}
			}
			if (base.Job.IsStaged)
			{
				if (this.CreateOrUpdateJobItemForStagedSEM(jobItem, nspiMigrationDataRow))
				{
					this.UpdatesEncountered++;
				}
				return null;
			}
			if (jobItem.RecipientType == MigrationUserRecipientType.Group && jobItem.Status == MigrationUserStatus.Synced)
			{
				LegacyExchangeMigrationGroupRecipient legacyExchangeMigrationGroupRecipient = exchangeProvisioningDataStorage.ExchangeRecipient as LegacyExchangeMigrationGroupRecipient;
				if (legacyExchangeMigrationGroupRecipient != null && legacyExchangeMigrationGroupRecipient.CountOfSkippedMembers > 0)
				{
					jobItem.UpdateDataRow(base.DataProvider, base.Job, dataRow);
					jobItem.UpdateAndEnableJobItem(base.DataProvider, base.Job, this.DetermineInitialStatus());
					return null;
				}
			}
			return base.ProcessExistingJobItem(jobItem, dataRow);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0004032A File Offset: 0x0003E52A
		protected override MigrationBatchError HandleDuplicateMigrationDataRow(MigrationJobItem jobItem, IMigrationDataRow dataRow)
		{
			if (base.Job.IsStaged)
			{
				return base.HandleDuplicateMigrationDataRow(jobItem, dataRow);
			}
			return null;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00040344 File Offset: 0x0003E544
		protected override MigrationBatchError GetValidationError(IMigrationDataRow dataRow, LocalizedString locErrorString)
		{
			NspiMigrationDataRow nspiMigrationDataRow = (NspiMigrationDataRow)dataRow;
			return new MigrationBatchError
			{
				RowIndex = nspiMigrationDataRow.CursorPosition,
				EmailAddress = dataRow.Identifier,
				LocalizedErrorMessage = locErrorString
			};
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00040380 File Offset: 0x0003E580
		protected override bool VerifyRequiredProperties(MigrationJobItem jobItem)
		{
			ExchangeProvisioningDataStorage exchangeProvisioningDataStorage = (ExchangeProvisioningDataStorage)jobItem.ProvisioningData;
			LocalizedException ex = null;
			LocalizedString localizedErrorMessage;
			if (!exchangeProvisioningDataStorage.ExchangeRecipient.TryValidateRequiredProperties(out localizedErrorMessage))
			{
				ex = new MigrationPermanentException(localizedErrorMessage);
			}
			else if (jobItem.MigrationJob.IsStaged)
			{
				ADRecipient adrecipientByProxyAddress = base.DataProvider.ADProvider.GetADRecipientByProxyAddress(jobItem.Identifier);
				RecipientTypeDetails recipientTypeDetails = (adrecipientByProxyAddress != null) ? adrecipientByProxyAddress.RecipientTypeDetails : RecipientTypeDetails.None;
				RecipientTypeDetails recipientTypeDetails2 = recipientTypeDetails;
				if (recipientTypeDetails2 <= (RecipientTypeDetails)((ulong)-2147483648))
				{
					if (recipientTypeDetails2 <= RecipientTypeDetails.RoomMailbox)
					{
						if (recipientTypeDetails2 <= RecipientTypeDetails.LegacyMailbox)
						{
							if (recipientTypeDetails2 < RecipientTypeDetails.None)
							{
								goto IL_13E;
							}
							switch ((int)recipientTypeDetails2)
							{
							case 0:
								ex = new MigrationInvalidTargetAddressException(jobItem.Identifier);
								goto IL_150;
							case 1:
							case 2:
							case 4:
							case 8:
								goto IL_150;
							case 3:
							case 5:
							case 6:
							case 7:
								goto IL_13E;
							}
						}
						if (recipientTypeDetails2 == RecipientTypeDetails.RoomMailbox)
						{
							goto IL_150;
						}
					}
					else if (recipientTypeDetails2 == RecipientTypeDetails.EquipmentMailbox || recipientTypeDetails2 == RecipientTypeDetails.MailUser || recipientTypeDetails2 == (RecipientTypeDetails)((ulong)-2147483648))
					{
						goto IL_150;
					}
				}
				else if (recipientTypeDetails2 <= RecipientTypeDetails.RemoteEquipmentMailbox)
				{
					if (recipientTypeDetails2 == RecipientTypeDetails.RemoteRoomMailbox || recipientTypeDetails2 == RecipientTypeDetails.RemoteEquipmentMailbox)
					{
						goto IL_150;
					}
				}
				else if (recipientTypeDetails2 == RecipientTypeDetails.RemoteSharedMailbox || recipientTypeDetails2 == RecipientTypeDetails.TeamMailbox || recipientTypeDetails2 == RecipientTypeDetails.RemoteTeamMailbox)
				{
					goto IL_150;
				}
				IL_13E:
				ex = new UnsupportedTargetRecipientTypeException(recipientTypeDetails.ToString());
			}
			IL_150:
			if (ex != null)
			{
				base.Job.ReportData.Append(Strings.MigrationReportJobItemFailed(jobItem.Identifier, ex.LocalizedString), ex, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
				jobItem.SetFailedStatus(base.DataProvider, MigrationUserStatus.Failed, ex, "all required properties are not available for job-item", true);
				return false;
			}
			return base.VerifyRequiredProperties(jobItem);
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00040524 File Offset: 0x0003E724
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeJobSyncInitializingProcessor>(this);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0004052C File Offset: 0x0003E72C
		private static string GenerateEncryptedPassword()
		{
			string clearString = Membership.GeneratePassword(16, 3);
			return MigrationServiceFactory.Instance.GetCryptoAdapter().ClearStringToEncryptedString(clearString);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00040554 File Offset: 0x0003E754
		private static MigrationUserRecipientType GetRecipientType(RecipientTypeDetails type)
		{
			type &= RecipientTypeDetails.AllUniqueRecipientTypes;
			RecipientTypeDetails recipientTypeDetails = type;
			if (recipientTypeDetails <= RecipientTypeDetails.MailContact)
			{
				if (recipientTypeDetails <= RecipientTypeDetails.SharedMailbox)
				{
					if (recipientTypeDetails != RecipientTypeDetails.UserMailbox && recipientTypeDetails != RecipientTypeDetails.SharedMailbox)
					{
						return MigrationUserRecipientType.Unsupported;
					}
				}
				else if (recipientTypeDetails != RecipientTypeDetails.RoomMailbox && recipientTypeDetails != RecipientTypeDetails.EquipmentMailbox)
				{
					if (recipientTypeDetails != RecipientTypeDetails.MailContact)
					{
						return MigrationUserRecipientType.Unsupported;
					}
					return MigrationUserRecipientType.Contact;
				}
				return MigrationUserRecipientType.Mailbox;
			}
			if (recipientTypeDetails <= RecipientTypeDetails.MailUniversalDistributionGroup)
			{
				if (recipientTypeDetails == RecipientTypeDetails.MailUser)
				{
					return MigrationUserRecipientType.Mailuser;
				}
				if (recipientTypeDetails != RecipientTypeDetails.MailUniversalDistributionGroup)
				{
					return MigrationUserRecipientType.Unsupported;
				}
			}
			else if (recipientTypeDetails != RecipientTypeDetails.MailNonUniversalGroup && recipientTypeDetails != RecipientTypeDetails.MailUniversalSecurityGroup)
			{
				if (recipientTypeDetails != RecipientTypeDetails.PublicFolder)
				{
					return MigrationUserRecipientType.Unsupported;
				}
				return MigrationUserRecipientType.PublicFolder;
			}
			return MigrationUserRecipientType.Group;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x000405F8 File Offset: 0x0003E7F8
		private bool CreateOrUpdateJobItemForStagedSEM(MigrationJobItem jobItem, NspiMigrationDataRow dataRow)
		{
			ADRecipient adrecipientByProxyAddress = base.DataProvider.ADProvider.GetADRecipientByProxyAddress(dataRow.Identifier);
			RecipientTypeDetails recipientTypeDetails = (adrecipientByProxyAddress != null) ? adrecipientByProxyAddress.RecipientTypeDetails : RecipientTypeDetails.None;
			RecipientTypeDetails recipientTypeDetails2 = recipientTypeDetails;
			if (recipientTypeDetails2 <= (RecipientTypeDetails)((ulong)-2147483648))
			{
				if (recipientTypeDetails2 <= RecipientTypeDetails.RoomMailbox)
				{
					if (recipientTypeDetails2 <= RecipientTypeDetails.LegacyMailbox)
					{
						if (recipientTypeDetails2 < RecipientTypeDetails.None)
						{
							goto IL_126;
						}
						switch ((int)recipientTypeDetails2)
						{
						case 0:
							return this.CreateOrUpdateFailedJobItem(jobItem, dataRow, new MigrationInvalidTargetAddressException(dataRow.Identifier));
						case 1:
						case 2:
						case 4:
						case 8:
							goto IL_145;
						case 3:
						case 5:
						case 6:
						case 7:
							goto IL_126;
						}
					}
					if (recipientTypeDetails2 == RecipientTypeDetails.RoomMailbox)
					{
						goto IL_145;
					}
				}
				else if (recipientTypeDetails2 == RecipientTypeDetails.EquipmentMailbox || recipientTypeDetails2 == RecipientTypeDetails.MailUser || recipientTypeDetails2 == (RecipientTypeDetails)((ulong)-2147483648))
				{
					goto IL_145;
				}
			}
			else if (recipientTypeDetails2 <= RecipientTypeDetails.RemoteEquipmentMailbox)
			{
				if (recipientTypeDetails2 == RecipientTypeDetails.RemoteRoomMailbox || recipientTypeDetails2 == RecipientTypeDetails.RemoteEquipmentMailbox)
				{
					goto IL_145;
				}
			}
			else if (recipientTypeDetails2 == RecipientTypeDetails.RemoteSharedMailbox || recipientTypeDetails2 == RecipientTypeDetails.TeamMailbox || recipientTypeDetails2 == RecipientTypeDetails.RemoteTeamMailbox)
			{
				goto IL_145;
			}
			IL_126:
			return this.CreateOrUpdateFailedJobItem(jobItem, dataRow, new UnsupportedTargetRecipientTypeException(recipientTypeDetails.ToString()));
			IL_145:
			if (jobItem == null)
			{
				base.CreateNewJobItem(dataRow, null, this.DetermineInitialStatus());
				return false;
			}
			bool flag = jobItem.UpdateDataRow(base.DataProvider, base.Job, dataRow);
			if (flag)
			{
				if (Array.Exists<MigrationUserStatus>(MigrationJobSyncInitializingProcessor.ForcedUpdateStatusJobItemStatusArray, (MigrationUserStatus status) => jobItem.Status == status))
				{
					jobItem.UpdateAndEnableJobItem(base.DataProvider, base.Job, this.DetermineInitialStatus());
				}
			}
			return flag;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000407C0 File Offset: 0x0003E9C0
		private bool CreateOrUpdateFailedJobItem(MigrationJobItem jobItem, IMigrationDataRow dataRow, LocalizedException localizedError)
		{
			if (jobItem == null)
			{
				base.CreateFailedJobItem(dataRow, localizedError);
				return false;
			}
			base.Job.ReportData.Append(Strings.MigrationReportJobItemFailed(jobItem.Identifier, localizedError.LocalizedString), localizedError, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
			bool result = jobItem.UpdateDataRow(base.DataProvider, base.Job, dataRow);
			jobItem.SetFailedStatus(base.DataProvider, MigrationUserStatus.Failed, localizedError, "CreateOrUpdateFailedJobItem had an error", true);
			return result;
		}

		// Token: 0x04000520 RID: 1312
		private const int MaxPasswordLength = 16;

		// Token: 0x04000521 RID: 1313
		private const int NumberAlphaNumericChars = 3;
	}
}
