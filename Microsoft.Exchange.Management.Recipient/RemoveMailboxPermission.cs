using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Core;
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
	// Token: 0x020000A8 RID: 168
	[Cmdlet("Remove", "MailboxPermission", DefaultParameterSetName = "AccessRights", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveMailboxPermission : SetMailboxPermissionTaskBase
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002D888 File Offset: 0x0002BA88
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxPermissionAccessRights(this.Identity.ToString(), base.FormatMultiValuedProperty(base.Instance.AccessRights), base.Instance.User.ToString());
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002D8BC File Offset: 0x0002BABC
		protected override void InternalBeginProcessing()
		{
			if (Constants.IsPowerShellWebService && base.ExchangeRunspaceConfig != null && base.ExchangeRunspaceConfig.ConfigurationSettings.EncodeDecodeKey && base.DynamicParametersInstance.IsModified(AcePresentationObjectSchema.User))
			{
				SecurityPrincipalIdParameter securityPrincipalIdParameter = base.DynamicParametersInstance[AcePresentationObjectSchema.User] as SecurityPrincipalIdParameter;
				IIdentityParameter identityParameter;
				if (securityPrincipalIdParameter != null && PswsPropertyConverterModule.TryDecodeIIdentityParameter(securityPrincipalIdParameter, out identityParameter))
				{
					base.DynamicParametersInstance[AcePresentationObjectSchema.User] = (identityParameter as SecurityPrincipalIdParameter);
				}
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002D93C File Offset: 0x0002BB3C
		internal override void ApplyModification(ADUser modifiedObject, ActiveDirectoryAccessRule[] modifiedAces, IConfigDataProvider modifyingSession)
		{
			PermissionTaskHelper.SetMailboxAces(modifiedObject, modifyingSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(this.WriteErrorPerObject), PermissionTaskHelper.GetReadOnlySession(base.DomainController), ref this.storeSession, true, modifiedAces);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002D989 File Offset: 0x0002BB89
		internal override void ApplyDelegation(bool fullAccess)
		{
			if (fullAccess)
			{
				base.ApplyDelegationInternal(true);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002D998 File Offset: 0x0002BB98
		protected override void UpdateAcl(List<ActiveDirectoryAccessRule> modifiedAcl, AccessControlType allowOrDeny, MailboxRights mailboxRights)
		{
			TaskLogger.LogEnter();
			base.UpdateAcl(modifiedAcl, allowOrDeny, mailboxRights);
			foreach (SecurityIdentifier identity in base.SecurityPrincipal.SidHistory)
			{
				modifiedAcl.Add(new ActiveDirectoryAccessRule(identity, (ActiveDirectoryRights)mailboxRights, allowOrDeny, Guid.Empty, base.Instance.InheritanceType, Guid.Empty));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002DA20 File Offset: 0x0002BC20
		protected override void WriteAces(ADObjectId id, IEnumerable<ActiveDirectoryAccessRule> aces)
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002DA22 File Offset: 0x0002BC22
		protected override void Dispose(bool disposing)
		{
			if (this.storeSession != null)
			{
				this.storeSession.Dispose();
				this.storeSession = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000247 RID: 583
		private MapiMessageStoreSession storeSession;
	}
}
