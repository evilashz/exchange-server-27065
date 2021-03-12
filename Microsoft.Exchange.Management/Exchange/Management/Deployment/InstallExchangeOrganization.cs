using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001EB RID: 491
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "ExchangeOrganization", SupportsShouldProcess = true)]
	public sealed class InstallExchangeOrganization : ComponentInfoBasedTask
	{
		// Token: 0x060010AE RID: 4270 RVA: 0x00049BF8 File Offset: 0x00047DF8
		public InstallExchangeOrganization()
		{
			base.ImplementsResume = false;
			base.Fields["InstallationMode"] = InstallationModes.Install;
			base.Fields["OrgConfigVersion"] = Organization.OrgConfigurationVersion;
			base.Fields["PrepareSchema"] = false;
			base.Fields["PrepareOrganization"] = false;
			base.Fields["CustomerFeedbackEnabled"] = null;
			base.Fields["Industry"] = IndustryType.NotSpecified;
			base.Fields["PrepareDomain"] = false;
			base.Fields["PrepareSCT"] = false;
			base.Fields["PrepareAllDomains"] = false;
			base.Fields["BinPath"] = ConfigurationContext.Setup.BinPath;
			base.Fields["ActiveDirectorySplitPermissions"] = null;
			ADSession.InitializeForestModeFlagForLocalForest();
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00049D02 File Offset: 0x00047F02
		protected override LocalizedString Description
		{
			get
			{
				if (this.PrepareOrganization)
				{
					return Strings.InstallExchangeOrganizationDescription;
				}
				if (this.PrepareDomain || this.PrepareAllDomains)
				{
					return Strings.PrepareDomainDescription;
				}
				return Strings.ConfigureSchema;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00049D2D File Offset: 0x00047F2D
		// (set) Token: 0x060010B1 RID: 4273 RVA: 0x00049D44 File Offset: 0x00047F44
		[Parameter(Mandatory = false)]
		public bool PrepareOrganization
		{
			get
			{
				return (bool)base.Fields["PrepareOrganization"];
			}
			set
			{
				base.Fields["PrepareOrganization"] = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00049D5C File Offset: 0x00047F5C
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x00049D73 File Offset: 0x00047F73
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)base.Fields["CustomerFeedbackEnabled"];
			}
			set
			{
				base.Fields["CustomerFeedbackEnabled"] = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00049D8B File Offset: 0x00047F8B
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x00049DA2 File Offset: 0x00047FA2
		[Parameter(Mandatory = false)]
		public IndustryType Industry
		{
			get
			{
				return (IndustryType)base.Fields["Industry"];
			}
			set
			{
				base.Fields["Industry"] = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00049DBA File Offset: 0x00047FBA
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00049DD1 File Offset: 0x00047FD1
		[Parameter(Mandatory = false)]
		public bool? ActiveDirectorySplitPermissions
		{
			get
			{
				return (bool?)base.Fields["ActiveDirectorySplitPermissions"];
			}
			set
			{
				base.Fields["ActiveDirectorySplitPermissions"] = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00049DE9 File Offset: 0x00047FE9
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00049E00 File Offset: 0x00048000
		[Parameter(Mandatory = false)]
		public bool PrepareSchema
		{
			get
			{
				return (bool)base.Fields["PrepareSchema"];
			}
			set
			{
				base.Fields["PrepareSchema"] = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x00049E18 File Offset: 0x00048018
		// (set) Token: 0x060010BB RID: 4283 RVA: 0x00049E2F File Offset: 0x0004802F
		[Parameter(Mandatory = false)]
		public bool PrepareDomain
		{
			get
			{
				return (bool)base.Fields["PrepareDomain"];
			}
			set
			{
				base.Fields["PrepareDomain"] = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x00049E47 File Offset: 0x00048047
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x00049E5E File Offset: 0x0004805E
		[Parameter(Mandatory = false)]
		public bool PrepareSCT
		{
			get
			{
				return (bool)base.Fields["PrepareSCT"];
			}
			set
			{
				base.Fields["PrepareSCT"] = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00049E76 File Offset: 0x00048076
		// (set) Token: 0x060010BF RID: 4287 RVA: 0x00049E8D File Offset: 0x0004808D
		[Parameter(Mandatory = false)]
		public bool PrepareAllDomains
		{
			get
			{
				return (bool)base.Fields["PrepareAllDomains"];
			}
			set
			{
				base.Fields["PrepareAllDomains"] = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00049EA5 File Offset: 0x000480A5
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x00049EBC File Offset: 0x000480BC
		[Parameter(Mandatory = false)]
		public string Domain
		{
			get
			{
				return (string)base.Fields["Domain"];
			}
			set
			{
				base.Fields["Domain"] = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00049ECF File Offset: 0x000480CF
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x00049EE6 File Offset: 0x000480E6
		[Parameter(Mandatory = false)]
		public string OrganizationName
		{
			get
			{
				return (string)base.Fields["OrganizationName"];
			}
			set
			{
				base.Fields["OrganizationName"] = value;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00049EFC File Offset: 0x000480FC
		protected override void CheckInstallationMode()
		{
			bool flag = false;
			try
			{
				IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 272, "CheckInstallationMode", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\InstallExchangeOrganization.cs");
				configurationSession.GetOrgContainer();
				flag = true;
			}
			catch (DataSourceOperationException)
			{
			}
			catch (DataSourceTransientException)
			{
			}
			catch (DataValidationException)
			{
			}
			if (flag)
			{
				base.Fields["InstallationMode"] = InstallationModes.BuildToBuildUpgrade;
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00049F8C File Offset: 0x0004818C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.PrepareSCT && base.Fields.Contains("DatacenterFfoEnvironment") && Convert.ToBoolean(base.Fields["DatacenterFfoEnvironment"]))
			{
				base.WriteVerbose(Strings.FFoDisablePrepareSct);
				this.PrepareSCT = false;
			}
			base.ComponentInfoFileNames = new List<string>();
			if (this.PrepareSchema)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\UpdateResourcePropertySchemaComponent.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\ADSchemaComponent.xml");
			}
			if (this.PrepareOrganization)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\CommonGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\TransportGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\BridgeheadGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\ClientAccessGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\MailboxGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\UnifiedMessagingGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\CafeGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\DatacenterGlobalConfig.xml");
			}
			if (this.PrepareDomain || this.PrepareAllDomains)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\DomainGlobalConfig.xml");
				base.ComponentInfoFileNames.Add("setup\\data\\DatacenterDomainGlobalConfig.xml");
			}
			if (this.PrepareOrganization)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\PostPrepForestGlobalConfig.xml");
			}
			if (this.PrepareSCT)
			{
				base.ComponentInfoFileNames.Add("setup\\data\\PrepareSharedConfig.xml");
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004A10A File Offset: 0x0004830A
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 364, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\InstallExchangeOrganization.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0004A13B File Offset: 0x0004833B
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.ComponentInfoFileNames.Count == 0)
			{
				base.WriteProgress(new LocalizedString(this.Description), Strings.ProgressStatusCompleted, 100);
			}
			else
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004A17C File Offset: 0x0004837C
		protected override void PopulateContextVariables()
		{
			ADDomain addomain = ADForest.GetLocalForest().FindLocalDomain();
			if (addomain == null || addomain.Fqdn == null)
			{
				throw new InvalidFqdnException();
			}
			base.Fields["FullyQualifiedDomainName"] = addomain.Fqdn;
			if (this.PrepareSchema)
			{
				ADSchemaVersion schemaVersion = DirectoryUtilities.GetSchemaVersion(base.DomainController);
				switch (schemaVersion)
				{
				case ADSchemaVersion.Windows:
					base.Fields["SchemaPrefix"] = "PostWindows2003_";
					break;
				case ADSchemaVersion.Exchange2000:
					base.Fields["SchemaPrefix"] = "PostExchange2000_";
					break;
				case ADSchemaVersion.Exchange2003:
				case ADSchemaVersion.Exchange2007Rtm:
					base.Fields["SchemaPrefix"] = "PostExchange2003_";
					break;
				}
				base.Fields["UpdateResourcePropertySchema"] = false;
				base.Fields["ResourcePropertySchemaSaveFile"] = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, "ResourcePropertySchema.xml");
				if (this.ShouldUpdateResourceSchemaAttributeId(schemaVersion))
				{
					base.Fields["UpdateResourcePropertySchema"] = true;
					base.WriteVerbose(Strings.WillSaveResourcePropertySchemaValue((string)base.Fields["ResourcePropertySchemaSaveFile"]));
				}
			}
			base.PopulateContextVariables();
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0004A2B4 File Offset: 0x000484B4
		private bool ShouldUpdateResourceSchemaAttributeId(ADSchemaVersion schemaVersion)
		{
			bool result = false;
			if (schemaVersion == ADSchemaVersion.Exchange2007Rtm)
			{
				ADSchemaAttributeObject[] array = this.configurationSession.Find<ADSchemaAttributeObject>(this.configurationSession.SchemaNamingContext, QueryScope.OneLevel, new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "ms-exch-resource-property-schema"),
					new ComparisonFilter(ComparisonOperator.Equal, ADSchemaAttributeSchema.AttributeID, "1.2.840.113556.1.4.7000.102.50329")
				}), null, 1);
				if (array.Length > 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0004A31D File Offset: 0x0004851D
		protected override bool IsKnownException(Exception e)
		{
			return e is DataSourceOperationException || e is DataSourceTransientException || e is DataValidationException || base.IsKnownException(e);
		}

		// Token: 0x04000781 RID: 1921
		private const string OldResourceSchemaAttributeID = "1.2.840.113556.1.4.7000.102.50329";

		// Token: 0x04000782 RID: 1922
		private IConfigurationSession configurationSession;
	}
}
