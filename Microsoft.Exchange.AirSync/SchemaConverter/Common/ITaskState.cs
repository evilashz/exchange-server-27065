using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000168 RID: 360
	internal interface ITaskState : IProperty
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001033 RID: 4147
		bool Complete { get; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001034 RID: 4148
		ExDateTime? DateCompleted { get; }
	}
}
