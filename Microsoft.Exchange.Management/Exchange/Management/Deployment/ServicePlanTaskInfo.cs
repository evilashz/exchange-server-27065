using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200018C RID: 396
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ServicePlanTaskInfo : OrgTaskInfo
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x00041CD1 File Offset: 0x0003FED1
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x00041CD9 File Offset: 0x0003FED9
		[XmlAttribute]
		public string FeatureName { get; set; }
	}
}
