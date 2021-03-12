using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x0200028B RID: 651
	public class ResultItem
	{
		// Token: 0x0600155A RID: 5466 RVA: 0x00042A0B File Offset: 0x00040C0B
		public ResultItem()
		{
			this.Index = -1;
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00042A1A File Offset: 0x00040C1A
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x00042A22 File Offset: 0x00040C22
		internal ResultVerifyMethod VerifyMethod
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00042A2B File Offset: 0x00040C2B
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00042A33 File Offset: 0x00040C33
		internal string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00042A3C File Offset: 0x00040C3C
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x00042A44 File Offset: 0x00040C44
		internal string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00042A4D File Offset: 0x00040C4D
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x00042A55 File Offset: 0x00040C55
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00042A5E File Offset: 0x00040C5E
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x00042A66 File Offset: 0x00040C66
		internal bool UseFile
		{
			get
			{
				return this.useFile;
			}
			set
			{
				this.useFile = value;
			}
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00042A70 File Offset: 0x00040C70
		public override string ToString()
		{
			return string.Format("VerifyMethod={0}, Index={1}, PropertyName={2}, Value={3}, UseFile={4}", new object[]
			{
				this.VerifyMethod,
				this.Index,
				this.PropertyName,
				this.Value,
				this.UseFile
			});
		}

		// Token: 0x04000A68 RID: 2664
		private ResultVerifyMethod method;

		// Token: 0x04000A69 RID: 2665
		private string propertyName;

		// Token: 0x04000A6A RID: 2666
		private string value;

		// Token: 0x04000A6B RID: 2667
		private int index;

		// Token: 0x04000A6C RID: 2668
		private bool useFile;
	}
}
