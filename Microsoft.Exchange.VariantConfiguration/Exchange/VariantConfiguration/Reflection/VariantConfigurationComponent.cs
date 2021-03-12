using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000F6 RID: 246
	public abstract class VariantConfigurationComponent
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00018B1E File Offset: 0x00016D1E
		protected VariantConfigurationComponent(string componentName)
		{
			this.ComponentName = componentName;
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00018B3D File Offset: 0x00016D3D
		public string FileName
		{
			get
			{
				return "Settings\\" + this.ComponentName + ".settings.ini";
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00018B54 File Offset: 0x00016D54
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x00018B5C File Offset: 0x00016D5C
		public string ComponentName { get; private set; }

		// Token: 0x170007AC RID: 1964
		public VariantConfigurationSection this[string name]
		{
			get
			{
				return this.sections[name];
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00018B84 File Offset: 0x00016D84
		public IEnumerable<string> GetSections(bool includeInternal)
		{
			if (includeInternal)
			{
				return this.sections.Keys;
			}
			return from section in this.sections.Keys
			where this[section].IsPublic
			select section;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00018BC3 File Offset: 0x00016DC3
		public bool Contains(string name, bool includeInternal)
		{
			return this.sections.ContainsKey(name) && (includeInternal || this[name].IsPublic);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00018BE6 File Offset: 0x00016DE6
		protected void Add(VariantConfigurationSection section)
		{
			this.sections.Add(section.SectionName, section);
		}

		// Token: 0x04000484 RID: 1156
		private Dictionary<string, VariantConfigurationSection> sections = new Dictionary<string, VariantConfigurationSection>(StringComparer.OrdinalIgnoreCase);
	}
}
