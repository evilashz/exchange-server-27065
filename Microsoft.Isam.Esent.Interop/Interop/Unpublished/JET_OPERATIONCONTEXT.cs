using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200008B RID: 139
	public struct JET_OPERATIONCONTEXT : IContentEquatable<JET_OPERATIONCONTEXT>
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000D153 File Offset: 0x0000B353
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000D15B File Offset: 0x0000B35B
		public uint UserID { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000D164 File Offset: 0x0000B364
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000D16C File Offset: 0x0000B36C
		public byte OperationID { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000D175 File Offset: 0x0000B375
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000D17D File Offset: 0x0000B37D
		public byte OperationType { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000D186 File Offset: 0x0000B386
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000D18E File Offset: 0x0000B38E
		public byte ClientType { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000D197 File Offset: 0x0000B397
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000D19F File Offset: 0x0000B39F
		public byte Flags { get; set; }

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public bool ContentEquals(JET_OPERATIONCONTEXT other)
		{
			return this.UserID == other.UserID && this.OperationID == other.OperationID && this.OperationType == other.OperationType && this.ClientType == other.ClientType && this.Flags == other.Flags;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000D208 File Offset: 0x0000B408
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_OPERATIONCONTEXT({0}:{1}:{2}:{3}:{4}:0x{5:x2})", new object[]
			{
				this.UserID,
				this.OperationID,
				this.OperationType,
				this.ClientType,
				this.Flags
			});
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000D274 File Offset: 0x0000B474
		internal NATIVE_OPERATIONCONTEXT GetNativeOperationContext()
		{
			return new NATIVE_OPERATIONCONTEXT
			{
				ulUserID = this.UserID,
				nOperationID = this.OperationID,
				nOperationType = this.OperationType,
				nClientType = this.ClientType,
				fFlags = this.Flags
			};
		}
	}
}
