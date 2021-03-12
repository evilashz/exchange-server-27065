using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F7 RID: 1527
	[DataServiceKey("objectId")]
	public class Role : DirectoryObject
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x00031534 File Offset: 0x0002F734
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Role CreateRole(string objectId)
		{
			return new Role
			{
				objectId = objectId
			};
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0003154F File Offset: 0x0002F74F
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x00031557 File Offset: 0x0002F757
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

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00031560 File Offset: 0x0002F760
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x00031568 File Offset: 0x0002F768
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

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00031571 File Offset: 0x0002F771
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00031579 File Offset: 0x0002F779
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

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00031582 File Offset: 0x0002F782
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0003158A File Offset: 0x0002F78A
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

		// Token: 0x04001C23 RID: 7203
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001C24 RID: 7204
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C25 RID: 7205
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isSystem;

		// Token: 0x04001C26 RID: 7206
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _roleDisabled;
	}
}
