using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C9 RID: 969
	[XmlType("ClassificationDefinitions")]
	public sealed class ClassificationDefinitions15 : IVisitee15
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0009B9FA File Offset: 0x00099BFA
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0009BA02 File Offset: 0x00099C02
		[XmlElement("ClassificationDefinition")]
		public List<ClassificationDefinition15> ClassificationDefinitions { get; set; }

		// Token: 0x06001B41 RID: 6977 RVA: 0x0009BA0B File Offset: 0x00099C0B
		public void Accept(Visitor15 visitor)
		{
			visitor.Visit(this);
		}
	}
}
