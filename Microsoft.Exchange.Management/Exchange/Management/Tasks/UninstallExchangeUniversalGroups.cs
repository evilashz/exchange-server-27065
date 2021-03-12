using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E9 RID: 745
	[Cmdlet("Uninstall", "ExchangeUniversalGroups", SupportsShouldProcess = true)]
	public sealed class UninstallExchangeUniversalGroups : SetupTaskBase
	{
		// Token: 0x060019B8 RID: 6584 RVA: 0x00072808 File Offset: 0x00070A08
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.RemoveGroupByWKGuid(WellKnownGuid.ExSWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.MaSWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EraWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EmaWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EpaWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.E3iWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EwpWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EtsWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EahoWkGuid);
			this.RemoveGroupByWKGuid(WellKnownGuid.EfomgWkGuid);
			foreach (RoleGroupDefinition roleGroupDefinition in InitializeExchangeUniversalGroups.RoleGroupsToCreate())
			{
				if (!roleGroupDefinition.RoleGroupGuid.Equals(WellKnownGuid.EoaWkGuid))
				{
					this.RemoveGroupByWKGuid(roleGroupDefinition.RoleGroupGuid);
				}
			}
			try
			{
				this.RemoveGroupByWKGuid(WellKnownGuid.EoaWkGuid);
			}
			catch (ADOperationException ex)
			{
				this.WriteWarning(Strings.NeedManuallyRemoveEOA(ex.Message));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0007291C File Offset: 0x00070B1C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.configContainer = this.configurationSession.Read<ConfigurationContainer>(this.configurationSession.ConfigurationNamingContext);
			if (this.configContainer == null)
			{
				base.ThrowTerminatingError(new ConfigurationContainerNotFoundException(), ErrorCategory.InvalidData, null);
			}
			this.exchangeConfigContainer = this.configurationSession.GetExchangeConfigurationContainer();
			if (this.exchangeConfigContainer == null)
			{
				base.ThrowTerminatingError(new MicrosoftExchangeContainerNotFoundException(), ErrorCategory.InvalidData, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00072990 File Offset: 0x00070B90
		private void RemoveGroupByWKGuid(Guid wkGuid)
		{
			ADGroup adgroup = base.ResolveExchangeGroupGuid<ADGroup>(wkGuid);
			DNWithBinary dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(this.exchangeConfigContainer.OtherWellKnownObjects, wkGuid);
			if (dnwithBinary != null && this.exchangeConfigContainer.OtherWellKnownObjects.Remove(dnwithBinary))
			{
				this.configurationSession.Save(this.exchangeConfigContainer);
			}
			dnwithBinary = DirectoryCommon.FindWellKnownObjectEntry(this.configContainer.OtherWellKnownObjects, wkGuid);
			if (dnwithBinary != null && this.configContainer.OtherWellKnownObjects.Remove(dnwithBinary))
			{
				this.configurationSession.Save(this.configContainer);
			}
			if (adgroup != null)
			{
				adgroup.Session.Delete(adgroup);
			}
		}

		// Token: 0x04000B1D RID: 2845
		private ConfigurationContainer configContainer;

		// Token: 0x04000B1E RID: 2846
		private ExchangeConfigurationContainer exchangeConfigContainer;
	}
}
