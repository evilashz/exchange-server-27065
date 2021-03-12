using System;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000009 RID: 9
	internal class TargetServerConfig
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002900 File Offset: 0x00000B00
		public TargetServerConfig(string name, string host, int port)
		{
			this.name = name;
			this.host = host;
			this.port = port;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000291D File Offset: 0x00000B1D
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002925 File Offset: 0x00000B25
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000292D File Offset: 0x00000B2D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002935 File Offset: 0x00000B35
		public virtual string ShortHostName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000014 RID: 20
		private readonly string host;

		// Token: 0x04000015 RID: 21
		private readonly string name;

		// Token: 0x04000016 RID: 22
		private readonly int port;
	}
}
