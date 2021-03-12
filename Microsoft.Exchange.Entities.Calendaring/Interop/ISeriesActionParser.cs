using System;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000065 RID: 101
	internal interface ISeriesActionParser
	{
		// Token: 0x060002A8 RID: 680
		ICalendarInteropSeriesAction DeserializeCommand(ActionInfo action, Event contextEntity);
	}
}
