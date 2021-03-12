using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F3 RID: 1523
	[DataServiceKey("objectId")]
	public class DirectAccessGrant : DirectoryObject
	{
		// Token: 0x06001A61 RID: 6753 RVA: 0x00031110 File Offset: 0x0002F310
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectAccessGrant CreateDirectAccessGrant(string objectId, Guid permissionId)
		{
			return new DirectAccessGrant
			{
				objectId = objectId,
				permissionId = permissionId
			};
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00031132 File Offset: 0x0002F332
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x0003113A File Offset: 0x0002F33A
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

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x00031143 File Offset: 0x0002F343
		// (set) Token: 0x06001A65 RID: 6757 RVA: 0x0003114B File Offset: 0x0002F34B
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

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x00031154 File Offset: 0x0002F354
		// (set) Token: 0x06001A67 RID: 6759 RVA: 0x0003115C File Offset: 0x0002F35C
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

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00031165 File Offset: 0x0002F365
		// (set) Token: 0x06001A69 RID: 6761 RVA: 0x0003116D File Offset: 0x0002F36D
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

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00031176 File Offset: 0x0002F376
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0003117E File Offset: 0x0002F37E
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

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00031187 File Offset: 0x0002F387
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x0003118F File Offset: 0x0002F38F
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

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x00031198 File Offset: 0x0002F398
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x000311A0 File Offset: 0x0002F3A0
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

		// Token: 0x04001BFF RID: 7167
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _creationTimestamp;

		// Token: 0x04001C00 RID: 7168
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid _permissionId;

		// Token: 0x04001C01 RID: 7169
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalDisplayName;

		// Token: 0x04001C02 RID: 7170
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _principalId;

		// Token: 0x04001C03 RID: 7171
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _principalType;

		// Token: 0x04001C04 RID: 7172
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _resourceDisplayName;

		// Token: 0x04001C05 RID: 7173
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _resourceId;
	}
}
