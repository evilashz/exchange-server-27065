using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000D9 RID: 217
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class UpgradeHandlerStrings
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0000F014 File Offset: 0x0000D214
		static UpgradeHandlerStrings()
		{
			UpgradeHandlerStrings.stringIDs.Add(3297992468U, "AnchorServiceInstanceNotActive");
			UpgradeHandlerStrings.stringIDs.Add(3742916171U, "TooManyPilotMailboxes");
			UpgradeHandlerStrings.stringIDs.Add(3847819607U, "InvalidE14Mailboxes");
			UpgradeHandlerStrings.stringIDs.Add(31377678U, "ErrorQueryingWorkItem");
			UpgradeHandlerStrings.stringIDs.Add(2176982493U, "ErrorNoE14ServersFound");
			UpgradeHandlerStrings.stringIDs.Add(683139314U, "InvalidE15Mailboxes");
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0000F0C8 File Offset: 0x0000D2C8
		public static LocalizedString OrganizationHasConstraints(UpgradeRequestTypes requestedType, string orgId, string orgName, string constraints)
		{
			return new LocalizedString("OrganizationHasConstraints", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				requestedType,
				orgId,
				orgName,
				constraints
			});
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000F104 File Offset: 0x0000D304
		public static LocalizedString InvalidRequestedType(string orgId, UpgradeRequestTypes currentType, string requestedType)
		{
			return new LocalizedString("InvalidRequestedType", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				orgId,
				currentType,
				requestedType
			});
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000F13C File Offset: 0x0000D33C
		public static LocalizedString UnsupportedUpgradeRequestType(UpgradeRequestTypes upgradeRequest)
		{
			return new LocalizedString("UnsupportedUpgradeRequestType", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				upgradeRequest
			});
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000F16C File Offset: 0x0000D36C
		public static LocalizedString InvalidOrganizationVersion(string org, ExchangeObjectVersion version)
		{
			return new LocalizedString("InvalidOrganizationVersion", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				org,
				version
			});
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000F198 File Offset: 0x0000D398
		public static LocalizedString SymphonyFault(string faultMessage)
		{
			return new LocalizedString("SymphonyFault", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				faultMessage
			});
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
		public static LocalizedString InvalidOrganizationState(string org, string servicePlan, ExchangeObjectVersion version, bool isUpgrading, bool isPiloting, bool isUpgradeInProgress)
		{
			return new LocalizedString("InvalidOrganizationState", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				org,
				servicePlan,
				version,
				isUpgrading,
				isPiloting,
				isUpgradeInProgress
			});
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000F210 File Offset: 0x0000D410
		public static LocalizedString OrganizationInDryRunMode(string tenant, string requestedType)
		{
			return new LocalizedString("OrganizationInDryRunMode", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				tenant,
				requestedType
			});
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000F23C File Offset: 0x0000D43C
		public static LocalizedString SymphonyInvalidOperationFault(string faultMessage)
		{
			return new LocalizedString("SymphonyInvalidOperationFault", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				faultMessage
			});
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000F264 File Offset: 0x0000D464
		public static LocalizedString InvalidUpgradeStatus(string id, UpgradeStatusTypes currentStatus)
		{
			return new LocalizedString("InvalidUpgradeStatus", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				id,
				currentStatus
			});
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000F298 File Offset: 0x0000D498
		public static LocalizedString SymphonyArgumentFault(string faultMessage)
		{
			return new LocalizedString("SymphonyArgumentFault", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				faultMessage
			});
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
		public static LocalizedString ErrorGettingFiles(string directory)
		{
			return new LocalizedString("ErrorGettingFiles", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				directory
			});
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public static LocalizedString AnchorServiceInstanceNotActive
		{
			get
			{
				return new LocalizedString("AnchorServiceInstanceNotActive", UpgradeHandlerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000F300 File Offset: 0x0000D500
		public static LocalizedString OrganizationNotFound(string org)
		{
			return new LocalizedString("OrganizationNotFound", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				org
			});
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000F328 File Offset: 0x0000D528
		public static LocalizedString TooManyPilotMailboxes
		{
			get
			{
				return new LocalizedString("TooManyPilotMailboxes", UpgradeHandlerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000F340 File Offset: 0x0000D540
		public static LocalizedString UnsupportedBatchType(string batchType)
		{
			return new LocalizedString("UnsupportedBatchType", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				batchType
			});
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000F368 File Offset: 0x0000D568
		public static LocalizedString InvalidE14Mailboxes
		{
			get
			{
				return new LocalizedString("InvalidE14Mailboxes", UpgradeHandlerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000F380 File Offset: 0x0000D580
		public static LocalizedString SymphonyCancelNotAllowedFault(string faultMessage)
		{
			return new LocalizedString("SymphonyCancelNotAllowedFault", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				faultMessage
			});
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		public static LocalizedString ErrorQueryingWorkItem
		{
			get
			{
				return new LocalizedString("ErrorQueryingWorkItem", UpgradeHandlerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		public static LocalizedString SymphonyAccessDeniedFault(string faultMessage)
		{
			return new LocalizedString("SymphonyAccessDeniedFault", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				faultMessage
			});
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public static LocalizedString ErrorNoE14ServersFound
		{
			get
			{
				return new LocalizedString("ErrorNoE14ServersFound", UpgradeHandlerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000F400 File Offset: 0x0000D600
		public static LocalizedString ErrorReadingFile(string file)
		{
			return new LocalizedString("ErrorReadingFile", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000F428 File Offset: 0x0000D628
		public static LocalizedString InvalidE15Mailboxes
		{
			get
			{
				return new LocalizedString("InvalidE15Mailboxes", UpgradeHandlerStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000F440 File Offset: 0x0000D640
		public static LocalizedString UserNotFound(string org)
		{
			return new LocalizedString("UserNotFound", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				org
			});
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000F468 File Offset: 0x0000D668
		public static LocalizedString ErrorGettingDatabases(string server)
		{
			return new LocalizedString("ErrorGettingDatabases", UpgradeHandlerStrings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0000F490 File Offset: 0x0000D690
		public static LocalizedString GetLocalizedString(UpgradeHandlerStrings.IDs key)
		{
			return new LocalizedString(UpgradeHandlerStrings.stringIDs[(uint)key], UpgradeHandlerStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400036B RID: 875
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x0400036C RID: 876
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.Strings", typeof(UpgradeHandlerStrings).GetTypeInfo().Assembly);

		// Token: 0x020000DA RID: 218
		public enum IDs : uint
		{
			// Token: 0x0400036E RID: 878
			AnchorServiceInstanceNotActive = 3297992468U,
			// Token: 0x0400036F RID: 879
			TooManyPilotMailboxes = 3742916171U,
			// Token: 0x04000370 RID: 880
			InvalidE14Mailboxes = 3847819607U,
			// Token: 0x04000371 RID: 881
			ErrorQueryingWorkItem = 31377678U,
			// Token: 0x04000372 RID: 882
			ErrorNoE14ServersFound = 2176982493U,
			// Token: 0x04000373 RID: 883
			InvalidE15Mailboxes = 683139314U
		}

		// Token: 0x020000DB RID: 219
		private enum ParamIDs
		{
			// Token: 0x04000375 RID: 885
			OrganizationHasConstraints,
			// Token: 0x04000376 RID: 886
			InvalidRequestedType,
			// Token: 0x04000377 RID: 887
			UnsupportedUpgradeRequestType,
			// Token: 0x04000378 RID: 888
			InvalidOrganizationVersion,
			// Token: 0x04000379 RID: 889
			SymphonyFault,
			// Token: 0x0400037A RID: 890
			InvalidOrganizationState,
			// Token: 0x0400037B RID: 891
			OrganizationInDryRunMode,
			// Token: 0x0400037C RID: 892
			SymphonyInvalidOperationFault,
			// Token: 0x0400037D RID: 893
			InvalidUpgradeStatus,
			// Token: 0x0400037E RID: 894
			SymphonyArgumentFault,
			// Token: 0x0400037F RID: 895
			ErrorGettingFiles,
			// Token: 0x04000380 RID: 896
			OrganizationNotFound,
			// Token: 0x04000381 RID: 897
			UnsupportedBatchType,
			// Token: 0x04000382 RID: 898
			SymphonyCancelNotAllowedFault,
			// Token: 0x04000383 RID: 899
			SymphonyAccessDeniedFault,
			// Token: 0x04000384 RID: 900
			ErrorReadingFile,
			// Token: 0x04000385 RID: 901
			UserNotFound,
			// Token: 0x04000386 RID: 902
			ErrorGettingDatabases
		}
	}
}
