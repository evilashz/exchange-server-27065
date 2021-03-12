using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000054 RID: 84
	public class RemoveMailboxBase<TIdentity> : RemoveRecipientObjectTask<TIdentity, ADUser> where TIdentity : IIdentityParameter
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00016741 File Offset: 0x00014941
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x00016762 File Offset: 0x00014962
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool Permanent
		{
			get
			{
				return (bool)(base.Fields["Permanent"] ?? false);
			}
			set
			{
				base.Fields["Permanent"] = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001677A File Offset: 0x0001497A
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x00016791 File Offset: 0x00014991
		[Parameter(Mandatory = true, ParameterSetName = "StoreMailboxIdentity")]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000167A4 File Offset: 0x000149A4
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x000167BB File Offset: 0x000149BB
		[Parameter(Mandatory = true, ParameterSetName = "StoreMailboxIdentity")]
		public StoreMailboxIdParameter StoreMailboxIdentity
		{
			get
			{
				return (StoreMailboxIdParameter)base.Fields["StoreMailboxIdentity"];
			}
			set
			{
				base.Fields["StoreMailboxIdentity"] = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000167CE File Offset: 0x000149CE
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x000167F4 File Offset: 0x000149F4
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter KeepWindowsLiveID
		{
			get
			{
				return (SwitchParameter)(base.Fields["KeepWindowsLiveID"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["KeepWindowsLiveID"] = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001680C File Offset: 0x00014A0C
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x00016832 File Offset: 0x00014A32
		[Parameter(Mandatory = false)]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? false);
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001684A File Offset: 0x00014A4A
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00016870 File Offset: 0x00014A70
		[Parameter(Mandatory = false)]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? false);
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00016888 File Offset: 0x00014A88
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x000168AE File Offset: 0x00014AAE
		[Parameter(Mandatory = false)]
		public SwitchParameter RemoveLastArbitrationMailboxAllowed
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveLastArbitrationMailboxAllowed"] ?? false);
			}
			set
			{
				base.Fields["RemoveLastArbitrationMailboxAllowed"] = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000168C6 File Offset: 0x00014AC6
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000168EC File Offset: 0x00014AEC
		[Parameter(Mandatory = false)]
		public SwitchParameter RemoveArbitrationMailboxWithOABsAllowed
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveArbitrationMailboxWithOABsAllowed"] ?? false);
			}
			set
			{
				base.Fields["RemoveArbitrationMailboxWithOABsAllowed"] = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00016904 File Offset: 0x00014B04
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001692A File Offset: 0x00014B2A
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreLegalHold
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreLegalHold"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreLegalHold"] = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00016942 File Offset: 0x00014B42
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001694A File Offset: 0x00014B4A
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00016953 File Offset: 0x00014B53
		internal virtual bool ArbitrationMailboxUsageValidationRequired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00016956 File Offset: 0x00014B56
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001697C File Offset: 0x00014B7C
		[Parameter(Mandatory = false)]
		public SwitchParameter Disconnect
		{
			get
			{
				return (SwitchParameter)(base.Fields["Disconnect"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Disconnect"] = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00016994 File Offset: 0x00014B94
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000169BA File Offset: 0x00014BBA
		[Parameter(Mandatory = false)]
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000169D4 File Offset: 0x00014BD4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("StoreMailboxIdentity" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageRemoveMailboxStoreMailboxIdentity(this.Database.ToString(), this.StoreMailboxIdentity.ToString());
				}
				if (base.DataObject.NetID != null)
				{
					if (this.KeepWindowsLiveID)
					{
						if (this.Permanent)
						{
							TIdentity identity = this.Identity;
							return Strings.ConfirmationMessageRemoveMailboxPermanentAndNotLiveId(identity.ToString(), base.DataObject.WindowsLiveID.ToString());
						}
						TIdentity identity2 = this.Identity;
						return Strings.ConfirmationMessageRemoveMailboxIdentityAndNotLiveId(identity2.ToString(), base.DataObject.WindowsLiveID.ToString());
					}
					else
					{
						if (this.Permanent)
						{
							TIdentity identity3 = this.Identity;
							return Strings.ConfirmationMessageRemoveMailboxPermanentAndLiveId(identity3.ToString(), base.DataObject.WindowsLiveID.ToString());
						}
						TIdentity identity4 = this.Identity;
						return Strings.ConfirmationMessageRemoveMailboxIdentityAndLiveId(identity4.ToString(), base.DataObject.WindowsLiveID.ToString());
					}
				}
				else
				{
					if (this.Permanent)
					{
						TIdentity identity5 = this.Identity;
						return Strings.ConfirmationMessageRemoveMailboxPermanent(identity5.ToString());
					}
					TIdentity identity6 = this.Identity;
					return Strings.ConfirmationMessageRemoveMailboxIdentity(identity6.ToString());
				}
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00016B50 File Offset: 0x00014D50
		private IConfigurationSession TenantLocalConfigurationSession
		{
			get
			{
				IConfigurationSession result;
				if ((result = this.tenantLocalConfigurationSession) == null)
				{
					result = (this.tenantLocalConfigurationSession = RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, false, ((IRecipientSession)base.DataSession).LastUsedDc.ToString(), null));
				}
				return result;
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00016B9E File Offset: 0x00014D9E
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.DisposeMapiSession();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016BAC File Offset: 0x00014DAC
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.Disconnect.IsPresent || base.ForReconciliation.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, null);
			}
			return recipientSession;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00016BF0 File Offset: 0x00014DF0
		private void InternalValidateStoreMailboxIdentity()
		{
			TaskLogger.LogEnter();
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())), ExchangeErrorCategory.Client);
			if (mailboxDatabase.Recovery)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorInvalidOperationOnRecoveryMailboxDatabase(this.Database.ToString())), ExchangeErrorCategory.Client, this.StoreMailboxIdentity);
			}
			DatabaseLocationInfo databaseLocationInfo = null;
			try
			{
				databaseLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(mailboxDatabase.Id.ObjectGuid);
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
			}
			try
			{
				base.WriteVerbose(Strings.VerboseConnectionAdminRpcInterface(databaseLocationInfo.ServerFqdn));
				this.mapiSession = new MapiAdministrationSession(databaseLocationInfo.ServerLegacyDN, Fqdn.Parse(databaseLocationInfo.ServerFqdn));
			}
			catch (MapiPermanentException exception2)
			{
				base.WriteError(exception2, ExchangeErrorCategory.ServerOperation, null);
			}
			catch (MapiRetryableException exception3)
			{
				base.WriteError(exception3, ExchangeErrorCategory.ServerTransient, null);
			}
			this.database = mailboxDatabase;
			TaskLogger.LogExit();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00016D28 File Offset: 0x00014F28
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.isToInactiveMailbox = false;
			this.isDisconnectInactiveMailbox = false;
			if (this.Identity != null)
			{
				base.InternalValidate();
				this.isToInactiveMailbox = this.IsToInactiveMailbox();
				this.isDisconnectInactiveMailbox = this.IsDisconnectInactiveMailbox();
				if (!this.isToInactiveMailbox && !this.isDisconnectInactiveMailbox)
				{
					MailboxTaskHelper.BlockRemoveOrDisableIfLitigationHoldEnabled(base.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), false, this.IgnoreLegalHold.ToBool());
					MailboxTaskHelper.BlockRemoveOrDisableIfDiscoveryHoldEnabled(base.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), false, this.IgnoreLegalHold.ToBool());
				}
				MailboxTaskHelper.BlockRemoveOrDisableIfJournalNDRMailbox(base.DataObject, this.TenantLocalConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), false);
				if (ComplianceConfigImpl.JournalArchivingHardeningEnabled && !this.skipJournalArchivingCheck)
				{
					MailboxTaskHelper.BlockRemoveOrDisableMailboxIfJournalArchiveEnabled(base.DataSession as IRecipientSession, this.ConfigurationSession, base.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), false);
				}
				if (base.DataObject.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox && this.ArbitrationMailboxUsageValidationRequired)
				{
					ADUser dataObject = base.DataObject;
					Task.ErrorLoggerDelegate writeError = new Task.ErrorLoggerDelegate(base.WriteError);
					TIdentity identity = this.Identity;
					MailboxTaskHelper.ValidateNotBuiltInArbitrationMailbox(dataObject, writeError, Strings.ErrorRemoveArbitrationMailbox(identity.ToString()));
					ADUser dataObject2 = base.DataObject;
					IRecipientSession tenantGlobalCatalogSession = base.TenantGlobalCatalogSession;
					Task.ErrorLoggerDelegate writeError2 = new Task.ErrorLoggerDelegate(base.WriteError);
					TIdentity identity2 = this.Identity;
					MailboxTaskHelper.ValidateArbitrationMailboxHasNoGroups(dataObject2, tenantGlobalCatalogSession, writeError2, Strings.ErrorRemoveMailboxWithAssociatedApprovalRecipents(identity2.ToString()));
					ADUser dataObject3 = base.DataObject;
					bool overrideCheck = this.RemoveArbitrationMailboxWithOABsAllowed.ToBool();
					Task.ErrorLoggerDelegate writeError3 = new Task.ErrorLoggerDelegate(base.WriteError);
					TIdentity identity3 = this.Identity;
					MailboxTaskHelper.ValidateNoOABsAssignedToArbitrationMailbox(dataObject3, overrideCheck, writeError3, Strings.ErrorRemoveArbitrationMailboxWithOABsAssigned(identity3.ToString()));
					ADUser dataObject4 = base.DataObject;
					IRecipientSession tenantGlobalCatalogSession2 = base.TenantGlobalCatalogSession;
					ADObjectId rootOrgContainerId = base.RootOrgContainerId;
					bool isPresent = this.RemoveLastArbitrationMailboxAllowed.IsPresent;
					Task.ErrorLoggerDelegate writeError4 = new Task.ErrorLoggerDelegate(base.WriteError);
					TIdentity identity4 = this.Identity;
					MailboxTaskHelper.ValidateNotLastArbitrationMailbox(dataObject4, tenantGlobalCatalogSession2, rootOrgContainerId, isPresent, writeError4, Strings.ErrorCannotRemoveLastArbitrationMailboxInOrganization(identity4.ToString()));
				}
				if (this.AuditLog)
				{
					if (base.DataObject.RecipientTypeDetails != RecipientTypeDetails.AuditLogMailbox)
					{
						LocalizedException exception = new RecipientTaskException(Strings.ErrorSpecifiedMailboxShouldBeAuditLogMailbox(base.DataObject.Identity.ToString()));
						ExchangeErrorCategory category = ExchangeErrorCategory.Context;
						TIdentity identity5 = this.Identity;
						base.WriteError(exception, category, identity5.ToString());
					}
				}
				else if (base.DataObject.RecipientTypeDetails == RecipientTypeDetails.AuditLogMailbox)
				{
					LocalizedException exception2 = new RecipientTaskException(Strings.ErrorAuditLogMailboxShouldBeDeletedWithAuditLogSpecified(base.DataObject.Identity.ToString()));
					ExchangeErrorCategory category2 = ExchangeErrorCategory.Context;
					TIdentity identity6 = this.Identity;
					base.WriteError(exception2, category2, identity6.ToString());
				}
			}
			else
			{
				this.InternalValidateStoreMailboxIdentity();
				try
				{
					this.mailboxStatistics = (MailboxStatistics)base.GetDataObject<MailboxStatistics>(this.StoreMailboxIdentity, this.mapiSession, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(this.database), new LocalizedString?(Strings.ErrorStoreMailboxNotFound(this.StoreMailboxIdentity.ToString(), this.Database.ToString())), new LocalizedString?(Strings.ErrorStoreMailboxNotUnique(this.StoreMailboxIdentity.ToString(), this.Database.ToString())), ExchangeErrorCategory.Client);
					MailboxTaskHelper.ValidateMailboxIsDisconnected(base.TenantGlobalCatalogSession, this.mailboxStatistics.MailboxGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
					this.mailboxStatistics.Database = this.database.Identity;
				}
				catch (DataSourceTransientException exception3)
				{
					base.WriteError(exception3, ExchangeErrorCategory.ServerTransient, this.StoreMailboxIdentity);
				}
			}
			if (this.PublicFolder)
			{
				Organization orgContainer = this.TenantLocalConfigurationSession.GetOrgContainer();
				if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid == Guid.Empty && !this.Force)
				{
					LocalizedException exception4 = new RecipientTaskException(Strings.ErrorPrimaryPublicFolderMailboxNotFound);
					ExchangeErrorCategory category3 = ExchangeErrorCategory.Context;
					TIdentity identity7 = this.Identity;
					base.WriteError(exception4, category3, identity7.ToString());
				}
				if (this.currentOrganizationId == null || this.currentOrganizationId != base.DataObject.OrganizationId)
				{
					this.currentOrganizationId = base.DataObject.OrganizationId;
					TenantPublicFolderConfigurationCache.Instance.RemoveValue(base.DataObject.OrganizationId);
				}
				MailboxTaskHelper.RemoveOrDisablePublicFolderMailbox(base.DataObject, Guid.Empty, this.tenantLocalConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), false, this.Force);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000171C4 File Offset: 0x000153C4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADObjectId adobjectId = null;
			Guid guid = Guid.Empty;
			string text = string.Empty;
			bool flag = this.ShouldSoftDeleteObject();
			ADUser dataObject = base.DataObject;
			if (Globals.IsMicrosoftHostedOnly)
			{
				if (flag)
				{
					bool flag2 = SoftDeletedTaskHelper.MSOSyncEnabled(this.ConfigurationSession, dataObject.OrganizationId);
					bool includeInGarbageCollection = (!flag2 || base.ForReconciliation) && !this.isToInactiveMailbox;
					SoftDeletedTaskHelper.UpdateRecipientForSoftDelete(base.DataSession as IRecipientSession, dataObject, includeInGarbageCollection, this.isToInactiveMailbox);
				}
				else
				{
					if (this.isDisconnectInactiveMailbox)
					{
						SoftDeletedTaskHelper.UpdateMailboxForDisconnectInactiveMailbox(dataObject);
						base.DataSession.Save(dataObject);
						TaskLogger.LogExit();
						this.LogRemoveMailboxDetails(dataObject);
						return;
					}
					dataObject.RecipientSoftDeletedStatus = 0;
				}
			}
			if (this.Identity != null)
			{
				adobjectId = base.DataObject.Database;
				guid = base.DataObject.ExchangeGuid;
				if (adobjectId == null)
				{
					TaskLogger.Trace("The homeMDB is empty for this user, we just try to remove the user AD object", new object[0]);
				}
				else if (guid == Guid.Empty)
				{
					TaskLogger.Trace("The ExchangeGuid is empty for this user, we just try to remove the user AD object", new object[0]);
				}
				else if (base.DataObject.RecipientTypeDetails == RecipientTypeDetails.MailboxPlan)
				{
					TaskLogger.Trace("This user is MailboxPlan, we just try to remove the user AD object", new object[0]);
				}
				else if (!flag)
				{
					try
					{
						DatabaseLocationInfo databaseLocationInfo = null;
						try
						{
							databaseLocationInfo = ActiveManager.GetActiveManagerInstance().GetServerForDatabase(adobjectId.ObjectGuid);
						}
						catch (ObjectNotFoundException exception)
						{
							base.WriteError(exception, ExchangeErrorCategory.ServerOperation, null);
						}
						if (databaseLocationInfo == null)
						{
							if (this.Permanent)
							{
								base.WriteError(new TaskInvalidOperationException(Strings.ErrorGetServerNameFromMailbox(base.DataObject.Identity.ToString())), ExchangeErrorCategory.ServerOperation, base.DataObject);
							}
							else
							{
								TaskLogger.Trace("cannot get the server name for mailbox {0}", new object[]
								{
									base.DataObject.Identity
								});
							}
						}
						else
						{
							text = databaseLocationInfo.ServerFqdn;
							base.WriteVerbose(Strings.VerboseConnectionAdminRpcInterface(text));
							this.mapiSession = new MapiAdministrationSession(databaseLocationInfo.ServerLegacyDN, Fqdn.Parse(text));
						}
					}
					catch (MapiPermanentException ex)
					{
						if (this.Permanent)
						{
							base.WriteError(ex, ExchangeErrorCategory.ServerOperation, this.Identity);
						}
						else
						{
							TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
							{
								ex
							});
						}
					}
					catch (MapiRetryableException ex2)
					{
						if (this.Permanent)
						{
							base.WriteError(ex2, ExchangeErrorCategory.ServerTransient, this.Identity);
						}
						else
						{
							TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
							{
								ex2
							});
						}
					}
					try
					{
						if (dataObject != null && base.ForReconciliation)
						{
							base.DataObject.ExternalDirectoryObjectId = string.Empty;
						}
						base.DataObject.PreviousDatabase = base.DataObject.Database;
						base.DataSession.Save(base.DataObject);
					}
					catch (DataSourceTransientException exception2)
					{
						base.WriteError(exception2, ExchangeErrorCategory.ServerTransient, null);
					}
					catch (InvalidObjectOperationException)
					{
					}
					catch (DataValidationException)
					{
					}
					catch (ADOperationException)
					{
					}
				}
				base.InternalProcessRecord();
				this.LogRemoveMailboxDetails(dataObject);
			}
			if (this.StoreMailboxIdentity != null || this.Permanent)
			{
				if (this.Permanent)
				{
					if (!(guid != Guid.Empty) || adobjectId == null)
					{
						goto IL_5A0;
					}
					try
					{
						base.WriteVerbose(Strings.VerboseDeleteMailboxInStore(guid.ToString(), adobjectId.ToString()));
						this.mapiSession.DeleteMailbox(new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(adobjectId), guid));
						goto IL_5A0;
					}
					catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException ex3)
					{
						TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
						{
							ex3
						});
						base.WriteVerbose(ex3.LocalizedString);
						goto IL_5A0;
					}
					catch (DataSourceOperationException exception3)
					{
						base.WriteError(exception3, ExchangeErrorCategory.ServerOperation, base.DataObject);
						goto IL_5A0;
					}
				}
				try
				{
					base.WriteVerbose(Strings.VerboseDeleteMailboxInStore(this.mailboxStatistics.MailboxGuid.ToString(), this.mailboxStatistics.Database.ToString()));
					((IConfigDataProvider)this.mapiSession).Delete(this.mailboxStatistics);
					goto IL_5A0;
				}
				catch (DataSourceOperationException exception4)
				{
					base.WriteError(exception4, ExchangeErrorCategory.ServerOperation, (this.Identity == null) ? this.StoreMailboxIdentity : this.Identity);
					goto IL_5A0;
				}
			}
			if (this.mapiSession != null)
			{
				try
				{
					TIdentity identity = this.Identity;
					base.WriteVerbose(Strings.VerboseSyncMailboxWithDS(identity.ToString(), base.DataObject.Database.ToString(), text));
					bool flag3 = true;
					if (base.DataObject.Database == null || Guid.Empty == base.DataObject.Database.ObjectGuid)
					{
						flag3 = false;
						TaskLogger.Trace("Cannot get the database for mailbox '{0}'", new object[]
						{
							base.DataObject.Identity
						});
					}
					if (Guid.Empty == base.DataObject.ExchangeGuid)
					{
						flag3 = false;
						TaskLogger.Trace("Cannot get the mailbox guid for mailbox '{0}'", new object[]
						{
							base.DataObject.Identity
						});
					}
					if (flag3)
					{
						this.mapiSession.SyncMailboxWithDS(new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(base.DataObject.Database), base.DataObject.ExchangeGuid));
					}
				}
				catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException ex4)
				{
					TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
					{
						ex4
					});
					base.WriteVerbose(ex4.LocalizedString);
				}
				catch (DataSourceTransientException ex5)
				{
					TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
					{
						ex5
					});
					this.WriteWarning(ex5.LocalizedString);
				}
				catch (DataSourceOperationException ex6)
				{
					TaskLogger.Trace("Swallowing exception {0} from mapi.net", new object[]
					{
						ex6
					});
					this.WriteWarning(ex6.LocalizedString);
				}
			}
			IL_5A0:
			if (!flag && this.mailboxStatistics != null)
			{
				this.mailboxStatistics.Dispose();
				this.mailboxStatistics = null;
			}
			this.DisposeMapiSession();
			TaskLogger.LogExit();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001783C File Offset: 0x00015A3C
		private void LogRemoveMailboxDetails(ADUser recipient)
		{
			Guid uniqueId = base.CurrentTaskContext.UniqueId;
			CmdletLogger.SafeAppendGenericInfo(uniqueId, "ExchangeGuid", recipient.ExchangeGuid.ToString());
			CmdletLogger.SafeAppendGenericInfo(uniqueId, "HomeMBD", (recipient.Database == null) ? string.Empty : recipient.Database.ToString());
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017899 File Offset: 0x00015A99
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeMapiSession();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000178AB File Offset: 0x00015AAB
		private void DisposeMapiSession()
		{
			if (this.mapiSession != null)
			{
				this.mapiSession.Dispose();
				this.mapiSession = null;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000178C7 File Offset: 0x00015AC7
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Mailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000178D4 File Offset: 0x00015AD4
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is StorageTransientException || exception is StoragePermanentException;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000178F4 File Offset: 0x00015AF4
		private bool IsToInactiveMailbox()
		{
			ADUser dataObject = base.DataObject;
			return this.ShouldSoftDeleteObject() && SoftDeletedTaskHelper.MSOSyncEnabled(this.ConfigurationSession, dataObject.OrganizationId) && dataObject.IsInLitigationHoldOrInplaceHold && !this.IgnoreLegalHold;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001793C File Offset: 0x00015B3C
		private bool IsDisconnectInactiveMailbox()
		{
			ADUser dataObject = base.DataObject;
			return dataObject.IsInactiveMailbox && this.Disconnect && !this.IgnoreLegalHold && (dataObject.LitigationHoldEnabled || (dataObject.InPlaceHolds != null && dataObject.InPlaceHolds.Count > 0));
		}

		// Token: 0x0400014C RID: 332
		private const string ParameterPermanent = "Permanent";

		// Token: 0x0400014D RID: 333
		private const string ParameterDatabase = "Database";

		// Token: 0x0400014E RID: 334
		private const string ParameterStoreMailboxIdentity = "StoreMailboxIdentity";

		// Token: 0x0400014F RID: 335
		private const string ParameterKeepWindowsLiveID = "KeepWindowsLiveID";

		// Token: 0x04000150 RID: 336
		private const string ParameterDisconnect = "Disconnect";

		// Token: 0x04000151 RID: 337
		internal const string ParameterSetStoreMailboxIdentity = "StoreMailboxIdentity";

		// Token: 0x04000152 RID: 338
		private MapiAdministrationSession mapiSession;

		// Token: 0x04000153 RID: 339
		private Database database;

		// Token: 0x04000154 RID: 340
		private MailboxStatistics mailboxStatistics;

		// Token: 0x04000155 RID: 341
		private bool isToInactiveMailbox;

		// Token: 0x04000156 RID: 342
		private bool isDisconnectInactiveMailbox;

		// Token: 0x04000157 RID: 343
		private OrganizationId currentOrganizationId;

		// Token: 0x04000158 RID: 344
		private IConfigurationSession tenantLocalConfigurationSession;

		// Token: 0x04000159 RID: 345
		protected bool skipJournalArchivingCheck;
	}
}
