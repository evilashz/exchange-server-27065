using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B20 RID: 2848
	[Cmdlet("Install", "ResourceHealthActiveFlags")]
	public sealed class InstallResourceHealthActiveFlags : Task
	{
		// Token: 0x06006517 RID: 25879 RVA: 0x001A5938 File Offset: 0x001A3B38
		static InstallResourceHealthActiveFlags()
		{
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.Assistants, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.DOMT, false);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.EAS, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.EWS, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.IMAP, false);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.MRS, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.OWA, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.POP, false);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.RCA, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.Transport, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.Provisioning, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.Search, true);
			InstallResourceHealthActiveFlags.dataCenterComponentMapping.Add(ResourceHealthComponent.ServiceHost, true);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping = new Dictionary<ResourceHealthComponent, bool>();
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.Assistants, true);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.DOMT, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.EAS, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.EWS, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.IMAP, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.MRS, true);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.OWA, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.POP, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.RCA, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.Transport, true);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.Provisioning, false);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.Search, true);
			InstallResourceHealthActiveFlags.enterpriseComponentMapping.Add(ResourceHealthComponent.ServiceHost, true);
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x001A5A9C File Offset: 0x001A3C9C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			bool flag = Datacenter.IsMicrosoftHostedOnly(true);
			Dictionary<ResourceHealthComponent, bool> dictionary = flag ? InstallResourceHealthActiveFlags.dataCenterComponentMapping : InstallResourceHealthActiveFlags.enterpriseComponentMapping;
			base.WriteVerbose(new LocalizedString(string.Format("Install is {0}", flag ? "Datacenter" : "Enterprise")));
			foreach (KeyValuePair<ResourceHealthComponent, bool> keyValuePair in dictionary)
			{
				bool? flag2 = this.CheckRegistryActive(keyValuePair.Key);
				if (flag2 != null)
				{
					base.WriteVerbose(new LocalizedString(string.Format("{0} was {1}.  Install suggests {2}.  However, we will not overwrite.", keyValuePair.Key, flag2.Value, keyValuePair.Value)));
				}
				else
				{
					base.WriteVerbose(new LocalizedString(string.Format("Attempting to set component {0} to {1}", keyValuePair.Key, keyValuePair.Value)));
					this.SetRegistryFlag(keyValuePair.Key, keyValuePair.Value);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x001A5BC8 File Offset: 0x001A3DC8
		internal bool? CheckRegistryActive(ResourceHealthComponent component)
		{
			bool? result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ResourceHealth", false))
				{
					if (registryKey == null)
					{
						result = null;
					}
					else
					{
						object value = registryKey.GetValue(component.ToString());
						if (value == null || !(value is int))
						{
							result = null;
						}
						else
						{
							result = new bool?((int)value != 0);
						}
					}
				}
			}
			catch (SecurityException ex)
			{
				base.WriteError(new LocalizedException(new LocalizedString(ex.Message)), ErrorCategory.PermissionDenied, null);
				result = new bool?(false);
			}
			catch (UnauthorizedAccessException ex2)
			{
				base.WriteError(new LocalizedException(new LocalizedString(ex2.Message)), ErrorCategory.PermissionDenied, null);
				result = new bool?(false);
			}
			return result;
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x001A5CB8 File Offset: 0x001A3EB8
		private void SetRegistryFlag(ResourceHealthComponent component, bool registryValue)
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ResourceHealth", true);
				if (registryKey == null)
				{
					registryKey = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ResourceHealth", RegistryKeyPermissionCheck.ReadWriteSubTree);
				}
				registryKey.SetValue(component.ToString(), registryValue ? 1 : 0, RegistryValueKind.DWord);
			}
			catch (SecurityException ex)
			{
				base.WriteError(new LocalizedException(new LocalizedString(ex.Message)), ErrorCategory.PermissionDenied, null);
			}
			catch (UnauthorizedAccessException ex2)
			{
				base.WriteError(new LocalizedException(new LocalizedString(ex2.Message)), ErrorCategory.PermissionDenied, null);
			}
			finally
			{
				if (registryKey != null)
				{
					((IDisposable)registryKey).Dispose();
				}
			}
		}

		// Token: 0x04003642 RID: 13890
		private static Dictionary<ResourceHealthComponent, bool> dataCenterComponentMapping = new Dictionary<ResourceHealthComponent, bool>();

		// Token: 0x04003643 RID: 13891
		private static Dictionary<ResourceHealthComponent, bool> enterpriseComponentMapping;
	}
}
