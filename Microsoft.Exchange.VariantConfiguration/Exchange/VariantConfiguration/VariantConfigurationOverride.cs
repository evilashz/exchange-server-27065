using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000142 RID: 322
	public abstract class VariantConfigurationOverride
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x0002511C File Offset: 0x0002331C
		public VariantConfigurationOverride(string componentName, string sectionName, params string[] parameters)
		{
			if (string.IsNullOrEmpty(componentName))
			{
				throw new ArgumentNullException("componentName");
			}
			if (string.IsNullOrEmpty(sectionName))
			{
				throw new ArgumentNullException("sectionName");
			}
			if (parameters == null || parameters.Length == 0)
			{
				throw new ArgumentNullException("parameters");
			}
			this.ComponentName = componentName;
			this.SectionName = sectionName;
			this.Parameters = parameters;
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0002517D File Offset: 0x0002337D
		public bool IsFlight
		{
			get
			{
				return this is VariantConfigurationFlightOverride;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00025188 File Offset: 0x00023388
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x00025190 File Offset: 0x00023390
		public string ComponentName { get; private set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06000EF1 RID: 3825
		public abstract string FileName { get; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00025199 File Offset: 0x00023399
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x000251A1 File Offset: 0x000233A1
		public string SectionName { get; private set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x000251AA File Offset: 0x000233AA
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x000251B2 File Offset: 0x000233B2
		public string[] Parameters { get; private set; }

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000251BC File Offset: 0x000233BC
		public override bool Equals(object obj)
		{
			VariantConfigurationOverride variantConfigurationOverride = obj as VariantConfigurationOverride;
			return variantConfigurationOverride != null && base.GetType() == variantConfigurationOverride.GetType() && this.IsFlight == variantConfigurationOverride.IsFlight && this.ComponentName.Equals(variantConfigurationOverride.ComponentName, StringComparison.InvariantCultureIgnoreCase) && this.SectionName.Equals(variantConfigurationOverride.SectionName, StringComparison.InvariantCultureIgnoreCase) && this.ParametersMatch(variantConfigurationOverride.Parameters);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002522C File Offset: 0x0002342C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ (this.IsFlight ? 1 : 0) ^ this.ComponentName.ToLowerInvariant().GetHashCode() ^ this.SectionName.ToLowerInvariant().GetHashCode() ^ this.GetParametersHashCode();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002527C File Offset: 0x0002347C
		private bool ParametersMatch(string[] parameters)
		{
			if (this.Parameters.Length != parameters.Length)
			{
				return false;
			}
			for (int i = 0; i < this.Parameters.Length; i++)
			{
				if (!this.Parameters[i].Equals(parameters[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000252C0 File Offset: 0x000234C0
		private int GetParametersHashCode()
		{
			int num = 0;
			foreach (string text in this.Parameters)
			{
				num ^= text.GetHashCode();
			}
			return num;
		}
	}
}
