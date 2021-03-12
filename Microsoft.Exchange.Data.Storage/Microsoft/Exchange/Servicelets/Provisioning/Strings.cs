using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000135 RID: 309
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Strings
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0006B2F0 File Offset: 0x000694F0
		static Strings()
		{
			Strings.stringIDs.Add(1246705638U, "LabelLogMailFooter");
			Strings.stringIDs.Add(1416516349U, "UnknownProvisioningType");
			Strings.stringIDs.Add(2604316530U, "PermissionDenied");
			Strings.stringIDs.Add(2795084079U, "LabelStartTime");
			Strings.stringIDs.Add(3123046798U, "LabelReportMailErrorHeaderCancel");
			Strings.stringIDs.Add(886329592U, "ReportSubject");
			Strings.stringIDs.Add(1921063387U, "ErrorReportFileName");
			Strings.stringIDs.Add(2079790351U, "LabelTotalRowsProcessed");
			Strings.stringIDs.Add(246748372U, "LabelReportMailErrorHeader");
			Strings.stringIDs.Add(283214974U, "LabelNewUsers");
			Strings.stringIDs.Add(345539229U, "LabelErrors");
			Strings.stringIDs.Add(3889683156U, "UnknownProvisioningOwner");
			Strings.stringIDs.Add(3917832382U, "LabelReportMailHeaderCancel");
			Strings.stringIDs.Add(3597640497U, "ReportSubjectCanceled");
			Strings.stringIDs.Add(3346179848U, "LabelStartedBy");
			Strings.stringIDs.Add(2060675488U, "LabelRunTime");
			Strings.stringIDs.Add(1144436407U, "LabelFileName");
			Strings.stringIDs.Add(2937969650U, "HelpText");
			Strings.stringIDs.Add(3449564964U, "LabelReportMailHeader");
			Strings.stringIDs.Add(264350496U, "ErrorSummary");
			Strings.stringIDs.Add(5503809U, "LabelTotalRows");
			Strings.stringIDs.Add(2821624239U, "HelpURLText");
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0006B4E4 File Offset: 0x000696E4
		public static LocalizedString LabelLogMailFooter
		{
			get
			{
				return new LocalizedString("LabelLogMailFooter", "Ex333202", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0006B502 File Offset: 0x00069702
		public static LocalizedString UnknownProvisioningType
		{
			get
			{
				return new LocalizedString("UnknownProvisioningType", "Ex5FE00C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0006B520 File Offset: 0x00069720
		public static LocalizedString PermissionDenied
		{
			get
			{
				return new LocalizedString("PermissionDenied", "Ex431543", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0006B53E File Offset: 0x0006973E
		public static LocalizedString LabelStartTime
		{
			get
			{
				return new LocalizedString("LabelStartTime", "Ex301A0B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0006B55C File Offset: 0x0006975C
		public static LocalizedString UsedEmailAddress(string email, string otherUserID)
		{
			return new LocalizedString("UsedEmailAddress", "ExD52E47", false, true, Strings.ResourceManager, new object[]
			{
				email,
				otherUserID
			});
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0006B58F File Offset: 0x0006978F
		public static LocalizedString LabelReportMailErrorHeaderCancel
		{
			get
			{
				return new LocalizedString("LabelReportMailErrorHeaderCancel", "Ex5BDA91", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0006B5AD File Offset: 0x000697AD
		public static LocalizedString ReportSubject
		{
			get
			{
				return new LocalizedString("ReportSubject", "ExB47335", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0006B5CB File Offset: 0x000697CB
		public static LocalizedString ErrorReportFileName
		{
			get
			{
				return new LocalizedString("ErrorReportFileName", "Ex098155", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0006B5E9 File Offset: 0x000697E9
		public static LocalizedString LabelTotalRowsProcessed
		{
			get
			{
				return new LocalizedString("LabelTotalRowsProcessed", "Ex1FE283", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0006B607 File Offset: 0x00069807
		public static LocalizedString LabelReportMailErrorHeader
		{
			get
			{
				return new LocalizedString("LabelReportMailErrorHeader", "ExAEEA91", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0006B625 File Offset: 0x00069825
		public static LocalizedString LabelNewUsers
		{
			get
			{
				return new LocalizedString("LabelNewUsers", "Ex097570", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0006B643 File Offset: 0x00069843
		public static LocalizedString LabelErrors
		{
			get
			{
				return new LocalizedString("LabelErrors", "ExF637C8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0006B661 File Offset: 0x00069861
		public static LocalizedString UnknownProvisioningOwner
		{
			get
			{
				return new LocalizedString("UnknownProvisioningOwner", "Ex6A6645", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0006B67F File Offset: 0x0006987F
		public static LocalizedString LabelReportMailHeaderCancel
		{
			get
			{
				return new LocalizedString("LabelReportMailHeaderCancel", "Ex6F67EB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0006B69D File Offset: 0x0006989D
		public static LocalizedString ReportSubjectCanceled
		{
			get
			{
				return new LocalizedString("ReportSubjectCanceled", "ExAC7874", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0006B6BB File Offset: 0x000698BB
		public static LocalizedString LabelStartedBy
		{
			get
			{
				return new LocalizedString("LabelStartedBy", "ExC5B354", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0006B6DC File Offset: 0x000698DC
		public static LocalizedString RunTimeFormatMinutes(int minutes)
		{
			return new LocalizedString("RunTimeFormatMinutes", "ExB9F67C", false, true, Strings.ResourceManager, new object[]
			{
				minutes
			});
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0006B710 File Offset: 0x00069910
		public static LocalizedString LabelRunTime
		{
			get
			{
				return new LocalizedString("LabelRunTime", "Ex866C0D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0006B730 File Offset: 0x00069930
		public static LocalizedString RunTimeFormatDays(int days, int hours, int minutes)
		{
			return new LocalizedString("RunTimeFormatDays", "ExE0E5DB", false, true, Strings.ResourceManager, new object[]
			{
				days,
				hours,
				minutes
			});
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0006B778 File Offset: 0x00069978
		public static LocalizedString FailedToUpdateProperty(string errorDetails)
		{
			return new LocalizedString("FailedToUpdateProperty", "Ex5800C0", false, true, Strings.ResourceManager, new object[]
			{
				errorDetails
			});
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0006B7A8 File Offset: 0x000699A8
		public static LocalizedString FailedToUpdateDistributionGroupMember(string errorDetails)
		{
			return new LocalizedString("FailedToUpdateDistributionGroupMember", "Ex5B2C5D", false, true, Strings.ResourceManager, new object[]
			{
				errorDetails
			});
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0006B7D7 File Offset: 0x000699D7
		public static LocalizedString LabelFileName
		{
			get
			{
				return new LocalizedString("LabelFileName", "Ex9557FE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0006B7F5 File Offset: 0x000699F5
		public static LocalizedString HelpText
		{
			get
			{
				return new LocalizedString("HelpText", "Ex7D046E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0006B813 File Offset: 0x00069A13
		public static LocalizedString LabelReportMailHeader
		{
			get
			{
				return new LocalizedString("LabelReportMailHeader", "ExEAE37B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0006B831 File Offset: 0x00069A31
		public static LocalizedString ErrorSummary
		{
			get
			{
				return new LocalizedString("ErrorSummary", "Ex5E3554", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0006B850 File Offset: 0x00069A50
		public static LocalizedString ErrorMessagePlural(int errorCount)
		{
			return new LocalizedString("ErrorMessagePlural", "Ex1CBEAA", false, true, Strings.ResourceManager, new object[]
			{
				errorCount
			});
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0006B884 File Offset: 0x00069A84
		public static LocalizedString RunTimeFormatHours(int hours, int minutes)
		{
			return new LocalizedString("RunTimeFormatHours", "ExDDC7DA", false, true, Strings.ResourceManager, new object[]
			{
				hours,
				minutes
			});
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0006B8C1 File Offset: 0x00069AC1
		public static LocalizedString LabelTotalRows
		{
			get
			{
				return new LocalizedString("LabelTotalRows", "ExE08087", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0006B8E0 File Offset: 0x00069AE0
		public static LocalizedString ErrorMessageSingular(int errorCount)
		{
			return new LocalizedString("ErrorMessageSingular", "Ex93FA17", false, true, Strings.ResourceManager, new object[]
			{
				errorCount
			});
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0006B914 File Offset: 0x00069B14
		public static LocalizedString HelpURLText
		{
			get
			{
				return new LocalizedString("HelpURLText", "Ex0EF214", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0006B932 File Offset: 0x00069B32
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040009D8 RID: 2520
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(22);

		// Token: 0x040009D9 RID: 2521
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.BulkProvisioning.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000136 RID: 310
		public enum IDs : uint
		{
			// Token: 0x040009DB RID: 2523
			LabelLogMailFooter = 1246705638U,
			// Token: 0x040009DC RID: 2524
			UnknownProvisioningType = 1416516349U,
			// Token: 0x040009DD RID: 2525
			PermissionDenied = 2604316530U,
			// Token: 0x040009DE RID: 2526
			LabelStartTime = 2795084079U,
			// Token: 0x040009DF RID: 2527
			LabelReportMailErrorHeaderCancel = 3123046798U,
			// Token: 0x040009E0 RID: 2528
			ReportSubject = 886329592U,
			// Token: 0x040009E1 RID: 2529
			ErrorReportFileName = 1921063387U,
			// Token: 0x040009E2 RID: 2530
			LabelTotalRowsProcessed = 2079790351U,
			// Token: 0x040009E3 RID: 2531
			LabelReportMailErrorHeader = 246748372U,
			// Token: 0x040009E4 RID: 2532
			LabelNewUsers = 283214974U,
			// Token: 0x040009E5 RID: 2533
			LabelErrors = 345539229U,
			// Token: 0x040009E6 RID: 2534
			UnknownProvisioningOwner = 3889683156U,
			// Token: 0x040009E7 RID: 2535
			LabelReportMailHeaderCancel = 3917832382U,
			// Token: 0x040009E8 RID: 2536
			ReportSubjectCanceled = 3597640497U,
			// Token: 0x040009E9 RID: 2537
			LabelStartedBy = 3346179848U,
			// Token: 0x040009EA RID: 2538
			LabelRunTime = 2060675488U,
			// Token: 0x040009EB RID: 2539
			LabelFileName = 1144436407U,
			// Token: 0x040009EC RID: 2540
			HelpText = 2937969650U,
			// Token: 0x040009ED RID: 2541
			LabelReportMailHeader = 3449564964U,
			// Token: 0x040009EE RID: 2542
			ErrorSummary = 264350496U,
			// Token: 0x040009EF RID: 2543
			LabelTotalRows = 5503809U,
			// Token: 0x040009F0 RID: 2544
			HelpURLText = 2821624239U
		}

		// Token: 0x02000137 RID: 311
		private enum ParamIDs
		{
			// Token: 0x040009F2 RID: 2546
			UsedEmailAddress,
			// Token: 0x040009F3 RID: 2547
			RunTimeFormatMinutes,
			// Token: 0x040009F4 RID: 2548
			RunTimeFormatDays,
			// Token: 0x040009F5 RID: 2549
			FailedToUpdateProperty,
			// Token: 0x040009F6 RID: 2550
			FailedToUpdateDistributionGroupMember,
			// Token: 0x040009F7 RID: 2551
			ErrorMessagePlural,
			// Token: 0x040009F8 RID: 2552
			RunTimeFormatHours,
			// Token: 0x040009F9 RID: 2553
			ErrorMessageSingular
		}
	}
}
