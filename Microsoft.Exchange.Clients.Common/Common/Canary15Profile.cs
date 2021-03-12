using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000010 RID: 16
	public class Canary15Profile
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000060BD File Offset: 0x000042BD
		public static Canary15Profile Owa
		{
			get
			{
				return Canary15Profile.owa.Value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000060C9 File Offset: 0x000042C9
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000060D1 File Offset: 0x000042D1
		public string Name { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000060DA File Offset: 0x000042DA
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000060E2 File Offset: 0x000042E2
		public string Path { get; private set; }

		// Token: 0x06000074 RID: 116 RVA: 0x000060EB File Offset: 0x000042EB
		public Canary15Profile(string name, string path)
		{
			this.Name = name;
			this.Path = path;
		}

		// Token: 0x040001FF RID: 511
		public const string OwaCanaryName = "X-OWA-CANARY";

		// Token: 0x04000200 RID: 512
		private static Lazy<Canary15Profile> owa = new Lazy<Canary15Profile>(() => new Canary15Profile("X-OWA-CANARY", "/"));
	}
}
