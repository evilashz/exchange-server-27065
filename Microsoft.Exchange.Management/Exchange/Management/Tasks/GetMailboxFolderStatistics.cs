using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200042A RID: 1066
	[Cmdlet("Get", "MailboxFolderStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxFolderStatistics : GetTenantADObjectWithIdentityTaskBase<MailboxOrMailUserIdParameter, MailboxFolderConfiguration>
	{
		// Token: 0x0600251C RID: 9500 RVA: 0x00094EF4 File Offset: 0x000930F4
		public GetMailboxFolderStatistics()
		{
			base.Fields["IncludeOldestAndNewestItems"] = new SwitchParameter(false);
			base.Fields["IncludeAnalysis"] = new SwitchParameter(false);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x00094F32 File Offset: 0x00093132
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception);
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00094F45 File Offset: 0x00093145
		// (set) Token: 0x0600251F RID: 9503 RVA: 0x00094F5C File Offset: 0x0009315C
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = false, ParameterSetName = "AuditLog")]
		public new MailboxOrMailUserIdParameter Identity
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x00094F6F File Offset: 0x0009316F
		// (set) Token: 0x06002521 RID: 9505 RVA: 0x00094F86 File Offset: 0x00093186
		[Parameter(Mandatory = false)]
		public ElcFolderType? FolderScope
		{
			get
			{
				return (ElcFolderType?)base.Fields["FolderScope"];
			}
			set
			{
				base.Fields["FolderScope"] = value;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x00094F9E File Offset: 0x0009319E
		// (set) Token: 0x06002523 RID: 9507 RVA: 0x00094FB5 File Offset: 0x000931B5
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeOldestAndNewestItems
		{
			get
			{
				return (SwitchParameter)base.Fields["IncludeOldestAndNewestItems"];
			}
			set
			{
				base.Fields["IncludeOldestAndNewestItems"] = value;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x00094FCD File Offset: 0x000931CD
		// (set) Token: 0x06002525 RID: 9509 RVA: 0x00094FF3 File Offset: 0x000931F3
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x0009500B File Offset: 0x0009320B
		// (set) Token: 0x06002527 RID: 9511 RVA: 0x00095022 File Offset: 0x00093222
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeAnalysis
		{
			get
			{
				return (SwitchParameter)base.Fields["IncludeAnalysis"];
			}
			set
			{
				base.Fields["IncludeAnalysis"] = value;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x0009503A File Offset: 0x0009323A
		// (set) Token: 0x06002529 RID: 9513 RVA: 0x00095060 File Offset: 0x00093260
		[Parameter(Mandatory = false, ParameterSetName = "AuditLog")]
		public SwitchParameter AuditLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuditLog"] ?? false);
			}
			set
			{
				base.Fields["AuditLog"] = value;
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00095078 File Offset: 0x00093278
		protected override IConfigDataProvider CreateSession()
		{
			TaskLogger.LogEnter();
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 206, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxFolder\\GetMailboxFolderStatistics.cs");
			TaskLogger.LogExit();
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000950C0 File Offset: 0x000932C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				using (MailboxSession mailboxSession = this.OpenMailboxSession())
				{
					if (mailboxSession == null)
					{
						base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.LogonFailure), ErrorCategory.ReadError, null);
					}
					else
					{
						ElcFolderType folderScope = (this.FolderScope != null) ? this.FolderScope.Value : ElcFolderType.All;
						List<object[]> list = this.PopulateFolderRows(mailboxSession, folderScope);
						if (list == null || list.Count == 0)
						{
							base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.NoFoldersInMailbox), ErrorCategory.ReadError, null);
						}
						else
						{
							for (int i = 0; i < list.Count; i++)
							{
								object[] array = list[i];
								if (GetMailboxFolderStatistics.PropertyExists(array[1]))
								{
									MailboxFolderConfiguration folderInformation = this.GetFolderInformation(i, list, mailboxSession, this.ConfigurationSession);
									if (folderInformation != null)
									{
										GetMailboxFolderStatistics.GetFolderSearchBacklinkNames(mailboxSession, (VersionedId)array[1], folderInformation);
										if (this.IncludeAnalysis)
										{
											GetMailboxFolderStatistics.GetAnalysis(mailboxSession, (VersionedId)array[1], folderInformation);
										}
										if (this.IncludeOldestAndNewestItems)
										{
											GetMailboxFolderStatistics.GetOldestAndNewestItem(mailboxSession, (VersionedId)array[1], folderInformation);
										}
										this.WriteResult(folderInformation);
									}
								}
							}
						}
					}
				}
			}
			catch (StorageTransientException ex)
			{
				base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), ex.ToString()), ErrorCategory.ReadError, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00095284 File Offset: 0x00093484
		private MailboxSession OpenMailboxSession()
		{
			IEnumerable<ADRecipient> objects = this.Identity.GetObjects<ADRecipient>(null, base.DataSession);
			MailboxSession result;
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.RecipientNotFoundException(this.Identity.ToString())), ErrorCategory.ReadError, null);
				}
				ADRecipient adrecipient = enumerator.Current;
				if (enumerator.MoveNext())
				{
					base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.RecipientNotUniqueException(this.Identity.ToString())), ErrorCategory.ReadError, null);
				}
				if (adrecipient.RecipientType == RecipientType.MailUser && !this.Archive.IsPresent)
				{
					base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.RecipientTypeNotValid(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
				}
				if (adrecipient.RecipientTypeDetails == RecipientTypeDetails.AuditLogMailbox && !this.AuditLog)
				{
					base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.RecipientNotFoundException(this.Identity.ToString())), ErrorCategory.ReadError, null);
				}
				ExchangePrincipal exchangePrincipal = null;
				if (this.Identity.RawMailboxGuidInvolvedInSearch != Guid.Empty)
				{
					ADUser aduser = adrecipient as ADUser;
					if (aduser != null && aduser.MailboxLocations != null)
					{
						IMailboxLocationInfo mailboxLocation = aduser.MailboxLocations.GetMailboxLocation(this.Identity.RawMailboxGuidInvolvedInSearch);
						if (mailboxLocation != null)
						{
							try
							{
								exchangePrincipal = ExchangePrincipal.FromMailboxGuid(adrecipient.Session.SessionSettings, mailboxLocation.MailboxGuid, null);
							}
							catch (ObjectNotFoundException)
							{
								base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.RecipientNotFoundException(this.Identity.ToString())), ErrorCategory.ReadError, null);
							}
						}
					}
				}
				if (exchangePrincipal == null)
				{
					ExchangePrincipal exchangePrincipal2 = null;
					try
					{
						exchangePrincipal2 = ExchangePrincipal.FromLegacyDN(adrecipient.OrganizationId.ToADSessionSettings(), adrecipient.LegacyExchangeDN, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
					}
					catch (ObjectNotFoundException)
					{
						base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.RecipientNotFoundException(this.Identity.ToString())), ErrorCategory.ReadError, null);
						return null;
					}
					exchangePrincipal = exchangePrincipal2;
					if (this.Archive && adrecipient.RecipientType != RecipientType.MailUser)
					{
						if (exchangePrincipal2.GetArchiveMailbox() != null)
						{
							exchangePrincipal = exchangePrincipal2.GetArchiveExchangePrincipal();
						}
						else
						{
							base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.ErrorArchiveNotEnabled(this.Identity.ToString())), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
				}
				try
				{
					result = MailboxSession.OpenAsSystemService(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Get-MailboxFolderStatistics");
				}
				catch (StorageTransientException ex)
				{
					base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.ExceptionStorageOther(ex.ErrorCode, ex.Message)), ErrorCategory.ReadError, null);
					result = null;
				}
				catch (StoragePermanentException ex2)
				{
					string failure;
					if (ex2 is AccessDeniedException)
					{
						failure = Strings.ExceptionStorageAccessDenied(ex2.ErrorCode, ex2.Message);
					}
					else
					{
						failure = Strings.ExceptionStorageOther(ex2.ErrorCode, ex2.Message);
					}
					base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), failure), ErrorCategory.ReadError, null);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x0009563C File Offset: 0x0009383C
		private MailboxFolderConfiguration GetFolderInformation(int folderIndex, List<object[]> allFolderRows, MailboxSession mailboxSession, IConfigurationSession adConfigSession)
		{
			MailboxFolderConfiguration mailboxFolderConfiguration = null;
			object[] array = allFolderRows[folderIndex];
			if (GetMailboxFolderStatistics.PropertyExists(array[1]) && GetMailboxFolderStatistics.PropertyExists(array[15]))
			{
				mailboxFolderConfiguration = new MailboxFolderConfiguration();
				string folderPath = (string)array[15];
				mailboxFolderConfiguration.FolderPath = folderPath;
				mailboxFolderConfiguration.SetIdentity(new MailboxFolderId(this.Identity.ToString(), mailboxFolderConfiguration.FolderPath));
				mailboxFolderConfiguration.Date = (DateTime)((ExDateTime)array[10]);
				if (GetMailboxFolderStatistics.PropertyExists(array[0]))
				{
					mailboxFolderConfiguration.Name = (string)array[0];
				}
				else
				{
					this.WriteWarning(Strings.UnableToRetrieveFolderName(mailboxFolderConfiguration.FolderPath));
				}
				StoreObjectId objectId = ((VersionedId)array[1]).ObjectId;
				mailboxFolderConfiguration.FolderId = objectId.ToBase64String();
				long num = 0L;
				if (GetMailboxFolderStatistics.PropertyExists(array[6]))
				{
					num = (long)array[6];
				}
				mailboxFolderConfiguration.FolderSize = ByteQuantifiedSize.FromBytes(checked((ulong)num));
				if (GetMailboxFolderStatistics.PropertyExists(array[4]))
				{
					mailboxFolderConfiguration.ItemsInFolder = (int)array[4];
					mailboxFolderConfiguration.ItemsInFolderAndSubfolders = mailboxFolderConfiguration.ItemsInFolder;
				}
				if (GetMailboxFolderStatistics.PropertyExists(array[5]))
				{
					mailboxFolderConfiguration.ItemsInFolder += (int)array[5];
					mailboxFolderConfiguration.ItemsInFolderAndSubfolders += (int)array[5];
				}
				if (GetMailboxFolderStatistics.PropertyExists(array[11]))
				{
					mailboxFolderConfiguration.DeletedItemsInFolder = (int)array[11];
					mailboxFolderConfiguration.DeletedItemsInFolderAndSubfolders = mailboxFolderConfiguration.DeletedItemsInFolder;
				}
				if (GetMailboxFolderStatistics.PropertyExists(array[12]))
				{
					mailboxFolderConfiguration.DeletedItemsInFolder += (int)array[12];
					mailboxFolderConfiguration.DeletedItemsInFolderAndSubfolders += mailboxFolderConfiguration.DeletedItemsInFolder;
				}
				int num2 = -1;
				if (GetMailboxFolderStatistics.PropertyExists(array[3]))
				{
					num2 = (int)array[3];
				}
				if (GetMailboxFolderStatistics.PropertyExists(array[2]) && (bool)array[2])
				{
					bool flag = false;
					bool flag2 = false;
					for (int i = folderIndex + 1; i < allFolderRows.Count; i++)
					{
						object[] array2 = allFolderRows[i];
						if (GetMailboxFolderStatistics.PropertyExists(array2[15]) && GetMailboxFolderStatistics.PropertyExists(array2[3]))
						{
							int num3 = (int)array2[3];
							if (num3 <= num2)
							{
								break;
							}
							if (GetMailboxFolderStatistics.PropertyExists(array2[6]))
							{
								long num4 = (long)array2[6];
								if (num4 > 0L)
								{
									num += num4;
								}
							}
							else
							{
								flag2 = true;
							}
							if (GetMailboxFolderStatistics.PropertyExists(array2[4]))
							{
								mailboxFolderConfiguration.ItemsInFolderAndSubfolders += (int)array2[4];
							}
							else
							{
								flag = true;
							}
							if (GetMailboxFolderStatistics.PropertyExists(array2[5]))
							{
								mailboxFolderConfiguration.ItemsInFolderAndSubfolders += (int)array2[5];
							}
							else
							{
								flag = true;
							}
							if (GetMailboxFolderStatistics.PropertyExists(array2[11]))
							{
								mailboxFolderConfiguration.DeletedItemsInFolderAndSubfolders += (int)array2[11];
							}
							if (GetMailboxFolderStatistics.PropertyExists(array2[12]))
							{
								mailboxFolderConfiguration.DeletedItemsInFolderAndSubfolders += (int)array2[12];
							}
						}
					}
					if (flag)
					{
						this.WriteWarning(Strings.TotalFolderCount(mailboxFolderConfiguration.FolderPath));
					}
					if (flag2)
					{
						this.WriteWarning(Strings.TotalFolderSize(mailboxFolderConfiguration.FolderPath));
					}
				}
				mailboxFolderConfiguration.FolderAndSubfolderSize = ByteQuantifiedSize.FromBytes(checked((ulong)num));
				if (GetMailboxFolderStatistics.PropertyExists(array[7]))
				{
					bool flag3 = ((int)array[7] & 1) != 0;
					mailboxFolderConfiguration.FolderType = (flag3 ? ElcFolderType.ManagedCustomFolder.ToString() : null);
				}
				if (mailboxFolderConfiguration.FolderType == null)
				{
					VersionedId folderId = (VersionedId)array[1];
					DefaultFolderType defaultFolderType = mailboxSession.IsDefaultFolderType(folderId);
					if (defaultFolderType == DefaultFolderType.None)
					{
						if (GetMailboxFolderStatistics.PropertyExists(array[9]))
						{
							StoreObjectId folderId2 = (StoreObjectId)array[9];
							defaultFolderType = mailboxSession.IsDefaultFolderType(folderId2);
						}
						switch (defaultFolderType)
						{
						case DefaultFolderType.AdminAuditLogs:
						case DefaultFolderType.Audits:
							mailboxFolderConfiguration.FolderType = defaultFolderType.ToString();
							break;
						default:
							mailboxFolderConfiguration.FolderType = Strings.UserCreatedFolder;
							break;
						}
					}
					else
					{
						mailboxFolderConfiguration.FolderType = defaultFolderType.ToString();
					}
				}
				if (GetMailboxFolderStatistics.PropertyExists(array[8]))
				{
					Guid guid = new Guid((string)array[8]);
					ADObjectId valueToSearch = new ADObjectId(guid.ToByteArray());
					List<ELCFolder> list = ELCTaskHelper.FindELCFolder(adConfigSession, valueToSearch, FindByType.FolderAdObjectId);
					if (list == null || list.Count != 1)
					{
						this.WriteWarning(Strings.UnableToRetrieveManagedFolder);
					}
					else
					{
						mailboxFolderConfiguration.ManagedFolder = new ELCFolderIdParameter(list[0].Id);
					}
				}
				IConfigurationSession configurationSession = null;
				if (GetMailboxFolderStatistics.PropertyExists(array[13]))
				{
					byte[] array3 = (byte[])array[13];
					if (array3 != null)
					{
						Guid tagGuid = new Guid(array3);
						configurationSession = this.CreateConfigurationSession(mailboxSession);
						List<RetentionPolicyTag> list2 = ELCTaskHelper.FindRetentionPolicyTag(configurationSession, tagGuid);
						if (list2 == null || list2.Count != 1)
						{
							this.WriteWarning(Strings.UnableToRetrieveDeletePolicyTag);
						}
						else
						{
							mailboxFolderConfiguration.DeletePolicy = new RetentionPolicyTagIdParameter(list2[0].Id);
						}
					}
				}
				if (GetMailboxFolderStatistics.PropertyExists(array[14]))
				{
					byte[] array4 = (byte[])array[14];
					if (array4 != null)
					{
						Guid tagGuid2 = new Guid(array4);
						if (configurationSession == null)
						{
							configurationSession = this.CreateConfigurationSession(mailboxSession);
						}
						List<RetentionPolicyTag> list3 = ELCTaskHelper.FindRetentionPolicyTag(configurationSession, tagGuid2);
						if (list3 == null || list3.Count != 1)
						{
							this.WriteWarning(Strings.UnableToRetrieveArchivePolicyTag);
						}
						else
						{
							mailboxFolderConfiguration.ArchivePolicy = new RetentionPolicyTagIdParameter(list3[0].Id);
						}
					}
				}
			}
			return mailboxFolderConfiguration;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00095B64 File Offset: 0x00093D64
		private IConfigurationSession CreateConfigurationSession(MailboxSession mailboxSession)
		{
			OrganizationId organizationId = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
			IConfigurationSession configurationSession;
			if (SharedConfiguration.IsDehydratedConfiguration(organizationId))
			{
				configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(organizationId);
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, configurationSession.SessionSettings, 742, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxFolder\\GetMailboxFolderStatistics.cs");
			}
			else
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, organizationId, base.ExecutingUserOrganizationId, false);
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 755, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxFolder\\GetMailboxFolderStatistics.cs");
			}
			return configurationSession;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00095BF4 File Offset: 0x00093DF4
		private List<object[]> PopulateFolderRows(MailboxSession mailboxSession, ElcFolderType folderScope)
		{
			List<object[]> list = null;
			List<object[]> result;
			try
			{
				using (Folder rootFolder = GetMailboxFolderStatistics.GetRootFolder(mailboxSession, folderScope))
				{
					if (rootFolder == null)
					{
						if (folderScope != ElcFolderType.ManagedCustomFolder)
						{
							base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.ErrorFailedToFindFolderInMailbox(folderScope.ToString())), ErrorCategory.ReadError, null);
						}
						else
						{
							base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.ErrorFailedToManagedFoldersInMailbox), ErrorCategory.ReadError, null);
						}
						result = list;
					}
					else
					{
						list = new List<object[]>();
						if (folderScope != ElcFolderType.ManagedCustomFolder)
						{
							object[] array = new object[GetMailboxFolderStatistics.folderStatProps.Length];
							array[1] = rootFolder[GetMailboxFolderStatistics.folderStatProps[1]];
							array[2] = rootFolder[GetMailboxFolderStatistics.folderStatProps[2]];
							array[4] = rootFolder[GetMailboxFolderStatistics.folderStatProps[4]];
							array[5] = rootFolder[GetMailboxFolderStatistics.folderStatProps[5]];
							array[6] = rootFolder.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[6]);
							array[0] = rootFolder.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[0]);
							array[7] = rootFolder.TryGetProperty(FolderSchema.AdminFolderFlags);
							array[9] = rootFolder.TryGetProperty(StoreObjectSchema.ParentItemId);
							array[8] = rootFolder.TryGetProperty(FolderSchema.ELCPolicyIds);
							array[13] = rootFolder.TryGetProperty(StoreObjectSchema.PolicyTag);
							array[14] = rootFolder.TryGetProperty(StoreObjectSchema.ArchiveTag);
							array[11] = rootFolder.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[11]);
							array[12] = rootFolder.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[12]);
							array[3] = 0;
							array[10] = rootFolder.TryGetProperty(StoreObjectSchema.CreationTime);
							list.Add(array);
						}
						GetMailboxFolderStatistics.GetFolderHierarchy(rootFolder, list);
						if (folderScope == ElcFolderType.All)
						{
							using (Folder rootFolder2 = GetMailboxFolderStatistics.GetRootFolder(mailboxSession, ElcFolderType.RecoverableItems))
							{
								if (rootFolder2 != null)
								{
									object[] array2 = new object[GetMailboxFolderStatistics.folderStatProps.Length];
									array2[1] = rootFolder2[GetMailboxFolderStatistics.folderStatProps[1]];
									array2[2] = rootFolder2[GetMailboxFolderStatistics.folderStatProps[2]];
									array2[4] = rootFolder2[GetMailboxFolderStatistics.folderStatProps[4]];
									array2[5] = rootFolder2[GetMailboxFolderStatistics.folderStatProps[5]];
									array2[6] = rootFolder2.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[6]);
									array2[0] = rootFolder2.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[0]);
									array2[7] = rootFolder2.TryGetProperty(FolderSchema.AdminFolderFlags);
									array2[9] = rootFolder2.TryGetProperty(StoreObjectSchema.ParentItemId);
									array2[8] = rootFolder2.TryGetProperty(FolderSchema.ELCPolicyIds);
									array2[13] = rootFolder2.TryGetProperty(StoreObjectSchema.PolicyTag);
									array2[14] = rootFolder2.TryGetProperty(StoreObjectSchema.ArchiveTag);
									array2[11] = rootFolder2.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[11]);
									array2[12] = rootFolder2.TryGetProperty(GetMailboxFolderStatistics.folderStatProps[12]);
									array2[3] = 0;
									array2[10] = rootFolder2.TryGetProperty(StoreObjectSchema.CreationTime);
									list.Add(array2);
									GetMailboxFolderStatistics.GetFolderHierarchy(rootFolder2, list);
								}
							}
						}
						ElcMailboxHelper.PopulateFolderPathProperty(list, new FolderPathIndices(0, 3, 1, 9, 15));
						result = list;
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				base.WriteError(new MailboxFolderStatisticsException(this.Identity.ToString(), Strings.ErrorFailedToFindFolderInMailbox(folderScope.ToString())), ErrorCategory.ReadError, null);
				result = null;
			}
			return result;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00095F54 File Offset: 0x00094154
		private static void GetFolderHierarchy(Folder rootFolder, List<object[]> rows)
		{
			using (QueryResult queryResult = rootFolder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, GetMailboxFolderStatistics.folderStatProps))
			{
				for (;;)
				{
					object[][] rows2 = queryResult.GetRows(100);
					if (rows2.Length <= 0)
					{
						break;
					}
					for (int i = 0; i < rows2.Length; i++)
					{
						rows.Add(rows2[i]);
					}
				}
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00095FB4 File Offset: 0x000941B4
		private static void GetFolderSearchBacklinkNames(MailboxSession mailboxSession, VersionedId folderId, MailboxFolderConfiguration folderInfo)
		{
			PropertyDefinition[] propsToReturn = new PropertyDefinition[]
			{
				FolderSchema.SearchBacklinkNames
			};
			using (Folder folder = Folder.Bind(mailboxSession, folderId, propsToReturn))
			{
				object obj = folder.TryGetProperty(FolderSchema.SearchBacklinkNames);
				if (GetMailboxFolderStatistics.PropertyExists(obj))
				{
					folderInfo.SearchFolders = (string[])obj;
				}
			}
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00096090 File Offset: 0x00094290
		private static void GetOldestAndNewestItem(MailboxSession mailboxSession, VersionedId folderId, MailboxFolderConfiguration folderInfo)
		{
			using (Folder folder = Folder.Bind(mailboxSession, folderId))
			{
				GetMailboxFolderStatistics.GetOldestAndNewestItemDatesByProperty(folder, GetMailboxFolderStatistics.receivedTimeProp, delegate(DateTime? value)
				{
					folderInfo.NewestItemReceivedDate = value;
				}, delegate(DateTime? value)
				{
					folderInfo.OldestItemReceivedDate = value;
				}, delegate(DateTime? value)
				{
					folderInfo.NewestDeletedItemReceivedDate = value;
				}, delegate(DateTime? value)
				{
					folderInfo.OldestDeletedItemReceivedDate = value;
				});
				GetMailboxFolderStatistics.GetOldestAndNewestItemDatesByProperty(folder, GetMailboxFolderStatistics.lastModifiedTimeProp, delegate(DateTime? value)
				{
					folderInfo.NewestItemLastModifiedDate = value;
				}, delegate(DateTime? value)
				{
					folderInfo.OldestItemLastModifiedDate = value;
				}, delegate(DateTime? value)
				{
					folderInfo.NewestDeletedItemLastModifiedDate = value;
				}, delegate(DateTime? value)
				{
					folderInfo.OldestDeletedItemLastModifiedDate = value;
				});
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0009619C File Offset: 0x0009439C
		private static void GetOldestAndNewestItemDatesByProperty(Folder folder, PropertyDefinition[] propertyToSortBy, GetMailboxFolderStatistics.SetItemDate newestItemDate, GetMailboxFolderStatistics.SetItemDate oldestItemDate, GetMailboxFolderStatistics.SetItemDate newestDeletedItemDate, GetMailboxFolderStatistics.SetItemDate oldestDeletedItemDate)
		{
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, GetMailboxFolderStatistics.sortColumns, propertyToSortBy))
			{
				if (queryResult.EstimatedRowCount < 1)
				{
					newestItemDate(null);
					oldestItemDate(null);
				}
				else
				{
					object[][] rows = queryResult.GetRows(1);
					if (GetMailboxFolderStatistics.PropertyExists(rows[0][0]))
					{
						newestItemDate(new DateTime?((DateTime)((ExDateTime)rows[0][0])));
					}
					queryResult.SeekToOffset(SeekReference.BackwardFromEnd, -1);
					rows = queryResult.GetRows(1);
					if (GetMailboxFolderStatistics.PropertyExists(rows[0][0]))
					{
						oldestItemDate(new DateTime?((DateTime)((ExDateTime)rows[0][0])));
					}
				}
			}
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00096264 File Offset: 0x00094464
		private static void GetAnalysis(MailboxSession mailboxSession, VersionedId folderId, MailboxFolderConfiguration folderInfo)
		{
			StoreObjectId[] folderIds = new StoreObjectId[]
			{
				folderId.ObjectId
			};
			AnalysisFolderItems analysisFolderItems = new AnalysisFolderItems(mailboxSession, folderIds);
			analysisFolderItems.Execute();
			if (analysisFolderItems.GroupsBySize().Count > 0)
			{
				AnalysisGroupData analysisGroupData = analysisFolderItems.GroupsBySize()[0];
				folderInfo.TopSubjectCount = analysisGroupData.GroupCount;
				folderInfo.TopSubjectSize = analysisGroupData.GroupSize;
				folderInfo.TopClientInfoForSubject = analysisGroupData.TopClientInfo.Key;
				folderInfo.TopClientInfoCountForSubject = analysisGroupData.TopClientInfo.Value;
				Item item = null;
				string topSubjectPath = string.Empty;
				try
				{
					item = analysisGroupData.GetItemInAllItems(GetMailboxFolderStatistics.itemAnalysisProperties);
					topSubjectPath = analysisGroupData.GetItemInAllItemsFolderPath();
					if (item == null)
					{
						item = analysisGroupData.GetItemInGroup(GetMailboxFolderStatistics.itemAnalysisProperties);
						topSubjectPath = analysisGroupData.GetItemInGroupFolderPath();
					}
					if (item != null)
					{
						folderInfo.TopSubject = item.GetValueOrDefault<string>(ItemSchema.NormalizedSubject, string.Empty);
						folderInfo.TopSubjectPath = topSubjectPath;
						folderInfo.TopSubjectClass = item.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
						Participant valueOrDefault = item.GetValueOrDefault<Participant>(ItemSchema.From);
						folderInfo.TopSubjectFrom = ((valueOrDefault != null) ? valueOrDefault.DisplayName : string.Empty);
						folderInfo.TopSubjectReceivedTime = new DateTime?((DateTime)item.GetValueOrDefault<ExDateTime>(ItemSchema.ReceivedTime, ExDateTime.MinValue));
					}
				}
				finally
				{
					if (item != null)
					{
						item.Dispose();
					}
				}
			}
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x000963C8 File Offset: 0x000945C8
		private static Folder GetRootFolder(MailboxSession mailboxSession, ElcFolderType folderType)
		{
			Folder result = null;
			if (folderType == ElcFolderType.Inbox)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Inbox, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Calendar)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Calendar, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.SentItems)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.SentItems, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.DeletedItems)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.DeletedItems, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Contacts)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Contacts, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Calendar)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Calendar, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Drafts)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Drafts, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Outbox)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Outbox, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.JunkEmail)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.JunkEmail, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Tasks)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Tasks, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Journal)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Journal, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.Notes)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Notes, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.All)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Root, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.SyncIssues)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.SyncIssues, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.RssSubscriptions)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.RssSubscription, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.ConversationHistory)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.CommunicatorHistory, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.LegacyArchiveJournals)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.LegacyArchiveJournals, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.NonIpmRoot)
			{
				result = Folder.Bind(mailboxSession, DefaultFolderType.Configuration, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.RecoverableItems)
			{
				if (mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot) == null)
				{
					return null;
				}
				result = Folder.Bind(mailboxSession, DefaultFolderType.RecoverableItemsRoot, GetMailboxFolderStatistics.folderStatProps);
			}
			else if (folderType == ElcFolderType.ManagedCustomFolder)
			{
				StoreObjectId storeObjectId = null;
				string text = null;
				string text2 = null;
				ProvisionedFolderReader.GetElcRootFolderInfo(mailboxSession, out storeObjectId, out text, out text2);
				if (storeObjectId == null)
				{
					return null;
				}
				result = Folder.Bind(mailboxSession, storeObjectId, GetMailboxFolderStatistics.folderStatProps);
			}
			return result;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x000965B7 File Offset: 0x000947B7
		private static bool PropertyExists(object property)
		{
			return property != null && !(property is PropertyError);
		}

		// Token: 0x04001D38 RID: 7480
		private const int displayNameIndex = 0;

		// Token: 0x04001D39 RID: 7481
		private const int folderIdIndex = 1;

		// Token: 0x04001D3A RID: 7482
		private const int hasChildrenIndex = 2;

		// Token: 0x04001D3B RID: 7483
		private const int depthIndex = 3;

		// Token: 0x04001D3C RID: 7484
		private const int countIndex = 4;

		// Token: 0x04001D3D RID: 7485
		private const int associatedCountIndex = 5;

		// Token: 0x04001D3E RID: 7486
		private const int sizeIndex = 6;

		// Token: 0x04001D3F RID: 7487
		private const int adminFolderFlagsIndex = 7;

		// Token: 0x04001D40 RID: 7488
		private const int policyIdIndex = 8;

		// Token: 0x04001D41 RID: 7489
		private const int parentIdIndex = 9;

		// Token: 0x04001D42 RID: 7490
		private const int creationTimeIndex = 10;

		// Token: 0x04001D43 RID: 7491
		private const int deletedMsgIndex = 11;

		// Token: 0x04001D44 RID: 7492
		private const int deletedAssocIndex = 12;

		// Token: 0x04001D45 RID: 7493
		private const int policyTagIndex = 13;

		// Token: 0x04001D46 RID: 7494
		private const int archiveTagIndex = 14;

		// Token: 0x04001D47 RID: 7495
		private const int folderPathIndex = 15;

		// Token: 0x04001D48 RID: 7496
		private const int provisionedFolderFlag = 1;

		// Token: 0x04001D49 RID: 7497
		private const string parameterIncludeOldestAndNewestItems = "IncludeOldestAndNewestItems";

		// Token: 0x04001D4A RID: 7498
		private const string parameterArchive = "Archive";

		// Token: 0x04001D4B RID: 7499
		private const string parameterIncludeAnalysyis = "IncludeAnalysis";

		// Token: 0x04001D4C RID: 7500
		private static readonly PropertyDefinition[] folderStatProps = new PropertyDefinition[]
		{
			StoreObjectSchema.DisplayName,
			FolderSchema.Id,
			FolderSchema.HasChildren,
			FolderSchema.FolderHierarchyDepth,
			FolderSchema.ItemCount,
			FolderSchema.AssociatedItemCount,
			FolderSchema.ExtendedSize,
			FolderSchema.AdminFolderFlags,
			FolderSchema.ELCPolicyIds,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.CreationTime,
			FolderSchema.DeletedMsgCount,
			FolderSchema.DeletedAssocMsgCount,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.ArchiveTag
		};

		// Token: 0x04001D4D RID: 7501
		private static readonly SortBy[] sortColumns = new SortBy[]
		{
			new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
		};

		// Token: 0x04001D4E RID: 7502
		private static readonly PropertyDefinition[] receivedTimeProp = new PropertyDefinition[]
		{
			ItemSchema.ReceivedTime
		};

		// Token: 0x04001D4F RID: 7503
		private static readonly PropertyDefinition[] lastModifiedTimeProp = new PropertyDefinition[]
		{
			StoreObjectSchema.LastModifiedTime
		};

		// Token: 0x04001D50 RID: 7504
		private static readonly PropertyDefinition[] itemAnalysisProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.NormalizedSubject,
			ItemSchema.ReceivedTime,
			StoreObjectSchema.ItemClass,
			ItemSchema.From
		};

		// Token: 0x0200042B RID: 1067
		// (Invoke) Token: 0x06002539 RID: 9529
		private delegate void SetItemDate(DateTime? value);
	}
}
