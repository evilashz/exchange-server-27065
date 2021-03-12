using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F9 RID: 249
	[Cmdlet("Set", "UMCallAnsweringRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetUMCallAnsweringRule : SetTenantADTaskBase<UMCallAnsweringRuleIdParameter, UMCallAnsweringRule, UMCallAnsweringRule>
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00042F6F File Offset: 0x0004116F
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00042F86 File Offset: 0x00041186
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

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00042F99 File Offset: 0x00041199
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetCallAnsweringRule(this.Identity.ToString());
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00042FB4 File Offset: 0x000411B4
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId executingUserId;
			base.TryGetExecutingUserId(out executingUserId);
			return UMCallAnsweringRuleUtils.GetDataProviderForCallAnsweringRuleTasks(this.Identity, this.Mailbox, base.SessionSettings, base.TenantGlobalCatalogSession, executingUserId, "set-callansweringrule", new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00043008 File Offset: 0x00041208
		protected override void InternalValidate()
		{
			base.InternalValidate();
			UMCallAnsweringRuleDataProvider umcallAnsweringRuleDataProvider = (UMCallAnsweringRuleDataProvider)base.DataSession;
			umcallAnsweringRuleDataProvider.ValidateUMCallAnsweringRuleProperties(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004303F File Offset: 0x0004123F
		protected override bool IsKnownException(Exception exception)
		{
			return UMCallAnsweringRuleUtils.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00043052 File Offset: 0x00041252
		protected override void InternalStateReset()
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00043065 File Offset: 0x00041265
		protected override void Dispose(bool disposing)
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.Dispose(disposing);
		}
	}
}
