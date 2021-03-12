using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x0200095D RID: 2397
	[Serializable]
	public class DlpTemplateRuleParameter
	{
		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x060055BC RID: 21948 RVA: 0x00160F98 File Offset: 0x0015F198
		// (set) Token: 0x060055BD RID: 21949 RVA: 0x00160FA0 File Offset: 0x0015F1A0
		public string Type { get; set; }

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x060055BE RID: 21950 RVA: 0x00160FA9 File Offset: 0x0015F1A9
		// (set) Token: 0x060055BF RID: 21951 RVA: 0x00160FB1 File Offset: 0x0015F1B1
		public bool Required { get; set; }

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x060055C0 RID: 21952 RVA: 0x00160FBA File Offset: 0x0015F1BA
		// (set) Token: 0x060055C1 RID: 21953 RVA: 0x00160FC2 File Offset: 0x0015F1C2
		public string Token { get; set; }

		// Token: 0x170019B1 RID: 6577
		// (set) Token: 0x060055C2 RID: 21954 RVA: 0x00160FCB File Offset: 0x0015F1CB
		public CultureInfo CurrentCulture
		{
			set
			{
				this.currentCulture = value;
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x060055C3 RID: 21955 RVA: 0x00160FD4 File Offset: 0x0015F1D4
		// (set) Token: 0x060055C4 RID: 21956 RVA: 0x00160FDC File Offset: 0x0015F1DC
		public Dictionary<string, string> LocalizedDescriptions { get; set; }

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x060055C5 RID: 21957 RVA: 0x00160FE5 File Offset: 0x0015F1E5
		public string Description
		{
			get
			{
				return DlpPolicyTemplateMetaData.GetLocalizedStringValue(this.LocalizedDescriptions, this.currentCulture);
			}
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x00160FF8 File Offset: 0x0015F1F8
		public string ToString(CultureInfo culture)
		{
			CultureInfo cultureInfo = this.currentCulture;
			this.currentCulture = culture;
			string result = this.ToString();
			this.CurrentCulture = cultureInfo;
			return result;
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x00161024 File Offset: 0x0015F224
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DlpTemplateRuleParameter.EscapeString(this.Type));
			stringBuilder.Append(";");
			stringBuilder.Append(this.Required);
			stringBuilder.Append(";");
			stringBuilder.Append(DlpTemplateRuleParameter.EscapeString(this.Token));
			stringBuilder.Append(";");
			stringBuilder.Append(DlpTemplateRuleParameter.EscapeString(this.Description));
			return stringBuilder.ToString();
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x001610A4 File Offset: 0x0015F2A4
		private static string EscapeString(string input)
		{
			string text = input.Replace("\\", "\\\\");
			return text.Replace(";", "\\;");
		}

		// Token: 0x040031BE RID: 12734
		private const string separator = ";";

		// Token: 0x040031BF RID: 12735
		private const string escapeCharacter = "\\";

		// Token: 0x040031C0 RID: 12736
		private CultureInfo currentCulture = DlpPolicyTemplateMetaData.DefaultCulture;
	}
}
