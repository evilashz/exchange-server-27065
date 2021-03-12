using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200003E RID: 62
	internal abstract class RecipientProvisioningData : ProvisioningData
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000AE30 File Offset: 0x00009030
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000AE54 File Offset: 0x00009054
		public bool IsSmtpAddressCheckWithAcceptedDomain
		{
			get
			{
				object obj = base["SMTPAddressCheckWithAcceptedDomain"];
				return obj != null && (bool)obj;
			}
			set
			{
				base["SMTPAddressCheckWithAcceptedDomain"] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000AE67 File Offset: 0x00009067
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000AE79 File Offset: 0x00009079
		public string DisplayName
		{
			get
			{
				return (string)base[ADRecipientSchema.DisplayName];
			}
			set
			{
				base[ADRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000AE87 File Offset: 0x00009087
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000AE99 File Offset: 0x00009099
		public string[] EmailAddresses
		{
			get
			{
				return (string[])base[ADRecipientSchema.EmailAddresses];
			}
			set
			{
				base[ADRecipientSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000AEA7 File Offset: 0x000090A7
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000AEB9 File Offset: 0x000090B9
		public string Name
		{
			get
			{
				return (string)base[ADObjectSchema.Name];
			}
			set
			{
				base[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000AEC7 File Offset: 0x000090C7
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000AED9 File Offset: 0x000090D9
		public string CustomAttribute1
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute1];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute1] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000AEE7 File Offset: 0x000090E7
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000AEF9 File Offset: 0x000090F9
		public string CustomAttribute2
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute2];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute2] = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000AF07 File Offset: 0x00009107
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000AF19 File Offset: 0x00009119
		public string CustomAttribute3
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute3];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute3] = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000AF27 File Offset: 0x00009127
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000AF39 File Offset: 0x00009139
		public string CustomAttribute4
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute4];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute4] = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000AF47 File Offset: 0x00009147
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000AF59 File Offset: 0x00009159
		public string CustomAttribute5
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute5];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute5] = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000AF67 File Offset: 0x00009167
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000AF79 File Offset: 0x00009179
		public string CustomAttribute6
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute6];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute6] = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AF87 File Offset: 0x00009187
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000AF99 File Offset: 0x00009199
		public string CustomAttribute7
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute7];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute7] = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000AFA7 File Offset: 0x000091A7
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000AFB9 File Offset: 0x000091B9
		public string CustomAttribute8
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute8];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute8] = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000AFC7 File Offset: 0x000091C7
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000AFD9 File Offset: 0x000091D9
		public string CustomAttribute9
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute9];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute9] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000AFE7 File Offset: 0x000091E7
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000AFF9 File Offset: 0x000091F9
		public string CustomAttribute10
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute10];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute10] = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000B007 File Offset: 0x00009207
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000B019 File Offset: 0x00009219
		public string CustomAttribute11
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute11];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute11] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B027 File Offset: 0x00009227
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000B039 File Offset: 0x00009239
		public string CustomAttribute12
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute12];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute12] = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B047 File Offset: 0x00009247
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000B059 File Offset: 0x00009259
		public string CustomAttribute13
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute13];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute13] = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B067 File Offset: 0x00009267
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000B079 File Offset: 0x00009279
		public string CustomAttribute14
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute14];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute14] = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B087 File Offset: 0x00009287
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000B099 File Offset: 0x00009299
		public string CustomAttribute15
		{
			get
			{
				return (string)base[ADRecipientSchema.CustomAttribute15];
			}
			set
			{
				base[ADRecipientSchema.CustomAttribute15] = value;
			}
		}

		// Token: 0x040000E4 RID: 228
		public const string SMTPAddressCheckWithAcceptedDomainFlagName = "SMTPAddressCheckWithAcceptedDomain";

		// Token: 0x040000E5 RID: 229
		public const string MicrosoftOnlineServicesIDParameterName = "MicrosoftOnlineServicesID";
	}
}
