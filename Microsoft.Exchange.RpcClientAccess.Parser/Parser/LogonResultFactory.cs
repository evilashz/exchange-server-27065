using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000F9 RID: 249
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LogonResultFactory : StandardResultFactory
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		internal LogonResultFactory(byte logonId) : base(RopId.Logon)
		{
			this.logonId = logonId;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000F6BD File Offset: 0x0000D8BD
		public byte LogonId
		{
			get
			{
				return this.logonId;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000F6C5 File Offset: 0x0000D8C5
		public RopResult CreateRedirectResult(string serverLegacyDn, LogonFlags logonFlags)
		{
			return new RedirectLogonResult(serverLegacyDn, logonFlags);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public RopResult CreateSuccessfulPrivateResult(IServerObject logonObject, LogonFlags logonFlags, StoreId[] folderIds, LogonExtendedResponseFlags extendedFlags, LocaleInfo? localeInfo, LogonResponseFlags logonResponseFlags, Guid mailboxInstanceGuid, ReplId databaseReplId, Guid databaseGuid, ExDateTime serverTime, ulong routingConfigurationTimestamp, StoreState storeState)
		{
			return new SuccessfulPrivateLogonResult(logonObject, logonFlags, folderIds, extendedFlags, localeInfo, logonResponseFlags, mailboxInstanceGuid, databaseReplId, databaseGuid, serverTime, routingConfigurationTimestamp, storeState);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F6F7 File Offset: 0x0000D8F7
		public RopResult CreateSuccessfulPublicResult(IServerObject logonObject, LogonFlags logonFlags, StoreId[] folderIds, LogonExtendedResponseFlags extendedFlags, LocaleInfo? localeInfo, ReplId databaseReplId, Guid databaseGuid, Guid perUserReadGuid)
		{
			return new SuccessfulPublicLogonResult(logonObject, logonFlags, folderIds, extendedFlags, localeInfo, databaseReplId, databaseGuid, perUserReadGuid);
		}

		// Token: 0x040002FA RID: 762
		private readonly byte logonId;
	}
}
