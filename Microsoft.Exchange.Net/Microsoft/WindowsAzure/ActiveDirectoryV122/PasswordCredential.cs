using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C4 RID: 1476
	public class PasswordCredential
	{
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0002E5E9 File Offset: 0x0002C7E9
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0002E605 File Offset: 0x0002C805
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

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0002E60E File Offset: 0x0002C80E
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0002E616 File Offset: 0x0002C816
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

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0002E61F File Offset: 0x0002C81F
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x0002E627 File Offset: 0x0002C827
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

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0002E630 File Offset: 0x0002C830
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0002E638 File Offset: 0x0002C838
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

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0002E641 File Offset: 0x0002C841
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x0002E649 File Offset: 0x0002C849
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

		// Token: 0x04001A66 RID: 6758
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _customKeyIdentifier;

		// Token: 0x04001A67 RID: 6759
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _endDate;

		// Token: 0x04001A68 RID: 6760
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _keyId;

		// Token: 0x04001A69 RID: 6761
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _startDate;

		// Token: 0x04001A6A RID: 6762
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _value;
	}
}
