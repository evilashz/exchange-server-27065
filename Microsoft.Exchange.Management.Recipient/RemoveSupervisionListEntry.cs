using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200010C RID: 268
	[Cmdlet("Remove", "SupervisionListEntry", DefaultParameterSetName = "RemoveOne", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSupervisionListEntry : ModifySupervisionListEntryBase
	{
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x000476B8 File Offset: 0x000458B8
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x000476CF File Offset: 0x000458CF
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		public override RecipientIdParameter Identity
		{
			get
			{
				return (RecipientIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x000476E2 File Offset: 0x000458E2
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x000476F9 File Offset: 0x000458F9
		[ValidateNotNull]
		[Parameter(ParameterSetName = "RemoveOne", Mandatory = true)]
		public string Tag
		{
			get
			{
				return (string)base.Fields["Tag"];
			}
			set
			{
				base.Fields["Tag"] = value;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0004770C File Offset: 0x0004590C
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x00047723 File Offset: 0x00045923
		[Parameter(ParameterSetName = "RemoveOne", Mandatory = true)]
		[ValidateNotNull]
		public RecipientIdParameter Entry
		{
			get
			{
				return (RecipientIdParameter)base.Fields["Entry"];
			}
			set
			{
				base.Fields["Entry"] = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00047736 File Offset: 0x00045936
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x0004775C File Offset: 0x0004595C
		[Parameter(ParameterSetName = "RemoveAll", Mandatory = true)]
		public SwitchParameter RemoveAll
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveAll"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveAll"] = value;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x00047774 File Offset: 0x00045974
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.RemoveAll)
				{
					return Strings.ConfirmationRemoveAllSupervisionListEntries;
				}
				return Strings.ConfirmationRemoveSupervisionListEntry(this.Entry.ToString(), this.Tag);
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000477A0 File Offset: 0x000459A0
		protected override void SupervisionListAction()
		{
			TaskLogger.LogEnter();
			if (this.RemoveAll)
			{
				this.DataObject[ADRecipientSchema.DLSupervisionList] = null;
				this.DataObject[ADRecipientSchema.InternalRecipientSupervisionList] = null;
				this.DataObject[ADRecipientSchema.OneOffSupervisionList] = null;
			}
			else
			{
				ADRecipient adrecipient = null;
				SmtpAddress? smtpAddress = null;
				base.ResolveEntry(this.Entry, out adrecipient, out smtpAddress);
				if (adrecipient != null)
				{
					bool isGroup = ADRecipient.IsAllowedDeliveryRestrictionGroup(adrecipient.RecipientType);
					this.RemoveADRecipientSupervisionListEntry(isGroup, adrecipient);
				}
				else if (smtpAddress != null)
				{
					this.RemoveExternalRecipientSupervisionListEntry(smtpAddress.Value);
				}
				else
				{
					base.WriteError(new ArgumentNullException("adRecipientToRemove, externalAddressToRemove"), (ErrorCategory)1000, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004785C File Offset: 0x00045A5C
		private void RemoveExternalRecipientSupervisionListEntry(SmtpAddress externalRecipientToRemove)
		{
			MultiValuedProperty<ADObjectIdWithString> supervisionListForExternalAddress = base.GetSupervisionListForExternalAddress();
			ADObjectIdWithString adobjectIdWithString = null;
			string[] array = null;
			PropertyValidationError propertyValidationError = base.FindExternalAddressEntry(externalRecipientToRemove, supervisionListForExternalAddress, out adobjectIdWithString, out array);
			if (propertyValidationError != null)
			{
				return;
			}
			if (adobjectIdWithString != null)
			{
				foreach (string text in array)
				{
					if (text.Equals(this.Tag, StringComparison.OrdinalIgnoreCase))
					{
						this.RemoveTagFromExternalAddressEntry(adobjectIdWithString, array, externalRecipientToRemove, supervisionListForExternalAddress);
						return;
					}
				}
				base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionEntryNotPresent(this.Entry.ToString(), this.Tag.ToLower())), (ErrorCategory)1003, null);
				return;
			}
			base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionEntryNotPresent(this.Entry.ToString(), this.Tag.ToLower())), (ErrorCategory)1003, null);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004791C File Offset: 0x00045B1C
		private void RemoveADRecipientSupervisionListEntry(bool isGroup, ADRecipient adRecipientToRemove)
		{
			MultiValuedProperty<ADObjectIdWithString> supervisionListForADRecipient = base.GetSupervisionListForADRecipient(isGroup);
			ADObjectIdWithString adobjectIdWithString = null;
			string[] array = null;
			PropertyValidationError propertyValidationError = base.FindADRecipientEntry(adRecipientToRemove, supervisionListForADRecipient, out adobjectIdWithString, out array);
			if (propertyValidationError != null)
			{
				return;
			}
			if (adobjectIdWithString != null)
			{
				foreach (string text in array)
				{
					if (text.Equals(this.Tag, StringComparison.OrdinalIgnoreCase))
					{
						this.RemoveTagFromADRecipientEntry(adobjectIdWithString, array, supervisionListForADRecipient);
						return;
					}
				}
				base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionEntryNotPresent(this.Entry.ToString(), this.Tag.ToLower())), (ErrorCategory)1003, null);
				return;
			}
			base.WriteError(new RecipientTaskException(Strings.ErrorSupervisionEntryNotPresent(this.Entry.ToString(), this.Tag.ToLower())), (ErrorCategory)1003, null);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000479DC File Offset: 0x00045BDC
		private void RemoveTagFromADRecipientEntry(ADObjectIdWithString entry, string[] tags, MultiValuedProperty<ADObjectIdWithString> supervisionList)
		{
			if (tags.Length == 1)
			{
				supervisionList.Remove(entry);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(entry.StringValue.Length - this.Tag.Length - 1);
			foreach (string text in tags)
			{
				if (!text.Equals(this.Tag, StringComparison.OrdinalIgnoreCase))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(SupervisionListEntryConstraint.Delimiter);
					}
					stringBuilder.Append(text);
				}
			}
			ADObjectIdWithString item = new ADObjectIdWithString(stringBuilder.ToString(), entry.ObjectIdValue);
			supervisionList.Remove(entry);
			supervisionList.Add(item);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00047A7C File Offset: 0x00045C7C
		private void RemoveTagFromExternalAddressEntry(ADObjectIdWithString entry, string[] tags, SmtpAddress externalRecipientToRemove, MultiValuedProperty<ADObjectIdWithString> supervisionList)
		{
			if (tags.Length == 1)
			{
				supervisionList.Remove(entry);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(entry.StringValue.Length - this.Tag.Length - 1);
			foreach (string text in tags)
			{
				if (!text.Equals(this.Tag, StringComparison.OrdinalIgnoreCase))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(SupervisionListEntryConstraint.Delimiter);
					}
					stringBuilder.Append(text);
				}
			}
			stringBuilder.Append(SupervisionListEntryConstraint.Delimiter);
			stringBuilder.Append(externalRecipientToRemove.ToString());
			ADObjectIdWithString item = new ADObjectIdWithString(stringBuilder.ToString(), entry.ObjectIdValue);
			supervisionList.Remove(entry);
			supervisionList.Add(item);
		}

		// Token: 0x040003C8 RID: 968
		private const string RemoveOneParameterSetName = "RemoveOne";

		// Token: 0x040003C9 RID: 969
		private const string RemoveAllParameterSetName = "RemoveAll";
	}
}
