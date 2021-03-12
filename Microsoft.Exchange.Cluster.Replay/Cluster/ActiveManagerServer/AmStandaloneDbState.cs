using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000061 RID: 97
	internal class AmStandaloneDbState : AmDbState
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00016412 File Offset: 0x00014612
		internal AmStandaloneDbState()
		{
			this.InitializeHandles();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016420 File Offset: 0x00014620
		protected override void InitializeHandles()
		{
			RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveManager");
			this.m_regDbHandle = registryKey.CreateSubKey("DbState");
			this.m_dbgOptionHandle = registryKey.CreateSubKey("DebugOption");
			registryKey.Close();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00016467 File Offset: 0x00014667
		protected override void CloseHandles()
		{
			if (this.m_regDbHandle != null)
			{
				this.m_regDbHandle.Close();
				this.m_regDbHandle = null;
			}
			if (this.m_dbgOptionHandle != null)
			{
				this.m_dbgOptionHandle.Close();
				this.m_dbgOptionHandle = null;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000164A0 File Offset: 0x000146A0
		protected override void WriteInternal(string guidStr, string stateInfoStr, AmServerName activeServerName)
		{
			try
			{
				this.m_regDbHandle.SetValue(guidStr, stateInfoStr);
				this.m_regDbHandle.Flush();
			}
			catch (IOException ex)
			{
				int hrforException = Marshal.GetHRForException(ex);
				AmTrace.Error("WriteInternal({0}, {1}): m_regDbHandle.SetValue failed with error {2} (hr={3})", new object[]
				{
					guidStr,
					stateInfoStr,
					ex.Message,
					hrforException
				});
				if (hrforException != 1018)
				{
					throw new AmRegistryException("m_regDbHandle.SetValue", ex);
				}
				throw;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00016524 File Offset: 0x00014724
		protected override Guid[] ReadDatabaseGuids(bool isBestEffort)
		{
			Guid[] result = null;
			try
			{
				string[] subKeyNames = this.m_regDbHandle.GetSubKeyNames();
				result = base.ConvertGuidStringsToGuids(subKeyNames);
			}
			catch (IOException ex)
			{
				int hrforException = Marshal.GetHRForException(ex);
				AmTrace.Error("ReadDatabaseGuids({0}): m_regDbHandle.GetSubKeyNames failed with error {1} (hr={2})", new object[]
				{
					isBestEffort,
					ex.Message,
					hrforException
				});
				if (!isBestEffort)
				{
					throw new AmRegistryException("m_regDbHandle.GetSubKeyNames", ex);
				}
			}
			return result;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000165A8 File Offset: 0x000147A8
		protected override AmDbStateInfo[] ReadAllInternal(bool isBestEffort)
		{
			AmDbStateInfo[] array = null;
			Guid[] array2 = this.ReadDatabaseGuids(isBestEffort);
			if (array2 != null)
			{
				array = new AmDbStateInfo[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array[i] = base.Read(array2[i], isBestEffort);
				}
			}
			return array;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000165F0 File Offset: 0x000147F0
		protected override bool ReadInternal(string guidStr, out string stateInfoStr)
		{
			bool result = false;
			object obj = null;
			try
			{
				obj = this.m_regDbHandle.GetValue(guidStr);
				if (obj == null)
				{
					AmTrace.Info("Subkeys count = {0}", new object[]
					{
						this.m_regDbHandle.SubKeyCount
					});
				}
			}
			catch (IOException ex)
			{
				int hrforException = Marshal.GetHRForException(ex);
				AmTrace.Error("ReadInternal({0}): m_regDbHandle.GetValue failed with error {1} (hr={2})", new object[]
				{
					guidStr,
					ex.Message,
					hrforException
				});
				if (hrforException != 1018)
				{
					throw new AmRegistryException("m_regDbHandle.GetValue", ex);
				}
				throw;
			}
			if (obj != null)
			{
				stateInfoStr = (string)obj;
				result = true;
			}
			else
			{
				stateInfoStr = null;
			}
			return result;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000166AC File Offset: 0x000148AC
		protected override void DeleteInternal(string guidStr)
		{
			try
			{
				this.m_regDbHandle.DeleteValue(guidStr);
			}
			catch (ArgumentException ex)
			{
				AmTrace.Error("DeleteInternal({0}): m_regDbHandle.DeleteValue failed with error {1}", new object[]
				{
					guidStr,
					ex.Message
				});
			}
			catch (IOException ex2)
			{
				AmTrace.Error("DeleteInternal({0}): m_regDbHandle.DeleteValue failed with error {1}", new object[]
				{
					guidStr,
					ex2.Message
				});
				throw new AmRegistryException("m_regDbHandle.DeleteValue", ex2);
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00016734 File Offset: 0x00014934
		protected override void SetLastLogPropertyInternal(string name, string value)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00016736 File Offset: 0x00014936
		protected override bool GetLastLogPropertyInternal(string name, out string value)
		{
			value = string.Empty;
			return false;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00016740 File Offset: 0x00014940
		protected override T GetDebugOptionInternal<T>(string serverName, string propertyName, T defaultValue, out bool doesValueExist)
		{
			T result = defaultValue;
			doesValueExist = false;
			try
			{
				if (serverName == null)
				{
					object value = this.m_dbgOptionHandle.GetValue(propertyName, defaultValue);
					if (value != null)
					{
						result = (T)((object)value);
						doesValueExist = true;
					}
				}
				else
				{
					using (RegistryKey registryKey = this.m_dbgOptionHandle.OpenSubKey(serverName))
					{
						if (registryKey != null)
						{
							object value2 = registryKey.GetValue(propertyName, defaultValue);
							if (value2 != null)
							{
								result = (T)((object)value2);
								doesValueExist = true;
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				AmTrace.Error("GetDebugOptionInternal({0}, {1}): m_dbgOptionHandle.GetValue or m_dbgOptionHandle.OpenSubKey failed with error {2}", new object[]
				{
					serverName,
					propertyName,
					ex.Message
				});
				throw new AmRegistryException("m_dbgOptionHandle.GetValue or m_dbgOptionHandle.OpenSubKey", ex);
			}
			return result;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00016808 File Offset: 0x00014A08
		protected override bool SetDebugOptionInternal<T>(string serverName, string propertyName, T propertyValue)
		{
			try
			{
				if (serverName == null)
				{
					this.m_dbgOptionHandle.SetValue(propertyName, propertyValue);
				}
				else
				{
					using (RegistryKey registryKey = this.m_dbgOptionHandle.CreateSubKey(serverName))
					{
						if (registryKey != null)
						{
							registryKey.SetValue(propertyName, propertyValue);
						}
					}
				}
			}
			catch (IOException ex)
			{
				AmTrace.Error("SetDebugOptionInternal({0}, {1}): m_dbgOptionHandle.SetValue or m_dbgOptionHandle.CreateSubKey failed with error {2}", new object[]
				{
					serverName,
					propertyName,
					ex.Message
				});
				throw new AmRegistryException("m_dbgOptionHandle.SetValue or m_dbgOptionHandle.CreateSubKey", ex);
			}
			return true;
		}

		// Token: 0x040001D7 RID: 471
		private const string ExchangeRootKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040001D8 RID: 472
		private const string AmRootKeyName = "ActiveManager";

		// Token: 0x040001D9 RID: 473
		private RegistryKey m_regDbHandle;

		// Token: 0x040001DA RID: 474
		private RegistryKey m_dbgOptionHandle;
	}
}
