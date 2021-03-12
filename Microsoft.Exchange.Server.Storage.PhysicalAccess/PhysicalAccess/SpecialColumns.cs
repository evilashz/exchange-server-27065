using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200008B RID: 139
	public struct SpecialColumns
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001C364 File Offset: 0x0001A564
		public SpecialColumns(PhysicalColumn propertyBlob, PhysicalColumn offPagePropertyBlob, PhysicalColumn propBag, int numberOfPartioningColumns)
		{
			this.PropertyBlob = propertyBlob;
			this.OffPagePropertyBlob = offPagePropertyBlob;
			this.PropBag = propBag;
			this.NumberOfPartioningColumns = numberOfPartioningColumns;
		}

		// Token: 0x040001ED RID: 493
		public PhysicalColumn PropertyBlob;

		// Token: 0x040001EE RID: 494
		public PhysicalColumn OffPagePropertyBlob;

		// Token: 0x040001EF RID: 495
		public PhysicalColumn PropBag;

		// Token: 0x040001F0 RID: 496
		public int NumberOfPartioningColumns;
	}
}
