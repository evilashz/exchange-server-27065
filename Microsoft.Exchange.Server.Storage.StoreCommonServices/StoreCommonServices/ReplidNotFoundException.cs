using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000074 RID: 116
	public class ReplidNotFoundException : StoreException
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x0001CE55 File Offset: 0x0001B055
		public ReplidNotFoundException(LID lid, ushort replid) : base(lid, ErrorCodeValue.NotFound)
		{
			this.replid = replid;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001CE6A File Offset: 0x0001B06A
		public ReplidNotFoundException(LID lid, ushort replid, Exception innerException) : base(lid, ErrorCodeValue.NotFound, string.Empty, innerException)
		{
			this.replid = replid;
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001CE85 File Offset: 0x0001B085
		public ushort Replid
		{
			get
			{
				return this.replid;
			}
		}

		// Token: 0x040003B3 RID: 947
		private const string ReplidSerializationLabel = "replid";

		// Token: 0x040003B4 RID: 948
		private ushort replid;
	}
}
