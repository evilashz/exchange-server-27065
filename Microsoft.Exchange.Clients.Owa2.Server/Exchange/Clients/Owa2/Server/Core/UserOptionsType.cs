using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000413 RID: 1043
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UserOptionsType : UserConfigurationBaseType
	{
		// Token: 0x060022A5 RID: 8869 RVA: 0x0007F295 File Offset: 0x0007D495
		public UserOptionsType() : base("OWA.UserOptions")
		{
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0007F2AC File Offset: 0x0007D4AC
		private static ExTimeZone LegacyDataCenterTimeZone
		{
			get
			{
				if (UserOptionsType.legacyDataCenterTimeZone == null)
				{
					ExTimeZone exTimeZone;
					ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName("Greenwich Standard Time", out exTimeZone);
					UserOptionsType.legacyDataCenterTimeZone = exTimeZone;
				}
				return UserOptionsType.legacyDataCenterTimeZone;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0007F2DD File Offset: 0x0007D4DD
		// (set) Token: 0x060022A8 RID: 8872 RVA: 0x0007F2EB File Offset: 0x0007D4EB
		[DataMember]
		public string TimeZone
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.TimeZone];
			}
			set
			{
				base[UserConfigurationPropertyId.TimeZone] = value;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0007F2F5 File Offset: 0x0007D4F5
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x0007F303 File Offset: 0x0007D503
		[DataMember]
		public string TimeFormat
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.TimeFormat];
			}
			set
			{
				base[UserConfigurationPropertyId.TimeFormat] = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0007F30D File Offset: 0x0007D50D
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x0007F31B File Offset: 0x0007D51B
		[DataMember]
		public string DateFormat
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.DateFormat];
			}
			set
			{
				base[UserConfigurationPropertyId.DateFormat] = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0007F325 File Offset: 0x0007D525
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x0007F333 File Offset: 0x0007D533
		[DataMember]
		public int WeekStartDay
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.WeekStartDay];
			}
			set
			{
				base[UserConfigurationPropertyId.WeekStartDay] = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0007F342 File Offset: 0x0007D542
		// (set) Token: 0x060022B0 RID: 8880 RVA: 0x0007F350 File Offset: 0x0007D550
		[DataMember]
		public int HourIncrement
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.HourIncrement];
			}
			set
			{
				base[UserConfigurationPropertyId.HourIncrement] = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x0007F35F File Offset: 0x0007D55F
		// (set) Token: 0x060022B2 RID: 8882 RVA: 0x0007F36D File Offset: 0x0007D56D
		[DataMember]
		public bool ShowWeekNumbers
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ShowWeekNumbers];
			}
			set
			{
				base[UserConfigurationPropertyId.ShowWeekNumbers] = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0007F37C File Offset: 0x0007D57C
		// (set) Token: 0x060022B4 RID: 8884 RVA: 0x0007F38A File Offset: 0x0007D58A
		[DataMember]
		public bool CheckNameInContactsFirst
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CheckNameInContactsFirst];
			}
			set
			{
				base[UserConfigurationPropertyId.CheckNameInContactsFirst] = value;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x0007F399 File Offset: 0x0007D599
		// (set) Token: 0x060022B6 RID: 8886 RVA: 0x0007F3AC File Offset: 0x0007D5AC
		[DataMember]
		public CalendarWeekRule FirstWeekOfYear
		{
			get
			{
				return ((FirstWeekRules)base[UserConfigurationPropertyId.FirstWeekOfYear]).ToCalendarWeekRule();
			}
			set
			{
				base[UserConfigurationPropertyId.FirstWeekOfYear] = value.ToFirstWeekRules();
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x0007F3C0 File Offset: 0x0007D5C0
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x0007F3CE File Offset: 0x0007D5CE
		[DataMember]
		public bool EnableReminders
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.EnableReminders];
			}
			set
			{
				base[UserConfigurationPropertyId.EnableReminders] = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0007F3DD File Offset: 0x0007D5DD
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x0007F3EC File Offset: 0x0007D5EC
		[DataMember]
		public bool EnableReminderSound
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.EnableReminderSound];
			}
			set
			{
				base[UserConfigurationPropertyId.EnableReminderSound] = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x0007F3FC File Offset: 0x0007D5FC
		// (set) Token: 0x060022BC RID: 8892 RVA: 0x0007F40B File Offset: 0x0007D60B
		[DataMember]
		public NewNotification NewItemNotify
		{
			get
			{
				return (NewNotification)base[UserConfigurationPropertyId.NewItemNotify];
			}
			set
			{
				base[UserConfigurationPropertyId.NewItemNotify] = value;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x0007F41B File Offset: 0x0007D61B
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x0007F42A File Offset: 0x0007D62A
		[DataMember]
		public int ViewRowCount
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.ViewRowCount];
			}
			set
			{
				base[UserConfigurationPropertyId.ViewRowCount] = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0007F43A File Offset: 0x0007D63A
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x0007F449 File Offset: 0x0007D649
		[DataMember]
		public int SpellingDictionaryLanguage
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.SpellingDictionaryLanguage];
			}
			set
			{
				base[UserConfigurationPropertyId.SpellingDictionaryLanguage] = value;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0007F459 File Offset: 0x0007D659
		// (set) Token: 0x060022C2 RID: 8898 RVA: 0x0007F468 File Offset: 0x0007D668
		[DataMember]
		public bool SpellingIgnoreUppercase
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.SpellingIgnoreUppercase];
			}
			set
			{
				base[UserConfigurationPropertyId.SpellingIgnoreUppercase] = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x0007F478 File Offset: 0x0007D678
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x0007F487 File Offset: 0x0007D687
		[DataMember]
		public bool SpellingIgnoreMixedDigits
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.SpellingIgnoreMixedDigits];
			}
			set
			{
				base[UserConfigurationPropertyId.SpellingIgnoreMixedDigits] = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x0007F497 File Offset: 0x0007D697
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x0007F4A6 File Offset: 0x0007D6A6
		[DataMember]
		public bool SpellingCheckBeforeSend
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.SpellingCheckBeforeSend];
			}
			set
			{
				base[UserConfigurationPropertyId.SpellingCheckBeforeSend] = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x0007F4B6 File Offset: 0x0007D6B6
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x0007F4C5 File Offset: 0x0007D6C5
		[DataMember]
		public bool SmimeEncrypt
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.SmimeEncrypt];
			}
			set
			{
				base[UserConfigurationPropertyId.SmimeEncrypt] = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0007F4D5 File Offset: 0x0007D6D5
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x0007F4E4 File Offset: 0x0007D6E4
		[DataMember]
		public bool SmimeSign
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.SmimeSign];
			}
			set
			{
				base[UserConfigurationPropertyId.SmimeSign] = value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0007F4F4 File Offset: 0x0007D6F4
		// (set) Token: 0x060022CC RID: 8908 RVA: 0x0007F503 File Offset: 0x0007D703
		[DataMember]
		public bool AlwaysShowBcc
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.AlwaysShowBcc];
			}
			set
			{
				base[UserConfigurationPropertyId.AlwaysShowBcc] = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x0007F513 File Offset: 0x0007D713
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x0007F522 File Offset: 0x0007D722
		[DataMember]
		public bool AlwaysShowFrom
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.AlwaysShowFrom];
			}
			set
			{
				base[UserConfigurationPropertyId.AlwaysShowFrom] = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x0007F532 File Offset: 0x0007D732
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x0007F541 File Offset: 0x0007D741
		public Markup ComposeMarkup
		{
			get
			{
				return (Markup)base[UserConfigurationPropertyId.ComposeMarkup];
			}
			set
			{
				base[UserConfigurationPropertyId.ComposeMarkup] = value;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0007F551 File Offset: 0x0007D751
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0007F563 File Offset: 0x0007D763
		[DataMember(Name = "ComposeMarkup")]
		public string ComposeMarkupString
		{
			get
			{
				return this.ComposeMarkup.ToString();
			}
			set
			{
				this.ComposeMarkup = (Markup)Enum.Parse(typeof(Markup), value);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0007F580 File Offset: 0x0007D780
		// (set) Token: 0x060022D4 RID: 8916 RVA: 0x0007F58F File Offset: 0x0007D78F
		[DataMember]
		public string ComposeFontName
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.ComposeFontName];
			}
			set
			{
				base[UserConfigurationPropertyId.ComposeFontName] = value;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0007F59A File Offset: 0x0007D79A
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0007F5A9 File Offset: 0x0007D7A9
		[DataMember]
		public int ComposeFontSize
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.ComposeFontSize];
			}
			set
			{
				base[UserConfigurationPropertyId.ComposeFontSize] = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0007F5B9 File Offset: 0x0007D7B9
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0007F5C8 File Offset: 0x0007D7C8
		[DataMember]
		public string ComposeFontColor
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.ComposeFontColor];
			}
			set
			{
				base[UserConfigurationPropertyId.ComposeFontColor] = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0007F5D3 File Offset: 0x0007D7D3
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x0007F5E2 File Offset: 0x0007D7E2
		[DataMember(Name = "ComposeFontFlags")]
		public FontFlags ComposeFontFlags
		{
			get
			{
				return (FontFlags)base[UserConfigurationPropertyId.ComposeFontFlags];
			}
			set
			{
				base[UserConfigurationPropertyId.ComposeFontFlags] = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x0007F5F2 File Offset: 0x0007D7F2
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x0007F601 File Offset: 0x0007D801
		[DataMember]
		public bool AutoAddSignature
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.AutoAddSignature];
			}
			set
			{
				base[UserConfigurationPropertyId.AutoAddSignature] = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0007F611 File Offset: 0x0007D811
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0007F620 File Offset: 0x0007D820
		[DataMember]
		public string SignatureText
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SignatureText];
			}
			set
			{
				base[UserConfigurationPropertyId.SignatureText] = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x0007F62B File Offset: 0x0007D82B
		// (set) Token: 0x060022E0 RID: 8928 RVA: 0x0007F63A File Offset: 0x0007D83A
		[DataMember]
		public string SignatureHtml
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SignatureHtml];
			}
			set
			{
				base[UserConfigurationPropertyId.SignatureHtml] = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0007F645 File Offset: 0x0007D845
		// (set) Token: 0x060022E2 RID: 8930 RVA: 0x0007F654 File Offset: 0x0007D854
		[DataMember]
		public bool AutoAddSignatureOnMobile
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.AutoAddSignatureOnMobile];
			}
			set
			{
				base[UserConfigurationPropertyId.AutoAddSignatureOnMobile] = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x0007F664 File Offset: 0x0007D864
		// (set) Token: 0x060022E4 RID: 8932 RVA: 0x0007F673 File Offset: 0x0007D873
		[DataMember]
		public string SignatureTextOnMobile
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SignatureTextOnMobile];
			}
			set
			{
				base[UserConfigurationPropertyId.SignatureTextOnMobile] = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x0007F67E File Offset: 0x0007D87E
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x0007F68D File Offset: 0x0007D88D
		[DataMember]
		public bool UseDesktopSignature
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.UseDesktopSignature];
			}
			set
			{
				base[UserConfigurationPropertyId.UseDesktopSignature] = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0007F69D File Offset: 0x0007D89D
		// (set) Token: 0x060022E8 RID: 8936 RVA: 0x0007F6AC File Offset: 0x0007D8AC
		[DataMember]
		public bool BlockExternalContent
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.BlockExternalContent];
			}
			set
			{
				base[UserConfigurationPropertyId.BlockExternalContent] = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0007F6BC File Offset: 0x0007D8BC
		// (set) Token: 0x060022EA RID: 8938 RVA: 0x0007F6CB File Offset: 0x0007D8CB
		public MarkAsRead PreviewMarkAsRead
		{
			get
			{
				return (MarkAsRead)base[UserConfigurationPropertyId.PreviewMarkAsRead];
			}
			set
			{
				base[UserConfigurationPropertyId.PreviewMarkAsRead] = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0007F6DB File Offset: 0x0007D8DB
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x0007F6ED File Offset: 0x0007D8ED
		[DataMember(Name = "PreviewMarkAsRead")]
		public string PreviewMarkAsReadString
		{
			get
			{
				return this.PreviewMarkAsRead.ToString();
			}
			set
			{
				this.PreviewMarkAsRead = (MarkAsRead)Enum.Parse(typeof(MarkAsRead), value);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x0007F70A File Offset: 0x0007D90A
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x0007F719 File Offset: 0x0007D919
		public EmailComposeMode EmailComposeMode
		{
			get
			{
				return (EmailComposeMode)base[UserConfigurationPropertyId.EmailComposeMode];
			}
			set
			{
				base[UserConfigurationPropertyId.EmailComposeMode] = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0007F729 File Offset: 0x0007D929
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0007F73B File Offset: 0x0007D93B
		[DataMember(Name = "EmailComposeMode")]
		public string EmailComposeModeString
		{
			get
			{
				return this.EmailComposeMode.ToString();
			}
			set
			{
				this.EmailComposeMode = (EmailComposeMode)Enum.Parse(typeof(EmailComposeMode), value);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x0007F758 File Offset: 0x0007D958
		// (set) Token: 0x060022F2 RID: 8946 RVA: 0x0007F767 File Offset: 0x0007D967
		[DataMember(EmitDefaultValue = false)]
		public string[] SendAsMruAddresses
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.SendAsMruAddresses];
			}
			set
			{
				base[UserConfigurationPropertyId.SendAsMruAddresses] = value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0007F772 File Offset: 0x0007D972
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x0007F781 File Offset: 0x0007D981
		[DataMember]
		public bool CheckForForgottenAttachments
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CheckForForgottenAttachments];
			}
			set
			{
				base[UserConfigurationPropertyId.CheckForForgottenAttachments] = value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0007F791 File Offset: 0x0007D991
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x0007F7A0 File Offset: 0x0007D9A0
		[DataMember]
		public int MarkAsReadDelaytime
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.MarkAsReadDelaytime];
			}
			set
			{
				base[UserConfigurationPropertyId.MarkAsReadDelaytime] = value;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x0007F7B0 File Offset: 0x0007D9B0
		// (set) Token: 0x060022F8 RID: 8952 RVA: 0x0007F7BF File Offset: 0x0007D9BF
		public NextSelectionDirection NextSelection
		{
			get
			{
				return (NextSelectionDirection)base[UserConfigurationPropertyId.NextSelection];
			}
			set
			{
				base[UserConfigurationPropertyId.NextSelection] = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0007F7CF File Offset: 0x0007D9CF
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0007F7E1 File Offset: 0x0007D9E1
		[DataMember(Name = "NextSelection")]
		public string NextSelectionString
		{
			get
			{
				return this.NextSelection.ToString();
			}
			set
			{
				this.NextSelection = (NextSelectionDirection)Enum.Parse(typeof(NextSelectionDirection), value);
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x0007F7FE File Offset: 0x0007D9FE
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x0007F80D File Offset: 0x0007DA0D
		public ReadReceiptResponse ReadReceipt
		{
			get
			{
				return (ReadReceiptResponse)base[UserConfigurationPropertyId.ReadReceipt];
			}
			set
			{
				base[UserConfigurationPropertyId.ReadReceipt] = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x0007F81D File Offset: 0x0007DA1D
		// (set) Token: 0x060022FE RID: 8958 RVA: 0x0007F82F File Offset: 0x0007DA2F
		[DataMember(Name = "ReadReceipt")]
		public string ReadReceiptString
		{
			get
			{
				return this.ReadReceipt.ToString();
			}
			set
			{
				this.ReadReceipt = (ReadReceiptResponse)Enum.Parse(typeof(ReadReceiptResponse), value);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0007F84C File Offset: 0x0007DA4C
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x0007F85B File Offset: 0x0007DA5B
		[DataMember]
		public bool EmptyDeletedItemsOnLogoff
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.EmptyDeletedItemsOnLogoff];
			}
			set
			{
				base[UserConfigurationPropertyId.EmptyDeletedItemsOnLogoff] = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0007F86B File Offset: 0x0007DA6B
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x0007F87A File Offset: 0x0007DA7A
		[DataMember]
		public int NavigationBarWidth
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.NavigationBarWidth];
			}
			set
			{
				base[UserConfigurationPropertyId.NavigationBarWidth] = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0007F88A File Offset: 0x0007DA8A
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x0007F899 File Offset: 0x0007DA99
		[DataMember]
		public string NavigationBarWidthRatio
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.NavigationBarWidthRatio];
			}
			set
			{
				base[UserConfigurationPropertyId.NavigationBarWidthRatio] = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0007F8A4 File Offset: 0x0007DAA4
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x0007F8B3 File Offset: 0x0007DAB3
		[DataMember]
		public bool MailFolderPaneExpanded
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.MailFolderPaneExpanded];
			}
			set
			{
				base[UserConfigurationPropertyId.MailFolderPaneExpanded] = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0007F8C3 File Offset: 0x0007DAC3
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x0007F8D2 File Offset: 0x0007DAD2
		[DataMember]
		public bool IsFavoritesFolderTreeCollapsed
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsFavoritesFolderTreeCollapsed];
			}
			set
			{
				base[UserConfigurationPropertyId.IsFavoritesFolderTreeCollapsed] = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0007F8E2 File Offset: 0x0007DAE2
		// (set) Token: 0x0600230A RID: 8970 RVA: 0x0007F8F1 File Offset: 0x0007DAF1
		[DataMember]
		public bool IsPeopleIKnowFolderTreeCollapsed
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsPeopleIKnowFolderTreeCollapsed];
			}
			set
			{
				base[UserConfigurationPropertyId.IsPeopleIKnowFolderTreeCollapsed] = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x0007F901 File Offset: 0x0007DB01
		// (set) Token: 0x0600230C RID: 8972 RVA: 0x0007F910 File Offset: 0x0007DB10
		[DataMember]
		public bool ShowReadingPaneOnFirstLoad
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ShowReadingPaneOnFirstLoad];
			}
			set
			{
				base[UserConfigurationPropertyId.ShowReadingPaneOnFirstLoad] = value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x0007F920 File Offset: 0x0007DB20
		// (set) Token: 0x0600230E RID: 8974 RVA: 0x0007F92F File Offset: 0x0007DB2F
		[DataMember]
		public NavigationPaneView NavigationPaneViewOption
		{
			get
			{
				return (NavigationPaneView)base[UserConfigurationPropertyId.NavigationPaneViewOption];
			}
			set
			{
				base[UserConfigurationPropertyId.NavigationPaneViewOption] = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x0007F93F File Offset: 0x0007DB3F
		// (set) Token: 0x06002310 RID: 8976 RVA: 0x0007F94E File Offset: 0x0007DB4E
		[DataMember]
		public bool IsMailRootFolderTreeCollapsed
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsMailRootFolderTreeCollapsed];
			}
			set
			{
				base[UserConfigurationPropertyId.IsMailRootFolderTreeCollapsed] = value;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x0007F95E File Offset: 0x0007DB5E
		// (set) Token: 0x06002312 RID: 8978 RVA: 0x0007F96D File Offset: 0x0007DB6D
		[DataMember]
		public bool IsMiniBarVisible
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsMiniBarVisible];
			}
			set
			{
				base[UserConfigurationPropertyId.IsMiniBarVisible] = value;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x0007F97D File Offset: 0x0007DB7D
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x0007F98C File Offset: 0x0007DB8C
		[DataMember]
		public bool IsQuickLinksBarVisible
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsQuickLinksBarVisible];
			}
			set
			{
				base[UserConfigurationPropertyId.IsQuickLinksBarVisible] = value;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0007F99C File Offset: 0x0007DB9C
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x0007F9AB File Offset: 0x0007DBAB
		[DataMember]
		public bool IsTaskDetailsVisible
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsTaskDetailsVisible];
			}
			set
			{
				base[UserConfigurationPropertyId.IsTaskDetailsVisible] = value;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0007F9BB File Offset: 0x0007DBBB
		// (set) Token: 0x06002318 RID: 8984 RVA: 0x0007F9CA File Offset: 0x0007DBCA
		[DataMember]
		public bool IsDocumentFavoritesVisible
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsDocumentFavoritesVisible];
			}
			set
			{
				base[UserConfigurationPropertyId.IsDocumentFavoritesVisible] = value;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0007F9DA File Offset: 0x0007DBDA
		// (set) Token: 0x0600231A RID: 8986 RVA: 0x0007F9E2 File Offset: 0x0007DBE2
		[DataMember]
		public bool IsOutlookSharedFoldersVisible { get; set; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0007F9EB File Offset: 0x0007DBEB
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x0007F9FA File Offset: 0x0007DBFA
		[DataMember(Name = "FormatBarState")]
		public FormatBarButtonGroups FormatBarState
		{
			get
			{
				return (FormatBarButtonGroups)base[UserConfigurationPropertyId.FormatBarState];
			}
			set
			{
				base[UserConfigurationPropertyId.FormatBarState] = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0007FA0A File Offset: 0x0007DC0A
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x0007FA19 File Offset: 0x0007DC19
		[DataMember]
		public string[] MruFonts
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.MruFonts];
			}
			set
			{
				base[UserConfigurationPropertyId.MruFonts] = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0007FA24 File Offset: 0x0007DC24
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x0007FA33 File Offset: 0x0007DC33
		[DataMember]
		public bool PrimaryNavigationCollapsed
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.PrimaryNavigationCollapsed];
			}
			set
			{
				base[UserConfigurationPropertyId.PrimaryNavigationCollapsed] = value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x0007FA43 File Offset: 0x0007DC43
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x0007FA52 File Offset: 0x0007DC52
		[DataMember]
		public string ThemeStorageId
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.ThemeStorageId];
			}
			set
			{
				base[UserConfigurationPropertyId.ThemeStorageId] = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0007FA5D File Offset: 0x0007DC5D
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x0007FA6C File Offset: 0x0007DC6C
		[DataMember]
		public bool MailFindBarOn
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.MailFindBarOn];
			}
			set
			{
				base[UserConfigurationPropertyId.MailFindBarOn] = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0007FA7C File Offset: 0x0007DC7C
		// (set) Token: 0x06002326 RID: 8998 RVA: 0x0007FA8B File Offset: 0x0007DC8B
		[DataMember]
		public bool CalendarFindBarOn
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CalendarFindBarOn];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarFindBarOn] = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x0007FA9B File Offset: 0x0007DC9B
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x0007FAAA File Offset: 0x0007DCAA
		[DataMember]
		public bool ContactsFindBarOn
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ContactsFindBarOn];
			}
			set
			{
				base[UserConfigurationPropertyId.ContactsFindBarOn] = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x0007FABA File Offset: 0x0007DCBA
		// (set) Token: 0x0600232A RID: 9002 RVA: 0x0007FAC9 File Offset: 0x0007DCC9
		[DataMember]
		public int SearchScope
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.SearchScope];
			}
			set
			{
				base[UserConfigurationPropertyId.SearchScope] = value;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x0007FAD9 File Offset: 0x0007DCD9
		// (set) Token: 0x0600232C RID: 9004 RVA: 0x0007FAE8 File Offset: 0x0007DCE8
		[DataMember]
		public int ContactsSearchScope
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.ContactsSearchScope];
			}
			set
			{
				base[UserConfigurationPropertyId.ContactsSearchScope] = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x0007FAF8 File Offset: 0x0007DCF8
		// (set) Token: 0x0600232E RID: 9006 RVA: 0x0007FB07 File Offset: 0x0007DD07
		[DataMember]
		public int TasksSearchScope
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.TasksSearchScope];
			}
			set
			{
				base[UserConfigurationPropertyId.TasksSearchScope] = value;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0007FB17 File Offset: 0x0007DD17
		// (set) Token: 0x06002330 RID: 9008 RVA: 0x0007FB26 File Offset: 0x0007DD26
		[DataMember]
		public bool IsOptimizedForAccessibility
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsOptimizedForAccessibility];
			}
			set
			{
				base[UserConfigurationPropertyId.IsOptimizedForAccessibility] = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x0007FB36 File Offset: 0x0007DD36
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x0007FB45 File Offset: 0x0007DD45
		[DataMember]
		public PontType NewEnabledPonts
		{
			get
			{
				return (PontType)base[UserConfigurationPropertyId.NewEnabledPonts];
			}
			set
			{
				base[UserConfigurationPropertyId.NewEnabledPonts] = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x0007FB55 File Offset: 0x0007DD55
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x0007FB64 File Offset: 0x0007DD64
		public FlagAction FlagAction
		{
			get
			{
				return (FlagAction)base[UserConfigurationPropertyId.FlagAction];
			}
			set
			{
				base[UserConfigurationPropertyId.FlagAction] = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0007FB74 File Offset: 0x0007DD74
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x0007FB86 File Offset: 0x0007DD86
		[DataMember(Name = "FlagAction")]
		public string FlagActionString
		{
			get
			{
				return this.FlagAction.ToString();
			}
			set
			{
				this.FlagAction = (FlagAction)Enum.Parse(typeof(FlagAction), value);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x0007FBA3 File Offset: 0x0007DDA3
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x0007FBB2 File Offset: 0x0007DDB2
		[DataMember]
		public bool AddRecipientsToAutoCompleteCache
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.AddRecipientsToAutoCompleteCache];
			}
			set
			{
				base[UserConfigurationPropertyId.AddRecipientsToAutoCompleteCache] = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0007FBC2 File Offset: 0x0007DDC2
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x0007FBD1 File Offset: 0x0007DDD1
		[DataMember]
		public bool ManuallyPickCertificate
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ManuallyPickCertificate];
			}
			set
			{
				base[UserConfigurationPropertyId.ManuallyPickCertificate] = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0007FBE1 File Offset: 0x0007DDE1
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x0007FBF0 File Offset: 0x0007DDF0
		[DataMember]
		public string SigningCertificateSubject
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SigningCertificateSubject];
			}
			set
			{
				base[UserConfigurationPropertyId.SigningCertificateSubject] = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0007FBFB File Offset: 0x0007DDFB
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x0007FC0A File Offset: 0x0007DE0A
		[DataMember]
		public string SigningCertificateId
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SigningCertificateId];
			}
			set
			{
				base[UserConfigurationPropertyId.SigningCertificateId] = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x0007FC15 File Offset: 0x0007DE15
		// (set) Token: 0x06002340 RID: 9024 RVA: 0x0007FC24 File Offset: 0x0007DE24
		[DataMember]
		public int UseDataCenterCustomTheme
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.UseDataCenterCustomTheme];
			}
			set
			{
				base[UserConfigurationPropertyId.UseDataCenterCustomTheme] = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x0007FC34 File Offset: 0x0007DE34
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x0007FC46 File Offset: 0x0007DE46
		[DataMember(Name = "ConversationSortOrder")]
		public string ConversationSortOrderString
		{
			get
			{
				return this.ConversationSortOrder.ToString();
			}
			set
			{
				this.ConversationSortOrder = (ConversationSortOrder)Enum.Parse(typeof(ConversationSortOrder), value);
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0007FC63 File Offset: 0x0007DE63
		// (set) Token: 0x06002344 RID: 9028 RVA: 0x0007FC72 File Offset: 0x0007DE72
		public ConversationSortOrder ConversationSortOrder
		{
			get
			{
				return (ConversationSortOrder)base[UserConfigurationPropertyId.ConversationSortOrder];
			}
			set
			{
				base[UserConfigurationPropertyId.ConversationSortOrder] = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x0007FC82 File Offset: 0x0007DE82
		// (set) Token: 0x06002346 RID: 9030 RVA: 0x0007FC91 File Offset: 0x0007DE91
		[DataMember]
		public bool ShowTreeInListView
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ShowTreeInListView];
			}
			set
			{
				base[UserConfigurationPropertyId.ShowTreeInListView] = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0007FCA1 File Offset: 0x0007DEA1
		// (set) Token: 0x06002348 RID: 9032 RVA: 0x0007FCB0 File Offset: 0x0007DEB0
		[DataMember]
		public bool HideDeletedItems
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HideDeletedItems];
			}
			set
			{
				base[UserConfigurationPropertyId.HideDeletedItems] = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0007FCC0 File Offset: 0x0007DEC0
		// (set) Token: 0x0600234A RID: 9034 RVA: 0x0007FCCF File Offset: 0x0007DECF
		[DataMember]
		public bool HideMailTipsByDefault
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HideMailTipsByDefault];
			}
			set
			{
				base[UserConfigurationPropertyId.HideMailTipsByDefault] = value;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x0007FCDF File Offset: 0x0007DEDF
		// (set) Token: 0x0600234C RID: 9036 RVA: 0x0007FCEE File Offset: 0x0007DEEE
		[DataMember]
		public string SendAddressDefault
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.SendAddressDefault];
			}
			set
			{
				base[UserConfigurationPropertyId.SendAddressDefault] = value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x0007FCF9 File Offset: 0x0007DEF9
		// (set) Token: 0x0600234E RID: 9038 RVA: 0x0007FD01 File Offset: 0x0007DF01
		[DataMember]
		public WorkingHoursType WorkingHours
		{
			get
			{
				return this.workingHours;
			}
			set
			{
				this.workingHours = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x0007FD0A File Offset: 0x0007DF0A
		// (set) Token: 0x06002350 RID: 9040 RVA: 0x0007FD12 File Offset: 0x0007DF12
		[DataMember]
		public int DefaultReminderTimeInMinutes
		{
			get
			{
				return this.defaultReminderTimeInMinutes;
			}
			set
			{
				this.defaultReminderTimeInMinutes = value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x0007FD1C File Offset: 0x0007DF1C
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x0007FD69 File Offset: 0x0007DF69
		[DataMember]
		public TimeZoneOffsetsType[] MailboxTimeZoneOffset
		{
			get
			{
				if (this.mailboxTimeZoneOffset == null)
				{
					ExDateTime startTime = ExDateTime.UtcNow.AddYears(-2);
					ExDateTime endTime = ExDateTime.UtcNow.AddYears(3);
					this.mailboxTimeZoneOffset = GetTimeZoneOffsetsCore.GetTheTimeZoneOffsets(startTime, endTime, this.TimeZone);
				}
				return this.mailboxTimeZoneOffset;
			}
			set
			{
				this.mailboxTimeZoneOffset = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x0007FD72 File Offset: 0x0007DF72
		// (set) Token: 0x06002354 RID: 9044 RVA: 0x0007FD81 File Offset: 0x0007DF81
		[DataMember]
		public bool ShowInferenceUiElements
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ShowInferenceUiElements];
			}
			set
			{
				base[UserConfigurationPropertyId.ShowInferenceUiElements] = value;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x0007FD91 File Offset: 0x0007DF91
		// (set) Token: 0x06002356 RID: 9046 RVA: 0x0007FDA0 File Offset: 0x0007DFA0
		[DataMember]
		public bool HasShownClutterBarIntroductionMouse
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownClutterBarIntroductionMouse];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownClutterBarIntroductionMouse] = value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x0007FDB0 File Offset: 0x0007DFB0
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x0007FDBF File Offset: 0x0007DFBF
		[DataMember]
		public bool HasShownClutterDeleteAllIntroductionMouse
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownClutterDeleteAllIntroductionMouse];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownClutterDeleteAllIntroductionMouse] = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0007FDCF File Offset: 0x0007DFCF
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x0007FDDE File Offset: 0x0007DFDE
		[DataMember]
		public bool HasShownClutterBarIntroductionTNarrow
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownClutterBarIntroductionTNarrow];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownClutterBarIntroductionTNarrow] = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x0007FDEE File Offset: 0x0007DFEE
		// (set) Token: 0x0600235C RID: 9052 RVA: 0x0007FDFD File Offset: 0x0007DFFD
		[DataMember]
		public bool HasShownClutterDeleteAllIntroductionTNarrow
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownClutterDeleteAllIntroductionTNarrow];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownClutterDeleteAllIntroductionTNarrow] = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0007FE0D File Offset: 0x0007E00D
		// (set) Token: 0x0600235E RID: 9054 RVA: 0x0007FE1C File Offset: 0x0007E01C
		[DataMember]
		public bool HasShownClutterBarIntroductionTWide
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownClutterBarIntroductionTWide];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownClutterBarIntroductionTWide] = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0007FE2C File Offset: 0x0007E02C
		// (set) Token: 0x06002360 RID: 9056 RVA: 0x0007FE3B File Offset: 0x0007E03B
		[DataMember]
		public bool HasShownClutterDeleteAllIntroductionTWide
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownClutterDeleteAllIntroductionTWide];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownClutterDeleteAllIntroductionTWide] = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x0007FE4B File Offset: 0x0007E04B
		// (set) Token: 0x06002362 RID: 9058 RVA: 0x0007FE5A File Offset: 0x0007E05A
		[DataMember]
		public bool HasShownIntroductionForPeopleCentricTriage
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownIntroductionForPeopleCentricTriage];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownIntroductionForPeopleCentricTriage] = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x0007FE6A File Offset: 0x0007E06A
		// (set) Token: 0x06002364 RID: 9060 RVA: 0x0007FE79 File Offset: 0x0007E079
		[DataMember]
		public bool HasShownIntroductionForModernGroups
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownIntroductionForModernGroups];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownIntroductionForModernGroups] = value;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x0007FE89 File Offset: 0x0007E089
		// (set) Token: 0x06002366 RID: 9062 RVA: 0x0007FE98 File Offset: 0x0007E098
		[DataMember]
		public UserOptionsLearnabilityTypes LearnabilityTypesShown
		{
			get
			{
				return (UserOptionsLearnabilityTypes)base[UserConfigurationPropertyId.LearnabilityTypesShown];
			}
			set
			{
				base[UserConfigurationPropertyId.LearnabilityTypesShown] = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x0007FEA8 File Offset: 0x0007E0A8
		// (set) Token: 0x06002368 RID: 9064 RVA: 0x0007FEB7 File Offset: 0x0007E0B7
		[DataMember]
		public bool HasShownPeopleIKnow
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.HasShownPeopleIKnow];
			}
			set
			{
				base[UserConfigurationPropertyId.HasShownPeopleIKnow] = value;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x0007FEC7 File Offset: 0x0007E0C7
		// (set) Token: 0x0600236A RID: 9066 RVA: 0x0007FED6 File Offset: 0x0007E0D6
		[DataMember]
		public bool ShowSenderOnTopInListView
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ShowSenderOnTopInListView];
			}
			set
			{
				base[UserConfigurationPropertyId.ShowSenderOnTopInListView] = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x0007FEE6 File Offset: 0x0007E0E6
		// (set) Token: 0x0600236C RID: 9068 RVA: 0x0007FEF5 File Offset: 0x0007E0F5
		[DataMember]
		public bool ShowPreviewTextInListView
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ShowPreviewTextInListView];
			}
			set
			{
				base[UserConfigurationPropertyId.ShowPreviewTextInListView] = value;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x0007FF05 File Offset: 0x0007E105
		// (set) Token: 0x0600236E RID: 9070 RVA: 0x0007FF14 File Offset: 0x0007E114
		[DataMember]
		public int GlobalReadingPanePosition
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.GlobalReadingPanePosition];
			}
			set
			{
				base[UserConfigurationPropertyId.GlobalReadingPanePosition] = value;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x0007FF24 File Offset: 0x0007E124
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x0007FF33 File Offset: 0x0007E133
		[DataMember]
		public bool ReportJunkSelected
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.ReportJunkSelected];
			}
			set
			{
				base[UserConfigurationPropertyId.ReportJunkSelected] = value;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x0007FF43 File Offset: 0x0007E143
		// (set) Token: 0x06002372 RID: 9074 RVA: 0x0007FF52 File Offset: 0x0007E152
		[DataMember]
		public bool CheckForReportJunkDialog
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.CheckForReportJunkDialog];
			}
			set
			{
				base[UserConfigurationPropertyId.CheckForReportJunkDialog] = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x0007FF62 File Offset: 0x0007E162
		// (set) Token: 0x06002374 RID: 9076 RVA: 0x0007FF71 File Offset: 0x0007E171
		public UserOptionsMigrationState UserOptionsMigrationState
		{
			get
			{
				return (UserOptionsMigrationState)base[UserConfigurationPropertyId.UserOptionsMigrationState];
			}
			set
			{
				base[UserConfigurationPropertyId.UserOptionsMigrationState] = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0007FF81 File Offset: 0x0007E181
		// (set) Token: 0x06002376 RID: 9078 RVA: 0x0007FF90 File Offset: 0x0007E190
		[DataMember]
		public bool IsInferenceSurveyComplete
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.IsInferenceSurveyComplete];
			}
			set
			{
				base[UserConfigurationPropertyId.IsInferenceSurveyComplete] = value;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x0007FFA0 File Offset: 0x0007E1A0
		// (set) Token: 0x06002378 RID: 9080 RVA: 0x0007FFAF File Offset: 0x0007E1AF
		[DataMember]
		public int ActiveSurvey
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.ActiveSurvey];
			}
			set
			{
				base[UserConfigurationPropertyId.ActiveSurvey] = value;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x0007FFBF File Offset: 0x0007E1BF
		// (set) Token: 0x0600237A RID: 9082 RVA: 0x0007FFCE File Offset: 0x0007E1CE
		[DataMember]
		public int CompletedSurveys
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CompletedSurveys];
			}
			set
			{
				base[UserConfigurationPropertyId.CompletedSurveys] = value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x0007FFDE File Offset: 0x0007E1DE
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x0007FFED File Offset: 0x0007E1ED
		[DataMember]
		public int DismissedSurveys
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.DismissedSurveys];
			}
			set
			{
				base[UserConfigurationPropertyId.DismissedSurveys] = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0007FFFD File Offset: 0x0007E1FD
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x0008000C File Offset: 0x0007E20C
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string LastSurveyDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.LastSurveyDate];
			}
			set
			{
				base[UserConfigurationPropertyId.LastSurveyDate] = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00080017 File Offset: 0x0007E217
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x00080026 File Offset: 0x0007E226
		[DataMember]
		public bool DontShowSurveys
		{
			get
			{
				return (bool)base[UserConfigurationPropertyId.DontShowSurveys];
			}
			set
			{
				base[UserConfigurationPropertyId.DontShowSurveys] = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00080036 File Offset: 0x0007E236
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x00080045 File Offset: 0x0007E245
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string PeopleIKnowFirstUseDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.PeopleIKnowFirstUseDate];
			}
			set
			{
				base[UserConfigurationPropertyId.PeopleIKnowFirstUseDate] = value;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x00080050 File Offset: 0x0007E250
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x0008005F File Offset: 0x0007E25F
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string PeopleIKnowLastUseDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.PeopleIKnowLastUseDate];
			}
			set
			{
				base[UserConfigurationPropertyId.PeopleIKnowFirstUseDate] = value;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0008006A File Offset: 0x0007E26A
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x00080079 File Offset: 0x0007E279
		[DataMember]
		public int PeopleIKnowUse
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.PeopleIKnowUse];
			}
			set
			{
				base[UserConfigurationPropertyId.PeopleIKnowUse] = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00080089 File Offset: 0x0007E289
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x00080098 File Offset: 0x0007E298
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string ModernGroupsFirstUseDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.ModernGroupsFirstUseDate];
			}
			set
			{
				base[UserConfigurationPropertyId.ModernGroupsFirstUseDate] = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x000800A3 File Offset: 0x0007E2A3
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x000800B2 File Offset: 0x0007E2B2
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ModernGroupsLastUseDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.ModernGroupsLastUseDate];
			}
			set
			{
				base[UserConfigurationPropertyId.ModernGroupsLastUseDate] = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000800BD File Offset: 0x0007E2BD
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x000800CC File Offset: 0x0007E2CC
		[DataMember]
		public int ModernGroupsUseCount
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.ModernGroupsUseCount];
			}
			set
			{
				base[UserConfigurationPropertyId.ModernGroupsUseCount] = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000800DC File Offset: 0x0007E2DC
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x000800EB File Offset: 0x0007E2EB
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string BuildGreenLightSurveyLastShownDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.BuildGreenLightSurveyLastShownDate];
			}
			set
			{
				base[UserConfigurationPropertyId.BuildGreenLightSurveyLastShownDate] = value;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x000800F6 File Offset: 0x0007E2F6
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x00080105 File Offset: 0x0007E305
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string InferenceSurveyDate
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.InferenceSurveyDate];
			}
			set
			{
				base[UserConfigurationPropertyId.InferenceSurveyDate] = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x00080110 File Offset: 0x0007E310
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x0008011F File Offset: 0x0007E31F
		[DataMember]
		public int CalendarSearchUseCount
		{
			get
			{
				return (int)base[UserConfigurationPropertyId.CalendarSearchUseCount];
			}
			set
			{
				base[UserConfigurationPropertyId.CalendarSearchUseCount] = value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0008012F File Offset: 0x0007E32F
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x0008013E File Offset: 0x0007E33E
		[DataMember(EmitDefaultValue = false)]
		public string[] FrequentlyUsedFolders
		{
			get
			{
				return (string[])base[UserConfigurationPropertyId.FrequentlyUsedFolders];
			}
			set
			{
				base[UserConfigurationPropertyId.FrequentlyUsedFolders] = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x00080149 File Offset: 0x0007E349
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x00080158 File Offset: 0x0007E358
		[DataMember]
		public string ArchiveFolderId
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.ArchiveFolderId];
			}
			set
			{
				base[UserConfigurationPropertyId.ArchiveFolderId] = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x00080163 File Offset: 0x0007E363
		// (set) Token: 0x06002398 RID: 9112 RVA: 0x00080172 File Offset: 0x0007E372
		[DataMember]
		public string DefaultAttachmentsUploadFolderId
		{
			get
			{
				return (string)base[UserConfigurationPropertyId.DefaultAttachmentsUploadFolderId];
			}
			set
			{
				base[UserConfigurationPropertyId.DefaultAttachmentsUploadFolderId] = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0008017D File Offset: 0x0007E37D
		internal override UserConfigurationPropertySchemaBase Schema
		{
			get
			{
				return UserOptionPropertySchema.Instance;
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00080184 File Offset: 0x0007E384
		internal void Load(MailboxSession mailboxSession, bool loadCalendarOptions, bool performMigrationFixup)
		{
			StorePerformanceCountersCapture countersCapture = StorePerformanceCountersCapture.Start(mailboxSession);
			bool flag = false;
			IList<UserConfigurationPropertyDefinition> properties = new List<UserConfigurationPropertyDefinition>(base.OptionProperties.Keys);
			base.Load(mailboxSession, properties, true);
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.UserOptionsLoad, countersCapture, true);
			bool flag2 = string.IsNullOrEmpty(this.TimeZone);
			if (flag2)
			{
				ExTraceGlobals.UserOptionsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UserOptions loaded without mailbox time zone set. User:{0}", mailboxSession.DisplayAddress);
				this.TimeZone = ExTimeZone.CurrentTimeZone.Id;
			}
			if (!loadCalendarOptions)
			{
				ExTraceGlobals.UserOptionsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UserOptions loaded without calendar options. User:{0}", mailboxSession.DisplayAddress);
				return;
			}
			if (flag2)
			{
				ExTraceGlobals.UserOptionsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UserOptions loaded with calendar options but without mailbox time zone set. User:{0}", mailboxSession.DisplayAddress);
				this.PopulateDefaultWorkingHours(ExTimeZone.CurrentTimeZone);
				this.PopulateDefaultReminderOptions();
				return;
			}
			this.LoadWorkingHours(mailboxSession);
			if (performMigrationFixup)
			{
				flag |= this.PerformWorkingHoursFixup(mailboxSession);
			}
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.WorkingHours, countersCapture, true);
			this.LoadReminderOptions(mailboxSession);
			OwaUserConfigurationLogUtilities.LogAndResetPerfCapture(OwaUserConfigurationLogType.LoadReminderOptions, countersCapture, false);
			if (flag)
			{
				this.CommitUserOptionMigrationState(mailboxSession);
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00080280 File Offset: 0x0007E480
		private void CommitUserOptionMigrationState(MailboxSession mailboxSession)
		{
			try
			{
				base.Commit(mailboxSession, new UserConfigurationPropertyDefinition[]
				{
					UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.UserOptionsMigrationState)
				});
			}
			catch (StoragePermanentException arg)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceError<StoragePermanentException, string>((long)this.GetHashCode(), "Permanent error while trying to update UserOptionsMigrationState. Error: {0}. User: {1}", arg, mailboxSession.DisplayAddress);
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceError<StorageTransientException, string>((long)this.GetHashCode(), "Transient error while trying to update UserOptionsMigrationState. Error: {0}. User: {1}.", arg2, mailboxSession.DisplayAddress);
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0008030C File Offset: 0x0007E50C
		private void LoadWorkingHours(MailboxSession mailboxSession)
		{
			Exception ex = null;
			try
			{
				this.WorkingHours = WorkingHoursType.Load(mailboxSession, this.TimeZone);
			}
			catch (ObjectExistedException ex2)
			{
				ex = ex2;
			}
			catch (QuotaExceededException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ExTraceGlobals.UserOptionsCallTracer.TraceError<string, Exception>((long)this.GetHashCode(), "UserOptionsType: WorkingHoursType.Load failed. User:{0} Exception: {1}.", mailboxSession.DisplayAddress, ex);
				this.PopulateDefaultWorkingHours(this.TimeZone);
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00080384 File Offset: 0x0007E584
		private bool PerformWorkingHoursFixup(MailboxSession session)
		{
			if ((this.UserOptionsMigrationState & UserOptionsMigrationState.WorkingHoursTimeZoneFixUp) == UserOptionsMigrationState.None)
			{
				bool flag = true;
				if (this.WorkingHours != null && this.WorkingHours.IsTimeZoneDifferent && (this.WorkingHours.WorkingHoursTimeZone.Id == ExTimeZone.CurrentTimeZone.Id || (UserOptionsType.LegacyDataCenterTimeZone != null && this.WorkingHours.WorkingHoursTimeZone.Id == UserOptionsType.LegacyDataCenterTimeZone.Id)))
				{
					ExTimeZone newTimeZone = null;
					if (ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(this.TimeZone, out newTimeZone))
					{
						ExTraceGlobals.UserOptionsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "UserOptionsType: Performing WorkingHours fixup - OM:227120 - User:{0}", session.DisplayAddress);
						try
						{
							WorkingHoursType.MoveWorkingHoursToTimeZone(session, newTimeZone);
							this.LoadWorkingHours(session);
						}
						catch (StorageTransientException arg)
						{
							ExTraceGlobals.UserOptionsCallTracer.TraceDebug<string, StorageTransientException>((long)this.GetHashCode(), "UserOptionsType: Caught transient error while performing WorkingHours fixup - OM:227120. User:{0} Exception: {1}", session.DisplayAddress, arg);
							flag = false;
						}
						catch (CorruptDataException arg2)
						{
							ExTraceGlobals.UserOptionsCallTracer.TraceError<string, CorruptDataException>((long)this.GetHashCode(), "UserOptionsType: Caught corrupt data error while performing WorkingHours fixup - OM:227120. User:{0} Exception: {1}", session.DisplayAddress, arg2);
						}
						catch (StoragePermanentException arg3)
						{
							ExTraceGlobals.UserOptionsCallTracer.TraceError<string, StoragePermanentException>((long)this.GetHashCode(), "UserOptionsType: Caught permanent error while performing WorkingHours fixup - OM:227120. User:{0} Exception: {1}", session.DisplayAddress, arg3);
						}
					}
				}
				if (flag)
				{
					this.UserOptionsMigrationState |= UserOptionsMigrationState.WorkingHoursTimeZoneFixUp;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000804F0 File Offset: 0x0007E6F0
		private void PopulateDefaultWorkingHours(ExTimeZone timeZone)
		{
			this.WorkingHours = WorkingHoursType.GetDefaultWorkingHoursInTimeZone(timeZone);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00080500 File Offset: 0x0007E700
		private void PopulateDefaultWorkingHours(string timeZoneId)
		{
			ExTimeZone timeZone;
			if (!string.IsNullOrEmpty(timeZoneId) && ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(timeZoneId, out timeZone))
			{
				this.PopulateDefaultWorkingHours(timeZone);
				return;
			}
			ExTraceGlobals.UserOptionsCallTracer.TraceError<string>((long)this.GetHashCode(), "Unable to map time zone id for Default working hours. Using UTC instead. TimeZoneId: {0}", string.IsNullOrEmpty(timeZoneId) ? "NullorEmpty" : timeZoneId);
			this.PopulateDefaultWorkingHours(ExTimeZone.UtcTimeZone);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0008055D File Offset: 0x0007E75D
		private void PopulateDefaultReminderOptions()
		{
			this.DefaultReminderTimeInMinutes = 15;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00080568 File Offset: 0x0007E768
		private void LoadReminderOptions(MailboxSession mailboxSession)
		{
			try
			{
				CalendarConfiguration calendarConfiguration;
				using (CalendarConfigurationDataProvider calendarConfigurationDataProvider = new CalendarConfigurationDataProvider(mailboxSession))
				{
					calendarConfiguration = (CalendarConfiguration)calendarConfigurationDataProvider.Read<CalendarConfiguration>(null);
				}
				if (calendarConfiguration == null)
				{
					ExTraceGlobals.UserOptionsCallTracer.TraceError<string>((long)this.GetHashCode(), "Unable to load Calendar configuration object for mailbox {0}", mailboxSession.DisplayAddress);
				}
				else
				{
					this.DefaultReminderTimeInMinutes = calendarConfiguration.DefaultReminderTime;
				}
			}
			catch (StoragePermanentException arg)
			{
				ExTraceGlobals.UserOptionsCallTracer.TraceError<string, StoragePermanentException>((long)this.GetHashCode(), "Unable to load Calendar configuration object for mailbox {0}. Exception: {1}", mailboxSession.DisplayAddress, arg);
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.UserOptionsCallTracer.TraceError<string, StorageTransientException>((long)this.GetHashCode(), "Unable to load Calendar configuration object for mailbox {0}, Exception: {1}", mailboxSession.DisplayAddress, arg2);
			}
		}

		// Token: 0x04001333 RID: 4915
		private const string LegacyDataCenterTimeZoneId = "Greenwich Standard Time";

		// Token: 0x04001334 RID: 4916
		private const string ConfigurationName = "OWA.UserOptions";

		// Token: 0x04001335 RID: 4917
		private static ExTimeZone legacyDataCenterTimeZone;

		// Token: 0x04001336 RID: 4918
		private WorkingHoursType workingHours;

		// Token: 0x04001337 RID: 4919
		private int defaultReminderTimeInMinutes = 15;

		// Token: 0x04001338 RID: 4920
		private TimeZoneOffsetsType[] mailboxTimeZoneOffset;
	}
}
