using System;
using System.CodeDom.Compiler;
using System.Data.Services.Client;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005FC RID: 1532
	[DataServiceKey("objectId")]
	public class StubDirectoryObject : DirectoryObject
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x00031A38 File Offset: 0x0002FC38
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static StubDirectoryObject CreateStubDirectoryObject(string objectId, DataServiceStreamLink thumbnailPhoto)
		{
			return new StubDirectoryObject
			{
				objectId = objectId,
				thumbnailPhoto = thumbnailPhoto
			};
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00031A5A File Offset: 0x0002FC5A
		// (set) Token: 0x06001B08 RID: 6920 RVA: 0x00031A62 File Offset: 0x0002FC62
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

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00031A6B File Offset: 0x0002FC6B
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x00031A73 File Offset: 0x0002FC73
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mail
		{
			get
			{
				return this._mail;
			}
			set
			{
				this._mail = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00031A7C File Offset: 0x0002FC7C
		// (set) Token: 0x06001B0C RID: 6924 RVA: 0x00031A84 File Offset: 0x0002FC84
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DataServiceStreamLink thumbnailPhoto
		{
			get
			{
				return this._thumbnailPhoto;
			}
			set
			{
				this._thumbnailPhoto = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00031A8D File Offset: 0x0002FC8D
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x00031A95 File Offset: 0x0002FC95
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string userPrincipalName
		{
			get
			{
				return this._userPrincipalName;
			}
			set
			{
				this._userPrincipalName = value;
			}
		}

		// Token: 0x04001C4C RID: 7244
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C4D RID: 7245
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001C4E RID: 7246
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DataServiceStreamLink _thumbnailPhoto;

		// Token: 0x04001C4F RID: 7247
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _userPrincipalName;
	}
}
