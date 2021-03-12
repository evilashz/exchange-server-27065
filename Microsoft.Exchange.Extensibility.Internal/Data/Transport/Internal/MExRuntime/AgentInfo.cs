using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000072 RID: 114
	internal sealed class AgentInfo
	{
		// Token: 0x06000371 RID: 881 RVA: 0x00011BDC File Offset: 0x0000FDDC
		public AgentInfo(string agent, string type, string factory, string path, bool enabled, bool isInternal)
		{
			this.agentName = agent;
			this.baseTypeName = type;
			this.factoryTypeName = factory;
			this.factoryAssemblyPath = path;
			this.enabled = enabled;
			this.isInternal = isInternal;
			this.id = this.agentName.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00011C32 File Offset: 0x0000FE32
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00011C3A File Offset: 0x0000FE3A
		public string AgentName
		{
			get
			{
				return this.agentName;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00011C42 File Offset: 0x0000FE42
		public string BaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00011C4A File Offset: 0x0000FE4A
		public string FactoryTypeName
		{
			get
			{
				return this.factoryTypeName;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00011C52 File Offset: 0x0000FE52
		public string FactoryAssemblyPath
		{
			get
			{
				return this.factoryAssemblyPath;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00011C5A File Offset: 0x0000FE5A
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00011C62 File Offset: 0x0000FE62
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00011C6B File Offset: 0x0000FE6B
		public bool IsInternal
		{
			get
			{
				return this.isInternal;
			}
		}

		// Token: 0x04000451 RID: 1105
		private readonly string id;

		// Token: 0x04000452 RID: 1106
		private readonly string agentName;

		// Token: 0x04000453 RID: 1107
		private readonly string baseTypeName;

		// Token: 0x04000454 RID: 1108
		private readonly string factoryTypeName;

		// Token: 0x04000455 RID: 1109
		private readonly string factoryAssemblyPath;

		// Token: 0x04000456 RID: 1110
		private bool isInternal;

		// Token: 0x04000457 RID: 1111
		private bool enabled;
	}
}
