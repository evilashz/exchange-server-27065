using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200026F RID: 623
	[Cmdlet("Unconfigure", "WSManIISHosting", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class UnconfigureWSManIISHosting : ConfigureWSManIISHostingBase
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00063252 File Offset: 0x00061452
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageUnconfigureWSManIISHosting;
			}
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0006325C File Offset: 0x0006145C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			if (File.Exists(base.IISConfigFilePath))
			{
				using (ServerManager serverManager = new ServerManager())
				{
					bool flag = false;
					SectionGroup sectionGroup = serverManager.GetApplicationHostConfiguration().RootSectionGroup.SectionGroups["system.webServer"];
					SectionDefinition sectionDefinition = sectionGroup.Sections["system.management.wsmanagement.config"];
					if (sectionDefinition != null)
					{
						base.WriteVerbose(Strings.VerboseRemoveWSManConfigSection(base.IISConfigFilePath));
						sectionGroup.Sections.Remove("system.management.wsmanagement.config");
						flag = true;
					}
					ConfigurationElementCollection collection = serverManager.GetApplicationHostConfiguration().GetSection("system.webServer/globalModules").GetCollection();
					for (int i = 0; i < collection.Count; i++)
					{
						ConfigurationElement configurationElement = collection[i];
						object attributeValue = configurationElement.GetAttributeValue("name");
						if (attributeValue != null && string.Equals("WSMan", attributeValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
						{
							base.WriteVerbose(Strings.VerboseRemoveWSManGlobalModule("WSMan", base.IISConfigFilePath));
							collection.Remove(configurationElement);
							flag = true;
							break;
						}
					}
					foreach (Site site in serverManager.Sites)
					{
						if (site.Id == 1L)
						{
							base.DefaultSiteName = site.Name;
						}
					}
					if (flag)
					{
						serverManager.CommitChanges();
						if (base.DefaultSiteName != null)
						{
							base.RestartDefaultWebSite();
						}
					}
				}
			}
			if (File.Exists(base.WSManCfgSchemaFilePath))
			{
				try
				{
					File.Delete(base.WSManCfgSchemaFilePath);
				}
				catch (SystemException ex)
				{
					this.WriteWarning(Strings.ErrorFailedToRemoveWinRMSchemaFile(base.WSManCfgSchemaFilePath, ex.ToString()));
				}
			}
			TaskLogger.LogEnter();
		}
	}
}
