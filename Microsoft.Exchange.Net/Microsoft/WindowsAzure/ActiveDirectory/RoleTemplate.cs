using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005AD RID: 1453
	[DataServiceKey("objectId")]
	public class RoleTemplate : DirectoryObject
	{
		// Token: 0x06001573 RID: 5491 RVA: 0x0002D324 File Offset: 0x0002B524
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static RoleTemplate CreateRoleTemplate(string objectId)
		{
			return new RoleTemplate
			{
				objectId = objectId
			};
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0002D33F File Offset: 0x0002B53F
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x0002D347 File Offset: 0x0002B547
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

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0002D350 File Offset: 0x0002B550
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x0002D358 File Offset: 0x0002B558
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

		// Token: 0x040019B7 RID: 6583
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x040019B8 RID: 6584
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;
	}
}
