using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000603 RID: 1539
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Permissions")]
	[Serializable]
	public class PermissionSetType : BasePermissionSetType
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000B3FDB File Offset: 0x000B21DB
		// (set) Token: 0x06002F52 RID: 12114 RVA: 0x000B3FE3 File Offset: 0x000B21E3
		[DataMember(EmitDefaultValue = false)]
		[XmlArrayItem("Permission", IsNullable = false)]
		public PermissionType[] Permissions { get; set; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000B3FEC File Offset: 0x000B21EC
		// (set) Token: 0x06002F54 RID: 12116 RVA: 0x000B3FF4 File Offset: 0x000B21F4
		[XmlArrayItem("UnknownEntry", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		public string[] UnknownEntries { get; set; }
	}
}
