using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000036 RID: 54
	internal class UMServerProxy : UMVersionedRpcTargetBase
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000ADE4 File Offset: 0x00008FE4
		internal UMServerProxy(Server server) : base(server)
		{
			this.Fqdn = server.Fqdn;
			CallIdTracer.TraceDebug(ExTraceGlobals.ClientAccessTracer, this.GetHashCode(), "UMServerProxy(fqdn={0})", new object[]
			{
				this.Fqdn
			});
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000AE2F File Offset: 0x0000902F
		internal UMServerProxy(string serverFqdn) : base(null)
		{
			this.Fqdn = serverFqdn;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000AE3F File Offset: 0x0000903F
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000AE47 File Offset: 0x00009047
		internal string Fqdn { get; private set; }

		// Token: 0x060002C5 RID: 709 RVA: 0x0000AE50 File Offset: 0x00009050
		internal UMCallInfoEx GetCallInfo(string callId)
		{
			GetCallInfoResponse getCallInfoResponse = (GetCallInfoResponse)this.SendReceive(new GetCallInfoRequest
			{
				CallId = callId
			});
			return getCallInfoResponse.CallInfo;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AE80 File Offset: 0x00009080
		internal void Disconnect(string callId)
		{
			this.SendReceive(new DisconnectRequest
			{
				CallId = callId
			});
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AEA4 File Offset: 0x000090A4
		internal string PlayOnPhoneMessage(string proxyAddress, Guid userObjectGuid, Guid tenantGuid, string objectId, string dialString)
		{
			PlayOnPhoneResponse playOnPhoneResponse = (PlayOnPhoneResponse)this.SendReceive(new PlayOnPhoneMessageRequest
			{
				ProxyAddress = proxyAddress,
				UserObjectGuid = userObjectGuid,
				TenantGuid = tenantGuid,
				ObjectId = objectId,
				DialString = dialString
			});
			return playOnPhoneResponse.CallId;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AEF0 File Offset: 0x000090F0
		internal string PlayOnPhoneGreeting(string proxyAddress, Guid userObjectGuid, Guid tenantGuid, UMGreetingType greetingType, string dialString)
		{
			PlayOnPhoneResponse playOnPhoneResponse = (PlayOnPhoneResponse)this.SendReceive(new PlayOnPhoneGreetingRequest
			{
				ProxyAddress = proxyAddress,
				UserObjectGuid = userObjectGuid,
				TenantGuid = tenantGuid,
				GreetingType = greetingType,
				DialString = dialString
			});
			return playOnPhoneResponse.CallId;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AF3C File Offset: 0x0000913C
		internal string PlayOnPhoneAAGreeting(UMAutoAttendant aa, Guid tenantGuid, string dialString, string userName, string fileName)
		{
			PlayOnPhoneResponse playOnPhoneResponse = (PlayOnPhoneResponse)this.SendReceive(new PlayOnPhoneAAGreetingRequest
			{
				AAIdentity = aa.Id.ObjectGuid,
				TenantGuid = tenantGuid,
				FileName = fileName,
				DialString = dialString,
				UserRecordingTheGreeting = userName
			});
			return playOnPhoneResponse.CallId;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000AF94 File Offset: 0x00009194
		internal string PlayOnPhonePAAGreeting(string proxyAddress, Guid userObjectGuid, Guid tenantGuid, Guid paaIdentity, string dialString)
		{
			PlayOnPhoneResponse playOnPhoneResponse = (PlayOnPhoneResponse)this.SendReceive(new PlayOnPhonePAAGreetingRequest
			{
				ProxyAddress = proxyAddress,
				UserObjectGuid = userObjectGuid,
				TenantGuid = tenantGuid,
				Identity = paaIdentity,
				DialString = dialString
			});
			return playOnPhoneResponse.CallId;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AFDF File Offset: 0x000091DF
		protected override UMVersionedRpcClientBase CreateRpcClient()
		{
			return new UMPlayOnPhoneRpcClient(this.Fqdn);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AFEC File Offset: 0x000091EC
		private ResponseBase SendReceive(RequestBase request)
		{
			ResponseBase responseBase = null;
			try
			{
				responseBase = (base.ExecuteRequest(request) as ResponseBase);
				if (responseBase == null)
				{
					throw new InvalidResponseException(this.Fqdn, string.Empty);
				}
				ErrorResponse errorResponse = responseBase as ErrorResponse;
				if (errorResponse != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ClientAccessTracer, this.GetHashCode(), "UMServerProxy(fqdn={0}): ErrorResponse={1}", new object[]
					{
						this.Fqdn,
						errorResponse.ErrorType
					});
					throw errorResponse.GetException();
				}
			}
			catch (RpcException ex)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.ClientAccessTracer, this.GetHashCode(), "UMServerProxy.SendReceive(fqdn={0}): Exception={1}", new object[]
				{
					this.Fqdn,
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CasToUmRpcFailure, this.Fqdn, new object[]
				{
					request.ProxyAddress,
					CommonUtil.ToEventLogString(ex)
				});
				throw new InvalidResponseException(this.Fqdn, ex.Message, ex);
			}
			return responseBase;
		}
	}
}
