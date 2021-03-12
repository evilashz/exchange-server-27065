using System;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A8 RID: 424
	internal class TestIntegrationBase
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002568E File Offset: 0x0002388E
		protected TestIntegrationBase(string regKeyName, bool autoRefresh)
		{
			this.regKeyName = regKeyName;
			this.lastRefreshTimestamp = DateTime.MinValue;
			bool enabled = this.Enabled;
			if (!autoRefresh)
			{
				this.lastRefreshTimestamp = DateTime.MaxValue;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x000256C0 File Offset: 0x000238C0
		public bool Enabled
		{
			get
			{
				if (this.lastRefreshTimestamp < DateTime.UtcNow - TestIntegrationBase.AutoRefreshInterval)
				{
					this.lastRefreshTimestamp = DateTime.UtcNow;
					using (RegistryKey registryKey = this.OpenTestKey(false))
					{
						this.testIntegrationEnabled = (this.GetParamValueInt(registryKey, "TestIntegrationEnabled", 0, 1) == 1);
					}
				}
				return this.testIntegrationEnabled;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00025748 File Offset: 0x00023948
		public void ForceRefresh()
		{
			this.lastRefreshTimestamp = DateTime.MinValue;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00025758 File Offset: 0x00023958
		public int GetIntValue(string valueName, int defaultValue, int minValue, int maxValue)
		{
			if (!this.Enabled)
			{
				return defaultValue;
			}
			int result;
			using (RegistryKey registryKey = this.OpenTestKey(false))
			{
				result = (this.GetParamValueInt(registryKey, valueName, minValue, maxValue) ?? defaultValue);
			}
			return result;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000257B4 File Offset: 0x000239B4
		public int GetIntValueAndDecrement(string valueName, int defaultValue, int minValue, int maxValue)
		{
			if (!this.Enabled)
			{
				return defaultValue;
			}
			int num;
			using (RegistryKey registryKey = this.OpenTestKey(true))
			{
				num = (this.GetParamValueInt(registryKey, valueName, minValue, maxValue) ?? defaultValue);
				if (num > 0)
				{
					registryKey.SetValue(valueName, num - 1);
				}
			}
			return num;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00025824 File Offset: 0x00023A24
		public string GetStrValue(string valueName)
		{
			if (!this.Enabled)
			{
				return null;
			}
			string paramValueStr;
			using (RegistryKey registryKey = this.OpenTestKey(false))
			{
				paramValueStr = this.GetParamValueStr(registryKey, valueName);
			}
			return paramValueStr;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0002586C File Offset: 0x00023A6C
		public Guid GetGuidValue(string valueName)
		{
			string strValue = this.GetStrValue(valueName);
			Guid result;
			if (!string.IsNullOrEmpty(strValue) && Guid.TryParse(strValue, out result))
			{
				return result;
			}
			return Guid.Empty;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0002589C File Offset: 0x00023A9C
		public void Barrier(string valueKeyName, Action abortDelegate)
		{
			if (!this.Enabled)
			{
				return;
			}
			DateTime t = DateTime.UtcNow + TestIntegrationBase.MaxBarrierDelay;
			bool flag = false;
			using (RegistryKey registryKey = this.OpenTestKey(true))
			{
				try
				{
					while (DateTime.UtcNow < t)
					{
						if (!this.Enabled || this.GetParamValueInt(registryKey, valueKeyName, 0, 1) != 1)
						{
							return;
						}
						if (abortDelegate != null)
						{
							abortDelegate();
						}
						CommonUtils.CheckForServiceStopping();
						MrsTracer.Common.Debug("Waiting at breakpoint {0}", new object[]
						{
							valueKeyName
						});
						if (!flag)
						{
							registryKey.SetValue("CurrentBreakpoint", valueKeyName);
							flag = true;
						}
						Thread.Sleep(TestIntegrationBase.BarrierPollInterval);
					}
					MrsTracer.Common.Debug("Breakpoint {0} timed out, unblocking execution", new object[]
					{
						valueKeyName
					});
				}
				finally
				{
					if (flag)
					{
						registryKey.DeleteValue("CurrentBreakpoint", false);
					}
				}
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000259B0 File Offset: 0x00023BB0
		protected RegistryKey OpenTestKey(bool writable)
		{
			RegistryKey result = null;
			try
			{
				result = Registry.LocalMachine.OpenSubKey(this.regKeyName, writable);
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			return result;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000259F8 File Offset: 0x00023BF8
		protected int? GetParamValueInt(RegistryKey key, string valueName, int minValue, int maxValue)
		{
			if (key == null)
			{
				return null;
			}
			object value = key.GetValue(valueName);
			if (value == null || !(value is int))
			{
				return null;
			}
			int num = (int)value;
			if (num < minValue)
			{
				num = minValue;
			}
			else if (num > maxValue)
			{
				num = maxValue;
			}
			return new int?(num);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00025A4C File Offset: 0x00023C4C
		protected bool GetFlagValue(string flagName)
		{
			return this.GetFlagValue(flagName, false);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00025A58 File Offset: 0x00023C58
		protected bool GetFlagValue(string flagName, bool defaultValue)
		{
			if (!this.Enabled)
			{
				return defaultValue;
			}
			bool result;
			using (RegistryKey registryKey = this.OpenTestKey(false))
			{
				int? paramValueInt = this.GetParamValueInt(registryKey, flagName, 0, 1);
				if (paramValueInt == null)
				{
					result = defaultValue;
				}
				else
				{
					result = (paramValueInt.Value != 0);
				}
			}
			return result;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00025ABC File Offset: 0x00023CBC
		private string GetParamValueStr(RegistryKey key, string valueName)
		{
			if (key == null)
			{
				return null;
			}
			object value = key.GetValue(valueName);
			if (value == null || !(value is string))
			{
				return null;
			}
			return (string)value;
		}

		// Token: 0x040008F5 RID: 2293
		public const string TestIntegrationEnabledName = "TestIntegrationEnabled";

		// Token: 0x040008F6 RID: 2294
		public const string CurrentBreakpointName = "CurrentBreakpoint";

		// Token: 0x040008F7 RID: 2295
		private static readonly TimeSpan MaxBarrierDelay = TimeSpan.FromHours(1.0);

		// Token: 0x040008F8 RID: 2296
		private static readonly TimeSpan BarrierPollInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x040008F9 RID: 2297
		private static readonly TimeSpan AutoRefreshInterval = TimeSpan.FromSeconds(60.0);

		// Token: 0x040008FA RID: 2298
		private readonly string regKeyName;

		// Token: 0x040008FB RID: 2299
		private bool testIntegrationEnabled;

		// Token: 0x040008FC RID: 2300
		private DateTime lastRefreshTimestamp;
	}
}
