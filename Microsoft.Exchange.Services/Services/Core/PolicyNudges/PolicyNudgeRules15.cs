using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C6 RID: 966
	[XmlType("PolicyNudgeRules")]
	public sealed class PolicyNudgeRules15 : IVisitee15
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0009B949 File Offset: 0x00099B49
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x0009B951 File Offset: 0x00099B51
		[XmlElement("PolicyNudgeRule")]
		public List<PolicyNudgeRule15> Rules { get; set; }

		// Token: 0x06001B2E RID: 6958 RVA: 0x0009B95A File Offset: 0x00099B5A
		public void Accept(Visitor15 visitor)
		{
			visitor.Visit(this);
		}
	}
}
