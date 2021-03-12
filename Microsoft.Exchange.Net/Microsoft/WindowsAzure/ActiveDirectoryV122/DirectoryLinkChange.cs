using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005CA RID: 1482
	[DataServiceKey("objectId")]
	public class DirectoryLinkChange : DirectoryObject
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x0002ECEC File Offset: 0x0002CEEC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectoryLinkChange CreateDirectoryLinkChange(string objectId)
		{
			return new DirectoryLinkChange
			{
				objectId = objectId
			};
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0002ED07 File Offset: 0x0002CF07
		// (set) Token: 0x06001781 RID: 6017 RVA: 0x0002ED0F File Offset: 0x0002CF0F
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

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0002ED18 File Offset: 0x0002CF18
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x0002ED20 File Offset: 0x0002CF20
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

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0002ED29 File Offset: 0x0002CF29
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x0002ED31 File Offset: 0x0002CF31
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

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0002ED3A File Offset: 0x0002CF3A
		// (set) Token: 0x06001787 RID: 6023 RVA: 0x0002ED42 File Offset: 0x0002CF42
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

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0002ED4B File Offset: 0x0002CF4B
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x0002ED53 File Offset: 0x0002CF53
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

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0002ED5C File Offset: 0x0002CF5C
		// (set) Token: 0x0600178B RID: 6027 RVA: 0x0002ED64 File Offset: 0x0002CF64
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

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0002ED6D File Offset: 0x0002CF6D
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x0002ED75 File Offset: 0x0002CF75
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

		// Token: 0x04001AA9 RID: 6825
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _associationType;

		// Token: 0x04001AAA RID: 6826
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectId;

		// Token: 0x04001AAB RID: 6827
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectType;

		// Token: 0x04001AAC RID: 6828
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectUri;

		// Token: 0x04001AAD RID: 6829
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectId;

		// Token: 0x04001AAE RID: 6830
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectType;

		// Token: 0x04001AAF RID: 6831
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectUri;
	}
}
