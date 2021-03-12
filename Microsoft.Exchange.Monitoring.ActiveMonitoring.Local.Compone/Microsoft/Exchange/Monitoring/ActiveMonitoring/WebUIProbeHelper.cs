using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Hygiene.Data.Directory;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000096 RID: 150
	internal class WebUIProbeHelper : ProbeDefinitionHelper
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x0002086C File Offset: 0x0001EA6C
		internal override List<ProbeDefinition> CreateDefinition()
		{
			List<ProbeDefinition> list = new List<ProbeDefinition>();
			XmlNode xmlNode = base.DefinitionNode.SelectSingleNode("ExtensionAttributes");
			if (xmlNode == null)
			{
				string message = "The required node ExtensionAttributes was not provided for the WebUIProbe.";
				WTFDiagnostics.TraceError(ExTraceGlobals.GenericHelperTracer, base.TraceContext, message, null, "CreateDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\WebUIProbeHelper.cs", 74);
				throw new InvalidOperationException(message);
			}
			ProbeDefinition probeDefinition = base.CreateProbeDefinition();
			probeDefinition.ExtensionAttributes = xmlNode.OuterXml;
			string optionalXmlAttribute = DefinitionHelperBase.GetOptionalXmlAttribute<string>(base.DiscoveryContext.ContextNode, "FeatureTag", string.Empty, base.TraceContext);
			if (optionalXmlAttribute != string.Empty)
			{
				WebUIProbeHelper.WorkDefinitionUser workDefinitionUser = this.GetWorkDefinitionUser(optionalXmlAttribute);
				string newValue = string.Empty;
				string newValue2 = string.Empty;
				string newValue3 = string.Empty;
				WebUIProbeHelper.DatacenterEnvironment datacenterEnvironment = this.GetDatacenterEnvironment();
				if (FfoLocalEndpointManager.IsForefrontForOfficeDatacenter)
				{
					switch (datacenterEnvironment)
					{
					case WebUIProbeHelper.DatacenterEnvironment.FfoDogfood:
						newValue = WebUIProbeHelper.UmcUrl.FfoDogfood;
						newValue2 = WebUIProbeHelper.FfoRwsUrl.FfoDogfood;
						newValue3 = WebUIProbeHelper.UccUrl.FfoDogfood;
						goto IL_168;
					case WebUIProbeHelper.DatacenterEnvironment.FfoProduction:
						newValue = WebUIProbeHelper.UmcUrl.FfoProduction;
						newValue2 = WebUIProbeHelper.FfoRwsUrl.FfoProduction;
						newValue3 = WebUIProbeHelper.UccUrl.FfoProduction;
						goto IL_168;
					}
					newValue = WebUIProbeHelper.UmcUrl.FfoTest;
					newValue2 = WebUIProbeHelper.FfoRwsUrl.FfoTest;
					newValue3 = WebUIProbeHelper.UccUrl.FfoTest;
				}
				else
				{
					switch (datacenterEnvironment)
					{
					case WebUIProbeHelper.DatacenterEnvironment.ExoDogFood:
						newValue = WebUIProbeHelper.UmcUrl.ExoDogfood;
						newValue2 = WebUIProbeHelper.FfoRwsUrl.ExoDogfood;
						goto IL_168;
					case WebUIProbeHelper.DatacenterEnvironment.ExoProduction:
						newValue = WebUIProbeHelper.UmcUrl.ExoProduction;
						newValue2 = WebUIProbeHelper.FfoRwsUrl.ExoProduction;
						goto IL_168;
					}
					newValue = WebUIProbeHelper.UmcUrl.ExoTest;
					newValue2 = WebUIProbeHelper.FfoRwsUrl.ExoTest;
				}
				IL_168:
				string text = probeDefinition.ExtensionAttributes;
				text = text.Replace(WebUIProbeHelper.ReplacementTokens.FfoRwsUrl, newValue2);
				text = text.Replace(WebUIProbeHelper.ReplacementTokens.UmcUrl, newValue);
				text = text.Replace(WebUIProbeHelper.ReplacementTokens.UccUrl, newValue3);
				text = text.Replace(WebUIProbeHelper.ReplacementTokens.Tenant, workDefinitionUser.TenantName);
				text = text.Replace(WebUIProbeHelper.ReplacementTokens.User, workDefinitionUser.Username);
				text = text.Replace(WebUIProbeHelper.ReplacementTokens.Password, workDefinitionUser.Password);
				probeDefinition.ExtensionAttributes = text;
			}
			list.Add(probeDefinition);
			return list;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00020A68 File Offset: 0x0001EC68
		private WebUIProbeHelper.DatacenterEnvironment GetDatacenterEnvironment()
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
				if (!string.IsNullOrWhiteSpace(ipproperties.DnsSuffix))
				{
					string text = ipproperties.DnsSuffix.ToLower();
					WebUIProbeHelper.DatacenterEnvironment result;
					if (text.Contains("protection.gbl"))
					{
						result = WebUIProbeHelper.DatacenterEnvironment.FfoProduction;
					}
					else if (text.Contains("ffo.gbl"))
					{
						result = WebUIProbeHelper.DatacenterEnvironment.FfoDogfood;
					}
					else if (text.Contains("sdf.exchangelabs.com"))
					{
						result = WebUIProbeHelper.DatacenterEnvironment.ExoDogFood;
					}
					else
					{
						if (!text.Contains("prod.outlook.com") && !text.Contains("prod.exchangelabs.com"))
						{
							goto IL_8C;
						}
						result = WebUIProbeHelper.DatacenterEnvironment.ExoProduction;
					}
					return result;
				}
				IL_8C:;
			}
			return WebUIProbeHelper.DatacenterEnvironment.Test;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00020B48 File Offset: 0x0001ED48
		private WebUIProbeHelper.WorkDefinitionUser GetWorkDefinitionUser(string featureTag)
		{
			GlobalConfigSession globalConfigSession = new GlobalConfigSession();
			IEnumerable<ProbeOrganizationInfo> probeOrganizations = globalConfigSession.GetProbeOrganizations(featureTag);
			if (probeOrganizations.Count<ProbeOrganizationInfo>() == 0)
			{
				throw new InvalidOperationException(string.Format("Cannot find any tenant for feature tag \"{0}\".", featureTag));
			}
			int count = WebUIProbeHelper.random.Next(0, probeOrganizations.Count<ProbeOrganizationInfo>());
			ProbeOrganizationInfo probeOrganizationInfo = probeOrganizations.Skip(count).First<ProbeOrganizationInfo>();
			ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(null, null, CultureInfo.InvariantCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromExternalDirectoryOrganizationId(probeOrganizationInfo.ProbeOrganizationId.ObjectGuid), 219, "GetWorkDefinitionUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\WebUIProbeHelper.cs");
			IEnumerable<ADUser> enumerable = tenantRecipientSession.Find<ADUser>(null, QueryScope.SubTree, null, null, int.MaxValue);
			if (enumerable == null || !enumerable.Any<ADUser>())
			{
				throw new InvalidOperationException(string.Format("Cannot find any user for feature tag \"{0}\".", featureTag));
			}
			ADUser aduser = enumerable.FirstOrDefault((ADUser adUser) => adUser.WindowsLiveID.ToString().ToLower().StartsWith("admin@")) ?? enumerable.ElementAt(WebUIProbeHelper.random.Next(enumerable.Count<ADUser>()));
			return new WebUIProbeHelper.WorkDefinitionUser(probeOrganizationInfo.OrganizationName, aduser.WindowsLiveID.ToString(), probeOrganizationInfo.LoginPassword);
		}

		// Token: 0x0400035F RID: 863
		private const string DefaultMonitoringAccount = "admin@";

		// Token: 0x04000360 RID: 864
		private static Random random = new Random();

		// Token: 0x02000097 RID: 151
		private enum DatacenterEnvironment
		{
			// Token: 0x04000363 RID: 867
			FfoDogfood,
			// Token: 0x04000364 RID: 868
			FfoProduction,
			// Token: 0x04000365 RID: 869
			ExoDogFood,
			// Token: 0x04000366 RID: 870
			ExoProduction,
			// Token: 0x04000367 RID: 871
			Test
		}

		// Token: 0x02000098 RID: 152
		private static class ReplacementTokens
		{
			// Token: 0x04000368 RID: 872
			internal static readonly string User = "{$User}";

			// Token: 0x04000369 RID: 873
			internal static readonly string Password = "{$Password}";

			// Token: 0x0400036A RID: 874
			internal static readonly string Tenant = "{$Tenant}";

			// Token: 0x0400036B RID: 875
			internal static readonly string UmcUrl = "{$UmcUrl}";

			// Token: 0x0400036C RID: 876
			internal static readonly string FfoRwsUrl = "{$FfoRwsUrl}";

			// Token: 0x0400036D RID: 877
			internal static readonly string UccUrl = "{$UccUrl}";
		}

		// Token: 0x02000099 RID: 153
		private static class UmcUrl
		{
			// Token: 0x0400036E RID: 878
			internal static readonly string ExoTest = "exchangelabs.live-int.com/ecp/?Realm={$Tenant}";

			// Token: 0x0400036F RID: 879
			internal static readonly string ExoProduction = "www.outlook.com/ecp/?exsvurl=1&amp;Realm={$Tenant}";

			// Token: 0x04000370 RID: 880
			internal static readonly string ExoDogfood = "sdfpilot.outlook.com/ecp/?exsvurl=1&amp;Realm={$Tenant}";

			// Token: 0x04000371 RID: 881
			internal static readonly string FfoTest = "admin.o365filtering-int.com/ecp/?Realm={$Tenant}";

			// Token: 0x04000372 RID: 882
			internal static readonly string FfoProduction = "admin.protection.outlook.com/ecp/?exsvurl=1&amp;Realm={$Tenant}";

			// Token: 0x04000373 RID: 883
			internal static readonly string FfoDogfood = "admin.o365filtering.com/ecp/?exsvurl=1&amp;Realm={$Tenant}";
		}

		// Token: 0x0200009A RID: 154
		private static class UccUrl
		{
			// Token: 0x04000374 RID: 884
			internal static readonly string FfoTest = "compliance.o365filtering-int.com:446/Ucc/?Realm={$Tenant}";

			// Token: 0x04000375 RID: 885
			internal static readonly string FfoProduction = "compliance.protection.outlook.com/Ucc/?exsvurl=1&amp;Realm={$Tenant}";

			// Token: 0x04000376 RID: 886
			internal static readonly string FfoDogfood = "compliance.o365filtering.com/Ucc/?exsvurl=1&amp;Realm={$Tenant}";
		}

		// Token: 0x0200009B RID: 155
		private static class FfoRwsUrl
		{
			// Token: 0x04000377 RID: 887
			internal static readonly string ExoTest = "exchangelabs.live-int.com";

			// Token: 0x04000378 RID: 888
			internal static readonly string FfoTest = "admin.o365filtering-int.com";

			// Token: 0x04000379 RID: 889
			internal static readonly string ExoDogfood = "sdfpilot.outlook.com";

			// Token: 0x0400037A RID: 890
			internal static readonly string FfoDogfood = "admin.o365filtering.com";

			// Token: 0x0400037B RID: 891
			internal static readonly string ExoProduction = "reports.office365.com";

			// Token: 0x0400037C RID: 892
			internal static readonly string FfoProduction = "admin.protection.outlook.com";
		}

		// Token: 0x0200009C RID: 156
		private class WorkDefinitionUser
		{
			// Token: 0x0600056F RID: 1391 RVA: 0x00020D5C File Offset: 0x0001EF5C
			public WorkDefinitionUser(string tenantName, string username, SecureString password)
			{
				this.TenantName = tenantName;
				this.Username = username;
				IntPtr ptr = Marshal.SecureStringToBSTR(password);
				try
				{
					this.Password = Marshal.PtrToStringBSTR(ptr);
				}
				finally
				{
					Marshal.FreeBSTR(ptr);
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x00020DAC File Offset: 0x0001EFAC
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x00020DB4 File Offset: 0x0001EFB4
			public string TenantName { get; private set; }

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x00020DBD File Offset: 0x0001EFBD
			// (set) Token: 0x06000573 RID: 1395 RVA: 0x00020DC5 File Offset: 0x0001EFC5
			public string Username { get; private set; }

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000574 RID: 1396 RVA: 0x00020DCE File Offset: 0x0001EFCE
			// (set) Token: 0x06000575 RID: 1397 RVA: 0x00020DD6 File Offset: 0x0001EFD6
			public string Password { get; private set; }
		}
	}
}
