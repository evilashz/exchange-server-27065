using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000368 RID: 872
	[FriendAccessAllowed]
	[SecurityCritical]
	internal class WindowsRuntimeResourceManagerBase
	{
		// Token: 0x06002BF2 RID: 11250 RVA: 0x000A586D File Offset: 0x000A3A6D
		[SecurityCritical]
		public virtual bool Initialize(string libpath, string reswFilename, out PRIExceptionInfo exceptionInfo)
		{
			exceptionInfo = null;
			return false;
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000A5873 File Offset: 0x000A3A73
		[SecurityCritical]
		public virtual string GetString(string stringName, string startingCulture, string neutralResourcesCulture)
		{
			return null;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000A5876 File Offset: 0x000A3A76
		public virtual CultureInfo GlobalResourceContextBestFitCultureInfo
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000A5879 File Offset: 0x000A3A79
		[SecurityCritical]
		public virtual bool SetGlobalResourceContextDefaultCulture(CultureInfo ci)
		{
			return false;
		}
	}
}
