using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000210 RID: 528
	internal abstract class UMGrammarBase
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x00045B88 File Offset: 0x00043D88
		protected UMGrammarBase(string path, CultureInfo culture)
		{
			this.path = path;
			this.culture = culture;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00045B9E File Offset: 0x00043D9E
		internal string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00045BA6 File Offset: 0x00043DA6
		internal CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00045BB0 File Offset: 0x00043DB0
		internal string ManifestFileGrammarNode
		{
			get
			{
				Uri uri = new Uri("file:///" + this.Path);
				return string.Format(CultureInfo.InvariantCulture, "<resource src=\"{0}\"/>", new object[]
				{
					uri.ToString()
				});
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00045BF4 File Offset: 0x00043DF4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			UMGrammarBase umgrammarBase = obj as UMGrammarBase;
			return umgrammarBase != null && this.Equals(umgrammarBase);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00045C19 File Offset: 0x00043E19
		public override int GetHashCode()
		{
			return this.Path.GetHashCode();
		}

		// Token: 0x06000F73 RID: 3955
		internal abstract bool Equals(UMGrammarBase umGrammarBase);

		// Token: 0x04000B44 RID: 2884
		private string path;

		// Token: 0x04000B45 RID: 2885
		private CultureInfo culture;
	}
}
