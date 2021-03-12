using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F3 RID: 243
	public abstract class EnableDisableUMCallAnsweringRuleBase : ObjectActionTenantADTask<UMCallAnsweringRuleIdParameter, UMCallAnsweringRule>
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0004299C File Offset: 0x00040B9C
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x000429B3 File Offset: 0x00040BB3
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000429C6 File Offset: 0x00040BC6
		public EnableDisableUMCallAnsweringRuleBase(bool enabled)
		{
			this.enabled = enabled;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000429D8 File Offset: 0x00040BD8
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId executingUserId;
			base.TryGetExecutingUserId(out executingUserId);
			return UMCallAnsweringRuleUtils.GetDataProviderForCallAnsweringRuleTasks(this.Identity, this.Mailbox, base.SessionSettings, base.TenantGlobalCatalogSession, executingUserId, "enabledisable-callansweringrule", new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00042A2C File Offset: 0x00040C2C
		protected override IConfigurable PrepareDataObject()
		{
			UMCallAnsweringRule umcallAnsweringRule = (UMCallAnsweringRule)base.PrepareDataObject();
			umcallAnsweringRule.Enabled = this.enabled;
			return umcallAnsweringRule;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00042A52 File Offset: 0x00040C52
		protected override bool IsKnownException(Exception exception)
		{
			return UMCallAnsweringRuleUtils.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00042A65 File Offset: 0x00040C65
		protected override void InternalStateReset()
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00042A78 File Offset: 0x00040C78
		protected override void Dispose(bool disposing)
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.Dispose(disposing);
		}

		// Token: 0x04000381 RID: 897
		private readonly bool enabled;
	}
}
