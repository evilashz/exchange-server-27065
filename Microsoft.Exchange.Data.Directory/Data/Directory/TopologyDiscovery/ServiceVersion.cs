using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C6 RID: 1734
	[DataContract]
	internal class ServiceVersion : IExtensibleDataObject
	{
		// Token: 0x06005012 RID: 20498 RVA: 0x001270BA File Offset: 0x001252BA
		public ServiceVersion(int major, int minor, int build, int revision)
		{
			this.Major = major;
			this.Minor = minor;
			this.Build = build;
			this.Revision = revision;
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x001270DF File Offset: 0x001252DF
		public static ServiceVersion Current
		{
			get
			{
				if (ServiceVersion.current == null)
				{
					ServiceVersion.current = new ServiceVersion(ServerVersion.InstalledVersion.Major, ServerVersion.InstalledVersion.Minor, ServerVersion.InstalledVersion.Build, ServerVersion.InstalledVersion.Revision);
				}
				return ServiceVersion.current;
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x06005014 RID: 20500 RVA: 0x0012711F File Offset: 0x0012531F
		// (set) Token: 0x06005015 RID: 20501 RVA: 0x00127127 File Offset: 0x00125327
		[DataMember(IsRequired = true)]
		public int Major { get; set; }

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x06005016 RID: 20502 RVA: 0x00127130 File Offset: 0x00125330
		// (set) Token: 0x06005017 RID: 20503 RVA: 0x00127138 File Offset: 0x00125338
		[DataMember(IsRequired = true)]
		public int Minor { get; set; }

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x00127141 File Offset: 0x00125341
		// (set) Token: 0x06005019 RID: 20505 RVA: 0x00127149 File Offset: 0x00125349
		[DataMember(IsRequired = true)]
		public int Build { get; set; }

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x00127152 File Offset: 0x00125352
		// (set) Token: 0x0600501B RID: 20507 RVA: 0x0012715A File Offset: 0x0012535A
		[DataMember(IsRequired = true)]
		public int Revision { get; set; }

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x00127163 File Offset: 0x00125363
		// (set) Token: 0x0600501D RID: 20509 RVA: 0x0012716B File Offset: 0x0012536B
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x0600501E RID: 20510 RVA: 0x00127174 File Offset: 0x00125374
		public ExchangeBuild ToExchangeBuild()
		{
			return new ExchangeBuild((byte)this.Major, (byte)this.Minor, (ushort)((byte)this.Build), (ushort)((byte)this.Revision));
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x00127198 File Offset: 0x00125398
		public override string ToString()
		{
			return string.Format("{0}.{1} (Build {2}.{3})", new object[]
			{
				this.Major,
				this.Minor,
				this.Build,
				this.Revision
			});
		}

		// Token: 0x04003682 RID: 13954
		private static ServiceVersion current;
	}
}
