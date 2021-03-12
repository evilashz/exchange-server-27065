using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BackoffInformation
	{
		// Token: 0x060001EE RID: 494 RVA: 0x000072C5 File Offset: 0x000054C5
		public BackoffInformation(byte logonId, uint duration, BackoffRopData[] backoffRopData, byte[] additionalData)
		{
			this.logonId = logonId;
			this.duration = duration;
			this.backoffRopData = backoffRopData;
			this.additionalData = additionalData;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000072EA File Offset: 0x000054EA
		internal byte LogonId
		{
			get
			{
				return this.logonId;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000072F2 File Offset: 0x000054F2
		internal uint Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000072FA File Offset: 0x000054FA
		internal BackoffRopData[] BackoffRopData
		{
			get
			{
				return this.backoffRopData;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007302 File Offset: 0x00005502
		internal byte[] AdditionalData
		{
			get
			{
				return this.additionalData;
			}
		}

		// Token: 0x040000D5 RID: 213
		private readonly byte logonId;

		// Token: 0x040000D6 RID: 214
		private readonly uint duration;

		// Token: 0x040000D7 RID: 215
		private readonly BackoffRopData[] backoffRopData;

		// Token: 0x040000D8 RID: 216
		private readonly byte[] additionalData;
	}
}
