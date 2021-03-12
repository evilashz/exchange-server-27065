using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200002E RID: 46
	public interface ITWIR
	{
		// Token: 0x06000267 RID: 615
		int GetColumnSize(Column column);

		// Token: 0x06000268 RID: 616
		object GetColumnValue(Column column);

		// Token: 0x06000269 RID: 617
		int GetPhysicalColumnSize(PhysicalColumn column);

		// Token: 0x0600026A RID: 618
		object GetPhysicalColumnValue(PhysicalColumn column);

		// Token: 0x0600026B RID: 619
		int GetPropertyColumnSize(PropertyColumn column);

		// Token: 0x0600026C RID: 620
		object GetPropertyColumnValue(PropertyColumn column);
	}
}
