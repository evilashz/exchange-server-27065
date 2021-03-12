using System;
using System.Linq;
using Microsoft.Search.Platform.Parallax.DataLoad;
using Microsoft.Search.Platform.Parallax.Util.IniFormat.FileModel;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200012D RID: 301
	internal class OverrideDataTransformation : IDataTransformation
	{
		// Token: 0x06000E4E RID: 3662 RVA: 0x000228BA File Offset: 0x00020ABA
		private OverrideDataTransformation(VariantConfigurationOverride validationOverride)
		{
			if (validationOverride == null)
			{
				throw new ArgumentNullException("validationOverride");
			}
			this.validationOverride = validationOverride;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000228D7 File Offset: 0x00020AD7
		private OverrideDataTransformation()
		{
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000228DF File Offset: 0x00020ADF
		public static OverrideDataTransformation Get(VariantConfigurationOverride validationOverride)
		{
			if (validationOverride == null)
			{
				return OverrideDataTransformation.instance;
			}
			return new OverrideDataTransformation(validationOverride);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000228F0 File Offset: 0x00020AF0
		public string Transform(string dataSourceName, string input)
		{
			string text = input;
			if (text.Contains("_meta.access=public"))
			{
				text = text.Replace("_meta.access=public", string.Empty);
			}
			VariantConfigurationOverride[] array = VariantConfiguration.Overrides;
			if (this.validationOverride != null)
			{
				if (array != null)
				{
					array = array.Concat(new VariantConfigurationOverride[]
					{
						this.validationOverride
					}).ToArray<VariantConfigurationOverride>();
				}
				else
				{
					array = new VariantConfigurationOverride[]
					{
						this.validationOverride
					};
				}
			}
			if (array != null && array.Length > 0)
			{
				IniFileModel iniFileModel = IniFileModel.CreateFromString(dataSourceName, text);
				bool flag = false;
				foreach (VariantConfigurationOverride variantConfigurationOverride in array.Reverse<VariantConfigurationOverride>())
				{
					if (variantConfigurationOverride.FileName.Equals(dataSourceName, StringComparison.InvariantCultureIgnoreCase) && iniFileModel.Sections.ContainsKey(variantConfigurationOverride.SectionName))
					{
						Section section = iniFileModel.Sections[variantConfigurationOverride.SectionName];
						Section section2 = new Section(section.Name);
						foreach (ParameterAssignmentRule parameterAssignmentRule in this.GetParameterAssignmentRules(variantConfigurationOverride))
						{
							section2.AddParameter(parameterAssignmentRule);
						}
						foreach (ParameterAssignmentRule parameterAssignmentRule2 in section.Parameters)
						{
							section2.AddParameter(parameterAssignmentRule2);
						}
						iniFileModel.RemoveSection(section);
						iniFileModel.AddSection(section2);
						flag = true;
					}
				}
				if (flag)
				{
					text = iniFileModel.Serialize();
				}
			}
			return text;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00022AA0 File Offset: 0x00020CA0
		private ParameterAssignmentRule[] GetParameterAssignmentRules(VariantConfigurationOverride o)
		{
			ParameterAssignmentRule[] array = new ParameterAssignmentRule[o.Parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = o.Parameters[i].Split(OverrideDataTransformation.Assignment, 2);
				string text = array2[1];
				array2 = array2[0].Split(OverrideDataTransformation.Ampersand, 2);
				string text2 = array2[0];
				string text3 = (array2.Length > 1) ? ("&" + array2[1]) : string.Empty;
				array[i] = new ParameterAssignmentRule(o.SectionName, text2, text3, text);
			}
			return array;
		}

		// Token: 0x0400048A RID: 1162
		private const string MetaAccess = "_meta.access=public";

		// Token: 0x0400048B RID: 1163
		private static readonly char[] Assignment = new char[]
		{
			'='
		};

		// Token: 0x0400048C RID: 1164
		private static readonly char[] Ampersand = new char[]
		{
			'&'
		};

		// Token: 0x0400048D RID: 1165
		private static OverrideDataTransformation instance = new OverrideDataTransformation();

		// Token: 0x0400048E RID: 1166
		private VariantConfigurationOverride validationOverride;
	}
}
