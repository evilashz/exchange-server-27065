using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C8 RID: 968
	[XmlType("ClassificationItems")]
	public sealed class ClassificationItems15 : IVisitee15
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x0009B9C7 File Offset: 0x00099BC7
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x0009B9CF File Offset: 0x00099BCF
		[XmlAttribute("EngineVersion")]
		public string EngineVersion { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0009B9D8 File Offset: 0x00099BD8
		// (set) Token: 0x06001B3C RID: 6972 RVA: 0x0009B9E0 File Offset: 0x00099BE0
		[XmlElement("ClassificationDefinitions")]
		public ClassificationDefinitions15 ClassificationDefinitions { get; set; }

		// Token: 0x06001B3D RID: 6973 RVA: 0x0009B9E9 File Offset: 0x00099BE9
		public void Accept(Visitor15 visitor)
		{
			visitor.Visit(this);
		}
	}
}
