using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000C6 RID: 198
	internal class AmFaultInjectHelper
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0002706A File Offset: 0x0002526A
		internal AmFaultInjectHelper()
		{
			this.Init();
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00027078 File Offset: 0x00025278
		internal bool IsEnabled
		{
			get
			{
				return this.m_isEnabled && !this.IsTempDisabled;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00027090 File Offset: 0x00025290
		internal bool IsTempDisabled
		{
			get
			{
				bool isTempDisabled = this.m_isTempDisabled;
				int num = 0;
				this.ReadProperty<int>("Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject", "TempDisabled", out num, 0);
				this.m_isTempDisabled = (num > 0);
				if (isTempDisabled != this.m_isTempDisabled)
				{
					if (this.m_isTempDisabled)
					{
						AmTrace.Debug("**** AM fault injector is temporarily disabled ****", new object[0]);
					}
					else
					{
						AmTrace.Debug("**** AM fault injector is re-enabled ****", new object[0]);
					}
				}
				return this.m_isTempDisabled;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00027100 File Offset: 0x00025300
		internal void Init()
		{
			int num = 0;
			this.ReadProperty<int>("Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject", "Enabled", out num, 0);
			this.m_isEnabled = (num > 0);
			if (this.m_isEnabled)
			{
				AmTrace.Debug("**** AM fault injector is enabled ****", new object[0]);
				if (this.IsTempDisabled)
				{
					AmTrace.Debug("**** But it is temporarily disabled by the TempDisabled setting ****", new object[0]);
				}
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002715D File Offset: 0x0002535D
		internal void SleepIfRequired(string propertyName)
		{
			this.SleepIfRequired(null, propertyName);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00027167 File Offset: 0x00025367
		internal void SleepIfRequired(Guid dbGuid, string propertyName)
		{
			this.SleepIfRequired(dbGuid.ToString(), propertyName);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00027180 File Offset: 0x00025380
		internal void SleepIfRequired(string subKeyName, string propertyName)
		{
			if (!this.IsEnabled)
			{
				return;
			}
			string text = "Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject";
			if (!string.IsNullOrEmpty(subKeyName))
			{
				text = text + "\\" + subKeyName;
			}
			int num = 0;
			int num2 = 0;
			while (!this.IsTempDisabled)
			{
				bool flag = this.ReadProperty<int>(text, propertyName, out num2, 0);
				if (!flag && !string.IsNullOrEmpty(subKeyName))
				{
					flag = this.ReadProperty<int>("Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject", propertyName, out num2, 0);
				}
				if (!flag || num >= num2)
				{
					break;
				}
				if (num % 30 == 0)
				{
					if (num == 0)
					{
						AmTrace.Debug("Sleep induced at:", new object[0]);
						AmTrace.Debug(new StackTrace(true).ToString(), new object[0]);
						AmTrace.Debug("Starting to sleeping for {0}\\{1}: (elasped={2}, max={3})", new object[]
						{
							subKeyName,
							propertyName,
							num,
							num2
						});
					}
					else
					{
						AmTrace.Debug("Sleeping for {0}\\{1}: (elasped={2}, max={3})", new object[]
						{
							subKeyName,
							propertyName,
							num,
							num2
						});
					}
				}
				Thread.Sleep(1000);
				num++;
			}
			if (num > 0)
			{
				AmTrace.Debug("Finished sleeping for {0}\\{1}: (elasped={2}, max={3})", new object[]
				{
					subKeyName,
					propertyName,
					num,
					num2
				});
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000272D8 File Offset: 0x000254D8
		internal void GenerateMapiExceptionIfRequired(Guid dbGuid, AmServerName serverName)
		{
			if (!this.IsEnabled)
			{
				return;
			}
			int num = 0;
			this.ReadDbOperationProperty<int>(dbGuid, serverName, "GenerateMapiError", out num, 0);
			if (num == 0)
			{
				return;
			}
			AmTrace.Debug("AmInject: GenerateMapiError enabled for {0},{1}", new object[]
			{
				dbGuid,
				AmServerName.IsNullOrEmpty(serverName) ? "<null>" : serverName.NetbiosName
			});
			int num2 = 0;
			int num3 = 0;
			bool flag = this.ReadDbOperationProperty<int>(dbGuid, serverName, "MapiHResult", out num2, 0);
			bool flag2 = this.ReadDbOperationProperty<int>(dbGuid, serverName, "MapiLowLevelError", out num3, 0);
			if (flag && flag2)
			{
				AmTrace.Debug("AmInject: Generating mapi exception (hr={0}, ec={1})", new object[]
				{
					num2,
					num3
				});
				MapiExceptionHelper.ThrowIfError(string.Format("Database operation failed with Mapi error. (hr={0}, ec={1})", num2, num3), num2, num3);
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000273B0 File Offset: 0x000255B0
		internal bool ReadDbOperationProperty<T>(Guid dbGuid, AmServerName serverName, string propertyName, out T foundValue, T defaultValue)
		{
			string str = "Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject";
			string text = str + "\\" + dbGuid.ToString();
			string text2 = string.Empty;
			if (!AmServerName.IsNullOrEmpty(serverName))
			{
				text2 = text + "\\" + serverName.NetbiosName;
			}
			bool flag = false;
			foundValue = defaultValue;
			if (!string.IsNullOrEmpty(text2))
			{
				flag = this.ReadProperty<T>(text2, propertyName, out foundValue, defaultValue);
			}
			if (!flag)
			{
				flag = this.ReadProperty<T>(text, propertyName, out foundValue, defaultValue);
			}
			if (!flag)
			{
				flag = this.ReadProperty<T>("Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject", propertyName, out foundValue, defaultValue);
			}
			return flag;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00027444 File Offset: 0x00025644
		private bool ReadProperty<T>(string keyName, string propertyName, out T foundValue, T defaultValue)
		{
			bool result = false;
			foundValue = defaultValue;
			Exception ex = null;
			using (IRegistryKey registryKey = SharedDependencies.RegistryKeyProvider.TryOpenKey(keyName, ref ex))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue(propertyName, defaultValue);
					if (value != null)
					{
						result = true;
						foundValue = (T)((object)value);
					}
				}
			}
			return result;
		}

		// Token: 0x0400038A RID: 906
		private const string RootKey = "Software\\Microsoft\\Exchange\\ActiveManager\\FaultInject";

		// Token: 0x0400038B RID: 907
		private const string EnabledProperty = "Enabled";

		// Token: 0x0400038C RID: 908
		private const string TempDisabledProperty = "TempDisabled";

		// Token: 0x0400038D RID: 909
		private const string GenerateMapiErrorProperty = "GenerateMapiError";

		// Token: 0x0400038E RID: 910
		private const string MapiHResultProperty = "MapiHResult";

		// Token: 0x0400038F RID: 911
		private const string MapiLowLevelErorProperty = "MapiLowLevelError";

		// Token: 0x04000390 RID: 912
		private bool m_isEnabled;

		// Token: 0x04000391 RID: 913
		private bool m_isTempDisabled;
	}
}
