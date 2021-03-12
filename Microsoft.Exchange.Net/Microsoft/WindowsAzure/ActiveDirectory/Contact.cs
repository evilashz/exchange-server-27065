using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A6 RID: 1446
	[DataServiceKey("objectId")]
	public class Contact : DirectoryObject
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x0002CB7C File Offset: 0x0002AD7C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Contact CreateContact(string objectId, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses, DataServiceStreamLink thumbnailPhoto)
		{
			Contact contact = new Contact();
			contact.objectId = objectId;
			if (provisioningErrors == null)
			{
				throw new ArgumentNullException("provisioningErrors");
			}
			contact.provisioningErrors = provisioningErrors;
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			contact.proxyAddresses = proxyAddresses;
			contact.thumbnailPhoto = thumbnailPhoto;
			return contact;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
		// (set) Token: 0x060014D1 RID: 5329 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
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

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0002CBD9 File Offset: 0x0002ADD9
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x0002CBE1 File Offset: 0x0002ADE1
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

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x0002CBEA File Offset: 0x0002ADEA
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x0002CBF2 File Offset: 0x0002ADF2
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

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0002CBFB File Offset: 0x0002ADFB
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x0002CC03 File Offset: 0x0002AE03
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

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0002CC0C File Offset: 0x0002AE0C
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x0002CC14 File Offset: 0x0002AE14
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0002CC1D File Offset: 0x0002AE1D
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x0002CC25 File Offset: 0x0002AE25
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x0002CC2E File Offset: 0x0002AE2E
		// (set) Token: 0x060014DD RID: 5341 RVA: 0x0002CC36 File Offset: 0x0002AE36
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0002CC3F File Offset: 0x0002AE3F
		// (set) Token: 0x060014DF RID: 5343 RVA: 0x0002CC47 File Offset: 0x0002AE47
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x0002CC50 File Offset: 0x0002AE50
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0002CC58 File Offset: 0x0002AE58
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

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0002CC61 File Offset: 0x0002AE61
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x0002CC69 File Offset: 0x0002AE69
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

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0002CC72 File Offset: 0x0002AE72
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0002CC7A File Offset: 0x0002AE7A
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0002CC83 File Offset: 0x0002AE83
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0002CC8B File Offset: 0x0002AE8B
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

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0002CC94 File Offset: 0x0002AE94
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0002CC9C File Offset: 0x0002AE9C
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

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0002CCA5 File Offset: 0x0002AEA5
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0002CCAD File Offset: 0x0002AEAD
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

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x0002CCB6 File Offset: 0x0002AEB6
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x0002CCBE File Offset: 0x0002AEBE
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

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x0002CCC7 File Offset: 0x0002AEC7
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x0002CCCF File Offset: 0x0002AECF
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

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
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

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0002CCE9 File Offset: 0x0002AEE9
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0002CCF1 File Offset: 0x0002AEF1
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

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0002CCFA File Offset: 0x0002AEFA
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0002CD02 File Offset: 0x0002AF02
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

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0002CD0B File Offset: 0x0002AF0B
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0002CD13 File Offset: 0x0002AF13
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0002CD1C File Offset: 0x0002AF1C
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0002CD24 File Offset: 0x0002AF24
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

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0002CD2D File Offset: 0x0002AF2D
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0002CD35 File Offset: 0x0002AF35
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

		// Token: 0x0400196C RID: 6508
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x0400196D RID: 6509
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x0400196E RID: 6510
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _department;

		// Token: 0x0400196F RID: 6511
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001970 RID: 6512
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001971 RID: 6513
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _facsimileTelephoneNumber;

		// Token: 0x04001972 RID: 6514
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _givenName;

		// Token: 0x04001973 RID: 6515
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _jobTitle;

		// Token: 0x04001974 RID: 6516
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001975 RID: 6517
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001976 RID: 6518
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001977 RID: 6519
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mobile;

		// Token: 0x04001978 RID: 6520
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _physicalDeliveryOfficeName;

		// Token: 0x04001979 RID: 6521
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x0400197A RID: 6522
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x0400197B RID: 6523
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x0400197C RID: 6524
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sipProxyAddress;

		// Token: 0x0400197D RID: 6525
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x0400197E RID: 6526
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _streetAddress;

		// Token: 0x0400197F RID: 6527
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _surname;

		// Token: 0x04001980 RID: 6528
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001981 RID: 6529
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;
	}
}
