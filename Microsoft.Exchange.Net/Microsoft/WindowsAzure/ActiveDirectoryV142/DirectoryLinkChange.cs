using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F2 RID: 1522
	[DataServiceKey("objectId")]
	public class DirectoryLinkChange : DirectoryObject
	{
		// Token: 0x06001A51 RID: 6737 RVA: 0x00031074 File Offset: 0x0002F274
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectoryLinkChange CreateDirectoryLinkChange(string objectId)
		{
			return new DirectoryLinkChange
			{
				objectId = objectId
			};
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0003108F File Offset: 0x0002F28F
		// (set) Token: 0x06001A53 RID: 6739 RVA: 0x00031097 File Offset: 0x0002F297
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string associationType
		{
			get
			{
				return this._associationType;
			}
			set
			{
				this._associationType = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000310A0 File Offset: 0x0002F2A0
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x000310A8 File Offset: 0x0002F2A8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string sourceObjectId
		{
			get
			{
				return this._sourceObjectId;
			}
			set
			{
				this._sourceObjectId = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000310B1 File Offset: 0x0002F2B1
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x000310B9 File Offset: 0x0002F2B9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string sourceObjectType
		{
			get
			{
				return this._sourceObjectType;
			}
			set
			{
				this._sourceObjectType = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000310C2 File Offset: 0x0002F2C2
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x000310CA File Offset: 0x0002F2CA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string sourceObjectUri
		{
			get
			{
				return this._sourceObjectUri;
			}
			set
			{
				this._sourceObjectUri = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000310D3 File Offset: 0x0002F2D3
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x000310DB File Offset: 0x0002F2DB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string targetObjectId
		{
			get
			{
				return this._targetObjectId;
			}
			set
			{
				this._targetObjectId = value;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x000310E4 File Offset: 0x0002F2E4
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x000310EC File Offset: 0x0002F2EC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string targetObjectType
		{
			get
			{
				return this._targetObjectType;
			}
			set
			{
				this._targetObjectType = value;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x000310F5 File Offset: 0x0002F2F5
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x000310FD File Offset: 0x0002F2FD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string targetObjectUri
		{
			get
			{
				return this._targetObjectUri;
			}
			set
			{
				this._targetObjectUri = value;
			}
		}

		// Token: 0x04001BF8 RID: 7160
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _associationType;

		// Token: 0x04001BF9 RID: 7161
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectId;

		// Token: 0x04001BFA RID: 7162
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectType;

		// Token: 0x04001BFB RID: 7163
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectUri;

		// Token: 0x04001BFC RID: 7164
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectId;

		// Token: 0x04001BFD RID: 7165
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectType;

		// Token: 0x04001BFE RID: 7166
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectUri;
	}
}
