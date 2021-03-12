using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C6 RID: 1478
	[DataServiceKey("objectId")]
	public class Contact : DirectoryObject
	{
		// Token: 0x06001720 RID: 5920 RVA: 0x0002E874 File Offset: 0x0002CA74
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

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
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

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0002E8D1 File Offset: 0x0002CAD1
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x0002E8D9 File Offset: 0x0002CAD9
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0002E8E2 File Offset: 0x0002CAE2
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x0002E8EA File Offset: 0x0002CAEA
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

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0002E8F3 File Offset: 0x0002CAF3
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x0002E8FB File Offset: 0x0002CAFB
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

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0002E904 File Offset: 0x0002CB04
		// (set) Token: 0x0600172A RID: 5930 RVA: 0x0002E90C File Offset: 0x0002CB0C
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

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0002E915 File Offset: 0x0002CB15
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0002E91D File Offset: 0x0002CB1D
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

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0002E926 File Offset: 0x0002CB26
		// (set) Token: 0x0600172E RID: 5934 RVA: 0x0002E92E File Offset: 0x0002CB2E
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

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0002E937 File Offset: 0x0002CB37
		// (set) Token: 0x06001730 RID: 5936 RVA: 0x0002E93F File Offset: 0x0002CB3F
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

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0002E948 File Offset: 0x0002CB48
		// (set) Token: 0x06001732 RID: 5938 RVA: 0x0002E950 File Offset: 0x0002CB50
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

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0002E959 File Offset: 0x0002CB59
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x0002E961 File Offset: 0x0002CB61
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

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0002E96A File Offset: 0x0002CB6A
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x0002E972 File Offset: 0x0002CB72
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

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0002E97B File Offset: 0x0002CB7B
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x0002E983 File Offset: 0x0002CB83
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

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0002E98C File Offset: 0x0002CB8C
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x0002E994 File Offset: 0x0002CB94
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

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0002E99D File Offset: 0x0002CB9D
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x0002E9A5 File Offset: 0x0002CBA5
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

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x0002E9AE File Offset: 0x0002CBAE
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x0002E9B6 File Offset: 0x0002CBB6
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

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0002E9BF File Offset: 0x0002CBBF
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x0002E9C7 File Offset: 0x0002CBC7
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

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0002E9D0 File Offset: 0x0002CBD0
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x0002E9D8 File Offset: 0x0002CBD8
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0002E9E1 File Offset: 0x0002CBE1
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x0002E9E9 File Offset: 0x0002CBE9
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0002E9F2 File Offset: 0x0002CBF2
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x0002E9FA File Offset: 0x0002CBFA
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

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0002EA03 File Offset: 0x0002CC03
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x0002EA0B File Offset: 0x0002CC0B
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

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0002EA14 File Offset: 0x0002CC14
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x0002EA1C File Offset: 0x0002CC1C
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

		// Token: 0x04001A7D RID: 6781
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x04001A7E RID: 6782
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001A7F RID: 6783
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _department;

		// Token: 0x04001A80 RID: 6784
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001A81 RID: 6785
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001A82 RID: 6786
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _facsimileTelephoneNumber;

		// Token: 0x04001A83 RID: 6787
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _givenName;

		// Token: 0x04001A84 RID: 6788
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _jobTitle;

		// Token: 0x04001A85 RID: 6789
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001A86 RID: 6790
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001A87 RID: 6791
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001A88 RID: 6792
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mobile;

		// Token: 0x04001A89 RID: 6793
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _physicalDeliveryOfficeName;

		// Token: 0x04001A8A RID: 6794
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001A8B RID: 6795
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001A8C RID: 6796
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x04001A8D RID: 6797
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x04001A8E RID: 6798
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _streetAddress;

		// Token: 0x04001A8F RID: 6799
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _surname;

		// Token: 0x04001A90 RID: 6800
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001A91 RID: 6801
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;
	}
}
