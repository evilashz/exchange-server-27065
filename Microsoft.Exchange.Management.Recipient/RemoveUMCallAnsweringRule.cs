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
	// Token: 0x020000F8 RID: 248
	[Cmdlet("Remove", "UMCallAnsweringRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUMCallAnsweringRule : RemoveTenantADTaskBase<UMCallAnsweringRuleIdParameter, UMCallAnsweringRule>
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00042EA5 File Offset: 0x000410A5
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00042EBC File Offset: 0x000410BC
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

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00042ECF File Offset: 0x000410CF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveCallAnsweringRule(this.Identity.ToString());
			}
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00042EE4 File Offset: 0x000410E4
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId executingUserId;
			base.TryGetExecutingUserId(out executingUserId);
			return UMCallAnsweringRuleUtils.GetDataProviderForCallAnsweringRuleTasks(this.Identity, this.Mailbox, base.SessionSettings, base.TenantGlobalCatalogSession, executingUserId, "remove-callansweringrule", new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00042F35 File Offset: 0x00041135
		protected override bool IsKnownException(Exception exception)
		{
			return UMCallAnsweringRuleUtils.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00042F48 File Offset: 0x00041148
		protected override void InternalStateReset()
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00042F5B File Offset: 0x0004115B
		protected override void Dispose(bool disposing)
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.Dispose(disposing);
		}
	}
}
