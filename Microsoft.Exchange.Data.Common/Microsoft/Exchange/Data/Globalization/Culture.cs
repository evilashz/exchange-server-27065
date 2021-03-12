using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public class Culture
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0005F3AC File Offset: 0x0005D5AC
		internal Culture(int lcid, string name)
		{
			this.lcid = lcid;
			this.name = name;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0005F3C2 File Offset: 0x0005D5C2
		public static Culture Default
		{
			get
			{
				return CultureCharsetDatabase.Data.DefaultCulture;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0005F3CE File Offset: 0x0005D5CE
		public static bool FallbackToDefaultCharset
		{
			get
			{
				return CultureCharsetDatabase.Data.FallbackToDefaultCharset;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0005F3DA File Offset: 0x0005D5DA
		public static Culture Invariant
		{
			get
			{
				return CultureCharsetDatabase.Data.InvariantCulture;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0005F3E6 File Offset: 0x0005D5E6
		public int LCID
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0005F3EE File Offset: 0x0005D5EE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0005F3F6 File Offset: 0x0005D5F6
		public Charset WindowsCharset
		{
			get
			{
				return this.windowsCharset;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0005F3FE File Offset: 0x0005D5FE
		public Charset MimeCharset
		{
			get
			{
				return this.mimeCharset;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0005F406 File Offset: 0x0005D606
		public Charset WebCharset
		{
			get
			{
				return this.webCharset;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0005F40E File Offset: 0x0005D60E
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0005F416 File Offset: 0x0005D616
		public string NativeDescription
		{
			get
			{
				return this.nativeDescription;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0005F41E File Offset: 0x0005D61E
		public Culture ParentCulture
		{
			get
			{
				return this.parentCulture;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0005F426 File Offset: 0x0005D626
		internal int[] CodepageDetectionPriorityOrder
		{
			get
			{
				return this.GetCodepageDetectionPriorityOrder(CultureCharsetDatabase.Data);
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0005F434 File Offset: 0x0005D634
		public static Culture GetCulture(string name)
		{
			Culture result;
			if (!Culture.TryGetCulture(name, out result))
			{
				throw new UnknownCultureException(name);
			}
			return result;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0005F453 File Offset: 0x0005D653
		public static bool TryGetCulture(string name, out Culture culture)
		{
			if (name == null)
			{
				culture = null;
				return false;
			}
			return CultureCharsetDatabase.Data.NameToCulture.TryGetValue(name, out culture);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0005F470 File Offset: 0x0005D670
		public static Culture GetCulture(int lcid)
		{
			Culture result;
			if (!Culture.TryGetCulture(lcid, out result))
			{
				throw new UnknownCultureException(lcid);
			}
			return result;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0005F48F File Offset: 0x0005D68F
		public static bool TryGetCulture(int lcid, out Culture culture)
		{
			return CultureCharsetDatabase.Data.LcidToCulture.TryGetValue(lcid, out culture);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0005F4A2 File Offset: 0x0005D6A2
		public CultureInfo GetCultureInfo()
		{
			if (this.cultureInfo == null)
			{
				return CultureInfo.InvariantCulture;
			}
			return this.cultureInfo;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0005F4B8 File Offset: 0x0005D6B8
		internal void SetWindowsCharset(Charset windowsCharset)
		{
			this.windowsCharset = windowsCharset;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0005F4C1 File Offset: 0x0005D6C1
		internal void SetMimeCharset(Charset mimeCharset)
		{
			this.mimeCharset = mimeCharset;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0005F4CA File Offset: 0x0005D6CA
		internal void SetWebCharset(Charset webCharset)
		{
			this.webCharset = webCharset;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0005F4D3 File Offset: 0x0005D6D3
		internal void SetDescription(string description)
		{
			this.description = description;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0005F4DC File Offset: 0x0005D6DC
		internal void SetNativeDescription(string description)
		{
			this.nativeDescription = description;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0005F4E5 File Offset: 0x0005D6E5
		internal void SetParentCulture(Culture parentCulture)
		{
			this.parentCulture = parentCulture;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0005F4EE File Offset: 0x0005D6EE
		internal void SetCultureInfo(CultureInfo cultureInfo)
		{
			this.cultureInfo = cultureInfo;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0005F4F8 File Offset: 0x0005D6F8
		internal int[] GetCodepageDetectionPriorityOrder(CultureCharsetDatabase.GlobalizationData data)
		{
			if (this.codepageDetectionPriorityOrder == null)
			{
				this.codepageDetectionPriorityOrder = CultureCharsetDatabase.GetCultureSpecificCodepageDetectionPriorityOrder(this, (this.parentCulture == null || this.parentCulture == this) ? data.DefaultDetectionPriorityOrder : this.parentCulture.GetCodepageDetectionPriorityOrder(data));
			}
			return this.codepageDetectionPriorityOrder;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0005F544 File Offset: 0x0005D744
		internal void SetCodepageDetectionPriorityOrder(int[] codepageDetectionPriorityOrder)
		{
			this.codepageDetectionPriorityOrder = codepageDetectionPriorityOrder;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0005F550 File Offset: 0x0005D750
		internal CultureInfo GetSpecificCultureInfo()
		{
			if (this.cultureInfo == null)
			{
				return CultureInfo.InvariantCulture;
			}
			CultureInfo result;
			try
			{
				result = CultureInfo.CreateSpecificCulture(this.cultureInfo.Name);
			}
			catch (ArgumentException)
			{
				result = CultureInfo.InvariantCulture;
			}
			return result;
		}

		// Token: 0x04000DC5 RID: 3525
		private int lcid;

		// Token: 0x04000DC6 RID: 3526
		private string name;

		// Token: 0x04000DC7 RID: 3527
		private Charset windowsCharset;

		// Token: 0x04000DC8 RID: 3528
		private Charset mimeCharset;

		// Token: 0x04000DC9 RID: 3529
		private Charset webCharset;

		// Token: 0x04000DCA RID: 3530
		private string description;

		// Token: 0x04000DCB RID: 3531
		private string nativeDescription;

		// Token: 0x04000DCC RID: 3532
		private Culture parentCulture;

		// Token: 0x04000DCD RID: 3533
		private int[] codepageDetectionPriorityOrder;

		// Token: 0x04000DCE RID: 3534
		private CultureInfo cultureInfo;
	}
}
