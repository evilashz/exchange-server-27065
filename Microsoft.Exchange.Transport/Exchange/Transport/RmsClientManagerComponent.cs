using System;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000052 RID: 82
	internal sealed class RmsClientManagerComponent : ITransportComponent, IDiagnosable
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		void ITransportComponent.Load()
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000ACAB File Offset: 0x00008EAB
		void ITransportComponent.Unload()
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ACAD File Offset: 0x00008EAD
		string ITransportComponent.OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "RmsClientManager";
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			if (string.IsNullOrEmpty(parameters.Argument) || string.Equals(parameters.Argument, "help", StringComparison.OrdinalIgnoreCase))
			{
				xelement.Add(new XElement("help", "Supported arguments: config, licenses, templates, help [org=tenantId].  If org is specified, it must be the last argument.  Example: Get-ExchangeDiagnosticInfo -Process EdgeTransport -Component RmsClientManager -Argument \"templates org=contoso.com\""));
			}
			else
			{
				OrganizationId organizationId = OrganizationId.ForestWideOrgId;
				bool flag = parameters.Argument.IndexOf("templates", StringComparison.OrdinalIgnoreCase) != -1;
				bool flag2 = parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
				bool flag3 = parameters.Argument.IndexOf("licenses", StringComparison.OrdinalIgnoreCase) != -1;
				int num = parameters.Argument.IndexOf("org=", StringComparison.OrdinalIgnoreCase);
				if (num != -1)
				{
					string[] array = parameters.Argument.Substring(num).Split(RmsClientManagerComponent.OrgSeparator);
					if (array.Length == 2)
					{
						string text = array[1].Trim();
						if (!string.IsNullOrEmpty(text))
						{
							OrganizationIdParameter organization = new OrganizationIdParameter(text);
							ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
							try
							{
								organizationId = MapiTaskHelper.ResolveTargetOrganization(null, organization, rootOrgContainerIdForLocalForest, OrganizationId.ForestWideOrgId, OrganizationId.ForestWideOrgId);
							}
							catch (ManagementObjectNotFoundException)
							{
								return new XElement("error", "Organization not found.");
							}
						}
					}
				}
				if (flag3)
				{
					xelement.Add(RmsClientManager.GetLicenseDiagnosticInfo(organizationId));
				}
				if (flag)
				{
					xelement.Add(RmsClientManager.GetTemplateDiagnosticInfo(organizationId));
				}
				if (flag2)
				{
					xelement.Add(RmsClientManager.GetConfigDiagnosticInfo(organizationId));
				}
			}
			return xelement;
		}

		// Token: 0x0400015C RID: 348
		private static readonly char[] OrgSeparator = new char[]
		{
			'='
		};
	}
}
