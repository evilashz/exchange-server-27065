using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000064 RID: 100
	internal static class TenantRelocationUtils
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00016C68 File Offset: 0x00014E68
		public static IList<UserExperienceVerificationRequest> GetRelocatingTenantsFromEndpoint()
		{
			IList<UserExperienceVerificationRequest> list = new List<UserExperienceVerificationRequest>();
			DirectoryInfo directoryInfo = new DirectoryInfo(TenantRelocationUtils.MonitoringRequestPath);
			if (directoryInfo.Exists)
			{
				foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.request"))
				{
					string name = fileInfo.Name;
					string text = name.Substring(0, name.Length - ".request".Length);
					string[] array = text.Split(new char[]
					{
						'_'
					});
					if (array.Length >= 3)
					{
						string tenantName = array[0];
						string stage = array[1];
						List<string> list2 = new List<string>();
						for (int j = 2; j < array.Length; j++)
						{
							list2.Add(array[j]);
						}
						list.Add(new UserExperienceVerificationRequest(tenantName, stage, list2));
					}
				}
			}
			return list;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00016D44 File Offset: 0x00014F44
		public static void GetUserExperienceMonitoringAccount(string tenantName, out ADUser monitoringAccount, out string password)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(tenantName);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, false, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 87, "GetUserExperienceMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Directory\\TenantRelocationUtils.cs");
			IConfigurationSession session = DirectorySessionFactory.Default.CreateTenantConfigurationSession(null, false, ConsistencyMode.FullyConsistent, sessionSettings, 95, "GetUserExperienceMonitoringAccount", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Directory\\TenantRelocationUtils.cs");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "TenantRelocationUserExperienceMonitoringUser");
			ADUser[] array = tenantOrRootOrgRecipientSession.Find<ADUser>(null, QueryScope.SubTree, filter, null, 1);
			if (array != null && array.Length > 0)
			{
				monitoringAccount = array[0];
				password = DirectoryAccessor.Instance.GeneratePasswordForMailbox(monitoringAccount);
				ILiveIdHelper liveIdHelper = TenantRelocationUtils.GetLiveIdHelper();
				liveIdHelper.ResetPassword(monitoringAccount.WindowsLiveID, monitoringAccount.NetID, session, password);
				return;
			}
			monitoringAccount = null;
			password = null;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00016DF4 File Offset: 0x00014FF4
		private static ILiveIdHelper GetLiveIdHelper()
		{
			string text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			text = Path.Combine(text, "Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Datacenter.Components.dll");
			Assembly assembly = Assembly.LoadFile(text);
			Type type = assembly.GetType("Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.Datacenter.LiveIdHelper");
			return (ILiveIdHelper)Activator.CreateInstance(type, new object[0]);
		}

		// Token: 0x04000276 RID: 630
		private const string DatacenterComponentAssemblyName = "Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Datacenter.Components.dll";

		// Token: 0x04000277 RID: 631
		private const string LiveIdHelperTypeName = "Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.Datacenter.LiveIdHelper";

		// Token: 0x04000278 RID: 632
		public static readonly string MonitoringRequestPath = Path.Combine(ConfigurationContext.Setup.InstallPath, UserExperienceMonitoringContants.MonitoringRequestPath);

		// Token: 0x04000279 RID: 633
		public static readonly string MonitoringResponsePath = Path.Combine(ConfigurationContext.Setup.InstallPath, UserExperienceMonitoringContants.MonitoringResponsePath);
	}
}
