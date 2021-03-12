using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000211 RID: 529
	internal class UMGrammar : UMGrammarBase
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x00045C26 File Offset: 0x00043E26
		internal UMGrammar(string path, string ruleName, CultureInfo culture) : base(path, culture)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00045C37 File Offset: 0x00043E37
		internal UMGrammar(string path, string ruleName, CultureInfo culture, Uri baseUri, bool deleteFileAfterUse) : this(path, ruleName, culture)
		{
			this.baseUri = baseUri;
			this.deleteFileAfterUse = deleteFileAfterUse;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00045C52 File Offset: 0x00043E52
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00045C5A File Offset: 0x00043E5A
		public bool DeleteFileAfterUse
		{
			get
			{
				return this.deleteFileAfterUse;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00045C62 File Offset: 0x00043E62
		internal string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00045C6A File Offset: 0x00043E6A
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x00045C72 File Offset: 0x00043E72
		internal string Script { get; set; }

		// Token: 0x06000F7B RID: 3963 RVA: 0x00045C7B File Offset: 0x00043E7B
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.RuleName.GetHashCode();
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00045C90 File Offset: 0x00043E90
		internal override bool Equals(UMGrammarBase umGrammarBase)
		{
			UMGrammar umgrammar = umGrammarBase as UMGrammar;
			return umgrammar != null && string.Equals(base.Path, umgrammar.Path, StringComparison.OrdinalIgnoreCase) && string.Equals(this.RuleName, umgrammar.RuleName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00045CD4 File Offset: 0x00043ED4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Path: {0}, Rule: {1}, BaseURI: '{2}'", new object[]
			{
				base.Path,
				this.RuleName,
				(this.BaseUri != null) ? this.BaseUri.ToString() : "<null>"
			});
		}

		// Token: 0x04000B46 RID: 2886
		private readonly bool deleteFileAfterUse;

		// Token: 0x04000B47 RID: 2887
		private string ruleName;

		// Token: 0x04000B48 RID: 2888
		private Uri baseUri;
	}
}
