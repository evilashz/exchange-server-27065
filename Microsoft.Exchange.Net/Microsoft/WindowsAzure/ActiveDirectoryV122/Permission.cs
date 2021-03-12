using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005D2 RID: 1490
	[DataServiceKey("objectId")]
	public class Permission
	{
		// Token: 0x06001812 RID: 6162 RVA: 0x0002F470 File Offset: 0x0002D670
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Permission CreatePermission(string objectId)
		{
			return new Permission
			{
				objectId = objectId
			};
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0002F48B File Offset: 0x0002D68B
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x0002F493 File Offset: 0x0002D693
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string clientId
		{
			get
			{
				return this._clientId;
			}
			set
			{
				this._clientId = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0002F49C File Offset: 0x0002D69C
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x0002F4A4 File Offset: 0x0002D6A4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string consentType
		{
			get
			{
				return this._consentType;
			}
			set
			{
				this._consentType = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0002F4AD File Offset: 0x0002D6AD
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x0002F4B5 File Offset: 0x0002D6B5
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? expiryTime
		{
			get
			{
				return this._expiryTime;
			}
			set
			{
				this._expiryTime = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0002F4BE File Offset: 0x0002D6BE
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0002F4C6 File Offset: 0x0002D6C6
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string objectId
		{
			get
			{
				return this._objectId;
			}
			set
			{
				this._objectId = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0002F4CF File Offset: 0x0002D6CF
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0002F4D7 File Offset: 0x0002D6D7
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string principalId
		{
			get
			{
				return this._principalId;
			}
			set
			{
				this._principalId = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string resourceId
		{
			get
			{
				return this._resourceId;
			}
			set
			{
				this._resourceId = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0002F4F1 File Offset: 0x0002D6F1
		// (set) Token: 0x06001820 RID: 6176 RVA: 0x0002F4F9 File Offset: 0x0002D6F9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string scope
		{
			get
			{
				return this._scope;
			}
			set
			{
				this._scope = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0002F502 File Offset: 0x0002D702
		// (set) Token: 0x06001822 RID: 6178 RVA: 0x0002F50A File Offset: 0x0002D70A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		// Token: 0x04001AEC RID: 6892
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _clientId;

		// Token: 0x04001AED RID: 6893
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _consentType;

		// Token: 0x04001AEE RID: 6894
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _expiryTime;

		// Token: 0x04001AEF RID: 6895
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001AF0 RID: 6896
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalId;

		// Token: 0x04001AF1 RID: 6897
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceId;

		// Token: 0x04001AF2 RID: 6898
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _scope;

		// Token: 0x04001AF3 RID: 6899
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startTime;
	}
}
