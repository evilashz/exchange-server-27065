using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C7 RID: 1479
	public class AlternativeSecurityId
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0002EA43 File Offset: 0x0002CC43
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x0002EA4B File Offset: 0x0002CC4B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0002EA54 File Offset: 0x0002CC54
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x0002EA5C File Offset: 0x0002CC5C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string identityProvider
		{
			get
			{
				return this._identityProvider;
			}
			set
			{
				this._identityProvider = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0002EA65 File Offset: 0x0002CC65
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x0002EA81 File Offset: 0x0002CC81
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public byte[] key
		{
			get
			{
				if (this._key != null)
				{
					return (byte[])this._key.Clone();
				}
				return null;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x04001A92 RID: 6802
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _type;

		// Token: 0x04001A93 RID: 6803
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _identityProvider;

		// Token: 0x04001A94 RID: 6804
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _key;
	}
}
