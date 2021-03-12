using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000742 RID: 1858
	[XmlType(TypeName = "DeletedOccurrenceStateDefinitionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public sealed class DeletedOccurrenceStateDefinition : BaseCalendarItemStateDefinition
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000C665A File Offset: 0x000C485A
		// (set) Token: 0x060037EE RID: 14318 RVA: 0x000C6662 File Offset: 0x000C4862
		[XmlElement]
		[DataMember(IsRequired = true, Order = 1)]
		public string OccurrenceDate { get; set; }

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000C666B File Offset: 0x000C486B
		// (set) Token: 0x060037F0 RID: 14320 RVA: 0x000C6673 File Offset: 0x000C4873
		[DataMember(IsRequired = false, Order = 2)]
		[XmlElement]
		public bool IsOccurrencePresent { get; set; }

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000C667C File Offset: 0x000C487C
		[XmlIgnore]
		public ExDateTime OccurrenceExDateTime
		{
			get
			{
				if (string.IsNullOrEmpty(this.OccurrenceDate))
				{
					return ExDateTime.MinValue;
				}
				return ExDateTimeConverter.ParseTimeZoneRelated(this.OccurrenceDate, EWSSettings.RequestTimeZone);
			}
		}
	}
}
