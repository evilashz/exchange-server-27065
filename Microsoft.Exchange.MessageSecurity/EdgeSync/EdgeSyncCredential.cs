using System;
using System.IO;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class EdgeSyncCredential
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00004374 File Offset: 0x00002574
		public static EdgeSyncCredential DeserializeEdgeSyncCredential(byte[] data)
		{
			EdgeSyncCredential result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				EdgeSyncCredential edgeSyncCredential = (EdgeSyncCredential)EdgeSyncCredential.serializer.Deserialize(memoryStream);
				result = edgeSyncCredential;
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043B8 File Offset: 0x000025B8
		public static byte[] SerializeEdgeSyncCredential(EdgeSyncCredential credential)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				EdgeSyncCredential.serializer.Serialize(memoryStream, credential);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000043FC File Offset: 0x000025FC
		public EdgeSyncCredential Clone()
		{
			return new EdgeSyncCredential
			{
				EdgeServerFQDN = this.EdgeServerFQDN,
				ESRAUsername = this.ESRAUsername,
				EncryptedESRAPassword = this.EncryptedESRAPassword,
				EffectiveDate = this.EffectiveDate,
				Duration = this.Duration,
				IsBootStrapAccount = this.IsBootStrapAccount
			};
		}

		// Token: 0x04000059 RID: 89
		private static EdgeSyncCredentialSerializer serializer = new EdgeSyncCredentialSerializer();

		// Token: 0x0400005A RID: 90
		public string EdgeServerFQDN;

		// Token: 0x0400005B RID: 91
		public string ESRAUsername;

		// Token: 0x0400005C RID: 92
		public byte[] EncryptedESRAPassword;

		// Token: 0x0400005D RID: 93
		public byte[] EdgeEncryptedESRAPassword;

		// Token: 0x0400005E RID: 94
		public long EffectiveDate;

		// Token: 0x0400005F RID: 95
		public long Duration;

		// Token: 0x04000060 RID: 96
		public bool IsBootStrapAccount;
	}
}
