using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005DC RID: 1500
	public class PasswordCredential
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0002FA3E File Offset: 0x0002DC3E
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0002FA5A File Offset: 0x0002DC5A
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

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0002FA63 File Offset: 0x0002DC63
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0002FA6B File Offset: 0x0002DC6B
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

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0002FA74 File Offset: 0x0002DC74
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0002FA7C File Offset: 0x0002DC7C
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

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0002FA85 File Offset: 0x0002DC85
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0002FA8D File Offset: 0x0002DC8D
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

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0002FA96 File Offset: 0x0002DC96
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0002FA9E File Offset: 0x0002DC9E
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

		// Token: 0x04001B24 RID: 6948
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _customKeyIdentifier;

		// Token: 0x04001B25 RID: 6949
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _endDate;

		// Token: 0x04001B26 RID: 6950
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _keyId;

		// Token: 0x04001B27 RID: 6951
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startDate;

		// Token: 0x04001B28 RID: 6952
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _value;
	}
}
