using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000596 RID: 1430
	public class KeyCredential
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0002BC43 File Offset: 0x00029E43
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0002BC5F File Offset: 0x00029E5F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public byte[] customKeyIdentifier
		{
			get
			{
				if (this._customKeyIdentifier != null)
				{
					return (byte[])this._customKeyIdentifier.Clone();
				}
				return null;
			}
			set
			{
				this._customKeyIdentifier = value;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0002BC68 File Offset: 0x00029E68
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x0002BC70 File Offset: 0x00029E70
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? endDate
		{
			get
			{
				return this._endDate;
			}
			set
			{
				this._endDate = value;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0002BC79 File Offset: 0x00029E79
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x0002BC81 File Offset: 0x00029E81
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? keyId
		{
			get
			{
				return this._keyId;
			}
			set
			{
				this._keyId = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0002BC8A File Offset: 0x00029E8A
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0002BC92 File Offset: 0x00029E92
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? startDate
		{
			get
			{
				return this._startDate;
			}
			set
			{
				this._startDate = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0002BC9B File Offset: 0x00029E9B
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0002BCA3 File Offset: 0x00029EA3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string type
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0002BCAC File Offset: 0x00029EAC
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x0002BCB4 File Offset: 0x00029EB4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string usage
		{
			get
			{
				return this._usage;
			}
			set
			{
				this._usage = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0002BCBD File Offset: 0x00029EBD
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x0002BCD9 File Offset: 0x00029ED9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public byte[] value
		{
			get
			{
				if (this._value != null)
				{
					return (byte[])this._value.Clone();
				}
				return null;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040018DD RID: 6365
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _customKeyIdentifier;

		// Token: 0x040018DE RID: 6366
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _endDate;

		// Token: 0x040018DF RID: 6367
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _keyId;

		// Token: 0x040018E0 RID: 6368
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startDate;

		// Token: 0x040018E1 RID: 6369
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _type;

		// Token: 0x040018E2 RID: 6370
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _usage;

		// Token: 0x040018E3 RID: 6371
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _value;
	}
}
