using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x0200009C RID: 156
	internal interface IJetSimpleQueryOperator : IJetRecordCounter, ITWIR
	{
		// Token: 0x06000672 RID: 1650
		byte[] GetColumnValueAsBytes(Column column);

		// Token: 0x06000673 RID: 1651
		byte[] GetPhysicalColumnValueAsBytes(PhysicalColumn column);

		// Token: 0x06000674 RID: 1652
		bool MoveFirst(out int rowsSkipped);

		// Token: 0x06000675 RID: 1653
		bool MoveNext();

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000676 RID: 1654
		bool Interrupted { get; }

		// Token: 0x06000677 RID: 1655
		void RequestResume();

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000678 RID: 1656
		bool CanMoveBack { get; }

		// Token: 0x06000679 RID: 1657
		void MoveBackAndInterrupt(int rows);
	}
}
