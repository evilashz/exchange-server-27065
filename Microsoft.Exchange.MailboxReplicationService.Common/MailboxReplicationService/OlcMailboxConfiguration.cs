using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	internal class OlcMailboxConfiguration
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00004422 File Offset: 0x00002622
		public override string ToString()
		{
			return string.Format("Puid=0x{0:X}, DGroup={1}", this.Puid, this.DGroup);
		}

		// Token: 0x04000105 RID: 261
		[DataMember]
		public long Puid;

		// Token: 0x04000106 RID: 262
		[DataMember]
		public int DGroup;

		// Token: 0x04000107 RID: 263
		[DataMember]
		public string RemoteHostName;
	}
}
