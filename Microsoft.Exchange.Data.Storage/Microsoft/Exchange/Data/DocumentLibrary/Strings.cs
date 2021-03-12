using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x02000132 RID: 306
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Strings
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x0006B114 File Offset: 0x00069314
		static Strings()
		{
			Strings.stringIDs.Add(926468432U, "ExCorruptData");
			Strings.stringIDs.Add(2758509155U, "ExProxyConnectionFailure");
			Strings.stringIDs.Add(2298744173U, "ExUnknownError");
			Strings.stringIDs.Add(2403235179U, "ExCorruptRegionalSetting");
			Strings.stringIDs.Add(1158351855U, "ExConnectionFailure");
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0006B1B3 File Offset: 0x000693B3
		public static string ExCorruptData
		{
			get
			{
				return Strings.ResourceManager.GetString("ExCorruptData");
			}
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0006B1C4 File Offset: 0x000693C4
		public static string ExCannotConnectToMachine(string machineName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExCannotConnectToMachine"), machineName);
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0006B1DB File Offset: 0x000693DB
		public static string ExProxyConnectionFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("ExProxyConnectionFailure");
			}
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0006B1EC File Offset: 0x000693EC
		public static string ExObjectMovedOrDeleted(string fileName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExObjectMovedOrDeleted"), fileName);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0006B203 File Offset: 0x00069403
		public static string ExFilterNotSupported(Type type)
		{
			return string.Format(Strings.ResourceManager.GetString("ExFilterNotSupported"), type);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0006B21A File Offset: 0x0006941A
		public static string ExDocumentStreamAccessDenied(string fileName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExDocumentStreamAccessDenied"), fileName);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0006B231 File Offset: 0x00069431
		public static string ExDocumentModified(string fileName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExDocumentModified"), fileName);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0006B248 File Offset: 0x00069448
		public static string ExAccessDeniedForGetViewUnder(string directoryName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExAccessDeniedForGetViewUnder"), directoryName);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0006B25F File Offset: 0x0006945F
		public static string ExAccessDenied(object targetObject)
		{
			return string.Format(Strings.ResourceManager.GetString("ExAccessDenied"), targetObject);
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0006B276 File Offset: 0x00069476
		public static string ExUnknownError
		{
			get
			{
				return Strings.ResourceManager.GetString("ExUnknownError");
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0006B287 File Offset: 0x00069487
		public static string ExCorruptRegionalSetting
		{
			get
			{
				return Strings.ResourceManager.GetString("ExCorruptRegionalSetting");
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0006B298 File Offset: 0x00069498
		public static string ExConnectionFailure
		{
			get
			{
				return Strings.ResourceManager.GetString("ExConnectionFailure");
			}
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0006B2A9 File Offset: 0x000694A9
		public static string ExObjectNotFound(string fileName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExObjectNotFound"), fileName);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0006B2C0 File Offset: 0x000694C0
		public static string ExPathTooLong(string fileName)
		{
			return string.Format(Strings.ResourceManager.GetString("ExPathTooLong"), fileName);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0006B2D7 File Offset: 0x000694D7
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x040009C6 RID: 2502
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(5);

		// Token: 0x040009C7 RID: 2503
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Data.DocumentLibrary.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000133 RID: 307
		public enum IDs : uint
		{
			// Token: 0x040009C9 RID: 2505
			ExCorruptData = 926468432U,
			// Token: 0x040009CA RID: 2506
			ExProxyConnectionFailure = 2758509155U,
			// Token: 0x040009CB RID: 2507
			ExUnknownError = 2298744173U,
			// Token: 0x040009CC RID: 2508
			ExCorruptRegionalSetting = 2403235179U,
			// Token: 0x040009CD RID: 2509
			ExConnectionFailure = 1158351855U
		}

		// Token: 0x02000134 RID: 308
		private enum ParamIDs
		{
			// Token: 0x040009CF RID: 2511
			ExCannotConnectToMachine,
			// Token: 0x040009D0 RID: 2512
			ExObjectMovedOrDeleted,
			// Token: 0x040009D1 RID: 2513
			ExFilterNotSupported,
			// Token: 0x040009D2 RID: 2514
			ExDocumentStreamAccessDenied,
			// Token: 0x040009D3 RID: 2515
			ExDocumentModified,
			// Token: 0x040009D4 RID: 2516
			ExAccessDeniedForGetViewUnder,
			// Token: 0x040009D5 RID: 2517
			ExAccessDenied,
			// Token: 0x040009D6 RID: 2518
			ExObjectNotFound,
			// Token: 0x040009D7 RID: 2519
			ExPathTooLong
		}
	}
}
