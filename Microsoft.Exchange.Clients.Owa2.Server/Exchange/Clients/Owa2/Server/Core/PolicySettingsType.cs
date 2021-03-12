using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003F4 RID: 1012
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PolicySettingsType
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0007922A File Offset: 0x0007742A
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x00079232 File Offset: 0x00077432
		public OutboundCharsetOptions OutboundCharset
		{
			get
			{
				return this.outboundCharset;
			}
			set
			{
				this.outboundCharset = value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0007923B File Offset: 0x0007743B
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x0007924D File Offset: 0x0007744D
		[DataMember(Name = "OutboundCharset")]
		public string OutboundCharsetString
		{
			get
			{
				return this.outboundCharset.ToString();
			}
			set
			{
				this.outboundCharset = (OutboundCharsetOptions)Enum.Parse(typeof(OutboundCharsetOptions), value);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0007926A File Offset: 0x0007746A
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x00079272 File Offset: 0x00077472
		[DataMember]
		public bool UseGB18030
		{
			get
			{
				return this.useGB18030;
			}
			set
			{
				this.useGB18030 = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0007927B File Offset: 0x0007747B
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x00079283 File Offset: 0x00077483
		[DataMember]
		public bool UseISO885915
		{
			get
			{
				return this.useISO885915;
			}
			set
			{
				this.useISO885915 = value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x0007928C File Offset: 0x0007748C
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x00079294 File Offset: 0x00077494
		public InstantMessagingTypeOptions InstantMessagingType
		{
			get
			{
				return this.instantMessagingType;
			}
			set
			{
				this.instantMessagingType = value;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x0007929D File Offset: 0x0007749D
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x000792AF File Offset: 0x000774AF
		[DataMember(Name = "InstantMessagingType")]
		public string InstantMessagingTypeString
		{
			get
			{
				return this.instantMessagingType.ToString();
			}
			set
			{
				this.instantMessagingType = (InstantMessagingTypeOptions)Enum.Parse(typeof(InstantMessagingTypeOptions), value);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000792CC File Offset: 0x000774CC
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x000792D4 File Offset: 0x000774D4
		[DataMember]
		public string DefaultTheme
		{
			get
			{
				return this.defaultTheme;
			}
			set
			{
				this.defaultTheme = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x000792DD File Offset: 0x000774DD
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x000792E5 File Offset: 0x000774E5
		[DataMember(Name = "PlacesEnabled")]
		public bool PlacesEnabled
		{
			get
			{
				return this.placesEnabled;
			}
			set
			{
				this.placesEnabled = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x000792EE File Offset: 0x000774EE
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x000792F6 File Offset: 0x000774F6
		[DataMember(Name = "WeatherEnabled")]
		public bool WeatherEnabled
		{
			get
			{
				return this.weatherEnabled;
			}
			set
			{
				this.weatherEnabled = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x000792FF File Offset: 0x000774FF
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x00079307 File Offset: 0x00077507
		[DataMember(Name = "AllowCopyContactsToDeviceAddressBook")]
		public bool AllowCopyContactsToDeviceAddressBook
		{
			get
			{
				return this.allowCopyContactsToDeviceAddressBook;
			}
			set
			{
				this.allowCopyContactsToDeviceAddressBook = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x00079310 File Offset: 0x00077510
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x00079318 File Offset: 0x00077518
		public AllowOfflineOnEnum AllowOfflineOn
		{
			get
			{
				return this.allowOfflineOn;
			}
			set
			{
				this.allowOfflineOn = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x00079321 File Offset: 0x00077521
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x00079333 File Offset: 0x00077533
		[DataMember(Name = "AllowOfflineOn")]
		public string AllowOfflineOnString
		{
			get
			{
				return this.allowOfflineOn.ToString();
			}
			set
			{
				this.allowOfflineOn = (AllowOfflineOnEnum)Enum.Parse(typeof(AllowOfflineOnEnum), value);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00079350 File Offset: 0x00077550
		// (set) Token: 0x060020ED RID: 8429 RVA: 0x00079358 File Offset: 0x00077558
		[DataMember]
		public string MySiteUrl { get; set; }

		// Token: 0x04001276 RID: 4726
		private OutboundCharsetOptions outboundCharset;

		// Token: 0x04001277 RID: 4727
		private bool useGB18030;

		// Token: 0x04001278 RID: 4728
		private bool useISO885915;

		// Token: 0x04001279 RID: 4729
		private InstantMessagingTypeOptions instantMessagingType;

		// Token: 0x0400127A RID: 4730
		private string defaultTheme;

		// Token: 0x0400127B RID: 4731
		private bool placesEnabled;

		// Token: 0x0400127C RID: 4732
		private bool weatherEnabled;

		// Token: 0x0400127D RID: 4733
		private bool allowCopyContactsToDeviceAddressBook;

		// Token: 0x0400127E RID: 4734
		private AllowOfflineOnEnum allowOfflineOn;
	}
}
