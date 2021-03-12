using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D39 RID: 3385
	[Cmdlet("Remove", "UMMailboxPrompt", SupportsShouldProcess = true, DefaultParameterSetName = "CustomVoicemailGreeting", ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUMMailboxPrompt : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x1700284D RID: 10317
		// (get) Token: 0x060081C4 RID: 33220 RVA: 0x00212B00 File Offset: 0x00210D00
		// (set) Token: 0x060081C5 RID: 33221 RVA: 0x00212B17 File Offset: 0x00210D17
		[Parameter(Mandatory = true, ParameterSetName = "CustomAwayGreeting", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "CustomVoicemailGreeting", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x060081C6 RID: 33222 RVA: 0x00212B2A File Offset: 0x00210D2A
		// (set) Token: 0x060081C7 RID: 33223 RVA: 0x00212B50 File Offset: 0x00210D50
		[Parameter(Mandatory = true, ParameterSetName = "CustomVoicemailGreeting")]
		public SwitchParameter CustomVoicemailGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["ClearVoicemailGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ClearVoicemailGreeting"] = value;
			}
		}

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x060081C8 RID: 33224 RVA: 0x00212B68 File Offset: 0x00210D68
		// (set) Token: 0x060081C9 RID: 33225 RVA: 0x00212B8E File Offset: 0x00210D8E
		[Parameter(Mandatory = true, ParameterSetName = "CustomAwayGreeting")]
		public SwitchParameter CustomAwayGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["ClearAwayGreeting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ClearAwayGreeting"] = value;
			}
		}

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x060081CA RID: 33226 RVA: 0x00212BA6 File Offset: 0x00210DA6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUMMailboxPrompt(this.Identity.ToString());
			}
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x00212BB8 File Offset: 0x00210DB8
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1000, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x00212C28 File Offset: 0x00210E28
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.CustomVoicemailGreeting || this.CustomAwayGreeting)
			{
				using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(this.DataObject))
				{
					if (umsubscriber != null)
					{
						try
						{
							if (this.CustomVoicemailGreeting)
							{
								umsubscriber.RemoveCustomGreeting(MailboxGreetingEnum.Voicemail);
							}
							if (this.CustomAwayGreeting)
							{
								umsubscriber.RemoveCustomGreeting(MailboxGreetingEnum.Away);
							}
							goto IL_85;
						}
						catch (UserConfigurationException exception)
						{
							base.WriteError(exception, (ErrorCategory)1001, null);
							goto IL_85;
						}
					}
					base.WriteError(new UserNotUmEnabledException(this.Identity.ToString()), (ErrorCategory)1000, null);
					IL_85:;
				}
			}
		}

		// Token: 0x02000D3A RID: 3386
		internal abstract class ParameterSet
		{
			// Token: 0x04003F2D RID: 16173
			internal const string CustomVoicemailGreeting = "CustomVoicemailGreeting";

			// Token: 0x04003F2E RID: 16174
			internal const string CustomAwayGreeting = "CustomAwayGreeting";
		}
	}
}
