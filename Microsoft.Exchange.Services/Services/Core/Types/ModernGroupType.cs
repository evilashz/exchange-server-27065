using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F7 RID: 1527
	[DataContract(Name = "ModernGroupType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ModernGroupType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ModernGroupType
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000B3CD3 File Offset: 0x000B1ED3
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000B3CDB File Offset: 0x000B1EDB
		[XmlElement("SmtpAddress", typeof(string))]
		[DataMember(Name = "SmtpAddress", EmitDefaultValue = false)]
		public string SmtpAddress { get; set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000B3CE4 File Offset: 0x000B1EE4
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x000B3CEC File Offset: 0x000B1EEC
		[DataMember(Name = "DisplayName", EmitDefaultValue = false)]
		[XmlElement("DisplayName", typeof(string))]
		public string DisplayName { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x000B3CF5 File Offset: 0x000B1EF5
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x000B3CFD File Offset: 0x000B1EFD
		[XmlElement("IsPinned", typeof(bool))]
		[DataMember(Name = "IsPinned", EmitDefaultValue = false)]
		public bool IsPinned { get; set; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002F16 RID: 12054 RVA: 0x000B3D06 File Offset: 0x000B1F06
		// (set) Token: 0x06002F17 RID: 12055 RVA: 0x000B3D0E File Offset: 0x000B1F0E
		[XmlElement("GroupObjectType", typeof(ModernGroupObjectType))]
		[DataMember(Name = "GroupObjectType", EmitDefaultValue = false)]
		public ModernGroupObjectType GroupObjectType { get; set; }
	}
}
