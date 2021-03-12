using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002A2 RID: 674
	internal struct FormatStyle
	{
		// Token: 0x06001AEE RID: 6894 RVA: 0x000D1718 File Offset: 0x000CF918
		internal FormatStyle(FormatStore store, int styleHandle)
		{
			this.Styles = store.Styles;
			this.StyleHandle = styleHandle;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000D172D File Offset: 0x000CF92D
		internal FormatStyle(FormatStore.StyleStore styles, int styleHandle)
		{
			this.Styles = styles;
			this.StyleHandle = styleHandle;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000D173D File Offset: 0x000CF93D
		public int Handle
		{
			get
			{
				return this.StyleHandle;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000D1745 File Offset: 0x000CF945
		public bool IsNull
		{
			get
			{
				return this.StyleHandle == 0;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x000D1750 File Offset: 0x000CF950
		public bool IsEmpty
		{
			get
			{
				return this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyMask.IsClear && (this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyList == null || this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyList.Length == 0);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000D17F3 File Offset: 0x000CF9F3
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x000D1821 File Offset: 0x000CFA21
		public FlagProperties FlagProperties
		{
			get
			{
				return this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].FlagProperties;
			}
			set
			{
				this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].FlagProperties = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000D1850 File Offset: 0x000CFA50
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x000D187E File Offset: 0x000CFA7E
		public PropertyBitMask PropertyMask
		{
			get
			{
				return this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyMask;
			}
			set
			{
				this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyMask = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000D18AD File Offset: 0x000CFAAD
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x000D18DB File Offset: 0x000CFADB
		public Property[] PropertyList
		{
			get
			{
				return this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyList;
			}
			set
			{
				this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].PropertyList = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000D190A File Offset: 0x000CFB0A
		internal int RefCount
		{
			get
			{
				return this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].RefCount;
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000D1938 File Offset: 0x000CFB38
		public void AddRef()
		{
			if (this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].RefCount != 2147483647)
			{
				FormatStore.StyleEntry[] array = this.Styles.Plane(this.StyleHandle);
				int num = this.Styles.Index(this.StyleHandle);
				array[num].RefCount = array[num].RefCount + 1;
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000D19AC File Offset: 0x000CFBAC
		public void Release()
		{
			if (this.Styles.Plane(this.StyleHandle)[this.Styles.Index(this.StyleHandle)].RefCount != 2147483647)
			{
				FormatStore.StyleEntry[] array = this.Styles.Plane(this.StyleHandle);
				int num = this.Styles.Index(this.StyleHandle);
				if ((array[num].RefCount = array[num].RefCount - 1) == 0)
				{
					this.Styles.Free(this.StyleHandle);
				}
			}
			this.StyleHandle = -1;
		}

		// Token: 0x040020A1 RID: 8353
		public static readonly FormatStyle Null = default(FormatStyle);

		// Token: 0x040020A2 RID: 8354
		internal FormatStore.StyleStore Styles;

		// Token: 0x040020A3 RID: 8355
		internal int StyleHandle;
	}
}
