using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000AF RID: 175
	[DataContract(Name = "ServerVersionInfo", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class ServerVersionInfo
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x00017F33 File Offset: 0x00016133
		public ServerVersionInfo()
		{
			this.Version = ExchangeVersion.Exchange2013_SP1.ToString();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00017F4C File Offset: 0x0001614C
		private ServerVersionInfo(FileVersionInfo version) : this()
		{
			this.MajorVersion = version.FileMajorPart;
			this.MinorVersion = version.FileMinorPart;
			this.MajorBuildNumber = version.FileBuildPart;
			this.MinorBuildNumber = version.FilePrivatePart;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00017F84 File Offset: 0x00016184
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00017F8C File Offset: 0x0001618C
		[DataMember(Name = "MajorVersion", IsRequired = false, Order = 1)]
		public int MajorVersion { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00017F95 File Offset: 0x00016195
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00017F9D File Offset: 0x0001619D
		[DataMember(Name = "MinorVersion", IsRequired = false, Order = 2)]
		public int MinorVersion { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00017FA6 File Offset: 0x000161A6
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00017FAE File Offset: 0x000161AE
		[DataMember(Name = "MajorBuildNumber", IsRequired = false, Order = 3)]
		public int MajorBuildNumber { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00017FB7 File Offset: 0x000161B7
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00017FBF File Offset: 0x000161BF
		[DataMember(Name = "MinorBuildNumber", IsRequired = false, Order = 4)]
		public int MinorBuildNumber { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00017FC8 File Offset: 0x000161C8
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00017FD0 File Offset: 0x000161D0
		[DataMember(Name = "Version", IsRequired = false, Order = 5)]
		public string Version { get; set; }

		// Token: 0x0400036A RID: 874
		private const ExchangeVersion CurrentExchangeVersion = ExchangeVersion.Exchange2013_SP1;

		// Token: 0x0400036B RID: 875
		internal static LazyMember<ServerVersionInfo> CurrentVersion = new LazyMember<ServerVersionInfo>(() => new ServerVersionInfo(Common.ServerVersion));
	}
}
