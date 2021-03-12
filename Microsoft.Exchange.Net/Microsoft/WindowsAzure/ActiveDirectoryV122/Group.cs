using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005CB RID: 1483
	[DataServiceKey("objectId")]
	public class Group : DirectoryObject
	{
		// Token: 0x0600178F RID: 6031 RVA: 0x0002ED88 File Offset: 0x0002CF88
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Group CreateGroup(string objectId, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses)
		{
			Group group = new Group();
			group.objectId = objectId;
			if (provisioningErrors == null)
			{
				throw new ArgumentNullException("provisioningErrors");
			}
			group.provisioningErrors = provisioningErrors;
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			group.proxyAddresses = proxyAddresses;
			return group;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0002EDCD File Offset: 0x0002CFCD
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x0002EDD5 File Offset: 0x0002CFD5
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0002EDDE File Offset: 0x0002CFDE
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0002EDE6 File Offset: 0x0002CFE6
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

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0002EDEF File Offset: 0x0002CFEF
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x0002EDF7 File Offset: 0x0002CFF7
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

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0002EE00 File Offset: 0x0002D000
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x0002EE08 File Offset: 0x0002D008
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

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0002EE11 File Offset: 0x0002D011
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x0002EE19 File Offset: 0x0002D019
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

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0002EE22 File Offset: 0x0002D022
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x0002EE2A File Offset: 0x0002D02A
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

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0002EE33 File Offset: 0x0002D033
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x0002EE3B File Offset: 0x0002D03B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? mailEnabled
		{
			get
			{
				return this._mailEnabled;
			}
			set
			{
				this._mailEnabled = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0002EE44 File Offset: 0x0002D044
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x0002EE4C File Offset: 0x0002D04C
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

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0002EE55 File Offset: 0x0002D055
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x0002EE5D File Offset: 0x0002D05D
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

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0002EE66 File Offset: 0x0002D066
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x0002EE6E File Offset: 0x0002D06E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? securityEnabled
		{
			get
			{
				return this._securityEnabled;
			}
			set
			{
				this._securityEnabled = value;
			}
		}

		// Token: 0x04001AB0 RID: 6832
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001AB1 RID: 6833
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001AB2 RID: 6834
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001AB3 RID: 6835
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001AB4 RID: 6836
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001AB5 RID: 6837
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001AB6 RID: 6838
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _mailEnabled;

		// Token: 0x04001AB7 RID: 6839
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001AB8 RID: 6840
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x04001AB9 RID: 6841
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _securityEnabled;
	}
}
