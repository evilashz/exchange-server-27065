using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x0200079B RID: 1947
	[Cmdlet("Get", "CalendarProcessing")]
	public sealed class GetCalendarProcessing : GetXsoObjectWithIdentityTaskBase<CalendarConfiguration, ADUser>
	{
		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x00119130 File Offset: 0x00117330
		// (set) Token: 0x06004490 RID: 17552 RVA: 0x00119138 File Offset: 0x00117338
		public new PSCredential Credential
		{
			get
			{
				return base.Credential;
			}
			set
			{
				base.Credential = value;
			}
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x00119141 File Offset: 0x00117341
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new CalendarConfigurationDataProvider(principal, "Get-CalendarProcessing");
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x00119150 File Offset: 0x00117350
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			IConfigurable configurable = base.ConvertDataObjectToPresentationObject(dataObject);
			CalendarConfiguration calendarConfiguration = configurable as CalendarConfiguration;
			if (calendarConfiguration != null)
			{
				calendarConfiguration.RequestOutOfPolicy = this.ResolveAndMerge(calendarConfiguration.RequestOutOfPolicyLegacy, calendarConfiguration.RequestOutOfPolicy);
				calendarConfiguration.BookInPolicy = this.ResolveAndMerge(calendarConfiguration.BookInPolicyLegacy, calendarConfiguration.BookInPolicy);
				calendarConfiguration.RequestInPolicy = this.ResolveAndMerge(calendarConfiguration.RequestInPolicyLegacy, calendarConfiguration.RequestInPolicy);
				if (dataObject is ADUser)
				{
					calendarConfiguration.ResourceDelegates = ((ADUser)dataObject).GrantSendOnBehalfTo;
				}
			}
			else
			{
				calendarConfiguration = new CalendarConfiguration();
				if (dataObject is ADUser)
				{
					calendarConfiguration.MailboxOwnerId = ((ADUser)dataObject).Id;
				}
			}
			return calendarConfiguration;
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x001191F4 File Offset: 0x001173F4
		private MultiValuedProperty<string> ResolveAndMerge(MultiValuedProperty<ADObjectId> recipientList, MultiValuedProperty<string> existingAddress)
		{
			if (recipientList == null || recipientList.Count == 0)
			{
				return existingAddress;
			}
			MultiValuedProperty<string> multiValuedProperty = null;
			IRecipientSession tenantGlobalCatalogSession = base.TenantGlobalCatalogSession;
			Result<ADRecipient>[] array = tenantGlobalCatalogSession.ReadMultiple(recipientList.ToArray());
			multiValuedProperty = new MultiValuedProperty<string>();
			foreach (Result<ADRecipient> result in array)
			{
				if (result.Error == null && result.Data != null && !this.DoesExist(result.Data, existingAddress))
				{
					multiValuedProperty.Add(result.Data.LegacyExchangeDN);
				}
			}
			foreach (string item in existingAddress)
			{
				multiValuedProperty.Add(item);
			}
			return multiValuedProperty;
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x001192C8 File Offset: 0x001174C8
		private bool DoesExist(ADRecipient recipient, MultiValuedProperty<string> existingAddress)
		{
			return existingAddress != null && existingAddress.Count != 0 && existingAddress.Contains(recipient.LegacyExchangeDN);
		}
	}
}
