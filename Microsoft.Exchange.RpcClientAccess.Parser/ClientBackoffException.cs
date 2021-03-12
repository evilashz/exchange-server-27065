using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientBackoffException : Exception
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0000DFEA File Offset: 0x0000C1EA
		public ClientBackoffException(string message) : base(message)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000DFF3 File Offset: 0x0000C1F3
		public ClientBackoffException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000DFFD File Offset: 0x0000C1FD
		public ClientBackoffException(string message, byte logonId, uint duration) : base(message)
		{
			this.backoffInformation = new BackoffInformation(logonId, duration, Array<BackoffRopData>.Empty, Array<byte>.Empty);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E01D File Offset: 0x0000C21D
		public ClientBackoffException(string message, byte logonId, uint duration, BackoffRopData[] backoffRopData, byte[] additionalData) : base(message)
		{
			this.backoffInformation = new BackoffInformation(logonId, duration, backoffRopData, additionalData);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000E037 File Offset: 0x0000C237
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000E03F File Offset: 0x0000C23F
		public bool IsRepeatingBackoff { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000E048 File Offset: 0x0000C248
		internal bool IsSpecificBackoff
		{
			get
			{
				return this.backoffInformation != null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000E056 File Offset: 0x0000C256
		internal BackoffInformation BackoffInformation
		{
			get
			{
				return this.backoffInformation;
			}
		}

		// Token: 0x0400026E RID: 622
		private readonly BackoffInformation backoffInformation;
	}
}
