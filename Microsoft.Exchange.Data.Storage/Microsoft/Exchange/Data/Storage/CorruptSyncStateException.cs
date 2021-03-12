using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000722 RID: 1826
	[Serializable]
	public class CorruptSyncStateException : CorruptDataException
	{
		// Token: 0x060047B2 RID: 18354 RVA: 0x00130425 File Offset: 0x0012E625
		public CorruptSyncStateException(string syncStateName, LocalizedString message) : base(message)
		{
			this.SyncStateName = syncStateName;
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00130435 File Offset: 0x0012E635
		public CorruptSyncStateException(string message, Exception innerException) : this(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00130444 File Offset: 0x0012E644
		public CorruptSyncStateException(LocalizedString message, Exception innerException) : this("<NoName>", message, innerException)
		{
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x00130453 File Offset: 0x0012E653
		public CorruptSyncStateException(string syncStateName, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			this.SyncStateName = syncStateName;
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x00130464 File Offset: 0x0012E664
		protected CorruptSyncStateException(SerializationInfo info, StreamingContext context) : this("<NoName>", info, context)
		{
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x00130473 File Offset: 0x0012E673
		protected CorruptSyncStateException(string syncStateName, SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.SyncStateName = syncStateName;
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x00130484 File Offset: 0x0012E684
		// (set) Token: 0x060047B9 RID: 18361 RVA: 0x0013048C File Offset: 0x0012E68C
		public string SyncStateName { get; private set; }

		// Token: 0x0400272D RID: 10029
		public const string NoName = "<NoName>";
	}
}
