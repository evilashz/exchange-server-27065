using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200009C RID: 156
	[Cmdlet("Add", "MailboxPermission", DefaultParameterSetName = "AccessRights", SupportsShouldProcess = true)]
	public sealed class AddMailboxPermission : SetMailboxPermissionTaskBase
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0002CA7C File Offset: 0x0002AC7C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Owner" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageAddMailboxPermissionOwner(this.Identity.ToString(), this.Owner.ToString());
				}
				return Strings.ConfirmationMessageAddMailboxPermissionAccessRights(this.Identity.ToString(), base.Instance.User.ToString(), base.FormatMultiValuedProperty(base.Instance.AccessRights));
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0002CAFF File Offset: 0x0002ACFF
		[Parameter(Mandatory = true, ParameterSetName = "Owner")]
		public SecurityPrincipalIdParameter Owner
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["Owner"];
			}
			set
			{
				base.Fields["Owner"] = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002CB12 File Offset: 0x0002AD12
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0002CB29 File Offset: 0x0002AD29
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public bool? AutoMapping
		{
			get
			{
				return (bool?)base.Fields["AutoMapping"];
			}
			set
			{
				base.Fields["AutoMapping"] = value;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002CB4C File Offset: 0x0002AD4C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.IsInherited)
			{
				return;
			}
			if (this.Owner != null)
			{
				this.owner = SecurityPrincipalIdParameter.GetUserSid(base.TenantGlobalCatalogSession, this.Owner, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (base.ParameterSetName == "Instance" || base.ParameterSetName == "AccessRights")
			{
				if (!base.ToGrantFullAccess() && this.AutoMapping != null && this.AutoMapping.Value)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSpecifyAutoMappingOnNonFullAccess), ErrorCategory.InvalidOperation, null);
				}
				if (base.Instance.AccessRights != null && base.Instance.AccessRights.Length != 0)
				{
					if (Array.Exists<MailboxRights>(base.Instance.AccessRights, (MailboxRights right) => (right & MailboxRights.SendAs) == MailboxRights.SendAs))
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorSetSendAsOnMailboxPermissionNotAllowed), ErrorCategory.InvalidOperation, null);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002CC74 File Offset: 0x0002AE74
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.IsInherited)
			{
				return;
			}
			if ("Owner" == base.ParameterSetName)
			{
				ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadMailboxSecurityDescriptor(this.DataObject, PermissionTaskHelper.GetReadOnlySession(base.DomainController), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				SecurityIdentifier sid = this.owner;
				activeDirectorySecurity.SetOwner(sid);
				new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
				PermissionTaskHelper.SaveMailboxSecurityDescriptor(this.DataObject, activeDirectorySecurity, base.DataSession, ref this.storeSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				string friendlyUserName = SecurityPrincipalIdParameter.GetFriendlyUserName(sid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				base.WriteObject(new OwnerPresentationObject(this.DataObject.Id, friendlyUserName));
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002CD58 File Offset: 0x0002AF58
		internal override void ApplyModification(ADUser modifiedObject, ActiveDirectoryAccessRule[] modifiedAces, IConfigDataProvider modifyingSession)
		{
			PermissionTaskHelper.SetMailboxAces(modifiedObject, modifyingSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(base.WriteError), PermissionTaskHelper.GetReadOnlySession(base.DomainController), ref this.storeSession, false, modifiedAces);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002CDA4 File Offset: 0x0002AFA4
		internal override void ApplyDelegation(bool fullAccess)
		{
			if (fullAccess)
			{
				if (this.AutoMapping != null && !this.AutoMapping.Value)
				{
					base.ApplyDelegationInternal(true);
					return;
				}
				base.ApplyDelegationInternal(false);
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002CDE4 File Offset: 0x0002AFE4
		protected override void WriteAces(ADObjectId id, IEnumerable<ActiveDirectoryAccessRule> aces)
		{
			foreach (ActiveDirectoryAccessRule ace in aces)
			{
				MailboxAcePresentationObject mailboxAcePresentationObject = new MailboxAcePresentationObject(ace, id);
				mailboxAcePresentationObject.ResetChangeTracking(true);
				base.WriteObject(mailboxAcePresentationObject);
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002CE3C File Offset: 0x0002B03C
		protected override void Dispose(bool disposing)
		{
			if (this.storeSession != null)
			{
				this.storeSession.Dispose();
				this.storeSession = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000225 RID: 549
		private MapiMessageStoreSession storeSession;

		// Token: 0x04000226 RID: 550
		private SecurityIdentifier owner;
	}
}
