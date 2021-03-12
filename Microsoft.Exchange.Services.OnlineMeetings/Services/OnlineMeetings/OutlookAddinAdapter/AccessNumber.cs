using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C1 RID: 193
	[XmlType("AccessNumber")]
	[DataContract(Name = "AccessNumber")]
	[KnownType(typeof(AccessNumber))]
	public class AccessNumber
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000BC04 File Offset: 0x00009E04
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000BC0C File Offset: 0x00009E0C
		[DataMember(Name = "Number", EmitDefaultValue = true)]
		[XmlAttribute("Number")]
		public string Number { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000BC15 File Offset: 0x00009E15
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000BC1D File Offset: 0x00009E1D
		[DataMember(Name = "LanguageID", EmitDefaultValue = true)]
		[XmlAttribute("LanguageID")]
		public int LanguageID { get; set; }
	}
}
