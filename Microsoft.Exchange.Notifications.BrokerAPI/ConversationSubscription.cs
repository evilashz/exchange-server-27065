using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000025 RID: 37
	[KnownType(typeof(ConversationResponseShape))]
	[XmlInclude(typeof(ConversationResponseShape))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class ConversationSubscription : RowSubscription
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000038AF File Offset: 0x00001AAF
		public ConversationSubscription() : base(NotificationType.Conversation)
		{
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000038B8 File Offset: 0x00001AB8
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000038C0 File Offset: 0x00001AC0
		[DataMember(EmitDefaultValue = false)]
		public ConversationResponseShape ConversationShape { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000038CC File Offset: 0x00001ACC
		[IgnoreDataMember]
		[XmlIgnore]
		public override IEnumerable<Tuple<string, object>> Differentiators
		{
			get
			{
				return base.Differentiators.Concat(new Tuple<string, object>[]
				{
					new Tuple<string, object>("CS", JsonConverter.Serialize<ConversationResponseShape>(this.ConversationShape, null))
				});
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003905 File Offset: 0x00001B05
		protected override bool Validate()
		{
			return base.Validate() && this.ConversationShape != null;
		}
	}
}
