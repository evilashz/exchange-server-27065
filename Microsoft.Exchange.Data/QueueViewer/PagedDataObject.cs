using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000270 RID: 624
	public interface PagedDataObject : IConfigurable
	{
		// Token: 0x060014E1 RID: 5345
		void ConvertDatesToLocalTime();

		// Token: 0x060014E2 RID: 5346
		void ConvertDatesToUniversalTime();
	}
}
