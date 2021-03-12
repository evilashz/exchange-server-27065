using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200000A RID: 10
	public abstract class SetMailEnabledOrgPersonObjectTask<TIdentity, TPublicObject, TDataObject> : SetMailEnabledRecipientObjectTask<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : MailEnabledOrgPerson, new() where TDataObject : ADRecipient, new()
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003E29 File Offset: 0x00002029
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003E40 File Offset: 0x00002040
		[Parameter(Mandatory = false)]
		public string SecondaryAddress
		{
			get
			{
				return (string)base.Fields["SecondaryAddress"];
			}
			set
			{
				base.Fields["SecondaryAddress"] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003E53 File Offset: 0x00002053
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003E6A File Offset: 0x0000206A
		[Parameter(Mandatory = false)]
		public UMDialPlanIdParameter SecondaryDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["SecondaryDialPlan"];
			}
			set
			{
				base.Fields["SecondaryDialPlan"] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003E7D File Offset: 0x0000207D
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003EA3 File Offset: 0x000020A3
		[Parameter]
		public SwitchParameter RemovePicture
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemovePicture"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemovePicture"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003EBB File Offset: 0x000020BB
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003EE1 File Offset: 0x000020E1
		[Parameter]
		public SwitchParameter RemoveSpokenName
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveSpokenName"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveSpokenName"] = value;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003EFC File Offset: 0x000020FC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.SecondaryAddress != null)
			{
				if (this.SecondaryDialPlan == null)
				{
					base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorUMInvalidParameters), ExchangeErrorCategory.Client, null);
				}
			}
			else if (this.SecondaryDialPlan != null)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorUMInvalidParameters), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003F60 File Offset: 0x00002160
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (this.SecondaryAddress != null && this.SecondaryDialPlan != null)
			{
				this.secondaryDialPlan = (UMDialPlan)base.GetDataObject<UMDialPlan>(this.SecondaryDialPlan, this.ConfigurationSession, null, new LocalizedString?(Strings.NonExistantDialPlan(this.SecondaryDialPlan.ToString())), new LocalizedString?(Strings.MultipleDialplansWithSameId(this.SecondaryDialPlan.ToString())), ExchangeErrorCategory.Client);
				if (!Utils.IsUriValid(this.SecondaryAddress, this.secondaryDialPlan.URIType, this.secondaryDialPlan.NumberOfDigitsInExtension))
				{
					if (this.secondaryDialPlan.URIType == UMUriType.E164)
					{
						if (!Utils.IsUriValid(this.SecondaryAddress, UMUriType.TelExtn, this.secondaryDialPlan.NumberOfDigitsInExtension))
						{
							base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorUMInvalidE164OrExtensionAddressFormat(this.SecondaryAddress)), ExchangeErrorCategory.Client, null);
							return;
						}
					}
					else if (this.secondaryDialPlan.URIType == UMUriType.SipName)
					{
						if (!Utils.IsUriValid(this.SecondaryAddress, UMUriType.TelExtn, this.secondaryDialPlan.NumberOfDigitsInExtension))
						{
							base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorUMInvalidSipNameOrExtensionAddressFormat(this.SecondaryAddress)), ExchangeErrorCategory.Client, null);
							return;
						}
					}
					else if (this.secondaryDialPlan.URIType == UMUriType.TelExtn)
					{
						base.WriteError(new TaskArgumentException(Strings.ErrorUMInvalidAddressFormat(this.SecondaryAddress)), ExchangeErrorCategory.Client, null);
					}
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000040B4 File Offset: 0x000022B4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADRecipient adrecipient = (ADRecipient)base.PrepareDataObject();
			if (this.secondaryDialPlan != null)
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateUmProxyAddressLookup(this.secondaryDialPlan);
				ADRecipient adrecipient2 = iadrecipientLookup.LookupByExtensionAndEquivalentDialPlan(this.SecondaryAddress, this.secondaryDialPlan);
				if (adrecipient2 != null && adrecipient2.PrimarySmtpAddress != adrecipient.PrimarySmtpAddress)
				{
					base.WriteError(new TaskArgumentException(DirectoryStrings.ExtensionNotUnique(this.SecondaryAddress, this.secondaryDialPlan.Name)), ExchangeErrorCategory.Client, adrecipient.Identity);
				}
				try
				{
					adrecipient.AddEUMProxyAddress(this.SecondaryAddress, this.secondaryDialPlan);
				}
				catch (InvalidOperationException ex)
				{
					base.WriteError(new TaskException(Strings.ErrorStampSecondaryAddress(ex.Message), ex), ExchangeErrorCategory.Client, adrecipient);
				}
			}
			if (this.RemovePicture.IsPresent)
			{
				adrecipient.ThumbnailPhoto = null;
			}
			if (this.RemoveSpokenName.IsPresent)
			{
				adrecipient.UMSpokenName = null;
			}
			TaskLogger.LogExit();
			return adrecipient;
		}

		// Token: 0x0400000C RID: 12
		private UMDialPlan secondaryDialPlan;
	}
}
