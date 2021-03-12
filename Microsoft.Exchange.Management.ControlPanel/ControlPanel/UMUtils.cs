using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004D2 RID: 1234
	internal static class UMUtils
	{
		// Token: 0x06003C69 RID: 15465 RVA: 0x000B57AC File Offset: 0x000B39AC
		public static string GetAudioQualityIconAndAlternateText(float? nmos, out string alternateText)
		{
			float? num = nmos;
			float num2 = (num != null) ? num.GetValueOrDefault() : AudioQuality.UnknownValue;
			switch (AudioQuality.GetQualityOfAudio(num2))
			{
			case 0:
				alternateText = Strings.UMAudioQualityExcellent;
				return CommandSprite.GetCssClass(CommandSprite.SpriteId.AudioQualityFiveBars);
			case 1:
				alternateText = Strings.UMAudioQualityGood;
				return CommandSprite.GetCssClass(CommandSprite.SpriteId.AudioQualityFourBars);
			case 2:
				alternateText = Strings.UMAudioQualityAverage;
				return CommandSprite.GetCssClass(CommandSprite.SpriteId.AudioQualityThreeBars);
			case 3:
				alternateText = Strings.UMAudioQualityPoor;
				return CommandSprite.GetCssClass(CommandSprite.SpriteId.AudioQualityTwoBars);
			case 4:
				alternateText = Strings.UMAudioQualityBad;
				return CommandSprite.GetCssClass(CommandSprite.SpriteId.AudioQualityOneBar);
			}
			alternateText = Strings.UMAudioQualityNotAvailable;
			return CommandSprite.GetCssClass(CommandSprite.SpriteId.AudioQualityNotAvailable);
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x000B5874 File Offset: 0x000B3A74
		public static string AppendMillisecondSuffix(string metric)
		{
			if (string.IsNullOrEmpty(metric))
			{
				return string.Empty;
			}
			return Strings.UMCallDataRecordMillisecondSuffix(metric);
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x000B5890 File Offset: 0x000B3A90
		public static Identity CreateUniqueUMReportingRowIdentity()
		{
			return new Identity(Guid.NewGuid().ToString(), Strings.UMAudioQualityDetails);
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x000B58C0 File Offset: 0x000B3AC0
		public static string FormatFloat(float? metric)
		{
			if (metric == null)
			{
				return string.Empty;
			}
			return metric.Value.ToString("F1");
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x000B58F1 File Offset: 0x000B3AF1
		public static string FormatAudioQualityMetricDisplay(string metric)
		{
			if (string.IsNullOrEmpty(metric))
			{
				return "-";
			}
			return metric;
		}

		// Token: 0x040027A0 RID: 10144
		internal const string PercentFormatString = "#0.0%";

		// Token: 0x040027A1 RID: 10145
		internal const string UMReportNoDataString = "-";
	}
}
