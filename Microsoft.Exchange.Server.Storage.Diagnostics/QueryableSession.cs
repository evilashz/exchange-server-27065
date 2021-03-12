using System;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000048 RID: 72
	internal class QueryableSession
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		private QueryableSession(Guid sessionId, string applicationId, int maxRecipients, bool shouldBeCommitted, int rpcContext, string userDn, Guid userGuid, string userSid, string clientVersion, string clientMachineName, string clientMode, string clientProcessName, Guid lastClientActivityId, string lastClientProtocol, string lastClientComponent, string lastClientAction, string lastUsedDatabase, byte lastUsedLogonIndex, string lastRememberedExceptionType, string lastRememberedExceptionMessage, string lastRememberedExceptionStack, DateTime connectTime, DateTime lastAccessTime, int codePage, int lcidSort, int lcidString, bool canConvertCodePage, bool usingDelegatedAuth, bool usingTransportPrivilege, bool usingAdminPrivilege, bool usingLogonAdminPrivilege, bool canAcceptRops, bool inRpc, bool isValid, int logonObjectCount, long messageObjectCount, long attachmentObjectCount, long folderObjectCount, long notifyObjectCount, long streamObjectCount, long messageViewCount, long folderViewCount, long attachmentViewCount, long permissionViewCount, long fastTransferSourceObjectCount, long fastTransferDestinationObjectCount, long untrackedObjectCount)
		{
			this.sessionId = sessionId;
			this.applicationId = applicationId;
			this.maxRecipients = maxRecipients;
			this.shouldBeCommitted = shouldBeCommitted;
			this.rpcContext = rpcContext;
			this.userDn = userDn;
			this.userGuid = userGuid;
			this.userSid = userSid;
			this.clientVersion = clientVersion;
			this.clientMachineName = clientMachineName;
			this.clientMode = clientMode;
			this.clientProcessName = clientProcessName;
			this.lastClientActivityId = lastClientActivityId;
			this.lastClientProtocol = lastClientProtocol;
			this.lastClientComponent = lastClientComponent;
			this.lastClientAction = lastClientAction;
			this.lastUsedDatabase = lastUsedDatabase;
			this.lastUsedLogonIndex = lastUsedLogonIndex;
			this.lastRememberedExceptionType = lastRememberedExceptionType;
			this.lastRememberedExceptionMessage = lastRememberedExceptionMessage;
			this.lastRememberedExceptionStack = lastRememberedExceptionStack;
			this.connectTime = connectTime;
			this.lastAccessTime = lastAccessTime;
			this.codePage = codePage;
			this.lcidSort = lcidSort;
			this.lcidString = lcidString;
			this.canConvertCodePage = canConvertCodePage;
			this.usingDelegatedAuth = usingDelegatedAuth;
			this.usingTransportPrivilege = usingTransportPrivilege;
			this.usingAdminPrivilege = usingAdminPrivilege;
			this.usingLogonAdminPrivilege = usingLogonAdminPrivilege;
			this.canAcceptRops = canAcceptRops;
			this.inRpc = inRpc;
			this.isValid = isValid;
			this.logonObjectCount = logonObjectCount;
			this.messageObjectCount = messageObjectCount;
			this.attachmentObjectCount = attachmentObjectCount;
			this.folderObjectCount = folderObjectCount;
			this.notifyObjectCount = notifyObjectCount;
			this.streamObjectCount = streamObjectCount;
			this.messageViewCount = messageViewCount;
			this.folderViewCount = folderViewCount;
			this.attachmentViewCount = attachmentViewCount;
			this.permissionViewCount = permissionViewCount;
			this.fastTransferSourceObjectCount = fastTransferSourceObjectCount;
			this.fastTransferDestinationObjectCount = fastTransferDestinationObjectCount;
			this.untrackedObjectCount = untrackedObjectCount;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000E134 File Offset: 0x0000C334
		[Queryable(Index = 0)]
		public Guid SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000E13C File Offset: 0x0000C33C
		[Queryable]
		public string ApplicationId
		{
			get
			{
				return this.applicationId;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000E144 File Offset: 0x0000C344
		[Queryable]
		public int MaxRecipients
		{
			get
			{
				return this.maxRecipients;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000E14C File Offset: 0x0000C34C
		[Queryable]
		public bool ShouldBeCommitted
		{
			get
			{
				return this.shouldBeCommitted;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000E154 File Offset: 0x0000C354
		[Queryable]
		public int RpcContext
		{
			get
			{
				return this.rpcContext;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000E15C File Offset: 0x0000C35C
		[Queryable]
		public string UserDN
		{
			get
			{
				return this.userDn;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000E164 File Offset: 0x0000C364
		[Queryable]
		public Guid UserGuid
		{
			get
			{
				return this.userGuid;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000E16C File Offset: 0x0000C36C
		[Queryable]
		public string UserSid
		{
			get
			{
				return this.userSid;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000E174 File Offset: 0x0000C374
		[Queryable]
		public string ClientVersion
		{
			get
			{
				return this.clientVersion;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000E17C File Offset: 0x0000C37C
		[Queryable]
		public string ClientMachineName
		{
			get
			{
				return this.clientMachineName;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000E184 File Offset: 0x0000C384
		[Queryable]
		public string ClientMode
		{
			get
			{
				return this.clientMode;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000E18C File Offset: 0x0000C38C
		[Queryable]
		public string ClientProcessName
		{
			get
			{
				return this.clientProcessName;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000E194 File Offset: 0x0000C394
		[Queryable]
		public Guid LastClientActivityId
		{
			get
			{
				return this.lastClientActivityId;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000E19C File Offset: 0x0000C39C
		[Queryable]
		public string LastClientProtocol
		{
			get
			{
				return this.lastClientProtocol;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		[Queryable]
		public string LastClientComponent
		{
			get
			{
				return this.lastClientComponent;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		[Queryable]
		public string LastClientAction
		{
			get
			{
				return this.lastClientAction;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		[Queryable]
		public string LastUsedDatabase
		{
			get
			{
				return this.lastUsedDatabase;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		[Queryable]
		public byte LastUsedLogonIndex
		{
			get
			{
				return this.lastUsedLogonIndex;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		[Queryable]
		public string LastRememberedExceptionType
		{
			get
			{
				return this.lastRememberedExceptionType;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		[Queryable]
		public string LastRememberedExceptionMessage
		{
			get
			{
				return this.lastRememberedExceptionMessage;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		[Queryable]
		public string LastRememberedExceptionStack
		{
			get
			{
				return this.lastRememberedExceptionStack;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		[Queryable]
		public DateTime ConnectTime
		{
			get
			{
				return this.connectTime;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		[Queryable]
		public DateTime LastAccessTime
		{
			get
			{
				return this.lastAccessTime;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		[Queryable]
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		[Queryable]
		public int LcidSort
		{
			get
			{
				return this.lcidSort;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		[Queryable]
		public int LcidString
		{
			get
			{
				return this.lcidString;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000E204 File Offset: 0x0000C404
		[Queryable]
		public bool CanConvertCodePage
		{
			get
			{
				return this.canConvertCodePage;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000E20C File Offset: 0x0000C40C
		[Queryable]
		public bool UsingDelegatedAuth
		{
			get
			{
				return this.usingDelegatedAuth;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000E214 File Offset: 0x0000C414
		[Queryable]
		public bool UsingTransportPrivilege
		{
			get
			{
				return this.usingTransportPrivilege;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000E21C File Offset: 0x0000C41C
		[Queryable]
		public bool UsingAdminPrivilege
		{
			get
			{
				return this.usingAdminPrivilege;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000E224 File Offset: 0x0000C424
		[Queryable]
		public bool UsingLogonAdminPrivilege
		{
			get
			{
				return this.usingLogonAdminPrivilege;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000E22C File Offset: 0x0000C42C
		[Queryable]
		public bool CanAcceptRops
		{
			get
			{
				return this.canAcceptRops;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000E234 File Offset: 0x0000C434
		[Queryable]
		public bool InRpc
		{
			get
			{
				return this.inRpc;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000E23C File Offset: 0x0000C43C
		[Queryable]
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000E244 File Offset: 0x0000C444
		[Queryable]
		public int LogonObjectCount
		{
			get
			{
				return this.logonObjectCount;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000E24C File Offset: 0x0000C44C
		[Queryable]
		public long MessageObjectCount
		{
			get
			{
				return this.messageObjectCount;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000E254 File Offset: 0x0000C454
		[Queryable]
		public long AttachmentObjectCount
		{
			get
			{
				return this.attachmentObjectCount;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000E25C File Offset: 0x0000C45C
		[Queryable]
		public long FolderObjectCount
		{
			get
			{
				return this.folderObjectCount;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000E264 File Offset: 0x0000C464
		[Queryable]
		public long NotifyObjectCount
		{
			get
			{
				return this.notifyObjectCount;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000E26C File Offset: 0x0000C46C
		[Queryable]
		public long StreamObjectCount
		{
			get
			{
				return this.streamObjectCount;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000E274 File Offset: 0x0000C474
		[Queryable]
		public long MessageViewCount
		{
			get
			{
				return this.messageViewCount;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000E27C File Offset: 0x0000C47C
		[Queryable]
		public long FolderViewCount
		{
			get
			{
				return this.folderViewCount;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000E284 File Offset: 0x0000C484
		[Queryable]
		public long AttachmentViewCount
		{
			get
			{
				return this.attachmentViewCount;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000E28C File Offset: 0x0000C48C
		[Queryable]
		public long PermissionViewCount
		{
			get
			{
				return this.permissionViewCount;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000E294 File Offset: 0x0000C494
		[Queryable]
		public long FastTransferSourceObjectCount
		{
			get
			{
				return this.fastTransferSourceObjectCount;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000E29C File Offset: 0x0000C49C
		[Queryable]
		public long FastTransferDestinationObjectCount
		{
			get
			{
				return this.fastTransferDestinationObjectCount;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		[Queryable]
		public long UntrackedObjectCount
		{
			get
			{
				return this.untrackedObjectCount;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public static QueryableSession Create(MapiSession session)
		{
			string text;
			string text2;
			string text3;
			ErrorHelper.GetExceptionSummary(session.LastRememberedException, out text, out text2, out text3);
			return QueryableSession.Create(session.SessionId, session.ApplicationId, session.MaxRecipients, session.ShouldBeCommitted, session.RpcContext, session.UserDN, session.UserGuid, session.KeyCurrentSecurityContext.ToString(), string.Format("{0}.{1:00}.{2:0000}.{3:000}", new object[]
			{
				session.ClientVersion.ProductMajor,
				session.ClientVersion.ProductMinor,
				session.ClientVersion.BuildMajor,
				session.ClientVersion.BuildMinor
			}), session.ClientMachineName, session.ClientMode.ToString(), session.ClientProcessName, session.LastClientActivityId, session.LastClientProtocol, session.LastClientComponent, session.LastClientAction, (session.LastUsedDatabase != null) ? session.LastUsedDatabase.MdbName : string.Empty, session.LastUsedLogonIndex, text, text2, text3, session.ConnectTime, session.LastAccessTime, (int)session.CodePage, session.LcidSort, session.LcidString, session.CanConvertCodePage, session.UsingDelegatedAuth, session.UsingTransportPrivilege, session.UsingAdminPrivilege, session.UsingLogonAdminPrivilege, session.CanAcceptROPs, session.InRpc, session.IsValid, session.LogonCount, session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.Attachment).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.Folder).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.Notify).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.Stream).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.MessageView).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.FolderView).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.AttachmentView).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.PermissionView).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferDestination).GetCount(), session.GetPerSessionObjectCounter(MapiObjectTrackedType.UntrackedObject).GetCount());
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public static QueryableSession Create(Guid sessionId, string applicationId, int maxRecipients, bool shouldBeCommitted, int rpcContext, string userDn, Guid userGuid, string userSid, string clientVersion, string clientMachineName, string clientMode, string clientProcessName, Guid lastClientActivityId, string lastClientProtocol, string lastClientComponent, string lastClientAction, string lastUsedDatabase, byte lastUsedLogonIndex, string lastRememberedExceptionType, string lastRememberedExceptionMessage, string lastRememberedExceptionStack, DateTime connectTime, DateTime lastAccessTime, int codePage, int lcidSort, int lcidString, bool canConvertCodePage, bool usingDelegatedAuth, bool usingTransportPrivilege, bool usingAdminPrivilege, bool usingLogonAdminPrivilege, bool canAcceptRops, bool inRpc, bool isValid, int logonObjectCount, long messageObjectCount, long attachmentObjectCount, long folderObjectCount, long notifyObjectCount, long streamObjectCount, long messageViewCount, long folderViewCount, long attachmentViewCount, long permissionViewCount, long fastTransferSourceObjectCount, long fastTransferDestinationObjectCount, long untrackedObjectCount)
		{
			return new QueryableSession(sessionId, applicationId, maxRecipients, shouldBeCommitted, rpcContext, userDn, userGuid, userSid, clientVersion, clientMachineName, clientMode, clientProcessName, lastClientActivityId, lastClientProtocol, lastClientComponent, lastClientAction, lastUsedDatabase, lastUsedLogonIndex, lastRememberedExceptionType, lastRememberedExceptionMessage, lastRememberedExceptionStack, connectTime, lastAccessTime, codePage, lcidSort, lcidString, canConvertCodePage, usingDelegatedAuth, usingTransportPrivilege, usingAdminPrivilege, usingLogonAdminPrivilege, canAcceptRops, inRpc, isValid, logonObjectCount, messageObjectCount, attachmentObjectCount, folderObjectCount, notifyObjectCount, streamObjectCount, messageViewCount, folderViewCount, attachmentViewCount, permissionViewCount, fastTransferSourceObjectCount, fastTransferDestinationObjectCount, untrackedObjectCount);
		}

		// Token: 0x0400013C RID: 316
		private readonly Guid sessionId;

		// Token: 0x0400013D RID: 317
		private readonly string applicationId;

		// Token: 0x0400013E RID: 318
		private readonly int maxRecipients;

		// Token: 0x0400013F RID: 319
		private readonly bool shouldBeCommitted;

		// Token: 0x04000140 RID: 320
		private readonly int rpcContext;

		// Token: 0x04000141 RID: 321
		private readonly string userDn;

		// Token: 0x04000142 RID: 322
		private readonly Guid userGuid;

		// Token: 0x04000143 RID: 323
		private readonly string userSid;

		// Token: 0x04000144 RID: 324
		private readonly string clientVersion;

		// Token: 0x04000145 RID: 325
		private readonly string clientMachineName;

		// Token: 0x04000146 RID: 326
		private readonly string clientMode;

		// Token: 0x04000147 RID: 327
		private readonly string clientProcessName;

		// Token: 0x04000148 RID: 328
		private readonly Guid lastClientActivityId;

		// Token: 0x04000149 RID: 329
		private readonly string lastClientProtocol;

		// Token: 0x0400014A RID: 330
		private readonly string lastClientComponent;

		// Token: 0x0400014B RID: 331
		private readonly string lastClientAction;

		// Token: 0x0400014C RID: 332
		private readonly string lastUsedDatabase;

		// Token: 0x0400014D RID: 333
		private readonly byte lastUsedLogonIndex;

		// Token: 0x0400014E RID: 334
		private readonly string lastRememberedExceptionType;

		// Token: 0x0400014F RID: 335
		private readonly string lastRememberedExceptionMessage;

		// Token: 0x04000150 RID: 336
		private readonly string lastRememberedExceptionStack;

		// Token: 0x04000151 RID: 337
		private readonly DateTime connectTime;

		// Token: 0x04000152 RID: 338
		private readonly DateTime lastAccessTime;

		// Token: 0x04000153 RID: 339
		private readonly int codePage;

		// Token: 0x04000154 RID: 340
		private readonly int lcidSort;

		// Token: 0x04000155 RID: 341
		private readonly int lcidString;

		// Token: 0x04000156 RID: 342
		private readonly bool canConvertCodePage;

		// Token: 0x04000157 RID: 343
		private readonly bool usingDelegatedAuth;

		// Token: 0x04000158 RID: 344
		private readonly bool usingTransportPrivilege;

		// Token: 0x04000159 RID: 345
		private readonly bool usingAdminPrivilege;

		// Token: 0x0400015A RID: 346
		private readonly bool usingLogonAdminPrivilege;

		// Token: 0x0400015B RID: 347
		private readonly bool canAcceptRops;

		// Token: 0x0400015C RID: 348
		private readonly bool inRpc;

		// Token: 0x0400015D RID: 349
		private readonly bool isValid;

		// Token: 0x0400015E RID: 350
		private readonly int logonObjectCount;

		// Token: 0x0400015F RID: 351
		private readonly long messageObjectCount;

		// Token: 0x04000160 RID: 352
		private readonly long attachmentObjectCount;

		// Token: 0x04000161 RID: 353
		private readonly long folderObjectCount;

		// Token: 0x04000162 RID: 354
		private readonly long notifyObjectCount;

		// Token: 0x04000163 RID: 355
		private readonly long streamObjectCount;

		// Token: 0x04000164 RID: 356
		private readonly long messageViewCount;

		// Token: 0x04000165 RID: 357
		private readonly long folderViewCount;

		// Token: 0x04000166 RID: 358
		private readonly long attachmentViewCount;

		// Token: 0x04000167 RID: 359
		private readonly long permissionViewCount;

		// Token: 0x04000168 RID: 360
		private readonly long fastTransferSourceObjectCount;

		// Token: 0x04000169 RID: 361
		private readonly long fastTransferDestinationObjectCount;

		// Token: 0x0400016A RID: 362
		private readonly long untrackedObjectCount;
	}
}
