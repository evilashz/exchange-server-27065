using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC4 RID: 3524
	internal static class ProxyHeaderConverter
	{
		// Token: 0x060059A0 RID: 22944 RVA: 0x00118298 File Offset: 0x00116498
		internal static AuthZClientInfo ToAuthZClientInfo(AuthZClientInfo callerClientInfo, ProxyHeaderType headerType, byte[] proxyHeaderBytes)
		{
			AuthZClientInfo result;
			try
			{
				ProxyHeaderConverter.VerifyTokenSerializationRight(callerClientInfo);
				ProxyHeaderValue proxyHeaderValue = new ProxyHeaderValue(headerType, proxyHeaderBytes);
				SerializedSecurityAccessToken serializedSecurityAccessToken = ProxySecurityContextDecoder.Decode(proxyHeaderValue);
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[ProxyHeaderConverter::ToAuthZClientInfo] Creating AuthZClientInfo from token (smtpAddress [for DC env] is \"{0}\")", (!string.IsNullOrEmpty(serializedSecurityAccessToken.SmtpAddress)) ? serializedSecurityAccessToken.SmtpAddress : "<null>");
				result = AuthZClientInfo.FromSecurityAccessToken(serializedSecurityAccessToken);
			}
			catch (AuthzException innerException)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[ProxyHeaderConverter::ToAuthZClientInfo] Trying to create a ClientSecurityContext from proxy context failed with AuthZException.");
				AuthZFailureException exception = new AuthZFailureException(innerException);
				throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			catch (InvalidSerializedAccessTokenException exception2)
			{
				throw FaultExceptionUtilities.CreateFault(exception2, FaultParty.Sender);
			}
			return result;
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0011833C File Offset: 0x0011653C
		internal static AuthZClientInfo ToPartnerAuthZClientInfo(AuthZClientInfo callerClientInfo, ProxyHeaderType headerType, byte[] proxyHeaderBytes)
		{
			ProxyHeaderConverter.VerifyTokenSerializationRight(callerClientInfo);
			PartnerAccessToken token = PartnerAccessToken.FromBytes(proxyHeaderBytes);
			return PartnerAuthZClientInfo.FromPartnerAccessToken(token);
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x0011835C File Offset: 0x0011655C
		private static void VerifyTokenSerializationRight(AuthZClientInfo callerClientInfo)
		{
			if (!LocalServer.AllowsTokenSerializationBy(callerClientInfo.ClientSecurityContext))
			{
				ProxyEventLogHelper.LogCallerDeniedProxyRight(callerClientInfo.ClientSecurityContext.UserSid);
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<SecurityIdentifier>(0L, "[ProxyHeaderConverter::VerifyTokenSerializationRight] Caller '{0}' sent a proxy request but does not have token serialization rights on this CAS server.", callerClientInfo.ClientSecurityContext.UserSid);
				throw FaultExceptionUtilities.CreateFault(new TokenSerializationDeniedException(), FaultParty.Sender);
			}
		}
	}
}
