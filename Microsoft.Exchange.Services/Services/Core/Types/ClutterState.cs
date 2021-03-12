using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005AD RID: 1453
	[XmlType(TypeName = "ClutterState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ClutterState
	{
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000AECD7 File Offset: 0x000ACED7
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x000AECDF File Offset: 0x000ACEDF
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		[XmlElement]
		public bool IsClutterEnabled { get; set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000AECE8 File Offset: 0x000ACEE8
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000AECF0 File Offset: 0x000ACEF0
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		[XmlElement]
		public bool IsClutterEligible { get; set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000AECF9 File Offset: 0x000ACEF9
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x000AED01 File Offset: 0x000ACF01
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		[XmlElement]
		public bool IsClassificationEnabled { get; set; }
	}
}
