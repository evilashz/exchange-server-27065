using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005CD RID: 1485
	[DataServiceKey("objectId")]
	public class RoleTemplate : DirectoryObject
	{
		// Token: 0x060017AF RID: 6063 RVA: 0x0002EF00 File Offset: 0x0002D100
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static RoleTemplate CreateRoleTemplate(string objectId)
		{
			return new RoleTemplate
			{
				objectId = objectId
			};
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0002EF1B File Offset: 0x0002D11B
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x0002EF23 File Offset: 0x0002D123
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0002EF2C File Offset: 0x0002D12C
		// (set) Token: 0x060017B3 RID: 6067 RVA: 0x0002EF34 File Offset: 0x0002D134
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

		// Token: 0x04001ABE RID: 6846
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001ABF RID: 6847
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;
	}
}
