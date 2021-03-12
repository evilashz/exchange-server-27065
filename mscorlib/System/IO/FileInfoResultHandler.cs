﻿using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000192 RID: 402
	internal class FileInfoResultHandler : SearchResultHandler<FileInfo>
	{
		// Token: 0x060018AB RID: 6315 RVA: 0x00050B2A File Offset: 0x0004ED2A
		[SecurityCritical]
		internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
		{
			return findData.IsFile;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00050B32 File Offset: 0x0004ED32
		[SecurityCritical]
		internal override FileInfo CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			return FileInfoResultHandler.CreateFileInfo(searchData, ref findData);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00050B3C File Offset: 0x0004ED3C
		[SecurityCritical]
		internal static FileInfo CreateFileInfo(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			string cFileName = findData.cFileName;
			string text = Path.CombineNoChecks(searchData.fullPath, cFileName);
			if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				new FileIOPermission(FileIOPermissionAccess.Read, new string[]
				{
					text
				}, false, false).Demand();
			}
			FileInfo fileInfo = new FileInfo(text, cFileName);
			fileInfo.InitializeFrom(ref findData);
			return fileInfo;
		}
	}
}
