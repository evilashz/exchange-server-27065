using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200017D RID: 381
	internal class BodyPreference
	{
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x0005CA2D File Offset: 0x0005AC2D
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x0005CA35 File Offset: 0x0005AC35
		public bool AllOrNone
		{
			get
			{
				return this.allOrNone;
			}
			set
			{
				this.allOrNone = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0005CA3E File Offset: 0x0005AC3E
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x0005CA46 File Offset: 0x0005AC46
		public long TruncationSize
		{
			get
			{
				return this.truncationSize;
			}
			set
			{
				this.truncationSize = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0005CA4F File Offset: 0x0005AC4F
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x0005CA57 File Offset: 0x0005AC57
		public virtual BodyType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0005CA60 File Offset: 0x0005AC60
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x0005CA68 File Offset: 0x0005AC68
		public int Preview
		{
			get
			{
				return this.preview;
			}
			set
			{
				this.preview = value;
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0005CA74 File Offset: 0x0005AC74
		public BodyPreference Clone()
		{
			return new BodyPreference
			{
				AllOrNone = this.AllOrNone,
				TruncationSize = this.TruncationSize,
				Type = this.Type,
				Preview = this.Preview
			};
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0005CAB8 File Offset: 0x0005ACB8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Type: ",
				this.Type,
				", AllOrNone: ",
				this.AllOrNone,
				", TruncationSize: ",
				this.TruncationSize,
				", Preview: ",
				this.Preview
			});
		}

		// Token: 0x04000ACF RID: 2767
		private bool allOrNone;

		// Token: 0x04000AD0 RID: 2768
		private long truncationSize = -1L;

		// Token: 0x04000AD1 RID: 2769
		private BodyType type;

		// Token: 0x04000AD2 RID: 2770
		private int preview;
	}
}
