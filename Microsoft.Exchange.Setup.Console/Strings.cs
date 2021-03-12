using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Console
{
	// Token: 0x02000003 RID: 3
	internal static class Strings
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000252C File Offset: 0x0000072C
		static Strings()
		{
			Strings.stringIDs.Add(1222440996U, "Banner");
			Strings.stringIDs.Add(792205043U, "PrereqCheckBanner");
			Strings.stringIDs.Add(1695043038U, "SetupExitsBecauseOfLPPathNotFoundException");
			Strings.stringIDs.Add(1251728767U, "ConfiguringExchangeServer");
			Strings.stringIDs.Add(1331356275U, "CabUtilityWrapperError");
			Strings.stringIDs.Add(2883940283U, "AdditionalErrorDetails");
			Strings.stringIDs.Add(3712765806U, "LPVersioningInvalidValue");
			Strings.stringIDs.Add(951135015U, "SuccessSummary");
			Strings.stringIDs.Add(2390584075U, "TreatPreReqErrorsAsWarnings");
			Strings.stringIDs.Add(4061842413U, "UnableToFindLPVersioning");
			Strings.stringIDs.Add(3856638130U, "SetupExitsBecauseOfTransientException");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002644 File Offset: 0x00000844
		public static LocalizedString Banner
		{
			get
			{
				return new LocalizedString("Banner", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000265C File Offset: 0x0000085C
		public static LocalizedString ExsetupTerminatedWithControlBreak(string message)
		{
			return new LocalizedString("ExsetupTerminatedWithControlBreak", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002684 File Offset: 0x00000884
		public static LocalizedString PrereqCheckBanner
		{
			get
			{
				return new LocalizedString("PrereqCheckBanner", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000269B File Offset: 0x0000089B
		public static LocalizedString SetupExitsBecauseOfLPPathNotFoundException
		{
			get
			{
				return new LocalizedString("SetupExitsBecauseOfLPPathNotFoundException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000026B2 File Offset: 0x000008B2
		public static LocalizedString ConfiguringExchangeServer
		{
			get
			{
				return new LocalizedString("ConfiguringExchangeServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000026C9 File Offset: 0x000008C9
		public static LocalizedString CabUtilityWrapperError
		{
			get
			{
				return new LocalizedString("CabUtilityWrapperError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000026E0 File Offset: 0x000008E0
		public static LocalizedString AdditionalErrorDetails
		{
			get
			{
				return new LocalizedString("AdditionalErrorDetails", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000026F7 File Offset: 0x000008F7
		public static LocalizedString LPVersioningInvalidValue
		{
			get
			{
				return new LocalizedString("LPVersioningInvalidValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002710 File Offset: 0x00000910
		public static LocalizedString CannotFindFile(string file)
		{
			return new LocalizedString("CannotFindFile", Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002738 File Offset: 0x00000938
		public static LocalizedString ExecutionResult(int index, string result)
		{
			return new LocalizedString("ExecutionResult", Strings.ResourceManager, new object[]
			{
				index,
				result
			});
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002769 File Offset: 0x00000969
		public static LocalizedString SuccessSummary
		{
			get
			{
				return new LocalizedString("SuccessSummary", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002780 File Offset: 0x00000980
		public static LocalizedString TreatPreReqErrorsAsWarnings
		{
			get
			{
				return new LocalizedString("TreatPreReqErrorsAsWarnings", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002797 File Offset: 0x00000997
		public static LocalizedString UnableToFindLPVersioning
		{
			get
			{
				return new LocalizedString("UnableToFindLPVersioning", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027B0 File Offset: 0x000009B0
		public static LocalizedString UnhandledErrorMessage(string message)
		{
			return new LocalizedString("UnhandledErrorMessage", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000027D8 File Offset: 0x000009D8
		public static LocalizedString SetupExitsBecauseOfTransientException
		{
			get
			{
				return new LocalizedString("SetupExitsBecauseOfTransientException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027EF File Offset: 0x000009EF
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(11);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.Console.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000004 RID: 4
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			Banner = 1222440996U,
			// Token: 0x04000005 RID: 5
			PrereqCheckBanner = 792205043U,
			// Token: 0x04000006 RID: 6
			SetupExitsBecauseOfLPPathNotFoundException = 1695043038U,
			// Token: 0x04000007 RID: 7
			ConfiguringExchangeServer = 1251728767U,
			// Token: 0x04000008 RID: 8
			CabUtilityWrapperError = 1331356275U,
			// Token: 0x04000009 RID: 9
			AdditionalErrorDetails = 2883940283U,
			// Token: 0x0400000A RID: 10
			LPVersioningInvalidValue = 3712765806U,
			// Token: 0x0400000B RID: 11
			SuccessSummary = 951135015U,
			// Token: 0x0400000C RID: 12
			TreatPreReqErrorsAsWarnings = 2390584075U,
			// Token: 0x0400000D RID: 13
			UnableToFindLPVersioning = 4061842413U,
			// Token: 0x0400000E RID: 14
			SetupExitsBecauseOfTransientException = 3856638130U
		}

		// Token: 0x02000005 RID: 5
		private enum ParamIDs
		{
			// Token: 0x04000010 RID: 16
			ExsetupTerminatedWithControlBreak,
			// Token: 0x04000011 RID: 17
			CannotFindFile,
			// Token: 0x04000012 RID: 18
			ExecutionResult,
			// Token: 0x04000013 RID: 19
			UnhandledErrorMessage
		}
	}
}
