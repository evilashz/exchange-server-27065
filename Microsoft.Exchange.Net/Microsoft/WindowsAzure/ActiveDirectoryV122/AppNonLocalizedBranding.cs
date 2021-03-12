using System;
using System.CodeDom.Compiler;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C1 RID: 1473
	[DataServiceKey("locale")]
	public class AppNonLocalizedBranding
	{
		// Token: 0x060016D0 RID: 5840 RVA: 0x0002E474 File Offset: 0x0002C674
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static AppNonLocalizedBranding CreateAppNonLocalizedBranding(string locale)
		{
			return new AppNonLocalizedBranding
			{
				locale = locale
			};
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0002E48F File Offset: 0x0002C68F
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0002E497 File Offset: 0x0002C697
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string heroBackgroundColor
		{
			get
			{
				return this._heroBackgroundColor;
			}
			set
			{
				this._heroBackgroundColor = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0002E4A0 File Offset: 0x0002C6A0
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string locale
		{
			get
			{
				return this._locale;
			}
			set
			{
				this._locale = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0002E4B1 File Offset: 0x0002C6B1
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0002E4B9 File Offset: 0x0002C6B9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string preloadUrl
		{
			get
			{
				return this._preloadUrl;
			}
			set
			{
				this._preloadUrl = value;
			}
		}

		// Token: 0x04001A59 RID: 6745
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _heroBackgroundColor;

		// Token: 0x04001A5A RID: 6746
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _locale;

		// Token: 0x04001A5B RID: 6747
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _preloadUrl;
	}
}
