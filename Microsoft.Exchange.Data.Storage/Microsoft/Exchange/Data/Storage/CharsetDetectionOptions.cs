using System;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005F3 RID: 1523
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CharsetDetectionOptions
	{
		// Token: 0x06003E9B RID: 16027 RVA: 0x00103FD4 File Offset: 0x001021D4
		public CharsetDetectionOptions()
		{
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x00103FEF File Offset: 0x001021EF
		public CharsetDetectionOptions(CharsetDetectionOptions options)
		{
			this.preferredInternetCodePageForShiftJis = options.preferredInternetCodePageForShiftJis;
			this.requiredCoverage = options.requiredCoverage;
			this.preferredCharset = options.preferredCharset;
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x0010402E File Offset: 0x0010222E
		// (set) Token: 0x06003E9E RID: 16030 RVA: 0x00104038 File Offset: 0x00102238
		public int PreferredInternetCodePageForShiftJis
		{
			get
			{
				return this.preferredInternetCodePageForShiftJis;
			}
			set
			{
				switch (value)
				{
				case 50220:
				case 50221:
					this.preferredInternetCodePageForShiftJis = value;
					return;
				}
				this.preferredInternetCodePageForShiftJis = 50222;
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x00104074 File Offset: 0x00102274
		// (set) Token: 0x06003EA0 RID: 16032 RVA: 0x0010407C File Offset: 0x0010227C
		public Charset PreferredCharset
		{
			get
			{
				return this.preferredCharset;
			}
			set
			{
				if (!value.IsAvailable)
				{
					throw new ArgumentException();
				}
				this.preferredCharset = value;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06003EA1 RID: 16033 RVA: 0x00104093 File Offset: 0x00102293
		// (set) Token: 0x06003EA2 RID: 16034 RVA: 0x0010409B File Offset: 0x0010229B
		public int RequiredCoverage
		{
			get
			{
				return this.requiredCoverage;
			}
			set
			{
				if (value >= 0 && value <= 100)
				{
					this.requiredCoverage = value;
					return;
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x001040B3 File Offset: 0x001022B3
		public override string ToString()
		{
			return string.Format("CharsetDetectionOptions:\r\n- preferredInternetCodePageForShiftJis: {0}\r\n- requiredCoverage:  {1}\r\n- preferredCharset:  {2}\r\n", this.preferredInternetCodePageForShiftJis, this.requiredCoverage, (this.preferredCharset == null) ? "null" : this.preferredCharset.Name);
		}

		// Token: 0x04002178 RID: 8568
		private int preferredInternetCodePageForShiftJis = 50222;

		// Token: 0x04002179 RID: 8569
		private int requiredCoverage = 100;

		// Token: 0x0400217A RID: 8570
		private Charset preferredCharset;
	}
}
