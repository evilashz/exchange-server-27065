using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200010B RID: 267
	[Cmdlet("Get", "SupervisionListEntry")]
	public sealed class GetSupervisionListEntry : GetRecipientObjectTask<RecipientIdParameter, ADRecipient>
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0004737B File Offset: 0x0004557B
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x00047392 File Offset: 0x00045592
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true)]
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

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000473A5 File Offset: 0x000455A5
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x000473BC File Offset: 0x000455BC
		[Parameter]
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

		// Token: 0x0600134B RID: 4939 RVA: 0x000473D0 File Offset: 0x000455D0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			List<SupervisionListEntry> list = new List<SupervisionListEntry>();
			ADRecipient dataObject = (ADRecipient)base.GetDataObject(this.Identity);
			this.GetADRecipientEntries(dataObject, false, list);
			this.GetADRecipientEntries(dataObject, true, list);
			this.GetExternalAddressEntries(dataObject, list);
			this.WriteResult<SupervisionListEntry>(list);
			TaskLogger.LogExit();
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00047434 File Offset: 0x00045634
		private void GetADRecipientEntries(ADRecipient dataObject, bool isGroup, List<SupervisionListEntry> results)
		{
			MultiValuedProperty<ADObjectIdWithString> multiValuedProperty = isGroup ? ((MultiValuedProperty<ADObjectIdWithString>)dataObject[ADRecipientSchema.DLSupervisionList]) : ((MultiValuedProperty<ADObjectIdWithString>)dataObject[ADRecipientSchema.InternalRecipientSupervisionList]);
			SupervisionRecipientType recipientType = isGroup ? SupervisionRecipientType.DistributionGroup : SupervisionRecipientType.IndividualRecipient;
			ADObjectId[] array = new ADObjectId[multiValuedProperty.Count];
			for (int i = 0; i < multiValuedProperty.Count; i++)
			{
				array[i] = multiValuedProperty[i].ObjectIdValue;
			}
			Result<ADRawEntry>[] array2 = base.TenantGlobalCatalogSession.ReadMultiple(array, new PropertyDefinition[]
			{
				ADObjectSchema.RawName
			});
			for (int j = 0; j < multiValuedProperty.Count; j++)
			{
				ADObjectIdWithString adobjectIdWithString = multiValuedProperty[j];
				SupervisionListEntryConstraint supervisionListEntryConstraint = new SupervisionListEntryConstraint(false);
				if (supervisionListEntryConstraint.Validate(adobjectIdWithString, null, null) == null)
				{
					string[] array3 = adobjectIdWithString.StringValue.Split(new char[]
					{
						SupervisionListEntryConstraint.Delimiter
					});
					string entryName = (string)array2[j].Data[ADObjectSchema.Name];
					foreach (string text in array3)
					{
						if (this.Tag == null || this.Tag.Equals(text, StringComparison.OrdinalIgnoreCase))
						{
							SupervisionListEntry item = null;
							try
							{
								item = new SupervisionListEntry(entryName, text, recipientType);
							}
							catch (ArgumentNullException exception)
							{
								base.WriteError(exception, (ErrorCategory)1000, null);
							}
							results.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000475B0 File Offset: 0x000457B0
		private void GetExternalAddressEntries(ADRecipient dataObject, List<SupervisionListEntry> results)
		{
			MultiValuedProperty<ADObjectIdWithString> multiValuedProperty = (MultiValuedProperty<ADObjectIdWithString>)dataObject[ADRecipientSchema.OneOffSupervisionList];
			SupervisionRecipientType recipientType = SupervisionRecipientType.ExternalAddress;
			foreach (ADObjectIdWithString adobjectIdWithString in multiValuedProperty)
			{
				SupervisionListEntryConstraint supervisionListEntryConstraint = new SupervisionListEntryConstraint(true);
				if (supervisionListEntryConstraint.Validate(adobjectIdWithString, null, null) == null)
				{
					string[] array = adobjectIdWithString.StringValue.Split(new char[]
					{
						SupervisionListEntryConstraint.Delimiter
					});
					SmtpAddress smtpAddress = new SmtpAddress(array[array.Length - 1]);
					for (int i = 0; i < array.Length - 1; i++)
					{
						string text = array[i];
						if (this.Tag == null || this.Tag.Equals(text, StringComparison.OrdinalIgnoreCase))
						{
							SupervisionListEntry item = new SupervisionListEntry(smtpAddress.ToString(), text, recipientType);
							results.Add(item);
						}
					}
				}
			}
		}
	}
}
