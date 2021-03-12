using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02001206 RID: 4614
	internal static class AgentStrings
	{
		// Token: 0x0600B9F4 RID: 47604 RVA: 0x002A6A34 File Offset: 0x002A4C34
		static AgentStrings()
		{
			AgentStrings.stringIDs.Add(3858184179U, "InvalidIdentity");
			AgentStrings.stringIDs.Add(2788510868U, "NoTransportPipelineData");
			AgentStrings.stringIDs.Add(1575515744U, "NoIdentityArgument");
			AgentStrings.stringIDs.Add(3657604156U, "AgentNameContainsInvalidCharacters");
			AgentStrings.stringIDs.Add(3268060226U, "TransportAgentTasksOnlyOnFewRoles");
			AgentStrings.stringIDs.Add(1173448710U, "AgentNameTooLargeArgument");
			AgentStrings.stringIDs.Add(2849847834U, "ReleaseAgentBinaryReference");
		}

		// Token: 0x0600B9F5 RID: 47605 RVA: 0x002A6AFC File Offset: 0x002A4CFC
		public static LocalizedString TransportServiceNotSupported(string service)
		{
			return new LocalizedString("TransportServiceNotSupported", "", false, false, AgentStrings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x0600B9F6 RID: 47606 RVA: 0x002A6B2C File Offset: 0x002A4D2C
		public static LocalizedString AssemblyFilePathRelativeOnHub(string assemblyPath)
		{
			return new LocalizedString("AssemblyFilePathRelativeOnHub", "Ex447E83", false, true, AgentStrings.ResourceManager, new object[]
			{
				assemblyPath
			});
		}

		// Token: 0x17003A59 RID: 14937
		// (get) Token: 0x0600B9F7 RID: 47607 RVA: 0x002A6B5B File Offset: 0x002A4D5B
		public static LocalizedString InvalidIdentity
		{
			get
			{
				return new LocalizedString("InvalidIdentity", "Ex1AA584", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A5A RID: 14938
		// (get) Token: 0x0600B9F8 RID: 47608 RVA: 0x002A6B79 File Offset: 0x002A4D79
		public static LocalizedString NoTransportPipelineData
		{
			get
			{
				return new LocalizedString("NoTransportPipelineData", "Ex5C457E", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B9F9 RID: 47609 RVA: 0x002A6B98 File Offset: 0x002A4D98
		public static LocalizedString ConfirmationMessageUninstallTransportAgent(string Identity)
		{
			return new LocalizedString("ConfirmationMessageUninstallTransportAgent", "ExE232D1", false, true, AgentStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A5B RID: 14939
		// (get) Token: 0x0600B9FA RID: 47610 RVA: 0x002A6BC7 File Offset: 0x002A4DC7
		public static LocalizedString NoIdentityArgument
		{
			get
			{
				return new LocalizedString("NoIdentityArgument", "Ex1FA692", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600B9FB RID: 47611 RVA: 0x002A6BE8 File Offset: 0x002A4DE8
		public static LocalizedString ConfirmationMessageDisableTransportAgent(string Identity)
		{
			return new LocalizedString("ConfirmationMessageDisableTransportAgent", "Ex3574A2", false, true, AgentStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B9FC RID: 47612 RVA: 0x002A6C18 File Offset: 0x002A4E18
		public static LocalizedString ConfirmationMessageSetTransportAgent(string Identity)
		{
			return new LocalizedString("ConfirmationMessageSetTransportAgent", "ExC46ADA", false, true, AgentStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x0600B9FD RID: 47613 RVA: 0x002A6C48 File Offset: 0x002A4E48
		public static LocalizedString DeliveryProtocolNotValid(string identity)
		{
			return new LocalizedString("DeliveryProtocolNotValid", "ExE646F6", false, true, AgentStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600B9FE RID: 47614 RVA: 0x002A6C78 File Offset: 0x002A4E78
		public static LocalizedString InvalidTransportAgentFactory(string transportAgentFactory)
		{
			return new LocalizedString("InvalidTransportAgentFactory", "ExCF76E7", false, true, AgentStrings.ResourceManager, new object[]
			{
				transportAgentFactory
			});
		}

		// Token: 0x0600B9FF RID: 47615 RVA: 0x002A6CA8 File Offset: 0x002A4EA8
		public static LocalizedString AssemblyFileNotExist(string assemblyPath)
		{
			return new LocalizedString("AssemblyFileNotExist", "ExE78D21", false, true, AgentStrings.ResourceManager, new object[]
			{
				assemblyPath
			});
		}

		// Token: 0x0600BA00 RID: 47616 RVA: 0x002A6CD8 File Offset: 0x002A4ED8
		public static LocalizedString RestartServiceForChanges(string serviceName)
		{
			return new LocalizedString("RestartServiceForChanges", "ExF30C00", false, true, AgentStrings.ResourceManager, new object[]
			{
				serviceName
			});
		}

		// Token: 0x17003A5C RID: 14940
		// (get) Token: 0x0600BA01 RID: 47617 RVA: 0x002A6D07 File Offset: 0x002A4F07
		public static LocalizedString AgentNameContainsInvalidCharacters
		{
			get
			{
				return new LocalizedString("AgentNameContainsInvalidCharacters", "Ex7A5D34", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA02 RID: 47618 RVA: 0x002A6D28 File Offset: 0x002A4F28
		public static LocalizedString PriorityOutOfRange(string maxPriority)
		{
			return new LocalizedString("PriorityOutOfRange", "ExFEB2E3", false, true, AgentStrings.ResourceManager, new object[]
			{
				maxPriority
			});
		}

		// Token: 0x0600BA03 RID: 47619 RVA: 0x002A6D58 File Offset: 0x002A4F58
		public static LocalizedString AgentAlreadyExist(string identity)
		{
			return new LocalizedString("AgentAlreadyExist", "Ex4D98F9", false, true, AgentStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600BA04 RID: 47620 RVA: 0x002A6D88 File Offset: 0x002A4F88
		public static LocalizedString InvalidDeliveryAgentManager(string identity)
		{
			return new LocalizedString("InvalidDeliveryAgentManager", "ExC97BC5", false, true, AgentStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600BA05 RID: 47621 RVA: 0x002A6DB8 File Offset: 0x002A4FB8
		public static LocalizedString AgentNotFound(string identity)
		{
			return new LocalizedString("AgentNotFound", "Ex23DBF5", false, true, AgentStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600BA06 RID: 47622 RVA: 0x002A6DE8 File Offset: 0x002A4FE8
		public static LocalizedString MissingConfigurationFileCreate(string filePath)
		{
			return new LocalizedString("MissingConfigurationFileCreate", "Ex5C98CA", false, true, AgentStrings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x0600BA07 RID: 47623 RVA: 0x002A6E18 File Offset: 0x002A5018
		public static LocalizedString MustHaveUniqueDeliveryProtocol(string identity, string protocol)
		{
			return new LocalizedString("MustHaveUniqueDeliveryProtocol", "Ex2E8D12", false, true, AgentStrings.ResourceManager, new object[]
			{
				identity,
				protocol
			});
		}

		// Token: 0x0600BA08 RID: 47624 RVA: 0x002A6E4C File Offset: 0x002A504C
		public static LocalizedString AgentFactoryTypeNotExist(string transportAgentFactory)
		{
			return new LocalizedString("AgentFactoryTypeNotExist", "Ex9BCFF1", false, true, AgentStrings.ResourceManager, new object[]
			{
				transportAgentFactory
			});
		}

		// Token: 0x0600BA09 RID: 47625 RVA: 0x002A6E7C File Offset: 0x002A507C
		public static LocalizedString ConfirmationMessageEnableTransportAgent(string Identity)
		{
			return new LocalizedString("ConfirmationMessageEnableTransportAgent", "ExAD0323", false, true, AgentStrings.ResourceManager, new object[]
			{
				Identity
			});
		}

		// Token: 0x17003A5D RID: 14941
		// (get) Token: 0x0600BA0A RID: 47626 RVA: 0x002A6EAB File Offset: 0x002A50AB
		public static LocalizedString TransportAgentTasksOnlyOnFewRoles
		{
			get
			{
				return new LocalizedString("TransportAgentTasksOnlyOnFewRoles", "", false, false, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17003A5E RID: 14942
		// (get) Token: 0x0600BA0B RID: 47627 RVA: 0x002A6EC9 File Offset: 0x002A50C9
		public static LocalizedString AgentNameTooLargeArgument
		{
			get
			{
				return new LocalizedString("AgentNameTooLargeArgument", "ExA21F3D", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA0C RID: 47628 RVA: 0x002A6EE8 File Offset: 0x002A50E8
		public static LocalizedString ConfirmationMessageInstallTransportAgent(string Name, string TransportAgentFactory, string AssemblyPath)
		{
			return new LocalizedString("ConfirmationMessageInstallTransportAgent", "Ex120410", false, true, AgentStrings.ResourceManager, new object[]
			{
				Name,
				TransportAgentFactory,
				AssemblyPath
			});
		}

		// Token: 0x0600BA0D RID: 47629 RVA: 0x002A6F20 File Offset: 0x002A5120
		public static LocalizedString DeliveryProtocolNotSpecified(string identity)
		{
			return new LocalizedString("DeliveryProtocolNotSpecified", "Ex0138E2", false, true, AgentStrings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600BA0E RID: 47630 RVA: 0x002A6F50 File Offset: 0x002A5150
		public static LocalizedString AgentTypeNotSupportedOnFrontEnd(string agentType)
		{
			return new LocalizedString("AgentTypeNotSupportedOnFrontEnd", "", false, false, AgentStrings.ResourceManager, new object[]
			{
				agentType
			});
		}

		// Token: 0x0600BA0F RID: 47631 RVA: 0x002A6F80 File Offset: 0x002A5180
		public static LocalizedString AssemblyFilePathCanNotBeUNC(string assemblyPath)
		{
			return new LocalizedString("AssemblyFilePathCanNotBeUNC", "Ex10CB51", false, true, AgentStrings.ResourceManager, new object[]
			{
				assemblyPath
			});
		}

		// Token: 0x17003A5F RID: 14943
		// (get) Token: 0x0600BA10 RID: 47632 RVA: 0x002A6FAF File Offset: 0x002A51AF
		public static LocalizedString ReleaseAgentBinaryReference
		{
			get
			{
				return new LocalizedString("ReleaseAgentBinaryReference", "Ex7C8B43", false, true, AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600BA11 RID: 47633 RVA: 0x002A6FCD File Offset: 0x002A51CD
		public static LocalizedString GetLocalizedString(AgentStrings.IDs key)
		{
			return new LocalizedString(AgentStrings.stringIDs[(uint)key], AgentStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04006474 RID: 25716
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(7);

		// Token: 0x04006475 RID: 25717
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.AgentStrings", typeof(AgentStrings).GetTypeInfo().Assembly);

		// Token: 0x02001207 RID: 4615
		public enum IDs : uint
		{
			// Token: 0x04006477 RID: 25719
			InvalidIdentity = 3858184179U,
			// Token: 0x04006478 RID: 25720
			NoTransportPipelineData = 2788510868U,
			// Token: 0x04006479 RID: 25721
			NoIdentityArgument = 1575515744U,
			// Token: 0x0400647A RID: 25722
			AgentNameContainsInvalidCharacters = 3657604156U,
			// Token: 0x0400647B RID: 25723
			TransportAgentTasksOnlyOnFewRoles = 3268060226U,
			// Token: 0x0400647C RID: 25724
			AgentNameTooLargeArgument = 1173448710U,
			// Token: 0x0400647D RID: 25725
			ReleaseAgentBinaryReference = 2849847834U
		}

		// Token: 0x02001208 RID: 4616
		private enum ParamIDs
		{
			// Token: 0x0400647F RID: 25727
			TransportServiceNotSupported,
			// Token: 0x04006480 RID: 25728
			AssemblyFilePathRelativeOnHub,
			// Token: 0x04006481 RID: 25729
			ConfirmationMessageUninstallTransportAgent,
			// Token: 0x04006482 RID: 25730
			ConfirmationMessageDisableTransportAgent,
			// Token: 0x04006483 RID: 25731
			ConfirmationMessageSetTransportAgent,
			// Token: 0x04006484 RID: 25732
			DeliveryProtocolNotValid,
			// Token: 0x04006485 RID: 25733
			InvalidTransportAgentFactory,
			// Token: 0x04006486 RID: 25734
			AssemblyFileNotExist,
			// Token: 0x04006487 RID: 25735
			RestartServiceForChanges,
			// Token: 0x04006488 RID: 25736
			PriorityOutOfRange,
			// Token: 0x04006489 RID: 25737
			AgentAlreadyExist,
			// Token: 0x0400648A RID: 25738
			InvalidDeliveryAgentManager,
			// Token: 0x0400648B RID: 25739
			AgentNotFound,
			// Token: 0x0400648C RID: 25740
			MissingConfigurationFileCreate,
			// Token: 0x0400648D RID: 25741
			MustHaveUniqueDeliveryProtocol,
			// Token: 0x0400648E RID: 25742
			AgentFactoryTypeNotExist,
			// Token: 0x0400648F RID: 25743
			ConfirmationMessageEnableTransportAgent,
			// Token: 0x04006490 RID: 25744
			ConfirmationMessageInstallTransportAgent,
			// Token: 0x04006491 RID: 25745
			DeliveryProtocolNotSpecified,
			// Token: 0x04006492 RID: 25746
			AgentTypeNotSupportedOnFrontEnd,
			// Token: 0x04006493 RID: 25747
			AssemblyFilePathCanNotBeUNC
		}
	}
}
