using System;
using System.Collections.Generic;
using System.Drawing;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D2 RID: 466
	public class EnumObjectToColorFormatter : IToColorFormatter
	{
		// Token: 0x060014BD RID: 5309 RVA: 0x0005563D File Offset: 0x0005383D
		public EnumObjectToColorFormatter(Dictionary<Enum, Color> colorMappings)
		{
			this.colorMappings = colorMappings;
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0005564C File Offset: 0x0005384C
		public Color Format(object colorKey)
		{
			if (colorKey != null)
			{
				Enum key = (Enum)colorKey;
				if (this.colorMappings != null && this.colorMappings.ContainsKey(key))
				{
					return this.colorMappings[key];
				}
			}
			return Color.Empty;
		}

		// Token: 0x0400079A RID: 1946
		private Dictionary<Enum, Color> colorMappings;
	}
}
