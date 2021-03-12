using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F8 RID: 1528
	[DataServiceKey("objectId")]
	public class RoleTemplate : DirectoryObject
	{
		// Token: 0x06001AB8 RID: 6840 RVA: 0x0003159C File Offset: 0x0002F79C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static RoleTemplate CreateRoleTemplate(string objectId)
		{
			return new RoleTemplate
			{
				objectId = objectId
			};
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000315B7 File Offset: 0x0002F7B7
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x000315BF File Offset: 0x0002F7BF
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

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x000315C8 File Offset: 0x0002F7C8
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x000315D0 File Offset: 0x0002F7D0
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

		// Token: 0x04001C27 RID: 7207
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001C28 RID: 7208
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;
	}
}
