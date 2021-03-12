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
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000C8 RID: 200
	[Cmdlet("New", "SyncMailContact", SupportsShouldProcess = true)]
	public sealed class NewSyncMailContact : NewMailContactBase
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00036059 File Offset: 0x00034259
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x00036070 File Offset: 0x00034270
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

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00036083 File Offset: 0x00034283
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0003609A File Offset: 0x0003429A
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

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x000360AD File Offset: 0x000342AD
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x000360C4 File Offset: 0x000342C4
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

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000360D7 File Offset: 0x000342D7
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x000360EE File Offset: 0x000342EE
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

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00036101 File Offset: 0x00034301
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0003610E File Offset: 0x0003430E
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

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0003611C File Offset: 0x0003431C
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00036129 File Offset: 0x00034329
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

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00036137 File Offset: 0x00034337
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0003614E File Offset: 0x0003434E
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

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00036161 File Offset: 0x00034361
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x00036178 File Offset: 0x00034378
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

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0003618B File Offset: 0x0003438B
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x00036198 File Offset: 0x00034398
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

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x000361A6 File Offset: 0x000343A6
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x000361B3 File Offset: 0x000343B3
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x000361C1 File Offset: 0x000343C1
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x000361CE File Offset: 0x000343CE
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

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000361DC File Offset: 0x000343DC
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x000361E9 File Offset: 0x000343E9
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

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000361F7 File Offset: 0x000343F7
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00036204 File Offset: 0x00034404
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

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00036212 File Offset: 0x00034412
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0003621F File Offset: 0x0003441F
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

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0003622D File Offset: 0x0003442D
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0003623A File Offset: 0x0003443A
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

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00036248 File Offset: 0x00034448
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00036255 File Offset: 0x00034455
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

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00036263 File Offset: 0x00034463
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x00036270 File Offset: 0x00034470
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

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003627E File Offset: 0x0003447E
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0003628B File Offset: 0x0003448B
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

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00036299 File Offset: 0x00034499
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x000362A6 File Offset: 0x000344A6
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

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000362B4 File Offset: 0x000344B4
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x000362C1 File Offset: 0x000344C1
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

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000362CF File Offset: 0x000344CF
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x000362DC File Offset: 0x000344DC
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x000362EA File Offset: 0x000344EA
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x000362F7 File Offset: 0x000344F7
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

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00036305 File Offset: 0x00034505
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x00036312 File Offset: 0x00034512
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

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00036320 File Offset: 0x00034520
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0003632D File Offset: 0x0003452D
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

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0003633B File Offset: 0x0003453B
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00036348 File Offset: 0x00034548
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

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00036356 File Offset: 0x00034556
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00036363 File Offset: 0x00034563
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

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00036371 File Offset: 0x00034571
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0003637E File Offset: 0x0003457E
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

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0003638C File Offset: 0x0003458C
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00036399 File Offset: 0x00034599
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

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x000363A7 File Offset: 0x000345A7
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x000363B4 File Offset: 0x000345B4
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x000363C2 File Offset: 0x000345C2
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x000363D9 File Offset: 0x000345D9
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

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000363EC File Offset: 0x000345EC
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x000363F9 File Offset: 0x000345F9
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

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00036407 File Offset: 0x00034607
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x0003640F File Offset: 0x0003460F
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

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00036418 File Offset: 0x00034618
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00036425 File Offset: 0x00034625
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

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x00036433 File Offset: 0x00034633
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x00036440 File Offset: 0x00034640
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

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0003644E File Offset: 0x0003464E
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0003645B File Offset: 0x0003465B
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

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x00036469 File Offset: 0x00034669
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x00036476 File Offset: 0x00034676
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

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00036484 File Offset: 0x00034684
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00036491 File Offset: 0x00034691
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0003649F File Offset: 0x0003469F
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x000364AC File Offset: 0x000346AC
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

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000364BA File Offset: 0x000346BA
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x000364C7 File Offset: 0x000346C7
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

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x000364D5 File Offset: 0x000346D5
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x000364EC File Offset: 0x000346EC
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)base.Fields[SyncMailContactSchema.CountryOrRegion];
			}
			set
			{
				base.Fields[SyncMailContactSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x000364FF File Offset: 0x000346FF
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x0003650C File Offset: 0x0003470C
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003651A File Offset: 0x0003471A
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x00036527 File Offset: 0x00034727
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

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00036535 File Offset: 0x00034735
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x00036542 File Offset: 0x00034742
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

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00036550 File Offset: 0x00034750
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0003655D File Offset: 0x0003475D
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0003656B File Offset: 0x0003476B
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x00036578 File Offset: 0x00034778
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00036586 File Offset: 0x00034786
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x00036593 File Offset: 0x00034793
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

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x000365A1 File Offset: 0x000347A1
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x000365B8 File Offset: 0x000347B8
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

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x000365CB File Offset: 0x000347CB
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x000365D8 File Offset: 0x000347D8
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

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000365E6 File Offset: 0x000347E6
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x000365F3 File Offset: 0x000347F3
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

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00036601 File Offset: 0x00034801
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0003660E File Offset: 0x0003480E
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

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003661C File Offset: 0x0003481C
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x00036629 File Offset: 0x00034829
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

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00036637 File Offset: 0x00034837
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x00036644 File Offset: 0x00034844
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

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00036652 File Offset: 0x00034852
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0003665F File Offset: 0x0003485F
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

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003666D File Offset: 0x0003486D
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0003667A File Offset: 0x0003487A
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

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00036688 File Offset: 0x00034888
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00036695 File Offset: 0x00034895
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

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x000366A3 File Offset: 0x000348A3
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x000366B0 File Offset: 0x000348B0
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

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000366BE File Offset: 0x000348BE
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x000366CB File Offset: 0x000348CB
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

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000366D9 File Offset: 0x000348D9
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x000366E6 File Offset: 0x000348E6
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

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000366F4 File Offset: 0x000348F4
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00036701 File Offset: 0x00034901
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

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003670F File Offset: 0x0003490F
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0003671C File Offset: 0x0003491C
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

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003672A File Offset: 0x0003492A
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00036737 File Offset: 0x00034937
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

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00036745 File Offset: 0x00034945
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00036752 File Offset: 0x00034952
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

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00036760 File Offset: 0x00034960
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0003676D File Offset: 0x0003496D
		[Parameter(Mandatory = false)]
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0003677B File Offset: 0x0003497B
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00036788 File Offset: 0x00034988
		[Parameter(Mandatory = false)]
		public ExchangeResourceType? ResourceType
		{
			get
			{
				return this.DataObject.ResourceType;
			}
			set
			{
				this.DataObject.ResourceType = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00036796 File Offset: 0x00034996
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x000367A3 File Offset: 0x000349A3
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x000367B1 File Offset: 0x000349B1
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x000367BE File Offset: 0x000349BE
		[Parameter(Mandatory = false)]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return this.DataObject.RequireAllSendersAreAuthenticated;
			}
			set
			{
				this.DataObject.RequireAllSendersAreAuthenticated = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000367CC File Offset: 0x000349CC
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x000367D9 File Offset: 0x000349D9
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000367E7 File Offset: 0x000349E7
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x000367F4 File Offset: 0x000349F4
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

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00036802 File Offset: 0x00034A02
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0003680F File Offset: 0x00034A0F
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

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0003681D File Offset: 0x00034A1D
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0003682A File Offset: 0x00034A2A
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00036838 File Offset: 0x00034A38
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00036845 File Offset: 0x00034A45
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

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00036853 File Offset: 0x00034A53
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00036879 File Offset: 0x00034A79
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

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00036891 File Offset: 0x00034A91
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x000368A8 File Offset: 0x00034AA8
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

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x000368BB File Offset: 0x00034ABB
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x000368D2 File Offset: 0x00034AD2
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

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x000368E5 File Offset: 0x00034AE5
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x000368FC File Offset: 0x00034AFC
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

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003690F File Offset: 0x00034B0F
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00036920 File Offset: 0x00034B20
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified(SyncMailContactSchema.CountryOrRegion) && (this.DataObject.IsModified(SyncMailContactSchema.C) || this.DataObject.IsModified(SyncMailContactSchema.Co) || this.DataObject.IsModified(SyncMailContactSchema.CountryCode)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictCountryOrRegion), ExchangeErrorCategory.Client, null);
			}
			if ((this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0) || (this.SipAddresses != null && this.SipAddresses.Count > 0))
			{
				this.DataObject.EmailAddresses = SyncTaskHelper.MergeAddresses(this.SmtpAndX500Addresses, this.SipAddresses);
			}
			base.InternalBeginProcessing();
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
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
					this.bypassModerationFromRecipient = SetMailEnabledRecipientObjectTask<MailContactIdParameter, SyncMailContact, ADContact>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFrom, "BypassModerationFrom", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty, out multiValuedProperty2);
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
					this.bypassModerationFromDLMembersRecipient = SetMailEnabledRecipientObjectTask<MailContactIdParameter, SyncMailContact, ADContact>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFromDLMembers, "BypassModerationFromDLMembers", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty3, out multiValuedProperty4);
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
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter in this.AcceptMessagesOnlyFrom)
				{
					ADRecipient item2 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter.ToString())), ExchangeErrorCategory.Client);
					this.acceptMessagesOnlyFrom.Add(item2);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers) && this.AcceptMessagesOnlyFromDLMembers != null && this.AcceptMessagesOnlyFromDLMembers.Length != 0)
			{
				this.acceptMessagesOnlyFromDLMembers = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter2 in this.AcceptMessagesOnlyFromDLMembers)
				{
					ADRecipient item3 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
					this.acceptMessagesOnlyFromDLMembers.Add(item3);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom) && this.RejectMessagesFrom != null && this.RejectMessagesFrom.Length != 0)
			{
				this.rejectMessagesFrom = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter3 in this.RejectMessagesFrom)
				{
					ADRecipient item4 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter3, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter3.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter3.ToString())), ExchangeErrorCategory.Client);
					this.rejectMessagesFrom.Add(item4);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers) && this.RejectMessagesFromDLMembers != null && this.RejectMessagesFromDLMembers.Length != 0)
			{
				this.rejectMessagesFromDLMembers = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter4 in this.RejectMessagesFromDLMembers)
				{
					ADRecipient item5 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter4, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter4.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter4.ToString())), ExchangeErrorCategory.Client);
					this.rejectMessagesFromDLMembers.Add(item5);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00036EB0 File Offset: 0x000350B0
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
			TaskLogger.LogExit();
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00037040 File Offset: 0x00035240
		protected override void PrepareRecipientObject(ADContact contact)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(contact);
			contact.BypassModerationCheck = true;
			if (base.Fields.IsModified("Manager"))
			{
				contact.Manager = ((this.manager == null) ? null : ((ADObjectId)this.manager.Identity));
			}
			if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.grantSendOnBehalfTo != null)
			{
				foreach (ADRecipient adrecipient in this.grantSendOnBehalfTo)
				{
					contact.GrantSendOnBehalfTo.Add(adrecipient.Identity as ADObjectId);
				}
			}
			if (!base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromSendersOrMembers))
			{
				if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom))
				{
					contact.BypassModerationFrom = this.bypassModerationFrom;
				}
				if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromDLMembers))
				{
					contact.BypassModerationFromDLMembers = this.bypassModerationFromDLMembers;
				}
			}
			if (this.DataObject != null && this.DataObject.IsModified(ADRecipientSchema.EmailAddresses))
			{
				contact.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, this.DataObject.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom))
			{
				contact.AcceptMessagesOnlyFrom = (from c in this.acceptMessagesOnlyFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers))
			{
				contact.AcceptMessagesOnlyFromDLMembers = (from c in this.acceptMessagesOnlyFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom))
			{
				contact.RejectMessagesFrom = (from c in this.rejectMessagesFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers))
			{
				contact.RejectMessagesFromDLMembers = (from c in this.rejectMessagesFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.Certificate))
			{
				contact.UserCertificate = this.UserCertificate;
			}
			if (base.Fields.IsModified(ADRecipientSchema.SMimeCertificate))
			{
				contact.UserSMIMECertificate = this.UserSMimeCertificate;
			}
			if (base.Fields.IsModified(SyncMailContactSchema.CountryOrRegion))
			{
				contact.CountryOrRegion = this.CountryOrRegion;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003732C File Offset: 0x0003552C
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			SyncMailContact result2 = new SyncMailContact((ADContact)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00037367 File Offset: 0x00035567
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(SyncMailContact).FullName;
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00037378 File Offset: 0x00035578
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailContact.FromDataObject((ADContact)dataObject);
		}

		// Token: 0x040002BA RID: 698
		private ADObject manager;

		// Token: 0x040002BB RID: 699
		private MultiValuedProperty<ADRecipient> grantSendOnBehalfTo;

		// Token: 0x040002BC RID: 700
		private MultiValuedProperty<ADObjectId> bypassModerationFrom;

		// Token: 0x040002BD RID: 701
		private MultiValuedProperty<ADRecipient> bypassModerationFromRecipient;

		// Token: 0x040002BE RID: 702
		private MultiValuedProperty<ADObjectId> bypassModerationFromDLMembers;

		// Token: 0x040002BF RID: 703
		private MultiValuedProperty<ADRecipient> bypassModerationFromDLMembersRecipient;

		// Token: 0x040002C0 RID: 704
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFrom;

		// Token: 0x040002C1 RID: 705
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFromDLMembers;

		// Token: 0x040002C2 RID: 706
		private MultiValuedProperty<ADRecipient> rejectMessagesFrom;

		// Token: 0x040002C3 RID: 707
		private MultiValuedProperty<ADRecipient> rejectMessagesFromDLMembers;
	}
}
