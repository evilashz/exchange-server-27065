using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A9 RID: 1449
	[DataServiceKey("objectId")]
	public class DirectoryLinkChange : DirectoryObject
	{
		// Token: 0x06001523 RID: 5411 RVA: 0x0002CF60 File Offset: 0x0002B160
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DirectoryLinkChange CreateDirectoryLinkChange(string objectId)
		{
			return new DirectoryLinkChange
			{
				objectId = objectId
			};
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0002CF7B File Offset: 0x0002B17B
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x0002CF83 File Offset: 0x0002B183
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

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0002CF8C File Offset: 0x0002B18C
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x0002CF94 File Offset: 0x0002B194
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

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0002CF9D File Offset: 0x0002B19D
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x0002CFA5 File Offset: 0x0002B1A5
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0002CFAE File Offset: 0x0002B1AE
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x0002CFB6 File Offset: 0x0002B1B6
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

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0002CFBF File Offset: 0x0002B1BF
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x0002CFC7 File Offset: 0x0002B1C7
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

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
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

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0002CFE1 File Offset: 0x0002B1E1
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x0002CFE9 File Offset: 0x0002B1E9
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

		// Token: 0x04001993 RID: 6547
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _associationType;

		// Token: 0x04001994 RID: 6548
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectId;

		// Token: 0x04001995 RID: 6549
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectType;

		// Token: 0x04001996 RID: 6550
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _sourceObjectUri;

		// Token: 0x04001997 RID: 6551
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectId;

		// Token: 0x04001998 RID: 6552
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectType;

		// Token: 0x04001999 RID: 6553
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _targetObjectUri;
	}
}
