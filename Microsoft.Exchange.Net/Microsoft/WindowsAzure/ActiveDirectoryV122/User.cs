using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005BF RID: 1471
	[DataServiceKey("objectId")]
	public class User : DirectoryObject
	{
		// Token: 0x06001678 RID: 5752 RVA: 0x0002E024 File Offset: 0x0002C224
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static User CreateUser(string objectId, Collection<AssignedLicense> assignedLicenses, Collection<AssignedPlan> assignedPlans, Collection<string> otherMails, Collection<ProvisionedPlan> provisionedPlans, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses, DataServiceStreamLink thumbnailPhoto)
		{
			User user = new User();
			user.objectId = objectId;
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
			user.thumbnailPhoto = thumbnailPhoto;
			return user;
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0002E0CB File Offset: 0x0002C2CB
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0002E0D3 File Offset: 0x0002C2D3
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

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0002E0DC File Offset: 0x0002C2DC
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
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

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0002E0ED File Offset: 0x0002C2ED
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x0002E0F5 File Offset: 0x0002C2F5
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

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0002E0FE File Offset: 0x0002C2FE
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x0002E106 File Offset: 0x0002C306
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

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0002E10F File Offset: 0x0002C30F
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x0002E117 File Offset: 0x0002C317
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

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0002E120 File Offset: 0x0002C320
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x0002E128 File Offset: 0x0002C328
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

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0002E131 File Offset: 0x0002C331
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x0002E139 File Offset: 0x0002C339
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

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0002E142 File Offset: 0x0002C342
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x0002E14A File Offset: 0x0002C34A
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

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0002E153 File Offset: 0x0002C353
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x0002E15B File Offset: 0x0002C35B
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

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0002E164 File Offset: 0x0002C364
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x0002E16C File Offset: 0x0002C36C
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

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x0002E175 File Offset: 0x0002C375
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x0002E17D File Offset: 0x0002C37D
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

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0002E186 File Offset: 0x0002C386
		// (set) Token: 0x06001690 RID: 5776 RVA: 0x0002E18E File Offset: 0x0002C38E
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

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0002E197 File Offset: 0x0002C397
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x0002E19F File Offset: 0x0002C39F
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

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0002E1A8 File Offset: 0x0002C3A8
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
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

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0002E1B9 File Offset: 0x0002C3B9
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0002E1C1 File Offset: 0x0002C3C1
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

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0002E1CA File Offset: 0x0002C3CA
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0002E1D2 File Offset: 0x0002C3D2
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

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0002E1DB File Offset: 0x0002C3DB
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x0002E1E3 File Offset: 0x0002C3E3
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

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0002E1EC File Offset: 0x0002C3EC
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x0002E1F4 File Offset: 0x0002C3F4
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

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0002E1FD File Offset: 0x0002C3FD
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x0002E227 File Offset: 0x0002C427
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

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0002E237 File Offset: 0x0002C437
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x0002E23F File Offset: 0x0002C43F
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

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0002E248 File Offset: 0x0002C448
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0002E250 File Offset: 0x0002C450
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

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x0002E259 File Offset: 0x0002C459
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x0002E261 File Offset: 0x0002C461
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

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x0002E26A File Offset: 0x0002C46A
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x0002E272 File Offset: 0x0002C472
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

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x0002E27B File Offset: 0x0002C47B
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x0002E283 File Offset: 0x0002C483
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

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x0002E28C File Offset: 0x0002C48C
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x0002E294 File Offset: 0x0002C494
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

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060016AB RID: 5803 RVA: 0x0002E29D File Offset: 0x0002C49D
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x0002E2A5 File Offset: 0x0002C4A5
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

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0002E2AE File Offset: 0x0002C4AE
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x0002E2B6 File Offset: 0x0002C4B6
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

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0002E2BF File Offset: 0x0002C4BF
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x0002E2C7 File Offset: 0x0002C4C7
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x0002E2D8 File Offset: 0x0002C4D8
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

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0002E2E1 File Offset: 0x0002C4E1
		// (set) Token: 0x060016B4 RID: 5812 RVA: 0x0002E2E9 File Offset: 0x0002C4E9
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0002E2F2 File Offset: 0x0002C4F2
		// (set) Token: 0x060016B6 RID: 5814 RVA: 0x0002E2FA File Offset: 0x0002C4FA
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

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0002E303 File Offset: 0x0002C503
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x0002E30B File Offset: 0x0002C50B
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

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0002E314 File Offset: 0x0002C514
		// (set) Token: 0x060016BA RID: 5818 RVA: 0x0002E31C File Offset: 0x0002C51C
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

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060016BB RID: 5819 RVA: 0x0002E325 File Offset: 0x0002C525
		// (set) Token: 0x060016BC RID: 5820 RVA: 0x0002E32D File Offset: 0x0002C52D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<Permission> permissions
		{
			get
			{
				return this._permissions;
			}
			set
			{
				if (value != null)
				{
					this._permissions = value;
				}
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x0002E339 File Offset: 0x0002C539
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x0002E341 File Offset: 0x0002C541
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

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x0002E34D File Offset: 0x0002C54D
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0002E355 File Offset: 0x0002C555
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

		// Token: 0x04001A2E RID: 6702
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001A2F RID: 6703
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedLicense> _assignedLicenses = new Collection<AssignedLicense>();

		// Token: 0x04001A30 RID: 6704
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();

		// Token: 0x04001A31 RID: 6705
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x04001A32 RID: 6706
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001A33 RID: 6707
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _department;

		// Token: 0x04001A34 RID: 6708
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001A35 RID: 6709
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001A36 RID: 6710
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _facsimileTelephoneNumber;

		// Token: 0x04001A37 RID: 6711
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _givenName;

		// Token: 0x04001A38 RID: 6712
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _immutableId;

		// Token: 0x04001A39 RID: 6713
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _jobTitle;

		// Token: 0x04001A3A RID: 6714
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001A3B RID: 6715
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001A3C RID: 6716
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001A3D RID: 6717
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mobile;

		// Token: 0x04001A3E RID: 6718
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _otherMails = new Collection<string>();

		// Token: 0x04001A3F RID: 6719
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _passwordPolicies;

		// Token: 0x04001A40 RID: 6720
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private PasswordProfile _passwordProfile;

		// Token: 0x04001A41 RID: 6721
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _passwordProfileInitialized;

		// Token: 0x04001A42 RID: 6722
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _physicalDeliveryOfficeName;

		// Token: 0x04001A43 RID: 6723
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001A44 RID: 6724
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredLanguage;

		// Token: 0x04001A45 RID: 6725
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();

		// Token: 0x04001A46 RID: 6726
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001A47 RID: 6727
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x04001A48 RID: 6728
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x04001A49 RID: 6729
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _streetAddress;

		// Token: 0x04001A4A RID: 6730
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _surname;

		// Token: 0x04001A4B RID: 6731
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001A4C RID: 6732
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;

		// Token: 0x04001A4D RID: 6733
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _usageLocation;

		// Token: 0x04001A4E RID: 6734
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userPrincipalName;

		// Token: 0x04001A4F RID: 6735
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userType;

		// Token: 0x04001A50 RID: 6736
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<Permission> _permissions = new Collection<Permission>();

		// Token: 0x04001A51 RID: 6737
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredDevices = new Collection<DirectoryObject>();

		// Token: 0x04001A52 RID: 6738
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _ownedDevices = new Collection<DirectoryObject>();
	}
}
