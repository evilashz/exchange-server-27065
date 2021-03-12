using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A77 RID: 2679
	[Serializable]
	public sealed class PublicFolderId : ObjectId, IEquatable<PublicFolderId>
	{
		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x0019DC28 File Offset: 0x0019BE28
		// (set) Token: 0x060061E2 RID: 25058 RVA: 0x0019DC30 File Offset: 0x0019BE30
		internal StoreObjectId StoreObjectId { get; set; }

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x0019DC39 File Offset: 0x0019BE39
		// (set) Token: 0x060061E4 RID: 25060 RVA: 0x0019DC41 File Offset: 0x0019BE41
		public MapiFolderPath MapiFolderPath { get; private set; }

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0019DC4A File Offset: 0x0019BE4A
		// (set) Token: 0x060061E6 RID: 25062 RVA: 0x0019DC52 File Offset: 0x0019BE52
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x060061E7 RID: 25063 RVA: 0x0019DC5B File Offset: 0x0019BE5B
		internal PublicFolderId(MapiFolderPath folderPath)
		{
			this.MapiFolderPath = folderPath;
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0019DC6A File Offset: 0x0019BE6A
		internal PublicFolderId(StoreObjectId storeObjectId)
		{
			this.StoreObjectId = storeObjectId;
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x0019DC79 File Offset: 0x0019BE79
		internal PublicFolderId(PublicFolderId publicFolderId) : this(publicFolderId.OrganizationId, publicFolderId.StoreObjectId, publicFolderId.MapiFolderPath)
		{
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x0019DC93 File Offset: 0x0019BE93
		internal PublicFolderId(OrganizationId organizationId, StoreObjectId storeObjectId, MapiFolderPath folderPath) : this(storeObjectId, folderPath)
		{
			this.OrganizationId = organizationId;
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x0019DCA4 File Offset: 0x0019BEA4
		internal PublicFolderId(StoreObjectId storeObjectId, MapiFolderPath folderPath)
		{
			this.StoreObjectId = storeObjectId;
			this.MapiFolderPath = folderPath;
		}

		// Token: 0x060061EC RID: 25068 RVA: 0x0019DCBC File Offset: 0x0019BEBC
		public bool Equals(PublicFolderId other)
		{
			if (other == null)
			{
				return false;
			}
			bool flag = object.Equals(this.StoreObjectId, other.StoreObjectId);
			if (flag && this.StoreObjectId != null)
			{
				return true;
			}
			bool flag2 = object.Equals(this.MapiFolderPath, other.MapiFolderPath);
			return (flag2 && this.MapiFolderPath != null) || (flag2 && flag);
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x0019DD18 File Offset: 0x0019BF18
		public override byte[] GetBytes()
		{
			return (this.StoreObjectId == null) ? Array<byte>.Empty : this.StoreObjectId.GetBytes();
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x0019DD41 File Offset: 0x0019BF41
		public override int GetHashCode()
		{
			if (this.StoreObjectId != null)
			{
				return this.StoreObjectId.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x0019DD5D File Offset: 0x0019BF5D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PublicFolderId);
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x0019DD6C File Offset: 0x0019BF6C
		public override string ToString()
		{
			string str = (this.OrganizationId == null) ? string.Empty : (this.OrganizationId.OrganizationalUnit.Name + '\\'.ToString());
			if (this.MapiFolderPath != null)
			{
				return str + this.MapiFolderPath.ToString();
			}
			if (this.StoreObjectId != null)
			{
				return str + this.StoreObjectId.ToString();
			}
			return null;
		}
	}
}
