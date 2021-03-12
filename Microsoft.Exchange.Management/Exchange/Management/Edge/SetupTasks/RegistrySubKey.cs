using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002FA RID: 762
	internal sealed class RegistrySubKey
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x0007407F File Offset: 0x0007227F
		public RegistrySubKey(RegistryKey hiveKey, string subkeyPath)
		{
			this.hiveKey = hiveKey;
			this.subkeyPath = subkeyPath;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00074095 File Offset: 0x00072295
		private RegistrySubKey()
		{
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0007409D File Offset: 0x0007229D
		public RegistryKey HiveKey
		{
			get
			{
				return this.hiveKey;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000740A5 File Offset: 0x000722A5
		public string SubkeyPath
		{
			get
			{
				return this.subkeyPath;
			}
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000740AD File Offset: 0x000722AD
		public RegistryKey Open()
		{
			return this.hiveKey.OpenSubKey(this.subkeyPath);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000740C0 File Offset: 0x000722C0
		public RegistryKey Open(bool writable)
		{
			return this.hiveKey.OpenSubKey(this.subkeyPath, writable);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000740D4 File Offset: 0x000722D4
		public RegistryKey Create()
		{
			return this.hiveKey.CreateSubKey(this.subkeyPath);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000740E7 File Offset: 0x000722E7
		public void DeleteTreeIfExist()
		{
			Utils.DeleteRegSubKeyTreeIfExist(this.hiveKey, this.subkeyPath);
		}

		// Token: 0x04000B65 RID: 2917
		private RegistryKey hiveKey;

		// Token: 0x04000B66 RID: 2918
		private readonly string subkeyPath;
	}
}
