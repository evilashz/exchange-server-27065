using System;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000015 RID: 21
	internal struct SynchronizationProviderInfo
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002BEB File Offset: 0x00000DEB
		public SynchronizationProviderInfo(string name, string assemblyPath, string synchronizationProvider, bool enabled)
		{
			this.name = name;
			this.assemblyPath = assemblyPath;
			this.synchronizationProvider = synchronizationProvider;
			this.enabled = enabled;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002C0A File Offset: 0x00000E0A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002C12 File Offset: 0x00000E12
		public string AssemblyPath
		{
			get
			{
				return this.assemblyPath;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002C1A File Offset: 0x00000E1A
		public string SynchronizationProvider
		{
			get
			{
				return this.synchronizationProvider;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002C22 File Offset: 0x00000E22
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x04000034 RID: 52
		private string name;

		// Token: 0x04000035 RID: 53
		private string assemblyPath;

		// Token: 0x04000036 RID: 54
		private string synchronizationProvider;

		// Token: 0x04000037 RID: 55
		private bool enabled;
	}
}
