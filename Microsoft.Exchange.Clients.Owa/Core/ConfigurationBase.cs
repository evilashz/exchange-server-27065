using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000F4 RID: 244
	public abstract class ConfigurationBase
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0003B90C File Offset: 0x00039B0C
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0003B914 File Offset: 0x00039B14
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0003B91D File Offset: 0x00039B1D
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0003B925 File Offset: 0x00039B25
		internal AuthenticationMethod InternalAuthenticationMethod
		{
			get
			{
				return this.internalAuthenticationMethod;
			}
			set
			{
				this.internalAuthenticationMethod = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0003B92E File Offset: 0x00039B2E
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0003B936 File Offset: 0x00039B36
		internal AuthenticationMethod ExternalAuthenticationMethod
		{
			get
			{
				return this.externalAuthenticationMethod;
			}
			set
			{
				this.externalAuthenticationMethod = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0003B93F File Offset: 0x00039B3F
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0003B947 File Offset: 0x00039B47
		internal Uri Exchange2003Url
		{
			get
			{
				return this.exchange2003Url;
			}
			set
			{
				this.exchange2003Url = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0003B950 File Offset: 0x00039B50
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0003B958 File Offset: 0x00039B58
		internal LegacyRedirectTypeOptions LegacyRedirectType
		{
			get
			{
				return this.legacyRedirectType;
			}
			set
			{
				this.legacyRedirectType = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0003B961 File Offset: 0x00039B61
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0003B969 File Offset: 0x00039B69
		public int DefaultClientLanguage
		{
			get
			{
				return this.defaultClientLanguage;
			}
			protected set
			{
				this.defaultClientLanguage = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0003B972 File Offset: 0x00039B72
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0003B97A File Offset: 0x00039B7A
		public int LogonAndErrorLanguage
		{
			get
			{
				return this.logonAndErrorLanguage;
			}
			protected set
			{
				this.logonAndErrorLanguage = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0003B983 File Offset: 0x00039B83
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x0003B98B File Offset: 0x00039B8B
		public bool PhoneticSupportEnabled
		{
			get
			{
				return this.phoneticSupportEnabled;
			}
			protected set
			{
				this.phoneticSupportEnabled = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0003B994 File Offset: 0x00039B94
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x0003B99C File Offset: 0x00039B9C
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

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0003B9A5 File Offset: 0x00039BA5
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0003B9AD File Offset: 0x00039BAD
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0003B9B6 File Offset: 0x00039BB6
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0003B9BE File Offset: 0x00039BBE
		public string SetPhotoURL
		{
			get
			{
				return this.setPhotoURL;
			}
			protected set
			{
				this.setPhotoURL = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0003B9C7 File Offset: 0x00039BC7
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0003B9CF File Offset: 0x00039BCF
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

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0003B9E0 File Offset: 0x00039BE0
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

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0003B9E9 File Offset: 0x00039BE9
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0003B9F1 File Offset: 0x00039BF1
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

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0003B9FA File Offset: 0x00039BFA
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0003BA02 File Offset: 0x00039C02
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

		// Token: 0x06000815 RID: 2069 RVA: 0x0003BA0C File Offset: 0x00039C0C
		internal static AuthenticationMethod GetAuthenticationMethod(object authenticationMethodObject)
		{
			if (authenticationMethodObject is AuthenticationMethodFlags)
			{
				AuthenticationMethod authenticationMethod = (AuthenticationMethod)authenticationMethodObject;
				if (EnumValidator.IsValidValue<AuthenticationMethod>(authenticationMethod))
				{
					return authenticationMethod;
				}
			}
			return AuthenticationMethod.None;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0003BA34 File Offset: 0x00039C34
		protected static AttachmentPolicy.Level AttachmentActionToLevel(AttachmentBlockingActions? action)
		{
			AttachmentBlockingActions valueOrDefault = action.GetValueOrDefault();
			if (action != null)
			{
				switch (valueOrDefault)
				{
				case AttachmentBlockingActions.Allow:
					return AttachmentPolicy.Level.Allow;
				case AttachmentBlockingActions.ForceSave:
					return AttachmentPolicy.Level.ForceSave;
				case AttachmentBlockingActions.Block:
					return AttachmentPolicy.Level.Block;
				}
			}
			return AttachmentPolicy.Level.Block;
		}

		// Token: 0x040005B9 RID: 1465
		private AttachmentPolicy attachmentPolicy;

		// Token: 0x040005BA RID: 1466
		private int defaultClientLanguage;

		// Token: 0x040005BB RID: 1467
		private int logonAndErrorLanguage;

		// Token: 0x040005BC RID: 1468
		private bool phoneticSupportEnabled;

		// Token: 0x040005BD RID: 1469
		private string defaultTheme;

		// Token: 0x040005BE RID: 1470
		private string setPhotoURL;

		// Token: 0x040005BF RID: 1471
		private bool useGB18030;

		// Token: 0x040005C0 RID: 1472
		private bool useISO885915;

		// Token: 0x040005C1 RID: 1473
		private OutboundCharsetOptions outboundCharset = OutboundCharsetOptions.AutoDetect;

		// Token: 0x040005C2 RID: 1474
		private ulong segmentationFlags;

		// Token: 0x040005C3 RID: 1475
		private InstantMessagingTypeOptions instantMessagingType;

		// Token: 0x040005C4 RID: 1476
		private LegacyRedirectTypeOptions legacyRedirectType;

		// Token: 0x040005C5 RID: 1477
		private AuthenticationMethod internalAuthenticationMethod;

		// Token: 0x040005C6 RID: 1478
		private AuthenticationMethod externalAuthenticationMethod;

		// Token: 0x040005C7 RID: 1479
		private Uri exchange2003Url;
	}
}
