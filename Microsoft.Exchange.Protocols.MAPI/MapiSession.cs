using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200007E RID: 126
	public sealed class MapiSession : DisposableBase, IConnectionInformation, INotificationSession
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0001CFEC File Offset: 0x0001B1EC
		public MapiSession()
		{
			this.notificationContext = new NotificationContext(this);
			this.internalClientType = ClientType.User;
			this.testCaseId = TestCaseId.GetInProcessTestCaseId();
			for (int i = 0; i < 13; i++)
			{
				this.objectTracker[i] = new MapiPerSessionObjectCounter((MapiObjectTrackedType)i, this);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001D093 File Offset: 0x0001B293
		internal ClientSecurityContext SessionSecurityContext
		{
			get
			{
				return this.sessionSecurityContext.SecurityContext;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0001D0A8 File Offset: 0x0001B2A8
		internal StoreDatabase LastUsedDatabase
		{
			get
			{
				return this.lastUsedDatabase;
			}
			set
			{
				this.lastUsedDatabase = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001D0B1 File Offset: 0x0001B2B1
		internal ClientSecurityContext CurrentSecurityContext
		{
			get
			{
				if (!this.usingDelegatedAuth || !this.CanAcceptROPs)
				{
					return this.sessionSecurityContext.SecurityContext;
				}
				return this.delegatedSecurityContext.SecurityContext;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001D0DA File Offset: 0x0001B2DA
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001D0E2 File Offset: 0x0001B2E2
		internal MapiExMonLogger MapiExMonLogger
		{
			get
			{
				return this.exmonLogger;
			}
			set
			{
				this.exmonLogger = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001D0EB File Offset: 0x0001B2EB
		public Guid SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		internal int MaxRecipients
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001D0FA File Offset: 0x0001B2FA
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001D102 File Offset: 0x0001B302
		internal bool ShouldBeCommitted
		{
			get
			{
				return this.shouldBeCommitted;
			}
			set
			{
				this.shouldBeCommitted = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001D10B File Offset: 0x0001B30B
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0001D113 File Offset: 0x0001B313
		public int RpcContext { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001D11C File Offset: 0x0001B31C
		public AddressInfo AddressInfoUser
		{
			get
			{
				return this.addressInfoUser;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001D124 File Offset: 0x0001B324
		public string UserDN
		{
			get
			{
				if (this.AddressInfoUser == null)
				{
					return this.userDn;
				}
				return this.AddressInfoUser.LegacyExchangeDN;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001D140 File Offset: 0x0001B340
		public Guid UserGuid
		{
			get
			{
				if (this.AddressInfoUser == null)
				{
					return Guid.Empty;
				}
				return this.AddressInfoUser.ObjectId;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001D15B File Offset: 0x0001B35B
		public SecurityIdentifier UserSid
		{
			get
			{
				return this.CurrentSecurityContext.UserSid;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001D168 File Offset: 0x0001B368
		internal SecurityContextKey KeySessionSecurityContext
		{
			get
			{
				return this.keySessionSecurityContext;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001D170 File Offset: 0x0001B370
		internal SecurityContextKey KeyDelegatedSecurityContext
		{
			get
			{
				return this.keyDelegatedSecurityContext;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001D178 File Offset: 0x0001B378
		internal SecurityContextKey KeyCurrentSecurityContext
		{
			get
			{
				if (!this.usingDelegatedAuth || !this.CanAcceptROPs)
				{
					return this.KeySessionSecurityContext;
				}
				return this.keyDelegatedSecurityContext;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0001D197 File Offset: 0x0001B397
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0001D19F File Offset: 0x0001B39F
		internal Version ClientVersion
		{
			get
			{
				return this.clientVersion;
			}
			set
			{
				this.clientVersion = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		public string ClientMachineName
		{
			get
			{
				return this.clientMachineName;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		public ClientMode ClientMode
		{
			get
			{
				return this.clientMode;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001D1B8 File Offset: 0x0001B3B8
		public string ClientProcessName
		{
			get
			{
				return this.clientProcessName;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
		public DateTime LastAccessTime
		{
			get
			{
				return this.lastAccessTime;
			}
			set
			{
				this.lastAccessTime = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001D1D1 File Offset: 0x0001B3D1
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001D1D9 File Offset: 0x0001B3D9
		public Guid LastClientActivityId
		{
			get
			{
				return this.lastClientActivityId;
			}
			set
			{
				this.lastClientActivityId = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001D1E2 File Offset: 0x0001B3E2
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001D1EA File Offset: 0x0001B3EA
		public string LastClientProtocol
		{
			get
			{
				return this.lastClientProtocol;
			}
			set
			{
				this.lastClientProtocol = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001D1F3 File Offset: 0x0001B3F3
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0001D1FB File Offset: 0x0001B3FB
		public string LastClientComponent
		{
			get
			{
				return this.lastClientComponent;
			}
			set
			{
				this.lastClientComponent = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001D204 File Offset: 0x0001B404
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0001D20C File Offset: 0x0001B40C
		public string LastClientAction
		{
			get
			{
				return this.lastClientAction;
			}
			set
			{
				this.lastClientAction = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001D215 File Offset: 0x0001B415
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0001D21D File Offset: 0x0001B41D
		public DateTime ConnectTime
		{
			get
			{
				return this.connectTime;
			}
			set
			{
				this.connectTime = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001D226 File Offset: 0x0001B426
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0001D22E File Offset: 0x0001B42E
		public CodePage CodePage
		{
			get
			{
				return this.codePage;
			}
			set
			{
				this.codePage = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001D238 File Offset: 0x0001B438
		public Encoding Encoding
		{
			get
			{
				CodePage codePage = this.codePage;
				Encoding result;
				if (codePage != CodePage.ReducedUnicode)
				{
					if (codePage == CodePage.None)
					{
						result = CodePageMap.GetEncoding(1252);
					}
					else
					{
						result = CodePageMap.GetEncoding((int)this.codePage);
					}
				}
				else
				{
					result = String8Encodings.ReducedUnicode;
				}
				return result;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001D27E File Offset: 0x0001B47E
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0001D286 File Offset: 0x0001B486
		public byte LastUsedLogonIndex
		{
			get
			{
				return this.lastUsedLogonIndex;
			}
			set
			{
				this.lastUsedLogonIndex = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001D28F File Offset: 0x0001B48F
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001D297 File Offset: 0x0001B497
		internal int LcidSort
		{
			get
			{
				return this.lcidSort;
			}
			set
			{
				this.lcidSort = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		internal int LcidString
		{
			get
			{
				return this.lcidString;
			}
			set
			{
				this.lcidString = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001D2B1 File Offset: 0x0001B4B1
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0001D2B9 File Offset: 0x0001B4B9
		public bool CanConvertCodePage
		{
			get
			{
				return this.canConvertCodePage;
			}
			set
			{
				this.canConvertCodePage = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001D2C2 File Offset: 0x0001B4C2
		public bool UsingDelegatedAuth
		{
			get
			{
				return this.usingDelegatedAuth;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001D2CA File Offset: 0x0001B4CA
		public bool UsingTransportPrivilege
		{
			get
			{
				return this.usingTransportPrivilege;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
		public bool UsingAdminPrivilege
		{
			get
			{
				return this.usingAdminPrivilege;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001D2DA File Offset: 0x0001B4DA
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0001D2E2 File Offset: 0x0001B4E2
		public bool UsingLogonAdminPrivilege
		{
			get
			{
				return this.usingLogonAdminPrivilege;
			}
			set
			{
				this.usingLogonAdminPrivilege = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001D2EB File Offset: 0x0001B4EB
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0001D2F3 File Offset: 0x0001B4F3
		public bool CanAcceptROPs
		{
			get
			{
				return this.canAcceptRops;
			}
			set
			{
				this.canAcceptRops = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0001D2FC File Offset: 0x0001B4FC
		public bool IsDelegatedContextInitialized
		{
			get
			{
				return this.isDelegatedContextInitialized;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0001D304 File Offset: 0x0001B504
		public int LogonCount
		{
			get
			{
				this.ThrowIfNotValid(null);
				return this.logons.Count;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0001D318 File Offset: 0x0001B518
		public bool NeedToClose
		{
			get
			{
				return this.needToClose;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0001D320 File Offset: 0x0001B520
		internal Dictionary<int, MapiLogon>.ValueCollection Logons
		{
			get
			{
				return this.logons.Values;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0001D32D File Offset: 0x0001B52D
		public NotificationContext NotificationContext
		{
			get
			{
				this.ThrowIfNotValid(null);
				return this.notificationContext;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001D33C File Offset: 0x0001B53C
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0001D344 File Offset: 0x0001B544
		public bool InRpc { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0001D34D File Offset: 0x0001B54D
		public bool IsLockHeld
		{
			get
			{
				return LockManager.TestLock(this.sessionLockObject, LockManager.LockType.Session);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001D35B File Offset: 0x0001B55B
		public bool IsValid
		{
			get
			{
				return this.valid;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001D363 File Offset: 0x0001B563
		internal IRopDriver RopDriver
		{
			get
			{
				this.ThrowIfNotValid(null);
				return this.ropDriver;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001D372 File Offset: 0x0001B572
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0001D381 File Offset: 0x0001B581
		internal ClientType InternalClientType
		{
			get
			{
				this.ThrowIfNotValid(null);
				return this.internalClientType;
			}
			set
			{
				this.ThrowIfNotValid(null);
				this.internalClientType = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001D391 File Offset: 0x0001B591
		public string ApplicationId
		{
			get
			{
				return this.applicationIdString;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001D399 File Offset: 0x0001B599
		public TestCaseId TestCaseId
		{
			get
			{
				return this.testCaseId;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001D3A1 File Offset: 0x0001B5A1
		internal IConnectionHandler ConnectionHandler
		{
			get
			{
				return this.connectionHandler;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001D3A9 File Offset: 0x0001B5A9
		ushort IConnectionInformation.SessionId
		{
			get
			{
				this.ThrowIfNotValid(null);
				return 0;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001D3B3 File Offset: 0x0001B5B3
		bool IConnectionInformation.ClientSupportsBackoffResult
		{
			get
			{
				this.ThrowIfNotValid(null);
				return true;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001D3BD File Offset: 0x0001B5BD
		bool IConnectionInformation.ClientSupportsBufferTooSmallBreakup
		{
			get
			{
				this.ThrowIfNotValid(null);
				return false;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001D3C7 File Offset: 0x0001B5C7
		Encoding IConnectionInformation.String8Encoding
		{
			get
			{
				this.ThrowIfNotValid(null);
				return this.Encoding;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		internal static bool CheckCreateSessionRightsOnConnect(SecurityDescriptor serverNTSecurityDescriptor, ClientSecurityContext callerSecurityContext, bool usingDelegatedAuth, bool usingTransportPrivilege, bool usingAdminPrivilege)
		{
			Microsoft.Exchange.Diagnostics.Trace createSessionTracer = ExTraceGlobals.CreateSessionTracer;
			bool flag = false;
			bool flag2 = false;
			if (usingTransportPrivilege && !flag)
			{
				flag2 = SecurityHelper.CheckTransportPrivilege(callerSecurityContext, serverNTSecurityDescriptor);
				if (!flag2)
				{
					flag = true;
					if (createSessionTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createSessionTracer.TraceDebug(0L, "Transport privilege requested but not granted");
					}
					DiagnosticContext.TraceLocation((LID)33863U);
				}
			}
			bool flag3 = false;
			if (usingDelegatedAuth && !flag)
			{
				flag3 = SecurityHelper.CheckConstrainedDelegationPrivilege(callerSecurityContext, serverNTSecurityDescriptor);
				if (!flag3)
				{
					flag = true;
					if (createSessionTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createSessionTracer.TraceDebug(0L, "Constrained delegation privilege requested but not granted");
					}
					DiagnosticContext.TraceLocation((LID)50247U);
				}
			}
			bool flag4 = false;
			if (usingAdminPrivilege && !flag)
			{
				flag4 = SecurityHelper.CheckAdministrativeRights(callerSecurityContext, serverNTSecurityDescriptor);
				if (!flag4)
				{
					if (createSessionTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createSessionTracer.TraceDebug(0L, "Admin privilege requested but not granted on server object");
					}
					DiagnosticContext.TraceLocation((LID)36648U);
				}
			}
			int num = (usingTransportPrivilege ? 1 : 0) + (usingDelegatedAuth ? 1 : 0) + (usingAdminPrivilege ? 1 : 0);
			int num2 = (flag2 ? 1 : 0) + (flag3 ? 1 : 0) + (flag4 ? 1 : 0);
			bool flag5 = num > 0 && num == num2;
			if (!flag5)
			{
				if (createSessionTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(64);
					stringBuilder.Append("ACCESS DENIED");
					stringBuilder.Append(". UserSid:[");
					SecurityHelper.AppendToString(stringBuilder, callerSecurityContext.UserSid);
					stringBuilder.Append("].");
					createSessionTracer.TraceError(0L, stringBuilder.ToString());
				}
				DiagnosticContext.TraceLocation((LID)63559U);
			}
			return flag5;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001D550 File Offset: 0x0001B750
		internal static bool CheckCreateSessionRightsOnLogon(AddressInfo addressInfo, Func<MailboxInfo> mailboxInfoGetter, Func<MailboxInfo, DatabaseInfo> databaseInfoGetter, SecurityDescriptor serverNTSecurityDescriptor, ClientSecurityContext callerSecurityContext, bool claimAdminPrivilegeOnDatabase)
		{
			Microsoft.Exchange.Diagnostics.Trace createSessionTracer = ExTraceGlobals.CreateSessionTracer;
			bool flag = SecurityHelper.CheckMailboxOwnerRights(callerSecurityContext, addressInfo);
			if (flag)
			{
				return true;
			}
			if (createSessionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				createSessionTracer.TraceDebug(0L, "create session rights check based on AddressInfo failed; fall through for a MailboxInfo based access");
			}
			DiagnosticContext.TraceLocation((LID)58951U);
			MailboxInfo mailboxInfo = mailboxInfoGetter();
			DatabaseInfo databaseInfo = databaseInfoGetter(mailboxInfo);
			bool hasMailbox = addressInfo.HasMailbox;
			if (databaseInfo != null)
			{
				if (claimAdminPrivilegeOnDatabase)
				{
					DiagnosticContext.TraceLocation((LID)35088U);
					flag = SecurityHelper.CheckAdministrativeRights(callerSecurityContext, databaseInfo.NTSecurityDescriptor);
					if (!flag)
					{
						DiagnosticContext.TraceLocation((LID)57616U);
					}
				}
				else if (mailboxInfo != null)
				{
					DiagnosticContext.TraceLocation((LID)51472U);
					flag = SecurityHelper.CheckMailboxOwnerRights(callerSecurityContext, mailboxInfo, databaseInfo);
					if (!flag)
					{
						DiagnosticContext.TraceLocation((LID)55336U);
					}
				}
				else
				{
					DiagnosticContext.TraceLocation((LID)45328U);
					flag = false;
				}
			}
			else if (mailboxInfo != null && mailboxInfo.IsSystemAttendantRecipient)
			{
				DiagnosticContext.TraceLocation((LID)59664U);
				flag = SecurityHelper.CheckAdministrativeRights(callerSecurityContext, serverNTSecurityDescriptor);
				if (!flag)
				{
					DiagnosticContext.TraceLocation((LID)43048U);
				}
			}
			else
			{
				DiagnosticContext.TraceLocation((LID)59432U);
				flag = false;
			}
			if (flag)
			{
				return true;
			}
			if (createSessionTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append("ACCESS DENIED");
				stringBuilder.Append(". UserSid:[");
				SecurityHelper.AppendToString(stringBuilder, callerSecurityContext.UserSid);
				stringBuilder.Append("]. ConnectingAs:[");
				stringBuilder.Append((addressInfo == null) ? "Local SERVER" : addressInfo.LegacyExchangeDN);
				stringBuilder.Append("].");
				createSessionTracer.TraceError(0L, stringBuilder.ToString());
			}
			DiagnosticContext.TraceLocation((LID)56296U);
			return false;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001D708 File Offset: 0x0001B908
		internal void HydrateSessionSecurityContext()
		{
			ClientSecurityContext clientSecurityContext = null;
			this.sessionSecurityContext = SecurityContextManager.StartRPCUse(this.keySessionSecurityContext, ref clientSecurityContext);
			this.keySessionSecurityContext = null;
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(null != this.sessionSecurityContext, "this.sessionSecurityContext should not be null post hydration");
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001D748 File Offset: 0x0001B948
		internal void DehydrateSessionSecurityContext()
		{
			if (!this.IsValid)
			{
				return;
			}
			this.keySessionSecurityContext = this.sessionSecurityContext.SecurityContextKey;
			SecurityContextManager.EndRPCUse(ref this.sessionSecurityContext, false);
			this.sessionSecurityContext = null;
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(null == this.sessionSecurityContext, "this.SessionSecurityContext should be null post de-hydration");
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(null != this.keySessionSecurityContext, "this.keySessionSecurityContext should be NOT null post de-hydration");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		internal void HydrateDelegatedSecurityContext()
		{
			if (!this.isDelegatedContextInitialized)
			{
				return;
			}
			ClientSecurityContext clientSecurityContext = null;
			this.delegatedSecurityContext = SecurityContextManager.StartRPCUse(this.keyDelegatedSecurityContext, ref clientSecurityContext);
			this.keyDelegatedSecurityContext = null;
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(null != this.delegatedSecurityContext, "bad refcounting for this.delegatedSecurityContext");
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		internal void DehydrateDelegatedSecurityContext()
		{
			if (!this.IsValid)
			{
				return;
			}
			if (!this.isDelegatedContextInitialized)
			{
				return;
			}
			this.keyDelegatedSecurityContext = this.delegatedSecurityContext.SecurityContextKey;
			SecurityContextManager.EndRPCUse(ref this.delegatedSecurityContext, false);
			this.delegatedSecurityContext = null;
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(null == this.delegatedSecurityContext, "this.delegatedSecurityContext should be null post de-hydration");
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(null != this.keyDelegatedSecurityContext, "delegatedSecurityContext should be NOT null post de-hydration");
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001D860 File Offset: 0x0001BA60
		internal void ConnectMailboxes(MapiContext context)
		{
			foreach (MapiLogon mapiLogon in this.Logons)
			{
				if (mapiLogon != null && mapiLogon.IsValid && context.IsAssociatedWithMailbox(mapiLogon.MapiMailbox))
				{
					mapiLogon.Connect(context);
				}
			}
			if (this.internalReferencedMailboxes != null)
			{
				foreach (int mailboxNumber in this.internalReferencedMailboxes)
				{
					MapiMailbox mapiMailbox = this.GetMapiMailbox(mailboxNumber);
					if (!mapiMailbox.SharedState.IsValid)
					{
						using (context.CriticalBlock((LID)38300U, CriticalBlockScope.MailboxSession))
						{
							throw new StoreException((LID)36252U, ErrorCodeValue.MdbNotInitialized);
						}
					}
					mapiMailbox.Connect(context);
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001D974 File Offset: 0x0001BB74
		internal IMailboxContext GetInternallyReferencedMailboxContext(MapiContext context, int mailboxNumber)
		{
			bool flag = false;
			MapiMailbox mapiMailbox = this.GetMapiMailbox(mailboxNumber);
			if (mapiMailbox == null)
			{
				MailboxState mailboxState = MailboxStateCache.Get(context, mailboxNumber);
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(mailboxState != null, "MailboxState not found. Who is referencing non-existng mailbox?");
				mapiMailbox = MapiMailbox.OpenMailbox(context, mailboxState, MailboxInfo.MailboxType.Private, 0);
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(mapiMailbox != null, "MapiMailbox not found. Who is referencing non-existng mailbox?");
				flag = true;
			}
			if (this.internalReferencedMailboxes == null)
			{
				this.internalReferencedMailboxes = new List<int>();
			}
			if (!this.internalReferencedMailboxes.Contains(mailboxNumber))
			{
				this.ReferenceMapiMailbox(mapiMailbox);
				this.internalReferencedMailboxes.Add(mailboxNumber);
				if (!flag)
				{
					mapiMailbox.Connect(context);
				}
			}
			return mapiMailbox.StoreMailbox;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001DA08 File Offset: 0x0001BC08
		internal void GetCurrentActivityDetails(out string activityId, out string protocol, out string component, out string action)
		{
			ExecutionDiagnostics currentDiagnostics = this.GetCurrentDiagnostics();
			if (currentDiagnostics != null)
			{
				activityId = currentDiagnostics.ClientActivityId.ToString();
				protocol = currentDiagnostics.ClientProtocolName;
				component = currentDiagnostics.ClientComponentName;
				action = currentDiagnostics.ClientActionString;
				return;
			}
			string empty;
			action = (empty = string.Empty);
			string text;
			component = (text = empty);
			string text2;
			protocol = (text2 = text);
			activityId = text2;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001DA68 File Offset: 0x0001BC68
		private ExecutionDiagnostics GetCurrentDiagnostics()
		{
			ExecutionDiagnostics result = null;
			IConnectionHandler connectionHandler = this.ConnectionHandler;
			if (connectionHandler != null)
			{
				IRopHandlerWithContext ropHandlerWithContext = connectionHandler.RopHandler as IRopHandlerWithContext;
				if (ropHandlerWithContext != null)
				{
					MapiContext mapiContext = ropHandlerWithContext.MapiContext;
					if (mapiContext != null)
					{
						result = mapiContext.Diagnostics;
					}
				}
			}
			return result;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		public MapiMailbox GetMapiMailbox(int mailboxNumber)
		{
			KeyValuePair<MapiMailbox, int> keyValuePair;
			if (this.mailboxes.TryGetValue(mailboxNumber, out keyValuePair))
			{
				return keyValuePair.Key;
			}
			return null;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public void ReferenceMapiMailbox(MapiMailbox mailbox)
		{
			int mailboxNumber = mailbox.MailboxNumber;
			KeyValuePair<MapiMailbox, int> value;
			if (this.mailboxes.TryGetValue(mailboxNumber, out value))
			{
				value = new KeyValuePair<MapiMailbox, int>(value.Key, value.Value + 1);
			}
			else
			{
				value = new KeyValuePair<MapiMailbox, int>(mailbox, 1);
			}
			this.mailboxes[mailboxNumber] = value;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public void ReleaseMapiMailbox(MapiMailbox mailbox)
		{
			int mailboxNumber = mailbox.MailboxNumber;
			KeyValuePair<MapiMailbox, int> keyValuePair = this.mailboxes[mailboxNumber];
			if (keyValuePair.Value - 1 == 0)
			{
				this.mailboxes.Remove(mailboxNumber);
				keyValuePair.Key.Dispose();
				return;
			}
			this.mailboxes[mailboxNumber] = new KeyValuePair<MapiMailbox, int>(keyValuePair.Key, keyValuePair.Value - 1);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001DB94 File Offset: 0x0001BD94
		internal void SetDelegatedAuthInfo(AddressInfo addressInfo, ref ClientSecurityContext callerSecurityContext)
		{
			this.addressInfoUser = addressInfo;
			if (this.isDelegatedContextInitialized || this.keyDelegatedSecurityContext != null)
			{
				throw new ExExceptionLogonFailed((LID)61680U, "Not supported: changing the delegated auth context on a session.");
			}
			this.delegatedSecurityContext = SecurityContextManager.StartRPCUse(null, ref callerSecurityContext);
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(this.delegatedSecurityContext != null, "can't have a null delegatedSecurityContext, we just added it to SecurityContextManager");
			this.isDelegatedContextInitialized = true;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001DBFD File Offset: 0x0001BDFD
		internal void SetAddressInfo(AddressInfo addressInfo)
		{
			this.addressInfoUser = addressInfo;
			this.userDn = null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001DC0D File Offset: 0x0001BE0D
		internal void AddLogon(int logonIndex, MapiLogon logon)
		{
			this.ThrowIfNotValid(null);
			this.logons.Add(logonIndex, logon);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001DC23 File Offset: 0x0001BE23
		internal void RemoveLogon(int logonIndex)
		{
			this.ThrowIfNotValid(null);
			if (!base.IsDisposing)
			{
				this.logons.Remove(logonIndex);
			}
			if (this.LogonCount == 0)
			{
				this.DecrementSessionCount(MapiObjectTrackingScope.Service | MapiObjectTrackingScope.User);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001DC50 File Offset: 0x0001BE50
		internal void UpdateApplicationId(MapiContext context, string applicationIdString)
		{
			ClientType clientType = ClientType.User;
			if (!ClientTypeHelper.TryGetClientType(applicationIdString, out clientType) && this.ClientVersion > Version.Exchange15MinVersion)
			{
				throw new StoreException((LID)58064U, ErrorCodeValue.InvalidParameter, "Unable to extract known client type from the applicationId:" + applicationIdString);
			}
			this.internalClientType = clientType;
			this.applicationIdString = applicationIdString;
			context.UpdateClientType(clientType);
			this.exmonLogger.ServiceName = applicationIdString;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		public MapiLogon GetLogon(int logonIndex)
		{
			this.ThrowIfNotValid(null);
			MapiLogon result;
			if (logonIndex >= 0 && this.logons.TryGetValue(logonIndex, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001DCE7 File Offset: 0x0001BEE7
		public void ThrowIfNotValid(string errorMessage)
		{
			if (!this.valid)
			{
				throw new ExExceptionInvalidObject((LID)41784U, (errorMessage == null) ? "This MapiSession object is not valid." : errorMessage);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001DD0C File Offset: 0x0001BF0C
		public void RequestClose()
		{
			this.ThrowIfNotValid(null);
			this.needToClose = true;
			this.NotificationPending();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001DD24 File Offset: 0x0001BF24
		internal void ConfigureMapiSessionForTest(AddressInfo addressInfoUser, ref ClientSecurityContext callerSecurityContext, CodePage codePage, int lcidString, int lcidSort, bool canConvertCodePage, Version clientVersion, bool usingDelegatedAuth, bool usingTransportPrivilege, bool usingAdminPrivilege, Func<MapiSession, IConnectionHandler> connectionHandlerFactory, IDriverFactory driverFactory, Action<int> notificationPendingCallback)
		{
			this.addressInfoUser = addressInfoUser;
			this.ConfigureMapiSessionHelper(ref callerSecurityContext, codePage, lcidString, lcidSort, canConvertCodePage, clientVersion, null, ClientMode.Unknown, null, usingDelegatedAuth, usingTransportPrivilege, usingAdminPrivilege, connectionHandlerFactory, driverFactory, notificationPendingCallback);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001DD58 File Offset: 0x0001BF58
		internal void ConfigureMapiSession(string userDn, ref ClientSecurityContext callerSecurityContext, CodePage codePage, int lcidString, int lcidSort, bool canConvertCodePage, Version clientVersion, string clientMachineName, ClientMode clientMode, string clientProcessName, bool usingDelegatedAuth, bool usingTransportPrivilege, bool usingAdminPrivilege, Func<MapiSession, IConnectionHandler> connectionHandlerFactory, IDriverFactory driverFactory, Action<int> notificationPendingCallback)
		{
			this.userDn = userDn;
			this.ConfigureMapiSessionHelper(ref callerSecurityContext, codePage, lcidString, lcidSort, canConvertCodePage, clientVersion, clientMachineName, clientMode, clientProcessName, usingDelegatedAuth, usingTransportPrivilege, usingAdminPrivilege, connectionHandlerFactory, driverFactory, notificationPendingCallback);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001DD90 File Offset: 0x0001BF90
		private void ConfigureMapiSessionHelper(ref ClientSecurityContext callerSecurityContext, CodePage codePage, int lcidString, int lcidSort, bool canConvertCodePage, Version clientVersion, string clientMachineName, ClientMode clientMode, string clientProcessName, bool usingDelegatedAuth, bool usingTransportPrivilege, bool usingAdminPrivilege, Func<MapiSession, IConnectionHandler> connectionHandlerFactory, IDriverFactory driverFactory, Action<int> notificationPendingCallback)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.sessionSecurityContext = SecurityContextManager.StartRPCUse(null, ref callerSecurityContext);
				this.keySessionSecurityContext = null;
				this.delegatedSecurityContext = null;
				this.keyDelegatedSecurityContext = null;
				this.ClientVersion = clientVersion;
				this.clientMachineName = clientMachineName;
				this.clientMode = clientMode;
				this.clientProcessName = clientProcessName;
				this.LcidString = lcidString;
				this.LcidSort = lcidSort;
				this.ConnectTime = DateTime.UtcNow;
				this.CodePage = codePage;
				this.CanConvertCodePage = canConvertCodePage;
				this.usingDelegatedAuth = usingDelegatedAuth;
				this.usingTransportPrivilege = usingTransportPrivilege;
				this.usingAdminPrivilege = usingAdminPrivilege;
				this.connectionHandler = connectionHandlerFactory(this);
				this.ropDriver = driverFactory.CreateIRopDriver(this.connectionHandler, this);
				this.canAcceptRops = !usingDelegatedAuth;
				this.notificationPendingCallback = notificationPendingCallback;
				disposeGuard.Success();
				this.valid = true;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001DE8C File Offset: 0x0001C08C
		public void LockSession(bool inRpc)
		{
			LockManager.GetLock(this.sessionLockObject, LockManager.LockType.Session, this.GetCurrentDiagnostics());
			if (this.IsValid)
			{
				NotificationContext.Current = this.NotificationContext;
				this.InRpc = inRpc;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001DEBA File Offset: 0x0001C0BA
		public bool TryLockSession(bool inRpc)
		{
			if (!LockManager.TryGetLock(this.sessionLockObject, LockManager.LockType.Session, this.GetCurrentDiagnostics()))
			{
				return false;
			}
			if (this.IsValid)
			{
				NotificationContext.Current = this.NotificationContext;
				this.InRpc = inRpc;
			}
			return true;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001DEED File Offset: 0x0001C0ED
		public void UnlockSession()
		{
			this.InRpc = false;
			NotificationContext.Current = null;
			LockManager.ReleaseAnyLock(LockManager.LockType.Session);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001DF02 File Offset: 0x0001C102
		[Conditional("DEBUG")]
		public void AssertLockHeld()
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001DF04 File Offset: 0x0001C104
		[Conditional("DEBUG")]
		public void AssertAllMailboxesAreDisconnected()
		{
			foreach (KeyValuePair<int, KeyValuePair<MapiMailbox, int>> keyValuePair in this.mailboxes)
			{
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001DF50 File Offset: 0x0001C150
		public void PumpPendingNotifications(MapiContext context, MailboxState mailboxState)
		{
			NotificationEvent nev;
			while ((nev = this.notificationContext.DequeueEvent(context.Database.MdbGuid, mailboxState.MailboxNumber)) != null)
			{
				NotificationSubscription.PumpOneNotificationInCurrentContext(context, nev);
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001DF86 File Offset: 0x0001C186
		public IMapiObjectCounter GetPerSessionObjectCounter(MapiObjectTrackedType trackedType)
		{
			if (trackedType >= MapiObjectTrackedType.UntrackedObject)
			{
				return UnlimitedObjectCounter.Instance;
			}
			if (this.UsingTransportPrivilege)
			{
				return UnlimitedObjectCounter.Instance;
			}
			return this.objectTracker[(int)trackedType];
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001DFA9 File Offset: 0x0001C1A9
		internal void NotificationPending()
		{
			if (this.notificationPendingCallback != null)
			{
				this.notificationPendingCallback(this.RpcContext);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		internal void IncrementSessionCount(MapiObjectTrackingScope scope)
		{
			if ((scope & MapiObjectTrackingScope.Service) != (MapiObjectTrackingScope)0U)
			{
				if (this.serviceSessionCounter != null)
				{
					this.serviceSessionCounter.DecrementCount();
				}
				this.serviceSessionCounter = MapiSessionPerServiceCounter.GetObjectCounter(this.ServiceClientType());
				this.serviceSessionCounter.IncrementCount();
				this.serviceSessionCounter.CheckObjectQuota(false);
			}
			if ((scope & MapiObjectTrackingScope.User) != (MapiObjectTrackingScope)0U)
			{
				if (this.userSessionCounter != null)
				{
					this.userSessionCounter.DecrementCount();
				}
				ClientType clientType = (this.UsingAdminPrivilege || this.UsingLogonAdminPrivilege) ? ClientType.Administrator : this.internalClientType;
				this.userSessionCounter = MapiSessionPerUserCounter.GetObjectCounter(this.UserDN, this.CurrentSecurityContext.UserSid, clientType);
				this.userSessionCounter.IncrementCount();
				this.userSessionCounter.CheckObjectQuota(false);
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001E077 File Offset: 0x0001C277
		internal void DecrementSessionCount(MapiObjectTrackingScope scope)
		{
			if ((scope & MapiObjectTrackingScope.Service) != (MapiObjectTrackingScope)0U && this.serviceSessionCounter != null)
			{
				this.serviceSessionCounter.DecrementCount();
				this.serviceSessionCounter = null;
			}
			if ((scope & MapiObjectTrackingScope.User) != (MapiObjectTrackingScope)0U && this.userSessionCounter != null)
			{
				this.userSessionCounter.DecrementCount();
				this.userSessionCounter = null;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001E0B7 File Offset: 0x0001C2B7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiSession>(this);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				foreach (KeyValuePair<int, MapiLogon> keyValuePair in this.logons)
				{
					MapiLogon value = keyValuePair.Value;
					if (value != null)
					{
						MailboxState sharedState = value.MapiMailbox.SharedState;
						bool assertCondition = sharedState.TryGetMailboxLock(false, LockManager.CrashingThresholdTimeout, this.GetCurrentDiagnostics());
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition, "Since we lock with crashing timeout we should either succeed or crash.");
						try
						{
							value.Dispose();
						}
						finally
						{
							sharedState.ReleaseMailboxLock(false);
						}
					}
				}
				if (this.internalReferencedMailboxes != null)
				{
					foreach (int mailboxNumber in this.internalReferencedMailboxes)
					{
						MapiMailbox mapiMailbox = this.GetMapiMailbox(mailboxNumber);
						MailboxState sharedState2 = mapiMailbox.SharedState;
						bool assertCondition2 = sharedState2.TryGetMailboxLock(false, LockManager.CrashingThresholdTimeout, this.GetCurrentDiagnostics());
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(assertCondition2, "Since we lock with crashing timeout we should either succeed or crash.");
						try
						{
							this.ReleaseMapiMailbox(mapiMailbox);
						}
						finally
						{
							sharedState2.ReleaseMailboxLock(false);
						}
					}
				}
				this.DecrementSessionCount(MapiObjectTrackingScope.Service | MapiObjectTrackingScope.User);
				foreach (KeyValuePair<int, KeyValuePair<MapiMailbox, int>> keyValuePair2 in this.mailboxes)
				{
					keyValuePair2.Value.Key.SharedState.GetMailboxLock(false, this.GetCurrentDiagnostics());
					try
					{
						keyValuePair2.Value.Key.Dispose();
					}
					finally
					{
						keyValuePair2.Value.Key.SharedState.ReleaseMailboxLock(false);
					}
				}
				this.notificationContext.Dispose();
				if (this.ropDriver != null)
				{
					this.ropDriver.Dispose();
				}
				if (this.connectionHandler != null)
				{
					this.connectionHandler.Dispose();
				}
				if (this.sessionSecurityContext != null)
				{
					SecurityContextManager.EndRPCUse(ref this.sessionSecurityContext, true);
					this.sessionSecurityContext = null;
				}
				if (this.delegatedSecurityContext != null)
				{
					SecurityContextManager.EndRPCUse(ref this.delegatedSecurityContext, true);
					this.delegatedSecurityContext = null;
				}
				if (this.exmonLogger != null)
				{
					this.exmonLogger.Dispose();
					this.exmonLogger = null;
				}
			}
			this.valid = false;
			this.logons = null;
			this.mailboxes = null;
			this.notificationContext = null;
			this.ropDriver = null;
			this.connectionHandler = null;
			this.sessionSecurityContext = null;
			this.keySessionSecurityContext = null;
			this.delegatedSecurityContext = null;
			this.keyDelegatedSecurityContext = null;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001E368 File Offset: 0x0001C568
		internal void RememberLastException(Exception e)
		{
			this.e1 = this.e2;
			this.e2 = this.e3;
			this.e3 = e;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001E389 File Offset: 0x0001C589
		internal Exception LastRememberedException
		{
			get
			{
				return this.e3;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001E394 File Offset: 0x0001C594
		internal MapiServiceType ServiceClientType()
		{
			if (this.internalClientType == ClientType.AvailabilityService)
			{
				return MapiServiceType.Availability;
			}
			if (this.internalClientType == ClientType.EventBasedAssistants || this.internalClientType == ClientType.TimeBasedAssistants)
			{
				return MapiServiceType.Assistants;
			}
			if (ClientTypeHelper.IsContentIndexing(this.internalClientType))
			{
				return MapiServiceType.ContentIndex;
			}
			if (this.internalClientType == ClientType.Inference)
			{
				return MapiServiceType.Inference;
			}
			if (this.internalClientType == ClientType.ELC)
			{
				return MapiServiceType.ELC;
			}
			if (this.internalClientType == ClientType.SMS)
			{
				return MapiServiceType.SMS;
			}
			if (this.usingTransportPrivilege)
			{
				return MapiServiceType.Transport;
			}
			if (this.usingAdminPrivilege)
			{
				return MapiServiceType.Admin;
			}
			return MapiServiceType.UnknownServiceType;
		}

		// Token: 0x04000277 RID: 631
		private readonly Guid sessionId = Guid.NewGuid();

		// Token: 0x04000278 RID: 632
		private readonly object sessionLockObject = new object();

		// Token: 0x04000279 RID: 633
		private bool valid;

		// Token: 0x0400027A RID: 634
		private bool needToClose;

		// Token: 0x0400027B RID: 635
		private StoreDatabase lastUsedDatabase;

		// Token: 0x0400027C RID: 636
		private Dictionary<int, MapiLogon> logons = new Dictionary<int, MapiLogon>();

		// Token: 0x0400027D RID: 637
		private Dictionary<int, KeyValuePair<MapiMailbox, int>> mailboxes = new Dictionary<int, KeyValuePair<MapiMailbox, int>>();

		// Token: 0x0400027E RID: 638
		private List<int> internalReferencedMailboxes;

		// Token: 0x0400027F RID: 639
		private CountedClientSecurityContext sessionSecurityContext;

		// Token: 0x04000280 RID: 640
		private SecurityContextKey keySessionSecurityContext;

		// Token: 0x04000281 RID: 641
		private CountedClientSecurityContext delegatedSecurityContext;

		// Token: 0x04000282 RID: 642
		private SecurityContextKey keyDelegatedSecurityContext;

		// Token: 0x04000283 RID: 643
		private CodePage codePage = CodePage.None;

		// Token: 0x04000284 RID: 644
		private AddressInfo addressInfoUser;

		// Token: 0x04000285 RID: 645
		private string userDn = string.Empty;

		// Token: 0x04000286 RID: 646
		private int lcidString;

		// Token: 0x04000287 RID: 647
		private int lcidSort;

		// Token: 0x04000288 RID: 648
		private bool canConvertCodePage;

		// Token: 0x04000289 RID: 649
		private DateTime connectTime;

		// Token: 0x0400028A RID: 650
		private Version clientVersion;

		// Token: 0x0400028B RID: 651
		private string clientMachineName;

		// Token: 0x0400028C RID: 652
		private ClientMode clientMode;

		// Token: 0x0400028D RID: 653
		private string clientProcessName;

		// Token: 0x0400028E RID: 654
		private DateTime lastAccessTime;

		// Token: 0x0400028F RID: 655
		private Guid lastClientActivityId;

		// Token: 0x04000290 RID: 656
		private string lastClientProtocol;

		// Token: 0x04000291 RID: 657
		private string lastClientComponent;

		// Token: 0x04000292 RID: 658
		private string lastClientAction;

		// Token: 0x04000293 RID: 659
		private bool usingDelegatedAuth;

		// Token: 0x04000294 RID: 660
		private bool usingTransportPrivilege;

		// Token: 0x04000295 RID: 661
		private bool usingAdminPrivilege;

		// Token: 0x04000296 RID: 662
		private bool usingLogonAdminPrivilege;

		// Token: 0x04000297 RID: 663
		private bool canAcceptRops;

		// Token: 0x04000298 RID: 664
		private bool isDelegatedContextInitialized;

		// Token: 0x04000299 RID: 665
		private bool shouldBeCommitted;

		// Token: 0x0400029A RID: 666
		private NotificationContext notificationContext;

		// Token: 0x0400029B RID: 667
		private string applicationIdString;

		// Token: 0x0400029C RID: 668
		private ClientType internalClientType;

		// Token: 0x0400029D RID: 669
		private TestCaseId testCaseId;

		// Token: 0x0400029E RID: 670
		private IConnectionHandler connectionHandler;

		// Token: 0x0400029F RID: 671
		private IRopDriver ropDriver;

		// Token: 0x040002A0 RID: 672
		private byte lastUsedLogonIndex = byte.MaxValue;

		// Token: 0x040002A1 RID: 673
		private Action<int> notificationPendingCallback;

		// Token: 0x040002A2 RID: 674
		private MapiExMonLogger exmonLogger;

		// Token: 0x040002A3 RID: 675
		private IMapiObjectCounter[] objectTracker = new MapiPerSessionObjectCounter[13];

		// Token: 0x040002A4 RID: 676
		private IMapiObjectCounter serviceSessionCounter;

		// Token: 0x040002A5 RID: 677
		private IMapiObjectCounter userSessionCounter;

		// Token: 0x040002A6 RID: 678
		private Exception e1;

		// Token: 0x040002A7 RID: 679
		private Exception e2;

		// Token: 0x040002A8 RID: 680
		private Exception e3;
	}
}
