using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001EE RID: 494
	internal class ImportanceConverter : EnumConverter
	{
		// Token: 0x06000D07 RID: 3335 RVA: 0x00042890 File Offset: 0x00040A90
		public static Importance Parse(string propertyString)
		{
			if (propertyString != null)
			{
				Importance result;
				if (!(propertyString == "Low"))
				{
					if (!(propertyString == "Normal"))
					{
						if (!(propertyString == "High"))
						{
							goto IL_3C;
						}
						result = Importance.High;
					}
					else
					{
						result = Importance.Normal;
					}
				}
				else
				{
					result = Importance.Low;
				}
				return result;
			}
			IL_3C:
			throw new FormatException("Invalid Importance string: " + propertyString);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000428EC File Offset: 0x00040AEC
		public static string ToString(Importance propertyValue)
		{
			string result = null;
			switch (propertyValue)
			{
			case Importance.Low:
				result = "Low";
				break;
			case Importance.Normal:
				result = "Normal";
				break;
			case Importance.High:
				result = "High";
				break;
			}
			return result;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00042928 File Offset: 0x00040B28
		public override object ConvertToObject(string propertyString)
		{
			return ImportanceConverter.Parse(propertyString);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00042935 File Offset: 0x00040B35
		public override string ConvertToString(object propertyValue)
		{
			return ImportanceConverter.ToString((Importance)propertyValue);
		}
	}
}
