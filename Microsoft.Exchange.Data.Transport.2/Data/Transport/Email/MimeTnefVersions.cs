using System;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F0 RID: 240
	internal struct MimeTnefVersions
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x00010C61 File Offset: 0x0000EE61
		public MimeTnefVersions(PureMimeMessage mimeMessage, MimePart tnefPart)
		{
			this.RootPartVersion = mimeMessage.Version;
			if (tnefPart != null)
			{
				this.TnefPartVersion = tnefPart.Version;
				return;
			}
			this.TnefPartVersion = -1;
		}

		// Token: 0x040003BD RID: 957
		public int RootPartVersion;

		// Token: 0x040003BE RID: 958
		public int TnefPartVersion;
	}
}
