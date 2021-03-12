using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "Sync")]
	public class Sync
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000056CF File Offset: 0x000038CF
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000056D7 File Offset: 0x000038D7
		[XmlElement(ElementName = "Collections", Type = typeof(List<Collection>))]
		public List<Collection> Collections { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000056E0 File Offset: 0x000038E0
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000056E8 File Offset: 0x000038E8
		[XmlElement(ElementName = "Wait")]
		public int? Wait { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000056F1 File Offset: 0x000038F1
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000056F9 File Offset: 0x000038F9
		[XmlElement(ElementName = "HeartbeatInterval")]
		public int? HeartbeatInterval { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005702 File Offset: 0x00003902
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000570A File Offset: 0x0000390A
		[XmlElement(ElementName = "WindowSize")]
		public string WindowSize { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005713 File Offset: 0x00003913
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000571B File Offset: 0x0000391B
		[XmlElement(ElementName = "Partial")]
		public object Partial { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005724 File Offset: 0x00003924
		[XmlIgnore]
		public bool WaitSpecified
		{
			get
			{
				return this.Wait != null;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00005740 File Offset: 0x00003940
		[XmlIgnore]
		public bool HeartbeatIntervalSpecified
		{
			get
			{
				return this.HeartbeatInterval != null;
			}
		}
	}
}
