using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000106 RID: 262
	internal static class WSTrustStrings
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x000171A0 File Offset: 0x000153A0
		static WSTrustStrings()
		{
			WSTrustStrings.stringIDs.Add(1535677007U, "CannotDecryptToken");
			WSTrustStrings.stringIDs.Add(3292530159U, "ProofTokenNotFoundException");
			WSTrustStrings.stringIDs.Add(767822664U, "MalformedRequestSecurityTokenResponse");
			WSTrustStrings.stringIDs.Add(1285681424U, "SoapXmlMalformedException");
			WSTrustStrings.stringIDs.Add(4152981396U, "SoapFaultException");
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001723F File Offset: 0x0001543F
		public static LocalizedString CannotDecryptToken
		{
			get
			{
				return new LocalizedString("CannotDecryptToken", "Ex2FCEEA", false, true, WSTrustStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001725D File Offset: 0x0001545D
		public static LocalizedString ProofTokenNotFoundException
		{
			get
			{
				return new LocalizedString("ProofTokenNotFoundException", "ExB3FB21", false, true, WSTrustStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001727C File Offset: 0x0001547C
		public static LocalizedString HttpClientFailedToCommunicate(string endpoint)
		{
			return new LocalizedString("HttpClientFailedToCommunicate", "Ex44EF69", false, true, WSTrustStrings.ResourceManager, new object[]
			{
				endpoint
			});
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000172AB File Offset: 0x000154AB
		public static LocalizedString MalformedRequestSecurityTokenResponse
		{
			get
			{
				return new LocalizedString("MalformedRequestSecurityTokenResponse", "Ex7DF18B", false, true, WSTrustStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x000172C9 File Offset: 0x000154C9
		public static LocalizedString SoapXmlMalformedException
		{
			get
			{
				return new LocalizedString("SoapXmlMalformedException", "ExC3152E", false, true, WSTrustStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000172E8 File Offset: 0x000154E8
		public static LocalizedString UnknownTokenIssuerException(string federatedTokenIssuerUri, string targetTokenIssuerUri)
		{
			return new LocalizedString("UnknownTokenIssuerException", "Ex831278", false, true, WSTrustStrings.ResourceManager, new object[]
			{
				federatedTokenIssuerUri,
				targetTokenIssuerUri
			});
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001731C File Offset: 0x0001551C
		public static LocalizedString FailedToSerializeToken(Exception e)
		{
			return new LocalizedString("FailedToSerializeToken", "", false, false, WSTrustStrings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001734B File Offset: 0x0001554B
		public static LocalizedString SoapFaultException
		{
			get
			{
				return new LocalizedString("SoapFaultException", "Ex45AD59", false, true, WSTrustStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001736C File Offset: 0x0001556C
		public static LocalizedString FailedToIssueToken(Exception e)
		{
			return new LocalizedString("FailedToIssueToken", "", false, false, WSTrustStrings.ResourceManager, new object[]
			{
				e
			});
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001739B File Offset: 0x0001559B
		public static LocalizedString GetLocalizedString(WSTrustStrings.IDs key)
		{
			return new LocalizedString(WSTrustStrings.stringIDs[(uint)key], WSTrustStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000544 RID: 1348
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(5);

		// Token: 0x04000545 RID: 1349
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.WSTrustStrings", typeof(WSTrustStrings).GetTypeInfo().Assembly);

		// Token: 0x02000107 RID: 263
		public enum IDs : uint
		{
			// Token: 0x04000547 RID: 1351
			CannotDecryptToken = 1535677007U,
			// Token: 0x04000548 RID: 1352
			ProofTokenNotFoundException = 3292530159U,
			// Token: 0x04000549 RID: 1353
			MalformedRequestSecurityTokenResponse = 767822664U,
			// Token: 0x0400054A RID: 1354
			SoapXmlMalformedException = 1285681424U,
			// Token: 0x0400054B RID: 1355
			SoapFaultException = 4152981396U
		}

		// Token: 0x02000108 RID: 264
		private enum ParamIDs
		{
			// Token: 0x0400054D RID: 1357
			HttpClientFailedToCommunicate,
			// Token: 0x0400054E RID: 1358
			UnknownTokenIssuerException,
			// Token: 0x0400054F RID: 1359
			FailedToSerializeToken,
			// Token: 0x04000550 RID: 1360
			FailedToIssueToken
		}
	}
}
