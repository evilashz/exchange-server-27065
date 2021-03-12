using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200009D RID: 157
	[DataContract(Name = "MediaRelay")]
	internal class MediaRelay : Resource
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public MediaRelay(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000A6C9 File Offset: 0x000088C9
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0000A6D6 File Offset: 0x000088D6
		[DataMember(Name = "Location", EmitDefaultValue = false)]
		public RelayLocation Location
		{
			get
			{
				return base.GetValue<RelayLocation>("Location");
			}
			set
			{
				base.SetValue<RelayLocation>("Location", value);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000A6E9 File Offset: 0x000088E9
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000A6F6 File Offset: 0x000088F6
		[DataMember(Name = "Host", EmitDefaultValue = false)]
		public string Host
		{
			get
			{
				return base.GetValue<string>("Host");
			}
			set
			{
				base.SetValue<string>("Host", value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000A704 File Offset: 0x00008904
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000A711 File Offset: 0x00008911
		[DataMember(Name = "TcpPort", EmitDefaultValue = false)]
		public int TcpPort
		{
			get
			{
				return base.GetValue<int>("TcpPort");
			}
			set
			{
				base.SetValue<int>("TcpPort", value);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000A724 File Offset: 0x00008924
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000A731 File Offset: 0x00008931
		[DataMember(Name = "UdpPort", EmitDefaultValue = false)]
		public int UdpPort
		{
			get
			{
				return base.GetValue<int>("UdpPort");
			}
			set
			{
				base.SetValue<int>("UdpPort", value);
			}
		}
	}
}
