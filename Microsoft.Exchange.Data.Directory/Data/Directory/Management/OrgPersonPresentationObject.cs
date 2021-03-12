using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006EE RID: 1774
	[Serializable]
	public abstract class OrgPersonPresentationObject : ADPresentationObject
	{
		// Token: 0x060052FF RID: 21247 RVA: 0x001302CE File Offset: 0x0012E4CE
		protected OrgPersonPresentationObject()
		{
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x001302D6 File Offset: 0x0012E4D6
		protected OrgPersonPresentationObject(ADObject dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x001302DF File Offset: 0x0012E4DF
		// (set) Token: 0x06005302 RID: 21250 RVA: 0x001302F1 File Offset: 0x0012E4F1
		[Parameter(Mandatory = false)]
		public string AssistantName
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.AssistantName];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.AssistantName] = value;
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x001302FF File Offset: 0x0012E4FF
		// (set) Token: 0x06005304 RID: 21252 RVA: 0x00130311 File Offset: 0x0012E511
		[Parameter(Mandatory = false)]
		public string City
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.City];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.City] = value;
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x0013031F File Offset: 0x0012E51F
		// (set) Token: 0x06005306 RID: 21254 RVA: 0x00130331 File Offset: 0x0012E531
		[Parameter(Mandatory = false)]
		public string Company
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Company];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Company] = value;
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x0013033F File Offset: 0x0012E53F
		// (set) Token: 0x06005308 RID: 21256 RVA: 0x00130351 File Offset: 0x0012E551
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[OrgPersonPresentationObjectSchema.CountryOrRegion];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06005309 RID: 21257 RVA: 0x0013035F File Offset: 0x0012E55F
		// (set) Token: 0x0600530A RID: 21258 RVA: 0x00130371 File Offset: 0x0012E571
		[Parameter(Mandatory = false)]
		public string Department
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Department];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Department] = value;
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x0013037F File Offset: 0x0012E57F
		public MultiValuedProperty<ADObjectId> DirectReports
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OrgPersonPresentationObjectSchema.DirectReports];
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x0600530C RID: 21260 RVA: 0x00130391 File Offset: 0x0012E591
		// (set) Token: 0x0600530D RID: 21261 RVA: 0x001303A3 File Offset: 0x0012E5A3
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.DisplayName];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x001303B1 File Offset: 0x0012E5B1
		// (set) Token: 0x0600530F RID: 21263 RVA: 0x001303C3 File Offset: 0x0012E5C3
		[Parameter(Mandatory = false)]
		public string Fax
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Fax];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Fax] = value;
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06005310 RID: 21264 RVA: 0x001303D1 File Offset: 0x0012E5D1
		// (set) Token: 0x06005311 RID: 21265 RVA: 0x001303E3 File Offset: 0x0012E5E3
		[Parameter(Mandatory = false)]
		public string FirstName
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.FirstName];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.FirstName] = value;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06005312 RID: 21266 RVA: 0x001303F1 File Offset: 0x0012E5F1
		// (set) Token: 0x06005313 RID: 21267 RVA: 0x00130403 File Offset: 0x0012E603
		[Parameter(Mandatory = false)]
		public GeoCoordinates GeoCoordinates
		{
			get
			{
				return (GeoCoordinates)this[OrgPersonPresentationObjectSchema.GeoCoordinates];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.GeoCoordinates] = value;
			}
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06005314 RID: 21268 RVA: 0x00130411 File Offset: 0x0012E611
		// (set) Token: 0x06005315 RID: 21269 RVA: 0x00130423 File Offset: 0x0012E623
		[Parameter(Mandatory = false)]
		public string HomePhone
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.HomePhone];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.HomePhone] = value;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06005316 RID: 21270 RVA: 0x00130431 File Offset: 0x0012E631
		// (set) Token: 0x06005317 RID: 21271 RVA: 0x00130443 File Offset: 0x0012E643
		[Parameter(Mandatory = false)]
		public string Initials
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Initials];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Initials] = value;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06005318 RID: 21272 RVA: 0x00130451 File Offset: 0x0012E651
		// (set) Token: 0x06005319 RID: 21273 RVA: 0x00130463 File Offset: 0x0012E663
		[Parameter(Mandatory = false)]
		public string LastName
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.LastName];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.LastName] = value;
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x00130471 File Offset: 0x0012E671
		// (set) Token: 0x0600531B RID: 21275 RVA: 0x00130483 File Offset: 0x0012E683
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[OrgPersonPresentationObjectSchema.Manager];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Manager] = value;
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x0600531C RID: 21276 RVA: 0x00130491 File Offset: 0x0012E691
		// (set) Token: 0x0600531D RID: 21277 RVA: 0x001304A3 File Offset: 0x0012E6A3
		[Parameter(Mandatory = false)]
		public string MobilePhone
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.MobilePhone];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.MobilePhone] = value;
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x001304B1 File Offset: 0x0012E6B1
		// (set) Token: 0x0600531F RID: 21279 RVA: 0x001304C3 File Offset: 0x0012E6C3
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Notes];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Notes] = value;
			}
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x001304D1 File Offset: 0x0012E6D1
		// (set) Token: 0x06005321 RID: 21281 RVA: 0x001304E3 File Offset: 0x0012E6E3
		[Parameter(Mandatory = false)]
		public string Office
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Office];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Office] = value;
			}
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x001304F1 File Offset: 0x0012E6F1
		// (set) Token: 0x06005323 RID: 21283 RVA: 0x00130503 File Offset: 0x0012E703
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.OtherFax];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.OtherFax] = value;
			}
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x00130511 File Offset: 0x0012E711
		// (set) Token: 0x06005325 RID: 21285 RVA: 0x00130523 File Offset: 0x0012E723
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.OtherHomePhone];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06005326 RID: 21286 RVA: 0x00130531 File Offset: 0x0012E731
		// (set) Token: 0x06005327 RID: 21287 RVA: 0x00130543 File Offset: 0x0012E743
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.OtherTelephone];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06005328 RID: 21288 RVA: 0x00130551 File Offset: 0x0012E751
		// (set) Token: 0x06005329 RID: 21289 RVA: 0x00130563 File Offset: 0x0012E763
		[Parameter(Mandatory = false)]
		public string Pager
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Pager];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Pager] = value;
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x0600532A RID: 21290 RVA: 0x00130571 File Offset: 0x0012E771
		// (set) Token: 0x0600532B RID: 21291 RVA: 0x00130583 File Offset: 0x0012E783
		[Parameter(Mandatory = false)]
		public string Phone
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Phone];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Phone] = value;
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00130591 File Offset: 0x0012E791
		// (set) Token: 0x0600532D RID: 21293 RVA: 0x001305A3 File Offset: 0x0012E7A3
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.PhoneticDisplayName];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x001305B1 File Offset: 0x0012E7B1
		// (set) Token: 0x0600532F RID: 21295 RVA: 0x001305C3 File Offset: 0x0012E7C3
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.PostalCode];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.PostalCode] = value;
			}
		}

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x001305D1 File Offset: 0x0012E7D1
		// (set) Token: 0x06005331 RID: 21297 RVA: 0x001305E3 File Offset: 0x0012E7E3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> PostOfficeBox
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.PostOfficeBox];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.PostOfficeBox] = value;
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x001305F1 File Offset: 0x0012E7F1
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[OrgPersonPresentationObjectSchema.RecipientType];
			}
		}

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06005333 RID: 21299 RVA: 0x00130603 File Offset: 0x0012E803
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[OrgPersonPresentationObjectSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06005334 RID: 21300 RVA: 0x00130615 File Offset: 0x0012E815
		// (set) Token: 0x06005335 RID: 21301 RVA: 0x00130627 File Offset: 0x0012E827
		[Parameter(Mandatory = false)]
		public string SimpleDisplayName
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.SimpleDisplayName];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.SimpleDisplayName] = value;
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x00130635 File Offset: 0x0012E835
		// (set) Token: 0x06005337 RID: 21303 RVA: 0x00130647 File Offset: 0x0012E847
		[Parameter(Mandatory = false)]
		public string StateOrProvince
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.StateOrProvince];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x00130655 File Offset: 0x0012E855
		// (set) Token: 0x06005339 RID: 21305 RVA: 0x00130667 File Offset: 0x0012E867
		[Parameter(Mandatory = false)]
		public string StreetAddress
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.StreetAddress];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.StreetAddress] = value;
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x00130675 File Offset: 0x0012E875
		// (set) Token: 0x0600533B RID: 21307 RVA: 0x00130687 File Offset: 0x0012E887
		[Parameter(Mandatory = false)]
		public string Title
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.Title];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.Title] = value;
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x0600533C RID: 21308 RVA: 0x00130695 File Offset: 0x0012E895
		// (set) Token: 0x0600533D RID: 21309 RVA: 0x001306A7 File Offset: 0x0012E8A7
		public ADObjectId UMDialPlan
		{
			get
			{
				return (ADObjectId)this[OrgPersonPresentationObjectSchema.UMRecipientDialPlanId];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.UMRecipientDialPlanId] = value;
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x001306B5 File Offset: 0x0012E8B5
		// (set) Token: 0x0600533F RID: 21311 RVA: 0x001306C7 File Offset: 0x0012E8C7
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UMDtmfMap
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.UMDtmfMap];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.UMDtmfMap] = value;
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06005340 RID: 21312 RVA: 0x001306D5 File Offset: 0x0012E8D5
		// (set) Token: 0x06005341 RID: 21313 RVA: 0x001306E7 File Offset: 0x0012E8E7
		[Parameter(Mandatory = false)]
		public AllowUMCallsFromNonUsersFlags AllowUMCallsFromNonUsers
		{
			get
			{
				return (AllowUMCallsFromNonUsersFlags)this[OrgPersonPresentationObjectSchema.AllowUMCallsFromNonUsers];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.AllowUMCallsFromNonUsers] = value;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06005342 RID: 21314 RVA: 0x001306FA File Offset: 0x0012E8FA
		// (set) Token: 0x06005343 RID: 21315 RVA: 0x0013070C File Offset: 0x0012E90C
		[Parameter(Mandatory = false)]
		public string WebPage
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.WebPage];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.WebPage] = value;
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06005344 RID: 21316 RVA: 0x0013071A File Offset: 0x0012E91A
		// (set) Token: 0x06005345 RID: 21317 RVA: 0x0013072C File Offset: 0x0012E92C
		[Parameter(Mandatory = false)]
		public string TelephoneAssistant
		{
			get
			{
				return (string)this[OrgPersonPresentationObjectSchema.TelephoneAssistant];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x0013073A File Offset: 0x0012E93A
		// (set) Token: 0x06005347 RID: 21319 RVA: 0x0013074C File Offset: 0x0012E94C
		[Parameter(Mandatory = false)]
		public SmtpAddress WindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)this[OrgPersonPresentationObjectSchema.WindowsEmailAddress];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.WindowsEmailAddress] = value;
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x0013075F File Offset: 0x0012E95F
		// (set) Token: 0x06005349 RID: 21321 RVA: 0x00130771 File Offset: 0x0012E971
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UMCallingLineIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.UMCallingLineIds];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.UMCallingLineIds] = value;
			}
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x0600534A RID: 21322 RVA: 0x0013077F File Offset: 0x0012E97F
		// (set) Token: 0x0600534B RID: 21323 RVA: 0x00130791 File Offset: 0x0012E991
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return (int?)this[OrgPersonPresentationObjectSchema.SeniorityIndex];
			}
			set
			{
				this[OrgPersonPresentationObjectSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x0600534C RID: 21324 RVA: 0x001307A4 File Offset: 0x0012E9A4
		public MultiValuedProperty<string> VoiceMailSettings
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrgPersonPresentationObjectSchema.VoiceMailSettings];
			}
		}
	}
}
