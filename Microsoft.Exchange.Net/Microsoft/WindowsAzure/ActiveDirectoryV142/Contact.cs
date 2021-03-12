using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005EF RID: 1519
	[DataServiceKey("objectId")]
	public class Contact : DirectoryObject
	{
		// Token: 0x060019FD RID: 6653 RVA: 0x00030C90 File Offset: 0x0002EE90
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

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x00030CDC File Offset: 0x0002EEDC
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x00030CE4 File Offset: 0x0002EEE4
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

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x00030CED File Offset: 0x0002EEED
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x00030CF5 File Offset: 0x0002EEF5
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

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00030CFE File Offset: 0x0002EEFE
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x00030D06 File Offset: 0x0002EF06
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

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00030D0F File Offset: 0x0002EF0F
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x00030D17 File Offset: 0x0002EF17
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

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00030D20 File Offset: 0x0002EF20
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x00030D28 File Offset: 0x0002EF28
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

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x00030D31 File Offset: 0x0002EF31
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x00030D39 File Offset: 0x0002EF39
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

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00030D42 File Offset: 0x0002EF42
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x00030D4A File Offset: 0x0002EF4A
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

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00030D53 File Offset: 0x0002EF53
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x00030D5B File Offset: 0x0002EF5B
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

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00030D64 File Offset: 0x0002EF64
		// (set) Token: 0x06001A0F RID: 6671 RVA: 0x00030D6C File Offset: 0x0002EF6C
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

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00030D75 File Offset: 0x0002EF75
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x00030D7D File Offset: 0x0002EF7D
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

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00030D86 File Offset: 0x0002EF86
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x00030D8E File Offset: 0x0002EF8E
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

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00030D97 File Offset: 0x0002EF97
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x00030D9F File Offset: 0x0002EF9F
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

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x00030DA8 File Offset: 0x0002EFA8
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00030DB0 File Offset: 0x0002EFB0
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

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00030DB9 File Offset: 0x0002EFB9
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x00030DC1 File Offset: 0x0002EFC1
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

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00030DCA File Offset: 0x0002EFCA
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x00030DD2 File Offset: 0x0002EFD2
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

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00030DDB File Offset: 0x0002EFDB
		// (set) Token: 0x06001A1D RID: 6685 RVA: 0x00030DE3 File Offset: 0x0002EFE3
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

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001A1E RID: 6686 RVA: 0x00030DEC File Offset: 0x0002EFEC
		// (set) Token: 0x06001A1F RID: 6687 RVA: 0x00030DF4 File Offset: 0x0002EFF4
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

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001A20 RID: 6688 RVA: 0x00030DFD File Offset: 0x0002EFFD
		// (set) Token: 0x06001A21 RID: 6689 RVA: 0x00030E05 File Offset: 0x0002F005
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

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00030E0E File Offset: 0x0002F00E
		// (set) Token: 0x06001A23 RID: 6691 RVA: 0x00030E16 File Offset: 0x0002F016
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

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x00030E1F File Offset: 0x0002F01F
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x00030E27 File Offset: 0x0002F027
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

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00030E30 File Offset: 0x0002F030
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x00030E38 File Offset: 0x0002F038
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

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00030E41 File Offset: 0x0002F041
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x00030E49 File Offset: 0x0002F049
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

		// Token: 0x04001BD1 RID: 7121
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _city;

		// Token: 0x04001BD2 RID: 7122
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _country;

		// Token: 0x04001BD3 RID: 7123
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _department;

		// Token: 0x04001BD4 RID: 7124
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001BD5 RID: 7125
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001BD6 RID: 7126
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _facsimileTelephoneNumber;

		// Token: 0x04001BD7 RID: 7127
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _givenName;

		// Token: 0x04001BD8 RID: 7128
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _jobTitle;

		// Token: 0x04001BD9 RID: 7129
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001BDA RID: 7130
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001BDB RID: 7131
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001BDC RID: 7132
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mobile;

		// Token: 0x04001BDD RID: 7133
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _physicalDeliveryOfficeName;

		// Token: 0x04001BDE RID: 7134
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _postalCode;

		// Token: 0x04001BDF RID: 7135
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001BE0 RID: 7136
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x04001BE1 RID: 7137
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sipProxyAddress;

		// Token: 0x04001BE2 RID: 7138
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _state;

		// Token: 0x04001BE3 RID: 7139
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _streetAddress;

		// Token: 0x04001BE4 RID: 7140
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _surname;

		// Token: 0x04001BE5 RID: 7141
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _telephoneNumber;

		// Token: 0x04001BE6 RID: 7142
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;
	}
}
