using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B5 RID: 1461
	[DataServiceKey("objectId")]
	public class ImpersonationAccessGrant
	{
		// Token: 0x06001610 RID: 5648 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ImpersonationAccessGrant CreateImpersonationAccessGrant(string objectId)
		{
			return new ImpersonationAccessGrant
			{
				objectId = objectId
			};
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0002DBBF File Offset: 0x0002BDBF
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x0002DBC7 File Offset: 0x0002BDC7
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

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0002DBD0 File Offset: 0x0002BDD0
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
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

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0002DBE1 File Offset: 0x0002BDE1
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x0002DBE9 File Offset: 0x0002BDE9
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

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0002DBF2 File Offset: 0x0002BDF2
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x0002DBFA File Offset: 0x0002BDFA
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

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0002DC03 File Offset: 0x0002BE03
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x0002DC0B File Offset: 0x0002BE0B
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

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x0002DC14 File Offset: 0x0002BE14
		// (set) Token: 0x0600161C RID: 5660 RVA: 0x0002DC1C File Offset: 0x0002BE1C
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

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0002DC25 File Offset: 0x0002BE25
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x0002DC2D File Offset: 0x0002BE2D
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

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x0002DC36 File Offset: 0x0002BE36
		// (set) Token: 0x06001620 RID: 5664 RVA: 0x0002DC3E File Offset: 0x0002BE3E
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

		// Token: 0x04001A00 RID: 6656
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _clientId;

		// Token: 0x04001A01 RID: 6657
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _consentType;

		// Token: 0x04001A02 RID: 6658
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _expiryTime;

		// Token: 0x04001A03 RID: 6659
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _objectId;

		// Token: 0x04001A04 RID: 6660
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalId;

		// Token: 0x04001A05 RID: 6661
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceId;

		// Token: 0x04001A06 RID: 6662
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _scope;

		// Token: 0x04001A07 RID: 6663
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startTime;
	}
}
