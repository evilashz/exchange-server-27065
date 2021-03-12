using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004D1 RID: 1233
	[Serializable]
	internal sealed class VirtualDirectoryPathExistsCondition : Condition
	{
		// Token: 0x06002AC4 RID: 10948 RVA: 0x000AB59C File Offset: 0x000A979C
		public VirtualDirectoryPathExistsCondition(string serverName, string path)
		{
			this.ServerName = serverName;
			this.Path = path;
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000AB5B2 File Offset: 0x000A97B2
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000AB5BA File Offset: 0x000A97BA
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
			set
			{
				this.serverName = value;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x000AB5C3 File Offset: 0x000A97C3
		// (set) Token: 0x06002AC8 RID: 10952 RVA: 0x000AB5CB File Offset: 0x000A97CB
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000AB5D4 File Offset: 0x000A97D4
		public override bool Verify()
		{
			return (this.Path.Length >= "\\\\.\\".Length && this.Path.StartsWith("\\\\.\\")) || WmiWrapper.IsDirectoryExisting(this.ServerName, this.Path);
		}

		// Token: 0x04001FE8 RID: 8168
		private const string uncPrefix = "\\\\.\\";

		// Token: 0x04001FE9 RID: 8169
		private string serverName;

		// Token: 0x04001FEA RID: 8170
		private string path;
	}
}
