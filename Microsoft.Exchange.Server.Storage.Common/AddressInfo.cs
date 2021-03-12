using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000007 RID: 7
	public class AddressInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000411A File Offset: 0x0000231A
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00004122 File Offset: 0x00002322
		public Guid ObjectId { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000412B File Offset: 0x0000232B
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00004133 File Offset: 0x00002333
		public Guid ADCachingUseOnlyMailboxGuid { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000413C File Offset: 0x0000233C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00004144 File Offset: 0x00002344
		public string DisplayName { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000414D File Offset: 0x0000234D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00004155 File Offset: 0x00002355
		public string SimpleDisplayName { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000415E File Offset: 0x0000235E
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00004166 File Offset: 0x00002366
		public SecurityIdentifier UserSid { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000416F File Offset: 0x0000236F
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00004177 File Offset: 0x00002377
		public SecurityIdentifier[] UserSidHistory { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00004180 File Offset: 0x00002380
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00004188 File Offset: 0x00002388
		public SecurityIdentifier MasterAccountSid { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00004191 File Offset: 0x00002391
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00004199 File Offset: 0x00002399
		public string LegacyExchangeDN { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000041A2 File Offset: 0x000023A2
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000041AA File Offset: 0x000023AA
		public string DistinguishedName { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000041B3 File Offset: 0x000023B3
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000041BB File Offset: 0x000023BB
		public string OriginalEmailAddress { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000041C4 File Offset: 0x000023C4
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000041CC File Offset: 0x000023CC
		public string OriginalEmailAddressType { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000041D5 File Offset: 0x000023D5
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000041DD File Offset: 0x000023DD
		public string PrimaryEmailAddress { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000041E6 File Offset: 0x000023E6
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000041EE File Offset: 0x000023EE
		public string PrimaryEmailAddressType { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000041F7 File Offset: 0x000023F7
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000041FF File Offset: 0x000023FF
		public string PrimarySmtpAddress { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004208 File Offset: 0x00002408
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00004210 File Offset: 0x00002410
		public bool IsDistributionList { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004219 File Offset: 0x00002419
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00004221 File Offset: 0x00002421
		public bool IsMailPublicFolder { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000422A File Offset: 0x0000242A
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00004232 File Offset: 0x00002432
		public SecurityDescriptor OSSecurityDescriptor { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000423B File Offset: 0x0000243B
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00004243 File Offset: 0x00002443
		public IList<AddressInfo.PublicDelegate> PublicDelegates { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000424C File Offset: 0x0000244C
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00004254 File Offset: 0x00002454
		public bool HasMailbox { get; private set; }

		// Token: 0x060001CD RID: 461 RVA: 0x00004260 File Offset: 0x00002460
		public AddressInfo(Guid objectId, Guid cachingUseOnlyMailboxGuid, string displayName, string simpleDisplayName, SecurityIdentifier masterAccountSid, string legacyExchangeDN, string distinguishedName, SecurityIdentifier userSid, SecurityIdentifier[] userSidHistory, string userOrgEmailAddr, string userOrgAddrType, string userEmailAddress, string userAddressType, string primarySmtpAddress, bool isDistributionList, bool isMailPublicFolder, SecurityDescriptor osSecurityDescriptor, IList<AddressInfo.PublicDelegate> publicDelegates, bool hasMailbox)
		{
			this.ObjectId = objectId;
			this.ADCachingUseOnlyMailboxGuid = cachingUseOnlyMailboxGuid;
			this.DisplayName = displayName;
			this.MasterAccountSid = masterAccountSid;
			this.LegacyExchangeDN = legacyExchangeDN;
			this.DistinguishedName = distinguishedName;
			this.UserSid = userSid;
			this.UserSidHistory = userSidHistory;
			if (!string.IsNullOrEmpty(simpleDisplayName))
			{
				this.SimpleDisplayName = simpleDisplayName;
			}
			else if (!string.IsNullOrEmpty(displayName))
			{
				this.SimpleDisplayName = displayName;
			}
			else
			{
				this.SimpleDisplayName = userEmailAddress;
			}
			if (!string.IsNullOrEmpty(userOrgEmailAddr))
			{
				this.OriginalEmailAddress = userOrgEmailAddr;
				this.OriginalEmailAddressType = userOrgAddrType;
			}
			if (!string.IsNullOrEmpty(userEmailAddress))
			{
				this.PrimaryEmailAddress = userEmailAddress;
				this.PrimaryEmailAddressType = userAddressType;
			}
			if (!string.IsNullOrEmpty(primarySmtpAddress))
			{
				this.PrimarySmtpAddress = primarySmtpAddress;
			}
			this.IsDistributionList = isDistributionList;
			this.IsMailPublicFolder = isMailPublicFolder;
			this.OSSecurityDescriptor = osSecurityDescriptor;
			this.PublicDelegates = publicDelegates;
			this.HasMailbox = hasMailbox;
		}

		// Token: 0x02000008 RID: 8
		public struct PublicDelegate
		{
			// Token: 0x060001CE RID: 462 RVA: 0x00004347 File Offset: 0x00002547
			internal PublicDelegate(string distinguishedName, Guid objectId, bool isDistributionList)
			{
				this.distinguishedName = distinguishedName;
				this.objectId = objectId;
				this.isDistributionList = isDistributionList;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x060001CF RID: 463 RVA: 0x0000435E File Offset: 0x0000255E
			public string DistinguishedName
			{
				get
				{
					return this.distinguishedName;
				}
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004366 File Offset: 0x00002566
			public Guid ObjectId
			{
				get
				{
					return this.objectId;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000436E File Offset: 0x0000256E
			public bool IsDistributionList
			{
				get
				{
					return this.isDistributionList;
				}
			}

			// Token: 0x040002C2 RID: 706
			private string distinguishedName;

			// Token: 0x040002C3 RID: 707
			private Guid objectId;

			// Token: 0x040002C4 RID: 708
			private bool isDistributionList;
		}
	}
}
