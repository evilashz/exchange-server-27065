using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02000D02 RID: 3330
	[Cmdlet("Get", "ToolInformation")]
	public sealed class GetToolInformation : Task
	{
		// Token: 0x170027AC RID: 10156
		// (get) Token: 0x06007FE4 RID: 32740 RVA: 0x0020B0E6 File Offset: 0x002092E6
		// (set) Token: 0x06007FE5 RID: 32741 RVA: 0x0020B0FD File Offset: 0x002092FD
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public ToolId Identity
		{
			get
			{
				return (ToolId)base.Fields["ToolId"];
			}
			set
			{
				base.Fields["ToolId"] = value;
			}
		}

		// Token: 0x170027AD RID: 10157
		// (get) Token: 0x06007FE6 RID: 32742 RVA: 0x0020B115 File Offset: 0x00209315
		// (set) Token: 0x06007FE7 RID: 32743 RVA: 0x0020B12C File Offset: 0x0020932C
		[Parameter(Mandatory = true)]
		public Version Version
		{
			get
			{
				return (Version)base.Fields["Version"];
			}
			set
			{
				base.Fields["Version"] = value;
			}
		}

		// Token: 0x170027AE RID: 10158
		// (get) Token: 0x06007FE8 RID: 32744 RVA: 0x0020B13F File Offset: 0x0020933F
		// (set) Token: 0x06007FE9 RID: 32745 RVA: 0x0020B156 File Offset: 0x00209356
		[Parameter(Mandatory = false)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x0020B16C File Offset: 0x0020936C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.LoadSupportedToolsData();
			this.tenantVersionRequired = this.toolsData.RequiresTenantVersion();
			if (this.tenantVersionRequired)
			{
				this.rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				base.CurrentOrganizationId = OrganizationTaskHelper.ResolveOrganization(this, this.Organization, this.rootOrgContainerId, Strings.ErrorOrganizationParameterRequired);
			}
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x0020B1C6 File Offset: 0x002093C6
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			if (this.tenantVersionRequired)
			{
				this.session = GetToolInformation.CreateSession();
			}
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x0020B1E4 File Offset: 0x002093E4
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			Version tenantVersion = null;
			if (this.tenantVersionRequired)
			{
				ExchangeConfigurationUnit dataObject = this.session.Read<ExchangeConfigurationUnit>(base.CurrentOrganizationId.ConfigurationUnit);
				Version installedVersion = ConfigurationContext.Setup.InstalledVersion;
				TenantOrganizationPresentationObject tenantOrganizationPresentationObject = new TenantOrganizationPresentationObject(dataObject);
				tenantVersion = new Version(installedVersion.Major, installedVersion.Minor, tenantOrganizationPresentationObject.BuildMajor, tenantOrganizationPresentationObject.BuildMinor);
			}
			SupportedVersion supportedVersion = this.toolsData.GetSupportedVersion(this.Identity, tenantVersion);
			base.WriteObject(this.ConstructOutputObject(supportedVersion));
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x0020B268 File Offset: 0x00209468
		private void LoadSupportedToolsData()
		{
			try
			{
				this.toolsData = SupportedToolsData.GetSupportedToolData(GetToolInformation.GetFilePath("SupportedTools.xml"), GetToolInformation.GetFilePath("SupportedTools.xsd"));
			}
			catch (Exception e)
			{
				this.HandleInvalidSupportedToolsData(e);
			}
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x0020B2B0 File Offset: 0x002094B0
		private static string GetFilePath(string fileName)
		{
			return Path.Combine(ConfigurationContext.Setup.BinPath, fileName);
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x0020B2C0 File Offset: 0x002094C0
		private void HandleInvalidSupportedToolsData(Exception e)
		{
			if (e is FileNotFoundException)
			{
				FileNotFoundException ex = e as FileNotFoundException;
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_SupportedToolsInformationFileMissing, new string[]
				{
					ex.FileName
				});
			}
			else if (e is InvalidDataException)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_SupportedToolsInformationDataFileInconsistent, new string[]
				{
					GetToolInformation.GetFilePath("SupportedTools.xml"),
					e.Message
				});
			}
			else
			{
				Exception ex2 = e.InnerException ?? e;
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_SupportedToolsInformationDataFileCorupted, new string[]
				{
					GetToolInformation.GetFilePath("SupportedTools.xml"),
					ex2.Message
				});
			}
			base.WriteError(new SupportedToolsDataException(Strings.SupportedToolsUnableToGetToolData), ErrorCategory.InvalidData, null);
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x0020B378 File Offset: 0x00209578
		private ToolInformation ConstructOutputObject(SupportedVersion supportedVersion)
		{
			ToolInformation result;
			try
			{
				Version version = SupportedToolsData.GetVersion(supportedVersion.minSupportedVersion, SupportedToolsData.MinimumVersion);
				Version version2 = SupportedToolsData.GetVersion(supportedVersion.latestVersion, SupportedToolsData.MaximumVersion);
				ToolVersionStatus versionStatus = GetToolInformation.GetVersionStatus(version, version2, this.Version);
				Uri updateInformationUrl = (versionStatus != ToolVersionStatus.LatestVersion) ? new Uri(supportedVersion.updateUrl) : null;
				result = new ToolInformation(this.Identity, versionStatus, version, version2, updateInformationUrl);
			}
			catch (Exception e)
			{
				this.HandleInvalidSupportedToolsData(e);
				result = null;
			}
			return result;
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x0020B3FC File Offset: 0x002095FC
		private static ToolVersionStatus GetVersionStatus(Version minSupportedVersion, Version latestVersion, Version toolVersion)
		{
			if (toolVersion < minSupportedVersion)
			{
				return ToolVersionStatus.VersionNoLongerSupported;
			}
			if (toolVersion < latestVersion)
			{
				return ToolVersionStatus.NewerVersionAvailable;
			}
			return ToolVersionStatus.LatestVersion;
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x0020B418 File Offset: 0x00209618
		private static IConfigurationSession CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 261, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\tools\\GetToolInformation.cs");
		}

		// Token: 0x04003EBB RID: 16059
		private const string ToolInfoIdParameterName = "ToolId";

		// Token: 0x04003EBC RID: 16060
		private const string VersionParameterName = "Version";

		// Token: 0x04003EBD RID: 16061
		private const string OrganizationParameterName = "Organization";

		// Token: 0x04003EBE RID: 16062
		private const string SupportedToolsDataFileName = "SupportedTools.xml";

		// Token: 0x04003EBF RID: 16063
		private const string SupportedToolsSchemaFileName = "SupportedTools.xsd";

		// Token: 0x04003EC0 RID: 16064
		private ADObjectId rootOrgContainerId;

		// Token: 0x04003EC1 RID: 16065
		private IConfigurationSession session;

		// Token: 0x04003EC2 RID: 16066
		private SupportedToolsData toolsData;

		// Token: 0x04003EC3 RID: 16067
		private bool tenantVersionRequired;
	}
}
