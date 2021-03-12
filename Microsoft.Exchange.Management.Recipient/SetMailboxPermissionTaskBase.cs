using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200009B RID: 155
	public abstract class SetMailboxPermissionTaskBase : SetRecipientObjectTask<MailboxIdParameter, MailboxAcePresentationObject, ADUser>
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002C224 File Offset: 0x0002A424
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0002C22C File Offset: 0x0002A42C
		protected int CurrentProcessedObject
		{
			get
			{
				return this.currentProcessedObject;
			}
			set
			{
				this.currentProcessedObject = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002C235 File Offset: 0x0002A435
		protected List<List<ActiveDirectoryAccessRule>> ModifiedAcl
		{
			get
			{
				return this.modifiedAcl;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0002C23D File Offset: 0x0002A43D
		protected List<ADUser> ModifiedObjects
		{
			get
			{
				return this.modifiedObjects;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0002C245 File Offset: 0x0002A445
		internal List<IConfigDataProvider> ModifyingRecipientSessions
		{
			get
			{
				return this.modifyingRecipientSessions;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0002C24D File Offset: 0x0002A44D
		internal IADSecurityPrincipal SecurityPrincipal
		{
			get
			{
				return this.securityPrincipal;
			}
		}

		// Token: 0x06000A50 RID: 2640
		internal abstract void ApplyDelegation(bool fullAccess);

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002C255 File Offset: 0x0002A455
		protected override void StampChangesOn(IConfigurable dataObject)
		{
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002C258 File Offset: 0x0002A458
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ParameterSetName = "AccessRights")]
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ParameterSetName = "Owner")]
		[Parameter(Mandatory = false, Position = 0, ParameterSetName = "Instance")]
		public override MailboxIdParameter Identity
		{
			get
			{
				if (base.Fields[SetMailboxPermissionTaskBase.paramIdentity] != null)
				{
					return (MailboxIdParameter)base.Fields[SetMailboxPermissionTaskBase.paramIdentity];
				}
				if (this.Instance != null)
				{
					MailboxIdParameter mailboxIdParameter = new MailboxIdParameter();
					mailboxIdParameter.Initialize((ADObjectId)this.Instance.Identity);
					return mailboxIdParameter;
				}
				return null;
			}
			set
			{
				base.Fields[SetMailboxPermissionTaskBase.paramIdentity] = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0002C2C7 File Offset: 0x0002A4C7
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0002C2CF File Offset: 0x0002A4CF
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Instance")]
		public new MailboxAcePresentationObject Instance
		{
			get
			{
				return base.Instance;
			}
			set
			{
				base.Instance = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0002C2D8 File Offset: 0x0002A4D8
		protected bool IsInherited
		{
			get
			{
				return this.Instance != null && this.Instance.IsInherited;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002C2EF File Offset: 0x0002A4EF
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.modifiedAcl.Clear();
			this.modifiedObjects.Clear();
			this.modifyingRecipientSessions.Clear();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002C324 File Offset: 0x0002A524
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.CurrentTaskContext.CanBypassRBACScope)
			{
				base.VerifyIsWithinScopes((IRecipientSession)base.DataSession, this.DataObject, true, new DataAccessTask<ADUser>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			}
			if (this.IsInherited)
			{
				this.WriteWarning(Strings.ErrorWillNotPerformOnInheritedAccessRight(this.Instance.Identity.ToString()));
				return;
			}
			if (base.ParameterSetName == "Owner")
			{
				return;
			}
			if (this.Instance.User != null)
			{
				this.securityPrincipal = SecurityPrincipalIdParameter.GetSecurityPrincipal(base.TenantGlobalCatalogSession, this.Instance.User, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (this.IsInherited)
			{
				return;
			}
			if (base.ParameterSetName == "Instance")
			{
				if (this.Instance.User == null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorUserNull, "User"), ErrorCategory.InvalidArgument, null);
				}
				if (this.Instance.AccessRights == null || this.Instance.AccessRights.Length == 0)
				{
					base.WriteError(new ArgumentException(Strings.ErrorAccessRightsEmpty, "AccessRights"), ErrorCategory.InvalidArgument, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002C468 File Offset: 0x0002A668
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			BackEndServer backEndServer = BackEndLocator.GetBackEndServer((ADUser)configurable);
			if (backEndServer != null && backEndServer.Version < Server.E15MinVersion)
			{
				CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, (ADUser)configurable, false, this.ConfirmationMessage, null);
			}
			return configurable;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002C4B2 File Offset: 0x0002A6B2
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.IsInherited)
			{
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.InheritedAceIgnored);
				}
				TaskLogger.LogExit();
				return;
			}
			this.PutEachEntryInList();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002C4E8 File Offset: 0x0002A6E8
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			if ("Owner" != base.ParameterSetName)
			{
				this.CurrentProcessedObject = 0;
				while (this.CurrentProcessedObject < this.ModifiedObjects.Count)
				{
					try
					{
						this.ApplyModification(this.ModifiedObjects[this.CurrentProcessedObject], this.ModifiedAcl[this.CurrentProcessedObject].ToArray(), this.ModifyingRecipientSessions[this.CurrentProcessedObject]);
					}
					catch (OverflowException exception)
					{
						this.WriteError(exception, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_15F;
					}
					catch (DataValidationException exception2)
					{
						this.WriteError(exception2, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_15F;
					}
					catch (DataSourceOperationException exception3)
					{
						this.WriteError(exception3, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_15F;
					}
					catch (DataSourceTransientException exception4)
					{
						this.WriteError(exception4, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_15F;
					}
					goto IL_D2;
					IL_15F:
					this.CurrentProcessedObject++;
					continue;
					IL_D2:
					if (this.CurrentProcessedObject + 1 < this.ModifiedObjects.Count)
					{
						this.WriteCurrentProgress(Strings.ProcessingAceActivity, Strings.ProcessingAceStatus(((ADRawEntry)this.ModifiedObjects[this.CurrentProcessedObject]).Id.ToString()), ProgressRecordType.Processing, this.CurrentProcessedObject * 100 / this.ModifiedObjects.Count);
					}
					this.WriteAces(((ADRawEntry)this.ModifiedObjects[this.CurrentProcessedObject]).Id, this.ModifiedAcl[this.CurrentProcessedObject]);
					goto IL_15F;
				}
				if (this.DataObject != null && this.SecurityPrincipal != null)
				{
					bool fullAccess = this.ToGrantFullAccess();
					this.ApplyDelegation(fullAccess);
				}
				this.WriteCurrentProgress(Strings.ProcessingAceActivity, Strings.CompletedAceActivity, ProgressRecordType.Completed, 100);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A5C RID: 2652
		internal abstract void ApplyModification(ADUser modifiedObject, ActiveDirectoryAccessRule[] modifiedAces, IConfigDataProvider modifyingSession);

		// Token: 0x06000A5D RID: 2653
		protected abstract void WriteAces(ADObjectId id, IEnumerable<ActiveDirectoryAccessRule> aces);

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		protected void WriteCurrentProgress(LocalizedString activityDesc, LocalizedString status, ProgressRecordType recordType, int percent)
		{
			base.WriteProgress(new ExProgressRecord(0, activityDesc, status)
			{
				RecordType = recordType,
				PercentComplete = percent
			});
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002C710 File Offset: 0x0002A910
		protected void ApplyDelegationInternal(bool removeDelegation)
		{
			ADRecipient adrecipient = this.SecurityPrincipal.Sid.IsWellKnown(WellKnownSidType.SelfSid) ? this.DataObject : ((ADRecipient)this.SecurityPrincipal);
			if (adrecipient.RecipientType == RecipientType.UserMailbox)
			{
				ADUser dataObject = this.DataObject;
				ADUser aduser = this.GetADUser(base.DataSession, dataObject);
				if (aduser != null)
				{
					try
					{
						PermissionTaskHelper.SetDelegation(aduser, adrecipient, base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), removeDelegation);
						if (!removeDelegation && ((ADUser)adrecipient).DelegateListBL.Count >= 16)
						{
							this.WriteWarning(Strings.WarningDelegatesExceededOutlookLimit);
						}
						return;
					}
					catch (DataValidationException exception)
					{
						base.WriteError(exception, ErrorCategory.WriteError, null);
						return;
					}
					catch (DataSourceOperationException exception2)
					{
						base.WriteError(exception2, ErrorCategory.WriteError, null);
						return;
					}
					catch (DataSourceTransientException exception3)
					{
						base.WriteError(exception3, ErrorCategory.WriteError, null);
						return;
					}
				}
				base.WriteVerbose(Strings.VerboseMailboxDelegateSkipNotADUser(this.DataObject.ToString()));
				return;
			}
			base.WriteVerbose(Strings.VerboseMailboxDelegateSkip(this.Instance.User.ToString()));
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002C82C File Offset: 0x0002AA2C
		internal virtual void WriteErrorPerObject(LocalizedException exception, ExchangeErrorCategory category, object target)
		{
			if (target == null)
			{
				this.WriteError(exception, category, this.CurrentProcessedObject, false);
				return;
			}
			base.WriteError(exception, category, target);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002C850 File Offset: 0x0002AA50
		protected void PutEachEntryInList()
		{
			List<ActiveDirectoryAccessRule> list = null;
			for (int i = 0; i < this.ModifiedObjects.Count; i++)
			{
				if (this.IsEqualEntry(i))
				{
					list = this.ModifiedAcl[i];
					break;
				}
			}
			if (list == null)
			{
				list = new List<ActiveDirectoryAccessRule>();
				this.ModifiedObjects.Add(this.DataObject);
				this.ModifiedAcl.Add(list);
				this.ModifyingRecipientSessions.Add(base.DataSession);
			}
			AccessControlType allowOrDeny = this.Instance.Deny ? AccessControlType.Deny : AccessControlType.Allow;
			MailboxRights mailboxRights = (MailboxRights)0;
			if (this.Instance.AccessRights != null)
			{
				foreach (MailboxRights mailboxRights2 in this.Instance.AccessRights)
				{
					mailboxRights |= mailboxRights2;
				}
			}
			if (mailboxRights != (MailboxRights)0)
			{
				this.UpdateAcl(list, allowOrDeny, mailboxRights);
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002C922 File Offset: 0x0002AB22
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is SecurityDescriptorAccessDeniedException || exception is BackEndLocatorException || exception is StoragePermanentException;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002C948 File Offset: 0x0002AB48
		private bool IsEqualEntry(int Index)
		{
			return this.ModifiedObjects[Index].Id.Equals(this.DataObject.Id);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002C96C File Offset: 0x0002AB6C
		protected virtual void UpdateAcl(List<ActiveDirectoryAccessRule> modifiedAcl, AccessControlType allowOrDeny, MailboxRights mailboxRights)
		{
			TaskLogger.LogEnter();
			SecurityIdentifier identity = (this.SecurityPrincipal.MasterAccountSid != null && !this.SecurityPrincipal.MasterAccountSid.IsWellKnown(WellKnownSidType.SelfSid)) ? this.SecurityPrincipal.MasterAccountSid : this.SecurityPrincipal.Sid;
			modifiedAcl.Add(new ActiveDirectoryAccessRule(identity, (ActiveDirectoryRights)mailboxRights, allowOrDeny, Guid.Empty, this.Instance.InheritanceType, Guid.Empty));
			TaskLogger.LogExit();
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		protected bool ToGrantFullAccess()
		{
			bool flag = false;
			if (this.Instance.AccessRights != null)
			{
				foreach (MailboxRights mailboxRights in this.Instance.AccessRights)
				{
					flag = ((mailboxRights & MailboxRights.FullAccess) == MailboxRights.FullAccess);
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002CA2D File Offset: 0x0002AC2D
		private ADUser GetADUser(IConfigDataProvider session, ADUser recipient)
		{
			if (recipient != null)
			{
				return session.Read<ADUser>(recipient.Id) as ADUser;
			}
			return null;
		}

		// Token: 0x0400021F RID: 543
		private IADSecurityPrincipal securityPrincipal;

		// Token: 0x04000220 RID: 544
		private static string paramIdentity = "Identity";

		// Token: 0x04000221 RID: 545
		private List<List<ActiveDirectoryAccessRule>> modifiedAcl = new List<List<ActiveDirectoryAccessRule>>();

		// Token: 0x04000222 RID: 546
		private List<ADUser> modifiedObjects = new List<ADUser>();

		// Token: 0x04000223 RID: 547
		private List<IConfigDataProvider> modifyingRecipientSessions = new List<IConfigDataProvider>();

		// Token: 0x04000224 RID: 548
		private int currentProcessedObject;
	}
}
