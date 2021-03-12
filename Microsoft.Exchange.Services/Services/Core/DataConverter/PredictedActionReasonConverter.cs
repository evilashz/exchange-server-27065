using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F8 RID: 504
	internal class PredictedActionReasonConverter : EnumConverter
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x00042FAC File Offset: 0x000411AC
		public static PredictedActionReasonType Parse(string propertyString)
		{
			ushort num = 0;
			if (!ushort.TryParse(propertyString, out num))
			{
				return PredictedActionReasonType.None;
			}
			if (Enum.IsDefined(typeof(PredictedActionReasonType), (int)num))
			{
				return (PredictedActionReasonType)num;
			}
			return PredictedActionReasonType.None;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00042FE4 File Offset: 0x000411E4
		public static string ToString(ushort propertyValue)
		{
			if (Enum.IsDefined(typeof(PredictedActionReasonType), propertyValue))
			{
				return Enum.GetName(typeof(PredictedActionReasonType), propertyValue);
			}
			return Enum.GetName(typeof(PredictedActionReasonType), PredictedActionReasonType.None);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00043033 File Offset: 0x00041233
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			return PredictedActionReasonConverter.ConvertToServiceObjectValue((ushort)propertyValue);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00043048 File Offset: 0x00041248
		internal static PredictedActionReasonType ConvertToServiceObjectValue(ushort propertyValue)
		{
			PredictedActionReasonType result = PredictedActionReasonType.None;
			if (Enum.IsDefined(typeof(PredictedActionReasonType), (int)propertyValue))
			{
				result = (PredictedActionReasonType)propertyValue;
			}
			return result;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00043071 File Offset: 0x00041271
		public override object ConvertToObject(string propertyString)
		{
			return PredictedActionReasonConverter.Parse(propertyString);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0004307E File Offset: 0x0004127E
		public override string ConvertToString(object propertyValue)
		{
			return PredictedActionReasonConverter.ToString((ushort)propertyValue);
		}
	}
}
