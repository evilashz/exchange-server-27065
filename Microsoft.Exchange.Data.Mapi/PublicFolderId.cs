using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mapi.Common;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public sealed class PublicFolderId : FolderId
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002934 File Offset: 0x00000B34
		public static PublicFolderId Parse(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new FormatException(Strings.ExceptionFormatNotSupported);
			}
			try
			{
				return new PublicFolderId(MapiFolderPath.Parse(input));
			}
			catch (FormatException)
			{
			}
			try
			{
				return new PublicFolderId(MapiEntryId.Parse(input));
			}
			catch (FormatException)
			{
			}
			return new PublicFolderId(input);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029A0 File Offset: 0x00000BA0
		public PublicFolderId()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029A8 File Offset: 0x00000BA8
		public PublicFolderId(byte[] bytes) : base(bytes)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029B1 File Offset: 0x00000BB1
		public PublicFolderId(string legacyDn) : base(new PublicStoreId(), legacyDn)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029BF File Offset: 0x00000BBF
		public PublicFolderId(MapiEntryId entryId) : base(new PublicStoreId(), entryId)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029CD File Offset: 0x00000BCD
		public PublicFolderId(MapiFolderPath folderPath) : base(new PublicStoreId(), folderPath)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029DB File Offset: 0x00000BDB
		internal PublicFolderId(MapiEntryId entryId, MapiFolderPath folderPath, string legacyDn) : base(new PublicStoreId(), entryId, folderPath, legacyDn)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029EC File Offset: 0x00000BEC
		public override string ToString()
		{
			string str = (this.organizationId == null) ? string.Empty : (this.organizationId.OrganizationalUnit.Name + '\\'.ToString());
			if (null != base.MapiFolderPath)
			{
				return str + base.MapiFolderPath.ToString();
			}
			if (null != base.MapiEntryId)
			{
				return str + base.MapiEntryId.ToString();
			}
			if (!string.IsNullOrEmpty(base.LegacyDistinguishedName))
			{
				return str + base.LegacyDistinguishedName;
			}
			return null;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A89 File Offset: 0x00000C89
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A91 File Offset: 0x00000C91
		internal OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			set
			{
				this.organizationId = value;
			}
		}

		// Token: 0x0400000F RID: 15
		private OrganizationId organizationId;
	}
}
