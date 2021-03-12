using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000242 RID: 578
	[Flags]
	internal enum SynchronizerConfigFlags
	{
		// Token: 0x0400113E RID: 4414
		None = 0,
		// Token: 0x0400113F RID: 4415
		Unicode = 1,
		// Token: 0x04001140 RID: 4416
		NoDeletions = 2,
		// Token: 0x04001141 RID: 4417
		NoSoftDeletions = 4,
		// Token: 0x04001142 RID: 4418
		ReadState = 8,
		// Token: 0x04001143 RID: 4419
		Associated = 16,
		// Token: 0x04001144 RID: 4420
		Normal = 32,
		// Token: 0x04001145 RID: 4421
		NoConflicts = 64,
		// Token: 0x04001146 RID: 4422
		OnlySpecifiedProps = 128,
		// Token: 0x04001147 RID: 4423
		NoForeignKeys = 256,
		// Token: 0x04001148 RID: 4424
		Catchup = 1024,
		// Token: 0x04001149 RID: 4425
		BestBody = 8192,
		// Token: 0x0400114A RID: 4426
		IgnoreSpecifiedOnAssociated = 16384,
		// Token: 0x0400114B RID: 4427
		ProgressMode = 32768,
		// Token: 0x0400114C RID: 4428
		FXRecoverMode = 65536,
		// Token: 0x0400114D RID: 4429
		ForceUnicode = 262144,
		// Token: 0x0400114E RID: 4430
		NoChanges = 524288,
		// Token: 0x0400114F RID: 4431
		OrderByDeliveryTime = 1048576,
		// Token: 0x04001150 RID: 4432
		PartialItem = 8388608,
		// Token: 0x04001151 RID: 4433
		UseCpId = 16777216,
		// Token: 0x04001152 RID: 4434
		SendPropErrors = 33554432,
		// Token: 0x04001153 RID: 4435
		ManifestMode = 67108864,
		// Token: 0x04001154 RID: 4436
		CatchupFull = 134217728
	}
}
