using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000504 RID: 1284
	[Cmdlet("Test", "ArchiveConnectivity", SupportsShouldProcess = true)]
	public sealed class TestArchiveConnectivityTask : Task
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x000B7F48 File Offset: 0x000B6148
		static TestArchiveConnectivityTask()
		{
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				StoreObjectSchema.EntryId,
				StoreObjectSchema.ChangeKey,
				StoreObjectSchema.ParentEntryId,
				StoreObjectSchema.ParentItemId,
				StoreObjectSchema.SearchKey,
				StoreObjectSchema.RecordKey
			};
			TestArchiveConnectivityTask.mailboxExtendedProperties = new List<PropertyDefinition>(MailboxSchema.Instance.AllProperties);
			foreach (PropertyDefinition item in array)
			{
				TestArchiveConnectivityTask.mailboxExtendedProperties.Remove(item);
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000B80EA File Offset: 0x000B62EA
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x000B8106 File Offset: 0x000B6306
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
		public SmtpAddress UserSmtp
		{
			get
			{
				return (SmtpAddress)base.Fields[TestArchiveConnectivityTask.UserSmtpParam];
			}
			set
			{
				base.Fields[TestArchiveConnectivityTask.UserSmtpParam] = value;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000B8123 File Offset: 0x000B6323
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x000B8149 File Offset: 0x000B6349
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeArchiveMRMConfiguration
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeArchiveMRMConfiguration"] ?? false);
			}
			set
			{
				base.Fields["IncludeArchiveMRMConfiguration"] = value;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000B8161 File Offset: 0x000B6361
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x000B8178 File Offset: 0x000B6378
		[Parameter(Mandatory = false)]
		public string MessageId
		{
			get
			{
				return (string)base.Fields[TestArchiveConnectivityTask.MessageIdParam];
			}
			set
			{
				base.Fields[TestArchiveConnectivityTask.MessageIdParam] = value;
			}
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000B818C File Offset: 0x000B638C
		private void PerformArchiveConnectivityTest(ref ArchiveConnectivityOutcome result)
		{
			bool flag = false;
			bool flag2 = false;
			try
			{
				SmtpAddress userSmtp = this.UserSmtp;
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromProxyAddress(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(userSmtp.Domain), userSmtp.ToString());
				if (exchangePrincipal != null)
				{
					if (this.IncludeArchiveMRMConfiguration || !string.IsNullOrEmpty(this.MessageId))
					{
						this.LogonPrimary(exchangePrincipal);
						result.PrimaryMRMConfiguration = this.primaryFAI;
						result.PrimaryLastProcessedTime = this.primaryLastProcessedTime;
					}
					flag = true;
					ADObjectId objectId = exchangePrincipal.ObjectId;
					IRecipientSession tenantOrRootOrgRecipientSession;
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
					{
						tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 276, "PerformArchiveConnectivityTest", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ArchiveConnectivity\\TestArchiveConnectivityTask.cs");
					}
					else
					{
						tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 284, "PerformArchiveConnectivityTest", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Monitoring\\ArchiveConnectivity\\TestArchiveConnectivityTask.cs");
					}
					ADUser aduser = tenantOrRootOrgRecipientSession.FindADUserByObjectId(objectId);
					this.complianceConfiguration = aduser.ElcMailboxFlags.ToString();
					result.ComplianceConfiguration = this.complianceConfiguration;
					if (exchangePrincipal.GetArchiveMailbox() == null)
					{
						result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, Strings.ArchiveConnectivityResultArchiveNotProvisioned);
					}
					else
					{
						if (aduser.ArchiveDomain != null)
						{
							result.ArchiveDomain = aduser.ArchiveDomain.ToString();
							flag2 = true;
						}
						if (aduser.ArchiveDatabase != null)
						{
							result.ArchiveDatabase = aduser.ArchiveDatabase.ToString();
						}
						if (aduser.RecipientType == RecipientType.UserMailbox)
						{
							if (flag2)
							{
								if (ArchiveStatusFlags.Active != aduser.ArchiveStatus)
								{
									result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, Strings.ArchiveConnectivityResultArchiveNotActive);
								}
								else if (this.LogonArchive(this.GetArchivePrincipal(exchangePrincipal, aduser)))
								{
									result.Update(ArchiveConnectivityResultEnum.Success, "");
								}
							}
							else if (this.LogonArchive(this.GetArchivePrincipal(exchangePrincipal, aduser)))
							{
								result.Update(ArchiveConnectivityResultEnum.Success, "");
							}
						}
						if (this.IncludeArchiveMRMConfiguration)
						{
							result.ArchiveMRMConfiguration = this.archiveFAI;
							result.ArchiveLastProcessedTime = this.archiveLastProcessedTime;
						}
					}
				}
				this.mrmProperties = this.mrmPropReport.ToString();
				if (!string.IsNullOrEmpty(this.mrmProperties))
				{
					result.ItemMRMProperties = this.mrmProperties;
				}
				else if (!string.IsNullOrEmpty(this.MessageId))
				{
					result.ItemMRMProperties = "Item not found.";
				}
			}
			catch (ObjectNotFoundException ex)
			{
				if (!flag)
				{
					result.Update(ArchiveConnectivityResultEnum.PrimaryFailure, this.GetAllInnerExceptions(ex));
				}
				else
				{
					result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, this.GetAllInnerExceptions(ex));
				}
			}
			catch (ConnectionFailedTransientException ex2)
			{
				result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, this.GetAllInnerExceptions(ex2));
			}
			catch (AutoDAccessException ex3)
			{
				result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, this.GetAllInnerExceptions(ex3));
			}
			catch (StoragePermanentException ex4)
			{
				result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, this.GetAllInnerExceptions(ex4));
			}
			catch (StorageTransientException ex5)
			{
				result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, this.GetAllInnerExceptions(ex5));
			}
			catch (ArgumentException ex6)
			{
				result.Update(ArchiveConnectivityResultEnum.ArchiveFailure, this.GetAllInnerExceptions(ex6));
			}
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000B850C File Offset: 0x000B670C
		private bool LogonArchive(ExchangePrincipal mailboxEP)
		{
			bool result;
			using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(mailboxEP, CultureInfo.InvariantCulture, "Client=Monitoring;Action=Test-ArchiveConnectivity"))
			{
				if (mailboxSession != null)
				{
					if (this.IncludeArchiveMRMConfiguration || !string.IsNullOrEmpty(this.MessageId))
					{
						UserConfiguration userConfiguration = ElcMailboxHelper.OpenFaiMessage(mailboxSession, "MRM", false);
						if (userConfiguration != null)
						{
							using (Stream xmlStream = userConfiguration.GetXmlStream())
							{
								using (StreamReader streamReader = new StreamReader(xmlStream))
								{
									this.archiveFAI = streamReader.ReadToEnd();
								}
								goto IL_80;
							}
						}
						this.archiveFAI = "No FAI found in Archive Mailbox.";
						IL_80:
						this.archiveLastProcessedTime = this.ReadMailboxTableProperties(mailboxSession);
						if (!string.IsNullOrEmpty(this.MessageId))
						{
							this.GetELCItemProperties(mailboxSession, this.MessageId);
						}
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000B8600 File Offset: 0x000B6800
		private void LogonPrimary(ExchangePrincipal primaryEP)
		{
			using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(primaryEP, CultureInfo.InvariantCulture, "Client=Monitoring;Action=Test-ArchiveConnectivity"))
			{
				if (mailboxSession != null)
				{
					UserConfiguration userConfiguration = ElcMailboxHelper.OpenFaiMessage(mailboxSession, "MRM", false);
					if (userConfiguration != null)
					{
						using (Stream xmlStream = userConfiguration.GetXmlStream())
						{
							using (StreamReader streamReader = new StreamReader(xmlStream))
							{
								this.primaryFAI = streamReader.ReadToEnd();
							}
							goto IL_63;
						}
					}
					this.primaryFAI = "No FAI found in Primary Mailbox.";
					IL_63:
					this.primaryLastProcessedTime = this.ReadMailboxTableProperties(mailboxSession);
					if (!string.IsNullOrEmpty(this.MessageId))
					{
						this.GetELCItemProperties(mailboxSession, this.MessageId);
					}
				}
			}
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000B86CC File Offset: 0x000B68CC
		private void GetELCItemProperties(MailboxSession mailboxSession, string messageId)
		{
			if (mailboxSession.GetDefaultFolderId(DefaultFolderType.AllItems) == null)
			{
				mailboxSession.CreateDefaultFolder(DefaultFolderType.AllItems);
			}
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.InternetMessageId, messageId);
			object[][] array = null;
			foreach (DefaultFolderType defaultFolderType in TestArchiveConnectivityTask.mailboxFolders)
			{
				if (mailboxSession.GetDefaultFolderId(defaultFolderType) != null)
				{
					using (Folder folder = Folder.Bind(mailboxSession, defaultFolderType))
					{
						using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, new PropertyDefinition[]
						{
							ItemSchema.Id,
							StoreObjectSchema.ParentItemId
						}))
						{
							array = queryResult.GetRows(queryResult.EstimatedRowCount);
							if (array.Length > 0)
							{
								for (int j = 0; j < array.Length; j++)
								{
									VersionedId storeId = (VersionedId)array[j][0];
									using (Item item = Item.Bind(mailboxSession, storeId, TestArchiveConnectivityTask.mrmStoreProps))
									{
										if (item != null)
										{
											this.mrmPropReport.AppendLine(this.GetMRMProps(item));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000B880C File Offset: 0x000B6A0C
		private string GetMRMProps(Item item)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			byte[] propertyBytes = null;
			string str = string.Empty;
			try
			{
				Guid guid = new Guid((byte[])item[StoreObjectSchema.PolicyTag]);
				stringBuilder.AppendLine("RetentionTagGuid: " + guid);
			}
			catch (PropertyErrorException)
			{
				stringBuilder.AppendLine("RetentionTagGuid: Not Found");
			}
			try
			{
				propertyBytes = (byte[])item[ItemSchema.StartDateEtc];
				flag = true;
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				if (flag)
				{
					DateTime value = CompositeProperty.Parse(propertyBytes, true).Date.Value;
					stringBuilder.AppendLine("StartDate: " + value);
				}
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				DateTime universalTime = ((ExDateTime)item[ItemSchema.RetentionDate]).UniversalTime;
				stringBuilder.AppendLine("RetentionDate: " + universalTime);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				int? num = new int?((int)item[StoreObjectSchema.RetentionPeriod]);
				stringBuilder.AppendLine("RetentionPeriod: " + num);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				Guid guid2 = new Guid((byte[])item[StoreObjectSchema.ArchiveTag]);
				stringBuilder.AppendLine("ArchiveGuid: " + guid2);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				DateTime universalTime2 = ((ExDateTime)item[ItemSchema.ArchiveDate]).UniversalTime;
				stringBuilder.AppendLine("ArchiveDate: " + universalTime2);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				int? num2 = new int?((int)item[StoreObjectSchema.ArchivePeriod]);
				stringBuilder.AppendLine("ArchivePeriod: " + num2);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				DateTime universalTime3 = ((ExDateTime)item[ItemSchema.ReceivedTime]).UniversalTime;
				stringBuilder.AppendLine("ReceivedDate: " + universalTime3);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				DateTime universalTime4 = ((ExDateTime)item[StoreObjectSchema.LastModifiedTime]).UniversalTime;
				stringBuilder.AppendLine("LastModifiedTime: " + universalTime4);
			}
			catch (PropertyErrorException)
			{
			}
			try
			{
				str = item[StoreObjectSchema.ParentItemId].ToString();
				stringBuilder.AppendLine("FolderId: " + str);
			}
			catch (PropertyErrorException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000B8AEC File Offset: 0x000B6CEC
		private ExchangePrincipal GetArchivePrincipal(ExchangePrincipal primaryEP, ADUser adUser)
		{
			return primaryEP.GetArchiveExchangePrincipal(RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000B8AF8 File Offset: 0x000B6CF8
		private string GetAllInnerExceptions(Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (ex != null)
			{
				stringBuilder.Append(ex.Message.ToString());
				stringBuilder.AppendLine();
				ex = ex.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000B8B38 File Offset: 0x000B6D38
		private string ReadMailboxTableProperties(MailboxSession archiveSession)
		{
			string result = string.Empty;
			archiveSession.Mailbox.Load(TestArchiveConnectivityTask.mailboxExtendedProperties);
			object[] properties = archiveSession.Mailbox.GetProperties(TestArchiveConnectivityTask.mailboxExtendedProperties);
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in TestArchiveConnectivityTask.mailboxExtendedProperties)
			{
				if (!(properties[num] is PropertyError))
				{
					if (propertyDefinition.Name.Equals(MailboxSchema.ControlDataForElcAssistant.Name))
					{
						if (propertyDefinition.Type.Equals(typeof(byte[])))
						{
							byte[] serializedData = (byte[])properties[num];
							ControlData controlData = ControlData.CreateFromByteArray(serializedData);
							result = controlData.LastProcessedDate.ToString("MMMM dd yyyy hh:mm tt") + " - GMT";
							break;
						}
						break;
					}
					else
					{
						result = "No Last Processed time found for the mailbox.";
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000B8C28 File Offset: 0x000B6E28
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (base.HasErrors)
				{
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000B8C64 File Offset: 0x000B6E64
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ArchiveConnectivityOutcome sendToPipeline = new ArchiveConnectivityOutcome(this.UserSmtp.ToString(), this.primaryFAI, this.primaryLastProcessedTime, this.archiveDomain, this.archiveDatabase, this.archiveFAI, this.archiveLastProcessedTime, this.complianceConfiguration, this.mrmProperties);
				this.PerformArchiveConnectivityTest(ref sendToPipeline);
				base.WriteObject(sendToPipeline);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000B8CE8 File Offset: 0x000B6EE8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestArchiveConnectivityIdentity(this.UserSmtp.ToString());
			}
		}

		// Token: 0x040020FD RID: 8445
		internal const string ArchiveConnectivity = "ArchiveConnectivity";

		// Token: 0x040020FE RID: 8446
		private static SmtpAddress UserSmtpParam = default(SmtpAddress);

		// Token: 0x040020FF RID: 8447
		private static string MessageIdParam = string.Empty;

		// Token: 0x04002100 RID: 8448
		private StringBuilder mrmPropReport = new StringBuilder();

		// Token: 0x04002101 RID: 8449
		private string primaryFAI = string.Empty;

		// Token: 0x04002102 RID: 8450
		private string primaryLastProcessedTime = string.Empty;

		// Token: 0x04002103 RID: 8451
		private string archiveFAI = string.Empty;

		// Token: 0x04002104 RID: 8452
		private string archiveLastProcessedTime = string.Empty;

		// Token: 0x04002105 RID: 8453
		private string complianceConfiguration = string.Empty;

		// Token: 0x04002106 RID: 8454
		private readonly string archiveDomain = string.Empty;

		// Token: 0x04002107 RID: 8455
		private readonly string archiveDatabase = string.Empty;

		// Token: 0x04002108 RID: 8456
		private static List<PropertyDefinition> mailboxExtendedProperties = null;

		// Token: 0x04002109 RID: 8457
		private string mrmProperties = string.Empty;

		// Token: 0x0400210A RID: 8458
		private static readonly DefaultFolderType[] mailboxFolders = new DefaultFolderType[]
		{
			DefaultFolderType.AllItems,
			DefaultFolderType.RecoverableItemsVersions,
			DefaultFolderType.RecoverableItemsDeletions,
			DefaultFolderType.RecoverableItemsPurges,
			DefaultFolderType.RecoverableItemsDiscoveryHolds
		};

		// Token: 0x0400210B RID: 8459
		private static readonly PropertyDefinition[] mrmStoreProps = new PropertyDefinition[]
		{
			StoreObjectSchema.PolicyTag,
			ItemSchema.StartDateEtc,
			ItemSchema.RetentionDate,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			ItemSchema.ArchiveDate,
			ItemSchema.ReceivedTime,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ParentItemId
		};
	}
}
