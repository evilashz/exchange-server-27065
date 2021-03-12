using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200009E RID: 158
	[Parent("application")]
	[DataContract(Name = "MediaRelayToken")]
	[Get(typeof(MediaRelayTokenResource))]
	internal class MediaRelayTokenResource : Resource
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000A744 File Offset: 0x00008944
		public MediaRelayTokenResource(string selfUri) : base(selfUri)
		{
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000A74D File Offset: 0x0000894D
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000A75A File Offset: 0x0000895A
		[DataMember(Name = "OwnerUri", EmitDefaultValue = false)]
		public string OwnerUri
		{
			get
			{
				return base.GetValue<string>("OwnerUri");
			}
			set
			{
				base.SetValue<string>("OwnerUri", value);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000A768 File Offset: 0x00008968
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000A775 File Offset: 0x00008975
		[DataMember(Name = "MediaRelays")]
		public ResourceCollection<MediaRelay> MediaRelays
		{
			get
			{
				return base.GetValue<ResourceCollection<MediaRelay>>("MediaRelays");
			}
			set
			{
				base.SetValue<ResourceCollection<MediaRelay>>("MediaRelays", value);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000A783 File Offset: 0x00008983
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000A790 File Offset: 0x00008990
		[DataMember(Name = "UserName", EmitDefaultValue = false)]
		public string UserName
		{
			get
			{
				return base.GetValue<string>("UserName");
			}
			set
			{
				base.SetValue<string>("UserName", value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000A79E File Offset: 0x0000899E
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000A7AB File Offset: 0x000089AB
		[DataMember(Name = "Password", EmitDefaultValue = false)]
		public string Password
		{
			get
			{
				return base.GetValue<string>("Password");
			}
			set
			{
				base.SetValue<string>("Password", value);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A7B9 File Offset: 0x000089B9
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000A7C6 File Offset: 0x000089C6
		[DataMember(Name = "Duration", EmitDefaultValue = false)]
		public int Duration
		{
			get
			{
				return base.GetValue<int>("Duration");
			}
			set
			{
				base.SetValue<int>("Duration", value);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000A7D9 File Offset: 0x000089D9
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000A7E6 File Offset: 0x000089E6
		[DataMember(Name = "ValidTo", EmitDefaultValue = false)]
		public DateTime ValidTo
		{
			get
			{
				return base.GetValue<DateTime>("ValidTo");
			}
			set
			{
				base.SetValue<DateTime>("ValidTo", value);
			}
		}

		// Token: 0x040002AD RID: 685
		public const string Token = "mrastoken";
	}
}
