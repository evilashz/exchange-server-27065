using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015B RID: 347
	internal class NewMergeRequestCommandBase : NewMrsRequestCommandBase
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x00047F38 File Offset: 0x00046138
		protected NewMergeRequestCommandBase(string commandName, ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobItemSubscriptionSettings subscriptionSettings, bool whatIf, bool useAdmin) : base(commandName, NewMergeRequestCommandBase.ExceptionsToIgnore)
		{
			MigrationUtil.ThrowOnNullArgument(endpoint, "endpoint");
			MigrationUtil.ThrowOnNullArgument(subscriptionSettings, "subscriptionSettings");
			base.BadItemLimit = Unlimited<int>.UnlimitedValue;
			base.LargeItemLimit = Unlimited<int>.UnlimitedValue;
			base.WhatIf = whatIf;
			this.IsAdministrativeCredential = useAdmin;
			this.ApplyConnectionSettings(endpoint, subscriptionSettings);
			this.AuthenticationMethod = endpoint.AuthenticationMethod;
			this.SourceMailboxLegacyDN = subscriptionSettings.MailboxDN;
			if (!whatIf)
			{
				base.AddParameter("SkipMerging", "InitialConnectionValidation");
			}
		}

		// Token: 0x17000516 RID: 1302
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x00047FC1 File Offset: 0x000461C1
		public string SourceMailboxLegacyDN
		{
			set
			{
				base.AddParameter("RemoteSourceMailboxLegacyDN", value);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00047FCF File Offset: 0x000461CF
		public AuthenticationMethod AuthenticationMethod
		{
			set
			{
				base.AddParameter("AuthenticationMethod", value);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00047FE2 File Offset: 0x000461E2
		public bool IsAdministrativeCredential
		{
			set
			{
				base.AddParameter("IsAdministrativeCredential", value);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00047FF5 File Offset: 0x000461F5
		public string RequestName
		{
			set
			{
				base.AddParameter("Name", value);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x00048003 File Offset: 0x00046203
		public DateTime? StartAfter
		{
			set
			{
				base.AddParameter("StartAfter", value);
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00048018 File Offset: 0x00046218
		protected void ApplyConnectionSettings(ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobItemSubscriptionSettings subscriptionSettings)
		{
			base.AddParameter("RemoteCredential", endpoint.Credentials);
			Fqdn fqdn = string.IsNullOrEmpty(subscriptionSettings.RPCProxyServer) ? endpoint.RpcProxyServer : new Fqdn(subscriptionSettings.RPCProxyServer);
			base.AddParameter("OutlookAnywhereHostName", fqdn.ToString());
			base.AddParameter("RemoteSourceMailboxServerLegacyDN", subscriptionSettings.ExchangeServerDN);
		}

		// Token: 0x040005F5 RID: 1525
		internal const string AuthenticationMethodParameter = "AuthenticationMethod";

		// Token: 0x040005F6 RID: 1526
		internal const string IsAdministrativeCredentialParameter = "IsAdministrativeCredential";

		// Token: 0x040005F7 RID: 1527
		internal const string StartAfterParameter = "StartAfter";

		// Token: 0x040005F8 RID: 1528
		protected static readonly Type[] ExceptionsToIgnore = new Type[]
		{
			typeof(ManagementObjectAlreadyExistsException)
		};
	}
}
