using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C5 RID: 965
	[XmlType("PolicyNudges")]
	public sealed class PolicyNudges15 : IVisitee15
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0009B8F4 File Offset: 0x00099AF4
		// (set) Token: 0x06001B23 RID: 6947 RVA: 0x0009B8FC File Offset: 0x00099AFC
		[XmlAttribute("OutlookVersion")]
		public string OutlookVersion { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0009B905 File Offset: 0x00099B05
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x0009B90D File Offset: 0x00099B0D
		[XmlAttribute("OutlookLocale")]
		public string OutlookLocale { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0009B916 File Offset: 0x00099B16
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x0009B91E File Offset: 0x00099B1E
		[XmlElement("PolicyNudgeRules")]
		public PolicyNudgeRules15 PolicyNudgeRules { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0009B927 File Offset: 0x00099B27
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x0009B92F File Offset: 0x00099B2F
		[XmlElement("ClassificationItems")]
		public ClassificationItems15 ClassificationItems { get; set; }

		// Token: 0x06001B2A RID: 6954 RVA: 0x0009B938 File Offset: 0x00099B38
		public void Accept(Visitor15 visitor)
		{
			visitor.Visit(this);
		}
	}
}
