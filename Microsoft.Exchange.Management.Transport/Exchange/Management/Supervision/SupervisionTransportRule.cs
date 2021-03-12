using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	public class SupervisionTransportRule : TransportRule
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00013F7D File Offset: 0x0001217D
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x00013F8F File Offset: 0x0001218F
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
			set
			{
				this[ADObjectSchema.Name] = value;
			}
		}
	}
}
