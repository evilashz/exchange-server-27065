using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003EA RID: 1002
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "CompanyInformation", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class CompanyInformation : IExtensibleDataObject
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0008D26C File Offset: 0x0008B46C
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x0008D274 File Offset: 0x0008B474
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0008D27D File Offset: 0x0008B47D
		// (set) Token: 0x06001899 RID: 6297 RVA: 0x0008D285 File Offset: 0x0008B485
		[DataMember]
		public string[] AuthorizedServiceInstances
		{
			get
			{
				return this.AuthorizedServiceInstancesField;
			}
			set
			{
				this.AuthorizedServiceInstancesField = value;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0008D28E File Offset: 0x0008B48E
		// (set) Token: 0x0600189B RID: 6299 RVA: 0x0008D296 File Offset: 0x0008B496
		[DataMember]
		public AuthorizedService[] AuthorizedServices
		{
			get
			{
				return this.AuthorizedServicesField;
			}
			set
			{
				this.AuthorizedServicesField = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0008D29F File Offset: 0x0008B49F
		// (set) Token: 0x0600189D RID: 6301 RVA: 0x0008D2A7 File Offset: 0x0008B4A7
		[DataMember]
		public string City
		{
			get
			{
				return this.CityField;
			}
			set
			{
				this.CityField = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0008D2B0 File Offset: 0x0008B4B0
		// (set) Token: 0x0600189F RID: 6303 RVA: 0x0008D2B8 File Offset: 0x0008B4B8
		[DataMember]
		public CompanyType CompanyType
		{
			get
			{
				return this.CompanyTypeField;
			}
			set
			{
				this.CompanyTypeField = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0008D2C1 File Offset: 0x0008B4C1
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x0008D2C9 File Offset: 0x0008B4C9
		[DataMember]
		public string Country
		{
			get
			{
				return this.CountryField;
			}
			set
			{
				this.CountryField = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0008D2D2 File Offset: 0x0008B4D2
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x0008D2DA File Offset: 0x0008B4DA
		[DataMember]
		public string CountryLetterCode
		{
			get
			{
				return this.CountryLetterCodeField;
			}
			set
			{
				this.CountryLetterCodeField = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0008D2E3 File Offset: 0x0008B4E3
		// (set) Token: 0x060018A5 RID: 6309 RVA: 0x0008D2EB File Offset: 0x0008B4EB
		[DataMember]
		public bool? DapEnabled
		{
			get
			{
				return this.DapEnabledField;
			}
			set
			{
				this.DapEnabledField = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x0008D2FC File Offset: 0x0008B4FC
		[DataMember]
		public bool? DirectorySynchronizationEnabled
		{
			get
			{
				return this.DirectorySynchronizationEnabledField;
			}
			set
			{
				this.DirectorySynchronizationEnabledField = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0008D305 File Offset: 0x0008B505
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0008D30D File Offset: 0x0008B50D
		[DataMember]
		public DirSyncStatus DirectorySynchronizationStatus
		{
			get
			{
				return this.DirectorySynchronizationStatusField;
			}
			set
			{
				this.DirectorySynchronizationStatusField = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0008D316 File Offset: 0x0008B516
		// (set) Token: 0x060018AB RID: 6315 RVA: 0x0008D31E File Offset: 0x0008B51E
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.DisplayNameField;
			}
			set
			{
				this.DisplayNameField = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0008D327 File Offset: 0x0008B527
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x0008D32F File Offset: 0x0008B52F
		[DataMember]
		public string InitialDomain
		{
			get
			{
				return this.InitialDomainField;
			}
			set
			{
				this.InitialDomainField = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0008D338 File Offset: 0x0008B538
		// (set) Token: 0x060018AF RID: 6319 RVA: 0x0008D340 File Offset: 0x0008B540
		[DataMember]
		public DateTime? LastDirSyncTime
		{
			get
			{
				return this.LastDirSyncTimeField;
			}
			set
			{
				this.LastDirSyncTimeField = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0008D349 File Offset: 0x0008B549
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0008D351 File Offset: 0x0008B551
		[DataMember]
		public string[] MarketingNotificationEmails
		{
			get
			{
				return this.MarketingNotificationEmailsField;
			}
			set
			{
				this.MarketingNotificationEmailsField = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0008D35A File Offset: 0x0008B55A
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0008D362 File Offset: 0x0008B562
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0008D36B File Offset: 0x0008B56B
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x0008D373 File Offset: 0x0008B573
		[DataMember]
		public XmlElement PortalSettings
		{
			get
			{
				return this.PortalSettingsField;
			}
			set
			{
				this.PortalSettingsField = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0008D37C File Offset: 0x0008B57C
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0008D384 File Offset: 0x0008B584
		[DataMember]
		public string PostalCode
		{
			get
			{
				return this.PostalCodeField;
			}
			set
			{
				this.PostalCodeField = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0008D38D File Offset: 0x0008B58D
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0008D395 File Offset: 0x0008B595
		[DataMember]
		public string PreferredLanguage
		{
			get
			{
				return this.PreferredLanguageField;
			}
			set
			{
				this.PreferredLanguageField = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0008D39E File Offset: 0x0008B59E
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0008D3A6 File Offset: 0x0008B5A6
		[DataMember]
		public bool SelfServePasswordResetEnabled
		{
			get
			{
				return this.SelfServePasswordResetEnabledField;
			}
			set
			{
				this.SelfServePasswordResetEnabledField = value;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0008D3AF File Offset: 0x0008B5AF
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x0008D3B7 File Offset: 0x0008B5B7
		[DataMember]
		public ServiceInformation[] ServiceInformation
		{
			get
			{
				return this.ServiceInformationField;
			}
			set
			{
				this.ServiceInformationField = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0008D3C0 File Offset: 0x0008B5C0
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x0008D3C8 File Offset: 0x0008B5C8
		[DataMember]
		public ServiceInstanceInformation[] ServiceInstanceInformation
		{
			get
			{
				return this.ServiceInstanceInformationField;
			}
			set
			{
				this.ServiceInstanceInformationField = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0008D3D1 File Offset: 0x0008B5D1
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x0008D3D9 File Offset: 0x0008B5D9
		[DataMember]
		public string State
		{
			get
			{
				return this.StateField;
			}
			set
			{
				this.StateField = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0008D3E2 File Offset: 0x0008B5E2
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x0008D3EA File Offset: 0x0008B5EA
		[DataMember]
		public string Street
		{
			get
			{
				return this.StreetField;
			}
			set
			{
				this.StreetField = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0008D3F3 File Offset: 0x0008B5F3
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x0008D3FB File Offset: 0x0008B5FB
		[DataMember]
		public string[] TechnicalNotificationEmails
		{
			get
			{
				return this.TechnicalNotificationEmailsField;
			}
			set
			{
				this.TechnicalNotificationEmailsField = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0008D404 File Offset: 0x0008B604
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0008D40C File Offset: 0x0008B60C
		[DataMember]
		public string TelephoneNumber
		{
			get
			{
				return this.TelephoneNumberField;
			}
			set
			{
				this.TelephoneNumberField = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0008D415 File Offset: 0x0008B615
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0008D41D File Offset: 0x0008B61D
		[DataMember]
		public Dictionary<string, string> UIExtensibilityUris
		{
			get
			{
				return this.UIExtensibilityUrisField;
			}
			set
			{
				this.UIExtensibilityUrisField = value;
			}
		}

		// Token: 0x04001146 RID: 4422
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001147 RID: 4423
		private string[] AuthorizedServiceInstancesField;

		// Token: 0x04001148 RID: 4424
		private AuthorizedService[] AuthorizedServicesField;

		// Token: 0x04001149 RID: 4425
		private string CityField;

		// Token: 0x0400114A RID: 4426
		private CompanyType CompanyTypeField;

		// Token: 0x0400114B RID: 4427
		private string CountryField;

		// Token: 0x0400114C RID: 4428
		private string CountryLetterCodeField;

		// Token: 0x0400114D RID: 4429
		private bool? DapEnabledField;

		// Token: 0x0400114E RID: 4430
		private bool? DirectorySynchronizationEnabledField;

		// Token: 0x0400114F RID: 4431
		private DirSyncStatus DirectorySynchronizationStatusField;

		// Token: 0x04001150 RID: 4432
		private string DisplayNameField;

		// Token: 0x04001151 RID: 4433
		private string InitialDomainField;

		// Token: 0x04001152 RID: 4434
		private DateTime? LastDirSyncTimeField;

		// Token: 0x04001153 RID: 4435
		private string[] MarketingNotificationEmailsField;

		// Token: 0x04001154 RID: 4436
		private Guid? ObjectIdField;

		// Token: 0x04001155 RID: 4437
		private XmlElement PortalSettingsField;

		// Token: 0x04001156 RID: 4438
		private string PostalCodeField;

		// Token: 0x04001157 RID: 4439
		private string PreferredLanguageField;

		// Token: 0x04001158 RID: 4440
		private bool SelfServePasswordResetEnabledField;

		// Token: 0x04001159 RID: 4441
		private ServiceInformation[] ServiceInformationField;

		// Token: 0x0400115A RID: 4442
		private ServiceInstanceInformation[] ServiceInstanceInformationField;

		// Token: 0x0400115B RID: 4443
		private string StateField;

		// Token: 0x0400115C RID: 4444
		private string StreetField;

		// Token: 0x0400115D RID: 4445
		private string[] TechnicalNotificationEmailsField;

		// Token: 0x0400115E RID: 4446
		private string TelephoneNumberField;

		// Token: 0x0400115F RID: 4447
		private Dictionary<string, string> UIExtensibilityUrisField;
	}
}
