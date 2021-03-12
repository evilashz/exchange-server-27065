using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000106 RID: 262
	internal abstract class SearchGrammarFile
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x0001D878 File Offset: 0x0001BA78
		protected SearchGrammarFile(CultureInfo culture) : this(culture, false)
		{
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001D882 File Offset: 0x0001BA82
		protected SearchGrammarFile(CultureInfo culture, bool compiled)
		{
			this.culture = culture;
			this.compiled = compiled;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001D898 File Offset: 0x0001BA98
		public virtual Uri BaseUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001D89B File Offset: 0x0001BA9B
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001D8A3 File Offset: 0x0001BAA3
		internal CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
		internal bool Compiled
		{
			get
			{
				return this.compiled;
			}
			set
			{
				this.compiled = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000749 RID: 1865
		internal abstract string FilePath { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600074A RID: 1866
		internal abstract bool HasEntries { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001D8BD File Offset: 0x0001BABD
		protected string Extension
		{
			get
			{
				if (!this.compiled)
				{
					return ".grxml";
				}
				return ".cfg";
			}
		}

		// Token: 0x04000824 RID: 2084
		private CultureInfo culture;

		// Token: 0x04000825 RID: 2085
		private bool compiled;
	}
}
