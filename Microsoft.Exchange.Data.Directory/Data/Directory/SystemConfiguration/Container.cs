using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003CA RID: 970
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class Container : ADLegacyVersionableObject
	{
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000B63F2 File Offset: 0x000B45F2
		internal override ADObjectSchema Schema
		{
			get
			{
				return Container.schema;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000B63F9 File Offset: 0x000B45F9
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Container.mostDerivedClass;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000B6400 File Offset: 0x000B4600
		// (set) Token: 0x06002C3A RID: 11322 RVA: 0x000B6412 File Offset: 0x000B4612
		public MultiValuedProperty<byte[]> EdgeSyncCookies
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ContainerSchema.EdgeSyncCookies];
			}
			internal set
			{
				this[ContainerSchema.EdgeSyncCookies] = value;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000B6420 File Offset: 0x000B4620
		// (set) Token: 0x06002C3C RID: 11324 RVA: 0x000B6432 File Offset: 0x000B4632
		public byte[] EncryptionKey0
		{
			get
			{
				return (byte[])this[ContainerSchema.CanaryData0];
			}
			internal set
			{
				this[ContainerSchema.CanaryData0] = value;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000B6440 File Offset: 0x000B4640
		// (set) Token: 0x06002C3E RID: 11326 RVA: 0x000B6452 File Offset: 0x000B4652
		public byte[] EncryptionKey1
		{
			get
			{
				return (byte[])this[ContainerSchema.CanaryData1];
			}
			internal set
			{
				this[ContainerSchema.CanaryData1] = value;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x000B6460 File Offset: 0x000B4660
		// (set) Token: 0x06002C40 RID: 11328 RVA: 0x000B6472 File Offset: 0x000B4672
		public byte[] EncryptionKey2
		{
			get
			{
				return (byte[])this[ContainerSchema.CanaryData2];
			}
			internal set
			{
				this[ContainerSchema.CanaryData2] = value;
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000B6480 File Offset: 0x000B4680
		internal Container GetChildContainer(string commonName)
		{
			return base.Session.Read<Container>(base.Id.GetChildId(commonName));
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000B6499 File Offset: 0x000B4699
		internal Container GetParentContainer()
		{
			return base.Session.Read<Container>(base.Id.Parent);
		}

		// Token: 0x04001A7C RID: 6780
		private static ContainerSchema schema = ObjectSchema.GetInstance<ContainerSchema>();

		// Token: 0x04001A7D RID: 6781
		private static string mostDerivedClass = "msExchContainer";
	}
}
