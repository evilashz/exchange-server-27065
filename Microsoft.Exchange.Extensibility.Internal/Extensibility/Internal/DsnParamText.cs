using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000046 RID: 70
	internal class DsnParamText
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00010640 File Offset: 0x0000E840
		private static bool IsMessageSizeTextInKB(long maxSize, long currentSize, CultureInfo culture, out string maxSizeText, out string currentSizeText)
		{
			bool result = true;
			if (maxSize >= 1024L && currentSize >= 1024L)
			{
				maxSize >>= 10;
				currentSize >>= 10;
				result = false;
			}
			if (currentSize == maxSize)
			{
				currentSize += 1L;
			}
			maxSizeText = (culture.IsNeutralCulture ? maxSize.ToString() : maxSize.ToString(culture));
			currentSizeText = (culture.IsNeutralCulture ? currentSize.ToString() : currentSize.ToString(culture));
			return result;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000106B1 File Offset: 0x0000E8B1
		private DsnParamText(DsnParamItem[] dsnParamItems)
		{
			this.dsnParamItems = dsnParamItems;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000106C0 File Offset: 0x0000E8C0
		public string[] GenerateTexts(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
		{
			List<string> list = null;
			bool flag = false;
			if (dsnParameters != null)
			{
				foreach (DsnParamItem dsnParamItem in this.dsnParamItems)
				{
					string @string = dsnParamItem.GetString(dsnParameters, culture, out overwriteDefault);
					if (overwriteDefault)
					{
						flag = true;
					}
					if (!string.IsNullOrEmpty(@string))
					{
						if (list == null)
						{
							list = new List<string>();
						}
						list.Add(@string);
					}
				}
			}
			overwriteDefault = flag;
			if (list != null)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00010A84 File Offset: 0x0000EC84
		// Note: this type is marked as 'beforefieldinit'.
		static DsnParamText()
		{
			DsnParamItem[] array = new DsnParamItem[2];
			array[0] = new DsnParamItem(new string[]
			{
				"MaxRecipMessageSizeInKB",
				"CurrentMessageSizeInKB"
			}, delegate(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
			{
				long maxSize = (long)dsnParameters["MaxRecipMessageSizeInKB"];
				long currentSize = (long)dsnParameters["CurrentMessageSizeInKB"];
				overwriteDefault = false;
				string maxSize2;
				string currentSize2;
				if (!DsnParamText.IsMessageSizeTextInKB(maxSize, currentSize, culture, out maxSize2, out currentSize2))
				{
					return SystemMessages.DsnParamTextMessageSizePerRecipientInMB(currentSize2, maxSize2).ToString(culture);
				}
				return SystemMessages.DsnParamTextMessageSizePerRecipientInKB(currentSize2, maxSize2).ToString(culture);
			});
			array[1] = new DsnParamItem(new string[]
			{
				"MapiMessageClass",
				"TextMessagingDeliveryPointType",
				"TextMessagingBodyText",
				"TextMessagingRecipientPhoneNumber",
				"TextMessagingRecipientCarrier",
				"TextMessagingRecipientExceptions"
			}, delegate(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
			{
				string text = (string)dsnParameters["MapiMessageClass"];
				string text2 = (string)dsnParameters["TextMessagingDeliveryPointType"];
				string text3 = (string)dsnParameters["TextMessagingBodyText"];
				string number = (string)dsnParameters["TextMessagingRecipientPhoneNumber"];
				string carrier = (string)dsnParameters["TextMessagingRecipientCarrier"];
				IList<Exception> list = (IList<Exception>)dsnParameters["TextMessagingRecipientExceptions"];
				overwriteDefault = true;
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				while (list.Count > num)
				{
					Exception ex = list[num];
					if (list.Count - 1 == num)
					{
						if (ex is LocalizedException)
						{
							stringBuilder.Append(((LocalizedException)ex).LocalizedString.ToString(culture));
						}
						else
						{
							stringBuilder.AppendLine(ex.Message);
						}
					}
					else if (ex is LocalizedException)
					{
						stringBuilder.Append("<BR><BR>" + ((LocalizedException)ex).LocalizedString.ToString(culture));
					}
					else
					{
						stringBuilder.AppendLine("<BR><BR>" + ex.Message);
					}
					num++;
				}
				string text4 = stringBuilder.ToString();
				if (text.StartsWith("IPM.Note.Mobile.SMS.Alert", StringComparison.OrdinalIgnoreCase))
				{
					if (string.IsNullOrEmpty(text4))
					{
						return SystemMessages.HumanTextFailedSmtpToSmsGatewayNotification(number, carrier, text3).ToString(culture);
					}
					if (string.IsNullOrEmpty(text3) && text2.Equals("ExchangeActiveSync", StringComparison.OrdinalIgnoreCase))
					{
						return text4;
					}
					return SystemMessages.HumanTextFailedOmsNotification(text3, text4).ToString(culture);
				}
				else
				{
					if (!text.StartsWith("IPM.Note.Mobile.SMS.Undercurrent", StringComparison.OrdinalIgnoreCase))
					{
						return text4;
					}
					if (string.IsNullOrEmpty(text4))
					{
						return SystemMessages.HumanTextFailedPasscodeWithoutReason(number);
					}
					return SystemMessages.HumanTextFailedPasscodeWithReason(number, text4);
				}
			});
			DsnParamText.PerRecipientItems = array;
			DsnParamItem[] array2 = new DsnParamItem[3];
			array2[0] = new DsnParamItem(new string[]
			{
				"MaxMessageSizeInKB",
				"CurrentMessageSizeInKB"
			}, delegate(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
			{
				long maxSize = (long)dsnParameters["MaxMessageSizeInKB"];
				long currentSize = (long)dsnParameters["CurrentMessageSizeInKB"];
				overwriteDefault = true;
				string maxSize2;
				string currentSize2;
				if (!DsnParamText.IsMessageSizeTextInKB(maxSize, currentSize, culture, out maxSize2, out currentSize2))
				{
					return SystemMessages.DsnParamTextMessageSizePerMessageInMB(currentSize2, maxSize2).ToString(culture);
				}
				return SystemMessages.DsnParamTextMessageSizePerMessageInKB(currentSize2, maxSize2).ToString(culture);
			});
			array2[1] = new DsnParamItem(new string[]
			{
				"MaxRecipientCount",
				"CurrentRecipientCount"
			}, delegate(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
			{
				int num = (int)dsnParameters["MaxRecipientCount"];
				int num2 = (int)dsnParameters["CurrentRecipientCount"];
				overwriteDefault = true;
				string maxRecipientCount = culture.IsNeutralCulture ? num.ToString() : num.ToString(culture);
				string currentRecipientCount = culture.IsNeutralCulture ? num2.ToString() : num2.ToString(culture);
				return SystemMessages.DsnParamTextRecipientCount(currentRecipientCount, maxRecipientCount).ToString(culture);
			});
			array2[2] = new DsnParamItem(new string[]
			{
				"MapiMessageClass"
			}, delegate(DsnParameters dsnParameters, CultureInfo culture, out bool overwriteDefault)
			{
				string text = (string)dsnParameters["MapiMessageClass"];
				overwriteDefault = true;
				if (text.StartsWith("IPM.Note.Mobile.SMS.Alert", StringComparison.OrdinalIgnoreCase))
				{
					return SystemMessages.FailedHumanReadableTopTextForTextMessageNotification.ToString(culture);
				}
				return SystemMessages.FailedHumanReadableTopTextForTextMessage.ToString(culture);
			});
			DsnParamText.PerMessageItems = array2;
			DsnParamText.PerMessageDsnParamText = new DsnParamText(DsnParamText.PerMessageItems);
			DsnParamText.PerRecipientDsnParamText = new DsnParamText(DsnParamText.PerRecipientItems);
		}

		// Token: 0x0400032E RID: 814
		public const string MaxRecipMesageSizeInKB = "MaxRecipMessageSizeInKB";

		// Token: 0x0400032F RID: 815
		public const string MaxMessageSizeInKB = "MaxMessageSizeInKB";

		// Token: 0x04000330 RID: 816
		public const string CurrentMessageSizeInKB = "CurrentMessageSizeInKB";

		// Token: 0x04000331 RID: 817
		public const string MaxRecipientCount = "MaxRecipientCount";

		// Token: 0x04000332 RID: 818
		public const string CurrentRecipientCount = "CurrentRecipientCount";

		// Token: 0x04000333 RID: 819
		public const string MapiMessageClass = "MapiMessageClass";

		// Token: 0x04000334 RID: 820
		public const string TextMessagingDeliveryPointType = "TextMessagingDeliveryPointType";

		// Token: 0x04000335 RID: 821
		public const string TextMessagingBodyText = "TextMessagingBodyText";

		// Token: 0x04000336 RID: 822
		public const string TextMessagingRecipientPhoneNumber = "TextMessagingRecipientPhoneNumber";

		// Token: 0x04000337 RID: 823
		public const string TextMessagingRecipientCarrier = "TextMessagingRecipientCarrier";

		// Token: 0x04000338 RID: 824
		public const string TextMessagingRecipientExceptions = "TextMessagingRecipientExceptions";

		// Token: 0x04000339 RID: 825
		private const string MessageClassSms = "IPM.Note.Mobile.SMS";

		// Token: 0x0400033A RID: 826
		private const string MessageClassSmsAlert = "IPM.Note.Mobile.SMS.Alert";

		// Token: 0x0400033B RID: 827
		private const string MessageClassSmsUndercurrent = "IPM.Note.Mobile.SMS.Undercurrent";

		// Token: 0x0400033C RID: 828
		private const string DeliveryPointTypeSmtpToSmsGateway = "SmtpToSmsGateway";

		// Token: 0x0400033D RID: 829
		private const string DeliveryPointTypeOutlookMobileService = "OutlookMobileService";

		// Token: 0x0400033E RID: 830
		private const string DeliveryPointTypeExchangeActiveSync = "ExchangeActiveSync";

		// Token: 0x0400033F RID: 831
		private static readonly DsnParamItem[] PerRecipientItems;

		// Token: 0x04000340 RID: 832
		private static readonly DsnParamItem[] PerMessageItems;

		// Token: 0x04000341 RID: 833
		public static readonly DsnParamText PerMessageDsnParamText;

		// Token: 0x04000342 RID: 834
		public static readonly DsnParamText PerRecipientDsnParamText;

		// Token: 0x04000343 RID: 835
		private DsnParamItem[] dsnParamItems;
	}
}
