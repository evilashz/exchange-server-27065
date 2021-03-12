using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000189 RID: 393
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(TypeName = "User", Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class User1 : DirectoryObject
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00020B85 File Offset: 0x0001ED85
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00020B8D File Offset: 0x0001ED8D
		public DirectoryPropertyBooleanSingle AccountEnabled
		{
			get
			{
				return this.accountEnabledField;
			}
			set
			{
				this.accountEnabledField = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00020B96 File Offset: 0x0001ED96
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00020B9E File Offset: 0x0001ED9E
		public DirectoryPropertyXmlAlternativeSecurityId AlternativeSecurityId
		{
			get
			{
				return this.alternativeSecurityIdField;
			}
			set
			{
				this.alternativeSecurityIdField = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00020BA7 File Offset: 0x0001EDA7
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00020BAF File Offset: 0x0001EDAF
		public DirectoryPropertyXmlAssignedPlan AssignedPlan
		{
			get
			{
				return this.assignedPlanField;
			}
			set
			{
				this.assignedPlanField = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		public DirectoryPropertyXmlAssignedLicense AssignedLicense
		{
			get
			{
				return this.assignedLicenseField;
			}
			set
			{
				this.assignedLicenseField = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00020BC9 File Offset: 0x0001EDC9
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00020BD1 File Offset: 0x0001EDD1
		public DirectoryPropertyReferenceAddressListSingle Assistant
		{
			get
			{
				return this.assistantField;
			}
			set
			{
				this.assistantField = value;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00020BDA File Offset: 0x0001EDDA
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00020BE2 File Offset: 0x0001EDE2
		public DirectoryPropertyBooleanSingle BelongsToFirstLoginObjectSet
		{
			get
			{
				return this.belongsToFirstLoginObjectSetField;
			}
			set
			{
				this.belongsToFirstLoginObjectSetField = value;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00020BEB File Offset: 0x0001EDEB
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00020BF3 File Offset: 0x0001EDF3
		public DirectoryPropertyStringSingleLength1To256 BesServiceInstance
		{
			get
			{
				return this.besServiceInstanceField;
			}
			set
			{
				this.besServiceInstanceField = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00020BFC File Offset: 0x0001EDFC
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00020C04 File Offset: 0x0001EE04
		public DirectoryPropertyStringSingleLength1To3 C
		{
			get
			{
				return this.cField;
			}
			set
			{
				this.cField = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00020C0D File Offset: 0x0001EE0D
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x00020C15 File Offset: 0x0001EE15
		public DirectoryPropertyStringSingleLength1To2048 CloudLegacyExchangeDN
		{
			get
			{
				return this.cloudLegacyExchangeDNField;
			}
			set
			{
				this.cloudLegacyExchangeDNField = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00020C1E File Offset: 0x0001EE1E
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00020C26 File Offset: 0x0001EE26
		public DirectoryPropertyInt32Single CloudMSExchArchiveStatus
		{
			get
			{
				return this.cloudMSExchArchiveStatusField;
			}
			set
			{
				this.cloudMSExchArchiveStatusField = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00020C2F File Offset: 0x0001EE2F
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00020C37 File Offset: 0x0001EE37
		public DirectoryPropertyBinarySingleLength1To4000 CloudMSExchBlockedSendersHash
		{
			get
			{
				return this.cloudMSExchBlockedSendersHashField;
			}
			set
			{
				this.cloudMSExchBlockedSendersHashField = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00020C40 File Offset: 0x0001EE40
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00020C48 File Offset: 0x0001EE48
		public DirectoryPropertyGuidSingle CloudMSExchMailboxGuid
		{
			get
			{
				return this.cloudMSExchMailboxGuidField;
			}
			set
			{
				this.cloudMSExchMailboxGuidField = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00020C51 File Offset: 0x0001EE51
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00020C59 File Offset: 0x0001EE59
		public DirectoryPropertyInt32Single CloudMSExchRecipientDisplayType
		{
			get
			{
				return this.cloudMSExchRecipientDisplayTypeField;
			}
			set
			{
				this.cloudMSExchRecipientDisplayTypeField = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00020C62 File Offset: 0x0001EE62
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00020C6A File Offset: 0x0001EE6A
		public DirectoryPropertyBinarySingleLength1To12000 CloudMSExchSafeRecipientsHash
		{
			get
			{
				return this.cloudMSExchSafeRecipientsHashField;
			}
			set
			{
				this.cloudMSExchSafeRecipientsHashField = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00020C73 File Offset: 0x0001EE73
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00020C7B File Offset: 0x0001EE7B
		public DirectoryPropertyBinarySingleLength1To32000 CloudMSExchSafeSendersHash
		{
			get
			{
				return this.cloudMSExchSafeSendersHashField;
			}
			set
			{
				this.cloudMSExchSafeSendersHashField = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00020C84 File Offset: 0x0001EE84
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00020C8C File Offset: 0x0001EE8C
		public DirectoryPropertyStringLength1To1123 CloudMSExchUCVoiceMailSettings
		{
			get
			{
				return this.cloudMSExchUCVoiceMailSettingsField;
			}
			set
			{
				this.cloudMSExchUCVoiceMailSettingsField = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00020C95 File Offset: 0x0001EE95
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00020C9D File Offset: 0x0001EE9D
		public DirectoryPropertyStringSingleLength1To454 CloudSipProxyAddress
		{
			get
			{
				return this.cloudSipProxyAddressField;
			}
			set
			{
				this.cloudSipProxyAddressField = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00020CA6 File Offset: 0x0001EEA6
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00020CAE File Offset: 0x0001EEAE
		public DirectoryPropertyStringSingleLength1To128 Co
		{
			get
			{
				return this.coField;
			}
			set
			{
				this.coField = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00020CB7 File Offset: 0x0001EEB7
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00020CBF File Offset: 0x0001EEBF
		public DirectoryPropertyStringSingleLength1To64 Company
		{
			get
			{
				return this.companyField;
			}
			set
			{
				this.companyField = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00020CC8 File Offset: 0x0001EEC8
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00020CD0 File Offset: 0x0001EED0
		public DirectoryPropertyInt32SingleMin0Max65535 CountryCode
		{
			get
			{
				return this.countryCodeField;
			}
			set
			{
				this.countryCodeField = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x00020CD9 File Offset: 0x0001EED9
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x00020CE1 File Offset: 0x0001EEE1
		public DirectoryPropertyStringSingleLength1To128 DefaultGeography
		{
			get
			{
				return this.defaultGeographyField;
			}
			set
			{
				this.defaultGeographyField = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00020CEA File Offset: 0x0001EEEA
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00020CF2 File Offset: 0x0001EEF2
		public DirectoryPropertyBooleanSingle DeliverAndRedirect
		{
			get
			{
				return this.deliverAndRedirectField;
			}
			set
			{
				this.deliverAndRedirectField = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00020CFB File Offset: 0x0001EEFB
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00020D03 File Offset: 0x0001EF03
		public DirectoryPropertyStringSingleLength1To64 Department
		{
			get
			{
				return this.departmentField;
			}
			set
			{
				this.departmentField = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00020D0C File Offset: 0x0001EF0C
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x00020D14 File Offset: 0x0001EF14
		public DirectoryPropertyStringSingleLength1To1024 Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00020D1D File Offset: 0x0001EF1D
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00020D25 File Offset: 0x0001EF25
		public DirectoryPropertyBooleanSingle DirSyncEnabled
		{
			get
			{
				return this.dirSyncEnabledField;
			}
			set
			{
				this.dirSyncEnabledField = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00020D2E File Offset: 0x0001EF2E
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00020D36 File Offset: 0x0001EF36
		public DirectoryPropertyInt32SingleMin0 DirSyncOverrides
		{
			get
			{
				return this.dirSyncOverridesField;
			}
			set
			{
				this.dirSyncOverridesField = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00020D3F File Offset: 0x0001EF3F
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x00020D47 File Offset: 0x0001EF47
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00020D50 File Offset: 0x0001EF50
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00020D58 File Offset: 0x0001EF58
		public DirectoryPropertyStringSingleLength1To16 EmployeeId
		{
			get
			{
				return this.employeeIdField;
			}
			set
			{
				this.employeeIdField = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00020D61 File Offset: 0x0001EF61
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00020D69 File Offset: 0x0001EF69
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute1
		{
			get
			{
				return this.extensionAttribute1Field;
			}
			set
			{
				this.extensionAttribute1Field = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00020D72 File Offset: 0x0001EF72
		// (set) Token: 0x0600096E RID: 2414 RVA: 0x00020D7A File Offset: 0x0001EF7A
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute10
		{
			get
			{
				return this.extensionAttribute10Field;
			}
			set
			{
				this.extensionAttribute10Field = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00020D83 File Offset: 0x0001EF83
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x00020D8B File Offset: 0x0001EF8B
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute11
		{
			get
			{
				return this.extensionAttribute11Field;
			}
			set
			{
				this.extensionAttribute11Field = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00020D94 File Offset: 0x0001EF94
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x00020D9C File Offset: 0x0001EF9C
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute12
		{
			get
			{
				return this.extensionAttribute12Field;
			}
			set
			{
				this.extensionAttribute12Field = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00020DA5 File Offset: 0x0001EFA5
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x00020DAD File Offset: 0x0001EFAD
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute13
		{
			get
			{
				return this.extensionAttribute13Field;
			}
			set
			{
				this.extensionAttribute13Field = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00020DB6 File Offset: 0x0001EFB6
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00020DBE File Offset: 0x0001EFBE
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute14
		{
			get
			{
				return this.extensionAttribute14Field;
			}
			set
			{
				this.extensionAttribute14Field = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00020DC7 File Offset: 0x0001EFC7
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00020DCF File Offset: 0x0001EFCF
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute15
		{
			get
			{
				return this.extensionAttribute15Field;
			}
			set
			{
				this.extensionAttribute15Field = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00020DE0 File Offset: 0x0001EFE0
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute2
		{
			get
			{
				return this.extensionAttribute2Field;
			}
			set
			{
				this.extensionAttribute2Field = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00020DE9 File Offset: 0x0001EFE9
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x00020DF1 File Offset: 0x0001EFF1
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute3
		{
			get
			{
				return this.extensionAttribute3Field;
			}
			set
			{
				this.extensionAttribute3Field = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00020DFA File Offset: 0x0001EFFA
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x00020E02 File Offset: 0x0001F002
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute4
		{
			get
			{
				return this.extensionAttribute4Field;
			}
			set
			{
				this.extensionAttribute4Field = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00020E0B File Offset: 0x0001F00B
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x00020E13 File Offset: 0x0001F013
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute5
		{
			get
			{
				return this.extensionAttribute5Field;
			}
			set
			{
				this.extensionAttribute5Field = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00020E1C File Offset: 0x0001F01C
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00020E24 File Offset: 0x0001F024
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute6
		{
			get
			{
				return this.extensionAttribute6Field;
			}
			set
			{
				this.extensionAttribute6Field = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00020E2D File Offset: 0x0001F02D
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00020E35 File Offset: 0x0001F035
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute7
		{
			get
			{
				return this.extensionAttribute7Field;
			}
			set
			{
				this.extensionAttribute7Field = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00020E3E File Offset: 0x0001F03E
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00020E46 File Offset: 0x0001F046
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute8
		{
			get
			{
				return this.extensionAttribute8Field;
			}
			set
			{
				this.extensionAttribute8Field = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00020E4F File Offset: 0x0001F04F
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00020E57 File Offset: 0x0001F057
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute9
		{
			get
			{
				return this.extensionAttribute9Field;
			}
			set
			{
				this.extensionAttribute9Field = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00020E60 File Offset: 0x0001F060
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00020E68 File Offset: 0x0001F068
		public DirectoryPropertyStringSingleLength1To64 FacsimileTelephoneNumber
		{
			get
			{
				return this.facsimileTelephoneNumberField;
			}
			set
			{
				this.facsimileTelephoneNumberField = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00020E71 File Offset: 0x0001F071
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00020E79 File Offset: 0x0001F079
		public DirectoryPropertyStringSingleLength1To64 GivenName
		{
			get
			{
				return this.givenNameField;
			}
			set
			{
				this.givenNameField = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00020E82 File Offset: 0x0001F082
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00020E8A File Offset: 0x0001F08A
		public DirectoryPropertyStringSingleLength1To64 HomePhone
		{
			get
			{
				return this.homePhoneField;
			}
			set
			{
				this.homePhoneField = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00020E93 File Offset: 0x0001F093
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00020E9B File Offset: 0x0001F09B
		public DirectoryPropertyStringSingleLength1To1024 Info
		{
			get
			{
				return this.infoField;
			}
			set
			{
				this.infoField = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00020EAC File Offset: 0x0001F0AC
		public DirectoryPropertyStringSingleLength1To6 Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00020EB5 File Offset: 0x0001F0B5
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00020EBD File Offset: 0x0001F0BD
		public DirectoryPropertyInt32Single InternetEncoding
		{
			get
			{
				return this.internetEncodingField;
			}
			set
			{
				this.internetEncodingField = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00020EC6 File Offset: 0x0001F0C6
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x00020ECE File Offset: 0x0001F0CE
		public DirectoryPropertyStringSingleLength1To64 IPPhone
		{
			get
			{
				return this.iPPhoneField;
			}
			set
			{
				this.iPPhoneField = value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00020ED7 File Offset: 0x0001F0D7
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x00020EDF File Offset: 0x0001F0DF
		public DirectoryPropertyStringSingleLength1To128 L
		{
			get
			{
				return this.lField;
			}
			set
			{
				this.lField = value;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		public DirectoryPropertyDateTimeSingle LastRestorationTimestamp
		{
			get
			{
				return this.lastRestorationTimestampField;
			}
			set
			{
				this.lastRestorationTimestampField = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00020EF9 File Offset: 0x0001F0F9
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00020F01 File Offset: 0x0001F101
		public DirectoryPropertyDateTimeSingle LastDirSyncTime
		{
			get
			{
				return this.lastDirSyncTimeField;
			}
			set
			{
				this.lastDirSyncTimeField = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00020F0A File Offset: 0x0001F10A
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00020F12 File Offset: 0x0001F112
		public DirectoryPropertyStringSingleLength1To256 Mail
		{
			get
			{
				return this.mailField;
			}
			set
			{
				this.mailField = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00020F1B File Offset: 0x0001F11B
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x00020F23 File Offset: 0x0001F123
		public DirectoryPropertyStringSingleMailNickname MailNickname
		{
			get
			{
				return this.mailNicknameField;
			}
			set
			{
				this.mailNicknameField = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00020F2C File Offset: 0x0001F12C
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00020F34 File Offset: 0x0001F134
		public DirectoryPropertyStringSingleLength1To64 MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00020F3D File Offset: 0x0001F13D
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00020F45 File Offset: 0x0001F145
		public DirectoryPropertyXmlMigrationDetail MigrationDetail
		{
			get
			{
				return this.migrationDetailField;
			}
			set
			{
				this.migrationDetailField = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x00020F4E File Offset: 0x0001F14E
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00020F56 File Offset: 0x0001F156
		public DirectoryPropertyStringSingleLength1To256 MigrationSourceAnchor
		{
			get
			{
				return this.migrationSourceAnchorField;
			}
			set
			{
				this.migrationSourceAnchorField = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00020F5F File Offset: 0x0001F15F
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00020F67 File Offset: 0x0001F167
		public DirectoryPropertyInt32SingleMin0 MigrationState
		{
			get
			{
				return this.migrationStateField;
			}
			set
			{
				this.migrationStateField = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00020F70 File Offset: 0x0001F170
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00020F78 File Offset: 0x0001F178
		public DirectoryPropertyStringSingleLength1To64 Mobile
		{
			get
			{
				return this.mobileField;
			}
			set
			{
				this.mobileField = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00020F81 File Offset: 0x0001F181
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00020F89 File Offset: 0x0001F189
		public DirectoryPropertyInt32Single MSDSHABSeniorityIndex
		{
			get
			{
				return this.mSDSHABSeniorityIndexField;
			}
			set
			{
				this.mSDSHABSeniorityIndexField = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00020F92 File Offset: 0x0001F192
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00020F9A File Offset: 0x0001F19A
		public DirectoryPropertyStringSingleLength1To256 MSDSPhoneticDisplayName
		{
			get
			{
				return this.mSDSPhoneticDisplayNameField;
			}
			set
			{
				this.mSDSPhoneticDisplayNameField = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00020FA3 File Offset: 0x0001F1A3
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00020FAB File Offset: 0x0001F1AB
		public DirectoryPropertyGuidSingle MSExchArchiveGuid
		{
			get
			{
				return this.mSExchArchiveGuidField;
			}
			set
			{
				this.mSExchArchiveGuidField = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00020FBC File Offset: 0x0001F1BC
		public DirectoryPropertyStringLength1To512 MSExchArchiveName
		{
			get
			{
				return this.mSExchArchiveNameField;
			}
			set
			{
				this.mSExchArchiveNameField = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00020FC5 File Offset: 0x0001F1C5
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00020FCD File Offset: 0x0001F1CD
		public DirectoryPropertyStringSingleLength1To256 MSExchAssistantName
		{
			get
			{
				return this.mSExchAssistantNameField;
			}
			set
			{
				this.mSExchAssistantNameField = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00020FD6 File Offset: 0x0001F1D6
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00020FDE File Offset: 0x0001F1DE
		public DirectoryPropertyInt32Single MSExchAuditAdmin
		{
			get
			{
				return this.mSExchAuditAdminField;
			}
			set
			{
				this.mSExchAuditAdminField = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00020FE7 File Offset: 0x0001F1E7
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00020FEF File Offset: 0x0001F1EF
		public DirectoryPropertyInt32Single MSExchAuditDelegate
		{
			get
			{
				return this.mSExchAuditDelegateField;
			}
			set
			{
				this.mSExchAuditDelegateField = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00020FF8 File Offset: 0x0001F1F8
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00021000 File Offset: 0x0001F200
		public DirectoryPropertyInt32Single MSExchAuditDelegateAdmin
		{
			get
			{
				return this.mSExchAuditDelegateAdminField;
			}
			set
			{
				this.mSExchAuditDelegateAdminField = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00021009 File Offset: 0x0001F209
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00021011 File Offset: 0x0001F211
		public DirectoryPropertyInt32Single MSExchAuditOwner
		{
			get
			{
				return this.mSExchAuditOwnerField;
			}
			set
			{
				this.mSExchAuditOwnerField = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002101A File Offset: 0x0001F21A
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00021022 File Offset: 0x0001F222
		public DirectoryPropertyBinarySingleLength1To4000 MSExchBlockedSendersHash
		{
			get
			{
				return this.mSExchBlockedSendersHashField;
			}
			set
			{
				this.mSExchBlockedSendersHashField = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002102B File Offset: 0x0001F22B
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00021033 File Offset: 0x0001F233
		public DirectoryPropertyBooleanSingle MSExchBypassAudit
		{
			get
			{
				return this.mSExchBypassAuditField;
			}
			set
			{
				this.mSExchBypassAuditField = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0002103C File Offset: 0x0001F23C
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00021044 File Offset: 0x0001F244
		public DirectoryPropertyDateTimeSingle MSExchElcExpirySuspensionEnd
		{
			get
			{
				return this.mSExchElcExpirySuspensionEndField;
			}
			set
			{
				this.mSExchElcExpirySuspensionEndField = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002104D File Offset: 0x0001F24D
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00021055 File Offset: 0x0001F255
		public DirectoryPropertyDateTimeSingle MSExchElcExpirySuspensionStart
		{
			get
			{
				return this.mSExchElcExpirySuspensionStartField;
			}
			set
			{
				this.mSExchElcExpirySuspensionStartField = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002105E File Offset: 0x0001F25E
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00021066 File Offset: 0x0001F266
		public DirectoryPropertyInt32Single MSExchElcMailboxFlags
		{
			get
			{
				return this.mSExchElcMailboxFlagsField;
			}
			set
			{
				this.mSExchElcMailboxFlagsField = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002106F File Offset: 0x0001F26F
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00021077 File Offset: 0x0001F277
		public DirectoryPropertyBooleanSingle MSExchEnableModeration
		{
			get
			{
				return this.mSExchEnableModerationField;
			}
			set
			{
				this.mSExchEnableModerationField = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00021080 File Offset: 0x0001F280
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00021088 File Offset: 0x0001F288
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute1
		{
			get
			{
				return this.mSExchExtensionCustomAttribute1Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute1Field = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00021091 File Offset: 0x0001F291
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00021099 File Offset: 0x0001F299
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute2
		{
			get
			{
				return this.mSExchExtensionCustomAttribute2Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute2Field = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x000210A2 File Offset: 0x0001F2A2
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x000210AA File Offset: 0x0001F2AA
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute3
		{
			get
			{
				return this.mSExchExtensionCustomAttribute3Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute3Field = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x000210B3 File Offset: 0x0001F2B3
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x000210BB File Offset: 0x0001F2BB
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute4
		{
			get
			{
				return this.mSExchExtensionCustomAttribute4Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute4Field = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000210C4 File Offset: 0x0001F2C4
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x000210CC File Offset: 0x0001F2CC
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute5
		{
			get
			{
				return this.mSExchExtensionCustomAttribute5Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute5Field = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x000210D5 File Offset: 0x0001F2D5
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x000210DD File Offset: 0x0001F2DD
		public DirectoryPropertyBooleanSingle MSExchHideFromAddressLists
		{
			get
			{
				return this.mSExchHideFromAddressListsField;
			}
			set
			{
				this.mSExchHideFromAddressListsField = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x000210E6 File Offset: 0x0001F2E6
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x000210EE File Offset: 0x0001F2EE
		public DirectoryPropertyStringSingleLength1To256 MSExchImmutableId
		{
			get
			{
				return this.mSExchImmutableIdField;
			}
			set
			{
				this.mSExchImmutableIdField = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x000210F7 File Offset: 0x0001F2F7
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x000210FF File Offset: 0x0001F2FF
		public DirectoryPropertyDateTimeSingle MSExchLitigationHoldDate
		{
			get
			{
				return this.mSExchLitigationHoldDateField;
			}
			set
			{
				this.mSExchLitigationHoldDateField = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00021108 File Offset: 0x0001F308
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00021110 File Offset: 0x0001F310
		public DirectoryPropertyStringSingleLength1To1024 MSExchLitigationHoldOwner
		{
			get
			{
				return this.mSExchLitigationHoldOwnerField;
			}
			set
			{
				this.mSExchLitigationHoldOwnerField = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00021119 File Offset: 0x0001F319
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00021121 File Offset: 0x0001F321
		public DirectoryPropertyBooleanSingle MSExchMailboxAuditEnable
		{
			get
			{
				return this.mSExchMailboxAuditEnableField;
			}
			set
			{
				this.mSExchMailboxAuditEnableField = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0002112A File Offset: 0x0001F32A
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00021132 File Offset: 0x0001F332
		public DirectoryPropertyInt32Single MSExchMailboxAuditLogAgeLimit
		{
			get
			{
				return this.mSExchMailboxAuditLogAgeLimitField;
			}
			set
			{
				this.mSExchMailboxAuditLogAgeLimitField = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002113B File Offset: 0x0001F33B
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00021143 File Offset: 0x0001F343
		public DirectoryPropertyGuidSingle MSExchMailboxGuid
		{
			get
			{
				return this.mSExchMailboxGuidField;
			}
			set
			{
				this.mSExchMailboxGuidField = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002114C File Offset: 0x0001F34C
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00021154 File Offset: 0x0001F354
		public DirectoryPropertyInt32Single MSExchModerationFlags
		{
			get
			{
				return this.mSExchModerationFlagsField;
			}
			set
			{
				this.mSExchModerationFlagsField = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002115D File Offset: 0x0001F35D
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00021165 File Offset: 0x0001F365
		public DirectoryPropertyInt32Single MSExchRecipientDisplayType
		{
			get
			{
				return this.mSExchRecipientDisplayTypeField;
			}
			set
			{
				this.mSExchRecipientDisplayTypeField = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002116E File Offset: 0x0001F36E
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00021176 File Offset: 0x0001F376
		public DirectoryPropertyInt64Single MSExchRecipientTypeDetails
		{
			get
			{
				return this.mSExchRecipientTypeDetailsField;
			}
			set
			{
				this.mSExchRecipientTypeDetailsField = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002117F File Offset: 0x0001F37F
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00021187 File Offset: 0x0001F387
		public DirectoryPropertyInt64Single MSExchRemoteRecipientType
		{
			get
			{
				return this.mSExchRemoteRecipientTypeField;
			}
			set
			{
				this.mSExchRemoteRecipientTypeField = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00021190 File Offset: 0x0001F390
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00021198 File Offset: 0x0001F398
		public DirectoryPropertyBooleanSingle MSExchRequireAuthToSendTo
		{
			get
			{
				return this.mSExchRequireAuthToSendToField;
			}
			set
			{
				this.mSExchRequireAuthToSendToField = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000211A1 File Offset: 0x0001F3A1
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x000211A9 File Offset: 0x0001F3A9
		public DirectoryPropertyInt32Single MSExchResourceCapacity
		{
			get
			{
				return this.mSExchResourceCapacityField;
			}
			set
			{
				this.mSExchResourceCapacityField = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x000211B2 File Offset: 0x0001F3B2
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x000211BA File Offset: 0x0001F3BA
		public DirectoryPropertyStringSingleLength1To1024 MSExchResourceDisplay
		{
			get
			{
				return this.mSExchResourceDisplayField;
			}
			set
			{
				this.mSExchResourceDisplayField = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000211C3 File Offset: 0x0001F3C3
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x000211CB File Offset: 0x0001F3CB
		public DirectoryPropertyStringLength1To1024 MSExchResourceMetadata
		{
			get
			{
				return this.mSExchResourceMetadataField;
			}
			set
			{
				this.mSExchResourceMetadataField = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x000211D4 File Offset: 0x0001F3D4
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x000211DC File Offset: 0x0001F3DC
		public DirectoryPropertyStringLength1To1024 MSExchResourceSearchProperties
		{
			get
			{
				return this.mSExchResourceSearchPropertiesField;
			}
			set
			{
				this.mSExchResourceSearchPropertiesField = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000211E5 File Offset: 0x0001F3E5
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x000211ED File Offset: 0x0001F3ED
		public DirectoryPropertyStringSingleLength1To1024 MSExchRetentionComment
		{
			get
			{
				return this.mSExchRetentionCommentField;
			}
			set
			{
				this.mSExchRetentionCommentField = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000211F6 File Offset: 0x0001F3F6
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x000211FE File Offset: 0x0001F3FE
		public DirectoryPropertyStringSingleLength1To2048 MSExchRetentionUrl
		{
			get
			{
				return this.mSExchRetentionUrlField;
			}
			set
			{
				this.mSExchRetentionUrlField = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00021207 File Offset: 0x0001F407
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0002120F File Offset: 0x0001F40F
		public DirectoryPropertyBinarySingleLength1To12000 MSExchSafeRecipientsHash
		{
			get
			{
				return this.mSExchSafeRecipientsHashField;
			}
			set
			{
				this.mSExchSafeRecipientsHashField = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00021218 File Offset: 0x0001F418
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00021220 File Offset: 0x0001F420
		public DirectoryPropertyBinarySingleLength1To32000 MSExchSafeSendersHash
		{
			get
			{
				return this.mSExchSafeSendersHashField;
			}
			set
			{
				this.mSExchSafeSendersHashField = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00021229 File Offset: 0x0001F429
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00021231 File Offset: 0x0001F431
		public DirectoryPropertyStringLength2To500 MSExchSenderHintTranslations
		{
			get
			{
				return this.mSExchSenderHintTranslationsField;
			}
			set
			{
				this.mSExchSenderHintTranslationsField = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002123A File Offset: 0x0001F43A
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00021242 File Offset: 0x0001F442
		public DirectoryPropertyDateTimeSingle MSExchTeamMailboxExpiration
		{
			get
			{
				return this.mSExchTeamMailboxExpirationField;
			}
			set
			{
				this.mSExchTeamMailboxExpirationField = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002124B File Offset: 0x0001F44B
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00021253 File Offset: 0x0001F453
		public DirectoryPropertyReferenceAddressList MSExchTeamMailboxOwners
		{
			get
			{
				return this.mSExchTeamMailboxOwnersField;
			}
			set
			{
				this.mSExchTeamMailboxOwnersField = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0002125C File Offset: 0x0001F45C
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00021264 File Offset: 0x0001F464
		public DirectoryPropertyReferenceAddressListSingle MSExchTeamMailboxSharePointLinkedBy
		{
			get
			{
				return this.mSExchTeamMailboxSharePointLinkedByField;
			}
			set
			{
				this.mSExchTeamMailboxSharePointLinkedByField = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002126D File Offset: 0x0001F46D
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00021275 File Offset: 0x0001F475
		public DirectoryPropertyStringSingleLength1To2048 MSExchTeamMailboxSharePointUrl
		{
			get
			{
				return this.mSExchTeamMailboxSharePointUrlField;
			}
			set
			{
				this.mSExchTeamMailboxSharePointUrlField = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002127E File Offset: 0x0001F47E
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00021286 File Offset: 0x0001F486
		public DirectoryPropertyStringLength1To40 MSExchUserHoldPolicies
		{
			get
			{
				return this.mSExchUserHoldPoliciesField;
			}
			set
			{
				this.mSExchUserHoldPoliciesField = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002128F File Offset: 0x0001F48F
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00021297 File Offset: 0x0001F497
		public DirectoryPropertyInt32SingleMin0 MSRtcSipApplicationOptions
		{
			get
			{
				return this.mSRtcSipApplicationOptionsField;
			}
			set
			{
				this.mSRtcSipApplicationOptionsField = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x000212A0 File Offset: 0x0001F4A0
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x000212A8 File Offset: 0x0001F4A8
		public DirectoryPropertyStringSingleLength1To256 MSRtcSipDeploymentLocator
		{
			get
			{
				return this.mSRtcSipDeploymentLocatorField;
			}
			set
			{
				this.mSRtcSipDeploymentLocatorField = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000212B1 File Offset: 0x0001F4B1
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x000212B9 File Offset: 0x0001F4B9
		public DirectoryPropertyStringSingleLength1To500 MSRtcSipLine
		{
			get
			{
				return this.mSRtcSipLineField;
			}
			set
			{
				this.mSRtcSipLineField = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000212C2 File Offset: 0x0001F4C2
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x000212CA File Offset: 0x0001F4CA
		public DirectoryPropertyInt32Single MSRtcSipOptionFlags
		{
			get
			{
				return this.mSRtcSipOptionFlagsField;
			}
			set
			{
				this.mSRtcSipOptionFlagsField = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x000212D3 File Offset: 0x0001F4D3
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x000212DB File Offset: 0x0001F4DB
		public DirectoryPropertyStringSingleLength1To512 MSRtcSipOwnerUrn
		{
			get
			{
				return this.mSRtcSipOwnerUrnField;
			}
			set
			{
				this.mSRtcSipOwnerUrnField = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x000212E4 File Offset: 0x0001F4E4
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x000212EC File Offset: 0x0001F4EC
		public DirectoryPropertyStringSingleLength1To454 MSRtcSipPrimaryUserAddress
		{
			get
			{
				return this.mSRtcSipPrimaryUserAddressField;
			}
			set
			{
				this.mSRtcSipPrimaryUserAddressField = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000212F5 File Offset: 0x0001F4F5
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x000212FD File Offset: 0x0001F4FD
		public DirectoryPropertyBooleanSingle MSRtcSipUserEnabled
		{
			get
			{
				return this.mSRtcSipUserEnabledField;
			}
			set
			{
				this.mSRtcSipUserEnabledField = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00021306 File Offset: 0x0001F506
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0002130E File Offset: 0x0001F50E
		public DirectoryPropertyBinarySingleLength1To128 OnPremiseSecurityIdentifier
		{
			get
			{
				return this.onPremiseSecurityIdentifierField;
			}
			set
			{
				this.onPremiseSecurityIdentifierField = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00021317 File Offset: 0x0001F517
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0002131F File Offset: 0x0001F51F
		public DirectoryPropertyStringLength1To64 OtherFacsimileTelephoneNumber
		{
			get
			{
				return this.otherFacsimileTelephoneNumberField;
			}
			set
			{
				this.otherFacsimileTelephoneNumberField = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00021328 File Offset: 0x0001F528
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x00021330 File Offset: 0x0001F530
		public DirectoryPropertyStringLength1To64 OtherHomePhone
		{
			get
			{
				return this.otherHomePhoneField;
			}
			set
			{
				this.otherHomePhoneField = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00021339 File Offset: 0x0001F539
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00021341 File Offset: 0x0001F541
		public DirectoryPropertyStringLength1To512 OtherIPPhone
		{
			get
			{
				return this.otherIPPhoneField;
			}
			set
			{
				this.otherIPPhoneField = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002134A File Offset: 0x0001F54A
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x00021352 File Offset: 0x0001F552
		public DirectoryPropertyStringLength1To256 OtherMail
		{
			get
			{
				return this.otherMailField;
			}
			set
			{
				this.otherMailField = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002135B File Offset: 0x0001F55B
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x00021363 File Offset: 0x0001F563
		public DirectoryPropertyStringLength1To64 OtherMobile
		{
			get
			{
				return this.otherMobileField;
			}
			set
			{
				this.otherMobileField = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002136C File Offset: 0x0001F56C
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00021374 File Offset: 0x0001F574
		public DirectoryPropertyStringLength1To64 OtherPager
		{
			get
			{
				return this.otherPagerField;
			}
			set
			{
				this.otherPagerField = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002137D File Offset: 0x0001F57D
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00021385 File Offset: 0x0001F585
		public DirectoryPropertyStringLength1To64 OtherTelephone
		{
			get
			{
				return this.otherTelephoneField;
			}
			set
			{
				this.otherTelephoneField = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002138E File Offset: 0x0001F58E
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x00021396 File Offset: 0x0001F596
		public DirectoryPropertyInt32SingleMin0 PasswordPolicies
		{
			get
			{
				return this.passwordPoliciesField;
			}
			set
			{
				this.passwordPoliciesField = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002139F File Offset: 0x0001F59F
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x000213A7 File Offset: 0x0001F5A7
		public DirectoryPropertyStringSingleLength1To64 Pager
		{
			get
			{
				return this.pagerField;
			}
			set
			{
				this.pagerField = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000213B0 File Offset: 0x0001F5B0
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x000213B8 File Offset: 0x0001F5B8
		public DirectoryPropertyStringSingleLength1To128 PhysicalDeliveryOfficeName
		{
			get
			{
				return this.physicalDeliveryOfficeNameField;
			}
			set
			{
				this.physicalDeliveryOfficeNameField = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000213C1 File Offset: 0x0001F5C1
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x000213C9 File Offset: 0x0001F5C9
		public DirectoryPropertyXmlAnySingle PortalSetting
		{
			get
			{
				return this.portalSettingField;
			}
			set
			{
				this.portalSettingField = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000213D2 File Offset: 0x0001F5D2
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x000213DA File Offset: 0x0001F5DA
		public DirectoryPropertyStringSingleLength1To40 PostalCode
		{
			get
			{
				return this.postalCodeField;
			}
			set
			{
				this.postalCodeField = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000213E3 File Offset: 0x0001F5E3
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x000213EB File Offset: 0x0001F5EB
		public DirectoryPropertyStringLength1To40 PostOfficeBox
		{
			get
			{
				return this.postOfficeBoxField;
			}
			set
			{
				this.postOfficeBoxField = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000213F4 File Offset: 0x0001F5F4
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x000213FC File Offset: 0x0001F5FC
		public DirectoryPropertyStringSingleLength1To64 PreferredLanguage
		{
			get
			{
				return this.preferredLanguageField;
			}
			set
			{
				this.preferredLanguageField = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00021405 File Offset: 0x0001F605
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0002140D File Offset: 0x0001F60D
		public DirectoryPropertyXmlProvisionedPlan ProvisionedPlan
		{
			get
			{
				return this.provisionedPlanField;
			}
			set
			{
				this.provisionedPlanField = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00021416 File Offset: 0x0001F616
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0002141E File Offset: 0x0001F61E
		public DirectoryPropertyProxyAddresses ProxyAddresses
		{
			get
			{
				return this.proxyAddressesField;
			}
			set
			{
				this.proxyAddressesField = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00021427 File Offset: 0x0001F627
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0002142F File Offset: 0x0001F62F
		public DirectoryPropertyXmlRightsManagementUserKeySingle RightsManagementUserKey
		{
			get
			{
				return this.rightsManagementUserKeyField;
			}
			set
			{
				this.rightsManagementUserKeyField = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00021438 File Offset: 0x0001F638
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00021440 File Offset: 0x0001F640
		public DirectoryPropertyXmlServiceInfo ServiceInfo
		{
			get
			{
				return this.serviceInfoField;
			}
			set
			{
				this.serviceInfoField = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00021449 File Offset: 0x0001F649
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00021451 File Offset: 0x0001F651
		public DirectoryPropertyXmlServiceOriginatedResource ServiceOriginatedResource
		{
			get
			{
				return this.serviceOriginatedResourceField;
			}
			set
			{
				this.serviceOriginatedResourceField = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002145A File Offset: 0x0001F65A
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00021462 File Offset: 0x0001F662
		public DirectoryPropertyStringSingleLength1To64 ShadowAlias
		{
			get
			{
				return this.shadowAliasField;
			}
			set
			{
				this.shadowAliasField = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002146B File Offset: 0x0001F66B
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00021473 File Offset: 0x0001F673
		public DirectoryPropertyStringSingleLength1To64 ShadowCommonName
		{
			get
			{
				return this.shadowCommonNameField;
			}
			set
			{
				this.shadowCommonNameField = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0002147C File Offset: 0x0001F67C
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x00021484 File Offset: 0x0001F684
		public DirectoryPropertyStringSingleLength1To256 ShadowDisplayName
		{
			get
			{
				return this.shadowDisplayNameField;
			}
			set
			{
				this.shadowDisplayNameField = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0002148D File Offset: 0x0001F68D
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x00021495 File Offset: 0x0001F695
		public DirectoryPropertyStringSingleLength1To2048 ShadowLegacyExchangeDN
		{
			get
			{
				return this.shadowLegacyExchangeDNField;
			}
			set
			{
				this.shadowLegacyExchangeDNField = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002149E File Offset: 0x0001F69E
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x000214A6 File Offset: 0x0001F6A6
		public DirectoryPropertyStringSingleLength1To256 ShadowMail
		{
			get
			{
				return this.shadowMailField;
			}
			set
			{
				this.shadowMailField = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000214AF File Offset: 0x0001F6AF
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x000214B7 File Offset: 0x0001F6B7
		public DirectoryPropertyStringSingleLength1To64 ShadowMobile
		{
			get
			{
				return this.shadowMobileField;
			}
			set
			{
				this.shadowMobileField = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000214C0 File Offset: 0x0001F6C0
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public DirectoryPropertyStringLength1To1123 ShadowProxyAddresses
		{
			get
			{
				return this.shadowProxyAddressesField;
			}
			set
			{
				this.shadowProxyAddressesField = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x000214D1 File Offset: 0x0001F6D1
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x000214D9 File Offset: 0x0001F6D9
		public DirectoryPropertyStringSingleLength1To1123 ShadowTargetAddress
		{
			get
			{
				return this.shadowTargetAddressField;
			}
			set
			{
				this.shadowTargetAddressField = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x000214E2 File Offset: 0x0001F6E2
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x000214EA File Offset: 0x0001F6EA
		public DirectoryPropertyStringSingleLength1To1024 ShadowUserPrincipalName
		{
			get
			{
				return this.shadowUserPrincipalNameField;
			}
			set
			{
				this.shadowUserPrincipalNameField = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000214F3 File Offset: 0x0001F6F3
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x000214FB File Offset: 0x0001F6FB
		public DirectoryPropertyStringSingleLength1To454 SipProxyAddress
		{
			get
			{
				return this.sipProxyAddressField;
			}
			set
			{
				this.sipProxyAddressField = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00021504 File Offset: 0x0001F704
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0002150C File Offset: 0x0001F70C
		public DirectoryPropertyStringSingleLength1To64 Sn
		{
			get
			{
				return this.snField;
			}
			set
			{
				this.snField = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00021515 File Offset: 0x0001F715
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0002151D File Offset: 0x0001F71D
		public DirectoryPropertyDateTimeSingle SoftDeletionTimestamp
		{
			get
			{
				return this.softDeletionTimestampField;
			}
			set
			{
				this.softDeletionTimestampField = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00021526 File Offset: 0x0001F726
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0002152E File Offset: 0x0001F72E
		public DirectoryPropertyStringSingleLength1To256 SourceAnchor
		{
			get
			{
				return this.sourceAnchorField;
			}
			set
			{
				this.sourceAnchorField = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00021537 File Offset: 0x0001F737
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0002153F File Offset: 0x0001F73F
		public DirectoryPropertyStringSingleLength1To128 St
		{
			get
			{
				return this.stField;
			}
			set
			{
				this.stField = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00021548 File Offset: 0x0001F748
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00021550 File Offset: 0x0001F750
		public DirectoryPropertyStringSingleLength1To1024 Street
		{
			get
			{
				return this.streetField;
			}
			set
			{
				this.streetField = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00021559 File Offset: 0x0001F759
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00021561 File Offset: 0x0001F761
		public DirectoryPropertyStringSingleLength1To1024 StreetAddress
		{
			get
			{
				return this.streetAddressField;
			}
			set
			{
				this.streetAddressField = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002156A File Offset: 0x0001F76A
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00021572 File Offset: 0x0001F772
		public DirectoryPropertyXmlStrongAuthenticationMethod StrongAuthenticationMethod
		{
			get
			{
				return this.strongAuthenticationMethodField;
			}
			set
			{
				this.strongAuthenticationMethodField = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002157B File Offset: 0x0001F77B
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00021583 File Offset: 0x0001F783
		public DirectoryPropertyTargetAddress TargetAddress
		{
			get
			{
				return this.targetAddressField;
			}
			set
			{
				this.targetAddressField = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002158C File Offset: 0x0001F78C
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00021594 File Offset: 0x0001F794
		public DirectoryPropertyStringSingleLength1To64 TelephoneAssistant
		{
			get
			{
				return this.telephoneAssistantField;
			}
			set
			{
				this.telephoneAssistantField = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002159D File Offset: 0x0001F79D
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x000215A5 File Offset: 0x0001F7A5
		public DirectoryPropertyStringSingleLength1To64 TelephoneNumber
		{
			get
			{
				return this.telephoneNumberField;
			}
			set
			{
				this.telephoneNumberField = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x000215AE File Offset: 0x0001F7AE
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x000215B6 File Offset: 0x0001F7B6
		public DirectoryPropertyBinarySingleLength1To102400 ThumbnailPhoto
		{
			get
			{
				return this.thumbnailPhotoField;
			}
			set
			{
				this.thumbnailPhotoField = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000215BF File Offset: 0x0001F7BF
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x000215C7 File Offset: 0x0001F7C7
		public DirectoryPropertyStringSingleLength1To128 Title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000215D0 File Offset: 0x0001F7D0
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x000215D8 File Offset: 0x0001F7D8
		public DirectoryPropertyStringLength1To1123 Url
		{
			get
			{
				return this.urlField;
			}
			set
			{
				this.urlField = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000215E1 File Offset: 0x0001F7E1
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x000215E9 File Offset: 0x0001F7E9
		public DirectoryPropertyStringSingleLength1To3 UsageLocation
		{
			get
			{
				return this.usageLocationField;
			}
			set
			{
				this.usageLocationField = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000215F2 File Offset: 0x0001F7F2
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000215FA File Offset: 0x0001F7FA
		public DirectoryPropertyStringSingleLength1To1024 UserPrincipalName
		{
			get
			{
				return this.userPrincipalNameField;
			}
			set
			{
				this.userPrincipalNameField = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00021603 File Offset: 0x0001F803
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0002160B File Offset: 0x0001F80B
		public DirectoryPropertyXmlValidationError ValidationError
		{
			get
			{
				return this.validationErrorField;
			}
			set
			{
				this.validationErrorField = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00021614 File Offset: 0x0001F814
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0002161C File Offset: 0x0001F81C
		public DirectoryPropertyBinarySingleLength8 WindowsLiveNetId
		{
			get
			{
				return this.windowsLiveNetIdField;
			}
			set
			{
				this.windowsLiveNetIdField = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00021625 File Offset: 0x0001F825
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0002162D File Offset: 0x0001F82D
		public DirectoryPropertyStringSingleLength1To2048 WwwHomepage
		{
			get
			{
				return this.wwwHomepageField;
			}
			set
			{
				this.wwwHomepageField = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00021636 File Offset: 0x0001F836
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0002163E File Offset: 0x0001F83E
		[XmlArray(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01")]
		[XmlArrayItem("AttributeSet", IsNullable = false)]
		public AttributeSet[] SingleAuthorityMetadata
		{
			get
			{
				return this.singleAuthorityMetadataField;
			}
			set
			{
				this.singleAuthorityMetadataField = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00021647 File Offset: 0x0001F847
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0002164F File Offset: 0x0001F84F
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x04000495 RID: 1173
		private DirectoryPropertyBooleanSingle accountEnabledField;

		// Token: 0x04000496 RID: 1174
		private DirectoryPropertyXmlAlternativeSecurityId alternativeSecurityIdField;

		// Token: 0x04000497 RID: 1175
		private DirectoryPropertyXmlAssignedPlan assignedPlanField;

		// Token: 0x04000498 RID: 1176
		private DirectoryPropertyXmlAssignedLicense assignedLicenseField;

		// Token: 0x04000499 RID: 1177
		private DirectoryPropertyReferenceAddressListSingle assistantField;

		// Token: 0x0400049A RID: 1178
		private DirectoryPropertyBooleanSingle belongsToFirstLoginObjectSetField;

		// Token: 0x0400049B RID: 1179
		private DirectoryPropertyStringSingleLength1To256 besServiceInstanceField;

		// Token: 0x0400049C RID: 1180
		private DirectoryPropertyStringSingleLength1To3 cField;

		// Token: 0x0400049D RID: 1181
		private DirectoryPropertyStringSingleLength1To2048 cloudLegacyExchangeDNField;

		// Token: 0x0400049E RID: 1182
		private DirectoryPropertyInt32Single cloudMSExchArchiveStatusField;

		// Token: 0x0400049F RID: 1183
		private DirectoryPropertyBinarySingleLength1To4000 cloudMSExchBlockedSendersHashField;

		// Token: 0x040004A0 RID: 1184
		private DirectoryPropertyGuidSingle cloudMSExchMailboxGuidField;

		// Token: 0x040004A1 RID: 1185
		private DirectoryPropertyInt32Single cloudMSExchRecipientDisplayTypeField;

		// Token: 0x040004A2 RID: 1186
		private DirectoryPropertyBinarySingleLength1To12000 cloudMSExchSafeRecipientsHashField;

		// Token: 0x040004A3 RID: 1187
		private DirectoryPropertyBinarySingleLength1To32000 cloudMSExchSafeSendersHashField;

		// Token: 0x040004A4 RID: 1188
		private DirectoryPropertyStringLength1To1123 cloudMSExchUCVoiceMailSettingsField;

		// Token: 0x040004A5 RID: 1189
		private DirectoryPropertyStringSingleLength1To454 cloudSipProxyAddressField;

		// Token: 0x040004A6 RID: 1190
		private DirectoryPropertyStringSingleLength1To128 coField;

		// Token: 0x040004A7 RID: 1191
		private DirectoryPropertyStringSingleLength1To64 companyField;

		// Token: 0x040004A8 RID: 1192
		private DirectoryPropertyInt32SingleMin0Max65535 countryCodeField;

		// Token: 0x040004A9 RID: 1193
		private DirectoryPropertyStringSingleLength1To128 defaultGeographyField;

		// Token: 0x040004AA RID: 1194
		private DirectoryPropertyBooleanSingle deliverAndRedirectField;

		// Token: 0x040004AB RID: 1195
		private DirectoryPropertyStringSingleLength1To64 departmentField;

		// Token: 0x040004AC RID: 1196
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040004AD RID: 1197
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x040004AE RID: 1198
		private DirectoryPropertyInt32SingleMin0 dirSyncOverridesField;

		// Token: 0x040004AF RID: 1199
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040004B0 RID: 1200
		private DirectoryPropertyStringSingleLength1To16 employeeIdField;

		// Token: 0x040004B1 RID: 1201
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute1Field;

		// Token: 0x040004B2 RID: 1202
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute10Field;

		// Token: 0x040004B3 RID: 1203
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute11Field;

		// Token: 0x040004B4 RID: 1204
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute12Field;

		// Token: 0x040004B5 RID: 1205
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute13Field;

		// Token: 0x040004B6 RID: 1206
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute14Field;

		// Token: 0x040004B7 RID: 1207
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute15Field;

		// Token: 0x040004B8 RID: 1208
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute2Field;

		// Token: 0x040004B9 RID: 1209
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute3Field;

		// Token: 0x040004BA RID: 1210
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute4Field;

		// Token: 0x040004BB RID: 1211
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute5Field;

		// Token: 0x040004BC RID: 1212
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute6Field;

		// Token: 0x040004BD RID: 1213
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute7Field;

		// Token: 0x040004BE RID: 1214
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute8Field;

		// Token: 0x040004BF RID: 1215
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute9Field;

		// Token: 0x040004C0 RID: 1216
		private DirectoryPropertyStringSingleLength1To64 facsimileTelephoneNumberField;

		// Token: 0x040004C1 RID: 1217
		private DirectoryPropertyStringSingleLength1To64 givenNameField;

		// Token: 0x040004C2 RID: 1218
		private DirectoryPropertyStringSingleLength1To64 homePhoneField;

		// Token: 0x040004C3 RID: 1219
		private DirectoryPropertyStringSingleLength1To1024 infoField;

		// Token: 0x040004C4 RID: 1220
		private DirectoryPropertyStringSingleLength1To6 initialsField;

		// Token: 0x040004C5 RID: 1221
		private DirectoryPropertyInt32Single internetEncodingField;

		// Token: 0x040004C6 RID: 1222
		private DirectoryPropertyStringSingleLength1To64 iPPhoneField;

		// Token: 0x040004C7 RID: 1223
		private DirectoryPropertyStringSingleLength1To128 lField;

		// Token: 0x040004C8 RID: 1224
		private DirectoryPropertyDateTimeSingle lastRestorationTimestampField;

		// Token: 0x040004C9 RID: 1225
		private DirectoryPropertyDateTimeSingle lastDirSyncTimeField;

		// Token: 0x040004CA RID: 1226
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x040004CB RID: 1227
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x040004CC RID: 1228
		private DirectoryPropertyStringSingleLength1To64 middleNameField;

		// Token: 0x040004CD RID: 1229
		private DirectoryPropertyXmlMigrationDetail migrationDetailField;

		// Token: 0x040004CE RID: 1230
		private DirectoryPropertyStringSingleLength1To256 migrationSourceAnchorField;

		// Token: 0x040004CF RID: 1231
		private DirectoryPropertyInt32SingleMin0 migrationStateField;

		// Token: 0x040004D0 RID: 1232
		private DirectoryPropertyStringSingleLength1To64 mobileField;

		// Token: 0x040004D1 RID: 1233
		private DirectoryPropertyInt32Single mSDSHABSeniorityIndexField;

		// Token: 0x040004D2 RID: 1234
		private DirectoryPropertyStringSingleLength1To256 mSDSPhoneticDisplayNameField;

		// Token: 0x040004D3 RID: 1235
		private DirectoryPropertyGuidSingle mSExchArchiveGuidField;

		// Token: 0x040004D4 RID: 1236
		private DirectoryPropertyStringLength1To512 mSExchArchiveNameField;

		// Token: 0x040004D5 RID: 1237
		private DirectoryPropertyStringSingleLength1To256 mSExchAssistantNameField;

		// Token: 0x040004D6 RID: 1238
		private DirectoryPropertyInt32Single mSExchAuditAdminField;

		// Token: 0x040004D7 RID: 1239
		private DirectoryPropertyInt32Single mSExchAuditDelegateField;

		// Token: 0x040004D8 RID: 1240
		private DirectoryPropertyInt32Single mSExchAuditDelegateAdminField;

		// Token: 0x040004D9 RID: 1241
		private DirectoryPropertyInt32Single mSExchAuditOwnerField;

		// Token: 0x040004DA RID: 1242
		private DirectoryPropertyBinarySingleLength1To4000 mSExchBlockedSendersHashField;

		// Token: 0x040004DB RID: 1243
		private DirectoryPropertyBooleanSingle mSExchBypassAuditField;

		// Token: 0x040004DC RID: 1244
		private DirectoryPropertyDateTimeSingle mSExchElcExpirySuspensionEndField;

		// Token: 0x040004DD RID: 1245
		private DirectoryPropertyDateTimeSingle mSExchElcExpirySuspensionStartField;

		// Token: 0x040004DE RID: 1246
		private DirectoryPropertyInt32Single mSExchElcMailboxFlagsField;

		// Token: 0x040004DF RID: 1247
		private DirectoryPropertyBooleanSingle mSExchEnableModerationField;

		// Token: 0x040004E0 RID: 1248
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute1Field;

		// Token: 0x040004E1 RID: 1249
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute2Field;

		// Token: 0x040004E2 RID: 1250
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute3Field;

		// Token: 0x040004E3 RID: 1251
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute4Field;

		// Token: 0x040004E4 RID: 1252
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute5Field;

		// Token: 0x040004E5 RID: 1253
		private DirectoryPropertyBooleanSingle mSExchHideFromAddressListsField;

		// Token: 0x040004E6 RID: 1254
		private DirectoryPropertyStringSingleLength1To256 mSExchImmutableIdField;

		// Token: 0x040004E7 RID: 1255
		private DirectoryPropertyDateTimeSingle mSExchLitigationHoldDateField;

		// Token: 0x040004E8 RID: 1256
		private DirectoryPropertyStringSingleLength1To1024 mSExchLitigationHoldOwnerField;

		// Token: 0x040004E9 RID: 1257
		private DirectoryPropertyBooleanSingle mSExchMailboxAuditEnableField;

		// Token: 0x040004EA RID: 1258
		private DirectoryPropertyInt32Single mSExchMailboxAuditLogAgeLimitField;

		// Token: 0x040004EB RID: 1259
		private DirectoryPropertyGuidSingle mSExchMailboxGuidField;

		// Token: 0x040004EC RID: 1260
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x040004ED RID: 1261
		private DirectoryPropertyInt32Single mSExchRecipientDisplayTypeField;

		// Token: 0x040004EE RID: 1262
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x040004EF RID: 1263
		private DirectoryPropertyInt64Single mSExchRemoteRecipientTypeField;

		// Token: 0x040004F0 RID: 1264
		private DirectoryPropertyBooleanSingle mSExchRequireAuthToSendToField;

		// Token: 0x040004F1 RID: 1265
		private DirectoryPropertyInt32Single mSExchResourceCapacityField;

		// Token: 0x040004F2 RID: 1266
		private DirectoryPropertyStringSingleLength1To1024 mSExchResourceDisplayField;

		// Token: 0x040004F3 RID: 1267
		private DirectoryPropertyStringLength1To1024 mSExchResourceMetadataField;

		// Token: 0x040004F4 RID: 1268
		private DirectoryPropertyStringLength1To1024 mSExchResourceSearchPropertiesField;

		// Token: 0x040004F5 RID: 1269
		private DirectoryPropertyStringSingleLength1To1024 mSExchRetentionCommentField;

		// Token: 0x040004F6 RID: 1270
		private DirectoryPropertyStringSingleLength1To2048 mSExchRetentionUrlField;

		// Token: 0x040004F7 RID: 1271
		private DirectoryPropertyBinarySingleLength1To12000 mSExchSafeRecipientsHashField;

		// Token: 0x040004F8 RID: 1272
		private DirectoryPropertyBinarySingleLength1To32000 mSExchSafeSendersHashField;

		// Token: 0x040004F9 RID: 1273
		private DirectoryPropertyStringLength2To500 mSExchSenderHintTranslationsField;

		// Token: 0x040004FA RID: 1274
		private DirectoryPropertyDateTimeSingle mSExchTeamMailboxExpirationField;

		// Token: 0x040004FB RID: 1275
		private DirectoryPropertyReferenceAddressList mSExchTeamMailboxOwnersField;

		// Token: 0x040004FC RID: 1276
		private DirectoryPropertyReferenceAddressListSingle mSExchTeamMailboxSharePointLinkedByField;

		// Token: 0x040004FD RID: 1277
		private DirectoryPropertyStringSingleLength1To2048 mSExchTeamMailboxSharePointUrlField;

		// Token: 0x040004FE RID: 1278
		private DirectoryPropertyStringLength1To40 mSExchUserHoldPoliciesField;

		// Token: 0x040004FF RID: 1279
		private DirectoryPropertyInt32SingleMin0 mSRtcSipApplicationOptionsField;

		// Token: 0x04000500 RID: 1280
		private DirectoryPropertyStringSingleLength1To256 mSRtcSipDeploymentLocatorField;

		// Token: 0x04000501 RID: 1281
		private DirectoryPropertyStringSingleLength1To500 mSRtcSipLineField;

		// Token: 0x04000502 RID: 1282
		private DirectoryPropertyInt32Single mSRtcSipOptionFlagsField;

		// Token: 0x04000503 RID: 1283
		private DirectoryPropertyStringSingleLength1To512 mSRtcSipOwnerUrnField;

		// Token: 0x04000504 RID: 1284
		private DirectoryPropertyStringSingleLength1To454 mSRtcSipPrimaryUserAddressField;

		// Token: 0x04000505 RID: 1285
		private DirectoryPropertyBooleanSingle mSRtcSipUserEnabledField;

		// Token: 0x04000506 RID: 1286
		private DirectoryPropertyBinarySingleLength1To128 onPremiseSecurityIdentifierField;

		// Token: 0x04000507 RID: 1287
		private DirectoryPropertyStringLength1To64 otherFacsimileTelephoneNumberField;

		// Token: 0x04000508 RID: 1288
		private DirectoryPropertyStringLength1To64 otherHomePhoneField;

		// Token: 0x04000509 RID: 1289
		private DirectoryPropertyStringLength1To512 otherIPPhoneField;

		// Token: 0x0400050A RID: 1290
		private DirectoryPropertyStringLength1To256 otherMailField;

		// Token: 0x0400050B RID: 1291
		private DirectoryPropertyStringLength1To64 otherMobileField;

		// Token: 0x0400050C RID: 1292
		private DirectoryPropertyStringLength1To64 otherPagerField;

		// Token: 0x0400050D RID: 1293
		private DirectoryPropertyStringLength1To64 otherTelephoneField;

		// Token: 0x0400050E RID: 1294
		private DirectoryPropertyInt32SingleMin0 passwordPoliciesField;

		// Token: 0x0400050F RID: 1295
		private DirectoryPropertyStringSingleLength1To64 pagerField;

		// Token: 0x04000510 RID: 1296
		private DirectoryPropertyStringSingleLength1To128 physicalDeliveryOfficeNameField;

		// Token: 0x04000511 RID: 1297
		private DirectoryPropertyXmlAnySingle portalSettingField;

		// Token: 0x04000512 RID: 1298
		private DirectoryPropertyStringSingleLength1To40 postalCodeField;

		// Token: 0x04000513 RID: 1299
		private DirectoryPropertyStringLength1To40 postOfficeBoxField;

		// Token: 0x04000514 RID: 1300
		private DirectoryPropertyStringSingleLength1To64 preferredLanguageField;

		// Token: 0x04000515 RID: 1301
		private DirectoryPropertyXmlProvisionedPlan provisionedPlanField;

		// Token: 0x04000516 RID: 1302
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x04000517 RID: 1303
		private DirectoryPropertyXmlRightsManagementUserKeySingle rightsManagementUserKeyField;

		// Token: 0x04000518 RID: 1304
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04000519 RID: 1305
		private DirectoryPropertyXmlServiceOriginatedResource serviceOriginatedResourceField;

		// Token: 0x0400051A RID: 1306
		private DirectoryPropertyStringSingleLength1To64 shadowAliasField;

		// Token: 0x0400051B RID: 1307
		private DirectoryPropertyStringSingleLength1To64 shadowCommonNameField;

		// Token: 0x0400051C RID: 1308
		private DirectoryPropertyStringSingleLength1To256 shadowDisplayNameField;

		// Token: 0x0400051D RID: 1309
		private DirectoryPropertyStringSingleLength1To2048 shadowLegacyExchangeDNField;

		// Token: 0x0400051E RID: 1310
		private DirectoryPropertyStringSingleLength1To256 shadowMailField;

		// Token: 0x0400051F RID: 1311
		private DirectoryPropertyStringSingleLength1To64 shadowMobileField;

		// Token: 0x04000520 RID: 1312
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x04000521 RID: 1313
		private DirectoryPropertyStringSingleLength1To1123 shadowTargetAddressField;

		// Token: 0x04000522 RID: 1314
		private DirectoryPropertyStringSingleLength1To1024 shadowUserPrincipalNameField;

		// Token: 0x04000523 RID: 1315
		private DirectoryPropertyStringSingleLength1To454 sipProxyAddressField;

		// Token: 0x04000524 RID: 1316
		private DirectoryPropertyStringSingleLength1To64 snField;

		// Token: 0x04000525 RID: 1317
		private DirectoryPropertyDateTimeSingle softDeletionTimestampField;

		// Token: 0x04000526 RID: 1318
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x04000527 RID: 1319
		private DirectoryPropertyStringSingleLength1To128 stField;

		// Token: 0x04000528 RID: 1320
		private DirectoryPropertyStringSingleLength1To1024 streetField;

		// Token: 0x04000529 RID: 1321
		private DirectoryPropertyStringSingleLength1To1024 streetAddressField;

		// Token: 0x0400052A RID: 1322
		private DirectoryPropertyXmlStrongAuthenticationMethod strongAuthenticationMethodField;

		// Token: 0x0400052B RID: 1323
		private DirectoryPropertyTargetAddress targetAddressField;

		// Token: 0x0400052C RID: 1324
		private DirectoryPropertyStringSingleLength1To64 telephoneAssistantField;

		// Token: 0x0400052D RID: 1325
		private DirectoryPropertyStringSingleLength1To64 telephoneNumberField;

		// Token: 0x0400052E RID: 1326
		private DirectoryPropertyBinarySingleLength1To102400 thumbnailPhotoField;

		// Token: 0x0400052F RID: 1327
		private DirectoryPropertyStringSingleLength1To128 titleField;

		// Token: 0x04000530 RID: 1328
		private DirectoryPropertyStringLength1To1123 urlField;

		// Token: 0x04000531 RID: 1329
		private DirectoryPropertyStringSingleLength1To3 usageLocationField;

		// Token: 0x04000532 RID: 1330
		private DirectoryPropertyStringSingleLength1To1024 userPrincipalNameField;

		// Token: 0x04000533 RID: 1331
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x04000534 RID: 1332
		private DirectoryPropertyBinarySingleLength8 windowsLiveNetIdField;

		// Token: 0x04000535 RID: 1333
		private DirectoryPropertyStringSingleLength1To2048 wwwHomepageField;

		// Token: 0x04000536 RID: 1334
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x04000537 RID: 1335
		private XmlAttribute[] anyAttrField;
	}
}
