using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002BC RID: 700
	internal struct MultiValueBuildHelper
	{
		// Token: 0x06001BD3 RID: 7123 RVA: 0x000D5CA6 File Offset: 0x000D3EA6
		internal MultiValueBuildHelper(FormatStore store)
		{
			this.Store = store;
			this.Values = null;
			this.ValuesCount = 0;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x000D5CBD File Offset: 0x000D3EBD
		public int Count
		{
			get
			{
				return this.ValuesCount;
			}
		}

		// Token: 0x17000728 RID: 1832
		public PropertyValue this[int i]
		{
			get
			{
				return this.Values[i];
			}
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000D5CD8 File Offset: 0x000D3ED8
		public void AddValue(PropertyValue value)
		{
			if (this.Values == null)
			{
				this.Values = new PropertyValue[4];
			}
			else if (this.ValuesCount == this.Values.Length)
			{
				if (this.ValuesCount == MultiValueBuildHelper.MaxValues)
				{
					throw new TextConvertersException(TextConvertersStrings.InputDocumentTooComplex);
				}
				PropertyValue[] array = new PropertyValue[this.ValuesCount * 2];
				Array.Copy(this.Values, 0, array, 0, this.ValuesCount);
				this.Values = array;
			}
			this.Values[this.ValuesCount++] = value;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000D5D70 File Offset: 0x000D3F70
		public PropertyValue[] GetValues()
		{
			if (this.ValuesCount == 0)
			{
				return null;
			}
			PropertyValue[] array = new PropertyValue[this.ValuesCount];
			Array.Copy(this.Values, 0, array, 0, this.ValuesCount);
			this.ValuesCount = 0;
			return array;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x000D5DB0 File Offset: 0x000D3FB0
		public void Cancel()
		{
			for (int i = 0; i < this.ValuesCount; i++)
			{
				if (this.Values[i].IsRefCountedHandle)
				{
					this.Store.ReleaseValue(this.Values[i]);
				}
			}
			this.ValuesCount = 0;
		}

		// Token: 0x0400212C RID: 8492
		internal static readonly int MaxValues = 32;

		// Token: 0x0400212D RID: 8493
		internal FormatStore Store;

		// Token: 0x0400212E RID: 8494
		internal PropertyValue[] Values;

		// Token: 0x0400212F RID: 8495
		internal int ValuesCount;
	}
}
