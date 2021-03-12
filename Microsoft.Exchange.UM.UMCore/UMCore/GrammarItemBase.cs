using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000153 RID: 339
	internal abstract class GrammarItemBase : IEquatable<GrammarItemBase>
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x0002B5F1 File Offset: 0x000297F1
		internal GrammarItemBase(float weight)
		{
			this.weight = Math.Max(1E-05f, (float)Math.Round((double)weight, 5));
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002B61D File Offset: 0x0002981D
		internal float Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A1F RID: 2591
		public abstract bool IsEmpty { get; }

		// Token: 0x06000A20 RID: 2592
		public abstract bool Equals(GrammarItemBase otherItemBase);

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002B628 File Offset: 0x00029828
		public override bool Equals(object obj)
		{
			GrammarItemBase grammarItemBase = obj as GrammarItemBase;
			return grammarItemBase != null && this.Equals(grammarItemBase);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002B648 File Offset: 0x00029848
		public override int GetHashCode()
		{
			return this.InternalGetHashCode();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002B650 File Offset: 0x00029850
		public override string ToString()
		{
			if (1f == this.weight)
			{
				return string.Format(CultureInfo.InvariantCulture, "\r\n            <item >{0}\r\n            </item>", new object[]
				{
					this.GetInnerItem()
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "\r\n            <item weight='{0}'>{1}\r\n            </item>", new object[]
			{
				this.weight.ToString("N5", CultureInfo.InvariantCulture),
				this.GetInnerItem()
			});
		}

		// Token: 0x06000A24 RID: 2596
		protected abstract string GetInnerItem();

		// Token: 0x06000A25 RID: 2597
		protected abstract int InternalGetHashCode();

		// Token: 0x04000937 RID: 2359
		private float weight = 1f;
	}
}
