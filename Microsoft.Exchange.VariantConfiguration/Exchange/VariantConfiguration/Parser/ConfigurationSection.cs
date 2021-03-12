using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Search.Platform.Parallax;
using Microsoft.Search.Platform.Parallax.Util.IniFormat.FileModel;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000134 RID: 308
	internal class ConfigurationSection
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x000234D8 File Offset: 0x000216D8
		internal ConfigurationSection(string sectionName, string typeName, IEnumerable<ConfigurationParameter> parameters)
		{
			this.SectionName = sectionName;
			this.TypeName = typeName;
			this.Parameters = parameters;
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x000234F5 File Offset: 0x000216F5
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x000234FD File Offset: 0x000216FD
		public string SectionName { get; private set; }

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00023506 File Offset: 0x00021706
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0002350E File Offset: 0x0002170E
		public string TypeName { get; private set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00023517 File Offset: 0x00021717
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0002351F File Offset: 0x0002171F
		public IEnumerable<ConfigurationParameter> Parameters { get; private set; }

		// Token: 0x06000E88 RID: 3720 RVA: 0x00023528 File Offset: 0x00021728
		public static ConfigurationSection Create(Section section)
		{
			string typeName = ConfigurationSection.GetTypeName(section);
			IEnumerable<ConfigurationParameter> parameters = ConfigurationSection.ParseParameters(section.Parameters);
			return new ConfigurationSection(section.Name, typeName, parameters);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00023558 File Offset: 0x00021758
		public override bool Equals(object obj)
		{
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			return configurationSection != null && string.Equals(this.SectionName, configurationSection.SectionName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.TypeName, configurationSection.TypeName, StringComparison.OrdinalIgnoreCase) && this.Parameters.SequenceEqual(configurationSection.Parameters);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000235AA File Offset: 0x000217AA
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.SectionName.ToLowerInvariant().GetHashCode() ^ this.TypeName.ToLowerInvariant().GetHashCode() ^ this.CalculateParametersHash();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00023634 File Offset: 0x00021834
		internal IEnumerable<string> GetFlightDependencies()
		{
			List<string> list = new List<string>();
			foreach (ConfigurationParameter configurationParameter in this.Parameters)
			{
				bool parsedValue;
				IEnumerable<string> collection = from variant in configurationParameter.Variants
				where variant.Key.StartsWith("flt.", StringComparison.OrdinalIgnoreCase) && bool.TryParse(variant.Value, out parsedValue) && parsedValue
				select variant.Key.Substring("flt.".Length);
				list.AddRange(collection);
			}
			return list.Distinct<string>();
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000236D4 File Offset: 0x000218D4
		private static string GetTypeName(Section section)
		{
			foreach (ParameterAssignmentRule parameterAssignmentRule in section.Parameters)
			{
				if (string.Equals(parameterAssignmentRule.ParameterName, "_meta.type"))
				{
					if (!string.IsNullOrEmpty(parameterAssignmentRule.VariantString))
					{
						throw new TypeNotFoundException("_meta.type property should not have a variant string.");
					}
					return parameterAssignmentRule.Value;
				}
			}
			throw new TypeNotFoundException(string.Format("Section '{0}' contains no type data.", section.Name));
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002376C File Offset: 0x0002196C
		private static IEnumerable<ConfigurationParameter> ParseParameters(IEnumerable<ParameterAssignmentRule> parameters)
		{
			List<ConfigurationParameter> list = new List<ConfigurationParameter>();
			foreach (ParameterAssignmentRule parameterAssignmentRule in parameters)
			{
				if (!string.Equals(parameterAssignmentRule.ParameterName, "_meta.type") && !string.Equals(parameterAssignmentRule.ParameterName, "_meta.access"))
				{
					list.Add(ConfigurationParameter.Create(string.Format("{0}{1}={2}", parameterAssignmentRule.ParameterName, parameterAssignmentRule.VariantString, parameterAssignmentRule.Value)));
				}
			}
			return list;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00023800 File Offset: 0x00021A00
		private int CalculateParametersHash()
		{
			int num = 0;
			foreach (ConfigurationParameter configurationParameter in this.Parameters)
			{
				num ^= configurationParameter.GetHashCode();
			}
			return num;
		}
	}
}
