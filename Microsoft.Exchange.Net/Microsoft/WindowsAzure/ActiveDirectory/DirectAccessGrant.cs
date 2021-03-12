using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005AA RID: 1450
	[DataServiceKey("objectId")]
	public class DirectAccessGrant : DirectoryObject
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectAccessGrant CreateDirectAccessGrant(string objectId, Guid permissionId)
		{
			return new DirectAccessGrant
			{
				objectId = objectId,
				permissionId = permissionId
			};
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0002D01E File Offset: 0x0002B21E
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0002D026 File Offset: 0x0002B226
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? creationTimestamp
		{
			get
			{
				return this._creationTimestamp;
			}
			set
			{
				this._creationTimestamp = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0002D02F File Offset: 0x0002B22F
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0002D037 File Offset: 0x0002B237
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid permissionId
		{
			get
			{
				return this._permissionId;
			}
			set
			{
				this._permissionId = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0002D040 File Offset: 0x0002B240
		// (set) Token: 0x06001539 RID: 5433 RVA: 0x0002D048 File Offset: 0x0002B248
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string principalDisplayName
		{
			get
			{
				return this._principalDisplayName;
			}
			set
			{
				this._principalDisplayName = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0002D051 File Offset: 0x0002B251
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x0002D059 File Offset: 0x0002B259
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? principalId
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

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0002D062 File Offset: 0x0002B262
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x0002D06A File Offset: 0x0002B26A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string principalType
		{
			get
			{
				return this._principalType;
			}
			set
			{
				this._principalType = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0002D073 File Offset: 0x0002B273
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0002D07B File Offset: 0x0002B27B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string resourceDisplayName
		{
			get
			{
				return this._resourceDisplayName;
			}
			set
			{
				this._resourceDisplayName = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0002D084 File Offset: 0x0002B284
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x0002D08C File Offset: 0x0002B28C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? resourceId
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

		// Token: 0x0400199A RID: 6554
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _creationTimestamp;

		// Token: 0x0400199B RID: 6555
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _permissionId;

		// Token: 0x0400199C RID: 6556
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalDisplayName;

		// Token: 0x0400199D RID: 6557
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _principalId;

		// Token: 0x0400199E RID: 6558
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalType;

		// Token: 0x0400199F RID: 6559
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceDisplayName;

		// Token: 0x040019A0 RID: 6560
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _resourceId;
	}
}
