using System;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Configuration.CertificateAuthentication.LocStrings
{
	// Token: 0x02000007 RID: 7
	internal static class Strings
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003395 File Offset: 0x00001595
		public static string UserNotFound(string certSubject)
		{
			return string.Format(Strings.ResourceManager.GetString("UserNotFound"), certSubject);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000033AC File Offset: 0x000015AC
		public static string ADTransientError(string certSubject)
		{
			return string.Format(Strings.ResourceManager.GetString("ADTransientError"), certSubject);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000033C3 File Offset: 0x000015C3
		public static string UnknownInternalError(string certSubject)
		{
			return string.Format(Strings.ResourceManager.GetString("UnknownInternalError"), certSubject);
		}

		// Token: 0x0400001E RID: 30
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Configuration.CertificateAuthentication.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000008 RID: 8
		private enum ParamIDs
		{
			// Token: 0x04000020 RID: 32
			UserNotFound,
			// Token: 0x04000021 RID: 33
			ADTransientError,
			// Token: 0x04000022 RID: 34
			UnknownInternalError
		}
	}
}
