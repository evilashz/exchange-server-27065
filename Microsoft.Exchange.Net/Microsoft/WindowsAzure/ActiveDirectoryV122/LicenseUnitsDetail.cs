using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005D3 RID: 1491
	public class LicenseUnitsDetail
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0002F51B File Offset: 0x0002D71B
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x0002F523 File Offset: 0x0002D723
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

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0002F52C File Offset: 0x0002D72C
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0002F534 File Offset: 0x0002D734
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

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0002F53D File Offset: 0x0002D73D
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0002F545 File Offset: 0x0002D745
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

		// Token: 0x04001AF4 RID: 6900
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _enabled;

		// Token: 0x04001AF5 RID: 6901
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _suspended;

		// Token: 0x04001AF6 RID: 6902
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _warning;
	}
}
