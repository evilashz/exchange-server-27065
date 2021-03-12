using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004C6 RID: 1222
	[Guid("70b51430-b6ca-11d0-b9b9-00a0c922e750")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IMSAdminBase
	{
		// Token: 0x06002A7A RID: 10874
		[PreserveSig]
		int AddKey(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x06002A7B RID: 10875
		[PreserveSig]
		int DeleteKey(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x06002A7C RID: 10876
		void DeleteChildKeys(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path);

		// Token: 0x06002A7D RID: 10877
		[PreserveSig]
		int EnumKeys(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, StringBuilder Buffer, int EnumKeyIndex);

		// Token: 0x06002A7E RID: 10878
		void CopyKey(SafeMetadataHandle source, [MarshalAs(UnmanagedType.LPWStr)] string SourcePath, SafeMetadataHandle dest, [MarshalAs(UnmanagedType.LPWStr)] string DestPath, [MarshalAs(UnmanagedType.Bool)] bool OverwriteFlag, [MarshalAs(UnmanagedType.Bool)] bool CopyFlag);

		// Token: 0x06002A7F RID: 10879
		void RenameKey(SafeMetadataHandle key, [MarshalAs(UnmanagedType.LPWStr)] string path, [MarshalAs(UnmanagedType.LPWStr)] string newName);

		// Token: 0x06002A80 RID: 10880
		[PreserveSig]
		int SetData(SafeMetadataHandle key, [MarshalAs(UnmanagedType.LPWStr)] string path, MetadataRecord data);

		// Token: 0x06002A81 RID: 10881
		[PreserveSig]
		int GetData(SafeMetadataHandle key, [MarshalAs(UnmanagedType.LPWStr)] string path, MetadataRecord data, out int RequiredDataLen);

		// Token: 0x06002A82 RID: 10882
		[PreserveSig]
		int DeleteData(SafeMetadataHandle key, [MarshalAs(UnmanagedType.LPWStr)] string path, int Identifier, MBDataType DataType);

		// Token: 0x06002A83 RID: 10883
		[PreserveSig]
		int EnumData(SafeMetadataHandle key, [MarshalAs(UnmanagedType.LPWStr)] string path, MetadataRecord data, int EnumDataIndex, out int RequiredDataLen);

		// Token: 0x06002A84 RID: 10884
		[PreserveSig]
		int GetAllData(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, MBAttributes Attributes, MBUserType UserType, MBDataType DataType, out int NumDataEntries, out int DataSetNumber, int BufferSize, SafeHGlobalHandle buffer, out int RequiredBufferSize);

		// Token: 0x06002A85 RID: 10885
		void DeleteAllData(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, MBUserType UserType, MBDataType DataType);

		// Token: 0x06002A86 RID: 10886
		[PreserveSig]
		int CopyData(SafeMetadataHandle sourcehandle, [MarshalAs(UnmanagedType.LPWStr)] string SourcePath, SafeMetadataHandle desthandle, [MarshalAs(UnmanagedType.LPWStr)] string DestPath, int Attributes, MBUserType UserType, MBDataType DataType, [MarshalAs(UnmanagedType.Bool)] bool CopyFlag);

		// Token: 0x06002A87 RID: 10887
		[PreserveSig]
		int GetDataPaths(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, MBIdentifier Identifier, MBDataType DataType, int BufferSize, [MarshalAs(UnmanagedType.LPArray)] char[] buffer, out int RequiredBufferSize);

		// Token: 0x06002A88 RID: 10888
		[PreserveSig]
		int OpenKey(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, [MarshalAs(UnmanagedType.U4)] MBKeyAccess AccessRequested, int TimeOut, out IntPtr _newHandle);

		// Token: 0x06002A89 RID: 10889
		[PreserveSig]
		int CloseKey(IntPtr _handle);

		// Token: 0x06002A8A RID: 10890
		void ChangePermissions(SafeMetadataHandle handle, int TimeOut, [MarshalAs(UnmanagedType.U4)] MBKeyAccess AccessRequested);

		// Token: 0x06002A8B RID: 10891
		int SaveData();

		// Token: 0x06002A8C RID: 10892
		[PreserveSig]
		void GetHandleInfo(SafeMetadataHandle handle, [Out] METADATA_HANDLE_INFO Info);

		// Token: 0x06002A8D RID: 10893
		[PreserveSig]
		void GetSystemChangeNumber(out int SystemChangeNumber);

		// Token: 0x06002A8E RID: 10894
		[PreserveSig]
		void GetDataSetNumber(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, out int DataSetNumber);

		// Token: 0x06002A8F RID: 10895
		[PreserveSig]
		void SetLastChangeTime(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, out System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, [MarshalAs(UnmanagedType.Bool)] bool LocalTime);

		// Token: 0x06002A90 RID: 10896
		[PreserveSig]
		int GetLastChangeTime(SafeMetadataHandle handle, [MarshalAs(UnmanagedType.LPWStr)] string Path, out System.Runtime.InteropServices.ComTypes.FILETIME LastChangeTime, [MarshalAs(UnmanagedType.Bool)] bool LocalTime);

		// Token: 0x06002A91 RID: 10897
		[PreserveSig]
		int KeyExchangePhase1();

		// Token: 0x06002A92 RID: 10898
		[PreserveSig]
		int KeyExchangePhase2();

		// Token: 0x06002A93 RID: 10899
		[PreserveSig]
		int Backup([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version, int Flags);

		// Token: 0x06002A94 RID: 10900
		[PreserveSig]
		int Restore([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version, int Flags);

		// Token: 0x06002A95 RID: 10901
		[PreserveSig]
		void EnumBackups([MarshalAs(UnmanagedType.LPWStr)] out string Location, out int Version, out System.Runtime.InteropServices.ComTypes.FILETIME BackupTime, int EnumIndex);

		// Token: 0x06002A96 RID: 10902
		[PreserveSig]
		void DeleteBackup([MarshalAs(UnmanagedType.LPWStr)] string Location, int Version);

		// Token: 0x06002A97 RID: 10903
		[Obsolete]
		[PreserveSig]
		int UnmarshalInterface(out IMSAdminBase interf);

		// Token: 0x06002A98 RID: 10904
		[Obsolete]
		[PreserveSig]
		int GetServerGuid();
	}
}
