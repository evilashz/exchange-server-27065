using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005DB RID: 1499
	public class KeyCredential
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0002F997 File Offset: 0x0002DB97
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0002F9B3 File Offset: 0x0002DBB3
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

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0002F9BC File Offset: 0x0002DBBC
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0002F9C4 File Offset: 0x0002DBC4
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

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0002F9CD File Offset: 0x0002DBCD
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x0002F9D5 File Offset: 0x0002DBD5
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

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0002F9E6 File Offset: 0x0002DBE6
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

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0002F9EF File Offset: 0x0002DBEF
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x0002F9F7 File Offset: 0x0002DBF7
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

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0002FA00 File Offset: 0x0002DC00
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0002FA08 File Offset: 0x0002DC08
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

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0002FA11 File Offset: 0x0002DC11
		// (set) Token: 0x0600188D RID: 6285 RVA: 0x0002FA2D File Offset: 0x0002DC2D
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

		// Token: 0x04001B1D RID: 6941
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _customKeyIdentifier;

		// Token: 0x04001B1E RID: 6942
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _endDate;

		// Token: 0x04001B1F RID: 6943
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _keyId;

		// Token: 0x04001B20 RID: 6944
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startDate;

		// Token: 0x04001B21 RID: 6945
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _type;

		// Token: 0x04001B22 RID: 6946
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _usage;

		// Token: 0x04001B23 RID: 6947
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _value;
	}
}
