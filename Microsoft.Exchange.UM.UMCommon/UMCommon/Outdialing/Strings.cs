using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon.Outdialing
{
	// Token: 0x02000222 RID: 546
	internal static class Strings
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x0003A0A4 File Offset: 0x000382A4
		static Strings()
		{
			Strings.stringIDs.Add(1751675783U, "NumberNotInStandardFormatNoRecipient");
			Strings.stringIDs.Add(3195800463U, "SkippingTargetDialPlan");
			Strings.stringIDs.Add(1369735291U, "CanonicalizationFailed");
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0003A11B File Offset: 0x0003831B
		public static LocalizedString NumberNotInStandardFormatNoRecipient
		{
			get
			{
				return new LocalizedString("NumberNotInStandardFormatNoRecipient", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003A134 File Offset: 0x00038334
		public static LocalizedString InvalidPlayOnPhoneNumber(string phoneNumber)
		{
			return new LocalizedString("InvalidPlayOnPhoneNumber", Strings.ResourceManager, new object[]
			{
				phoneNumber
			});
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003A15C File Offset: 0x0003835C
		public static LocalizedString DialPlanPropertyNotSet(string propertyName, string dialPlan)
		{
			return new LocalizedString("DialPlanPropertyNotSet", Strings.ResourceManager, new object[]
			{
				propertyName,
				dialPlan
			});
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0003A188 File Offset: 0x00038388
		public static LocalizedString SkippingTargetDialPlan
		{
			get
			{
				return new LocalizedString("SkippingTargetDialPlan", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003A1A0 File Offset: 0x000383A0
		public static LocalizedString AccessCheckFailed(string phoneNumber)
		{
			return new LocalizedString("AccessCheckFailed", Strings.ResourceManager, new object[]
			{
				phoneNumber
			});
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003A1C8 File Offset: 0x000383C8
		public static LocalizedString InvalidRecipientPhoneLength(string recipient, string dialPlan)
		{
			return new LocalizedString("InvalidRecipientPhoneLength", Strings.ResourceManager, new object[]
			{
				recipient,
				dialPlan
			});
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003A1F4 File Offset: 0x000383F4
		public static LocalizedString NumberNotInStandardFormat(string recipient)
		{
			return new LocalizedString("NumberNotInStandardFormat", Strings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0003A21C File Offset: 0x0003841C
		public static LocalizedString CanonicalizationFailed
		{
			get
			{
				return new LocalizedString("CanonicalizationFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0003A234 File Offset: 0x00038434
		public static LocalizedString CanonicalizationResult(string phoneNumber)
		{
			return new LocalizedString("CanonicalizationResult", Strings.ResourceManager, new object[]
			{
				phoneNumber
			});
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003A25C File Offset: 0x0003845C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000898 RID: 2200
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x04000899 RID: 2201
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.UM.UMCommon.Outdialing.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000223 RID: 547
		public enum IDs : uint
		{
			// Token: 0x0400089B RID: 2203
			NumberNotInStandardFormatNoRecipient = 1751675783U,
			// Token: 0x0400089C RID: 2204
			SkippingTargetDialPlan = 3195800463U,
			// Token: 0x0400089D RID: 2205
			CanonicalizationFailed = 1369735291U
		}

		// Token: 0x02000224 RID: 548
		private enum ParamIDs
		{
			// Token: 0x0400089F RID: 2207
			InvalidPlayOnPhoneNumber,
			// Token: 0x040008A0 RID: 2208
			DialPlanPropertyNotSet,
			// Token: 0x040008A1 RID: 2209
			AccessCheckFailed,
			// Token: 0x040008A2 RID: 2210
			InvalidRecipientPhoneLength,
			// Token: 0x040008A3 RID: 2211
			NumberNotInStandardFormat,
			// Token: 0x040008A4 RID: 2212
			CanonicalizationResult
		}
	}
}
