using System;
using System.Linq;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000C3 RID: 195
	[Cmdlet("New", "SyncMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "User")]
	public sealed class NewSyncMailbox : NewMailboxOrSyncMailbox
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0003353F File Offset: 0x0003173F
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00033547 File Offset: 0x00031747
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return base.AddressBookPolicy;
			}
			set
			{
				base.AddressBookPolicy = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00033550 File Offset: 0x00031750
		private new Guid MailboxContainerGuid
		{
			get
			{
				return base.MailboxContainerGuid;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00033558 File Offset: 0x00031758
		private new SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return base.ForestWideDomainControllerAffinityByExecutingUser;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00033560 File Offset: 0x00031760
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0003356D File Offset: 0x0003176D
		[Parameter(Mandatory = false)]
		public Guid ArchiveGuid
		{
			get
			{
				return this.DataObject.ArchiveGuid;
			}
			set
			{
				this.DataObject.ArchiveGuid = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0003357B File Offset: 0x0003177B
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00033588 File Offset: 0x00031788
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return this.DataObject.ArchiveName;
			}
			set
			{
				this.DataObject.ArchiveName = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00033596 File Offset: 0x00031796
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x000335AD File Offset: 0x000317AD
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

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x000335C0 File Offset: 0x000317C0
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x000335D7 File Offset: 0x000317D7
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

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x000335EA File Offset: 0x000317EA
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00033601 File Offset: 0x00031801
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

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00033614 File Offset: 0x00031814
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0003362B File Offset: 0x0003182B
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

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0003363E File Offset: 0x0003183E
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00033655 File Offset: 0x00031855
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

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00033668 File Offset: 0x00031868
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0003367F File Offset: 0x0003187F
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

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00033692 File Offset: 0x00031892
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0003369F File Offset: 0x0003189F
		[Parameter(Mandatory = false)]
		public bool AntispamBypassEnabled
		{
			get
			{
				return this.DataObject.AntispamBypassEnabled;
			}
			set
			{
				this.DataObject.AntispamBypassEnabled = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x000336AD File Offset: 0x000318AD
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x000336BA File Offset: 0x000318BA
		[Parameter(Mandatory = false)]
		public string AssistantName
		{
			get
			{
				return this.DataObject.AssistantName;
			}
			set
			{
				this.DataObject.AssistantName = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000336C8 File Offset: 0x000318C8
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x000336D5 File Offset: 0x000318D5
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

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x000336E3 File Offset: 0x000318E3
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x000336F0 File Offset: 0x000318F0
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

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000336FE File Offset: 0x000318FE
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0003370B File Offset: 0x0003190B
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

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00033719 File Offset: 0x00031919
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00033726 File Offset: 0x00031926
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00033734 File Offset: 0x00031934
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00033741 File Offset: 0x00031941
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0003374F File Offset: 0x0003194F
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0003375C File Offset: 0x0003195C
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

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0003376A File Offset: 0x0003196A
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00033777 File Offset: 0x00031977
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

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00033785 File Offset: 0x00031985
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00033792 File Offset: 0x00031992
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

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x000337A0 File Offset: 0x000319A0
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x000337AD File Offset: 0x000319AD
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

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000337BB File Offset: 0x000319BB
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x000337C8 File Offset: 0x000319C8
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

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x000337D6 File Offset: 0x000319D6
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x000337E3 File Offset: 0x000319E3
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

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000337F1 File Offset: 0x000319F1
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x000337FE File Offset: 0x000319FE
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

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0003380C File Offset: 0x00031A0C
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00033819 File Offset: 0x00031A19
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

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00033827 File Offset: 0x00031A27
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00033834 File Offset: 0x00031A34
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

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00033842 File Offset: 0x00031A42
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0003384F File Offset: 0x00031A4F
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

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0003385D File Offset: 0x00031A5D
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0003386A File Offset: 0x00031A6A
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00033878 File Offset: 0x00031A78
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00033885 File Offset: 0x00031A85
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

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00033893 File Offset: 0x00031A93
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x000338A0 File Offset: 0x00031AA0
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

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000338AE File Offset: 0x00031AAE
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x000338BB File Offset: 0x00031ABB
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x000338C9 File Offset: 0x00031AC9
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x000338D6 File Offset: 0x00031AD6
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

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x000338E4 File Offset: 0x00031AE4
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x000338F1 File Offset: 0x00031AF1
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000338FF File Offset: 0x00031AFF
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00033907 File Offset: 0x00031B07
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00033910 File Offset: 0x00031B10
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0003391D File Offset: 0x00031B1D
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

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0003392B File Offset: 0x00031B2B
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x00033942 File Offset: 0x00031B42
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

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00033955 File Offset: 0x00031B55
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0003396C File Offset: 0x00031B6C
		[Parameter(Mandatory = false)]
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return (bool)base.Fields[ADRecipientSchema.HiddenFromAddressListsEnabled];
			}
			set
			{
				base.Fields[ADRecipientSchema.HiddenFromAddressListsEnabled] = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00033984 File Offset: 0x00031B84
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00033991 File Offset: 0x00031B91
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return this.DataObject.Notes;
			}
			set
			{
				this.DataObject.Notes = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0003399F File Offset: 0x00031B9F
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x000339AC File Offset: 0x00031BAC
		[Parameter(Mandatory = false)]
		public int? ResourceCapacity
		{
			get
			{
				return this.DataObject.ResourceCapacity;
			}
			set
			{
				this.DataObject.ResourceCapacity = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x000339BA File Offset: 0x00031BBA
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x000339C7 File Offset: 0x00031BC7
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceCustom
		{
			get
			{
				return this.DataObject.ResourceCustom;
			}
			set
			{
				this.DataObject.ResourceCustom = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000339D5 File Offset: 0x00031BD5
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x000339E2 File Offset: 0x00031BE2
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

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x000339F0 File Offset: 0x00031BF0
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x000339FD File Offset: 0x00031BFD
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

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00033A0B File Offset: 0x00031C0B
		// (set) Token: 0x06000CDB RID: 3291 RVA: 0x00033A18 File Offset: 0x00031C18
		[Parameter(Mandatory = false)]
		public int? SCLDeleteThreshold
		{
			get
			{
				return this.DataObject.SCLDeleteThreshold;
			}
			set
			{
				this.DataObject.SCLDeleteThreshold = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00033A26 File Offset: 0x00031C26
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00033A33 File Offset: 0x00031C33
		[Parameter(Mandatory = false)]
		public int? SCLQuarantineThreshold
		{
			get
			{
				return this.DataObject.SCLQuarantineThreshold;
			}
			set
			{
				this.DataObject.SCLQuarantineThreshold = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00033A41 File Offset: 0x00031C41
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00033A4E File Offset: 0x00031C4E
		[Parameter(Mandatory = false)]
		public int? SCLJunkThreshold
		{
			get
			{
				return this.DataObject.SCLJunkThreshold;
			}
			set
			{
				this.DataObject.SCLJunkThreshold = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00033A5C File Offset: 0x00031C5C
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00033A69 File Offset: 0x00031C69
		[Parameter(Mandatory = false)]
		public int? SCLRejectThreshold
		{
			get
			{
				return this.DataObject.SCLRejectThreshold;
			}
			set
			{
				this.DataObject.SCLRejectThreshold = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00033A77 File Offset: 0x00031C77
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00033A84 File Offset: 0x00031C84
		[Parameter(Mandatory = false)]
		public byte[] Picture
		{
			get
			{
				return this.DataObject.ThumbnailPhoto;
			}
			set
			{
				this.DataObject.ThumbnailPhoto = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00033A92 File Offset: 0x00031C92
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00033A9F File Offset: 0x00031C9F
		[Parameter(Mandatory = false)]
		public byte[] SpokenName
		{
			get
			{
				return this.DataObject.UMSpokenName;
			}
			set
			{
				this.DataObject.UMSpokenName = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00033AAD File Offset: 0x00031CAD
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00033ABA File Offset: 0x00031CBA
		[Parameter(Mandatory = false)]
		public UseMapiRichTextFormat UseMapiRichTextFormat
		{
			get
			{
				return this.DataObject.UseMapiRichTextFormat;
			}
			set
			{
				this.DataObject.UseMapiRichTextFormat = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00033AC8 File Offset: 0x00031CC8
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x00033AD5 File Offset: 0x00031CD5
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

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00033AE3 File Offset: 0x00031CE3
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00033AF0 File Offset: 0x00031CF0
		[Parameter(Mandatory = false)]
		public string City
		{
			get
			{
				return this.DataObject.City;
			}
			set
			{
				this.DataObject.City = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00033AFE File Offset: 0x00031CFE
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x00033B0B File Offset: 0x00031D0B
		[Parameter(Mandatory = false)]
		public string Company
		{
			get
			{
				return this.DataObject.Company;
			}
			set
			{
				this.DataObject.Company = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00033B19 File Offset: 0x00031D19
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00033B30 File Offset: 0x00031D30
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)base.Fields[SyncMailboxSchema.CountryOrRegion];
			}
			set
			{
				base.Fields[SyncMailboxSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00033B43 File Offset: 0x00031D43
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00033B50 File Offset: 0x00031D50
		[Parameter(Mandatory = false)]
		public string Co
		{
			get
			{
				return this.DataObject.Co;
			}
			set
			{
				this.DataObject.Co = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00033B5E File Offset: 0x00031D5E
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00033B6B File Offset: 0x00031D6B
		[Parameter(Mandatory = false)]
		public string C
		{
			get
			{
				return this.DataObject.C;
			}
			set
			{
				this.DataObject.C = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00033B79 File Offset: 0x00031D79
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00033B86 File Offset: 0x00031D86
		[Parameter(Mandatory = false)]
		public int CountryCode
		{
			get
			{
				return this.DataObject.CountryCode;
			}
			set
			{
				this.DataObject.CountryCode = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00033B94 File Offset: 0x00031D94
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00033BA1 File Offset: 0x00031DA1
		[Parameter(Mandatory = false)]
		public string Department
		{
			get
			{
				return this.DataObject.Department;
			}
			set
			{
				this.DataObject.Department = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00033BAF File Offset: 0x00031DAF
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00033BBC File Offset: 0x00031DBC
		[Parameter(Mandatory = false)]
		public string Fax
		{
			get
			{
				return this.DataObject.Fax;
			}
			set
			{
				this.DataObject.Fax = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00033BCA File Offset: 0x00031DCA
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00033BD7 File Offset: 0x00031DD7
		[Parameter(Mandatory = false)]
		public string HomePhone
		{
			get
			{
				return this.DataObject.HomePhone;
			}
			set
			{
				this.DataObject.HomePhone = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00033BE5 File Offset: 0x00031DE5
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00033BFC File Offset: 0x00031DFC
		[Parameter(Mandatory = false)]
		public UserContactIdParameter Manager
		{
			get
			{
				return (UserContactIdParameter)base.Fields["Manager"];
			}
			set
			{
				base.Fields["Manager"] = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00033C0F File Offset: 0x00031E0F
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00033C1C File Offset: 0x00031E1C
		[Parameter(Mandatory = false)]
		public string MobilePhone
		{
			get
			{
				return this.DataObject.MobilePhone;
			}
			set
			{
				this.DataObject.MobilePhone = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00033C2A File Offset: 0x00031E2A
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00033C37 File Offset: 0x00031E37
		[Parameter(Mandatory = false)]
		public string Office
		{
			get
			{
				return this.DataObject.Office;
			}
			set
			{
				this.DataObject.Office = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00033C45 File Offset: 0x00031E45
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00033C52 File Offset: 0x00031E52
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return this.DataObject.OtherFax;
			}
			set
			{
				this.DataObject.OtherFax = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00033C60 File Offset: 0x00031E60
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00033C6D File Offset: 0x00031E6D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return this.DataObject.OtherHomePhone;
			}
			set
			{
				this.DataObject.OtherHomePhone = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00033C7B File Offset: 0x00031E7B
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00033C88 File Offset: 0x00031E88
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return this.DataObject.OtherTelephone;
			}
			set
			{
				this.DataObject.OtherTelephone = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00033C96 File Offset: 0x00031E96
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00033CA3 File Offset: 0x00031EA3
		[Parameter(Mandatory = false)]
		public string Pager
		{
			get
			{
				return this.DataObject.Pager;
			}
			set
			{
				this.DataObject.Pager = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00033CB1 File Offset: 0x00031EB1
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00033CBE File Offset: 0x00031EBE
		[Parameter(Mandatory = false)]
		public string Phone
		{
			get
			{
				return this.DataObject.Phone;
			}
			set
			{
				this.DataObject.Phone = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00033CCC File Offset: 0x00031ECC
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00033CD9 File Offset: 0x00031ED9
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return this.DataObject.PostalCode;
			}
			set
			{
				this.DataObject.PostalCode = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00033CE7 File Offset: 0x00031EE7
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00033CF4 File Offset: 0x00031EF4
		[Parameter(Mandatory = false)]
		public string StateOrProvince
		{
			get
			{
				return this.DataObject.StateOrProvince;
			}
			set
			{
				this.DataObject.StateOrProvince = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00033D02 File Offset: 0x00031F02
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00033D0F File Offset: 0x00031F0F
		[Parameter(Mandatory = false)]
		public string StreetAddress
		{
			get
			{
				return this.DataObject.StreetAddress;
			}
			set
			{
				this.DataObject.StreetAddress = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00033D1D File Offset: 0x00031F1D
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00033D2A File Offset: 0x00031F2A
		[Parameter(Mandatory = false)]
		public string TelephoneAssistant
		{
			get
			{
				return this.DataObject.TelephoneAssistant;
			}
			set
			{
				this.DataObject.TelephoneAssistant = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00033D38 File Offset: 0x00031F38
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00033D45 File Offset: 0x00031F45
		[Parameter(Mandatory = false)]
		public string Title
		{
			get
			{
				return this.DataObject.Title;
			}
			set
			{
				this.DataObject.Title = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00033D53 File Offset: 0x00031F53
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00033D60 File Offset: 0x00031F60
		[Parameter(Mandatory = false)]
		public string WebPage
		{
			get
			{
				return this.DataObject.WebPage;
			}
			set
			{
				this.DataObject.WebPage = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00033D6E File Offset: 0x00031F6E
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00033D7B File Offset: 0x00031F7B
		[Parameter(Mandatory = true, ParameterSetName = "LinkedWithSyncMailbox")]
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return this.DataObject.MasterAccountSid;
			}
			set
			{
				this.DataObject.MasterAccountSid = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00033D89 File Offset: 0x00031F89
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00033D91 File Offset: 0x00031F91
		[Parameter(Mandatory = false)]
		public MailboxPlanIdParameter MailboxPlanName
		{
			get
			{
				return this.MailboxPlan;
			}
			set
			{
				this.MailboxPlan = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00033D9A File Offset: 0x00031F9A
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00033DA2 File Offset: 0x00031FA2
		public new MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return base.MailboxPlan;
			}
			set
			{
				base.MailboxPlan = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00033DAB File Offset: 0x00031FAB
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00033DC2 File Offset: 0x00031FC2
		[Parameter(Mandatory = false)]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)base.Fields[MailboxSchema.DeliverToMailboxAndForward];
			}
			set
			{
				base.Fields[MailboxSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00033DDA File Offset: 0x00031FDA
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00033DF1 File Offset: 0x00031FF1
		[Parameter(Mandatory = false)]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)base.Fields[MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled] = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00033E09 File Offset: 0x00032009
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00033E16 File Offset: 0x00032016
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

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00033E24 File Offset: 0x00032024
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00033E31 File Offset: 0x00032031
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

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00033E3F File Offset: 0x0003203F
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00033E4C File Offset: 0x0003204C
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

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00033E5A File Offset: 0x0003205A
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00033E67 File Offset: 0x00032067
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00033E75 File Offset: 0x00032075
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00033E82 File Offset: 0x00032082
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00033E90 File Offset: 0x00032090
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00033EB6 File Offset: 0x000320B6
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

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00033ECE File Offset: 0x000320CE
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00033EDB File Offset: 0x000320DB
		[Parameter(Mandatory = false)]
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return this.DataObject.RemoteRecipientType;
			}
			set
			{
				this.DataObject.RemoteRecipientType = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00033EE9 File Offset: 0x000320E9
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00033EF6 File Offset: 0x000320F6
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return this.DataObject.UserCertificate;
			}
			set
			{
				this.DataObject.UserCertificate = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00033F04 File Offset: 0x00032104
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00033F11 File Offset: 0x00032111
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserSMimeCertificate
		{
			get
			{
				return this.DataObject.UserSMIMECertificate;
			}
			set
			{
				this.DataObject.UserSMIMECertificate = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00033F1F File Offset: 0x0003211F
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00033F27 File Offset: 0x00032127
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		public WindowsLiveId ResourceWindowsLiveID
		{
			get
			{
				return base.WindowsLiveID;
			}
			set
			{
				base.WindowsLiveID = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00033F30 File Offset: 0x00032130
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00033F38 File Offset: 0x00032138
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		public SwitchParameter UseExistingResourceLiveId
		{
			get
			{
				return base.UseExistingLiveId;
			}
			set
			{
				base.UseExistingLiveId = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00033F41 File Offset: 0x00032141
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00033F49 File Offset: 0x00032149
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		public new NetID NetID
		{
			get
			{
				return base.NetID;
			}
			set
			{
				base.NetID = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00033F52 File Offset: 0x00032152
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00033F5A File Offset: 0x0003215A
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		public new SwitchParameter BypassLiveId
		{
			get
			{
				return base.BypassLiveId;
			}
			set
			{
				base.BypassLiveId = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00033F63 File Offset: 0x00032163
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00033F6B File Offset: 0x0003216B
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		public new CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
			set
			{
				base.UsageLocation = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00033F74 File Offset: 0x00032174
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00033F8B File Offset: 0x0003218B
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

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00033F9E File Offset: 0x0003219E
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00033FB5 File Offset: 0x000321B5
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

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00033FC8 File Offset: 0x000321C8
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00033FDF File Offset: 0x000321DF
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)base.Fields["ReleaseTrack"];
			}
			set
			{
				base.Fields["ReleaseTrack"] = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00033FF7 File Offset: 0x000321F7
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0003400E File Offset: 0x0003220E
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

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00034021 File Offset: 0x00032221
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00034038 File Offset: 0x00032238
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> InPlaceHoldsRaw
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[SyncMailboxSchema.InPlaceHoldsRaw];
			}
			set
			{
				base.Fields[SyncMailboxSchema.InPlaceHoldsRaw] = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0003404B File Offset: 0x0003224B
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00034062 File Offset: 0x00032262
		[Parameter(Mandatory = false)]
		public new SwitchParameter AccountDisabled
		{
			get
			{
				return (SwitchParameter)base.Fields[SyncMailboxSchema.AccountDisabled];
			}
			set
			{
				base.Fields[SyncMailboxSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0003407A File Offset: 0x0003227A
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00034087 File Offset: 0x00032287
		[Parameter(Mandatory = false)]
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return this.DataObject.StsRefreshTokensValidFrom;
			}
			set
			{
				this.DataObject.StsRefreshTokensValidFrom = value;
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00034098 File Offset: 0x00032298
		public NewSyncMailbox()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfNewSyncMailboxCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulNewSyncMailboxCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageNewSyncMailboxResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageNewSyncMailboxResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageNewSyncMailboxResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageNewSyncMailboxResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageNewSyncMailboxResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageNewSyncMailboxResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalNewSyncMailboxResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.NewSyncMailboxCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.NewSyncMailboxCacheActivePercentageBase;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00034124 File Offset: 0x00032324
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00034134 File Offset: 0x00032334
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewMailboxOrSyncMailbox.InternalBeginProcessing", LoggerHelper.CmdletPerfMonitors))
			{
				if ((this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0) || (this.SipAddresses != null && this.SipAddresses.Count > 0))
				{
					this.DataObject.EmailAddresses = SyncTaskHelper.MergeAddresses(this.SmtpAndX500Addresses, this.SipAddresses);
				}
				base.InternalBeginProcessing();
				if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
				}
				if (base.Fields.IsModified(SyncMailboxSchema.CountryOrRegion) && (this.DataObject.IsModified(SyncMailboxSchema.C) || this.DataObject.IsModified(SyncMailboxSchema.Co) || this.DataObject.IsModified(SyncMailboxSchema.CountryCode)))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorConflictCountryOrRegion), ExchangeErrorCategory.Client, null);
				}
				if (base.Fields.IsModified("Manager"))
				{
					this.manager = MailboxTaskHelper.LookupManager(this.Manager, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), ExchangeErrorCategory.Client, base.TenantGlobalCatalogSession);
				}
				if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.GrantSendOnBehalfTo != null && this.GrantSendOnBehalfTo.Length != 0)
				{
					this.grantSendOnBehalfTo = new MultiValuedProperty<ADRecipient>();
					foreach (RecipientIdParameter recipientIdParameter in this.GrantSendOnBehalfTo)
					{
						ADRecipient item = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), ExchangeErrorCategory.Client);
						this.grantSendOnBehalfTo.Add(item);
					}
				}
				if (!base.Fields.IsModified(ADRecipientSchema.BypassModerationFromSendersOrMembers))
				{
					if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom) && this.BypassModerationFrom != null)
					{
						MultiValuedProperty<ADObjectId> multiValuedProperty;
						MultiValuedProperty<ADObjectId> multiValuedProperty2;
						this.bypassModerationFromRecipient = SetMailEnabledRecipientObjectTask<MailboxIdParameter, SyncMailbox, ADUser>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFrom, "BypassModerationFrom", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty, out multiValuedProperty2);
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
						this.bypassModerationFromDLMembersRecipient = SetMailEnabledRecipientObjectTask<MailboxIdParameter, SyncMailbox, ADUser>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFromDLMembers, "BypassModerationFromDLMembers", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty3, out multiValuedProperty4);
						if (multiValuedProperty4 != null && multiValuedProperty4.Count > 0)
						{
							base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorGroupRecipientNeeded(multiValuedProperty4[0].ToString())), ExchangeErrorCategory.Client, null);
						}
						this.bypassModerationFromDLMembers = multiValuedProperty3;
					}
				}
				if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom) && this.AcceptMessagesOnlyFrom != null && this.AcceptMessagesOnlyFrom.Length != 0)
				{
					this.acceptMessagesOnlyFrom = new MultiValuedProperty<ADRecipient>();
					foreach (DeliveryRecipientIdParameter recipientIdParameter2 in this.AcceptMessagesOnlyFrom)
					{
						ADRecipient item2 = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
						this.acceptMessagesOnlyFrom.Add(item2);
					}
				}
				if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers) && this.AcceptMessagesOnlyFromDLMembers != null && this.AcceptMessagesOnlyFromDLMembers.Length != 0)
				{
					this.acceptMessagesOnlyFromDLMembers = new MultiValuedProperty<ADRecipient>();
					foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter in this.AcceptMessagesOnlyFromDLMembers)
					{
						ADRecipient item3 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter.ToString())), ExchangeErrorCategory.Client);
						this.acceptMessagesOnlyFromDLMembers.Add(item3);
					}
				}
				if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom) && this.RejectMessagesFrom != null && this.RejectMessagesFrom.Length != 0)
				{
					this.rejectMessagesFrom = new MultiValuedProperty<ADRecipient>();
					foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter2 in this.RejectMessagesFrom)
					{
						ADRecipient item4 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
						this.rejectMessagesFrom.Add(item4);
					}
				}
				if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers) && this.RejectMessagesFromDLMembers != null && this.RejectMessagesFromDLMembers.Length != 0)
				{
					this.rejectMessagesFromDLMembers = new MultiValuedProperty<ADRecipient>();
					foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter3 in this.RejectMessagesFromDLMembers)
					{
						ADRecipient item5 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter3, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter3.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter3.ToString())), ExchangeErrorCategory.Client);
						this.rejectMessagesFromDLMembers.Add(item5);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003470C File Offset: 0x0003290C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.manager != null)
			{
				RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, this.manager, new Task.ErrorLoggerDelegate(base.WriteError));
			}
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
			if (this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.ArchiveGuid != Guid.Empty)
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewMailboxOrSyncMailbox.IsExchangeGuidOrArchiveGuidUnique", LoggerHelper.CmdletPerfMonitors))
				{
					RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADUserSchema.ArchiveGuid, this.ArchiveGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003492C File Offset: 0x00032B2C
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			user.BypassModerationCheck = true;
			if (base.Fields.IsModified("Manager"))
			{
				user.Manager = ((this.manager == null) ? null : ((ADObjectId)this.manager.Identity));
			}
			if (base.Fields.IsModified(ADRecipientSchema.HiddenFromAddressListsEnabled))
			{
				user.HiddenFromAddressListsEnabled = this.HiddenFromAddressListsEnabled;
			}
			if (base.Fields.IsModified(MailboxSchema.DeliverToMailboxAndForward))
			{
				user.DeliverToMailboxAndForward = this.DeliverToMailboxAndForward;
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled))
			{
				user.RequireAllSendersAreAuthenticated = this.RequireSenderAuthenticationEnabled;
			}
			if (base.Fields.IsModified(SyncMailboxSchema.ReleaseTrack))
			{
				user.ReleaseTrack = this.ReleaseTrack;
			}
			if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.grantSendOnBehalfTo != null)
			{
				foreach (ADRecipient adrecipient in this.grantSendOnBehalfTo)
				{
					user.GrantSendOnBehalfTo.Add(adrecipient.Identity as ADObjectId);
				}
			}
			if (!base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromSendersOrMembers))
			{
				if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom))
				{
					user.BypassModerationFrom = this.bypassModerationFrom;
				}
				if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromDLMembers))
				{
					user.BypassModerationFromDLMembers = this.bypassModerationFromDLMembers;
				}
			}
			if (this.DataObject.IsModified(ADRecipientSchema.EmailAddresses))
			{
				user.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, this.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom))
			{
				user.AcceptMessagesOnlyFrom = (from c in this.acceptMessagesOnlyFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers))
			{
				user.AcceptMessagesOnlyFromDLMembers = (from c in this.acceptMessagesOnlyFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom))
			{
				user.RejectMessagesFrom = (from c in this.rejectMessagesFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers))
			{
				user.RejectMessagesFromDLMembers = (from c in this.rejectMessagesFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(SyncMailboxSchema.CountryOrRegion))
			{
				user.CountryOrRegion = this.CountryOrRegion;
			}
			if (base.Fields.IsModified(SyncMailboxSchema.InPlaceHoldsRaw))
			{
				user[ADRecipientSchema.InPlaceHoldsRaw] = this.InPlaceHoldsRaw;
			}
			if (base.Fields.IsModified(ADRecipientSchema.Certificate))
			{
				user.UserCertificate = this.UserCertificate;
			}
			if (base.Fields.IsModified(ADRecipientSchema.SMimeCertificate))
			{
				user.UserSMIMECertificate = this.UserSMimeCertificate;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00034CA8 File Offset: 0x00032EA8
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewMailboxOrSyncMailbox.WriteResult", LoggerHelper.CmdletPerfMonitors))
			{
				ADUser aduser = (ADUser)result;
				if (null != aduser.MasterAccountSid)
				{
					aduser.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(aduser.MasterAccountSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
				if (this.mailboxPlanObject != null)
				{
					aduser.MailboxPlanName = this.mailboxPlanObject.DisplayName;
				}
				aduser.ResetChangeTracking();
				SyncMailbox result2 = new SyncMailbox(aduser);
				base.WriteResult(result2);
				GalsyncCounters.NumberOfMailboxesCreated.Increment();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00034D78 File Offset: 0x00032F78
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(SyncMailbox).FullName;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00034D8C File Offset: 0x00032F8C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			IConfigurable result;
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewMailboxOrSyncMailbox.ConvertDataObjectToPresentationObject", LoggerHelper.CmdletPerfMonitors))
			{
				result = SyncMailbox.FromDataObject((ADUser)dataObject);
			}
			return result;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00034DE4 File Offset: 0x00032FE4
		protected override void StampChangesAfterSettingPassword()
		{
			base.StampChangesAfterSettingPassword();
			if (base.Fields.IsModified(SyncMailboxSchema.AccountDisabled))
			{
				SyncTaskHelper.SetExchangeAccountDisabledWithADLogon(this.DataObject, this.AccountDisabled);
			}
		}

		// Token: 0x040002A6 RID: 678
		private ADObject manager;

		// Token: 0x040002A7 RID: 679
		private MultiValuedProperty<ADRecipient> grantSendOnBehalfTo;

		// Token: 0x040002A8 RID: 680
		private MultiValuedProperty<ADObjectId> bypassModerationFrom;

		// Token: 0x040002A9 RID: 681
		private MultiValuedProperty<ADRecipient> bypassModerationFromRecipient;

		// Token: 0x040002AA RID: 682
		private MultiValuedProperty<ADObjectId> bypassModerationFromDLMembers;

		// Token: 0x040002AB RID: 683
		private MultiValuedProperty<ADRecipient> bypassModerationFromDLMembersRecipient;

		// Token: 0x040002AC RID: 684
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFrom;

		// Token: 0x040002AD RID: 685
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFromDLMembers;

		// Token: 0x040002AE RID: 686
		private MultiValuedProperty<ADRecipient> rejectMessagesFrom;

		// Token: 0x040002AF RID: 687
		private MultiValuedProperty<ADRecipient> rejectMessagesFromDLMembers;
	}
}
