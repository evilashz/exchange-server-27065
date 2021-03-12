using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200086A RID: 2154
	internal static class S2SRightsWrapper
	{
		// Token: 0x06003DC3 RID: 15811 RVA: 0x000D7D40 File Offset: 0x000D5F40
		public static bool AllowsTokenSerializationBy(ClientSecurityContext clientContext)
		{
			bool result;
			try
			{
				result = LocalServer.AllowsTokenSerializationBy(clientContext);
			}
			catch (DataSourceOperationException innerException)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug(0L, "[S2SRightsWrapper::AllowsTokenSerializationBy] DataSourceOperationException encountered while checking for token serialization right.");
				throw new ImpersonationFailedException(innerException);
			}
			catch (DataSourceTransientException innerException2)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug(0L, "[S2SRightsWrapper::AllowsTokenSerializationBy] DataSourceTransientException encountered while checking for token serialization right.");
				throw new ImpersonationFailedException(innerException2);
			}
			catch (DataValidationException innerException3)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug(0L, "[S2SRightsWrapper::AllowsTokenSerializationBy] DataValidationException encountered while checking for token serialization right.");
				throw new ImpersonationFailedException(innerException3);
			}
			return result;
		}
	}
}
