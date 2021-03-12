using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020007A4 RID: 1956
	[Cmdlet("Enable", "MailboxQuarantine", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class EnableMailboxQuarantine : MailboxQuarantineTaskBase
	{
		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060044F5 RID: 17653 RVA: 0x0011B3E8 File Offset: 0x001195E8
		// (set) Token: 0x060044F6 RID: 17654 RVA: 0x0011B426 File Offset: 0x00119626
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? Duration
		{
			get
			{
				if (!base.Fields.Contains("Duration"))
				{
					return null;
				}
				return (EnhancedTimeSpan?)base.Fields["Duration"];
			}
			set
			{
				base.Fields["Duration"] = value;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x0011B43E File Offset: 0x0011963E
		// (set) Token: 0x060044F8 RID: 17656 RVA: 0x0011B464 File Offset: 0x00119664
		[Parameter(Mandatory = false)]
		public SwitchParameter AllowMigration
		{
			get
			{
				return (SwitchParameter)(base.Fields["AllowMigration"] ?? false);
			}
			set
			{
				base.Fields["AllowMigration"] = value;
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x0011B47C File Offset: 0x0011967C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableMailboxQuarantine(base.Identity.ToString());
			}
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x0011B490 File Offset: 0x00119690
		protected override void InternalValidate()
		{
			if (this.Duration != null)
			{
				EnhancedTimeSpan zero = EnhancedTimeSpan.Zero;
				if (this.Duration.Value <= zero)
				{
					base.WriteError(new ArgumentException(Strings.DurationShouldBeGreaterThanZero(zero.ToString()), "Duration"), ErrorCategory.InvalidData, null);
				}
				EnhancedTimeSpan t = EnhancedTimeSpan.FromDays(365.0);
				if (this.Duration.Value > t)
				{
					base.WriteError(new ArgumentException(Strings.DurationShouldBeLessThan1Year(t.ToString()), "Duration"), ErrorCategory.InvalidData, null);
				}
			}
			base.InternalValidate();
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x0011B54C File Offset: 0x0011974C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				if (base.RegistryKeyHive != null)
				{
					string text = string.Format("{0}\\{1}\\Private-{2}", MailboxQuarantineTaskBase.QuarantineBaseRegistryKey, base.Server, base.Database.Guid);
					int num;
					using (RegistryKey registryKey = base.RegistryKeyHive.CreateSubKey(text))
					{
						num = (int)registryKey.GetValue("MailboxQuarantineCrashThreshold", 3);
					}
					string subkey = string.Format("{0}\\QuarantinedMailboxes\\{1}", text, base.ExchangeGuid);
					using (RegistryKey registryKey2 = base.RegistryKeyHive.CreateSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree))
					{
						registryKey2.SetValue("CrashCount", num);
						registryKey2.SetValue("LastCrashTime", DateTime.UtcNow.ToFileTime(), RegistryValueKind.QWord);
						registryKey2.SetValue("MailboxQuarantineDescription", "Enable-MailboxQuarantine - " + base.ExecutingUserIdentityName, RegistryValueKind.String);
						if (this.Duration != null)
						{
							registryKey2.SetValue("MailboxQuarantineDurationInSeconds", (int)this.Duration.Value.TotalSeconds);
						}
						if (this.AllowMigration)
						{
							registryKey2.SetValue("AllowMigrationOfQuarantinedMailbox", 1);
						}
					}
				}
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.SuccessEnableMailboxQuarantine(base.Identity.ToString()));
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

		// Token: 0x04002A9A RID: 10906
		private const string ParameterDuration = "Duration";

		// Token: 0x04002A9B RID: 10907
		private const string ParameterAllowMigration = "AllowMigration";

		// Token: 0x04002A9C RID: 10908
		private const int DefaultCrashCount = 3;

		// Token: 0x04002A9D RID: 10909
		private const int AllowMigrationValue = 1;
	}
}
