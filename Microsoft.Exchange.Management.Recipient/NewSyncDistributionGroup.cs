using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000BC RID: 188
	[Cmdlet("New", "SyncDistributionGroup", SupportsShouldProcess = true)]
	public sealed class NewSyncDistributionGroup : NewDistributionGroupBase
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00030EE0 File Offset: 0x0002F0E0
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00030EF7 File Offset: 0x0002F0F7
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] AcceptMessagesOnlyFrom
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.AcceptMessagesOnlyFrom];
			}
			set
			{
				base.Fields[ADRecipientSchema.AcceptMessagesOnlyFrom] = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00030F0A File Offset: 0x0002F10A
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x00030F21 File Offset: 0x0002F121
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
			set
			{
				base.Fields[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers] = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00030F34 File Offset: 0x0002F134
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x00030F41 File Offset: 0x0002F141
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return this.DataObject.BlockedSendersHash;
			}
			set
			{
				this.DataObject.BlockedSendersHash = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00030F4F File Offset: 0x0002F14F
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x00030F66 File Offset: 0x0002F166
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] BypassModerationFrom
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[MailEnabledRecipientSchema.BypassModerationFrom];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00030F79 File Offset: 0x0002F179
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x00030F90 File Offset: 0x0002F190
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] BypassModerationFromDLMembers
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[MailEnabledRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00030FA3 File Offset: 0x0002F1A3
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00030FB0 File Offset: 0x0002F1B0
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return this.DataObject.DirSyncId;
			}
			set
			{
				this.DataObject.DirSyncId = value;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00030FBE File Offset: 0x0002F1BE
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00030FCB File Offset: 0x0002F1CB
		[Parameter(Mandatory = false)]
		public string CustomAttribute1
		{
			get
			{
				return this.DataObject.CustomAttribute1;
			}
			set
			{
				this.DataObject.CustomAttribute1 = value;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00030FD9 File Offset: 0x0002F1D9
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00030FE6 File Offset: 0x0002F1E6
		[Parameter(Mandatory = false)]
		public string CustomAttribute10
		{
			get
			{
				return this.DataObject.CustomAttribute10;
			}
			set
			{
				this.DataObject.CustomAttribute10 = value;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00030FF4 File Offset: 0x0002F1F4
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00031001 File Offset: 0x0002F201
		[Parameter(Mandatory = false)]
		public string CustomAttribute11
		{
			get
			{
				return this.DataObject.CustomAttribute11;
			}
			set
			{
				this.DataObject.CustomAttribute11 = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0003100F File Offset: 0x0002F20F
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0003101C File Offset: 0x0002F21C
		[Parameter(Mandatory = false)]
		public string CustomAttribute12
		{
			get
			{
				return this.DataObject.CustomAttribute12;
			}
			set
			{
				this.DataObject.CustomAttribute12 = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0003102A File Offset: 0x0002F22A
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x00031037 File Offset: 0x0002F237
		[Parameter(Mandatory = false)]
		public string CustomAttribute13
		{
			get
			{
				return this.DataObject.CustomAttribute13;
			}
			set
			{
				this.DataObject.CustomAttribute13 = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00031045 File Offset: 0x0002F245
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00031052 File Offset: 0x0002F252
		[Parameter(Mandatory = false)]
		public string CustomAttribute14
		{
			get
			{
				return this.DataObject.CustomAttribute14;
			}
			set
			{
				this.DataObject.CustomAttribute14 = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00031060 File Offset: 0x0002F260
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0003106D File Offset: 0x0002F26D
		[Parameter(Mandatory = false)]
		public string CustomAttribute15
		{
			get
			{
				return this.DataObject.CustomAttribute15;
			}
			set
			{
				this.DataObject.CustomAttribute15 = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0003107B File Offset: 0x0002F27B
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00031088 File Offset: 0x0002F288
		[Parameter(Mandatory = false)]
		public string CustomAttribute2
		{
			get
			{
				return this.DataObject.CustomAttribute2;
			}
			set
			{
				this.DataObject.CustomAttribute2 = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00031096 File Offset: 0x0002F296
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x000310A3 File Offset: 0x0002F2A3
		[Parameter(Mandatory = false)]
		public string CustomAttribute3
		{
			get
			{
				return this.DataObject.CustomAttribute3;
			}
			set
			{
				this.DataObject.CustomAttribute3 = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000310B1 File Offset: 0x0002F2B1
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x000310BE File Offset: 0x0002F2BE
		[Parameter(Mandatory = false)]
		public string CustomAttribute4
		{
			get
			{
				return this.DataObject.CustomAttribute4;
			}
			set
			{
				this.DataObject.CustomAttribute4 = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000310CC File Offset: 0x0002F2CC
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x000310D9 File Offset: 0x0002F2D9
		[Parameter(Mandatory = false)]
		public string CustomAttribute5
		{
			get
			{
				return this.DataObject.CustomAttribute5;
			}
			set
			{
				this.DataObject.CustomAttribute5 = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000310E7 File Offset: 0x0002F2E7
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x000310F4 File Offset: 0x0002F2F4
		[Parameter(Mandatory = false)]
		public string CustomAttribute6
		{
			get
			{
				return this.DataObject.CustomAttribute6;
			}
			set
			{
				this.DataObject.CustomAttribute6 = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00031102 File Offset: 0x0002F302
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0003110F File Offset: 0x0002F30F
		[Parameter(Mandatory = false)]
		public string CustomAttribute7
		{
			get
			{
				return this.DataObject.CustomAttribute7;
			}
			set
			{
				this.DataObject.CustomAttribute7 = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003111D File Offset: 0x0002F31D
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0003112A File Offset: 0x0002F32A
		[Parameter(Mandatory = false)]
		public string CustomAttribute8
		{
			get
			{
				return this.DataObject.CustomAttribute8;
			}
			set
			{
				this.DataObject.CustomAttribute8 = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00031138 File Offset: 0x0002F338
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00031145 File Offset: 0x0002F345
		[Parameter(Mandatory = false)]
		public string CustomAttribute9
		{
			get
			{
				return this.DataObject.CustomAttribute9;
			}
			set
			{
				this.DataObject.CustomAttribute9 = value;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00031153 File Offset: 0x0002F353
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00031160 File Offset: 0x0002F360
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute1;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute1 = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0003116E File Offset: 0x0002F36E
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0003117B File Offset: 0x0002F37B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute2;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute2 = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00031189 File Offset: 0x0002F389
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00031196 File Offset: 0x0002F396
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute3;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute3 = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000311A4 File Offset: 0x0002F3A4
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x000311B1 File Offset: 0x0002F3B1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute4;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute4 = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000311BF File Offset: 0x0002F3BF
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x000311CC File Offset: 0x0002F3CC
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute5;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute5 = value;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x000311DA File Offset: 0x0002F3DA
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x000311E7 File Offset: 0x0002F3E7
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return this.DataObject.EmailAddresses;
			}
			set
			{
				this.DataObject.EmailAddresses = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x000311F5 File Offset: 0x0002F3F5
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0003120C File Offset: 0x0002F40C
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] GrantSendOnBehalfTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[ADRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				base.Fields[ADRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003121F File Offset: 0x0002F41F
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0003122C File Offset: 0x0002F42C
		[Parameter(Mandatory = false)]
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return this.DataObject.HiddenFromAddressListsEnabled;
			}
			set
			{
				this.DataObject.HiddenFromAddressListsEnabled = value;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0003123A File Offset: 0x0002F43A
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00031242 File Offset: 0x0002F442
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return base.MailTipTranslations;
			}
			set
			{
				base.MailTipTranslations = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0003124B File Offset: 0x0002F44B
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00031258 File Offset: 0x0002F458
		[Parameter(Mandatory = false)]
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return this.DataObject.RecipientDisplayType;
			}
			set
			{
				this.DataObject.RecipientDisplayType = value;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00031266 File Offset: 0x0002F466
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00031273 File Offset: 0x0002F473
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return this.DataObject.SafeRecipientsHash;
			}
			set
			{
				this.DataObject.SafeRecipientsHash = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00031281 File Offset: 0x0002F481
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0003128E File Offset: 0x0002F48E
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return this.DataObject.SafeSendersHash;
			}
			set
			{
				this.DataObject.SafeSendersHash = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0003129C File Offset: 0x0002F49C
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x000312B3 File Offset: 0x0002F4B3
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] RejectMessagesFrom
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.RejectMessagesFrom];
			}
			set
			{
				base.Fields[ADRecipientSchema.RejectMessagesFrom] = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000312C6 File Offset: 0x0002F4C6
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000312DD File Offset: 0x0002F4DD
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] RejectMessagesFromDLMembers
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.RejectMessagesFromDLMembers];
			}
			set
			{
				base.Fields[ADRecipientSchema.RejectMessagesFromDLMembers] = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x000312F0 File Offset: 0x0002F4F0
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00031311 File Offset: 0x0002F511
		[Parameter(Mandatory = false)]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)(base.Fields["RequireAllSendersAreAuthenticated"] ?? false);
			}
			set
			{
				base.Fields["RequireAllSendersAreAuthenticated"] = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00031329 File Offset: 0x0002F529
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00031336 File Offset: 0x0002F536
		[Parameter]
		public bool ReportToManagerEnabled
		{
			get
			{
				return this.DataObject.ReportToManagerEnabled;
			}
			set
			{
				this.DataObject.ReportToManagerEnabled = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00031344 File Offset: 0x0002F544
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00031365 File Offset: 0x0002F565
		[Parameter(Mandatory = false)]
		public bool ReportToOriginatorEnabled
		{
			get
			{
				return (bool)(base.Fields["ReportToOriginatorEnabled"] ?? false);
			}
			set
			{
				base.Fields["ReportToOriginatorEnabled"] = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0003137D File Offset: 0x0002F57D
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0003138A File Offset: 0x0002F58A
		[Parameter(Mandatory = false)]
		public bool SendOofMessageToOriginatorEnabled
		{
			get
			{
				return this.DataObject.SendOofMessageToOriginatorEnabled;
			}
			set
			{
				this.DataObject.SendOofMessageToOriginatorEnabled = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00031398 File Offset: 0x0002F598
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x000313A5 File Offset: 0x0002F5A5
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return this.DataObject.HABSeniorityIndex;
			}
			set
			{
				this.DataObject.HABSeniorityIndex = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x000313B3 File Offset: 0x0002F5B3
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x000313C0 File Offset: 0x0002F5C0
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return this.DataObject.PhoneticDisplayName;
			}
			set
			{
				this.DataObject.PhoneticDisplayName = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000313CE File Offset: 0x0002F5CE
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x000313DB File Offset: 0x0002F5DB
		[Parameter(Mandatory = false)]
		public bool IsHierarchicalGroup
		{
			get
			{
				return this.DataObject.IsOrganizationalGroup;
			}
			set
			{
				this.DataObject.IsOrganizationalGroup = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000313E9 File Offset: 0x0002F5E9
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x000313F6 File Offset: 0x0002F5F6
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return this.DataObject.OnPremisesObjectId;
			}
			set
			{
				this.DataObject.OnPremisesObjectId = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00031404 File Offset: 0x0002F604
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00031411 File Offset: 0x0002F611
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return this.DataObject.IsDirSynced;
			}
			set
			{
				this.DataObject.IsDirSynced = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0003141F File Offset: 0x0002F61F
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0003142C File Offset: 0x0002F62C
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return this.DataObject.DirSyncAuthorityMetadata;
			}
			set
			{
				this.DataObject.DirSyncAuthorityMetadata = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0003143A File Offset: 0x0002F63A
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00031451 File Offset: 0x0002F651
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection SmtpAndX500Addresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["SmtpAndX500Addresses"];
			}
			set
			{
				base.Fields["SmtpAndX500Addresses"] = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00031464 File Offset: 0x0002F664
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0003147B File Offset: 0x0002F67B
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection SipAddresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["SipAddresses"];
			}
			set
			{
				base.Fields["SipAddresses"] = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0003148E File Offset: 0x0002F68E
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x000314B4 File Offset: 0x0002F6B4
		[Parameter(Mandatory = false)]
		public SwitchParameter DoNotCheckAcceptedDomains
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotCheckAcceptedDomains"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DoNotCheckAcceptedDomains"] = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x000314CC File Offset: 0x0002F6CC
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x000314E3 File Offset: 0x0002F6E3
		[Parameter(Mandatory = false)]
		public string ValidationOrganization
		{
			get
			{
				return (string)base.Fields["ValidationOrganization"];
			}
			set
			{
				base.Fields["ValidationOrganization"] = value;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000314F6 File Offset: 0x0002F6F6
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00031508 File Offset: 0x0002F708
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if ((this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0) || (this.SipAddresses != null && this.SipAddresses.Count > 0))
			{
				this.DataObject.EmailAddresses = SyncTaskHelper.MergeAddresses(this.SmtpAndX500Addresses, this.SipAddresses);
			}
			base.InternalBeginProcessing();
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified(ADGroupSchema.ManagedBy) && base.ManagedBy != null && base.ManagedBy.Count != 0)
			{
				this.managedByRecipients = new MultiValuedProperty<ADRecipient>();
				foreach (RecipientIdParameter recipientIdParameter in base.ManagedBy)
				{
					ADRecipient item = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), ExchangeErrorCategory.Client);
					this.managedByRecipients.Add(item);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.GrantSendOnBehalfTo != null && this.GrantSendOnBehalfTo.Length != 0)
			{
				this.grantSendOnBehalfTo = new MultiValuedProperty<ADRecipient>();
				foreach (RecipientIdParameter recipientIdParameter2 in this.GrantSendOnBehalfTo)
				{
					ADRecipient item2 = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
					this.grantSendOnBehalfTo.Add(item2);
				}
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom) && this.BypassModerationFrom != null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty;
				MultiValuedProperty<ADObjectId> multiValuedProperty2;
				this.bypassModerationFromRecipient = SetMailEnabledRecipientObjectTask<DistributionGroupIdParameter, SyncDistributionGroup, ADGroup>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFrom, "BypassModerationFrom", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty, out multiValuedProperty2);
				if (multiValuedProperty != null && multiValuedProperty.Count > 0)
				{
					base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorIndividualRecipientNeeded(multiValuedProperty[0].ToString())), ExchangeErrorCategory.Client, null);
				}
				this.bypassModerationFrom = multiValuedProperty2;
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromDLMembers) && this.BypassModerationFromDLMembers != null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty3;
				MultiValuedProperty<ADObjectId> multiValuedProperty4;
				this.bypassModerationFromDLMembersRecipient = SetMailEnabledRecipientObjectTask<DistributionGroupIdParameter, SyncDistributionGroup, ADGroup>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFromDLMembers, "BypassModerationFromDLMembers", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty3, out multiValuedProperty4);
				if (multiValuedProperty4 != null && multiValuedProperty4.Count > 0)
				{
					base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorGroupRecipientNeeded(multiValuedProperty4[0].ToString())), ExchangeErrorCategory.Client, null);
				}
				this.bypassModerationFromDLMembers = multiValuedProperty3;
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom) && this.AcceptMessagesOnlyFrom != null && this.AcceptMessagesOnlyFrom.Length != 0)
			{
				this.acceptMessagesOnlyFrom = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter in this.AcceptMessagesOnlyFrom)
				{
					ADRecipient item3 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter.ToString())), ExchangeErrorCategory.Client);
					this.acceptMessagesOnlyFrom.Add(item3);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers) && this.AcceptMessagesOnlyFromDLMembers != null && this.AcceptMessagesOnlyFromDLMembers.Length != 0)
			{
				this.acceptMessagesOnlyFromDLMembers = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter2 in this.AcceptMessagesOnlyFromDLMembers)
				{
					ADRecipient item4 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
					this.acceptMessagesOnlyFromDLMembers.Add(item4);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom) && this.RejectMessagesFrom != null && this.RejectMessagesFrom.Length != 0)
			{
				this.rejectMessagesFrom = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter3 in this.RejectMessagesFrom)
				{
					ADRecipient item5 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter3, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter3.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter3.ToString())), ExchangeErrorCategory.Client);
					this.rejectMessagesFrom.Add(item5);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers) && this.RejectMessagesFromDLMembers != null && this.RejectMessagesFromDLMembers.Length != 0)
			{
				this.rejectMessagesFromDLMembers = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter4 in this.RejectMessagesFromDLMembers)
				{
					ADRecipient item6 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter4, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter4.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter4.ToString())), ExchangeErrorCategory.Client);
					this.rejectMessagesFromDLMembers.Add(item6);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00031AB0 File Offset: 0x0002FCB0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.grantSendOnBehalfTo != null)
			{
				foreach (ADRecipient recipient in this.grantSendOnBehalfTo)
				{
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, recipient, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.bypassModerationFromRecipient != null)
			{
				foreach (ADRecipient recipient2 in this.bypassModerationFromRecipient)
				{
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, recipient2, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.bypassModerationFromDLMembersRecipient != null)
			{
				foreach (ADRecipient recipient3 in this.bypassModerationFromDLMembersRecipient)
				{
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, recipient3, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00031C18 File Offset: 0x0002FE18
		protected override void PrepareRecipientObject(ADGroup group)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(group);
			if (base.ManagedBy == null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
				RoleGroupIdParameter roleGroupIdParameter = new RoleGroupIdParameter("Organization Management");
				ADGroup adgroup = (ADGroup)base.GetDataObject<ADGroup>(roleGroupIdParameter, base.TenantGlobalCatalogSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(roleGroupIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(roleGroupIdParameter.ToString())), ExchangeErrorCategory.Client);
				multiValuedProperty.Add(adgroup.Id);
				group.ManagedBy = multiValuedProperty;
			}
			group.BypassModerationCheck = true;
			if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.grantSendOnBehalfTo != null)
			{
				foreach (ADRecipient adrecipient in this.grantSendOnBehalfTo)
				{
					group.GrantSendOnBehalfTo.Add(adrecipient.Identity as ADObjectId);
				}
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom))
			{
				group.BypassModerationFrom = this.bypassModerationFrom;
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromDLMembers))
			{
				group.BypassModerationFromDLMembers = this.bypassModerationFromDLMembers;
			}
			if (this.DataObject != null && this.DataObject.IsModified(ADRecipientSchema.EmailAddresses))
			{
				group.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, this.DataObject.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom))
			{
				group.AcceptMessagesOnlyFrom = (from c in this.acceptMessagesOnlyFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers))
			{
				group.AcceptMessagesOnlyFromDLMembers = (from c in this.acceptMessagesOnlyFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom))
			{
				group.RejectMessagesFrom = (from c in this.rejectMessagesFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers))
			{
				group.RejectMessagesFromDLMembers = (from c in this.rejectMessagesFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			group.ReportToOriginatorEnabled = this.ReportToOriginatorEnabled;
			group.RequireAllSendersAreAuthenticated = this.RequireSenderAuthenticationEnabled;
			TaskLogger.LogExit();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00031EE4 File Offset: 0x000300E4
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			SyncDistributionGroup result2 = new SyncDistributionGroup((ADGroup)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00031F24 File Offset: 0x00030124
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(SyncDistributionGroup).FullName;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00031F35 File Offset: 0x00030135
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncDistributionGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x0400028B RID: 651
		private MultiValuedProperty<ADRecipient> grantSendOnBehalfTo;

		// Token: 0x0400028C RID: 652
		private MultiValuedProperty<ADObjectId> bypassModerationFrom;

		// Token: 0x0400028D RID: 653
		private MultiValuedProperty<ADRecipient> bypassModerationFromRecipient;

		// Token: 0x0400028E RID: 654
		private MultiValuedProperty<ADObjectId> bypassModerationFromDLMembers;

		// Token: 0x0400028F RID: 655
		private MultiValuedProperty<ADRecipient> bypassModerationFromDLMembersRecipient;

		// Token: 0x04000290 RID: 656
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFrom;

		// Token: 0x04000291 RID: 657
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFromDLMembers;

		// Token: 0x04000292 RID: 658
		private MultiValuedProperty<ADRecipient> rejectMessagesFrom;

		// Token: 0x04000293 RID: 659
		private MultiValuedProperty<ADRecipient> rejectMessagesFromDLMembers;
	}
}
