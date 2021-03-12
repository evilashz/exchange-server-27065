using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F3 RID: 2035
	[Cmdlet("Install", "AvailabilityConfig")]
	public sealed class InstallAvailabilityConfig : NewFixedNameSystemConfigurationObjectTask<AvailabilityConfig>
	{
		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x00123753 File Offset: 0x00121953
		// (set) Token: 0x06004706 RID: 18182 RVA: 0x0012376A File Offset: 0x0012196A
		[Parameter(Mandatory = true)]
		public new Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x00123780 File Offset: 0x00121980
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				return configurationSession.GetOrgContainerId();
			}
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x001237A0 File Offset: 0x001219A0
		protected override IConfigurable PrepareDataObject()
		{
			AvailabilityConfig availabilityConfig = (AvailabilityConfig)base.PrepareDataObject();
			IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
			availabilityConfig.SetId(configurationSession.GetOrgContainerId().GetDescendantId(AvailabilityConfig.Container));
			return availabilityConfig;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x001237DC File Offset: 0x001219DC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 84, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Availability\\InstallAvailabilityConfig.cs");
			configurationSession.UseConfigNC = false;
			ADDomain addomain = ADForest.GetLocalForest().FindRootDomain(true);
			if (addomain == null)
			{
				base.ThrowTerminatingError(new RootDomainNotFoundException(), ErrorCategory.InvalidData, null);
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(addomain.OriginatingServer, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 97, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Availability\\InstallAvailabilityConfig.cs");
			IRecipientSession tenantOrRootOrgRecipientSession2 = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 104, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Availability\\InstallAvailabilityConfig.cs");
			this.exs = this.ResolveExchangeGroupGuid(WellKnownGuid.ExSWkGuid, tenantOrRootOrgRecipientSession, tenantOrRootOrgRecipientSession2, configurationSession);
			if (this.exs == null)
			{
				base.ThrowTerminatingError(new ExSGroupNotFoundException(WellKnownGuid.ExSWkGuid), ErrorCategory.InvalidData, null);
			}
			this.eoa = this.ResolveExchangeGroupGuid(WellKnownGuid.EoaWkGuid, tenantOrRootOrgRecipientSession, tenantOrRootOrgRecipientSession2, configurationSession);
			if (this.eoa == null)
			{
				base.ThrowTerminatingError(new ExOrgAdminSGroupNotFoundException(WellKnownGuid.EoaWkGuid), ErrorCategory.InvalidData, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x001238D8 File Offset: 0x00121AD8
		private ADGroup ResolveExchangeGroupGuid(Guid wkg, IRecipientSession rootSession, IRecipientSession gcSession, IConfigurationSession configSession)
		{
			ADGroup adgroup = null;
			try
			{
				adgroup = rootSession.ResolveWellKnownGuid<ADGroup>(wkg, configSession.ConfigurationNamingContext);
			}
			catch (ADReferralException)
			{
			}
			if (adgroup == null)
			{
				adgroup = gcSession.ResolveWellKnownGuid<ADGroup>(wkg, configSession.ConfigurationNamingContext);
			}
			return adgroup;
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00123920 File Offset: 0x00121B20
		internal static void SetAvailabilityAces(SecurityIdentifier exchangeServersSid, AvailabilityConfig availabilityConfig, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			Guid schemaGuid;
			using (ActiveDirectorySchema currentSchema = ActiveDirectorySchema.GetCurrentSchema())
			{
				using (ActiveDirectorySchemaClass activeDirectorySchemaClass = currentSchema.FindClass("msExchAvailabilityAddressSpace"))
				{
					schemaGuid = activeDirectorySchemaClass.SchemaGuid;
				}
			}
			Guid schemaGuid2;
			using (ActiveDirectorySchema currentSchema2 = ActiveDirectorySchema.GetCurrentSchema())
			{
				using (ActiveDirectorySchemaProperty activeDirectorySchemaProperty = currentSchema2.FindProperty("msExchAvailabilityUserPassword"))
				{
					schemaGuid2 = activeDirectorySchemaProperty.SchemaGuid;
				}
			}
			DirectoryCommon.SetAces(verboseLogger, null, availabilityConfig, new List<ActiveDirectoryAccessRule>
			{
				new ActiveDirectoryAccessRule(exchangeServersSid, ActiveDirectoryRights.ReadProperty, AccessControlType.Allow, schemaGuid2, ActiveDirectorySecurityInheritance.Descendents, schemaGuid)
			}.ToArray());
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x001239F4 File Offset: 0x00121BF4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			AvailabilityConfig availabilityConfig = (AvailabilityConfig)base.DataSession.Read<AvailabilityConfig>(this.DataObject.Id);
			if (availabilityConfig == null)
			{
				base.InternalProcessRecord();
				if (base.HasErrors)
				{
					return;
				}
				availabilityConfig = (AvailabilityConfig)base.DataSession.Read<AvailabilityConfig>(this.DataObject.Id);
				if (availabilityConfig == null)
				{
					base.WriteError(new AvailabilityConfigReadException(this.DataObject.Id.ToDNString()), ErrorCategory.ReadError, this.DataObject.Identity);
					return;
				}
			}
			try
			{
				InstallAvailabilityConfig.SetAvailabilityAces(this.exs.Sid, availabilityConfig, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			catch (SecurityDescriptorAccessDeniedException exception)
			{
				base.WriteError(exception, ErrorCategory.PermissionDenied, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002B0B RID: 11019
		private ADGroup exs;

		// Token: 0x04002B0C RID: 11020
		private ADGroup eoa;
	}
}
