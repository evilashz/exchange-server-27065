using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200001E RID: 30
	internal class LiveIdBasicAuthenticationMock : ILiveIdBasicAuthentication
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x000068BC File Offset: 0x00004ABC
		public LiveIdBasicAuthenticationMock(ILiveIdBasicAuthentication innerLiveIdBasicAuth)
		{
			this.innerLiveIdBasicAuth = innerLiveIdBasicAuth;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000068CB File Offset: 0x00004ACB
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000068D8 File Offset: 0x00004AD8
		public string ApplicationName
		{
			get
			{
				return this.innerLiveIdBasicAuth.ApplicationName;
			}
			set
			{
				this.innerLiveIdBasicAuth.ApplicationName = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000068E6 File Offset: 0x00004AE6
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000068F3 File Offset: 0x00004AF3
		public string UserIpAddress
		{
			get
			{
				return this.innerLiveIdBasicAuth.UserIpAddress;
			}
			set
			{
				this.innerLiveIdBasicAuth.UserIpAddress = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006901 File Offset: 0x00004B01
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000690E File Offset: 0x00004B0E
		public bool AllowLiveIDOnlyAuth
		{
			get
			{
				return this.innerLiveIdBasicAuth.AllowLiveIDOnlyAuth;
			}
			set
			{
				this.innerLiveIdBasicAuth.AllowLiveIDOnlyAuth = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000691C File Offset: 0x00004B1C
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006924 File Offset: 0x00004B24
		public LiveIdAuthResult LastAuthResult { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000692D File Offset: 0x00004B2D
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006935 File Offset: 0x00004B35
		public string LastRequestErrorMessage { get; private set; }

		// Token: 0x060001B1 RID: 433 RVA: 0x00006940 File Offset: 0x00004B40
		public SecurityStatus GetWindowsIdentity(byte[] userBytes, byte[] passBytes, out WindowsIdentity identity, out IAccountValidationContext accountValidationContext)
		{
			string text = null;
			ProtocolBaseServices.FaultInjectionTracer.TraceTest<string>(3112578365U, ref text);
			accountValidationContext = null;
			if (!string.IsNullOrEmpty(text))
			{
				userBytes = Encoding.ASCII.GetBytes(text);
				return this.innerLiveIdBasicAuth.GetWindowsIdentity(userBytes, passBytes, out identity, out accountValidationContext);
			}
			this.LastRequestErrorMessage = "UserIP in Auth: " + this.UserIpAddress;
			identity = null;
			this.LastAuthResult = LiveIdAuthResult.Success;
			return SecurityStatus.LogonDenied;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000069B0 File Offset: 0x00004BB0
		public SecurityStatus GetCommonAccessToken(byte[] userBytes, byte[] passBytes, Guid requestId, out string commonAccessToken, out IAccountValidationContext accountValidationContext)
		{
			string text = null;
			ProtocolBaseServices.FaultInjectionTracer.TraceTest<string>(3112578365U, ref text);
			accountValidationContext = null;
			if (!string.IsNullOrEmpty(text))
			{
				userBytes = Encoding.ASCII.GetBytes(text);
				SecurityStatus commonAccessToken2 = this.innerLiveIdBasicAuth.GetCommonAccessToken(userBytes, passBytes, requestId, out commonAccessToken, out accountValidationContext);
				if (commonAccessToken2 == SecurityStatus.OK)
				{
					CommonAccessToken commonAccessToken3 = CommonAccessToken.Deserialize(commonAccessToken);
					commonAccessToken3.ExtensionData.Remove("UserSid");
					commonAccessToken = commonAccessToken3.Serialize();
				}
				return commonAccessToken2;
			}
			this.LastRequestErrorMessage = "UserIP in Auth: " + this.UserIpAddress;
			commonAccessToken = null;
			this.LastAuthResult = LiveIdAuthResult.Success;
			return SecurityStatus.LogonDenied;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006A4A File Offset: 0x00004C4A
		public LiveIdAuthResult SyncADPassword(string puid, byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, bool syncHrd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000F5 RID: 245
		private ILiveIdBasicAuthentication innerLiveIdBasicAuth;
	}
}
