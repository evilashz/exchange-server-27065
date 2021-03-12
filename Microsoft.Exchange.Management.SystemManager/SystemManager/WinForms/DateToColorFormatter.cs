using System;
using System.Drawing;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D3 RID: 467
	public class DateToColorFormatter : IToColorFormatter
	{
		// Token: 0x060014BF RID: 5311 RVA: 0x0005568C File Offset: 0x0005388C
		public Color Format(object colorKey)
		{
			Color result = Color.Empty;
			if (colorKey != null && !DBNull.Value.Equals(colorKey))
			{
				DateTime d = (DateTime)colorKey;
				if ((d - DateTime.UtcNow).TotalDays <= 0.0)
				{
					result = Color.Red;
				}
			}
			return result;
		}
	}
}
