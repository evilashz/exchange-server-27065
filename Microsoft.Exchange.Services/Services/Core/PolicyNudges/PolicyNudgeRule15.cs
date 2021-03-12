using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C7 RID: 967
	[XmlType("PolicyNudgeRule")]
	public sealed class PolicyNudgeRule15 : IVisitee15, IVersionedItem, IOtherAttributes
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x0009B96B File Offset: 0x00099B6B
		public PolicyNudgeRule15()
		{
			this.OtherAttributes = new List<OtherAttribute>();
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0009B97E File Offset: 0x00099B7E
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0009B986 File Offset: 0x00099B86
		[XmlAttribute("id")]
		public string ID { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0009B98F File Offset: 0x00099B8F
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x0009B997 File Offset: 0x00099B97
		[XmlAttribute("version")]
		public long Version { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0009B9A0 File Offset: 0x00099BA0
		[XmlIgnore]
		DateTime IVersionedItem.Version
		{
			get
			{
				return DateTime.FromBinary(this.Version);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0009B9AD File Offset: 0x00099BAD
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0009B9B5 File Offset: 0x00099BB5
		[XmlIgnore]
		public List<OtherAttribute> OtherAttributes { get; private set; }

		// Token: 0x06001B38 RID: 6968 RVA: 0x0009B9BE File Offset: 0x00099BBE
		public void Accept(Visitor15 visitor)
		{
			visitor.Visit(this);
		}
	}
}
