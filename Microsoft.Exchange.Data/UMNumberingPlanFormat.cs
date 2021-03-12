using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	public class UMNumberingPlanFormat
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x0003138A File Offset: 0x0002F58A
		private UMNumberingPlanFormat(string numberPlanFormat)
		{
			this.numberPlanFormat = numberPlanFormat;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003139C File Offset: 0x0002F59C
		public bool TryMapNumber(string number, out string mappedNumber)
		{
			mappedNumber = null;
			if (!string.IsNullOrEmpty(number))
			{
				char[] array = this.numberPlanFormat.ToCharArray();
				int num = number.Length - 1;
				int num2 = this.numberPlanFormat.Length - 1;
				while (num >= 0 && num2 >= 0)
				{
					if (number[num] != array[num2])
					{
						if (array[num2] != 'x')
						{
							return false;
						}
						array[num2] = number[num];
					}
					num--;
					num2--;
				}
				if (num < 0 && -1 == Array.IndexOf<char>(array, 'x'))
				{
					mappedNumber = new string(array);
				}
			}
			return null != mappedNumber;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0003142C File Offset: 0x0002F62C
		public static UMNumberingPlanFormat Parse(string numberPlanFormat)
		{
			if (string.IsNullOrEmpty(numberPlanFormat) || !Regex.IsMatch(numberPlanFormat, "^\\+?[x\\d]+$"))
			{
				string message = DataStrings.ConstraintViolationStringDoesNotMatchRegularExpression(DataStrings.NumberingPlanPatternDescription, numberPlanFormat);
				throw new ArgumentException(message);
			}
			return new UMNumberingPlanFormat(numberPlanFormat);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00031471 File Offset: 0x0002F671
		public override string ToString()
		{
			return this.numberPlanFormat;
		}

		// Token: 0x040009A1 RID: 2465
		public const string RegexPattern = "^\\+?[x\\d]+$";

		// Token: 0x040009A2 RID: 2466
		public const int MaxLength = 128;

		// Token: 0x040009A3 RID: 2467
		private string numberPlanFormat;
	}
}
