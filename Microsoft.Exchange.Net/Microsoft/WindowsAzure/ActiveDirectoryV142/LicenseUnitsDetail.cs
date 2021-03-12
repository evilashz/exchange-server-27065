using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000603 RID: 1539
	public class LicenseUnitsDetail
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00031FDB File Offset: 0x000301DB
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x00031FE3 File Offset: 0x000301E3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00031FEC File Offset: 0x000301EC
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x00031FF4 File Offset: 0x000301F4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? suspended
		{
			get
			{
				return this._suspended;
			}
			set
			{
				this._suspended = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00031FFD File Offset: 0x000301FD
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x00032005 File Offset: 0x00030205
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? warning
		{
			get
			{
				return this._warning;
			}
			set
			{
				this._warning = value;
			}
		}

		// Token: 0x04001C83 RID: 7299
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _enabled;

		// Token: 0x04001C84 RID: 7300
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _suspended;

		// Token: 0x04001C85 RID: 7301
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _warning;
	}
}
