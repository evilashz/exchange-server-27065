using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Configuration.FailFast.LocStrings
{
	// Token: 0x02000015 RID: 21
	internal static class Strings
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000043E0 File Offset: 0x000025E0
		static Strings()
		{
			Strings.stringIDs.Add(2782861920U, "FailBecauseOfServer");
			Strings.stringIDs.Add(2406874761U, "FailBecauseOfSelf");
			Strings.stringIDs.Add(1130355941U, "ErrorRpsNotEnabled");
			Strings.stringIDs.Add(362638241U, "FailBecauseOfTenant");
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000446B File Offset: 0x0000266B
		public static string RequestBeingBlockedInFailFast(string failedReason)
		{
			return string.Format(Strings.ResourceManager.GetString("RequestBeingBlockedInFailFast"), failedReason);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004482 File Offset: 0x00002682
		public static string FailBecauseOfServer
		{
			get
			{
				return Strings.ResourceManager.GetString("FailBecauseOfServer");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004493 File Offset: 0x00002693
		public static string FailBecauseOfSelf
		{
			get
			{
				return Strings.ResourceManager.GetString("FailBecauseOfSelf");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000044A4 File Offset: 0x000026A4
		public static string ErrorRpsNotEnabled
		{
			get
			{
				return Strings.ResourceManager.GetString("ErrorRpsNotEnabled");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000044B5 File Offset: 0x000026B5
		public static string FailBecauseOfTenant
		{
			get
			{
				return Strings.ResourceManager.GetString("FailBecauseOfTenant");
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000044C6 File Offset: 0x000026C6
		public static string ErrorOperationTarpitting(int delaySeconds)
		{
			return string.Format(Strings.ResourceManager.GetString("ErrorOperationTarpitting"), delaySeconds);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000044E2 File Offset: 0x000026E2
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x04000044 RID: 68
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(4);

		// Token: 0x04000045 RID: 69
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Configuration.FailFast.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000016 RID: 22
		public enum IDs : uint
		{
			// Token: 0x04000047 RID: 71
			FailBecauseOfServer = 2782861920U,
			// Token: 0x04000048 RID: 72
			FailBecauseOfSelf = 2406874761U,
			// Token: 0x04000049 RID: 73
			ErrorRpsNotEnabled = 1130355941U,
			// Token: 0x0400004A RID: 74
			FailBecauseOfTenant = 362638241U
		}

		// Token: 0x02000017 RID: 23
		private enum ParamIDs
		{
			// Token: 0x0400004C RID: 76
			RequestBeingBlockedInFailFast,
			// Token: 0x0400004D RID: 77
			ErrorOperationTarpitting
		}
	}
}
