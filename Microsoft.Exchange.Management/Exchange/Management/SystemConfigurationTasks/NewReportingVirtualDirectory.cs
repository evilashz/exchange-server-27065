using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C2D RID: 3117
	[Cmdlet("New", "ReportingVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewReportingVirtualDirectory : ManageReportingVirtualDirectory
	{
		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x060075F8 RID: 30200 RVA: 0x001E1761 File Offset: 0x001DF961
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewReportingVirtualDirectory;
			}
		}

		// Token: 0x060075F9 RID: 30201 RVA: 0x001E1768 File Offset: 0x001DF968
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
			createVirtualDirectory.Name = "Reporting";
			createVirtualDirectory.Parent = "IIS://localhost/W3SVC/1/Root";
			createVirtualDirectory.LocalPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\Reporting");
			createVirtualDirectory.CustomizedVDirProperties = new List<MetabaseProperty>
			{
				new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous),
				new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSLNegotiateCert | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128),
				new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script)
			};
			createVirtualDirectory.ApplicationPool = "MSExchangeReportingAppPool";
			createVirtualDirectory.AppPoolIdentityType = MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			createVirtualDirectory.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			createVirtualDirectory.Initialize();
			createVirtualDirectory.Execute();
			TaskLogger.LogExit();
		}
	}
}
