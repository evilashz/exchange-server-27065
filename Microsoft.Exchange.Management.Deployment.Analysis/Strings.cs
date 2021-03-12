using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200003A RID: 58
	internal static class Strings
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00007ABC File Offset: 0x00005CBC
		static Strings()
		{
			Strings.stringIDs.Add(967409192U, "CanceledMessage");
			Strings.stringIDs.Add(260127048U, "CannotReturnNullForResult");
			Strings.stringIDs.Add(1163620802U, "CanOnlyHaveOneFeatureOfEachType");
			Strings.stringIDs.Add(3944784639U, "CannotGetStartTimeBeforeMemberStart");
			Strings.stringIDs.Add(718315535U, "CannotGetMembersBeforeDiscovery");
			Strings.stringIDs.Add(1536053379U, "CannotGetStopTimeBeforeCompletion");
			Strings.stringIDs.Add(4284209231U, "CannotGetMemberNameBeforeDiscovery");
			Strings.stringIDs.Add(1141647571U, "AccessedFailedResult");
			Strings.stringIDs.Add(4164286807U, "EmptyResults");
			Strings.stringIDs.Add(59961664U, "CannotGetCancellationExceptionWithoutCancellation");
			Strings.stringIDs.Add(2424943235U, "CannotAddNullFeature");
			Strings.stringIDs.Add(1813193876U, "FilteredResult");
			Strings.stringIDs.Add(2464627714U, "NullResult");
			Strings.stringIDs.Add(1032838303U, "CannotGetStopTimeBeforeMemberCompletion");
			Strings.stringIDs.Add(337673388U, "CannotModifyReadOnlyProperty");
			Strings.stringIDs.Add(556668270U, "CriticalMessage");
			Strings.stringIDs.Add(2543736554U, "FailedResult");
			Strings.stringIDs.Add(3833953363U, "CannotGetStartTimeBeforeStart");
			Strings.stringIDs.Add(3425349017U, "AnalysisMustBeCompleteToCreateConclusionSet");
			Strings.stringIDs.Add(1376602368U, "AccessedValueWhenMultipleResults");
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007C88 File Offset: 0x00005E88
		public static LocalizedString CanceledMessage
		{
			get
			{
				return new LocalizedString("CanceledMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007C9F File Offset: 0x00005E9F
		public static LocalizedString CannotReturnNullForResult
		{
			get
			{
				return new LocalizedString("CannotReturnNullForResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007CB6 File Offset: 0x00005EB6
		public static LocalizedString CanOnlyHaveOneFeatureOfEachType
		{
			get
			{
				return new LocalizedString("CanOnlyHaveOneFeatureOfEachType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007CCD File Offset: 0x00005ECD
		public static LocalizedString CannotGetStartTimeBeforeMemberStart
		{
			get
			{
				return new LocalizedString("CannotGetStartTimeBeforeMemberStart", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public static LocalizedString ResultAncestorNotFound(string ancestorName)
		{
			return new LocalizedString("ResultAncestorNotFound", Strings.ResourceManager, new object[]
			{
				ancestorName
			});
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007D0C File Offset: 0x00005F0C
		public static LocalizedString CannotGetMembersBeforeDiscovery
		{
			get
			{
				return new LocalizedString("CannotGetMembersBeforeDiscovery", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007D23 File Offset: 0x00005F23
		public static LocalizedString CannotGetStopTimeBeforeCompletion
		{
			get
			{
				return new LocalizedString("CannotGetStopTimeBeforeCompletion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007D3A File Offset: 0x00005F3A
		public static LocalizedString CannotGetMemberNameBeforeDiscovery
		{
			get
			{
				return new LocalizedString("CannotGetMemberNameBeforeDiscovery", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007D51 File Offset: 0x00005F51
		public static LocalizedString AccessedFailedResult
		{
			get
			{
				return new LocalizedString("AccessedFailedResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007D68 File Offset: 0x00005F68
		public static LocalizedString EmptyResults
		{
			get
			{
				return new LocalizedString("EmptyResults", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007D7F File Offset: 0x00005F7F
		public static LocalizedString CannotGetCancellationExceptionWithoutCancellation
		{
			get
			{
				return new LocalizedString("CannotGetCancellationExceptionWithoutCancellation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007D96 File Offset: 0x00005F96
		public static LocalizedString CannotAddNullFeature
		{
			get
			{
				return new LocalizedString("CannotAddNullFeature", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00007DAD File Offset: 0x00005FAD
		public static LocalizedString FilteredResult
		{
			get
			{
				return new LocalizedString("FilteredResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public static LocalizedString NullResult
		{
			get
			{
				return new LocalizedString("NullResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007DDC File Offset: 0x00005FDC
		public static LocalizedString FeatureMissing(string featureType)
		{
			return new LocalizedString("FeatureMissing", Strings.ResourceManager, new object[]
			{
				featureType
			});
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007E04 File Offset: 0x00006004
		public static LocalizedString CannotGetStopTimeBeforeMemberCompletion
		{
			get
			{
				return new LocalizedString("CannotGetStopTimeBeforeMemberCompletion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00007E1B File Offset: 0x0000601B
		public static LocalizedString CannotModifyReadOnlyProperty
		{
			get
			{
				return new LocalizedString("CannotModifyReadOnlyProperty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007E32 File Offset: 0x00006032
		public static LocalizedString CriticalMessage
		{
			get
			{
				return new LocalizedString("CriticalMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007E49 File Offset: 0x00006049
		public static LocalizedString FailedResult
		{
			get
			{
				return new LocalizedString("FailedResult", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007E60 File Offset: 0x00006060
		public static LocalizedString CannotGetStartTimeBeforeStart
		{
			get
			{
				return new LocalizedString("CannotGetStartTimeBeforeStart", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007E77 File Offset: 0x00006077
		public static LocalizedString AnalysisMustBeCompleteToCreateConclusionSet
		{
			get
			{
				return new LocalizedString("AnalysisMustBeCompleteToCreateConclusionSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007E8E File Offset: 0x0000608E
		public static LocalizedString AccessedValueWhenMultipleResults
		{
			get
			{
				return new LocalizedString("AccessedValueWhenMultipleResults", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007EA5 File Offset: 0x000060A5
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400008B RID: 139
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(20);

		// Token: 0x0400008C RID: 140
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.Deployment.Analysis.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200003B RID: 59
		public enum IDs : uint
		{
			// Token: 0x0400008E RID: 142
			CanceledMessage = 967409192U,
			// Token: 0x0400008F RID: 143
			CannotReturnNullForResult = 260127048U,
			// Token: 0x04000090 RID: 144
			CanOnlyHaveOneFeatureOfEachType = 1163620802U,
			// Token: 0x04000091 RID: 145
			CannotGetStartTimeBeforeMemberStart = 3944784639U,
			// Token: 0x04000092 RID: 146
			CannotGetMembersBeforeDiscovery = 718315535U,
			// Token: 0x04000093 RID: 147
			CannotGetStopTimeBeforeCompletion = 1536053379U,
			// Token: 0x04000094 RID: 148
			CannotGetMemberNameBeforeDiscovery = 4284209231U,
			// Token: 0x04000095 RID: 149
			AccessedFailedResult = 1141647571U,
			// Token: 0x04000096 RID: 150
			EmptyResults = 4164286807U,
			// Token: 0x04000097 RID: 151
			CannotGetCancellationExceptionWithoutCancellation = 59961664U,
			// Token: 0x04000098 RID: 152
			CannotAddNullFeature = 2424943235U,
			// Token: 0x04000099 RID: 153
			FilteredResult = 1813193876U,
			// Token: 0x0400009A RID: 154
			NullResult = 2464627714U,
			// Token: 0x0400009B RID: 155
			CannotGetStopTimeBeforeMemberCompletion = 1032838303U,
			// Token: 0x0400009C RID: 156
			CannotModifyReadOnlyProperty = 337673388U,
			// Token: 0x0400009D RID: 157
			CriticalMessage = 556668270U,
			// Token: 0x0400009E RID: 158
			FailedResult = 2543736554U,
			// Token: 0x0400009F RID: 159
			CannotGetStartTimeBeforeStart = 3833953363U,
			// Token: 0x040000A0 RID: 160
			AnalysisMustBeCompleteToCreateConclusionSet = 3425349017U,
			// Token: 0x040000A1 RID: 161
			AccessedValueWhenMultipleResults = 1376602368U
		}

		// Token: 0x0200003C RID: 60
		private enum ParamIDs
		{
			// Token: 0x040000A3 RID: 163
			ResultAncestorNotFound,
			// Token: 0x040000A4 RID: 164
			FeatureMissing
		}
	}
}
