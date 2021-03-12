using System;
using System.DirectoryServices.Protocols;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C5 RID: 1733
	[DataContract]
	internal class ServerInfo : IExtensibleDataObject
	{
		// Token: 0x06004FF7 RID: 20471 RVA: 0x00126EE8 File Offset: 0x001250E8
		public ServerInfo(string serverFqdn, string partitionFqdn, int port)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfOutOfRange<int>("port", port, 0, 65535);
			this.fqdn = serverFqdn;
			this.partitionFqdn = partitionFqdn;
			this.Port = port;
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x00126F47 File Offset: 0x00125147
		// (set) Token: 0x06004FF9 RID: 20473 RVA: 0x00126F4F File Offset: 0x0012514F
		[DataMember]
		public bool IsServerSuitable
		{
			get
			{
				return this.isServerSuitable;
			}
			internal set
			{
				this.isServerSuitable = value;
			}
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x00126F58 File Offset: 0x00125158
		// (set) Token: 0x06004FFB RID: 20475 RVA: 0x00126F60 File Offset: 0x00125160
		[DataMember]
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
			internal set
			{
				this.fqdn = value;
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x00126F69 File Offset: 0x00125169
		// (set) Token: 0x06004FFD RID: 20477 RVA: 0x00126F71 File Offset: 0x00125171
		[DataMember]
		public int Port { get; internal set; }

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06004FFE RID: 20478 RVA: 0x00126F7A File Offset: 0x0012517A
		// (set) Token: 0x06004FFF RID: 20479 RVA: 0x00126F82 File Offset: 0x00125182
		[DataMember]
		public string PartitionFqdn
		{
			get
			{
				return this.partitionFqdn;
			}
			internal set
			{
				this.partitionFqdn = value;
			}
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x00126F8B File Offset: 0x0012518B
		// (set) Token: 0x06005001 RID: 20481 RVA: 0x00126F93 File Offset: 0x00125193
		[DataMember(EmitDefaultValue = false)]
		public string SiteName
		{
			get
			{
				return this.siteName;
			}
			internal set
			{
				this.siteName = value;
			}
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06005002 RID: 20482 RVA: 0x00126F9C File Offset: 0x0012519C
		// (set) Token: 0x06005003 RID: 20483 RVA: 0x00126FA4 File Offset: 0x001251A4
		[DataMember(EmitDefaultValue = false)]
		public int DnsWeight
		{
			get
			{
				return this.dnsWeight;
			}
			set
			{
				this.dnsWeight = value;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06005004 RID: 20484 RVA: 0x00126FAD File Offset: 0x001251AD
		// (set) Token: 0x06005005 RID: 20485 RVA: 0x00126FB5 File Offset: 0x001251B5
		[DataMember(EmitDefaultValue = false)]
		public int AuthType
		{
			get
			{
				return this.authType;
			}
			set
			{
				this.authType = value;
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06005006 RID: 20486 RVA: 0x00126FBE File Offset: 0x001251BE
		// (set) Token: 0x06005007 RID: 20487 RVA: 0x00126FC6 File Offset: 0x001251C6
		[DataMember(EmitDefaultValue = false)]
		public string WritableNC
		{
			get
			{
				return this.writableNC;
			}
			internal set
			{
				this.writableNC = value;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06005008 RID: 20488 RVA: 0x00126FCF File Offset: 0x001251CF
		// (set) Token: 0x06005009 RID: 20489 RVA: 0x00126FD7 File Offset: 0x001251D7
		[DataMember(EmitDefaultValue = false)]
		public string ConfigNC
		{
			get
			{
				return this.configNC;
			}
			internal set
			{
				this.configNC = value;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x0600500A RID: 20490 RVA: 0x00126FE0 File Offset: 0x001251E0
		// (set) Token: 0x0600500B RID: 20491 RVA: 0x00126FE8 File Offset: 0x001251E8
		[DataMember(EmitDefaultValue = false)]
		public string RootDomainNC
		{
			get
			{
				return this.rootNC;
			}
			internal set
			{
				this.rootNC = value;
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x0600500C RID: 20492 RVA: 0x00126FF1 File Offset: 0x001251F1
		// (set) Token: 0x0600500D RID: 20493 RVA: 0x00126FF9 File Offset: 0x001251F9
		[DataMember(EmitDefaultValue = false)]
		public string SchemaNC
		{
			get
			{
				return this.schemaNC;
			}
			internal set
			{
				this.schemaNC = value;
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x00127002 File Offset: 0x00125202
		// (set) Token: 0x0600500F RID: 20495 RVA: 0x0012700A File Offset: 0x0012520A
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x06005010 RID: 20496 RVA: 0x00127014 File Offset: 0x00125214
		public ADServerInfo ToADServerInfo()
		{
			return new ADServerInfo(this.Fqdn, this.PartitionFqdn, this.Port, this.WritableNC, this.DnsWeight, (AuthType)this.AuthType, this.IsServerSuitable)
			{
				ConfigNC = this.ConfigNC,
				RootDomainNC = this.RootDomainNC,
				SchemaNC = this.SchemaNC,
				SiteName = this.SiteName,
				IsServerSuitable = this.IsServerSuitable
			};
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0012708E File Offset: 0x0012528E
		public override string ToString()
		{
			return string.Format("{0}-{1}-{2}", this.Fqdn, this.Port, this.PartitionFqdn ?? string.Empty);
		}

		// Token: 0x04003675 RID: 13941
		private const int DefaultAuthType = 9;

		// Token: 0x04003676 RID: 13942
		private string fqdn;

		// Token: 0x04003677 RID: 13943
		private string partitionFqdn;

		// Token: 0x04003678 RID: 13944
		private string siteName;

		// Token: 0x04003679 RID: 13945
		private string writableNC;

		// Token: 0x0400367A RID: 13946
		private string configNC;

		// Token: 0x0400367B RID: 13947
		private string rootNC;

		// Token: 0x0400367C RID: 13948
		private string schemaNC;

		// Token: 0x0400367D RID: 13949
		private int authType = 9;

		// Token: 0x0400367E RID: 13950
		private int dnsWeight = 100;

		// Token: 0x0400367F RID: 13951
		private bool isServerSuitable;
	}
}
