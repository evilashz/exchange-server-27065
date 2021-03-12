using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C6 RID: 710
	internal interface IUMCompressAudio
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600158A RID: 5514
		// (set) Token: 0x0600158B RID: 5515
		ITempFile CompressedAudioFile { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600158C RID: 5516
		AudioCodecEnum AudioCodec { get; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600158D RID: 5517
		string FileToCompressPath { get; }
	}
}
