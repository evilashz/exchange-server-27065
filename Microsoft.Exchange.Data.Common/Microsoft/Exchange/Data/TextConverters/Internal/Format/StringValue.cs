using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002A3 RID: 675
	internal struct StringValue
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x000D1A4A File Offset: 0x000CFC4A
		internal StringValue(FormatStore store, int stringHandle)
		{
			this.Strings = store.Strings;
			this.StringHandle = stringHandle;
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000D1A5F File Offset: 0x000CFC5F
		internal StringValue(FormatStore.StringValueStore strings, int stringHandle)
		{
			this.Strings = strings;
			this.StringHandle = stringHandle;
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000D1A6F File Offset: 0x000CFC6F
		public PropertyValue PropertyValue
		{
			get
			{
				return new PropertyValue(PropertyType.String, this.StringHandle);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001B00 RID: 6912 RVA: 0x000D1A7D File Offset: 0x000CFC7D
		public int Length
		{
			get
			{
				return this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].Str.Length;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000D1AB0 File Offset: 0x000CFCB0
		public int RefCount
		{
			get
			{
				return this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].RefCount;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000D1ADE File Offset: 0x000CFCDE
		internal int Handle
		{
			get
			{
				return this.StringHandle;
			}
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000D1AE6 File Offset: 0x000CFCE6
		public string GetString()
		{
			return this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].Str;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000D1B14 File Offset: 0x000CFD14
		public void CopyTo(int sourceOffset, char[] buffer, int offset, int count)
		{
			this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].Str.CopyTo(sourceOffset, buffer, offset, count);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000D1B4C File Offset: 0x000CFD4C
		public void AddRef()
		{
			if (this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].RefCount != 2147483647)
			{
				FormatStore.StringValueEntry[] array = this.Strings.Plane(this.StringHandle);
				int num = this.Strings.Index(this.StringHandle);
				array[num].RefCount = array[num].RefCount + 1;
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000D1BC0 File Offset: 0x000CFDC0
		public void Release()
		{
			if (this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].RefCount != 2147483647)
			{
				FormatStore.StringValueEntry[] array = this.Strings.Plane(this.StringHandle);
				int num = this.Strings.Index(this.StringHandle);
				if ((array[num].RefCount = array[num].RefCount - 1) == 0)
				{
					this.Strings.Free(this.StringHandle);
				}
			}
			this.StringHandle = -1;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000D1C51 File Offset: 0x000CFE51
		internal void SetString(string str)
		{
			this.Strings.Plane(this.StringHandle)[this.Strings.Index(this.StringHandle)].Str = str;
		}

		// Token: 0x040020A4 RID: 8356
		internal FormatStore.StringValueStore Strings;

		// Token: 0x040020A5 RID: 8357
		internal int StringHandle;
	}
}
