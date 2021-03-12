using System;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000088 RID: 136
	internal class InterServerMailboxAccessor
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00011079 File Offset: 0x0000F279
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00011080 File Offset: 0x0000F280
		public static bool TestXSOHook { get; set; }

		// Token: 0x060004AB RID: 1195 RVA: 0x00011088 File Offset: 0x0000F288
		public static IUMPromptStorage GetUMPromptStoreAccessor(ADUser user, Guid configurationObject)
		{
			ValidateArgument.NotNull(user, "User passed is null");
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			if (exchangePrincipal.MailboxInfo.Location.ServerVersion < Server.E15MinVersion || InterServerMailboxAccessor.TestXSOHook)
			{
				return new XSOUMPromptStoreAccessor(exchangePrincipal, configurationObject);
			}
			return new EWSUMPromptStoreAccessor(exchangePrincipal, configurationObject);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public static IUMCallDataRecordStorage GetUMCallDataRecordsAcessor(ADUser user)
		{
			ValidateArgument.NotNull(user, "User passed is null");
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			if (exchangePrincipal.MailboxInfo.Location.ServerVersion < Server.E15MinVersion || InterServerMailboxAccessor.TestXSOHook)
			{
				return new XSOUMCallDataRecordAccessor(exchangePrincipal);
			}
			return new EWSUMCallDataRecordAccessor(exchangePrincipal);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00011124 File Offset: 0x0000F324
		public static IUMUserMailboxStorage GetUMUserMailboxAccessor(ADUser user, bool useLocalServerOptimization = false)
		{
			ValidateArgument.NotNull(user, "User passed is null");
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			bool flag = InterServerMailboxAccessor.TestXSOHook || exchangePrincipal.MailboxInfo.Location.ServerVersion < Server.E15MinVersion || (useLocalServerOptimization && InterServerMailboxAccessor.IsUsersMailboxOnLocalServer(user));
			if (flag)
			{
				return new XSOUMUserMailboxAccessor(exchangePrincipal, user);
			}
			return new EWSUMUserMailboxAccessor(exchangePrincipal, user);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00011184 File Offset: 0x0000F384
		private static bool IsUsersMailboxOnLocalServer(ADUser user)
		{
			bool flag = false;
			FaultInjectionUtils.FaultInjectChangeValue<bool>(3576048957U, ref flag);
			if (flag)
			{
				return false;
			}
			BackEndServer backEndServer = BackEndLocator.GetBackEndServer(user);
			Server server = LocalServer.GetServer();
			return backEndServer != null && server != null && backEndServer.Fqdn.Equals(server.Fqdn, StringComparison.OrdinalIgnoreCase);
		}
	}
}
