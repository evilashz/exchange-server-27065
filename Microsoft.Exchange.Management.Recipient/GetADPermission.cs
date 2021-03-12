using System;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A1 RID: 161
	[Cmdlet("Get", "ADPermission", DefaultParameterSetName = "AccessRights")]
	public sealed class GetADPermission : GetPermissionTaskBase<ADRawEntryIdParameter, ADRawEntry>
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x0002D15B File Offset: 0x0002B35B
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.readOnlySession = PermissionTaskHelper.GetReadOnlySession(base.DomainController);
			TaskLogger.LogExit();
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002D17E File Offset: 0x0002B37E
		protected override IConfigDataProvider CreateSession()
		{
			return this.readOnlySession;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002D188 File Offset: 0x0002B388
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			base.HasObjectMatchingIdentity = true;
			ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadAdSecurityDescriptor((ADRawEntry)dataObject, (IConfigurationSession)base.DataSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (!base.Owner.IsPresent)
			{
				AuthorizationRuleCollection accessRules = activeDirectorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
				int num = 0;
				while (accessRules.Count > num)
				{
					ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)accessRules[num];
					if (base.SecurityPrincipal == null || (base.SecurityPrincipal != null && base.SecurityPrincipal == activeDirectoryAccessRule.IdentityReference))
					{
						ADAcePresentationObject adacePresentationObject = new ADAcePresentationObject(activeDirectoryAccessRule, ((ADRawEntry)dataObject).Id);
						adacePresentationObject.ResetChangeTracking(true);
						base.WriteResult(adacePresentationObject);
					}
					num++;
				}
			}
			else
			{
				IdentityReference owner = activeDirectorySecurity.GetOwner(typeof(NTAccount));
				base.WriteResult(new OwnerPresentationObject(((ADRawEntry)dataObject).Id, owner.ToString()));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000237 RID: 567
		private IConfigurationSession readOnlySession;
	}
}
