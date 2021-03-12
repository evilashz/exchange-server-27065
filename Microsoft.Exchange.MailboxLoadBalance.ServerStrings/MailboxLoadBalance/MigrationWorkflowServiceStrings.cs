using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000002 RID: 2
	internal static class MigrationWorkflowServiceStrings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static MigrationWorkflowServiceStrings()
		{
			MigrationWorkflowServiceStrings.stringIDs.Add(2604524602U, "ErrorCmdletPoolExhausted");
			MigrationWorkflowServiceStrings.stringIDs.Add(3276599839U, "ErrorHeatMapNotBuilt");
			MigrationWorkflowServiceStrings.stringIDs.Add(3174005438U, "ErrorInsufficientCapacityProvisioning");
			MigrationWorkflowServiceStrings.stringIDs.Add(334017145U, "ErrorAutomaticMailboxLoadBalancingNotAllowed");
			MigrationWorkflowServiceStrings.stringIDs.Add(2617355097U, "ErrorUnknownProvisioningStatus");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002170 File Offset: 0x00000370
		public static LocalizedString ErrorMultipleRecipientFound(string userId)
		{
			return new LocalizedString("ErrorMultipleRecipientFound", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002198 File Offset: 0x00000398
		public static LocalizedString ErrorDatabaseNotFound(string guid)
		{
			return new LocalizedString("ErrorDatabaseNotFound", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021C0 File Offset: 0x000003C0
		public static LocalizedString ErrorMissingAnchorMailbox(string capability)
		{
			return new LocalizedString("ErrorMissingAnchorMailbox", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				capability
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021E8 File Offset: 0x000003E8
		public static LocalizedString ErrorCannotRetrieveCapacityData(string objectIdentity)
		{
			return new LocalizedString("ErrorCannotRetrieveCapacityData", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				objectIdentity
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002210 File Offset: 0x00000410
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002238 File Offset: 0x00000438
		public static LocalizedString ErrorServerNotFound(string guid)
		{
			return new LocalizedString("ErrorServerNotFound", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002260 File Offset: 0x00000460
		public static LocalizedString ErrorConstraintCouldNotBeSatisfied(string constraintExpression)
		{
			return new LocalizedString("ErrorConstraintCouldNotBeSatisfied", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				constraintExpression
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002288 File Offset: 0x00000488
		public static LocalizedString ErrorMissingDatabaseActivationPreference(string databaseName)
		{
			return new LocalizedString("ErrorMissingDatabaseActivationPreference", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022B0 File Offset: 0x000004B0
		public static LocalizedString ErrorCmdletPoolExhausted
		{
			get
			{
				return new LocalizedString("ErrorCmdletPoolExhausted", MigrationWorkflowServiceStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C8 File Offset: 0x000004C8
		public static LocalizedString ErrorContainerCannotTakeLoad(string containerGuid)
		{
			return new LocalizedString("ErrorContainerCannotTakeLoad", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				containerGuid
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022F0 File Offset: 0x000004F0
		public static LocalizedString ErrorDatabaseFailedOver(string guid)
		{
			return new LocalizedString("ErrorDatabaseFailedOver", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002318 File Offset: 0x00000518
		public static LocalizedString ErrorHeatMapNotBuilt
		{
			get
			{
				return new LocalizedString("ErrorHeatMapNotBuilt", MigrationWorkflowServiceStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002330 File Offset: 0x00000530
		public static LocalizedString ErrorInvalidOrganization(string orgName)
		{
			return new LocalizedString("ErrorInvalidOrganization", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				orgName
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002358 File Offset: 0x00000558
		public static LocalizedString ErrorInsufficientCapacityProvisioning
		{
			get
			{
				return new LocalizedString("ErrorInsufficientCapacityProvisioning", MigrationWorkflowServiceStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002370 File Offset: 0x00000570
		public static LocalizedString ErrorRecipientNotFound(string userId)
		{
			return new LocalizedString("ErrorRecipientNotFound", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				userId
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002398 File Offset: 0x00000598
		public static LocalizedString ErrorObjectCannotBeMoved(string objectType, string objectIdentity)
		{
			return new LocalizedString("ErrorObjectCannotBeMoved", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				objectType,
				objectIdentity
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023C4 File Offset: 0x000005C4
		public static LocalizedString ErrorAutomaticMailboxLoadBalancingNotAllowed
		{
			get
			{
				return new LocalizedString("ErrorAutomaticMailboxLoadBalancingNotAllowed", MigrationWorkflowServiceStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023DC File Offset: 0x000005DC
		public static LocalizedString ErrorOverlappingBandDefinition(string newBand, string existingBand)
		{
			return new LocalizedString("ErrorOverlappingBandDefinition", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				newBand,
				existingBand
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002408 File Offset: 0x00000608
		public static LocalizedString ErrorEntityNotMovable(string orgId, string userId)
		{
			return new LocalizedString("ErrorEntityNotMovable", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				orgId,
				userId
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002434 File Offset: 0x00000634
		public static LocalizedString ErrorUnknownProvisioningStatus
		{
			get
			{
				return new LocalizedString("ErrorUnknownProvisioningStatus", MigrationWorkflowServiceStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000244C File Offset: 0x0000064C
		public static LocalizedString ErrorDagNotFound(string guid)
		{
			return new LocalizedString("ErrorDagNotFound", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002474 File Offset: 0x00000674
		public static LocalizedString ErrorInvalidExternalOrganizationId(string orgName, string externalId)
		{
			return new LocalizedString("ErrorInvalidExternalOrganizationId", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				orgName,
				externalId
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024A0 File Offset: 0x000006A0
		public static LocalizedString ErrorDatabaseNotLocal(string databaseName, string edbPath)
		{
			return new LocalizedString("ErrorDatabaseNotLocal", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				databaseName,
				edbPath
			});
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024CC File Offset: 0x000006CC
		public static LocalizedString ErrorNotEnoughDatabaseCapacity(string databaseGuid, string capacityType, long requestedCapacityUnits, long availableCapacityUnits)
		{
			return new LocalizedString("ErrorNotEnoughDatabaseCapacity", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				databaseGuid,
				capacityType,
				requestedCapacityUnits,
				availableCapacityUnits
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000250C File Offset: 0x0000070C
		public static LocalizedString ErrorBandDefinitionNotFound(string band)
		{
			return new LocalizedString("ErrorBandDefinitionNotFound", MigrationWorkflowServiceStrings.ResourceManager, new object[]
			{
				band
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002534 File Offset: 0x00000734
		public static LocalizedString GetLocalizedString(MigrationWorkflowServiceStrings.IDs key)
		{
			return new LocalizedString(MigrationWorkflowServiceStrings.stringIDs[(uint)key], MigrationWorkflowServiceStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(5);

		// Token: 0x04000002 RID: 2
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxLoadBalance.Strings", typeof(MigrationWorkflowServiceStrings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		public enum IDs : uint
		{
			// Token: 0x04000004 RID: 4
			ErrorCmdletPoolExhausted = 2604524602U,
			// Token: 0x04000005 RID: 5
			ErrorHeatMapNotBuilt = 3276599839U,
			// Token: 0x04000006 RID: 6
			ErrorInsufficientCapacityProvisioning = 3174005438U,
			// Token: 0x04000007 RID: 7
			ErrorAutomaticMailboxLoadBalancingNotAllowed = 334017145U,
			// Token: 0x04000008 RID: 8
			ErrorUnknownProvisioningStatus = 2617355097U
		}

		// Token: 0x02000004 RID: 4
		private enum ParamIDs
		{
			// Token: 0x0400000A RID: 10
			ErrorMultipleRecipientFound,
			// Token: 0x0400000B RID: 11
			ErrorDatabaseNotFound,
			// Token: 0x0400000C RID: 12
			ErrorMissingAnchorMailbox,
			// Token: 0x0400000D RID: 13
			ErrorCannotRetrieveCapacityData,
			// Token: 0x0400000E RID: 14
			UsageText,
			// Token: 0x0400000F RID: 15
			ErrorServerNotFound,
			// Token: 0x04000010 RID: 16
			ErrorConstraintCouldNotBeSatisfied,
			// Token: 0x04000011 RID: 17
			ErrorMissingDatabaseActivationPreference,
			// Token: 0x04000012 RID: 18
			ErrorContainerCannotTakeLoad,
			// Token: 0x04000013 RID: 19
			ErrorDatabaseFailedOver,
			// Token: 0x04000014 RID: 20
			ErrorInvalidOrganization,
			// Token: 0x04000015 RID: 21
			ErrorRecipientNotFound,
			// Token: 0x04000016 RID: 22
			ErrorObjectCannotBeMoved,
			// Token: 0x04000017 RID: 23
			ErrorOverlappingBandDefinition,
			// Token: 0x04000018 RID: 24
			ErrorEntityNotMovable,
			// Token: 0x04000019 RID: 25
			ErrorDagNotFound,
			// Token: 0x0400001A RID: 26
			ErrorInvalidExternalOrganizationId,
			// Token: 0x0400001B RID: 27
			ErrorDatabaseNotLocal,
			// Token: 0x0400001C RID: 28
			ErrorNotEnoughDatabaseCapacity,
			// Token: 0x0400001D RID: 29
			ErrorBandDefinitionNotFound
		}
	}
}
