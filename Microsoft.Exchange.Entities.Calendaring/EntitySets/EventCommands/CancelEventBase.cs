using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000038 RID: 56
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CancelEventBase : KeyedEntityCommand<Events, VoidResult>
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000061A4 File Offset: 0x000043A4
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000061AC File Offset: 0x000043AC
		[DataMember]
		public CancelEventParameters Parameters { get; set; }
	}
}
