using System;
using System.Xml.Serialization;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003CA RID: 970
	[XmlType("ClassificationDefinition")]
	public sealed class ClassificationDefinition15 : IVisitee15, IVersionedItem
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0009BA1C File Offset: 0x00099C1C
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0009BA24 File Offset: 0x00099C24
		[XmlAttribute("id")]
		public string ID { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0009BA2D File Offset: 0x00099C2D
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0009BA35 File Offset: 0x00099C35
		[XmlAttribute("version")]
		public long Version { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0009BA3E File Offset: 0x00099C3E
		[XmlIgnore]
		DateTime IVersionedItem.Version
		{
			get
			{
				return DateTime.FromBinary(this.Version);
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0009BA4B File Offset: 0x00099C4B
		public void Accept(Visitor15 visitor)
		{
			visitor.Visit(this);
		}
	}
}
