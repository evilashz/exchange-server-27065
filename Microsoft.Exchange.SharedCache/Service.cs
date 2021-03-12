using System;
using System.Configuration;
using System.ServiceProcess;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.SharedCache.EventLog;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x0200001E RID: 30
	public sealed class Service : ExServiceBase, IDisposable
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004792 File Offset: 0x00002992
		public Service()
		{
			ExWatson.Register();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000047A0 File Offset: 0x000029A0
		public static void Main(string[] args)
		{
			using (Service service = new Service())
			{
				int num = Privileges.RemoveAllExcept(Service.requiredPrivileges);
				if (num != 0)
				{
					Environment.Exit(num);
				}
				bool flag = false;
				foreach (string text in args)
				{
					string a = text.ToLower();
					if (!(a == "-console"))
					{
						Console.WriteLine("invalid options\n");
						return;
					}
					flag = true;
				}
				if (flag)
				{
					ExServiceBase.RunAsConsole(service);
				}
				else
				{
					ServiceBase.Run(service);
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000483C File Offset: 0x00002A3C
		protected override void OnStartInternal(string[] args)
		{
			try
			{
				if (Globals.InstanceType == InstanceType.NotInitialized)
				{
					Globals.InitializeSinglePerfCounterInstance();
				}
				SettingOverrideSync.Instance.Start(true);
				bool flag = SharedCacheServer.Start();
				if (flag)
				{
					CacheSettingsCollection cacheSettingsCollection = this.GetCacheSettingsCollection();
					foreach (object obj in cacheSettingsCollection)
					{
						CacheSettings cacheSettings = (CacheSettings)obj;
						string[] array = cacheSettings.ServerRole.Split(new char[]
						{
							','
						});
						bool flag2 = false;
						foreach (string a in array)
						{
							flag2 = (flag2 || a == "All" || (a == "FE" && Service.CheckRoleInstalled("CafeRole")) || (a == "BE" && Service.CheckRoleInstalled("MailboxRole")));
						}
						if (flag2)
						{
							SharedCacheServer.RegisterCache(cacheSettings.Guid, this.CreateCacheForType(cacheSettings));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Diagnostics.ReportException(ex, MSExchangeSharedCacheEventLogConstants.Tuple_UnhandledException, true, this, "Exception during OnStartInternal: {0}");
				throw;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000049A8 File Offset: 0x00002BA8
		protected override void OnStopInternal()
		{
			if (SharedCacheServer.IsRunning)
			{
				SharedCacheServer.Stop();
			}
			SettingOverrideSync.Instance.Stop();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000049C0 File Offset: 0x00002BC0
		private static bool CheckRoleInstalled(string roleName)
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + roleName))
				{
					result = (registryKey != null);
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004A1C File Offset: 0x00002C1C
		private ISharedCache CreateCacheForType(CacheSettings settings)
		{
			string type;
			if ((type = settings.Type) != null)
			{
				if (type == "SharedTimeoutCache")
				{
					return new SharedTimeoutCache(settings);
				}
				if (type == "PersistentDatabase")
				{
					return new PersistentDatabase(settings);
				}
			}
			throw new Exception("Unrecognised cache type: " + settings.Type);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004A74 File Offset: 0x00002C74
		private CacheSettingsCollection GetCacheSettingsCollection()
		{
			ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			CachesSection cachesSection = ConfigurationManager.GetSection("cachesSection") as CachesSection;
			if (cachesSection == null)
			{
				throw new Exception("Config section is null. Config not loaded correctly?");
			}
			CacheSettingsCollection cachesElement = cachesSection.CachesElement;
			foreach (object obj in cachesElement)
			{
				CacheSettings cacheSettings = (CacheSettings)obj;
				if (cacheSettings.Name == "AnchorMailboxCache" && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).SharedCache.UsePersistenceForCafe.Enabled)
				{
					cacheSettings.Type = "PersistentDatabase";
				}
			}
			return cachesElement;
		}

		// Token: 0x0400005B RID: 91
		private static readonly string[] requiredPrivileges = new string[]
		{
			"SeAuditPrivilege",
			"SeChangeNotifyPrivilege",
			"SeCreateGlobalPrivilege"
		};
	}
}
