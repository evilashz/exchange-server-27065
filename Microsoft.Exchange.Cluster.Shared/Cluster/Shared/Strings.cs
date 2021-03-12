using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000B2 RID: 178
	internal static class Strings
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0001A448 File Offset: 0x00018648
		static Strings()
		{
			Strings.stringIDs.Add(1578974238U, "FixUpIpAddressStatusUnchanged");
			Strings.stringIDs.Add(4106740448U, "RemoteClusterWin2k3ToWin2k8NotSupportedException");
			Strings.stringIDs.Add(3232505775U, "GroupStatePending");
			Strings.stringIDs.Add(801548279U, "NoErrorSpecified");
			Strings.stringIDs.Add(487199380U, "GroupStatePartialOnline");
			Strings.stringIDs.Add(3972288899U, "GroupStateFailed");
			Strings.stringIDs.Add(2874666956U, "GroupStateUnknown");
			Strings.stringIDs.Add(2356112387U, "GroupStateOnline");
			Strings.stringIDs.Add(344705101U, "GroupStateOffline");
			Strings.stringIDs.Add(330900787U, "ClusterNotRunningException");
			Strings.stringIDs.Add(4244700832U, "RemoteClusterWin2k8ToWin2k3NotSupportedException");
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001A560 File Offset: 0x00018760
		public static LocalizedString IpResourceCreationOnWrongTypeOfNetworkException(string network)
		{
			return new LocalizedString("IpResourceCreationOnWrongTypeOfNetworkException", Strings.ResourceManager, new object[]
			{
				network
			});
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001A588 File Offset: 0x00018788
		public static LocalizedString FixUpIpAddressStatusUnchanged
		{
			get
			{
				return new LocalizedString("FixUpIpAddressStatusUnchanged", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001A5A0 File Offset: 0x000187A0
		public static LocalizedString ClusCommonRetryableTransientException(string msg)
		{
			return new LocalizedString("ClusCommonRetryableTransientException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001A5C8 File Offset: 0x000187C8
		public static LocalizedString OfflineOperationTimedOutException(string objectName, int count, int secondsTimeout)
		{
			return new LocalizedString("OfflineOperationTimedOutException", Strings.ResourceManager, new object[]
			{
				objectName,
				count,
				secondsTimeout
			});
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001A602 File Offset: 0x00018802
		public static LocalizedString RemoteClusterWin2k3ToWin2k8NotSupportedException
		{
			get
			{
				return new LocalizedString("RemoteClusterWin2k3ToWin2k8NotSupportedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001A61C File Offset: 0x0001881C
		public static LocalizedString RequestedNetworkIsNotDhcpEnabled(string network)
		{
			return new LocalizedString("RequestedNetworkIsNotDhcpEnabled", Strings.ResourceManager, new object[]
			{
				network
			});
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001A644 File Offset: 0x00018844
		public static LocalizedString FailedToFindNetwork(string network)
		{
			return new LocalizedString("FailedToFindNetwork", Strings.ResourceManager, new object[]
			{
				network
			});
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001A66C File Offset: 0x0001886C
		public static LocalizedString DxStoreKeyApiFailedMessage(string api, string keyName, string msg)
		{
			return new LocalizedString("DxStoreKeyApiFailedMessage", Strings.ResourceManager, new object[]
			{
				api,
				keyName,
				msg
			});
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001A69C File Offset: 0x0001889C
		public static LocalizedString ClusCommonFailException(string error)
		{
			return new LocalizedString("ClusCommonFailException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001A6C4 File Offset: 0x000188C4
		public static LocalizedString ClusResourceAlreadyExistsException(string resourceName)
		{
			return new LocalizedString("ClusResourceAlreadyExistsException", Strings.ResourceManager, new object[]
			{
				resourceName
			});
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001A6EC File Offset: 0x000188EC
		public static LocalizedString ClusCommonNonRetryableTransientException(string msg)
		{
			return new LocalizedString("ClusCommonNonRetryableTransientException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001A714 File Offset: 0x00018914
		public static LocalizedString RegistryParameterKeyNotOpenedException(string keyName)
		{
			return new LocalizedString("RegistryParameterKeyNotOpenedException", Strings.ResourceManager, new object[]
			{
				keyName
			});
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001A73C File Offset: 0x0001893C
		public static LocalizedString RegistryParameterWriteException(string valueName, string errMsg)
		{
			return new LocalizedString("RegistryParameterWriteException", Strings.ResourceManager, new object[]
			{
				valueName,
				errMsg
			});
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A768 File Offset: 0x00018968
		public static LocalizedString RegistryParameterException(string errorMsg)
		{
			return new LocalizedString("RegistryParameterException", Strings.ResourceManager, new object[]
			{
				errorMsg
			});
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001A790 File Offset: 0x00018990
		public static LocalizedString ClusterFileNotFoundException(string nodeName)
		{
			return new LocalizedString("ClusterFileNotFoundException", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001A7B8 File Offset: 0x000189B8
		public static LocalizedString AmGetFqdnFailedADError(string nodeName, string adError)
		{
			return new LocalizedString("AmGetFqdnFailedADError", Strings.ResourceManager, new object[]
			{
				nodeName,
				adError
			});
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001A7E4 File Offset: 0x000189E4
		public static LocalizedString ClusCommonTransientException(string error)
		{
			return new LocalizedString("ClusCommonTransientException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001A80C File Offset: 0x00018A0C
		public static LocalizedString ClusterEvictWithoutCleanupException(string nodeName)
		{
			return new LocalizedString("ClusterEvictWithoutCleanupException", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001A834 File Offset: 0x00018A34
		public static LocalizedString GroupStatePending
		{
			get
			{
				return new LocalizedString("GroupStatePending", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001A84C File Offset: 0x00018A4C
		public static LocalizedString ClusterNotJoinedException(string nodeName)
		{
			return new LocalizedString("ClusterNotJoinedException", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001A874 File Offset: 0x00018A74
		public static LocalizedString AmCoreGroupRegNotFound(string regvalueName)
		{
			return new LocalizedString("AmCoreGroupRegNotFound", Strings.ResourceManager, new object[]
			{
				regvalueName
			});
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001A89C File Offset: 0x00018A9C
		public static LocalizedString IPv4NetworksHasDuplicateEntries(string duplicate)
		{
			return new LocalizedString("IPv4NetworksHasDuplicateEntries", Strings.ResourceManager, new object[]
			{
				duplicate
			});
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001A8C4 File Offset: 0x00018AC4
		public static LocalizedString AmServerNameResolveFqdnException(string error)
		{
			return new LocalizedString("AmServerNameResolveFqdnException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001A8EC File Offset: 0x00018AEC
		public static LocalizedString ClusCommonTaskPendingException(string msg)
		{
			return new LocalizedString("ClusCommonTaskPendingException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001A914 File Offset: 0x00018B14
		public static LocalizedString ClusterNotInstalledException(string nodeName)
		{
			return new LocalizedString("ClusterNotInstalledException", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001A93C File Offset: 0x00018B3C
		public static LocalizedString NoErrorSpecified
		{
			get
			{
				return new LocalizedString("NoErrorSpecified", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001A953 File Offset: 0x00018B53
		public static LocalizedString GroupStatePartialOnline
		{
			get
			{
				return new LocalizedString("GroupStatePartialOnline", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001A96C File Offset: 0x00018B6C
		public static LocalizedString InvalidResourceOpException(string resName)
		{
			return new LocalizedString("InvalidResourceOpException", Strings.ResourceManager, new object[]
			{
				resName
			});
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001A994 File Offset: 0x00018B94
		public static LocalizedString ClusterNodeNotFoundException(string nodeName)
		{
			return new LocalizedString("ClusterNodeNotFoundException", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001A9BC File Offset: 0x00018BBC
		public static LocalizedString RequestedNetworkIsNotIPv6Enabled(string network)
		{
			return new LocalizedString("RequestedNetworkIsNotIPv6Enabled", Strings.ResourceManager, new object[]
			{
				network
			});
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public static LocalizedString ClusterApiException(string msg)
		{
			return new LocalizedString("ClusterApiException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001AA0C File Offset: 0x00018C0C
		public static LocalizedString DxStoreKeyNotFoundException(string keyName)
		{
			return new LocalizedString("DxStoreKeyNotFoundException", Strings.ResourceManager, new object[]
			{
				keyName
			});
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001AA34 File Offset: 0x00018C34
		public static LocalizedString RegistryParameterReadException(string valueName, string errMsg)
		{
			return new LocalizedString("RegistryParameterReadException", Strings.ResourceManager, new object[]
			{
				valueName,
				errMsg
			});
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001AA60 File Offset: 0x00018C60
		public static LocalizedString DxStoreKeyInvalidKeyException(string keyName)
		{
			return new LocalizedString("DxStoreKeyInvalidKeyException", Strings.ResourceManager, new object[]
			{
				keyName
			});
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001AA88 File Offset: 0x00018C88
		public static LocalizedString DagTaskNotEnoughStaticIPAddresses(string network, string staticIps)
		{
			return new LocalizedString("DagTaskNotEnoughStaticIPAddresses", Strings.ResourceManager, new object[]
			{
				network,
				staticIps
			});
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		public static LocalizedString ClusteringNotInstalledOnLHException(string errorMessage)
		{
			return new LocalizedString("ClusteringNotInstalledOnLHException", Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001AADC File Offset: 0x00018CDC
		public static LocalizedString OperationValidOnlyOnLonghornException(string resName)
		{
			return new LocalizedString("OperationValidOnlyOnLonghornException", Strings.ResourceManager, new object[]
			{
				resName
			});
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001AB04 File Offset: 0x00018D04
		public static LocalizedString ClusterUnsupportedRegistryTypeException(string typeName)
		{
			return new LocalizedString("ClusterUnsupportedRegistryTypeException", Strings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001AB2C File Offset: 0x00018D2C
		public static LocalizedString FixUpIpAddressStatusUpdated(int deletedResources, int newResources)
		{
			return new LocalizedString("FixUpIpAddressStatusUpdated", Strings.ResourceManager, new object[]
			{
				deletedResources,
				newResources
			});
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001AB64 File Offset: 0x00018D64
		public static LocalizedString NoClusResourceFoundException(string groupName, string resourceName)
		{
			return new LocalizedString("NoClusResourceFoundException", Strings.ResourceManager, new object[]
			{
				groupName,
				resourceName
			});
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001AB90 File Offset: 0x00018D90
		public static LocalizedString AmGetFqdnFailedNotFound(string nodeName)
		{
			return new LocalizedString("AmGetFqdnFailedNotFound", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001ABB8 File Offset: 0x00018DB8
		public static LocalizedString FailToOfflineClusResourceException(string groupName, string resourceId, string reason)
		{
			return new LocalizedString("FailToOfflineClusResourceException", Strings.ResourceManager, new object[]
			{
				groupName,
				resourceId,
				reason
			});
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public static LocalizedString ClusterNodeJoinedException(string nodeName)
		{
			return new LocalizedString("ClusterNodeJoinedException", Strings.ResourceManager, new object[]
			{
				nodeName
			});
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001AC10 File Offset: 0x00018E10
		public static LocalizedString ClusterDatabaseTransientException(string msg)
		{
			return new LocalizedString("ClusterDatabaseTransientException", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001AC38 File Offset: 0x00018E38
		public static LocalizedString OpenClusterTimedoutException(string serverName, int timeoutInSeconds, string context)
		{
			return new LocalizedString("OpenClusterTimedoutException", Strings.ResourceManager, new object[]
			{
				serverName,
				timeoutInSeconds,
				context
			});
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001AC70 File Offset: 0x00018E70
		public static LocalizedString IPv6NetworksHasDuplicateEntries(string duplicate)
		{
			return new LocalizedString("IPv6NetworksHasDuplicateEntries", Strings.ResourceManager, new object[]
			{
				duplicate
			});
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001AC98 File Offset: 0x00018E98
		public static LocalizedString NoInstancesFoundForManagementPath(string path)
		{
			return new LocalizedString("NoInstancesFoundForManagementPath", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		public static LocalizedString GroupStateFailed
		{
			get
			{
				return new LocalizedString("GroupStateFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		public static LocalizedString ClusterApiErrorMessage(string method, int error, string message)
		{
			return new LocalizedString("ClusterApiErrorMessage", Strings.ResourceManager, new object[]
			{
				method,
				error,
				message
			});
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001AD10 File Offset: 0x00018F10
		public static LocalizedString ClusCommonValidationFailedException(string error)
		{
			return new LocalizedString("ClusCommonValidationFailedException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001AD38 File Offset: 0x00018F38
		public static LocalizedString ResPropTypeNotSupportedException(string propType)
		{
			return new LocalizedString("ResPropTypeNotSupportedException", Strings.ResourceManager, new object[]
			{
				propType
			});
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001AD60 File Offset: 0x00018F60
		public static LocalizedString DxStorePropertyNotFoundException(string propertyName)
		{
			return new LocalizedString("DxStorePropertyNotFoundException", Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001AD88 File Offset: 0x00018F88
		public static LocalizedString GroupStateUnknown
		{
			get
			{
				return new LocalizedString("GroupStateUnknown", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001AD9F File Offset: 0x00018F9F
		public static LocalizedString GroupStateOnline
		{
			get
			{
				return new LocalizedString("GroupStateOnline", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001ADB6 File Offset: 0x00018FB6
		public static LocalizedString GroupStateOffline
		{
			get
			{
				return new LocalizedString("GroupStateOffline", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001ADD0 File Offset: 0x00018FD0
		public static LocalizedString IPv4AddressesHasDuplicateEntries(string duplicate)
		{
			return new LocalizedString("IPv4AddressesHasDuplicateEntries", Strings.ResourceManager, new object[]
			{
				duplicate
			});
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		public static LocalizedString FailToOnlineClusResourceException(string groupName, string resourceId, string reason)
		{
			return new LocalizedString("FailToOnlineClusResourceException", Strings.ResourceManager, new object[]
			{
				groupName,
				resourceId,
				reason
			});
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001AE28 File Offset: 0x00019028
		public static LocalizedString DxStoreKeyApiOperationException(string operationName, string keyName)
		{
			return new LocalizedString("DxStoreKeyApiOperationException", Strings.ResourceManager, new object[]
			{
				operationName,
				keyName
			});
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001AE54 File Offset: 0x00019054
		public static LocalizedString CouldNotFindServerObject(string serverName)
		{
			return new LocalizedString("CouldNotFindServerObject", Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001AE7C File Offset: 0x0001907C
		public static LocalizedString InvalidSubnetSpec(string userInput)
		{
			return new LocalizedString("InvalidSubnetSpec", Strings.ResourceManager, new object[]
			{
				userInput
			});
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001AEA4 File Offset: 0x000190A4
		public static LocalizedString ClusterException(string clusterError)
		{
			return new LocalizedString("ClusterException", Strings.ResourceManager, new object[]
			{
				clusterError
			});
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001AECC File Offset: 0x000190CC
		public static LocalizedString ClusterNoServerToConnect(string dagName)
		{
			return new LocalizedString("ClusterNoServerToConnect", Strings.ResourceManager, new object[]
			{
				dagName
			});
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001AEF4 File Offset: 0x000190F4
		public static LocalizedString ClusterNotRunningException
		{
			get
			{
				return new LocalizedString("ClusterNotRunningException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001AF0B File Offset: 0x0001910B
		public static LocalizedString RemoteClusterWin2k8ToWin2k3NotSupportedException
		{
			get
			{
				return new LocalizedString("RemoteClusterWin2k8ToWin2k3NotSupportedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001AF24 File Offset: 0x00019124
		public static LocalizedString NoSuchNetwork(string netName)
		{
			return new LocalizedString("NoSuchNetwork", Strings.ResourceManager, new object[]
			{
				netName
			});
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001AF4C File Offset: 0x0001914C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040006C3 RID: 1731
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(11);

		// Token: 0x040006C4 RID: 1732
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Cluster.Shared.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020000B3 RID: 179
		public enum IDs : uint
		{
			// Token: 0x040006C6 RID: 1734
			FixUpIpAddressStatusUnchanged = 1578974238U,
			// Token: 0x040006C7 RID: 1735
			RemoteClusterWin2k3ToWin2k8NotSupportedException = 4106740448U,
			// Token: 0x040006C8 RID: 1736
			GroupStatePending = 3232505775U,
			// Token: 0x040006C9 RID: 1737
			NoErrorSpecified = 801548279U,
			// Token: 0x040006CA RID: 1738
			GroupStatePartialOnline = 487199380U,
			// Token: 0x040006CB RID: 1739
			GroupStateFailed = 3972288899U,
			// Token: 0x040006CC RID: 1740
			GroupStateUnknown = 2874666956U,
			// Token: 0x040006CD RID: 1741
			GroupStateOnline = 2356112387U,
			// Token: 0x040006CE RID: 1742
			GroupStateOffline = 344705101U,
			// Token: 0x040006CF RID: 1743
			ClusterNotRunningException = 330900787U,
			// Token: 0x040006D0 RID: 1744
			RemoteClusterWin2k8ToWin2k3NotSupportedException = 4244700832U
		}

		// Token: 0x020000B4 RID: 180
		private enum ParamIDs
		{
			// Token: 0x040006D2 RID: 1746
			IpResourceCreationOnWrongTypeOfNetworkException,
			// Token: 0x040006D3 RID: 1747
			ClusCommonRetryableTransientException,
			// Token: 0x040006D4 RID: 1748
			OfflineOperationTimedOutException,
			// Token: 0x040006D5 RID: 1749
			RequestedNetworkIsNotDhcpEnabled,
			// Token: 0x040006D6 RID: 1750
			FailedToFindNetwork,
			// Token: 0x040006D7 RID: 1751
			DxStoreKeyApiFailedMessage,
			// Token: 0x040006D8 RID: 1752
			ClusCommonFailException,
			// Token: 0x040006D9 RID: 1753
			ClusResourceAlreadyExistsException,
			// Token: 0x040006DA RID: 1754
			ClusCommonNonRetryableTransientException,
			// Token: 0x040006DB RID: 1755
			RegistryParameterKeyNotOpenedException,
			// Token: 0x040006DC RID: 1756
			RegistryParameterWriteException,
			// Token: 0x040006DD RID: 1757
			RegistryParameterException,
			// Token: 0x040006DE RID: 1758
			ClusterFileNotFoundException,
			// Token: 0x040006DF RID: 1759
			AmGetFqdnFailedADError,
			// Token: 0x040006E0 RID: 1760
			ClusCommonTransientException,
			// Token: 0x040006E1 RID: 1761
			ClusterEvictWithoutCleanupException,
			// Token: 0x040006E2 RID: 1762
			ClusterNotJoinedException,
			// Token: 0x040006E3 RID: 1763
			AmCoreGroupRegNotFound,
			// Token: 0x040006E4 RID: 1764
			IPv4NetworksHasDuplicateEntries,
			// Token: 0x040006E5 RID: 1765
			AmServerNameResolveFqdnException,
			// Token: 0x040006E6 RID: 1766
			ClusCommonTaskPendingException,
			// Token: 0x040006E7 RID: 1767
			ClusterNotInstalledException,
			// Token: 0x040006E8 RID: 1768
			InvalidResourceOpException,
			// Token: 0x040006E9 RID: 1769
			ClusterNodeNotFoundException,
			// Token: 0x040006EA RID: 1770
			RequestedNetworkIsNotIPv6Enabled,
			// Token: 0x040006EB RID: 1771
			ClusterApiException,
			// Token: 0x040006EC RID: 1772
			DxStoreKeyNotFoundException,
			// Token: 0x040006ED RID: 1773
			RegistryParameterReadException,
			// Token: 0x040006EE RID: 1774
			DxStoreKeyInvalidKeyException,
			// Token: 0x040006EF RID: 1775
			DagTaskNotEnoughStaticIPAddresses,
			// Token: 0x040006F0 RID: 1776
			ClusteringNotInstalledOnLHException,
			// Token: 0x040006F1 RID: 1777
			OperationValidOnlyOnLonghornException,
			// Token: 0x040006F2 RID: 1778
			ClusterUnsupportedRegistryTypeException,
			// Token: 0x040006F3 RID: 1779
			FixUpIpAddressStatusUpdated,
			// Token: 0x040006F4 RID: 1780
			NoClusResourceFoundException,
			// Token: 0x040006F5 RID: 1781
			AmGetFqdnFailedNotFound,
			// Token: 0x040006F6 RID: 1782
			FailToOfflineClusResourceException,
			// Token: 0x040006F7 RID: 1783
			ClusterNodeJoinedException,
			// Token: 0x040006F8 RID: 1784
			ClusterDatabaseTransientException,
			// Token: 0x040006F9 RID: 1785
			OpenClusterTimedoutException,
			// Token: 0x040006FA RID: 1786
			IPv6NetworksHasDuplicateEntries,
			// Token: 0x040006FB RID: 1787
			NoInstancesFoundForManagementPath,
			// Token: 0x040006FC RID: 1788
			ClusterApiErrorMessage,
			// Token: 0x040006FD RID: 1789
			ClusCommonValidationFailedException,
			// Token: 0x040006FE RID: 1790
			ResPropTypeNotSupportedException,
			// Token: 0x040006FF RID: 1791
			DxStorePropertyNotFoundException,
			// Token: 0x04000700 RID: 1792
			IPv4AddressesHasDuplicateEntries,
			// Token: 0x04000701 RID: 1793
			FailToOnlineClusResourceException,
			// Token: 0x04000702 RID: 1794
			DxStoreKeyApiOperationException,
			// Token: 0x04000703 RID: 1795
			CouldNotFindServerObject,
			// Token: 0x04000704 RID: 1796
			InvalidSubnetSpec,
			// Token: 0x04000705 RID: 1797
			ClusterException,
			// Token: 0x04000706 RID: 1798
			ClusterNoServerToConnect,
			// Token: 0x04000707 RID: 1799
			NoSuchNetwork
		}
	}
}
