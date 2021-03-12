using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C3 RID: 1475
	public class KeyCredential
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0002E542 File Offset: 0x0002C742
		// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0002E55E File Offset: 0x0002C75E
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

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0002E567 File Offset: 0x0002C767
		// (set) Token: 0x060016E3 RID: 5859 RVA: 0x0002E56F File Offset: 0x0002C76F
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

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0002E578 File Offset: 0x0002C778
		// (set) Token: 0x060016E5 RID: 5861 RVA: 0x0002E580 File Offset: 0x0002C780
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

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0002E589 File Offset: 0x0002C789
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x0002E591 File Offset: 0x0002C791
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

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0002E59A File Offset: 0x0002C79A
		// (set) Token: 0x060016E9 RID: 5865 RVA: 0x0002E5A2 File Offset: 0x0002C7A2
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

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0002E5AB File Offset: 0x0002C7AB
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x0002E5B3 File Offset: 0x0002C7B3
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

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		// (set) Token: 0x060016ED RID: 5869 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
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

		// Token: 0x04001A5F RID: 6751
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _customKeyIdentifier;

		// Token: 0x04001A60 RID: 6752
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _endDate;

		// Token: 0x04001A61 RID: 6753
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _keyId;

		// Token: 0x04001A62 RID: 6754
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startDate;

		// Token: 0x04001A63 RID: 6755
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _type;

		// Token: 0x04001A64 RID: 6756
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _usage;

		// Token: 0x04001A65 RID: 6757
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _value;
	}
}
