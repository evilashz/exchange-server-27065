using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001DD RID: 477
	internal class BusyTypeConverter : EnumConverter
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x00041BCC File Offset: 0x0003FDCC
		public static BusyType Parse(string busyTypeValue)
		{
			if (busyTypeValue != null)
			{
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000c17-1 == null)
				{
					<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000c17-1 = new Dictionary<string, int>(6)
					{
						{
							"Busy",
							0
						},
						{
							"Free",
							1
						},
						{
							"OOF",
							2
						},
						{
							"Tentative",
							3
						},
						{
							"WorkingElsewhere",
							4
						},
						{
							"NoData",
							5
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000c17-1.TryGetValue(busyTypeValue, out num))
				{
					BusyType result;
					switch (num)
					{
					case 0:
						result = BusyType.Busy;
						break;
					case 1:
						result = BusyType.Free;
						break;
					case 2:
						result = BusyType.OOF;
						break;
					case 3:
						result = BusyType.Tentative;
						break;
					case 4:
						if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
						{
							result = BusyType.WorkingElseWhere;
						}
						else
						{
							result = BusyType.Free;
						}
						break;
					case 5:
						result = BusyType.Unknown;
						break;
					default:
						goto IL_C4;
					}
					return result;
				}
			}
			IL_C4:
			throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Invalid busyType string: {0}", new object[]
			{
				busyTypeValue
			}));
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00041CC0 File Offset: 0x0003FEC0
		public static string ToString(BusyType propertyValue)
		{
			string result = null;
			switch (propertyValue)
			{
			case BusyType.Unknown:
				result = "NoData";
				break;
			case BusyType.Free:
				result = "Free";
				break;
			case BusyType.Tentative:
				result = "Tentative";
				break;
			case BusyType.Busy:
				result = "Busy";
				break;
			case BusyType.OOF:
				result = "OOF";
				break;
			case BusyType.WorkingElseWhere:
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
				{
					result = "WorkingElsewhere";
				}
				else
				{
					result = "Free";
				}
				break;
			}
			return result;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00041D3B File Offset: 0x0003FF3B
		public override object ConvertToObject(string propertyString)
		{
			return BusyTypeConverter.Parse(propertyString);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00041D48 File Offset: 0x0003FF48
		public override string ConvertToString(object propertyValue)
		{
			return BusyTypeConverter.ToString((BusyType)propertyValue);
		}

		// Token: 0x04000A51 RID: 2641
		private const string BusyStringValue = "Busy";

		// Token: 0x04000A52 RID: 2642
		private const string FreeStringValue = "Free";

		// Token: 0x04000A53 RID: 2643
		private const string OOFStringValue = "OOF";

		// Token: 0x04000A54 RID: 2644
		private const string TentativeStringValue = "Tentative";

		// Token: 0x04000A55 RID: 2645
		private const string WorkingElsewhereStringValue = "WorkingElsewhere";

		// Token: 0x04000A56 RID: 2646
		private const string UnknownStringValue = "NoData";
	}
}
