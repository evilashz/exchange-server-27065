using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000976 RID: 2422
	[Cmdlet("mount", "Database", SupportsShouldProcess = true)]
	public sealed class MountDatabase : DatabaseActionTaskBase<Database>
	{
		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06005680 RID: 22144 RVA: 0x001638A7 File Offset: 0x00161AA7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMountDatabase(this.Identity.ToString());
			}
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06005681 RID: 22145 RVA: 0x001638B9 File Offset: 0x00161AB9
		// (set) Token: 0x06005682 RID: 22146 RVA: 0x001638DF File Offset: 0x00161ADF
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06005683 RID: 22147 RVA: 0x001638F7 File Offset: 0x00161AF7
		// (set) Token: 0x06005684 RID: 22148 RVA: 0x0016391D File Offset: 0x00161B1D
		[Parameter]
		public SwitchParameter AcceptDataLoss
		{
			get
			{
				return (SwitchParameter)(base.Fields["AcceptDataLoss"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AcceptDataLoss"] = value;
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06005685 RID: 22149 RVA: 0x00163938 File Offset: 0x00161B38
		private IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 208, "RecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\database\\ActionsOnDatabase.cs");
					tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
					this.recipientSession = tenantOrRootOrgRecipientSession;
				}
				return this.recipientSession;
			}
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x0016398D File Offset: 0x00161B8D
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			MapiTaskHelper.VerifyDatabaseAndItsOwningServerInScope(base.SessionSettings, this.DataObject, new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			TaskLogger.LogExit();
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x001639BC File Offset: 0x00161BBC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				try
				{
					MailboxDatabase mailboxDatabase = this.ConfigurationSession.Read<MailboxDatabase>((ADObjectId)this.DataObject.Identity);
					Server server = null;
					ADComputer adcomputer = null;
					bool useConfigNC = this.ConfigurationSession.UseConfigNC;
					bool useGlobalCatalog = this.ConfigurationSession.UseGlobalCatalog;
					if (mailboxDatabase != null)
					{
						server = mailboxDatabase.GetServer();
						try
						{
							this.ConfigurationSession.UseConfigNC = false;
							this.ConfigurationSession.UseGlobalCatalog = true;
							adcomputer = ((ITopologyConfigurationSession)this.ConfigurationSession).FindComputerByHostName(server.Name);
						}
						finally
						{
							this.ConfigurationSession.UseConfigNC = useConfigNC;
							this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
						}
						if (adcomputer == null)
						{
							base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorDBOwningServerNotFound(mailboxDatabase.Identity.ToString())), ErrorCategory.ObjectNotFound, server.Identity);
						}
						ADObjectId adobjectId = adcomputer.Id.DomainId;
						adobjectId = adobjectId.GetChildId("Microsoft Exchange System Objects");
						adobjectId = adobjectId.GetChildId("SystemMailbox" + mailboxDatabase.Guid.ToString("B"));
						string identity = adobjectId.ToDNString();
						GeneralMailboxIdParameter generalMailboxIdParameter = GeneralMailboxIdParameter.Parse(identity);
						base.WriteVerbose(TaskVerboseStringHelper.GetFindByIdParameterVerboseString(generalMailboxIdParameter, this.RecipientSession, typeof(ADRecipient), null));
						IEnumerable<ADSystemMailbox> objects = generalMailboxIdParameter.GetObjects<ADSystemMailbox>(adobjectId, this.RecipientSession);
						using (IEnumerator<ADSystemMailbox> enumerator = objects.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								ADSystemMailbox adsystemMailbox = enumerator.Current;
							}
							else
							{
								NewMailboxDatabase.SaveSystemMailbox(mailboxDatabase, mailboxDatabase.GetServer(), base.RootOrgContainerId, (ITopologyConfigurationSession)this.ConfigurationSession, this.RecipientSession, null, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
							}
						}
					}
					base.WriteVerbose(Strings.VerboseMountDatabase(this.Identity.ToString()));
					this.RequestMount(MountFlags.None);
					if (!this.DataObject.DatabaseCreated)
					{
						this.DataObject.DatabaseCreated = true;
						base.InternalProcessRecord();
					}
				}
				catch (AmServerException ex)
				{
					Exception ex2;
					if (ex.TryGetInnerExceptionOfType(out ex2))
					{
						TaskLogger.Trace("Database already mounted (database={0}, exception={1})", new object[]
						{
							this.DataObject.Name,
							ex2.Message
						});
					}
					else if (ex.TryGetInnerExceptionOfType(out ex2) || ex.TryGetInnerExceptionOfType(out ex2))
					{
						this.AttemptForcedMountIfNecessary(this.Force, Strings.ContinueMountWhenDBFilesNotExist, Strings.VerboseMountDatabaseForcely(this.Identity.ToString()), Strings.ErrorFailedToMountReplicatedDbWithMissingEdbFile(this.Identity.ToString()), ex, MountFlags.ForceDatabaseCreation);
					}
					else if (ex.TryGetInnerExceptionOfType(out ex2))
					{
						this.PromptForMountIfNecessary(this.AcceptDataLoss, Strings.ContinueMountWithDataLoss, Strings.VerboseMountDatabaseDataLoss(this.Identity.ToString()), MountFlags.AcceptDataLoss);
					}
					else
					{
						TaskLogger.Trace("MountDatabase.InternalProcessRecord raises exception while mounting database: {0}", new object[]
						{
							ex.Message
						});
						base.WriteError(new InvalidOperationException(Strings.ErrorFailedToMountDatabase(this.Identity.ToString(), ex.Message), ex), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
				}
			}
			catch (AmServerException ex3)
			{
				TaskLogger.Trace("MountDatabase.InternalProcessRecord raises exception while mounting database: {0}", new object[]
				{
					ex3.Message
				});
				base.WriteError(new InvalidOperationException(Strings.ErrorFailedToMountDatabase(this.Identity.ToString(), ex3.Message), ex3), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			catch (AmServerTransientException ex4)
			{
				TaskLogger.Trace("MountDatabase.InternalProcessRecord raises exception while mounting database: {0}", new object[]
				{
					ex4.Message
				});
				base.WriteError(new InvalidOperationException(Strings.ErrorFailedToMountDatabase(this.Identity.ToString(), ex4.Message), ex4), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06005688 RID: 22152 RVA: 0x00163E34 File Offset: 0x00162034
		private void AttemptForcedMountIfNecessary(bool suppressPrompt, LocalizedString promptMessage, LocalizedString verboseMessage, LocalizedString replicatedDbErrorMessage, Exception mountException, MountFlags mountFlags)
		{
			if (this.DataObject.ReplicationType == ReplicationType.Remote)
			{
				TaskLogger.Trace("MountDatabase.InternalProcessRecord raised exception while mounting database: {0}", new object[]
				{
					mountException.Message
				});
				base.WriteError(new InvalidOperationException(replicatedDbErrorMessage, mountException), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				return;
			}
			this.PromptForMountIfNecessary(suppressPrompt, promptMessage, verboseMessage, mountFlags);
		}

		// Token: 0x06005689 RID: 22153 RVA: 0x00163E97 File Offset: 0x00162097
		private void PromptForMountIfNecessary(bool suppressPrompt, LocalizedString promptMessage, LocalizedString verboseMessage, MountFlags mountFlags)
		{
			if (suppressPrompt || base.ShouldContinue(promptMessage))
			{
				base.WriteVerbose(verboseMessage);
				this.RequestMount(mountFlags);
			}
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x00163EB4 File Offset: 0x001620B4
		private void RequestMount(MountFlags storeMountFlags)
		{
			AmMountFlags amMountFlags = AmMountFlags.None;
			if (this.Force)
			{
				amMountFlags |= AmMountFlags.MountWithForce;
			}
			AmRpcClientHelper.MountDatabase(ADObjectWrapperFactory.CreateWrapper(this.DataObject), (int)storeMountFlags, (int)amMountFlags, 0);
		}

		// Token: 0x04003208 RID: 12808
		internal const string paramForce = "Force";

		// Token: 0x04003209 RID: 12809
		internal const string paramAcceptDataLoss = "AcceptDataLoss";

		// Token: 0x0400320A RID: 12810
		private const int OnlineShortTimeout = 50;

		// Token: 0x0400320B RID: 12811
		private const int OnlineLongTimeout = 200;

		// Token: 0x0400320C RID: 12812
		private IRecipientSession recipientSession;
	}
}
