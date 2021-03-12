using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E2F RID: 3631
	public class GetSyncStateResult
	{
		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06007D9D RID: 32157 RVA: 0x0022A1EC File Offset: 0x002283EC
		// (set) Token: 0x06007D9E RID: 32158 RVA: 0x0022A1F4 File Offset: 0x002283F4
		[XmlAttribute]
		public bool LoggingEnabled { get; set; }

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06007D9F RID: 32159 RVA: 0x0022A1FD File Offset: 0x002283FD
		// (set) Token: 0x06007DA0 RID: 32160 RVA: 0x0022A205 File Offset: 0x00228405
		public List<DeviceData> Devices { get; set; }
	}
}
