using System;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Common.Extensions
{
	// Token: 0x02000004 RID: 4
	internal class RegistryWriter : IRegistryWriter
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002184 File Offset: 0x00000384
		private RegistryWriter()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002197 File Offset: 0x00000397
		public static RegistryWriter Instance
		{
			get
			{
				return RegistryWriter.instance;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000219E File Offset: 0x0000039E
		public void SetValue(RegistryKey baseKey, string subkeyName, string valueName, object value, RegistryValueKind valueKind)
		{
			this.registryWriter.SetValue(baseKey, subkeyName, valueName, value, valueKind);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021B2 File Offset: 0x000003B2
		public void SetValue(RegistryKey baseKey, string subkeyName, string valueName, object value)
		{
			this.SetValue(baseKey, subkeyName, valueName, value, RegistryValueKind.String);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021C0 File Offset: 0x000003C0
		public void DeleteValue(RegistryKey baseKey, string subkeyName, string valueName)
		{
			this.registryWriter.DeleteValue(baseKey, subkeyName, valueName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D0 File Offset: 0x000003D0
		public void CreateSubKey(RegistryKey baseKey, string subkeyName)
		{
			this.registryWriter.CreateSubKey(baseKey, subkeyName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021DF File Offset: 0x000003DF
		public void DeleteSubKeyTree(RegistryKey baseKey, string subkeyName)
		{
			this.registryWriter.DeleteSubKeyTree(baseKey, subkeyName);
		}

		// Token: 0x04000004 RID: 4
		private static RegistryWriter instance = new RegistryWriter();

		// Token: 0x04000005 RID: 5
		private IRegistryWriter registryWriter = RegistryWriter.Instance;
	}
}
