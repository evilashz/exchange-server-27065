using System;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000005 RID: 5
	internal class AttachmentInfo
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000035A0 File Offset: 0x000017A0
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000035A8 File Offset: 0x000017A8
		public string Name { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000035B1 File Offset: 0x000017B1
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000035B9 File Offset: 0x000017B9
		public string Extension { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000035C2 File Offset: 0x000017C2
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000035CA File Offset: 0x000017CA
		public string FileType { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000035D3 File Offset: 0x000017D3
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000035DB File Offset: 0x000017DB
		public AttachmentInfo Parent { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000035E4 File Offset: 0x000017E4
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000035EC File Offset: 0x000017EC
		public uint[] FileTypes { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x000035F5 File Offset: 0x000017F5
		public AttachmentInfo(string name, string extension, uint[] types)
		{
			this.Name = name;
			this.Extension = extension;
			this.FileType = AttachmentInfo.GetFileTypeFromFipsTypes(types);
			this.FileTypes = types;
			this.Parent = null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003640 File Offset: 0x00001840
		public bool IsFileEmbeddingSupported()
		{
			uint aggregatedTypeBits = 1879048192U;
			return this.FileTypes.Any((uint type) => (aggregatedTypeBits & type) != 0U || 4U == type);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003684 File Offset: 0x00001884
		private static string GetFileTypeFromFipsTypes(uint[] types)
		{
			if (!types.Any((uint type) => 0U != (2147483648U & type)))
			{
				return "unknown";
			}
			return "executable";
		}
	}
}
