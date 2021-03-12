using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncOperation
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0001755C File Offset: 0x0001575C
		internal DeltaSyncOperation(DeltaSyncOperation.Type type, DeltaSyncObject deltaSyncObject)
		{
			SyncUtilities.ThrowIfArgumentNull("deltaSyncObject", deltaSyncObject);
			this.deltaSyncObject = deltaSyncObject;
			this.type = type;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001757D File Offset: 0x0001577D
		internal DeltaSyncOperation.Type OperationType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00017585 File Offset: 0x00015785
		internal DeltaSyncObject DeltaSyncObject
		{
			get
			{
				return this.deltaSyncObject;
			}
		}

		// Token: 0x040002C0 RID: 704
		private DeltaSyncObject deltaSyncObject;

		// Token: 0x040002C1 RID: 705
		private DeltaSyncOperation.Type type;

		// Token: 0x02000070 RID: 112
		internal enum Type
		{
			// Token: 0x040002C3 RID: 707
			Add,
			// Token: 0x040002C4 RID: 708
			Change,
			// Token: 0x040002C5 RID: 709
			Delete
		}
	}
}
