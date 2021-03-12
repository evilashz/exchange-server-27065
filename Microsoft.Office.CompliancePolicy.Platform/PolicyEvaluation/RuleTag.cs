using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E1 RID: 225
	public class RuleTag
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x00012CBA File Offset: 0x00010EBA
		public RuleTag(string name, string tagType)
		{
			this.Name = name;
			this.TagType = tagType;
			this.Data = new Dictionary<string, string>();
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00012CDB File Offset: 0x00010EDB
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00012CE3 File Offset: 0x00010EE3
		public int Size { get; protected set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00012CEC File Offset: 0x00010EEC
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public string Name { get; protected set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00012CFD File Offset: 0x00010EFD
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00012D05 File Offset: 0x00010F05
		public string TagType { get; protected set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00012D0E File Offset: 0x00010F0E
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x00012D16 File Offset: 0x00010F16
		public Dictionary<string, string> Data { get; protected set; }
	}
}
