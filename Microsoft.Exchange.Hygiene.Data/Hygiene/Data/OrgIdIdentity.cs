using System;
using System.IO;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public sealed class OrgIdIdentity : GenericIdentity
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x00010E48 File Offset: 0x0000F048
		public OrgIdIdentity(string userPrincipal) : this(userPrincipal, string.Empty)
		{
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00010E56 File Offset: 0x0000F056
		public OrgIdIdentity(string userPrincipal, string authType) : base(userPrincipal, authType)
		{
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010E60 File Offset: 0x0000F060
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00010E68 File Offset: 0x0000F068
		public ADObjectId UserId { get; internal set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00010E71 File Offset: 0x0000F071
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00010E79 File Offset: 0x0000F079
		public ADObjectId TenantId { get; internal set; }

		// Token: 0x06000529 RID: 1321 RVA: 0x00010E84 File Offset: 0x0000F084
		public static OrgIdIdentity Deserialize(string token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			OrgIdIdentity result;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(token)))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					string authType = binaryReader.ReadString();
					string userPrincipal = binaryReader.ReadString();
					string distinguishedName = binaryReader.ReadString();
					string input = binaryReader.ReadString();
					string distinguishedName2 = binaryReader.ReadString();
					string input2 = binaryReader.ReadString();
					result = new OrgIdIdentity(userPrincipal, authType)
					{
						UserId = new ADObjectId(distinguishedName, Guid.Parse(input)),
						TenantId = new ADObjectId(distinguishedName2, Guid.Parse(input2))
					};
				}
			}
			return result;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00010F4C File Offset: 0x0000F14C
		public string Serialize()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(this.AuthenticationType);
					binaryWriter.Write(this.Name);
					binaryWriter.Write(this.UserId.DistinguishedName);
					binaryWriter.Write(this.UserId.ObjectGuid.ToString());
					binaryWriter.Write(this.TenantId.DistinguishedName);
					binaryWriter.Write(this.TenantId.ObjectGuid.ToString());
					binaryWriter.Flush();
				}
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}
	}
}
