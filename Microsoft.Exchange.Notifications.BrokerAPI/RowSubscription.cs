using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000023 RID: 35
	[XmlInclude(typeof(MessageItemSubscription))]
	[XmlInclude(typeof(CalendarItemSubscription))]
	[XmlInclude(typeof(PeopleIKnowSubscription))]
	[KnownType(typeof(PeopleIKnowSubscription))]
	[XmlInclude(typeof(ClutterFilter))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(ConversationSubscription))]
	[XmlInclude(typeof(ConversationSubscription))]
	[KnownType(typeof(MessageItemSubscription))]
	[KnownType(typeof(CalendarItemSubscription))]
	[KnownType(typeof(SortResults))]
	[XmlInclude(typeof(SortResults))]
	[KnownType(typeof(ViewFilter))]
	[XmlInclude(typeof(ViewFilter))]
	[KnownType(typeof(ClutterFilter))]
	public abstract class RowSubscription : BaseSubscription
	{
		// Token: 0x060000CB RID: 203 RVA: 0x0000375E File Offset: 0x0000195E
		protected RowSubscription(NotificationType notificationType) : base(notificationType)
		{
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003767 File Offset: 0x00001967
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000376F File Offset: 0x0000196F
		[DataMember(EmitDefaultValue = false)]
		public string FolderId { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003778 File Offset: 0x00001978
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003780 File Offset: 0x00001980
		[DataMember(EmitDefaultValue = false)]
		public SortResults[] SortBy { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003789 File Offset: 0x00001989
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003791 File Offset: 0x00001991
		[DataMember(EmitDefaultValue = false)]
		public ViewFilter Filter { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000379A File Offset: 0x0000199A
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000037A2 File Offset: 0x000019A2
		[DataMember(EmitDefaultValue = false)]
		public ClutterFilter ClutterFilter { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000037AB File Offset: 0x000019AB
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000037B3 File Offset: 0x000019B3
		[DataMember(EmitDefaultValue = false)]
		public string FromFilter { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000037BC File Offset: 0x000019BC
		[IgnoreDataMember]
		[XmlIgnore]
		public override IEnumerable<Tuple<string, object>> Differentiators
		{
			get
			{
				return base.Differentiators.Concat(new Tuple<string, object>[]
				{
					new Tuple<string, object>("FId", this.FolderId),
					new Tuple<string, object>("F", this.Filter),
					new Tuple<string, object>("CF", this.ClutterFilter),
					new Tuple<string, object>("FF", this.FromFilter),
					new Tuple<string, object>("SB", JsonConverter.Serialize<SortResults[]>(this.SortBy, null))
				});
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000384C File Offset: 0x00001A4C
		protected override bool Validate()
		{
			return base.Validate() && !string.IsNullOrEmpty(this.FolderId) && Enum.IsDefined(typeof(ViewFilter), this.Filter) && Enum.IsDefined(typeof(ClutterFilter), this.ClutterFilter);
		}
	}
}
