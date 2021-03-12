using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Mobility;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000031 RID: 49
	[Cmdlet("Remove", "RemoteAccountSyncCache", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRemoteAccountSyncCache : RemoveTenantADTaskBase<CacheIdParameter, SubscriptionsCache>
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009D29 File Offset: 0x00007F29
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.RemoveCacheMessageConfirmation(this.Identity);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009D38 File Offset: 0x00007F38
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings sessionSettings = base.SessionSettings;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.IgnoreInvalid, sessionSettings, 58, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\RemoveRemoteAccountSyncCache.cs");
			string idStringValue = this.Identity.ToString();
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Identity.MailboxId, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(idStringValue)), new LocalizedString?(Strings.ErrorUserNotUnique(idStringValue)));
			IRecipientSession recipientSession = AggregationTaskUtils.VerifyIsWithinWriteScopes(tenantOrRootOrgRecipientSession, aduser, new Task.TaskErrorLoggingDelegate(this.WriteDebugInfoAndError));
			try
			{
				this.userPrincipal = ExchangePrincipal.FromLegacyDN(recipientSession.SessionSettings, aduser.LegacyExchangeDN, RemotingOptions.AllowCrossSite);
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, this.Identity.MailboxId);
			}
			return new CacheDataProvider(SubscriptionCacheAction.Delete, this.userPrincipal);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009E10 File Offset: 0x00008010
		protected override void InternalEndProcessing()
		{
			if (base.DataObject != null)
			{
				try
				{
					ValidationError[] array = base.DataObject.Validate();
					if (array.Length > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < array.Length; i++)
						{
							stringBuilder.AppendLine(array[i].Description);
						}
						LocalizedString info = new LocalizedString(stringBuilder.ToString());
						this.WriteDebugInfoAndError(new SubscriptionCacheOperationFailedException(info), (ErrorCategory)1001, base.DataObject.Identity);
					}
				}
				finally
				{
					this.WriteDebugInfo();
				}
			}
			base.InternalEndProcessing();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009EA8 File Offset: 0x000080A8
		private void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009EB9 File Offset: 0x000080B9
		private void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}

		// Token: 0x04000090 RID: 144
		private ExchangePrincipal userPrincipal;
	}
}
