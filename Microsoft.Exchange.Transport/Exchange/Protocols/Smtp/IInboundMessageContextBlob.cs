using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020003F1 RID: 1009
	internal interface IInboundMessageContextBlob
	{
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06002E15 RID: 11797
		bool IsMandatory { get; }

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06002E16 RID: 11798
		string Name { get; }

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06002E17 RID: 11799
		ByteQuantifiedSize MaxBlobSize { get; }

		// Token: 0x06002E18 RID: 11800
		bool IsAdvertised(IEhloOptions ehloOptions);

		// Token: 0x06002E19 RID: 11801
		void DeserializeBlob(Stream stream, SmtpInSessionState sessionState, long blobSize);

		// Token: 0x06002E1A RID: 11802
		bool VerifyPermission(Permission permission);
	}
}
