using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000725 RID: 1829
	[Cmdlet("Remove", "RMSTrustedPublishingDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRmsTrustedPublishingDomain : RemoveSystemConfigurationObjectTask<RmsTrustedPublishingDomainIdParameter, RMSTrustedPublishingDomain>
	{
		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x00109B7C File Offset: 0x00107D7C
		// (set) Token: 0x060040DC RID: 16604 RVA: 0x00109BA2 File Offset: 0x00107DA2
		[Parameter(Mandatory = false)]
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

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x00109BBA File Offset: 0x00107DBA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRMSTPD(this.Identity.ToString());
			}
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x00109BCC File Offset: 0x00107DCC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			bool flag = false;
			ADPagedReader<RMSTrustedPublishingDomain> adpagedReader = this.ConfigurationSession.FindPaged<RMSTrustedPublishingDomain>(base.DataObject.Id.Parent, QueryScope.OneLevel, null, null, 0);
			foreach (RMSTrustedPublishingDomain rmstrustedPublishingDomain in adpagedReader)
			{
				if (!rmstrustedPublishingDomain.Default)
				{
					flag = true;
					break;
				}
			}
			if (base.DataObject.Default && flag)
			{
				base.WriteError(new CannotRemoveDefaultRmsTpdWithoutSettingAnotherDefaultException(), (ErrorCategory)1000, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00109C74 File Offset: 0x00107E74
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = false;
			IRMConfiguration irmconfiguration = IRMConfiguration.Read((IConfigurationSession)base.DataSession, out flag);
			if (irmconfiguration == null)
			{
				base.WriteError(new FailedToAccessIrmConfigurationException(), (ErrorCategory)1002, this.Identity);
			}
			if (base.DataObject.Default)
			{
				if (!this.Force && !base.ShouldContinue(Strings.RemoveDefaultTPD(this.Identity.ToString())))
				{
					TaskLogger.LogExit();
					return;
				}
				irmconfiguration.InternalLicensingEnabled = false;
				irmconfiguration.SharedServerBoxRacIdentity = null;
				irmconfiguration.PublishingLocation = null;
				irmconfiguration.ServiceLocation = null;
				irmconfiguration.LicensingLocation = null;
			}
			else
			{
				if (irmconfiguration.LicensingLocation.Contains(base.DataObject.IntranetLicensingUrl))
				{
					irmconfiguration.LicensingLocation.Remove(base.DataObject.IntranetLicensingUrl);
				}
				if (irmconfiguration.LicensingLocation.Contains(base.DataObject.ExtranetLicensingUrl))
				{
					irmconfiguration.LicensingLocation.Remove(base.DataObject.ExtranetLicensingUrl);
				}
			}
			base.DataSession.Save(irmconfiguration);
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00109D86 File Offset: 0x00107F86
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception);
		}
	}
}
