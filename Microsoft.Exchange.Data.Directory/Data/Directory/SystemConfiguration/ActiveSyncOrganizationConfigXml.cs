using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000313 RID: 787
	[XmlType(TypeName = "ActiveSyncOrganizationConfig")]
	[Serializable]
	public class ActiveSyncOrganizationConfigXml : XMLSerializableBase
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x0009B584 File Offset: 0x00099784
		// (set) Token: 0x06002443 RID: 9283 RVA: 0x0009B58C File Offset: 0x0009978C
		[XmlElement("DeviceFiltering")]
		public ActiveSyncDeviceFilterArray DeviceFiltering { get; set; }
	}
}
