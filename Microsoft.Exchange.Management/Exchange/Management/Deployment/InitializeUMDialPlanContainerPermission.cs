using System;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D9 RID: 473
	[Cmdlet("initialize", "umdialplancontainerpermission")]
	public class InitializeUMDialPlanContainerPermission : SetupTaskBase
	{
		// Token: 0x0600105E RID: 4190 RVA: 0x00048A30 File Offset: 0x00046C30
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ADGroup adgroup = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.RgDelegatedSetupWkGuid);
				if (adgroup != null)
				{
					ADObjectId descendantId = this.configurationSession.GetOrgContainerId().GetDescendantId(new ADObjectId("CN=UM DialPlan Container", Guid.Empty));
					ActiveDirectoryAccessRule[] umdialPlanAcesToServerAdmin = this.GetUMDialPlanAcesToServerAdmin(this.configurationSession, adgroup.Sid);
					DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.configurationSession, descendantId, umdialPlanAcesToServerAdmin);
				}
			}
			catch (SecurityDescriptorAccessDeniedException exception)
			{
				base.WriteError(exception, ErrorCategory.PermissionDenied, null);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00048AD4 File Offset: 0x00046CD4
		private ActiveDirectoryAccessRule[] GetUMDialPlanAcesToServerAdmin(IConfigurationSession configSession, SecurityIdentifier sid)
		{
			return new ActiveDirectoryAccessRule[]
			{
				new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.WriteProperty, AccessControlType.Allow, DirectoryCommon.GetSchemaPropertyGuid(configSession, "msExchUMAvailableLanguages"), ActiveDirectorySecurityInheritance.Descendents, DirectoryCommon.GetSchemaClassGuid(configSession, "msExchUMDialPlan"))
			};
		}
	}
}
