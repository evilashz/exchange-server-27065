using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002AE RID: 686
	internal class AudioCompressionStage : SynchronousPipelineStageBase
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x000597DC File Offset: 0x000579DC
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.CpuBound;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x000597DF File Offset: 0x000579DF
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(3.0);
			}
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x000597F0 File Offset: 0x000579F0
		protected override void InternalDoSynchronousWork()
		{
			IUMCompressAudio iumcompressAudio = base.WorkItem.Message as IUMCompressAudio;
			ExAssert.RetailAssert(iumcompressAudio != null, "AudioCompressionStage must operate on PipelineContext whcih implements IUMCompressAudio.");
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (iumcompressAudio.FileToCompressPath != null)
			{
				iumcompressAudio.CompressedAudioFile = MediaMethods.FromPcm(iumcompressAudio.FileToCompressPath, iumcompressAudio.AudioCodec);
			}
			base.WorkItem.PipelineStatisticsLogRow.AudioCodec = iumcompressAudio.AudioCodec;
			base.WorkItem.PipelineStatisticsLogRow.AudioCompressionElapsedTime = ExDateTime.UtcNow - utcNow;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x00059875 File Offset: 0x00057A75
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AudioCompressionStage>(this);
		}
	}
}
