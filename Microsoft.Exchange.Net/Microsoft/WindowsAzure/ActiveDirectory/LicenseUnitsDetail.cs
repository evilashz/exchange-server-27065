using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B6 RID: 1462
	public class LicenseUnitsDetail
	{
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0002DC4F File Offset: 0x0002BE4F
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0002DC57 File Offset: 0x0002BE57
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

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0002DC60 File Offset: 0x0002BE60
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0002DC68 File Offset: 0x0002BE68
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0002DC71 File Offset: 0x0002BE71
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x0002DC79 File Offset: 0x0002BE79
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

		// Token: 0x04001A08 RID: 6664
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _enabled;

		// Token: 0x04001A09 RID: 6665
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _suspended;

		// Token: 0x04001A0A RID: 6666
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _warning;
	}
}
