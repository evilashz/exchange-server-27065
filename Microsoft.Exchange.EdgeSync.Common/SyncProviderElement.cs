using System;
using System.Configuration;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000019 RID: 25
	internal class SyncProviderElement : ConfigurationElement
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000339C File Offset: 0x0000159C
		static SyncProviderElement()
		{
			SyncProviderElement.properties.Add(SyncProviderElement.propName);
			SyncProviderElement.properties.Add(SyncProviderElement.propSynchronizationProvider);
			SyncProviderElement.properties.Add(SyncProviderElement.propAssemblyPath);
			SyncProviderElement.properties.Add(SyncProviderElement.propEnabled);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003472 File Offset: 0x00001672
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base[SyncProviderElement.propName];
			}
			set
			{
				base[SyncProviderElement.propName] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003480 File Offset: 0x00001680
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003492 File Offset: 0x00001692
		[ConfigurationProperty("synchronizationProvider")]
		public string SynchronizationProvider
		{
			get
			{
				return (string)base[SyncProviderElement.propSynchronizationProvider];
			}
			set
			{
				base[SyncProviderElement.propSynchronizationProvider] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000034A0 File Offset: 0x000016A0
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000034B2 File Offset: 0x000016B2
		[ConfigurationProperty("assemblyPath")]
		public string AssemblyPath
		{
			get
			{
				return (string)base[SyncProviderElement.propAssemblyPath];
			}
			set
			{
				base[SyncProviderElement.propAssemblyPath] = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000034C0 File Offset: 0x000016C0
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000034D2 File Offset: 0x000016D2
		[ConfigurationProperty("enabled")]
		public bool Enabled
		{
			get
			{
				return (bool)base[SyncProviderElement.propEnabled];
			}
			set
			{
				base[SyncProviderElement.propEnabled] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000034E5 File Offset: 0x000016E5
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SyncProviderElement.properties;
			}
		}

		// Token: 0x0400004D RID: 77
		private static readonly ConfigurationProperty propName = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400004E RID: 78
		private static readonly ConfigurationProperty propSynchronizationProvider = new ConfigurationProperty("synchronizationProvider", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x0400004F RID: 79
		private static readonly ConfigurationProperty propAssemblyPath = new ConfigurationProperty("assemblyPath", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000050 RID: 80
		private static readonly ConfigurationProperty propEnabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04000051 RID: 81
		private static readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
