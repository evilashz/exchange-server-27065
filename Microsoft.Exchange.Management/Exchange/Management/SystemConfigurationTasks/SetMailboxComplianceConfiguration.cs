using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200031F RID: 799
	[Cmdlet("Set", "MailboxComplianceConfiguration", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetMailboxComplianceConfiguration : SetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x00077B1E File Offset: 0x00075D1E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAutoTagging(this.Identity.ToString());
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00077B30 File Offset: 0x00075D30
		// (set) Token: 0x06001B03 RID: 6915 RVA: 0x00077B47 File Offset: 0x00075D47
		[Parameter(Mandatory = false)]
		public bool RetentionAutoTaggingEnabled
		{
			get
			{
				return (bool)base.Fields["RetentionAutoTaggingEnabled"];
			}
			set
			{
				base.Fields["RetentionAutoTaggingEnabled"] = value;
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00077B60 File Offset: 0x00075D60
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ELCTaskHelper.VerifyIsInScopes(this.DataObject, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.DataObject.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				base.WriteError(new InvalidOperationException(Strings.NotSupportedForPre14Mailbox(ExchangeObjectVersion.Exchange2010.ToString(), this.Identity.ToString(), this.DataObject.ExchangeVersion.ToString())), ErrorCategory.InvalidOperation, this.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00077BEC File Offset: 0x00075DEC
		public override object GetDynamicParameters()
		{
			return null;
		}
	}
}
