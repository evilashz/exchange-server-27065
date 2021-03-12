using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000EA3 RID: 3747
	public class GetPushNotificationSubscriptionCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x0600DC0E RID: 56334 RVA: 0x0013806C File Offset: 0x0013626C
		private GetPushNotificationSubscriptionCommand() : base("Get-PushNotificationSubscription")
		{
		}

		// Token: 0x0600DC0F RID: 56335 RVA: 0x00138079 File Offset: 0x00136279
		public GetPushNotificationSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DC10 RID: 56336 RVA: 0x00138088 File Offset: 0x00136288
		public virtual GetPushNotificationSubscriptionCommand SetParameters(GetPushNotificationSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC11 RID: 56337 RVA: 0x00138092 File Offset: 0x00136292
		public virtual GetPushNotificationSubscriptionCommand SetParameters(GetPushNotificationSubscriptionCommand.ShowAllParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC12 RID: 56338 RVA: 0x0013809C File Offset: 0x0013629C
		public virtual GetPushNotificationSubscriptionCommand SetParameters(GetPushNotificationSubscriptionCommand.ExpirationTimeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000EA4 RID: 3748
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA81 RID: 43649
			// (set) Token: 0x0600DC13 RID: 56339 RVA: 0x001380A6 File Offset: 0x001362A6
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA82 RID: 43650
			// (set) Token: 0x0600DC14 RID: 56340 RVA: 0x001380C4 File Offset: 0x001362C4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700AA83 RID: 43651
			// (set) Token: 0x0600DC15 RID: 56341 RVA: 0x001380DC File Offset: 0x001362DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA84 RID: 43652
			// (set) Token: 0x0600DC16 RID: 56342 RVA: 0x001380F4 File Offset: 0x001362F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA85 RID: 43653
			// (set) Token: 0x0600DC17 RID: 56343 RVA: 0x0013810C File Offset: 0x0013630C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA86 RID: 43654
			// (set) Token: 0x0600DC18 RID: 56344 RVA: 0x00138124 File Offset: 0x00136324
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000EA5 RID: 3749
		public class ShowAllParameters : ParametersBase
		{
			// Token: 0x1700AA87 RID: 43655
			// (set) Token: 0x0600DC1A RID: 56346 RVA: 0x00138144 File Offset: 0x00136344
			public virtual SwitchParameter ShowAll
			{
				set
				{
					base.PowerSharpParameters["ShowAll"] = value;
				}
			}

			// Token: 0x1700AA88 RID: 43656
			// (set) Token: 0x0600DC1B RID: 56347 RVA: 0x0013815C File Offset: 0x0013635C
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA89 RID: 43657
			// (set) Token: 0x0600DC1C RID: 56348 RVA: 0x0013817A File Offset: 0x0013637A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700AA8A RID: 43658
			// (set) Token: 0x0600DC1D RID: 56349 RVA: 0x00138192 File Offset: 0x00136392
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA8B RID: 43659
			// (set) Token: 0x0600DC1E RID: 56350 RVA: 0x001381AA File Offset: 0x001363AA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA8C RID: 43660
			// (set) Token: 0x0600DC1F RID: 56351 RVA: 0x001381C2 File Offset: 0x001363C2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA8D RID: 43661
			// (set) Token: 0x0600DC20 RID: 56352 RVA: 0x001381DA File Offset: 0x001363DA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000EA6 RID: 3750
		public class ExpirationTimeParameters : ParametersBase
		{
			// Token: 0x1700AA8E RID: 43662
			// (set) Token: 0x0600DC22 RID: 56354 RVA: 0x001381FA File Offset: 0x001363FA
			public virtual uint ExpirationTimeInHours
			{
				set
				{
					base.PowerSharpParameters["ExpirationTimeInHours"] = value;
				}
			}

			// Token: 0x1700AA8F RID: 43663
			// (set) Token: 0x0600DC23 RID: 56355 RVA: 0x00138212 File Offset: 0x00136412
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700AA90 RID: 43664
			// (set) Token: 0x0600DC24 RID: 56356 RVA: 0x00138230 File Offset: 0x00136430
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700AA91 RID: 43665
			// (set) Token: 0x0600DC25 RID: 56357 RVA: 0x00138248 File Offset: 0x00136448
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA92 RID: 43666
			// (set) Token: 0x0600DC26 RID: 56358 RVA: 0x00138260 File Offset: 0x00136460
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA93 RID: 43667
			// (set) Token: 0x0600DC27 RID: 56359 RVA: 0x00138278 File Offset: 0x00136478
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA94 RID: 43668
			// (set) Token: 0x0600DC28 RID: 56360 RVA: 0x00138290 File Offset: 0x00136490
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
