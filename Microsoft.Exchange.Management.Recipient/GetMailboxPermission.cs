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
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A2 RID: 162
	[Cmdlet("Get", "MailboxPermission", DefaultParameterSetName = "AccessRights")]
	public sealed class GetMailboxPermission : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002D299 File Offset: 0x0002B499
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002D2B0 File Offset: 0x0002B4B0
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002D2C3 File Offset: 0x0002B4C3
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0002D2DA File Offset: 0x0002B4DA
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public SecurityPrincipalIdParameter User
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["User"];
			}
			set
			{
				base.Fields["User"] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0002D2ED File Offset: 0x0002B4ED
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0002D313 File Offset: 0x0002B513
		[Parameter(Mandatory = false, ParameterSetName = "Owner")]
		public SwitchParameter Owner
		{
			get
			{
				return (SwitchParameter)(base.Fields["Owner"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Owner"] = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0002D32B File Offset: 0x0002B52B
		private IADSecurityPrincipal SecurityPrincipal
		{
			get
			{
				return this.securityPrincipal;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002D333 File Offset: 0x0002B533
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0002D33B File Offset: 0x0002B53B
		private bool HasObjectMatchingIdentity
		{
			get
			{
				return this.hasObjectMatchingIdentity;
			}
			set
			{
				this.hasObjectMatchingIdentity = value;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002D344 File Offset: 0x0002B544
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.User != null)
			{
				this.securityPrincipal = SecurityPrincipalIdParameter.GetSecurityPrincipal(base.TenantGlobalCatalogSession, this.User, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002D398 File Offset: 0x0002B598
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			this.HasObjectMatchingIdentity = false;
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002D3B1 File Offset: 0x0002B5B1
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is DatabaseNotFoundException;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002D3C8 File Offset: 0x0002B5C8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			if (this.Identity != null)
			{
				LocalizedString? localizedString;
				IEnumerable<ADUser> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
				this.WriteResult<ADUser>(dataObjects);
				if (!base.HasErrors && !this.HasObjectMatchingIdentity)
				{
					base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(MailboxIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, null);
				}
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002D488 File Offset: 0x0002B688
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			this.HasObjectMatchingIdentity = true;
			ADUser aduser = (ADUser)dataObject;
			if (aduser.Database == null || aduser.ExchangeGuid == Guid.Empty)
			{
				base.Validate(aduser);
			}
			else
			{
				ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadMailboxSecurityDescriptor((ADUser)dataObject, PermissionTaskHelper.GetReadOnlySession(base.DomainController), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				if (!this.Owner.IsPresent)
				{
					AuthorizationRuleCollection accessRules = activeDirectorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
					int num = 0;
					while (accessRules.Count > num)
					{
						ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)accessRules[num];
						if (this.SecurityPrincipal == null || this.SecurityPrincipal.Sid == activeDirectoryAccessRule.IdentityReference || this.SecurityPrincipal.SidHistory.Contains(activeDirectoryAccessRule.IdentityReference as SecurityIdentifier))
						{
							MailboxAcePresentationObject mailboxAcePresentationObject = new MailboxAcePresentationObject(activeDirectoryAccessRule, ((ADRawEntry)dataObject).Id);
							if (Globals.IsDatacenter && base.TenantGlobalCatalogSession != null)
							{
								SecurityIdentifier securityIdentifier = (SecurityIdentifier)activeDirectoryAccessRule.IdentityReference;
								ADRecipient adrecipient = null;
								try
								{
									adrecipient = base.TenantGlobalCatalogSession.FindBySid(securityIdentifier);
								}
								catch
								{
								}
								if (adrecipient != null)
								{
									string friendlyName = (!string.IsNullOrEmpty(adrecipient.DisplayName)) ? adrecipient.DisplayName : adrecipient.Name;
									mailboxAcePresentationObject.User = new SecurityPrincipalIdParameter(securityIdentifier, friendlyName);
								}
							}
							mailboxAcePresentationObject.ResetChangeTracking(true);
							base.WriteResult(mailboxAcePresentationObject);
						}
						num++;
					}
				}
				else
				{
					IdentityReference owner = activeDirectorySecurity.GetOwner(typeof(NTAccount));
					OwnerPresentationObject dataObject2 = new OwnerPresentationObject(((ADUser)dataObject).Id, owner.ToString());
					base.WriteResult(dataObject2);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002D664 File Offset: 0x0002B864
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			if (this.storeSession != null)
			{
				this.storeSession.Dispose();
				this.storeSession = null;
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002D686 File Offset: 0x0002B886
		protected override void Dispose(bool disposing)
		{
			if (this.storeSession != null)
			{
				this.storeSession.Dispose();
				this.storeSession = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000238 RID: 568
		private bool hasObjectMatchingIdentity;

		// Token: 0x04000239 RID: 569
		private IADSecurityPrincipal securityPrincipal;

		// Token: 0x0400023A RID: 570
		private MapiMessageStoreSession storeSession;
	}
}
