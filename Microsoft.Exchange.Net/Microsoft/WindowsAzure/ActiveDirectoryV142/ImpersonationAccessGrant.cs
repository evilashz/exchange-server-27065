using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000602 RID: 1538
	[DataServiceKey("objectId")]
	public class ImpersonationAccessGrant
	{
		// Token: 0x06001B6C RID: 7020 RVA: 0x00031F30 File Offset: 0x00030130
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ImpersonationAccessGrant CreateImpersonationAccessGrant(string objectId)
		{
			return new ImpersonationAccessGrant
			{
				objectId = objectId
			};
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00031F4B File Offset: 0x0003014B
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x00031F53 File Offset: 0x00030153
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

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00031F5C File Offset: 0x0003015C
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x00031F64 File Offset: 0x00030164
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

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00031F6D File Offset: 0x0003016D
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x00031F75 File Offset: 0x00030175
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

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00031F7E File Offset: 0x0003017E
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x00031F86 File Offset: 0x00030186
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x00031F8F File Offset: 0x0003018F
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x00031F97 File Offset: 0x00030197
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00031FA0 File Offset: 0x000301A0
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x00031FA8 File Offset: 0x000301A8
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00031FB1 File Offset: 0x000301B1
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x00031FB9 File Offset: 0x000301B9
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00031FC2 File Offset: 0x000301C2
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x00031FCA File Offset: 0x000301CA
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

		// Token: 0x04001C7B RID: 7291
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _clientId;

		// Token: 0x04001C7C RID: 7292
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _consentType;

		// Token: 0x04001C7D RID: 7293
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _expiryTime;

		// Token: 0x04001C7E RID: 7294
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001C7F RID: 7295
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalId;

		// Token: 0x04001C80 RID: 7296
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceId;

		// Token: 0x04001C81 RID: 7297
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _scope;

		// Token: 0x04001C82 RID: 7298
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startTime;
	}
}
