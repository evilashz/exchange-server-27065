using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x0200059B RID: 1435
	public class AlternativeSecurityId
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0002C0EB File Offset: 0x0002A2EB
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x0002C0F3 File Offset: 0x0002A2F3
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0002C0FC File Offset: 0x0002A2FC
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x0002C104 File Offset: 0x0002A304
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0002C10D File Offset: 0x0002A30D
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x0002C129 File Offset: 0x0002A329
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

		// Token: 0x04001904 RID: 6404
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _type;

		// Token: 0x04001905 RID: 6405
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _identityProvider;

		// Token: 0x04001906 RID: 6406
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _key;
	}
}
