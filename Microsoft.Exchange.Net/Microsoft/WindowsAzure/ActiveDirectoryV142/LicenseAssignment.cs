using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F4 RID: 1524
	public class LicenseAssignment
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x000311B1 File Offset: 0x0002F3B1
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x000311B9 File Offset: 0x0002F3B9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? accountId
		{
			get
			{
				return this._accountId;
			}
			set
			{
				this._accountId = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000311C2 File Offset: 0x0002F3C2
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x000311CA File Offset: 0x0002F3CA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? skuId
		{
			get
			{
				return this._skuId;
			}
			set
			{
				this._skuId = value;
			}
		}

		// Token: 0x04001C06 RID: 7174
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _accountId;

		// Token: 0x04001C07 RID: 7175
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _skuId;
	}
}
