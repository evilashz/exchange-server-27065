using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000C1 RID: 193
	internal class RealRegistry : RegistryManipulator
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x000266DC File Offset: 0x000248DC
		public RealRegistry(string root, SafeHandle handle) : base(root, handle)
		{
			bool flag = true;
			string apiName = string.Empty;
			try
			{
				apiName = "OpenSubKey";
				this.rootKey = Registry.LocalMachine.OpenSubKey(root, true);
				if (this.rootKey == null)
				{
					apiName = "CreateSubKey";
					this.rootKey = Registry.LocalMachine.CreateSubKey(root);
				}
				flag = false;
			}
			catch (SecurityException innerException)
			{
				throw new AmRegistryException(apiName, innerException);
			}
			catch (IOException innerException2)
			{
				throw new AmRegistryException(apiName, innerException2);
			}
			finally
			{
				if (flag)
				{
					base.SuppressDisposeTracker();
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002677C File Offset: 0x0002497C
		public RegistryKey RootKey
		{
			get
			{
				return this.rootKey;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00026784 File Offset: 0x00024984
		public override string[] GetSubKeyNames(string keyName)
		{
			string[] array = null;
			try
			{
				using (RegistryKey registryKey = this.OpenKey(keyName))
				{
					if (registryKey != null)
					{
						array = registryKey.GetSubKeyNames();
					}
				}
			}
			catch (SecurityException innerException)
			{
				throw new AmRegistryException("GetSubKeyNames", innerException);
			}
			catch (UnauthorizedAccessException innerException2)
			{
				throw new AmRegistryException("GetSubKeyNames", innerException2);
			}
			catch (IOException innerException3)
			{
				throw new AmRegistryException("GetSubKeyNames", innerException3);
			}
			return array ?? new string[0];
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002681C File Offset: 0x00024A1C
		public override string[] GetValueNames(string keyName)
		{
			string[] array = null;
			try
			{
				using (RegistryKey registryKey = this.OpenKey(keyName))
				{
					if (registryKey != null)
					{
						array = registryKey.GetValueNames();
					}
				}
			}
			catch (SecurityException innerException)
			{
				throw new AmRegistryException("GetValueNames", innerException);
			}
			catch (UnauthorizedAccessException innerException2)
			{
				throw new AmRegistryException("GetValueNames", innerException2);
			}
			catch (IOException innerException3)
			{
				throw new AmRegistryException("GetValueNames", innerException3);
			}
			return array ?? new string[0];
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000268B4 File Offset: 0x00024AB4
		public override void SetValue(string keyName, RegistryValue value)
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = this.OpenKey(keyName, true);
				if (registryKey == null)
				{
					this.CreateKey(keyName);
					registryKey = this.OpenKey(keyName, true);
				}
				registryKey.SetValue(value.Name, value.Value, value.Kind);
			}
			catch (UnauthorizedAccessException innerException)
			{
				throw new AmRegistryException("SetValue", innerException);
			}
			catch (SecurityException innerException2)
			{
				throw new AmRegistryException("SetValue", innerException2);
			}
			catch (IOException innerException3)
			{
				throw new AmRegistryException("SetValue", innerException3);
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00026960 File Offset: 0x00024B60
		public override RegistryValue GetValue(string keyName, string valueName)
		{
			RegistryValue result = null;
			try
			{
				using (RegistryKey registryKey = this.OpenKey(keyName))
				{
					if (registryKey != null)
					{
						result = new RegistryValue(valueName, registryKey.GetValue(valueName), registryKey.GetValueKind(valueName));
					}
				}
			}
			catch (UnauthorizedAccessException innerException)
			{
				throw new AmRegistryException("GetValue", innerException);
			}
			catch (SecurityException innerException2)
			{
				throw new AmRegistryException("GetValue", innerException2);
			}
			catch (IOException innerException3)
			{
				throw new AmRegistryException("GetValue", innerException3);
			}
			return result;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000269FC File Offset: 0x00024BFC
		public override void DeleteValue(string keyName, string valueName)
		{
			try
			{
				using (RegistryKey registryKey = this.OpenKey(keyName, true))
				{
					if (registryKey != null)
					{
						registryKey.DeleteValue(valueName);
					}
				}
			}
			catch (UnauthorizedAccessException innerException)
			{
				throw new AmRegistryException("DeleteValue", innerException);
			}
			catch (SecurityException innerException2)
			{
				throw new AmRegistryException("DeleteValue", innerException2);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00026A6C File Offset: 0x00024C6C
		public override void DeleteKey(string keyName)
		{
			try
			{
				this.rootKey.DeleteSubKeyTree(keyName);
			}
			catch (ArgumentException)
			{
				using (this.OpenKey(keyName))
				{
				}
			}
			catch (UnauthorizedAccessException innerException)
			{
				throw new AmRegistryException("DeleteSubKeyTree", innerException);
			}
			catch (SecurityException innerException2)
			{
				throw new AmRegistryException("DeleteSubKeyTree", innerException2);
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00026AEC File Offset: 0x00024CEC
		public override void CreateKey(string keyName)
		{
			try
			{
				using (this.rootKey.CreateSubKey(keyName))
				{
				}
			}
			catch (SecurityException innerException)
			{
				throw new AmRegistryException("CreateSubKey", innerException);
			}
			catch (IOException innerException2)
			{
				throw new AmRegistryException("CreateSubKey", innerException2);
			}
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00026B58 File Offset: 0x00024D58
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RealRegistry>(this);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00026B60 File Offset: 0x00024D60
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00026B6F File Offset: 0x00024D6F
		private RegistryKey OpenKey(string keyName)
		{
			return this.OpenKey(keyName, false);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00026B7C File Offset: 0x00024D7C
		private RegistryKey OpenKey(string keyName, bool writable)
		{
			RegistryKey result;
			try
			{
				result = this.rootKey.OpenSubKey(keyName, writable);
			}
			catch (SecurityException innerException)
			{
				throw new AmRegistryException("OpenSubKey", innerException);
			}
			return result;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00026BB8 File Offset: 0x00024DB8
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.rootKey.Close();
				this.Handle.Close();
			}
			base.Dispose();
		}

		// Token: 0x04000380 RID: 896
		private RegistryKey rootKey;
	}
}
