using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006E2 RID: 1762
	[DataContract]
	public class RegexValidatorInfo : ValidatorInfo
	{
		// Token: 0x06004A4C RID: 19020 RVA: 0x000E3627 File Offset: 0x000E1827
		internal RegexValidatorInfo(RegexConstraint constraint) : this(constraint.Pattern, constraint.PatternDescription, RegexValidatorInfo.ConvertToJavaScriptOptions(constraint.Options))
		{
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x000E364B File Offset: 0x000E184B
		public RegexValidatorInfo(string pattern, string patternDescription, string options) : this("RegexValidator", pattern, patternDescription, options)
		{
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x000E365B File Offset: 0x000E185B
		protected RegexValidatorInfo(string validatorType, string pattern, string patternDescription, string options) : base(validatorType)
		{
			this.Pattern = pattern;
			this.PatternDescription = patternDescription;
			this.Options = options;
		}

		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x000E367A File Offset: 0x000E187A
		// (set) Token: 0x06004A50 RID: 19024 RVA: 0x000E3682 File Offset: 0x000E1882
		[DataMember]
		public string Pattern { get; set; }

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x000E368B File Offset: 0x000E188B
		// (set) Token: 0x06004A52 RID: 19026 RVA: 0x000E3693 File Offset: 0x000E1893
		[DataMember]
		public string PatternDescription { get; set; }

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x000E369C File Offset: 0x000E189C
		// (set) Token: 0x06004A54 RID: 19028 RVA: 0x000E36A4 File Offset: 0x000E18A4
		[DataMember]
		public string Options { get; set; }

		// Token: 0x06004A55 RID: 19029 RVA: 0x000E36B0 File Offset: 0x000E18B0
		private static string ConvertToJavaScriptOptions(RegexOptions regexOptions)
		{
			string text = string.Empty;
			if ((regexOptions & RegexOptions.Multiline) == RegexOptions.Multiline)
			{
				text += "m";
			}
			if ((regexOptions & RegexOptions.IgnoreCase) == RegexOptions.IgnoreCase)
			{
				text += "i";
			}
			return text;
		}
	}
}
