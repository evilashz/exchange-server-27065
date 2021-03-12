using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F6 RID: 246
	[Cmdlet("Get", "UMCallAnsweringRule", DefaultParameterSetName = "Identity")]
	public sealed class GetUMCallAnsweringRule : GetTenantADObjectWithIdentityTaskBase<UMCallAnsweringRuleIdParameter, UMCallAnsweringRule>
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00042ACA File Offset: 0x00040CCA
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x00042AE1 File Offset: 0x00040CE1
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

		// Token: 0x06001252 RID: 4690 RVA: 0x00042AF4 File Offset: 0x00040CF4
		protected override void WriteResult(IConfigurable dataObject)
		{
			UMCallAnsweringRule umcallAnsweringRule = dataObject as UMCallAnsweringRule;
			if (umcallAnsweringRule.InError)
			{
				base.WriteWarning(Strings.WarningUMCallAnsweringRuleInError(umcallAnsweringRule.Name));
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00042B28 File Offset: 0x00040D28
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId executingUserId;
			base.TryGetExecutingUserId(out executingUserId);
			return UMCallAnsweringRuleUtils.GetDataProviderForCallAnsweringRuleTasks(this.Identity, this.Mailbox, base.SessionSettings, base.TenantGlobalCatalogSession, executingUserId, "get-umcallansweringrule", new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00042B79 File Offset: 0x00040D79
		protected override bool IsKnownException(Exception exception)
		{
			return UMCallAnsweringRuleUtils.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00042B8C File Offset: 0x00040D8C
		protected override void InternalStateReset()
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00042B9F File Offset: 0x00040D9F
		protected override void Dispose(bool disposing)
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.Dispose(disposing);
		}
	}
}
