using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000310 RID: 784
	[Serializable]
	public class ActiveSyncDeviceFilterArray : XMLSerializableBase
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x0009B455 File Offset: 0x00099655
		// (set) Token: 0x06002434 RID: 9268 RVA: 0x0009B45D File Offset: 0x0009965D
		[XmlArray("DeviceFilters")]
		[XmlArrayItem("DeviceFilter")]
		public List<ActiveSyncDeviceFilter> DeviceFilters { get; set; }
	}
}
