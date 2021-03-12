using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000013 RID: 19
	internal class FaultInjection
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00005329 File Offset: 0x00003529
		public static void GenerateFault(FaultInjection.LIDs faultLid)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest((uint)faultLid);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005338 File Offset: 0x00003538
		public static T TraceTest<T>(FaultInjection.LIDs faultLid)
		{
			T result = default(T);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)faultLid, ref result);
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000535C File Offset: 0x0000355C
		public static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (exceptionType.Equals("Microsoft.Exchange.Data.Directory.Recipient.NonUniqueRecipientException"))
				{
					result = new NonUniqueRecipientException("SomeSid", new ObjectValidationError(new LocalizedString("someSid"), null, "provider"));
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Data.Directory.SuitabilityDirectoryException"))
				{
					result = new SuitabilityDirectoryException("FQDN", 1, "SuitabilityDirectoryException");
				}
				else if (exceptionType.Equals("System.SystemException"))
				{
					result = new SystemException("SomeSystemException");
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Data.Directory.ADPossibleOperationException"))
				{
					result = new ADPossibleOperationException(new LocalizedString("ADPossibleOperationException"));
				}
			}
			return result;
		}

		// Token: 0x02000014 RID: 20
		internal enum LIDs : uint
		{
			// Token: 0x040000D0 RID: 208
			ADExceptionsOnBudgetLookup = 2917543229U,
			// Token: 0x040000D1 RID: 209
			AutodiscoverProxy = 2535861565U,
			// Token: 0x040000D2 RID: 210
			AutodiscoverProxyForceLoopback = 2804297021U,
			// Token: 0x040000D3 RID: 211
			AutodiscoverUseClientCertificateAuthentication = 4213583165U,
			// Token: 0x040000D4 RID: 212
			AutodiscoverBasicAuthWindowsPrincipalMappingError = 4154862909U,
			// Token: 0x040000D5 RID: 213
			AutodiscoverGetUserSettingForExternalUser = 2745576765U,
			// Token: 0x040000D6 RID: 214
			ADExceptionsOnBudgetDispose = 4081462589U,
			// Token: 0x040000D7 RID: 215
			AutoDiscoverMobileRedirectOptimizationOverrideOwaURL = 3866504509U
		}
	}
}
