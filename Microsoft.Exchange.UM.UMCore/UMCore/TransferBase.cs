using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200003F RID: 63
	internal abstract class TransferBase
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000BAED File Offset: 0x00009CED
		internal TransferBase(BaseUMCallSession session, CallContext context)
		{
			this.session = session;
			this.context = context;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000BB03 File Offset: 0x00009D03
		protected BaseUMCallSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000BB0B File Offset: 0x00009D0B
		protected CallContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060002A4 RID: 676
		internal abstract void Transfer();

		// Token: 0x060002A5 RID: 677
		protected abstract PlatformSipUri GetReferredBySipUri();

		// Token: 0x060002A6 RID: 678
		protected abstract PlatformSipUri GetReferTargetUri(PhoneNumber phone, PlatformSipUri refByUri);

		// Token: 0x060002A7 RID: 679 RVA: 0x0000BB14 File Offset: 0x00009D14
		protected PlatformSipUri GetSipUriFromSubscriber(UMSubscriber user)
		{
			string extension = user.Extension;
			if (string.IsNullOrEmpty(extension))
			{
				PIIMessage[] data = new PIIMessage[]
				{
					PIIMessage.Create(PIIType._User, user),
					PIIMessage.Create(PIIType._PhoneNumber, user.Extension)
				};
				CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, data, "GetReferredBySipUri: Invalid SIPResourceIdentifier, User=_User, Extension=_PhoneNumber, CallType={0}", new object[]
				{
					this.Context.CallType
				});
				throw new InvalidOperationException();
			}
			return Platform.Builder.CreateSipUri("SIP:" + extension.Trim());
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		protected PlatformSipUri GetReferTargetForPhoneNumbers(PhoneNumber phone, PlatformSipUri refByUri)
		{
			PlatformSipUri platformSipUri = Platform.Builder.CreateSipUri(SipUriScheme.Sip, phone.RenderUserPart(this.context.DialPlan), this.GetReferToHostPart(refByUri));
			platformSipUri.UserParameter = UserParameter.Phone;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferTargetForPhoneNumbers(refertarget={0}):", new object[]
			{
				platformSipUri
			});
			return platformSipUri;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		protected void FrameTransferTargetAndTransferForSIPNames(PhoneNumber phone)
		{
			PlatformSipUri referredBySipUri = this.GetReferredBySipUri();
			PlatformSipUri referTargetUri = this.GetReferTargetUri(phone, referredBySipUri);
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phone);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "FrameTransferTargetAndTransfer(input=_PhoneNumber): type={0} target={1}, referred-by={2}", new object[]
			{
				phone.UriType,
				referTargetUri,
				referredBySipUri
			});
			if (referredBySipUri != null)
			{
				List<PlatformSignalingHeader> list = new List<PlatformSignalingHeader>();
				list.Add(Platform.Builder.CreateSignalingHeader("Referred-By", "<" + referredBySipUri.ToString() + ">"));
				this.session.TransferAsync(referTargetUri, list);
				return;
			}
			this.session.TransferAsync(referTargetUri);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		private string GetReferToHostPart(PlatformSipUri referredByUri)
		{
			string text;
			if (this.Context.DialPlan.URIType != UMUriType.SipName && SipRoutingHelper.UseGlobalSBCSettingsForOutbound(this.Context.GatewayConfig))
			{
				text = this.Context.GatewayConfig.Address.ToString();
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferToHostPart: SBC case.", new object[0]);
			}
			else if (referredByUri != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferToHostPart: Using referred-by's host part", new object[0]);
				text = referredByUri.Host;
			}
			else if (this.Context.FromUriOfCall != null && this.Context.DialPlan.URIType == UMUriType.SipName)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferToHostPart: Using from's host part", new object[0]);
				text = this.Context.FromUriOfCall.Host;
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferToHostPart: Using sip peer's end point", new object[0]);
				text = this.Context.ImmediatePeer.ToString();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferToHostPart: returning hostPart={0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x040000D7 RID: 215
		private CallContext context;

		// Token: 0x040000D8 RID: 216
		private BaseUMCallSession session;
	}
}
