using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000085 RID: 133
	public interface IColumnStreamAccess
	{
		// Token: 0x060005E9 RID: 1513
		int GetColumnSize(PhysicalColumn column);

		// Token: 0x060005EA RID: 1514
		int ReadStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count);

		// Token: 0x060005EB RID: 1515
		void WriteStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count);
	}
}
