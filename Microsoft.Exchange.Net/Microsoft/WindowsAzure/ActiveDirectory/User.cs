using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A1 RID: 1441
	[DataServiceKey("objectId")]
	public class User : DirectoryObject
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x0002C298 File Offset: 0x0002A498
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static User CreateUser(string objectId, Collection<AlternativeSecurityId> alternativeSecurityIds, Collection<AssignedLicense> assignedLicenses, Collection<AssignedPlan> assignedPlans, Collection<string> otherMails, Collection<ProvisionedPlan> provisionedPlans, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses, Collection<string> smtpAddresses, DataServiceStreamLink thumbnailPhoto)
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0002C36D File Offset: 0x0002A56D
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x0002C375 File Offset: 0x0002A575
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0002C37E File Offset: 0x0002A57E
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x0002C386 File Offset: 0x0002A586
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

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0002C38F File Offset: 0x0002A58F
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x0002C3B9 File Offset: 0x0002A5B9
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

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0002C3C9 File Offset: 0x0002A5C9
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x0002C3D1 File Offset: 0x0002A5D1
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0002C3DA File Offset: 0x0002A5DA
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0002C3E2 File Offset: 0x0002A5E2
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

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0002C3EB File Offset: 0x0002A5EB
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0002C3F3 File Offset: 0x0002A5F3
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0002C3FC File Offset: 0x0002A5FC
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x0002C404 File Offset: 0x0002A604
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

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0002C40D File Offset: 0x0002A60D
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x0002C415 File Offset: 0x0002A615
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

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0002C41E File Offset: 0x0002A61E
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x0002C426 File Offset: 0x0002A626
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0002C42F File Offset: 0x0002A62F
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x0002C437 File Offset: 0x0002A637
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0002C440 File Offset: 0x0002A640
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0002C448 File Offset: 0x0002A648
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

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0002C451 File Offset: 0x0002A651
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0002C459 File Offset: 0x0002A659
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

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0002C462 File Offset: 0x0002A662
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x0002C46A File Offset: 0x0002A66A
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0002C473 File Offset: 0x0002A673
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0002C47B File Offset: 0x0002A67B
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

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0002C484 File Offset: 0x0002A684
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0002C48C File Offset: 0x0002A68C
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

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0002C495 File Offset: 0x0002A695
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0002C49D File Offset: 0x0002A69D
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

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0002C4A6 File Offset: 0x0002A6A6
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0002C4AE File Offset: 0x0002A6AE
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0002C4B7 File Offset: 0x0002A6B7
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x0002C4BF File Offset: 0x0002A6BF
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

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0002C4C8 File Offset: 0x0002A6C8
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
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

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0002C4D9 File Offset: 0x0002A6D9
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0002C4E1 File Offset: 0x0002A6E1
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

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0002C4EA File Offset: 0x0002A6EA
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0002C4F2 File Offset: 0x0002A6F2
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

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0002C4FB File Offset: 0x0002A6FB
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0002C503 File Offset: 0x0002A703
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

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0002C50C File Offset: 0x0002A70C
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x0002C514 File Offset: 0x0002A714
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0002C51D File Offset: 0x0002A71D
		// (set) Token: 0x0600144D RID: 5197 RVA: 0x0002C525 File Offset: 0x0002A725
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

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0002C52E File Offset: 0x0002A72E
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x0002C536 File Offset: 0x0002A736
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0002C53F File Offset: 0x0002A73F
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x0002C547 File Offset: 0x0002A747
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

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0002C550 File Offset: 0x0002A750
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x0002C57A File Offset: 0x0002A77A
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0002C58A File Offset: 0x0002A78A
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x0002C592 File Offset: 0x0002A792
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

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0002C59B File Offset: 0x0002A79B
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x0002C5A3 File Offset: 0x0002A7A3
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0002C5AC File Offset: 0x0002A7AC
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
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

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0002C5BD File Offset: 0x0002A7BD
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x0002C5C5 File Offset: 0x0002A7C5
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

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x0002C5CE File Offset: 0x0002A7CE
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x0002C5D6 File Offset: 0x0002A7D6
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

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0002C5DF File Offset: 0x0002A7DF
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x0002C5E7 File Offset: 0x0002A7E7
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

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0002C5F0 File Offset: 0x0002A7F0
		// (set) Token: 0x06001461 RID: 5217 RVA: 0x0002C5F8 File Offset: 0x0002A7F8
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0002C601 File Offset: 0x0002A801
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x0002C609 File Offset: 0x0002A809
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

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0002C612 File Offset: 0x0002A812
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x0002C61A File Offset: 0x0002A81A
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0002C623 File Offset: 0x0002A823
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x0002C62B File Offset: 0x0002A82B
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

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0002C634 File Offset: 0x0002A834
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x0002C63C File Offset: 0x0002A83C
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

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0002C645 File Offset: 0x0002A845
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0002C64D File Offset: 0x0002A84D
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

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0002C656 File Offset: 0x0002A856
		// (set) Token: 0x0600146D RID: 5229 RVA: 0x0002C65E File Offset: 0x0002A85E
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

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0002C667 File Offset: 0x0002A867
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x0002C66F File Offset: 0x0002A86F
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

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0002C678 File Offset: 0x0002A878
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x0002C680 File Offset: 0x0002A880
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0002C689 File Offset: 0x0002A889
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x0002C691 File Offset: 0x0002A891
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

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0002C69A File Offset: 0x0002A89A
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x0002C6A2 File Offset: 0x0002A8A2
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

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0002C6AB File Offset: 0x0002A8AB
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x0002C6B3 File Offset: 0x0002A8B3
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

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0002C6BF File Offset: 0x0002A8BF
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0002C6C7 File Offset: 0x0002A8C7
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

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0002C6D3 File Offset: 0x0002A8D3
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x0002C6DB File Offset: 0x0002A8DB
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

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0002C6E7 File Offset: 0x0002A8E7
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0002C6EF File Offset: 0x0002A8EF
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

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0002C6FB File Offset: 0x0002A8FB
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x0002C703 File Offset: 0x0002A903
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

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0002C70F File Offset: 0x0002A90F
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x0002C717 File Offset: 0x0002A917
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

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0002C723 File Offset: 0x0002A923
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x0002C72B File Offset: 0x0002A92B
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

		// Token: 0x04001916 RID: 6422
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001917 RID: 6423
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AlternativeSecurityId> _alternativeSecurityIds = new Collection<AlternativeSecurityId>();

		// Token: 0x04001918 RID: 6424
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x04001919 RID: 6425
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x0400191A RID: 6426
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedLicense> _assignedLicenses = new Collection<AssignedLicense>();

		// Token: 0x0400191B RID: 6427
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AssignedPlan> _assignedPlans = new Collection<AssignedPlan>();

		// Token: 0x0400191C RID: 6428
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x0400191D RID: 6429
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x0400191E RID: 6430
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _department;

		// Token: 0x0400191F RID: 6431
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001920 RID: 6432
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001921 RID: 6433
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute1;

		// Token: 0x04001922 RID: 6434
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute2;

		// Token: 0x04001923 RID: 6435
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute3;

		// Token: 0x04001924 RID: 6436
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute4;

		// Token: 0x04001925 RID: 6437
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _extensionAttribute5;

		// Token: 0x04001926 RID: 6438
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _facsimileTelephoneNumber;

		// Token: 0x04001927 RID: 6439
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _givenName;

		// Token: 0x04001928 RID: 6440
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _immutableId;

		// Token: 0x04001929 RID: 6441
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _jobTitle;

		// Token: 0x0400192A RID: 6442
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x0400192B RID: 6443
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x0400192C RID: 6444
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x0400192D RID: 6445
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mobile;

		// Token: 0x0400192E RID: 6446
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _netId;

		// Token: 0x0400192F RID: 6447
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _otherMails = new Collection<string>();

		// Token: 0x04001930 RID: 6448
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _passwordPolicies;

		// Token: 0x04001931 RID: 6449
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private PasswordProfile _passwordProfile;

		// Token: 0x04001932 RID: 6450
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _passwordProfileInitialized;

		// Token: 0x04001933 RID: 6451
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _physicalDeliveryOfficeName;

		// Token: 0x04001934 RID: 6452
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001935 RID: 6453
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preferredLanguage;

		// Token: 0x04001936 RID: 6454
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _primarySMTPAddress;

		// Token: 0x04001937 RID: 6455
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisionedPlan> _provisionedPlans = new Collection<ProvisionedPlan>();

		// Token: 0x04001938 RID: 6456
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001939 RID: 6457
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x0400193A RID: 6458
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sipProxyAddress;

		// Token: 0x0400193B RID: 6459
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _smtpAddresses = new Collection<string>();

		// Token: 0x0400193C RID: 6460
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x0400193D RID: 6461
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _streetAddress;

		// Token: 0x0400193E RID: 6462
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _surname;

		// Token: 0x0400193F RID: 6463
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001940 RID: 6464
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;

		// Token: 0x04001941 RID: 6465
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _usageLocation;

		// Token: 0x04001942 RID: 6466
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userPrincipalName;

		// Token: 0x04001943 RID: 6467
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userType;

		// Token: 0x04001944 RID: 6468
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ImpersonationAccessGrant> _impersonationAccessGrants = new Collection<ImpersonationAccessGrant>();

		// Token: 0x04001945 RID: 6469
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceEndpoint> _serviceEndpoints = new Collection<ServiceEndpoint>();

		// Token: 0x04001946 RID: 6470
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ServiceInfo> _serviceInfo = new Collection<ServiceInfo>();

		// Token: 0x04001947 RID: 6471
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredDevices = new Collection<DirectoryObject>();

		// Token: 0x04001948 RID: 6472
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _ownedDevices = new Collection<DirectoryObject>();

		// Token: 0x04001949 RID: 6473
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrants = new Collection<DirectAccessGrant>();

		// Token: 0x0400194A RID: 6474
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _pendingMemberOf = new Collection<DirectoryObject>();
	}
}
