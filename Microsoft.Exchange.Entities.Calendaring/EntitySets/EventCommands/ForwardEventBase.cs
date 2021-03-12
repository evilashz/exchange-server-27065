using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200004D RID: 77
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ForwardEventBase : KeyedEntityCommand<Events, VoidResult>
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000083B1 File Offset: 0x000065B1
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000083B9 File Offset: 0x000065B9
		[DataMember]
		public ForwardEventParameters Parameters { get; set; }

		// Token: 0x060001ED RID: 493 RVA: 0x000083C2 File Offset: 0x000065C2
		protected override void UpdateCustomLoggingData()
		{
			base.UpdateCustomLoggingData();
			this.SetCustomLoggingData("ForwardEventParameters", this.Parameters);
		}
	}
}
