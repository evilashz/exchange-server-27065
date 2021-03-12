using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000597 RID: 1431
	public class PasswordCredential
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0002BCEA File Offset: 0x00029EEA
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0002BD06 File Offset: 0x00029F06
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0002BD0F File Offset: 0x00029F0F
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0002BD17 File Offset: 0x00029F17
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0002BD20 File Offset: 0x00029F20
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x0002BD28 File Offset: 0x00029F28
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0002BD31 File Offset: 0x00029F31
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0002BD39 File Offset: 0x00029F39
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0002BD42 File Offset: 0x00029F42
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0002BD4A File Offset: 0x00029F4A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040018E4 RID: 6372
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _customKeyIdentifier;

		// Token: 0x040018E5 RID: 6373
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _endDate;

		// Token: 0x040018E6 RID: 6374
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _keyId;

		// Token: 0x040018E7 RID: 6375
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startDate;

		// Token: 0x040018E8 RID: 6376
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _value;
	}
}
