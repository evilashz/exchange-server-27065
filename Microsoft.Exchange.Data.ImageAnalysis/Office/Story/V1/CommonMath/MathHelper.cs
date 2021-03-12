using System;

namespace Microsoft.Office.Story.V1.CommonMath
{
	// Token: 0x02000012 RID: 18
	internal static class MathHelper
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000366C File Offset: 0x0000186C
		public static float ChannelLuminance(float intensity)
		{
			if ((double)intensity <= 0.03928)
			{
				return (float)((double)intensity / 12.92);
			}
			return (float)Math.Pow(((double)intensity + 0.055) / 1.055, 2.4);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000036BC File Offset: 0x000018BC
		public static float ContrastRatio(double firstLuminance, double secondLuminance)
		{
			double num = Math.Min(firstLuminance, secondLuminance);
			double num2 = Math.Max(firstLuminance, secondLuminance);
			return (float)((num2 + 0.05) / (num + 0.05));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000036F1 File Offset: 0x000018F1
		public static float Clamp(this float value, float min, float max)
		{
			value = ((value > max) ? max : value);
			value = ((value < min) ? min : value);
			return value;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003708 File Offset: 0x00001908
		public static bool ExactEquals(this float first, float second)
		{
			if (float.IsNaN(first))
			{
				return float.IsNaN(second);
			}
			if (float.IsNaN(second))
			{
				return float.IsNaN(first);
			}
			return first == second;
		}
	}
}
