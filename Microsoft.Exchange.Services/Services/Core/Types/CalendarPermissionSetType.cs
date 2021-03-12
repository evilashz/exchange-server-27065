using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000604 RID: 1540
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "CalendarPermissions")]
	[Serializable]
	public class CalendarPermissionSetType : BasePermissionSetType
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000B4005 File Offset: 0x000B2205
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x000B400D File Offset: 0x000B220D
		[XmlArrayItem("CalendarPermission", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		public CalendarPermissionType[] CalendarPermissions { get; set; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000B4016 File Offset: 0x000B2216
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000B401E File Offset: 0x000B221E
		[XmlArrayItem("UnknownEntry", IsNullable = false)]
		[DataMember(EmitDefaultValue = false)]
		public string[] UnknownEntries { get; set; }
	}
}
