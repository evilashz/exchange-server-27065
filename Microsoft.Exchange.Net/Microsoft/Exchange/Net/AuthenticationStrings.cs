using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000C0 RID: 192
	internal static class AuthenticationStrings
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x00012454 File Offset: 0x00010654
		static AuthenticationStrings()
		{
			AuthenticationStrings.stringIDs.Add(1520490179U, "AuthenticationException");
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000124A3 File Offset: 0x000106A3
		public static LocalizedString AuthenticationException
		{
			get
			{
				return new LocalizedString("AuthenticationException", "", false, false, AuthenticationStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000124C4 File Offset: 0x000106C4
		public static LocalizedString MaximumUriRedirectionsReachedException(int maximumUriRedirections)
		{
			return new LocalizedString("MaximumUriRedirectionsReachedException", "", false, false, AuthenticationStrings.ResourceManager, new object[]
			{
				maximumUriRedirections
			});
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000124F8 File Offset: 0x000106F8
		public static LocalizedString GetLocalizedString(AuthenticationStrings.IDs key)
		{
			return new LocalizedString(AuthenticationStrings.stringIDs[(uint)key], AuthenticationStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040003E4 RID: 996
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x040003E5 RID: 997
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.AuthenticationStrings", typeof(AuthenticationStrings).GetTypeInfo().Assembly);

		// Token: 0x020000C1 RID: 193
		public enum IDs : uint
		{
			// Token: 0x040003E7 RID: 999
			AuthenticationException = 1520490179U
		}

		// Token: 0x020000C2 RID: 194
		private enum ParamIDs
		{
			// Token: 0x040003E9 RID: 1001
			MaximumUriRedirectionsReachedException
		}
	}
}
