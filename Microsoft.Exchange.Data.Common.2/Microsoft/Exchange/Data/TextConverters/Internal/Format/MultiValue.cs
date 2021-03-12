using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002A4 RID: 676
	internal struct MultiValue
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x000D1C80 File Offset: 0x000CFE80
		internal MultiValue(FormatStore store, int multiValueHandle)
		{
			this.MultiValues = store.MultiValues;
			this.MultiValueHandle = multiValueHandle;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000D1C95 File Offset: 0x000CFE95
		internal MultiValue(FormatStore.MultiValueStore multiValues, int multiValueHandle)
		{
			this.MultiValues = multiValues;
			this.MultiValueHandle = multiValueHandle;
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000D1CA5 File Offset: 0x000CFEA5
		public PropertyValue PropertyValue
		{
			get
			{
				return new PropertyValue(PropertyType.MultiValue, this.MultiValueHandle);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000D1CB3 File Offset: 0x000CFEB3
		public int Length
		{
			get
			{
				return this.MultiValues.Plane(this.MultiValueHandle)[this.MultiValues.Index(this.MultiValueHandle)].Values.Length;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000D1CE3 File Offset: 0x000CFEE3
		internal int Handle
		{
			get
			{
				return this.MultiValueHandle;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000D1CEB File Offset: 0x000CFEEB
		internal int RefCount
		{
			get
			{
				return this.MultiValues.Plane(this.MultiValueHandle)[this.MultiValues.Index(this.MultiValueHandle)].RefCount;
			}
		}

		// Token: 0x1700070B RID: 1803
		public PropertyValue this[int index]
		{
			get
			{
				return this.MultiValues.Plane(this.MultiValueHandle)[this.MultiValues.Index(this.MultiValueHandle)].Values[index];
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000D1D54 File Offset: 0x000CFF54
		public StringValue GetStringValue(int index)
		{
			return this.MultiValues.Store.GetStringValue(this.MultiValues.Plane(this.MultiValueHandle)[this.MultiValues.Index(this.MultiValueHandle)].Values[index]);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000D1DA8 File Offset: 0x000CFFA8
		public void AddRef()
		{
			if (this.MultiValues.Plane(this.MultiValueHandle)[this.MultiValues.Index(this.MultiValueHandle)].RefCount != 2147483647)
			{
				FormatStore.MultiValueEntry[] array = this.MultiValues.Plane(this.MultiValueHandle);
				int num = this.MultiValues.Index(this.MultiValueHandle);
				array[num].RefCount = array[num].RefCount + 1;
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000D1E1C File Offset: 0x000D001C
		public void Release()
		{
			if (this.MultiValues.Plane(this.MultiValueHandle)[this.MultiValues.Index(this.MultiValueHandle)].RefCount != 2147483647)
			{
				FormatStore.MultiValueEntry[] array = this.MultiValues.Plane(this.MultiValueHandle);
				int num = this.MultiValues.Index(this.MultiValueHandle);
				if ((array[num].RefCount = array[num].RefCount - 1) == 0)
				{
					this.MultiValues.Free(this.MultiValueHandle);
				}
			}
			this.MultiValueHandle = -1;
		}

		// Token: 0x040020A6 RID: 8358
		internal FormatStore.MultiValueStore MultiValues;

		// Token: 0x040020A7 RID: 8359
		internal int MultiValueHandle;
	}
}
