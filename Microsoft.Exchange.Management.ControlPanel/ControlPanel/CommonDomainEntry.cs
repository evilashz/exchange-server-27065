using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000058 RID: 88
	[DataContract]
	public class CommonDomainEntry
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x0005395D File Offset: 0x00051B5D
		public CommonDomainEntry(SmartHost smartHost)
		{
			if (smartHost.IsIPAddress)
			{
				this.address = smartHost.Address.ToString();
				return;
			}
			this.address = smartHost.Domain.HostnameString;
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00053990 File Offset: 0x00051B90
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x00053998 File Offset: 0x00051B98
		[DataMember]
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x04001B01 RID: 6913
		private string address;
	}
}
