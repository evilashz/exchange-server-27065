using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMPhoneSession
{
	// Token: 0x02000131 RID: 305
	internal class UMPlayOnPhoneDataProvider : IConfigDataProvider
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00025E7C File Offset: 0x0002407C
		public UMPlayOnPhoneDataProvider(ADUser adUser, TypeOfPlayOnPhoneGreetingCall callType)
		{
			if (adUser != null)
			{
				this.principal = ExchangePrincipal.FromADUser(adUser, null);
			}
			this.typeOfCall = callType;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00025E9B File Offset: 0x0002409B
		public UMPlayOnPhoneDataProvider(ADUser adUser, Guid? paaIdentity) : this(adUser, TypeOfPlayOnPhoneGreetingCall.PlayOnPhoneGreetingRecording)
		{
			ValidateArgument.NotNull(adUser, "adUser");
			ValidateArgument.NotNull(paaIdentity, "paaIdentity");
			this.paaIdentity = paaIdentity;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00025EC7 File Offset: 0x000240C7
		public string Source
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00025ED0 File Offset: 0x000240D0
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			UMPhoneSession umphoneSession = new UMPhoneSession();
			umphoneSession[SimpleProviderObjectSchema.Identity] = identity;
			UMClientCommonBase umClientCommon = this.GetUmClientCommon();
			try
			{
				UMCallInfoEx callInfo = umClientCommon.GetCallInfo(identity.ToString());
				umphoneSession.OperationResult = callInfo.EndResult;
				umphoneSession.CallState = callInfo.CallState;
				umphoneSession.EventCause = callInfo.EventCause;
			}
			catch (LocalizedException ex)
			{
				throw new DataSourceOperationException(ex.LocalizedString);
			}
			finally
			{
				umClientCommon.Dispose();
			}
			return umphoneSession;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00025F5C File Offset: 0x0002415C
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00025F63 File Offset: 0x00024163
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00025F6C File Offset: 0x0002416C
		public void Save(IConfigurable instance)
		{
			UMPhoneSession umphoneSession = instance as UMPhoneSession;
			UMClientCommonBase umClientCommon = this.GetUmClientCommon();
			try
			{
				string sessionId = this.PlayOnPhoneGreeting(umClientCommon, umphoneSession.PhoneNumber);
				umphoneSession[SimpleProviderObjectSchema.Identity] = new UMPhoneSessionId(sessionId);
				umphoneSession.OperationResult = UMOperationResult.InProgress;
				umphoneSession.CallState = UMCallState.Connecting;
				umphoneSession.EventCause = UMEventCause.None;
			}
			catch (LocalizedException ex)
			{
				throw new DataSourceOperationException(ex.LocalizedString);
			}
			finally
			{
				umClientCommon.Dispose();
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00025FF0 File Offset: 0x000241F0
		public void Delete(IConfigurable instance)
		{
			UMPhoneSession umphoneSession = instance as UMPhoneSession;
			UMClientCommonBase umClientCommon = this.GetUmClientCommon();
			try
			{
				umClientCommon.Disconnect(umphoneSession.Identity.ToString());
			}
			catch (LocalizedException ex)
			{
				throw new DataSourceOperationException(ex.LocalizedString);
			}
			finally
			{
				umClientCommon.Dispose();
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00026050 File Offset: 0x00024250
		private UMClientCommonBase GetUmClientCommon()
		{
			TypeOfPlayOnPhoneGreetingCall typeOfPlayOnPhoneGreetingCall = this.typeOfCall;
			if (typeOfPlayOnPhoneGreetingCall == TypeOfPlayOnPhoneGreetingCall.Unknown)
			{
				return new UMClientCommon();
			}
			return new UMClientCommon(this.principal);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002607C File Offset: 0x0002427C
		private string PlayOnPhoneGreeting(UMClientCommonBase client, string dialString)
		{
			switch (this.typeOfCall)
			{
			case TypeOfPlayOnPhoneGreetingCall.VoicemailGreetingRecording:
				return ((UMClientCommon)client).PlayOnPhoneGreeting(UMGreetingType.NormalCustom, dialString);
			case TypeOfPlayOnPhoneGreetingCall.AwayGreetingRecording:
				return ((UMClientCommon)client).PlayOnPhoneGreeting(UMGreetingType.OofCustom, dialString);
			case TypeOfPlayOnPhoneGreetingCall.PlayOnPhoneGreetingRecording:
				return ((UMClientCommon)client).PlayOnPhonePAAGreeting(this.paaIdentity.Value, dialString);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04000574 RID: 1396
		private Guid? paaIdentity;

		// Token: 0x04000575 RID: 1397
		private ExchangePrincipal principal;

		// Token: 0x04000576 RID: 1398
		private TypeOfPlayOnPhoneGreetingCall typeOfCall;
	}
}
