using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000103 RID: 259
	internal static class SystemStrings
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x00016F2C File Offset: 0x0001512C
		static SystemStrings()
		{
			SystemStrings.stringIDs.Add(4266870920U, "InvalidDaylightTransition");
			SystemStrings.stringIDs.Add(3036516007U, "RegistryKeyDoesNotContainValidTimeZone");
			SystemStrings.stringIDs.Add(1348502081U, "TimeZoneKeyNameTooLong");
			SystemStrings.stringIDs.Add(522392603U, "NoPrivateKey");
			SystemStrings.stringIDs.Add(2126902361U, "InvalidSystemTime");
			SystemStrings.stringIDs.Add(874632872U, "CouldNotAccessPrivateKey");
			SystemStrings.stringIDs.Add(4261244944U, "InvalidNthDayOfWeek");
			SystemStrings.stringIDs.Add(2227579325U, "WrongPrivateKeyType");
			SystemStrings.stringIDs.Add(1181227695U, "PrivateKeyInvalid");
			SystemStrings.stringIDs.Add(1877416323U, "FailedToAddAccessRule");
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00017030 File Offset: 0x00015230
		public static string InvalidDaylightTransition
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("InvalidDaylightTransition");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00017041 File Offset: 0x00015241
		public static string RegistryKeyDoesNotContainValidTimeZone
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("RegistryKeyDoesNotContainValidTimeZone");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00017052 File Offset: 0x00015252
		public static string TimeZoneKeyNameTooLong
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("TimeZoneKeyNameTooLong");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00017063 File Offset: 0x00015263
		public static string NoPrivateKey
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("NoPrivateKey");
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017074 File Offset: 0x00015274
		public static string InvalidRelativeTransitionDay(int day)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("InvalidRelativeTransitionDay"), day);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017090 File Offset: 0x00015290
		public static string UseLocalizableStringResource(string locMethod)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("UseLocalizableStringResource"), locMethod);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000170A7 File Offset: 0x000152A7
		public static string FailedToOpenRegistryKey(string path)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("FailedToOpenRegistryKey"), path);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000170BE File Offset: 0x000152BE
		public static string InvalidSystemTime
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("InvalidSystemTime");
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000170CF File Offset: 0x000152CF
		public static string InvalidBaseType(Type enumType)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("InvalidBaseType"), enumType);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000170E6 File Offset: 0x000152E6
		public static string InvalidTypeParam(Type enumType)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("InvalidTypeParam"), enumType);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x000170FD File Offset: 0x000152FD
		public static string CouldNotAccessPrivateKey
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("CouldNotAccessPrivateKey");
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001710E File Offset: 0x0001530E
		public static string BadEnumValue(Type enumType, object invalidValue)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("BadEnumValue"), enumType, invalidValue);
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00017126 File Offset: 0x00015326
		public static string InvalidNthDayOfWeek
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("InvalidNthDayOfWeek");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00017137 File Offset: 0x00015337
		public static string WrongPrivateKeyType
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("WrongPrivateKeyType");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00017148 File Offset: 0x00015348
		public static string PrivateKeyInvalid
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("PrivateKeyInvalid");
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00017159 File Offset: 0x00015359
		public static string Win32GetTimeZoneInformationFailed(int error)
		{
			return string.Format(SystemStrings.ResourceManager.GetString("Win32GetTimeZoneInformationFailed"), error);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00017175 File Offset: 0x00015375
		public static string FailedToAddAccessRule
		{
			get
			{
				return SystemStrings.ResourceManager.GetString("FailedToAddAccessRule");
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00017186 File Offset: 0x00015386
		public static string GetLocalizedString(SystemStrings.IDs key)
		{
			return SystemStrings.ResourceManager.GetString(SystemStrings.stringIDs[(uint)key]);
		}

		// Token: 0x0400052F RID: 1327
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(10);

		// Token: 0x04000530 RID: 1328
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Net.SystemStrings", typeof(SystemStrings).GetTypeInfo().Assembly);

		// Token: 0x02000104 RID: 260
		public enum IDs : uint
		{
			// Token: 0x04000532 RID: 1330
			InvalidDaylightTransition = 4266870920U,
			// Token: 0x04000533 RID: 1331
			RegistryKeyDoesNotContainValidTimeZone = 3036516007U,
			// Token: 0x04000534 RID: 1332
			TimeZoneKeyNameTooLong = 1348502081U,
			// Token: 0x04000535 RID: 1333
			NoPrivateKey = 522392603U,
			// Token: 0x04000536 RID: 1334
			InvalidSystemTime = 2126902361U,
			// Token: 0x04000537 RID: 1335
			CouldNotAccessPrivateKey = 874632872U,
			// Token: 0x04000538 RID: 1336
			InvalidNthDayOfWeek = 4261244944U,
			// Token: 0x04000539 RID: 1337
			WrongPrivateKeyType = 2227579325U,
			// Token: 0x0400053A RID: 1338
			PrivateKeyInvalid = 1181227695U,
			// Token: 0x0400053B RID: 1339
			FailedToAddAccessRule = 1877416323U
		}

		// Token: 0x02000105 RID: 261
		private enum ParamIDs
		{
			// Token: 0x0400053D RID: 1341
			InvalidRelativeTransitionDay,
			// Token: 0x0400053E RID: 1342
			UseLocalizableStringResource,
			// Token: 0x0400053F RID: 1343
			FailedToOpenRegistryKey,
			// Token: 0x04000540 RID: 1344
			InvalidBaseType,
			// Token: 0x04000541 RID: 1345
			InvalidTypeParam,
			// Token: 0x04000542 RID: 1346
			BadEnumValue,
			// Token: 0x04000543 RID: 1347
			Win32GetTimeZoneInformationFailed
		}
	}
}
