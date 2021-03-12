using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005CC RID: 1484
	[DataServiceKey("objectId")]
	public class Role : DirectoryObject
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x0002EE98 File Offset: 0x0002D098
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Role CreateRole(string objectId)
		{
			return new Role
			{
				objectId = objectId
			};
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0002EEB3 File Offset: 0x0002D0B3
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x0002EEBB File Offset: 0x0002D0BB
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

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0002EEC4 File Offset: 0x0002D0C4
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x0002EECC File Offset: 0x0002D0CC
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

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0002EED5 File Offset: 0x0002D0D5
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x0002EEDD File Offset: 0x0002D0DD
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

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0002EEE6 File Offset: 0x0002D0E6
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x0002EEEE File Offset: 0x0002D0EE
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

		// Token: 0x04001ABA RID: 6842
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001ABB RID: 6843
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001ABC RID: 6844
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isSystem;

		// Token: 0x04001ABD RID: 6845
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _roleDisabled;
	}
}
