using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000193 RID: 403
	internal class DirectoryInfoResultHandler : SearchResultHandler<DirectoryInfo>
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x00050B93 File Offset: 0x0004ED93
		[SecurityCritical]
		internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
		{
			return findData.IsNormalDirectory;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00050B9B File Offset: 0x0004ED9B
		[SecurityCritical]
		internal override DirectoryInfo CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			return DirectoryInfoResultHandler.CreateDirectoryInfo(searchData, ref findData);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00050BA4 File Offset: 0x0004EDA4
		[SecurityCritical]
		internal static DirectoryInfo CreateDirectoryInfo(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			string cFileName = findData.cFileName;
			string text = Path.CombineNoChecks(searchData.fullPath, cFileName);
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(FileIOPermissionAccess.Read, new string[]
				{
					text + "\\."
				}, false, false).Demand();
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text, cFileName);
			directoryInfo.InitializeFrom(ref findData);
			return directoryInfo;
		}
	}
}
