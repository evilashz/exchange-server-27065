using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020007A5 RID: 1957
	[Cmdlet("Disable", "MailboxQuarantine", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableMailboxQuarantine : MailboxQuarantineTaskBase
	{
		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x0011B7B0 File Offset: 0x001199B0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableMailboxQuarantine(base.Identity.ToString());
			}
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x0011B7C4 File Offset: 0x001199C4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!base.GetMailboxQuarantineStatus())
			{
				this.WriteWarning(Strings.DisableMailboxQuarantineWarningMessage(base.Identity.ToString()));
				return;
			}
			try
			{
				if (base.RegistryKeyHive != null)
				{
					string subkey = string.Format("{0}\\{1}\\Private-{2}\\QuarantinedMailboxes\\{3}", new object[]
					{
						MailboxQuarantineTaskBase.QuarantineBaseRegistryKey,
						base.Server,
						base.Database.Guid,
						base.ExchangeGuid
					});
					base.RegistryKeyHive.DeleteSubKey(subkey, false);
					if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.SuccessDisableMailboxQuarantine(base.Identity.ToString()));
					}
				}
			}
			catch (IOException ex)
			{
				base.WriteError(new FailedMailboxQuarantineException(base.Identity.ToString(), ex.ToString()), ErrorCategory.ObjectNotFound, null);
			}
			catch (SecurityException ex2)
			{
				base.WriteError(new FailedMailboxQuarantineException(base.Identity.ToString(), ex2.ToString()), ErrorCategory.SecurityError, null);
			}
			catch (UnauthorizedAccessException ex3)
			{
				base.WriteError(new FailedMailboxQuarantineException(base.Identity.ToString(), ex3.ToString()), ErrorCategory.PermissionDenied, null);
			}
		}

		// Token: 0x04002A9E RID: 10910
		private const string QuarantinedMailboxRegistryKey = "{0}\\{1}\\Private-{2}\\QuarantinedMailboxes\\{3}";
	}
}
