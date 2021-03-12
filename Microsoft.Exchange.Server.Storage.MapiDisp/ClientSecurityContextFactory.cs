using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000019 RID: 25
	internal static class ClientSecurityContextFactory
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00039F34 File Offset: 0x00038134
		public static ClientSecurityContext Create(Context operationContext, byte[] serialization)
		{
			return ClientSecurityContextFactory.Create(serialization, delegate(Exception ex)
			{
				ClientSecurityContextFactory.OnException(ex, operationContext);
			});
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00039F78 File Offset: 0x00038178
		public static ClientSecurityContext Create(Context operationContext, AuthenticationContext authenticationContext)
		{
			if (authenticationContext.RegularGroups.Length == 0)
			{
				DiagnosticContext.TraceLocation((LID)48424U);
				return null;
			}
			if (authenticationContext.PrimaryGroupIndex < 0 && !authenticationContext.UserSid.IsWellKnown(WellKnownSidType.LocalSystemSid))
			{
				DiagnosticContext.TraceLocation((LID)64808U);
				return null;
			}
			return ClientSecurityContextFactory.Create(authenticationContext, delegate(Exception ex)
			{
				ClientSecurityContextFactory.OnException(ex, operationContext);
			});
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00039FE8 File Offset: 0x000381E8
		private static void OnException(Exception exception, Context operationContext)
		{
			operationContext.OnExceptionCatch(exception);
			if (exception is BufferParseException)
			{
				ClientSecurityContextFactory.RecordError((LID)49896U, exception);
				return;
			}
			if (exception is AuthzException)
			{
				ClientSecurityContextFactory.RecordError((LID)58088U, exception);
				return;
			}
			if (exception is InvalidSidException)
			{
				ClientSecurityContextFactory.RecordError((LID)33512U, exception);
				return;
			}
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Unexpected exception");
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0003A052 File Offset: 0x00038252
		private static void RecordError(LID lid, Exception exception)
		{
			DiagnosticContext.TraceLocation(lid);
			if (ExTraceGlobals.RpcDetailTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				ExTraceGlobals.RpcDetailTracer.TraceError<Exception>((long)((ulong)lid.Value), "Caught exception {0} while creating a ClientSecurityContext from an AuthenticationContext", exception);
			}
		}
	}
}
