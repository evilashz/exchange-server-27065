using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000191 RID: 401
	internal class StringResultHandler : SearchResultHandler<string>
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x00050ADD File Offset: 0x0004ECDD
		internal StringResultHandler(bool includeFiles, bool includeDirs)
		{
			this._includeFiles = includeFiles;
			this._includeDirs = includeDirs;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00050AF3 File Offset: 0x0004ECF3
		[SecurityCritical]
		internal override bool IsResultIncluded(ref Win32Native.WIN32_FIND_DATA findData)
		{
			return (this._includeFiles && findData.IsFile) || (this._includeDirs && findData.IsNormalDirectory);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00050B17 File Offset: 0x0004ED17
		[SecurityCritical]
		internal override string CreateObject(Directory.SearchData searchData, ref Win32Native.WIN32_FIND_DATA findData)
		{
			return Path.CombineNoChecks(searchData.userPath, findData.cFileName);
		}

		// Token: 0x04000899 RID: 2201
		private bool _includeFiles;

		// Token: 0x0400089A RID: 2202
		private bool _includeDirs;
	}
}
