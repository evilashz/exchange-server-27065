using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E0 RID: 1504
	public class AlternativeSecurityId
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0002FE91 File Offset: 0x0002E091
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x0002FE99 File Offset: 0x0002E099
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

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0002FEA2 File Offset: 0x0002E0A2
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x0002FEAA File Offset: 0x0002E0AA
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

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0002FEB3 File Offset: 0x0002E0B3
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x0002FECF File Offset: 0x0002E0CF
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

		// Token: 0x04001B46 RID: 6982
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _type;

		// Token: 0x04001B47 RID: 6983
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _identityProvider;

		// Token: 0x04001B48 RID: 6984
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _key;
	}
}
