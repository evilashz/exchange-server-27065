using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000436 RID: 1078
	[DataContract]
	internal class NumberEnumParameter : EnumParameter
	{
		// Token: 0x060035E8 RID: 13800 RVA: 0x000A73EF File Offset: 0x000A55EF
		public NumberEnumParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, int upperLimit, int lowerLimit, int defaultValue) : this(name, dialogTitle, dialogLabel, upperLimit, lowerLimit, defaultValue, null)
		{
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000A74B4 File Offset: 0x000A56B4
		public NumberEnumParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, int upperLimit, int lowerLimit, int defaultValue, IEnumerable<KeyValuePair<int, LocalizedString>> displayValues) : base(name, dialogTitle, dialogLabel, defaultValue.ToString())
		{
			if (upperLimit < lowerLimit || defaultValue > upperLimit || defaultValue < lowerLimit)
			{
				base.Values = null;
			}
			else
			{
				IEnumerable<EnumValue> source = Enumerable.Range(lowerLimit, upperLimit - lowerLimit + 1).Select(delegate(int currentNum)
				{
					string displayText = currentNum.ToString();
					if (displayValues != null)
					{
						KeyValuePair<int, LocalizedString> keyValuePair = displayValues.FirstOrDefault((KeyValuePair<int, LocalizedString> f) => f.Key == currentNum);
						if (!keyValuePair.Equals(default(KeyValuePair<int, LocalizedString>)))
						{
							displayText = keyValuePair.Value;
						}
					}
					return new EnumValue(displayText, currentNum.ToString());
				});
				base.Values = source.ToArray<EnumValue>();
			}
			base.FormletType = typeof(EnumComboBoxModalEditor);
		}
	}
}
