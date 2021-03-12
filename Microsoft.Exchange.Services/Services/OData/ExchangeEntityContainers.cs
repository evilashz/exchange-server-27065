using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E3B RID: 3643
	internal class ExchangeEntityContainers : IExchangeEntityContainers
	{
		// Token: 0x06005DE9 RID: 24041 RVA: 0x001243B9 File Offset: 0x001225B9
		public ExchangeEntityContainers(StoreSession storeSession)
		{
			this.Calendaring = new CalendaringContainer(storeSession, null);
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x001243CE File Offset: 0x001225CE
		// (set) Token: 0x06005DEB RID: 24043 RVA: 0x001243D6 File Offset: 0x001225D6
		public ICalendaringContainer Calendaring { get; private set; }
	}
}
