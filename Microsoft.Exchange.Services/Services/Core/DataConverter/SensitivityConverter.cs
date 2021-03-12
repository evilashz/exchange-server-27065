using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001FA RID: 506
	internal class SensitivityConverter : EnumConverter
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x000431FC File Offset: 0x000413FC
		public static Sensitivity Parse(string propertyString)
		{
			if (propertyString != null)
			{
				Sensitivity result;
				if (!(propertyString == "Normal"))
				{
					if (!(propertyString == "Personal"))
					{
						if (!(propertyString == "Private"))
						{
							if (!(propertyString == "Confidential") && !(propertyString == "CompanyConfidential"))
							{
								goto IL_5A;
							}
							result = Sensitivity.CompanyConfidential;
						}
						else
						{
							result = Sensitivity.Private;
						}
					}
					else
					{
						result = Sensitivity.Personal;
					}
				}
				else
				{
					result = Sensitivity.Normal;
				}
				return result;
			}
			IL_5A:
			throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Sensitivity type not supported '{0}'", new object[]
			{
				propertyString
			}));
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00043288 File Offset: 0x00041488
		public static string ToString(Sensitivity propertyValue)
		{
			string result = null;
			switch (propertyValue)
			{
			case Sensitivity.Normal:
				result = "Normal";
				break;
			case Sensitivity.Personal:
				result = "Personal";
				break;
			case Sensitivity.Private:
				result = "Private";
				break;
			case Sensitivity.CompanyConfidential:
				result = "Confidential";
				break;
			}
			return result;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x000432D0 File Offset: 0x000414D0
		public override object ConvertToObject(string propertyString)
		{
			return SensitivityConverter.Parse(propertyString);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x000432DD File Offset: 0x000414DD
		public override string ConvertToString(object propertyValue)
		{
			return SensitivityConverter.ToString((Sensitivity)propertyValue);
		}
	}
}
