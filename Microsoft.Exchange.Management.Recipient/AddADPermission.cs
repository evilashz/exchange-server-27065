using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000098 RID: 152
	[Cmdlet("Add", "ADPermission", DefaultParameterSetName = "AccessRights", SupportsShouldProcess = true)]
	public sealed class AddADPermission : SetADPermissionTaskBase
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002B8AC File Offset: 0x00029AAC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Owner" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageAddADPermissionOwner(this.Identity.ToString(), this.Owner.ToString());
				}
				return Strings.ConfirmationMessageAddADPermissionAccessRights(this.Identity.ToString(), base.Instance.User.ToString(), (base.Instance.AccessRights != null) ? base.FormatMultiValuedProperty(base.Instance.AccessRights) : base.FormatMultiValuedProperty(base.Instance.ExtendedRights));
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002B938 File Offset: 0x00029B38
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0002B94F File Offset: 0x00029B4F
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

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002B964 File Offset: 0x00029B64
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
				this.owner = SecurityPrincipalIdParameter.GetUserSid(base.GlobalCatalogRecipientSession, this.Owner, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002B9C4 File Offset: 0x00029BC4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.IsInherited)
			{
				return;
			}
			if ("Owner" == base.ParameterSetName)
			{
				IConfigurationSession writableSession = base.GetWritableSession(this.DataObject.Id);
				ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadAdSecurityDescriptor(this.DataObject, writableSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
				SecurityIdentifier sid = this.owner;
				activeDirectorySecurity.SetOwner(sid);
				RawSecurityDescriptor sd = new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
				writableSession.SaveSecurityDescriptor(this.DataObject.Id, sd, true);
				string friendlyUserName = SecurityPrincipalIdParameter.GetFriendlyUserName(sid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				base.WriteObject(new OwnerPresentationObject(this.DataObject.Id, friendlyUserName));
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002BA86 File Offset: 0x00029C86
		protected override void ApplyModification(ADRawEntry modifiedObject, ActiveDirectoryAccessRule[] modifiedAces)
		{
			DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.ErrorLoggerDelegate(this.WriteErrorPerObject), base.GetWritableSession(modifiedObject.Id), modifiedObject.Id, modifiedAces);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002BAC8 File Offset: 0x00029CC8
		protected override void WriteAces(ADObjectId id, IEnumerable<ActiveDirectoryAccessRule> aces)
		{
			foreach (ActiveDirectoryAccessRule ace in aces)
			{
				ADAcePresentationObject adacePresentationObject = new ADAcePresentationObject(ace, id);
				adacePresentationObject.ResetChangeTracking(true);
				base.WriteObject(adacePresentationObject);
			}
		}

		// Token: 0x0400021A RID: 538
		private SecurityIdentifier owner;
	}
}
