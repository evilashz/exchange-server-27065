using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005EA RID: 1514
	[DataServiceKey("objectId")]
	public class User : DirectoryObject
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x000300D4 File Offset: 0x0002E2D4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static User CreateUser(string objectId, Collection<AlternativeSecurityId> alternativeSecurityIds, Collection<AssignedLicense> assignedLicenses, Collection<AssignedPlan> assignedPlans, Collection<KeyValue> inviteReplyUrl, Collection<string> inviteResources, Collection<InvitationTicket> inviteTicket, Collection<LogonIdentifier> logonIdentifiers, Collection<string> otherMails, Collection<ProvisionedPlan> provisionedPlans, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses, Collection<string> smtpAddresses, DataServiceStreamLink thumbnailPhoto)
		{
			User user = new User();
			user.objectId = objectId;
			if (alternativeSecurityIds == null)
			{
				throw new ArgumentNullException("alternativeSecurityIds");
			}
			user.alternativeSecurityIds = alternativeSecurityIds;
			if (assignedLicenses == null)
			{
				throw new ArgumentNullException("assignedLicenses");
			}
			user.assignedLicenses = assignedLicenses;
			if (assignedPlans == null)
			{
				throw new ArgumentNullException("assignedPlans");
			}
			user.assignedPlans = assignedPlans;
			if (inviteReplyUrl == null)
			{
				throw new ArgumentNullException("inviteReplyUrl");
			}
			user.inviteReplyUrl = inviteReplyUrl;
			if (inviteResources == null)
			{
				throw new ArgumentNullException("inviteResources");
			}
			user.inviteResources = inviteResources;
			if (inviteTicket == null)
			{
				throw new ArgumentNullException("inviteTicket");
			}
			user.inviteTicket = inviteTicket;
			if (logonIdentifiers == null)
			{
				throw new ArgumentNullException("logonIdentifiers");
			}
			user.logonIdentifiers = logonIdentifiers;
			if (otherMails == null)
			{
				throw new ArgumentNullException("otherMails");
			}
			user.otherMails = otherMails;
			if (provisionedPlans == null)
			{
				throw new ArgumentNullException("provisionedPlans");
			}
			user.provisionedPlans = provisionedPlans;
			if (provisioningErrors == null)
			{
				throw new ArgumentNullException("provisioningErrors");
			}
			user.provisioningErrors = provisioningErrors;
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			user.proxyAddresses = proxyAddresses;
			if (smtpAddresses == null)
			{
				throw new ArgumentNullException("smtpAddresses");
			}
			user.smtpAddresses = smtpAddresses;
			user.thumbnailPhoto = thumbnailPhoto;
			return user;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x00030205 File Offset: 0x0002E405
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0003020D File Offset: 0x0002E40D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string acceptedAs
		{
			get
			{
				return this._acceptedAs;
			}
			set
			{
				this._acceptedAs = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00030216 File Offset: 0x0002E416
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x0003021E File Offset: 0x0002E41E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? acceptedOn
		{
			get
			{
				return this._acceptedOn;
			}
			set
			{
				this._acceptedOn = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00030227 File Offset: 0x0002E427
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x0003022F File Offset: 0x0002E42F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? accountEnabled
		{
			get
			{
				return this._accountEnabled;
			}
			set
			{
				this._accountEnabled = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x00030238 File Offset: 0x0002E438
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x00030240 File Offset: 0x0002E440
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AlternativeSecurityId> alternativeSecurityIds
		{
			get
			{
				return this._alternativeSecurityIds;
			}
			set
			{
				this._alternativeSecurityIds = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x00030249 File Offset: 0x0002E449
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x00030273 File Offset: 0x0002E473
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public AppMetadata appMetadata
		{
			get
			{
				if (this._appMetadata == null && !this._appMetadataInitialized)
				{
					this._appMetadata = new AppMetadata();
					this._appMetadataInitialized = true;
				}
				return this._appMetadata;
			}
			set
			{
				this._appMetadata = value;
				this._appMetadataInitialized = true;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x00030283 File Offset: 0x0002E483
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0003028B File Offset: 0x0002E48B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AssignedLicense> assignedLicenses
		{
			get
			{
				return this._assignedLicenses;
			}
			set
			{
				this._assignedLicenses = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00030294 File Offset: 0x0002E494
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0003029C File Offset: 0x0002E49C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AssignedPlan> assignedPlans
		{
			get
			{
				return this._assignedPlans;
			}
			set
			{
				this._assignedPlans = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x000302A5 File Offset: 0x0002E4A5
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x000302AD File Offset: 0x0002E4AD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string city
		{
			get
			{
				return this._city;
			}
			set
			{
				this._city = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x000302B6 File Offset: 0x0002E4B6
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x000302BE File Offset: 0x0002E4BE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string country
		{
			get
			{
				return this._country;
			}
			set
			{
				this._country = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x000302C7 File Offset: 0x0002E4C7
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x000302CF File Offset: 0x0002E4CF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string creationType
		{
			get
			{
				return this._creationType;
			}
			set
			{
				this._creationType = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x000302D8 File Offset: 0x0002E4D8
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x000302E0 File Offset: 0x0002E4E0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string department
		{
			get
			{
				return this._department;
			}
			set
			{
				this._department = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x000302E9 File Offset: 0x0002E4E9
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x000302F1 File Offset: 0x0002E4F1
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? dirSyncEnabled
		{
			get
			{
				return this._dirSyncEnabled;
			}
			set
			{
				this._dirSyncEnabled = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000302FA File Offset: 0x0002E4FA
		// (set) Token: 0x0600192F RID: 6447 RVA: 0x00030302 File Offset: 0x0002E502
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x0003030B File Offset: 0x0002E50B
		// (set) Token: 0x06001931 RID: 6449 RVA: 0x00030313 File Offset: 0x0002E513
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute1
		{
			get
			{
				return this._extensionAttribute1;
			}
			set
			{
				this._extensionAttribute1 = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0003031C File Offset: 0x0002E51C
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x00030324 File Offset: 0x0002E524
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute2
		{
			get
			{
				return this._extensionAttribute2;
			}
			set
			{
				this._extensionAttribute2 = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x0003032D File Offset: 0x0002E52D
		// (set) Token: 0x06001935 RID: 6453 RVA: 0x00030335 File Offset: 0x0002E535
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute3
		{
			get
			{
				return this._extensionAttribute3;
			}
			set
			{
				this._extensionAttribute3 = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x0003033E File Offset: 0x0002E53E
		// (set) Token: 0x06001937 RID: 6455 RVA: 0x00030346 File Offset: 0x0002E546
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute4
		{
			get
			{
				return this._extensionAttribute4;
			}
			set
			{
				this._extensionAttribute4 = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0003034F File Offset: 0x0002E54F
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x00030357 File Offset: 0x0002E557
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute5
		{
			get
			{
				return this._extensionAttribute5;
			}
			set
			{
				this._extensionAttribute5 = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00030360 File Offset: 0x0002E560
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x00030368 File Offset: 0x0002E568
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute6
		{
			get
			{
				return this._extensionAttribute6;
			}
			set
			{
				this._extensionAttribute6 = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00030371 File Offset: 0x0002E571
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x00030379 File Offset: 0x0002E579
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute7
		{
			get
			{
				return this._extensionAttribute7;
			}
			set
			{
				this._extensionAttribute7 = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00030382 File Offset: 0x0002E582
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0003038A File Offset: 0x0002E58A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute8
		{
			get
			{
				return this._extensionAttribute8;
			}
			set
			{
				this._extensionAttribute8 = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00030393 File Offset: 0x0002E593
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x0003039B File Offset: 0x0002E59B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute9
		{
			get
			{
				return this._extensionAttribute9;
			}
			set
			{
				this._extensionAttribute9 = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x000303A4 File Offset: 0x0002E5A4
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x000303AC File Offset: 0x0002E5AC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute10
		{
			get
			{
				return this._extensionAttribute10;
			}
			set
			{
				this._extensionAttribute10 = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x000303B5 File Offset: 0x0002E5B5
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x000303BD File Offset: 0x0002E5BD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute11
		{
			get
			{
				return this._extensionAttribute11;
			}
			set
			{
				this._extensionAttribute11 = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000303C6 File Offset: 0x0002E5C6
		// (set) Token: 0x06001947 RID: 6471 RVA: 0x000303CE File Offset: 0x0002E5CE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute12
		{
			get
			{
				return this._extensionAttribute12;
			}
			set
			{
				this._extensionAttribute12 = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x000303D7 File Offset: 0x0002E5D7
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x000303DF File Offset: 0x0002E5DF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute13
		{
			get
			{
				return this._extensionAttribute13;
			}
			set
			{
				this._extensionAttribute13 = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x000303E8 File Offset: 0x0002E5E8
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x000303F0 File Offset: 0x0002E5F0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute14
		{
			get
			{
				return this._extensionAttribute14;
			}
			set
			{
				this._extensionAttribute14 = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x000303F9 File Offset: 0x0002E5F9
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x00030401 File Offset: 0x0002E601
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string extensionAttribute15
		{
			get
			{
				return this._extensionAttribute15;
			}
			set
			{
				this._extensionAttribute15 = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0003040A File Offset: 0x0002E60A
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x00030412 File Offset: 0x0002E612
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string facsimileTelephoneNumber
		{
			get
			{
				return this._facsimileTelephoneNumber;
			}
			set
			{
				this._facsimileTelephoneNumber = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x0003041B File Offset: 0x0002E61B
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x00030423 File Offset: 0x0002E623
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string givenName
		{
			get
			{
				return this._givenName;
			}
			set
			{
				this._givenName = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x0003042C File Offset: 0x0002E62C
		// (set) Token: 0x06001953 RID: 6483 RVA: 0x00030434 File Offset: 0x0002E634
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string immutableId
		{
			get
			{
				return this._immutableId;
			}
			set
			{
				this._immutableId = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x0003043D File Offset: 0x0002E63D
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x00030445 File Offset: 0x0002E645
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? invitedOn
		{
			get
			{
				return this._invitedOn;
			}
			set
			{
				this._invitedOn = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x0003044E File Offset: 0x0002E64E
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x00030456 File Offset: 0x0002E656
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<KeyValue> inviteReplyUrl
		{
			get
			{
				return this._inviteReplyUrl;
			}
			set
			{
				this._inviteReplyUrl = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x0003045F File Offset: 0x0002E65F
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x00030467 File Offset: 0x0002E667
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> inviteResources
		{
			get
			{
				return this._inviteResources;
			}
			set
			{
				this._inviteResources = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00030470 File Offset: 0x0002E670
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x00030478 File Offset: 0x0002E678
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<InvitationTicket> inviteTicket
		{
			get
			{
				return this._inviteTicket;
			}
			set
			{
				this._inviteTicket = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00030481 File Offset: 0x0002E681
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x00030489 File Offset: 0x0002E689
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string jobTitle
		{
			get
			{
				return this._jobTitle;
			}
			set
			{
				this._jobTitle = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00030492 File Offset: 0x0002E692
		// (set) Token: 0x0600195F RID: 6495 RVA: 0x0003049A File Offset: 0x0002E69A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? lastDirSyncTime
		{
			get
			{
				return this._lastDirSyncTime;
			}
			set
			{
				this._lastDirSyncTime = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x000304A3 File Offset: 0x0002E6A3
		// (set) Token: 0x06001961 RID: 6497 RVA: 0x000304AB File Offset: 0x0002E6AB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<LogonIdentifier> logonIdentifiers
		{
			get
			{
				return this._logonIdentifiers;
			}
			set
			{
				this._logonIdentifiers = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x000304B4 File Offset: 0x0002E6B4
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x000304BC File Offset: 0x0002E6BC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mail
		{
			get
			{
				return this._mail;
			}
			set
			{
				this._mail = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x000304C5 File Offset: 0x0002E6C5
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x000304CD File Offset: 0x0002E6CD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mailNickname
		{
			get
			{
				return this._mailNickname;
			}
			set
			{
				this._mailNickname = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x000304D6 File Offset: 0x0002E6D6
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x000304DE File Offset: 0x0002E6DE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mobile
		{
			get
			{
				return this._mobile;
			}
			set
			{
				this._mobile = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x000304E7 File Offset: 0x0002E6E7
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x000304EF File Offset: 0x0002E6EF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string netId
		{
			get
			{
				return this._netId;
			}
			set
			{
				this._netId = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000304F8 File Offset: 0x0002E6F8
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x00030514 File Offset: 0x0002E714
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public byte[] onPremiseSecurityIdentifier
		{
			get
			{
				if (this._onPremiseSecurityIdentifier != null)
				{
					return (byte[])this._onPremiseSecurityIdentifier.Clone();
				}
				return null;
			}
			set
			{
				this._onPremiseSecurityIdentifier = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0003051D File Offset: 0x0002E71D
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00030525 File Offset: 0x0002E725
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> otherMails
		{
			get
			{
				return this._otherMails;
			}
			set
			{
				this._otherMails = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x0003052E File Offset: 0x0002E72E
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00030536 File Offset: 0x0002E736
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string passwordPolicies
		{
			get
			{
				return this._passwordPolicies;
			}
			set
			{
				this._passwordPolicies = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0003053F File Offset: 0x0002E73F
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x00030569 File Offset: 0x0002E769
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public PasswordProfile passwordProfile
		{
			get
			{
				if (this._passwordProfile == null && !this._passwordProfileInitialized)
				{
					this._passwordProfile = new PasswordProfile();
					this._passwordProfileInitialized = true;
				}
				return this._passwordProfile;
			}
			set
			{
				this._passwordProfile = value;
				this._passwordProfileInitialized = true;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00030579 File Offset: 0x0002E779
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x00030581 File Offset: 0x0002E781
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string physicalDeliveryOfficeName
		{
			get
			{
				return this._physicalDeliveryOfficeName;
			}
			set
			{
				this._physicalDeliveryOfficeName = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0003058A File Offset: 0x0002E78A
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x00030592 File Offset: 0x0002E792
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string postalCode
		{
			get
			{
				return this._postalCode;
			}
			set
			{
				this._postalCode = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x0003059B File Offset: 0x0002E79B
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x000305A3 File Offset: 0x0002E7A3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string preferredLanguage
		{
			get
			{
				return this._preferredLanguage;
			}
			set
			{
				this._preferredLanguage = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x000305AC File Offset: 0x0002E7AC
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x000305B4 File Offset: 0x0002E7B4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string primarySMTPAddress
		{
			get
			{
				return this._primarySMTPAddress;
			}
			set
			{
				this._primarySMTPAddress = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x000305BD File Offset: 0x0002E7BD
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x000305C5 File Offset: 0x0002E7C5
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ProvisionedPlan> provisionedPlans
		{
			get
			{
				return this._provisionedPlans;
			}
			set
			{
				this._provisionedPlans = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x000305CE File Offset: 0x0002E7CE
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x000305D6 File Offset: 0x0002E7D6
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ProvisioningError> provisioningErrors
		{
			get
			{
				return this._provisioningErrors;
			}
			set
			{
				this._provisioningErrors = value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x000305DF File Offset: 0x0002E7DF
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x000305E7 File Offset: 0x0002E7E7
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> proxyAddresses
		{
			get
			{
				return this._proxyAddresses;
			}
			set
			{
				this._proxyAddresses = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x000305F0 File Offset: 0x0002E7F0
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x0003061A File Offset: 0x0002E81A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public SelfServePasswordResetData selfServePasswordResetData
		{
			get
			{
				if (this._selfServePasswordResetData == null && !this._selfServePasswordResetDataInitialized)
				{
					this._selfServePasswordResetData = new SelfServePasswordResetData();
					this._selfServePasswordResetDataInitialized = true;
				}
				return this._selfServePasswordResetData;
			}
			set
			{
				this._selfServePasswordResetData = value;
				this._selfServePasswordResetDataInitialized = true;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0003062A File Offset: 0x0002E82A
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x00030632 File Offset: 0x0002E832
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string signInName
		{
			get
			{
				return this._signInName;
			}
			set
			{
				this._signInName = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0003063B File Offset: 0x0002E83B
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x00030643 File Offset: 0x0002E843
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string sipProxyAddress
		{
			get
			{
				return this._sipProxyAddress;
			}
			set
			{
				this._sipProxyAddress = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0003064C File Offset: 0x0002E84C
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x00030654 File Offset: 0x0002E854
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> smtpAddresses
		{
			get
			{
				return this._smtpAddresses;
			}
			set
			{
				this._smtpAddresses = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0003065D File Offset: 0x0002E85D
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x00030665 File Offset: 0x0002E865
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0003066E File Offset: 0x0002E86E
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x00030676 File Offset: 0x0002E876
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string streetAddress
		{
			get
			{
				return this._streetAddress;
			}
			set
			{
				this._streetAddress = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0003067F File Offset: 0x0002E87F
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x00030687 File Offset: 0x0002E887
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string surname
		{
			get
			{
				return this._surname;
			}
			set
			{
				this._surname = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00030690 File Offset: 0x0002E890
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x00030698 File Offset: 0x0002E898
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string telephoneNumber
		{
			get
			{
				return this._telephoneNumber;
			}
			set
			{
				this._telephoneNumber = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x000306A1 File Offset: 0x0002E8A1
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x000306A9 File Offset: 0x0002E8A9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink thumbnailPhoto
		{
			get
			{
				return this._thumbnailPhoto;
			}
			set
			{
				this._thumbnailPhoto = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x000306B2 File Offset: 0x0002E8B2
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x000306BA File Offset: 0x0002E8BA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string usageLocation
		{
			get
			{
				return this._usageLocation;
			}
			set
			{
				this._usageLocation = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x000306C3 File Offset: 0x0002E8C3
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x000306CB File Offset: 0x0002E8CB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userPrincipalName
		{
			get
			{
				return this._userPrincipalName;
			}
			set
			{
				this._userPrincipalName = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x000306D4 File Offset: 0x0002E8D4
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x000306DC File Offset: 0x0002E8DC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userState
		{
			get
			{
				return this._userState;
			}
			set
			{
				this._userState = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x000306E5 File Offset: 0x0002E8E5
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x000306ED File Offset: 0x0002E8ED
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? userStateChangedOn
		{
			get
			{
				return this._userStateChangedOn;
			}
			set
			{
				this._userStateChangedOn = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x000306F6 File Offset: 0x0002E8F6
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x000306FE File Offset: 0x0002E8FE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userType
		{
			get
			{
				return this._userType;
			}
			set
			{
				this._userType = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00030707 File Offset: 0x0002E907
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x0003070F File Offset: 0x0002E90F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ImpersonationAccessGrant> impersonationAccessGrants
		{
			get
			{
				return this._impersonationAccessGrants;
			}
			set
			{
				if (value != null)
				{
					this._impersonationAccessGrants = value;
				}
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0003071B File Offset: 0x0002E91B
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x00030723 File Offset: 0x0002E923
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ServiceEndpoint> serviceEndpoints
		{
			get
			{
				return this._serviceEndpoints;
			}
			set
			{
				if (value != null)
				{
					this._serviceEndpoints = value;
				}
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0003072F File Offset: 0x0002E92F
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x00030737 File Offset: 0x0002E937
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ServiceInfo> serviceInfo
		{
			get
			{
				return this._serviceInfo;
			}
			set
			{
				if (value != null)
				{
					this._serviceInfo = value;
				}
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00030743 File Offset: 0x0002E943
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0003074B File Offset: 0x0002E94B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> registeredDevices
		{
			get
			{
				return this._registeredDevices;
			}
			set
			{
				if (value != null)
				{
					this._registeredDevices = value;
				}
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x00030757 File Offset: 0x0002E957
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x0003075F File Offset: 0x0002E95F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> ownedDevices
		{
			get
			{
				return this._ownedDevices;
			}
			set
			{
				if (value != null)
				{
					this._ownedDevices = value;
				}
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0003076B File Offset: 0x0002E96B
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x00030773 File Offset: 0x0002E973
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectAccessGrant> directAccessGrants
		{
			get
			{
				return this._directAccessGrants;
			}
			set
			{
				if (value != null)
				{
					this._directAccessGrants = value;
				}
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0003077F File Offset: 0x0002E97F
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x00030787 File Offset: 0x0002E987
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> pendingMemberOf
		{
			get
			{
				return this._pendingMemberOf;
			}
			set
			{
				if (value != null)
				{
					this._pendingMemberOf = value;
				}
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00030793 File Offset: 0x0002E993
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x0003079B File Offset: 0x0002E99B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> invitedBy
		{
			get
			{
				return this._invitedBy;
			}
			set
			{
				if (value != null)
				{
					this._invitedBy = value;
				}
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060019AC RID: 6572 RVA: 0x000307A7 File Offset: 0x0002E9A7
		// (set) Token: 0x060019AD RID: 6573 RVA: 0x000307AF File Offset: 0x0002E9AF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> invitedUsers
		{
			get
			{
				return this._invitedUsers;
			}
			set
			{
				if (value != null)
				{
					this._invitedUsers = value;
				}
			}
		}

		// Token: 0x04001B5F RID: 7007
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _acceptedAs;

		// Token: 0x04001B60 RID: 7008
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _acceptedOn;

		// Token: 0x04001B61 RID: 7009
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001B62 RID: 7010
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AlternativeSecurityId> _alternativeSecurityIds = new Collection<AlternativeSecurityId>();

		// Token: 0x04001B63 RID: 7011
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x04001B64 RID: 7012
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x04001B65 RID: 7013
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedLicense> _assignedLicenses = new Collection<AssignedLicense>();

		// Token: 0x04001B66 RID: 7014
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();

		// Token: 0x04001B67 RID: 7015
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x04001B68 RID: 7016
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001B69 RID: 7017
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _creationType;

		// Token: 0x04001B6A RID: 7018
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _department;

		// Token: 0x04001B6B RID: 7019
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001B6C RID: 7020
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001B6D RID: 7021
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute1;

		// Token: 0x04001B6E RID: 7022
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute2;

		// Token: 0x04001B6F RID: 7023
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute3;

		// Token: 0x04001B70 RID: 7024
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute4;

		// Token: 0x04001B71 RID: 7025
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute5;

		// Token: 0x04001B72 RID: 7026
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute6;

		// Token: 0x04001B73 RID: 7027
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute7;

		// Token: 0x04001B74 RID: 7028
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute8;

		// Token: 0x04001B75 RID: 7029
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute9;

		// Token: 0x04001B76 RID: 7030
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute10;

		// Token: 0x04001B77 RID: 7031
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute11;

		// Token: 0x04001B78 RID: 7032
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute12;

		// Token: 0x04001B79 RID: 7033
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute13;

		// Token: 0x04001B7A RID: 7034
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute14;

		// Token: 0x04001B7B RID: 7035
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute15;

		// Token: 0x04001B7C RID: 7036
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _facsimileTelephoneNumber;

		// Token: 0x04001B7D RID: 7037
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _givenName;

		// Token: 0x04001B7E RID: 7038
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _immutableId;

		// Token: 0x04001B7F RID: 7039
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _invitedOn;

		// Token: 0x04001B80 RID: 7040
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<KeyValue> _inviteReplyUrl = new Collection<KeyValue>();

		// Token: 0x04001B81 RID: 7041
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _inviteResources = new Collection<string>();

		// Token: 0x04001B82 RID: 7042
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<InvitationTicket> _inviteTicket = new Collection<InvitationTicket>();

		// Token: 0x04001B83 RID: 7043
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _jobTitle;

		// Token: 0x04001B84 RID: 7044
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001B85 RID: 7045
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<LogonIdentifier> _logonIdentifiers = new Collection<LogonIdentifier>();

		// Token: 0x04001B86 RID: 7046
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001B87 RID: 7047
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001B88 RID: 7048
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mobile;

		// Token: 0x04001B89 RID: 7049
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _netId;

		// Token: 0x04001B8A RID: 7050
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _onPremiseSecurityIdentifier;

		// Token: 0x04001B8B RID: 7051
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _otherMails = new Collection<string>();

		// Token: 0x04001B8C RID: 7052
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _passwordPolicies;

		// Token: 0x04001B8D RID: 7053
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private PasswordProfile _passwordProfile;

		// Token: 0x04001B8E RID: 7054
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _passwordProfileInitialized;

		// Token: 0x04001B8F RID: 7055
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _physicalDeliveryOfficeName;

		// Token: 0x04001B90 RID: 7056
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001B91 RID: 7057
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredLanguage;

		// Token: 0x04001B92 RID: 7058
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _primarySMTPAddress;

		// Token: 0x04001B93 RID: 7059
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();

		// Token: 0x04001B94 RID: 7060
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001B95 RID: 7061
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x04001B96 RID: 7062
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private SelfServePasswordResetData _selfServePasswordResetData;

		// Token: 0x04001B97 RID: 7063
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _selfServePasswordResetDataInitialized;

		// Token: 0x04001B98 RID: 7064
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _signInName;

		// Token: 0x04001B99 RID: 7065
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sipProxyAddress;

		// Token: 0x04001B9A RID: 7066
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _smtpAddresses = new Collection<string>();

		// Token: 0x04001B9B RID: 7067
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x04001B9C RID: 7068
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _streetAddress;

		// Token: 0x04001B9D RID: 7069
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _surname;

		// Token: 0x04001B9E RID: 7070
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001B9F RID: 7071
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;

		// Token: 0x04001BA0 RID: 7072
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _usageLocation;

		// Token: 0x04001BA1 RID: 7073
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userPrincipalName;

		// Token: 0x04001BA2 RID: 7074
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userState;

		// Token: 0x04001BA3 RID: 7075
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _userStateChangedOn;

		// Token: 0x04001BA4 RID: 7076
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userType;

		// Token: 0x04001BA5 RID: 7077
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ImpersonationAccessGrant> _impersonationAccessGrants = new Collection<ImpersonationAccessGrant>();

		// Token: 0x04001BA6 RID: 7078
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceEndpoint> _serviceEndpoints = new Collection<ServiceEndpoint>();

		// Token: 0x04001BA7 RID: 7079
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceInfo> _serviceInfo = new Collection<ServiceInfo>();

		// Token: 0x04001BA8 RID: 7080
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredDevices = new Collection<DirectoryObject>();

		// Token: 0x04001BA9 RID: 7081
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _ownedDevices = new Collection<DirectoryObject>();

		// Token: 0x04001BAA RID: 7082
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrants = new Collection<DirectAccessGrant>();

		// Token: 0x04001BAB RID: 7083
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _pendingMemberOf = new Collection<DirectoryObject>();

		// Token: 0x04001BAC RID: 7084
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _invitedBy = new Collection<DirectoryObject>();

		// Token: 0x04001BAD RID: 7085
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _invitedUsers = new Collection<DirectoryObject>();
	}
}
