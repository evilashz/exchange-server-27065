using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005AC RID: 1452
	[DataServiceKey("objectId")]
	public class Role : DirectoryObject
	{
		// Token: 0x06001569 RID: 5481 RVA: 0x0002D2BC File Offset: 0x0002B4BC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Role CreateRole(string objectId)
		{
			return new Role
			{
				objectId = objectId
			};
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0002D2D7 File Offset: 0x0002B4D7
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x0002D2DF File Offset: 0x0002B4DF
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

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x0002D2F0 File Offset: 0x0002B4F0
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

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0002D2F9 File Offset: 0x0002B4F9
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x0002D301 File Offset: 0x0002B501
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? isSystem
		{
			get
			{
				return this._isSystem;
			}
			set
			{
				this._isSystem = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0002D30A File Offset: 0x0002B50A
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x0002D312 File Offset: 0x0002B512
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? roleDisabled
		{
			get
			{
				return this._roleDisabled;
			}
			set
			{
				this._roleDisabled = value;
			}
		}

		// Token: 0x040019B3 RID: 6579
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x040019B4 RID: 6580
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x040019B5 RID: 6581
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isSystem;

		// Token: 0x040019B6 RID: 6582
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _roleDisabled;
	}
}
