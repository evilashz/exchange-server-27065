using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Win32;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000061 RID: 97
	internal static class ClusapiMethods
	{
		// Token: 0x0600029F RID: 671
		[DllImport("clusapi.dll", SetLastError = true)]
		public static extern AmClusterRegkeyHandle GetClusterResourceKey(AmClusterResourceHandle clusterResourceHandle, RegSAM samDesired);

		// Token: 0x060002A0 RID: 672
		[DllImport("clusapi.dll", SetLastError = true)]
		public static extern AmClusterRegkeyHandle GetClusterNetworkKey(AmClusterNetworkHandle clusterNetworkHandle, RegSAM samDesired);

		// Token: 0x060002A1 RID: 673
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		public static extern int ClusterRegEnumKey(AmClusterRegkeyHandle RegKeyHandle, int dwIndex, StringBuilder lpszName, ref int lpcchName, IntPtr lpftLastWriteTime);

		// Token: 0x060002A2 RID: 674
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		public static extern int ClusterRegEnumValue(AmClusterRegkeyHandle RegKeyHandle, int dwIndex, StringBuilder lpszName, ref int lpcchName, ref int lpdwType, IntPtr lpData, ref int lpcbData);

		// Token: 0x060002A3 RID: 675
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegDeleteKey")]
		public static extern int ClusterRegDeleteKeyUnsafe(AmClusterRegkeyHandle RegKeyHandle, string lpszSubKey);

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public static int ClusterRegDeleteKey(AmClusterRegkeyHandle RegKeyHandle, string lpszSubKey)
		{
			AmClusterRegkeyHandle amClusterRegkeyHandle = null;
			int retCode = ClusapiMethods.ClusterRegOpenKey(RegKeyHandle, lpszSubKey, RegSAM.Read, out amClusterRegkeyHandle);
			if (retCode == 0)
			{
				amClusterRegkeyHandle.Dispose();
				retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegDeleteKey, string.Format("RootKey = {0} Subkey = {1}", RegKeyHandle.Name, lpszSubKey), delegate
				{
					retCode = ClusapiMethods.ClusterRegDeleteKeyUnsafe(RegKeyHandle, lpszSubKey);
					return retCode;
				});
			}
			return retCode;
		}

		// Token: 0x060002A5 RID: 677
		[DllImport("resutils.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern uint ResUtilFindDwordProperty([In] IntPtr pPropertyList, [In] uint cbPropertyListSize, [In] string pszPropertyName, out uint pdwPropertyValue);

		// Token: 0x060002A6 RID: 678
		[DllImport("resutils.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern uint ResUtilFindSzProperty([In] IntPtr pPropertyList, [In] uint cbPropertyListSize, [In] string pszPropertyName, [MarshalAs(UnmanagedType.LPWStr)] out string propertyValue);

		// Token: 0x060002A7 RID: 679
		[DllImport("resutils.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern uint ResUtilFindMultiSzProperty([In] IntPtr pPropertyList, [In] uint cbPropertyListSize, [In] string pszPropertyName, out SafeHGlobalHandle pbPropertyValue, out uint pcbPropertyValueSize);

		// Token: 0x060002A8 RID: 680
		[DllImport("resutils.dll")]
		public static extern uint ResUtilVerifyPrivatePropertyList([In] IntPtr pInPropertyList, [In] int cbInPropertyListSize);

		// Token: 0x060002A9 RID: 681
		[DllImport("msvcrt.dll")]
		public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);

		// Token: 0x060002AA RID: 682
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenCluster", SetLastError = true)]
		private static extern AmClusterHandle OpenClusterInternal([In] string clusterName);

		// Token: 0x060002AB RID: 683 RVA: 0x0000CC70 File Offset: 0x0000AE70
		internal static AmClusterHandle OpenCluster([In] string clusterName)
		{
			AmClusterHandle handle = null;
			ClusApiHook.CallBackDriver(ClusApiHooks.OpenCluster, string.Format("clusterName = {0}", clusterName), delegate
			{
				int result = 0;
				handle = ClusapiMethods.OpenClusterInternal(clusterName);
				if (handle == null || handle.IsInvalid)
				{
					result = Marshal.GetLastWin32Error();
				}
				return result;
			});
			return handle;
		}

		// Token: 0x060002AC RID: 684
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll", EntryPoint = "CloseCluster")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseClusterInternal([In] IntPtr hCluster);

		// Token: 0x060002AD RID: 685 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		internal static bool CloseCluster([In] IntPtr hCluster)
		{
			bool isSuccess = false;
			ClusApiHook.CallBackDriver(ClusApiHooks.CloseCluster, string.Format("hCluster = {0}", hCluster), delegate
			{
				int result = 0;
				isSuccess = ClusapiMethods.CloseClusterInternal(hCluster);
				return result;
			});
			return isSuccess;
		}

		// Token: 0x060002AE RID: 686
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetNodeClusterState([In] string nodeName, [In] [Out] ref AmNodeClusterState dwClusterState);

		// Token: 0x060002AF RID: 687
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterHandle CreateCluster([In] SafeHGlobalHandle pconfig, [In] [Optional] ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK pfnProgressCallback, [In] [Optional] IntPtr pvCallbackArg);

		// Token: 0x060002B0 RID: 688
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterHandle CreateCluster([In] IntPtr pconfig, [In] [Optional] IntPtr pfnProgressCallback, [In] [Optional] IntPtr pvCallbackArg);

		// Token: 0x060002B1 RID: 689
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern uint DestroyCluster([In] AmClusterHandle hCluster, [In] [Optional] IntPtr pfnProgressCallback, [In] [Optional] IntPtr pvCallbackArg, [In] uint fdeleteVirtualComputerObjects);

		// Token: 0x060002B2 RID: 690
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern uint DestroyCluster([In] AmClusterHandle hCluster, [In] [Optional] ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK pfnProgressCallback, [In] [Optional] IntPtr pvCallbackArg, [In] uint fdeleteVirtualComputerObjects);

		// Token: 0x060002B3 RID: 691
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern uint GetClusterInformation([In] AmClusterHandle hCluster, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder clusterName, ref int clusterNameLength, IntPtr clusterInformation);

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CD38 File Offset: 0x0000AF38
		internal static IntPtr ClusterIpEntryArrayToIntPtr(ClusapiMethods.CLUSTER_IP_ENTRY[] ipEntryArray)
		{
			int num = ipEntryArray.Length;
			int num2 = Marshal.SizeOf(typeof(ClusapiMethods.CLUSTER_IP_ENTRY));
			IntPtr result = Marshal.AllocHGlobal(num2 * num);
			for (int i = 0; i < num; i++)
			{
				IntPtr ptr = (IntPtr)(result.ToInt64() + (long)(i * num2));
				Marshal.StructureToPtr(ipEntryArray[i], ptr, false);
			}
			return result;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000CD90 File Offset: 0x0000AF90
		internal static IntPtr StringArrayToIntPtr(string[] stringArray)
		{
			int num = stringArray.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * num);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteIntPtr(intPtr, Marshal.SizeOf(typeof(IntPtr)) * i, Marshal.StringToHGlobalUni(stringArray[i]));
			}
			return intPtr;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		internal static void FreeIntPtrOfMarshalledObjectsArray(IntPtr MarshalledArrayPtr, int numStrings)
		{
			for (int i = 0; i < numStrings; i++)
			{
				IntPtr hglobal = Marshal.ReadIntPtr(MarshalledArrayPtr, Marshal.SizeOf(typeof(IntPtr)) * i);
				Marshal.FreeHGlobal(hglobal);
			}
			Marshal.FreeHGlobal(MarshalledArrayPtr);
		}

		// Token: 0x060002B7 RID: 695
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterResourceHandle OpenClusterResource([In] AmClusterHandle hCluster, [In] string resourceName);

		// Token: 0x060002B8 RID: 696
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClusterResource([In] IntPtr hResource);

		// Token: 0x060002B9 RID: 697
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern AmResourceState GetClusterResourceState([In] AmClusterResourceHandle hresource, [Out] StringBuilder owningNodeName, [In] [Out] ref uint nodeNameLength, [Out] StringBuilder owningGroupName, [In] [Out] ref uint groupNameLength);

		// Token: 0x060002BA RID: 698
		[DllImport("clusapi.dll")]
		internal static extern uint OnlineClusterResource([In] AmClusterResourceHandle hresource);

		// Token: 0x060002BB RID: 699
		[DllImport("clusapi.dll")]
		internal static extern uint OfflineClusterResource([In] AmClusterResourceHandle hresource);

		// Token: 0x060002BC RID: 700
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern AmClusterResourceHandle CreateClusterResource([In] AmClusterGroupHandle hGroup, [In] string lpszResourceName, [In] string lpszResourceType, [In] ClusterResourceCreateFlags dwFlags);

		// Token: 0x060002BD RID: 701
		[DllImport("clusapi.dll")]
		internal static extern uint DeleteClusterResource([In] AmClusterResourceHandle hResource);

		// Token: 0x060002BE RID: 702
		[DllImport("clusapi.dll")]
		internal static extern uint RemoveClusterResourceDependency([In] AmClusterResourceHandle hResource, [In] AmClusterResourceHandle hDependsOn);

		// Token: 0x060002BF RID: 703
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint SetClusterResourceDependencyExpression([In] AmClusterResourceHandle clusterResourceHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string dependencyExpression);

		// Token: 0x060002C0 RID: 704
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint GetClusterQuorumResource([In] AmClusterHandle hCluster, StringBuilder lpszResourceName, [In] [Out] ref uint lpcchResourceName, StringBuilder lpszDeviceName, [In] [Out] ref uint lpcchDeviceName, out uint lpdwMaxQuorumLogSize);

		// Token: 0x060002C1 RID: 705
		[DllImport("clusapi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint SetClusterQuorumResource([In] AmClusterResourceHandle hResource, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string deviceName, [In] uint maxQuorumLogSize);

		// Token: 0x060002C2 RID: 706
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int ClusterResourceControl([In] AmClusterResourceHandle hResource, [In] IntPtr hNode, [In] AmClusterResourceControlCode controlCode, [In] IntPtr inBuffer, [In] uint inBufferSize, [Out] IntPtr outBuffer, [In] uint outBufferSize, out uint bytesReturned);

		// Token: 0x060002C3 RID: 707
		[DllImport("clusapi.dll")]
		internal static extern uint AddClusterResourceNode([In] AmClusterResourceHandle hResource, [In] AmClusterNodeHandle hNode);

		// Token: 0x060002C4 RID: 708
		[DllImport("clusapi.dll")]
		internal static extern uint RemoveClusterResourceNode([In] AmClusterResourceHandle hResource, [In] AmClusterNodeHandle hNode);

		// Token: 0x060002C5 RID: 709
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterGroupHandle OpenClusterGroup([In] AmClusterHandle hCluster, [In] string groupName);

		// Token: 0x060002C6 RID: 710
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClusterGroup([In] IntPtr hGroup);

		// Token: 0x060002C7 RID: 711
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmGroupState GetClusterGroupState([In] AmClusterGroupHandle hGroup, StringBuilder nodeName, ref int nodeNameLenth);

		// Token: 0x060002C8 RID: 712
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterGroupControl([In] AmClusterGroupHandle hGroup, [In] IntPtr hNode, [In] AmClusterGroupControlCode controlCode, [In] IntPtr inBuffer, [In] uint inBufferSize, [Out] IntPtr outBuffer, [In] uint outBufferSize, out uint bytesReturned);

		// Token: 0x060002C9 RID: 713
		[DllImport("clusapi.dll")]
		internal static extern uint MoveClusterGroup(AmClusterGroupHandle hGroup, AmClusterNodeHandle hDestinationNode);

		// Token: 0x060002CA RID: 714
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterNodeHandle OpenClusterNode([In] AmClusterHandle hCluster, [In] string nodeName);

		// Token: 0x060002CB RID: 715
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClusterNode([In] IntPtr hNode);

		// Token: 0x060002CC RID: 716
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmNodeState GetClusterNodeState([In] AmClusterNodeHandle hNode);

		// Token: 0x060002CD RID: 717
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterNodeHandle AddClusterNode([In] AmClusterHandle hCluster, [In] string lpszNodeName, [In] [Optional] ClusapiMethods.PCLUSTER_SETUP_PROGRESS_CALLBACK pfnProgressCallback, [In] [Optional] IntPtr pvCallbackArg);

		// Token: 0x060002CE RID: 718
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint EvictClusterNodeEx([In] AmClusterNodeHandle hNode, [In] uint dwTimeOut, out int phrCleanupStatus);

		// Token: 0x060002CF RID: 719
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterNodeControl([In] AmClusterNodeHandle hNode, [In] IntPtr hHostNode, [In] AmClusterNodeControlCode controlCode, [In] IntPtr inBuffer, [In] uint inBufferSize, [Out] IntPtr outBuffer, [In] uint outBufferSize, out uint bytesReturned);

		// Token: 0x060002D0 RID: 720
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterNetworkHandle OpenClusterNetwork([In] AmClusterHandle hCluster, [In] string networkName);

		// Token: 0x060002D1 RID: 721
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClusterNetwork([In] IntPtr hNetwork);

		// Token: 0x060002D2 RID: 722
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmNetworkState GetClusterNetworkState([In] AmClusterNetworkHandle hNetwork);

		// Token: 0x060002D3 RID: 723
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int ClusterNetworkControl([In] AmClusterNetworkHandle hCluster, [In] IntPtr hHostNode, [In] AmClusterNetworkControlCode controlCode, [In] IntPtr inBuffer, [In] uint inBufferSize, [Out] IntPtr outBuffer, [In] uint outBufferSize, out uint bytesReturned);

		// Token: 0x060002D4 RID: 724
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern AmClusterNetInterfaceHandle OpenClusterNetInterface([In] AmClusterHandle hCluster, [In] string networkName);

		// Token: 0x060002D5 RID: 725
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClusterNetInterface([In] IntPtr hNetInterface);

		// Token: 0x060002D6 RID: 726
		[DllImport("clusapi.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern AmNetInterfaceState GetClusterNetInterfaceState([In] AmClusterNetInterfaceHandle hNetInterface);

		// Token: 0x060002D7 RID: 727
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int ClusterNetInterfaceControl([In] AmClusterNetInterfaceHandle hCluster, [In] IntPtr hHostNode, [In] AmClusterNetInterfaceControlCode controlCode, [In] IntPtr inBuffer, [In] uint inBufferSize, [Out] IntPtr outBuffer, [In] uint outBufferSize, out uint bytesReturned);

		// Token: 0x060002D8 RID: 728
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmClusEnumHandle ClusterOpenEnum([In] AmClusterHandle hCluster, [In] AmClusterEnum dwType);

		// Token: 0x060002D9 RID: 729
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterEnum([In] AmClusEnumHandle hEnum, [In] int dwIndex, out AmClusterEnum lpdwType, [Out] StringBuilder pName, [In] [Out] ref int count);

		// Token: 0x060002DA RID: 730
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		internal static extern int ClusterCloseEnum([In] IntPtr hEnum);

		// Token: 0x060002DB RID: 731
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int ClusterControl([In] AmClusterHandle hCluster, [In] IntPtr hHostNode, [In] AmClusterClusterControlCode controlCode, [In] IntPtr inBuffer, [In] uint inBufferSize, [Out] IntPtr outBuffer, [In] uint outBufferSize, out uint bytesReturned);

		// Token: 0x060002DC RID: 732
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmClusGroupEnumHandle ClusterGroupOpenEnum([In] AmClusterGroupHandle hCluster, [In] AmClusterGroupEnum dwType);

		// Token: 0x060002DD RID: 733
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterGroupEnum([In] AmClusGroupEnumHandle hEnum, [In] int dwIndex, out AmClusterGroupEnum lpdwType, [Out] StringBuilder pName, [In] [Out] ref int count);

		// Token: 0x060002DE RID: 734
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		internal static extern int ClusterGroupCloseEnum([In] IntPtr hEnum);

		// Token: 0x060002DF RID: 735
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmClusResourceEnumHandle ClusterResourceOpenEnum([In] AmClusterResourceHandle hCluster, [In] AmClusterResourceEnum dwType);

		// Token: 0x060002E0 RID: 736
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterResourceEnum([In] AmClusResourceEnumHandle hEnum, [In] int dwIndex, out AmClusterResourceEnum lpdwType, [Out] StringBuilder pName, [In] [Out] ref int count);

		// Token: 0x060002E1 RID: 737
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		internal static extern int ClusterResourceCloseEnum([In] IntPtr hEnum);

		// Token: 0x060002E2 RID: 738
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmClusNodeEnumHandle ClusterNodeOpenEnum([In] AmClusterNodeHandle hCluster, [In] AmClusterNodeEnum dwType);

		// Token: 0x060002E3 RID: 739
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterNodeEnum([In] AmClusNodeEnumHandle hEnum, [In] int dwIndex, out AmClusterNodeEnum lpdwType, [Out] StringBuilder pName, [In] [Out] ref int count);

		// Token: 0x060002E4 RID: 740
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		internal static extern int ClusterNodeCloseEnum([In] IntPtr hEnum);

		// Token: 0x060002E5 RID: 741
		[DllImport("clusapi.dll", SetLastError = true)]
		internal static extern AmClusNetworkEnumHandle ClusterNetworkOpenEnum([In] AmClusterNetworkHandle hCluster, [In] AmClusterNetworkEnum dwType);

		// Token: 0x060002E6 RID: 742
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int ClusterNetworkEnum([In] AmClusNetworkEnumHandle hEnum, [In] int dwIndex, out AmClusterNetworkEnum lpdwType, [Out] StringBuilder pName, [In] [Out] ref int count);

		// Token: 0x060002E7 RID: 743
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll")]
		internal static extern int ClusterNetworkCloseEnum([In] IntPtr hEnum);

		// Token: 0x060002E8 RID: 744
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern AmClusterNotifyHandle CreateClusterNotifyPort([In] AmClusterNotifyHandle hChange, [In] AmClusterHandle hCluster, [In] ClusterNotifyFlags dwFilter, [In] IntPtr dwNotifyKey);

		// Token: 0x060002E9 RID: 745
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetClusterNotify([In] AmClusterNotifyHandle hChange, out IntPtr dwNotifyKey, out ClusterNotifyFlags dwFilter, [MarshalAs(UnmanagedType.LPWStr)] [In] [Out] StringBuilder lpszName, [In] [Out] ref uint cbNameSize, [In] uint dwMillisecTimeout);

		// Token: 0x060002EA RID: 746
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode)]
		internal static extern int RegisterClusterNotify([In] AmClusterNotifyHandle hChange, [In] ClusterNotifyFlags dwFilter, [In] SafeHandle hObject, [In] IntPtr dwNotifyKey);

		// Token: 0x060002EB RID: 747
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClusterNotifyPort([In] IntPtr hChange);

		// Token: 0x060002EC RID: 748
		[DllImport("clusapi.dll", EntryPoint = "GetClusterKey", SetLastError = true)]
		private static extern AmClusterRegkeyHandle GetClusterKeyInternal(AmClusterHandle clusterHandle, RegSAM samDesired);

		// Token: 0x060002ED RID: 749 RVA: 0x0000CE80 File Offset: 0x0000B080
		internal static AmClusterRegkeyHandle GetClusterKey(AmClusterHandle clusterHandle, RegSAM samDesired)
		{
			AmClusterRegkeyHandle handle = null;
			ClusApiHook.CallBackDriver(ClusApiHooks.GetClusterKey, string.Format("clusterHandle = {0} samDesired = {1}", clusterHandle, samDesired), delegate
			{
				int result = 0;
				handle = ClusapiMethods.GetClusterKeyInternal(clusterHandle, samDesired);
				if (handle == null || handle.IsInvalid)
				{
					result = Marshal.GetLastWin32Error();
				}
				else
				{
					handle.Name = "HKLM:\\Cluster";
				}
				return result;
			});
			return handle;
		}

		// Token: 0x060002EE RID: 750
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegOpenKey")]
		private static extern int ClusterRegOpenKeyInternal(AmClusterRegkeyHandle RegKeyHandle, string lpszSubKey, RegSAM samDesired, out AmClusterRegkeyHandle phkResult);

		// Token: 0x060002EF RID: 751 RVA: 0x0000CF50 File Offset: 0x0000B150
		internal static int ClusterRegOpenKey(AmClusterRegkeyHandle RegKeyHandle, string lpszSubKey, RegSAM samDesired, out AmClusterRegkeyHandle phkResult)
		{
			AmClusterRegkeyHandle phkResultTmp = null;
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegOpenKey, string.Format("RootKeyName = {0} SubKey = {1}", RegKeyHandle.Name, lpszSubKey), delegate
			{
				retCode = ClusapiMethods.ClusterRegOpenKeyInternal(RegKeyHandle, lpszSubKey, samDesired, out phkResultTmp);
				if (retCode == 0 && phkResultTmp != null)
				{
					phkResultTmp.Name = RegKeyHandle.Name + "\\" + lpszSubKey;
				}
				return retCode;
			});
			phkResult = phkResultTmp;
			return retCode;
		}

		// Token: 0x060002F0 RID: 752
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegCreateKey")]
		private static extern int ClusterRegCreateKeyInternal(AmClusterRegkeyHandle RegKeyHandle, string lpszSubKey, uint options, RegSAM samDesired, IntPtr securityAttributes, out AmClusterRegkeyHandle phkResult, out uint disposition);

		// Token: 0x060002F1 RID: 753 RVA: 0x0000D048 File Offset: 0x0000B248
		internal static int ClusterRegCreateKey(AmClusterRegkeyHandle RegKeyHandle, string lpszSubKey, uint options, RegSAM samDesired, IntPtr securityAttributes, out AmClusterRegkeyHandle phkResult, out uint disposition)
		{
			AmClusterRegkeyHandle phkResultTmp = null;
			uint dispositionTmp = 0U;
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegCreateKey, string.Format("RootKeyName = {0} SubKey = {1}", RegKeyHandle.Name, lpszSubKey), delegate
			{
				retCode = ClusapiMethods.ClusterRegCreateKeyInternal(RegKeyHandle, lpszSubKey, options, samDesired, securityAttributes, out phkResultTmp, out dispositionTmp);
				if (retCode == 0 && phkResultTmp != null)
				{
					phkResultTmp.Name = RegKeyHandle.Name + "\\" + lpszSubKey;
				}
				return retCode;
			});
			phkResult = phkResultTmp;
			disposition = dispositionTmp;
			return retCode;
		}

		// Token: 0x060002F2 RID: 754
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegQueryValue")]
		private static extern int ClusterRegQueryValueInternal(AmClusterRegkeyHandle RegKeyHandle, string lpszValueName, out int lpdwValueType, IntPtr lpbData, ref int lpcbData);

		// Token: 0x060002F3 RID: 755 RVA: 0x0000D114 File Offset: 0x0000B314
		internal static int ClusterRegQueryValue(AmClusterRegkeyHandle RegKeyHandle, string lpszValueName, out RegistryValueKind valueType, IntPtr lpbData, ref int lpcbData)
		{
			int lpcbDataTmp = lpcbData;
			int valueTypeInt = 0;
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegQueryValue, string.Format("KeyName = {0} lpszValueName = {1}", RegKeyHandle.Name, lpszValueName), delegate
			{
				retCode = ClusapiMethods.ClusterRegQueryValueInternal(RegKeyHandle, lpszValueName, out valueTypeInt, lpbData, ref lpcbDataTmp);
				return retCode;
			});
			valueType = (RegistryValueKind)valueTypeInt;
			lpcbData = lpcbDataTmp;
			return retCode;
		}

		// Token: 0x060002F4 RID: 756
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegSetValue")]
		private static extern int ClusterRegSetValueInternal(AmClusterRegkeyHandle RegKeyHandle, string lpszValueName, RegistryValueKind dwType, IntPtr lpbData, int cbData);

		// Token: 0x060002F5 RID: 757 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		internal static int ClusterRegSetValue(AmClusterRegkeyHandle RegKeyHandle, string lpszValueName, RegistryValueKind dwType, IntPtr lpbData, int cbData)
		{
			string text = "ClusterRegSetValue";
			string text2 = string.Format("KeyName = {0} lpszValueName = {1}", RegKeyHandle.Name, lpszValueName);
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegSetValue, text2, delegate
			{
				retCode = ClusapiMethods.ClusterRegSetValueInternal(RegKeyHandle, lpszValueName, dwType, lpbData, cbData);
				return retCode;
			});
			if (retCode != 0)
			{
				ReplayCrimsonEvents.CriticalClusterApiFailed.Log<string, string, int>(text, text2, retCode);
			}
			return retCode;
		}

		// Token: 0x060002F6 RID: 758
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegDeleteValue")]
		private static extern int ClusterRegDeleteValueUnsafe(AmClusterRegkeyHandle RegKeyHandle, string lpszValueName);

		// Token: 0x060002F7 RID: 759 RVA: 0x0000D28C File Offset: 0x0000B48C
		internal static int ClusterRegDeleteValue(AmClusterRegkeyHandle RegKeyHandle, string lpszValueName)
		{
			int num = 0;
			RegistryValueKind registryValueKind;
			int retCode = ClusapiMethods.ClusterRegQueryValue(RegKeyHandle, lpszValueName, out registryValueKind, IntPtr.Zero, ref num);
			if (retCode == 0)
			{
				retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegDeleteValue, string.Format("KeyName = {0} lpszValueName = {1}", RegKeyHandle.Name, lpszValueName), delegate
				{
					retCode = ClusapiMethods.ClusterRegDeleteValueUnsafe(RegKeyHandle, lpszValueName);
					return retCode;
				});
			}
			return retCode;
		}

		// Token: 0x060002F8 RID: 760
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("clusapi.dll", EntryPoint = "ClusterRegCloseKey")]
		private static extern int ClusterRegCloseKeyInternal(IntPtr RegKeyHandle);

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D33C File Offset: 0x0000B53C
		internal static int ClusterRegCloseKey(IntPtr RegKeyHandle, string name)
		{
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegCloseKey, string.Format("KeyName = {0}, Handle = {1}", name, RegKeyHandle), delegate
			{
				retCode = ClusapiMethods.ClusterRegCloseKeyInternal(RegKeyHandle);
				return retCode;
			});
			return retCode;
		}

		// Token: 0x060002FA RID: 762
		[DllImport("clusapi.dll", EntryPoint = "ClusterRegCreateBatch")]
		private static extern int ClusterRegCreateBatchInternal([In] AmClusterRegkeyHandle hKey, out AmClusterBatchHandle hBatch);

		// Token: 0x060002FB RID: 763 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		internal static int ClusterRegCreateBatch([In] AmClusterRegkeyHandle hKey, out AmClusterBatchHandle hBatch)
		{
			AmClusterBatchHandle hBatchTmp = null;
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegCreateBatch, string.Format("KeyName = {0}", hKey.Name), delegate
			{
				retCode = ClusapiMethods.ClusterRegCreateBatchInternal(hKey, out hBatchTmp);
				return retCode;
			});
			hBatch = hBatchTmp;
			return retCode;
		}

		// Token: 0x060002FC RID: 764
		[DllImport("clusapi.dll", CharSet = CharSet.Unicode, EntryPoint = "ClusterRegBatchAddCommand")]
		private static extern int ClusterRegBatchAddCommandInternal([In] AmClusterBatchHandle hBatch, [In] CLUSTER_REG_COMMAND dwCommand, [MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, [In] RegistryValueKind dwOptions, [In] IntPtr lpData, [In] int cbData);

		// Token: 0x060002FD RID: 765 RVA: 0x0000D450 File Offset: 0x0000B650
		internal static int ClusterRegBatchAddCommand([In] AmClusterBatchHandle hBatch, [In] CLUSTER_REG_COMMAND dwCommand, [MarshalAs(UnmanagedType.LPWStr)] [In] string wzName, [In] RegistryValueKind dwOptions, [In] IntPtr lpData, [In] int cbData)
		{
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegBatchAddCommand, string.Format("hBatch = {0} dwCommand = {1} wsName = {2}", hBatch, dwCommand, wzName), delegate
			{
				retCode = ClusapiMethods.ClusterRegBatchAddCommandInternal(hBatch, dwCommand, wzName, dwOptions, lpData, cbData);
				return retCode;
			});
			return retCode;
		}

		// Token: 0x060002FE RID: 766
		[DllImport("clusapi.dll", EntryPoint = "ClusterRegCloseBatch")]
		private static extern int ClusterRegCloseBatchInternal([In] IntPtr hBatch, [MarshalAs(UnmanagedType.Bool)] [In] bool bCommit, [Optional] out int failedCommandNumber);

		// Token: 0x060002FF RID: 767 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		internal static int ClusterRegCloseBatch([In] IntPtr hBatch, [MarshalAs(UnmanagedType.Bool)] [In] bool bCommit, [Optional] out int failedCommandNumber)
		{
			int failedCommandNumberTmp = 0;
			int retCode = ClusApiHook.CallBackDriver(ClusApiHooks.ClusterRegCloseBatch, string.Format("hBatch = {0} bCommit = {1}", hBatch, bCommit), delegate
			{
				retCode = ClusapiMethods.ClusterRegCloseBatchInternal(hBatch, bCommit, out failedCommandNumberTmp);
				return retCode;
			});
			failedCommandNumber = failedCommandNumberTmp;
			return retCode;
		}

		// Token: 0x040001AA RID: 426
		internal const int MAX_PATH = 260;

		// Token: 0x040001AB RID: 427
		internal const int INVALID_HANDLE_VALUE = -1;

		// Token: 0x040001AC RID: 428
		internal const uint CREATE_CLUSTER_VERSION = 1536U;

		// Token: 0x040001AD RID: 429
		internal const uint CLUSAPI_VERSION_WINDOWSBLUE = 1794U;

		// Token: 0x040001AE RID: 430
		private const string CLUSAPI = "clusapi.dll";

		// Token: 0x040001AF RID: 431
		private const string ResUtilsDll = "resutils.dll";

		// Token: 0x040001B0 RID: 432
		private const string CrtDll = "msvcrt.dll";

		// Token: 0x02000062 RID: 98
		public enum CLUSTER_SETUP_PHASE
		{
			// Token: 0x040001B2 RID: 434
			ClusterSetupPhaseInitialize = 1,
			// Token: 0x040001B3 RID: 435
			ClusterSetupPhaseValidateNodeState = 100,
			// Token: 0x040001B4 RID: 436
			ClusterSetupPhaseValidateClusterNameAccount_Obsolete,
			// Token: 0x040001B5 RID: 437
			ClusterSetupPhaseValidateNetft,
			// Token: 0x040001B6 RID: 438
			ClusterSetupPhaseValidateClusDisk,
			// Token: 0x040001B7 RID: 439
			ClusterSetupPhaseConfigureClusSvc,
			// Token: 0x040001B8 RID: 440
			ClusterSetupPhaseStartingClusSvc,
			// Token: 0x040001B9 RID: 441
			ClusterSetupPhaseQueryClusterNameAccount,
			// Token: 0x040001BA RID: 442
			ClusterSetupPhaseValidateClusterNameAccount,
			// Token: 0x040001BB RID: 443
			ClusterSetupPhaseCreateClusterAccount,
			// Token: 0x040001BC RID: 444
			ClusterSetupPhaseConfigureClusterAccount,
			// Token: 0x040001BD RID: 445
			ClusterSetupPhaseFormingCluster = 200,
			// Token: 0x040001BE RID: 446
			ClusterSetupPhaseAddClusterProperties,
			// Token: 0x040001BF RID: 447
			ClusterSetupPhaseCreateResourceTypes,
			// Token: 0x040001C0 RID: 448
			ClusterSetupPhaseCreateGroups,
			// Token: 0x040001C1 RID: 449
			ClusterSetupPhaseCreateIPAddressResources,
			// Token: 0x040001C2 RID: 450
			ClusterSetupPhaseCreateNetworkName,
			// Token: 0x040001C3 RID: 451
			ClusterSetupPhaseClusterGroupOnline,
			// Token: 0x040001C4 RID: 452
			ClusterSetupPhaseGettingCurrentMembership = 300,
			// Token: 0x040001C5 RID: 453
			ClusterSetupPhaseAddNodeToCluster,
			// Token: 0x040001C6 RID: 454
			ClusterSetupPhaseNodeUp,
			// Token: 0x040001C7 RID: 455
			ClusterSetupPhaseMoveGroup = 400,
			// Token: 0x040001C8 RID: 456
			ClusterSetupPhaseDeleteGroup,
			// Token: 0x040001C9 RID: 457
			ClusterSetupPhaseCleanupCOs,
			// Token: 0x040001CA RID: 458
			ClusterSetupPhaseOfflineGroup,
			// Token: 0x040001CB RID: 459
			ClusterSetupPhaseEvictNode,
			// Token: 0x040001CC RID: 460
			ClusterSetupPhaseCleanupNode,
			// Token: 0x040001CD RID: 461
			ClusterSetupPhaseCoreGroupCleanup,
			// Token: 0x040001CE RID: 462
			ClusterSetupPhaseFailureCleanup = 999
		}

		// Token: 0x02000063 RID: 99
		public enum CLUSTER_SETUP_PHASE_TYPE
		{
			// Token: 0x040001D0 RID: 464
			ClusterSetupPhaseStart = 1,
			// Token: 0x040001D1 RID: 465
			ClusterSetupPhaseContinue,
			// Token: 0x040001D2 RID: 466
			ClusterSetupPhaseEnd
		}

		// Token: 0x02000064 RID: 100
		public enum CLUSTER_SETUP_PHASE_SEVERITY
		{
			// Token: 0x040001D4 RID: 468
			ClusterSetupPhaseInformational = 1,
			// Token: 0x040001D5 RID: 469
			ClusterSetupPhaseWarning,
			// Token: 0x040001D6 RID: 470
			ClusterSetupPhaseFatal
		}

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x06000301 RID: 769
		public delegate int PCLUSTER_SETUP_PROGRESS_CALLBACK(IntPtr pvCallbackArg, ClusapiMethods.CLUSTER_SETUP_PHASE eSetupPhase, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE ePhaseType, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY ePhaseSeverity, uint dwPercentComplete, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpszObjectName, uint dwStatus);

		// Token: 0x02000066 RID: 102
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class CLUSTER_IP_ENTRY
		{
			// Token: 0x06000304 RID: 772 RVA: 0x0000D56A File Offset: 0x0000B76A
			public CLUSTER_IP_ENTRY(string ipaddr, uint prefixLength)
			{
				this.szIpAddress = ipaddr;
				this.dwPrefixLength = prefixLength;
			}

			// Token: 0x040001D7 RID: 471
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string szIpAddress;

			// Token: 0x040001D8 RID: 472
			internal uint dwPrefixLength;
		}

		// Token: 0x02000067 RID: 103
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class CREATE_CLUSTER_CONFIG
		{
			// Token: 0x040001D9 RID: 473
			internal uint dwVersion;

			// Token: 0x040001DA RID: 474
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string lpszClusterName;

			// Token: 0x040001DB RID: 475
			internal uint cNodes;

			// Token: 0x040001DC RID: 476
			internal IntPtr ppszNodeNames;

			// Token: 0x040001DD RID: 477
			internal uint cIpEntries;

			// Token: 0x040001DE RID: 478
			internal IntPtr pIpEntries;

			// Token: 0x040001DF RID: 479
			internal uint fEmptyCluster;
		}
	}
}
