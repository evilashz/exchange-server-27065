using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000C3 RID: 195
	internal abstract class ConfigurationBase
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00019FCA File Offset: 0x000181CA
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00019FD2 File Offset: 0x000181D2
		public AttachmentPolicy AttachmentPolicy
		{
			get
			{
				return this.attachmentPolicy;
			}
			protected set
			{
				this.attachmentPolicy = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00019FDB File Offset: 0x000181DB
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00019FE3 File Offset: 0x000181E3
		public ulong SegmentationFlags
		{
			get
			{
				return this.segmentationFlags;
			}
			protected set
			{
				this.segmentationFlags = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00019FEC File Offset: 0x000181EC
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00019FF4 File Offset: 0x000181F4
		public string DefaultTheme
		{
			get
			{
				return this.defaultTheme;
			}
			protected set
			{
				this.defaultTheme = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00019FFD File Offset: 0x000181FD
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0001A005 File Offset: 0x00018205
		public bool UseGB18030
		{
			get
			{
				return this.useGB18030;
			}
			protected set
			{
				this.useGB18030 = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001A00E File Offset: 0x0001820E
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0001A016 File Offset: 0x00018216
		public bool UseISO885915
		{
			get
			{
				return this.useISO885915;
			}
			protected set
			{
				this.useISO885915 = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001A01F File Offset: 0x0001821F
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0001A027 File Offset: 0x00018227
		public OutboundCharsetOptions OutboundCharset
		{
			get
			{
				return this.outboundCharset;
			}
			protected set
			{
				this.outboundCharset = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001A030 File Offset: 0x00018230
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0001A038 File Offset: 0x00018238
		public InstantMessagingTypeOptions InstantMessagingType
		{
			get
			{
				return this.instantMessagingType;
			}
			protected set
			{
				this.instantMessagingType = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001A041 File Offset: 0x00018241
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0001A049 File Offset: 0x00018249
		public bool InstantMessagingEnabled { get; protected set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001A052 File Offset: 0x00018252
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0001A05A File Offset: 0x0001825A
		public bool PlacesEnabled { get; protected set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001A063 File Offset: 0x00018263
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0001A06B File Offset: 0x0001826B
		public bool WeatherEnabled { get; protected set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001A074 File Offset: 0x00018274
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0001A07C File Offset: 0x0001827C
		public bool AllowCopyContactsToDeviceAddressBook { get; protected set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001A085 File Offset: 0x00018285
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0001A08D File Offset: 0x0001828D
		public AllowOfflineOnEnum AllowOfflineOn { get; protected set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0001A096 File Offset: 0x00018296
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x0001A09E File Offset: 0x0001829E
		public bool RecoverDeletedItemsEnabled { get; protected set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001A0A7 File Offset: 0x000182A7
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001A0AF File Offset: 0x000182AF
		public bool GroupCreationEnabled { get; protected set; }

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001A0B8 File Offset: 0x000182B8
		protected static AttachmentPolicyLevel AttachmentActionToPolicyLevel(AttachmentBlockingActions? action)
		{
			AttachmentBlockingActions valueOrDefault = action.GetValueOrDefault();
			if (action != null)
			{
				switch (valueOrDefault)
				{
				case AttachmentBlockingActions.Allow:
					return AttachmentPolicyLevel.Allow;
				case AttachmentBlockingActions.ForceSave:
					return AttachmentPolicyLevel.ForceSave;
				case AttachmentBlockingActions.Block:
					return AttachmentPolicyLevel.Block;
				}
			}
			return AttachmentPolicyLevel.Block;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001A0F0 File Offset: 0x000182F0
		protected static ulong SetSegmentationFlags(int segmentationBits1, int segmentationBits2)
		{
			return (ulong)segmentationBits1 + (ulong)((ulong)((long)segmentationBits2) << 32);
		}

		// Token: 0x04000449 RID: 1097
		private AttachmentPolicy attachmentPolicy;

		// Token: 0x0400044A RID: 1098
		private string defaultTheme;

		// Token: 0x0400044B RID: 1099
		private bool useGB18030;

		// Token: 0x0400044C RID: 1100
		private bool useISO885915;

		// Token: 0x0400044D RID: 1101
		private OutboundCharsetOptions outboundCharset = OutboundCharsetOptions.AutoDetect;

		// Token: 0x0400044E RID: 1102
		private ulong segmentationFlags;

		// Token: 0x0400044F RID: 1103
		private InstantMessagingTypeOptions instantMessagingType;
	}
}
