using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000147 RID: 327
	internal sealed class UserPhotoRecipientQuery : RecipientQuery
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x00026A4B File Offset: 0x00024C4B
		internal UserPhotoRecipientQuery(ClientContext clientContext, ADObjectId baseDN, OrganizationId organizationId, DateTime queryPrepareDeadline, ITracer upstreamTracer) : base(clientContext, baseDN, organizationId, queryPrepareDeadline, UserPhotoRecipientQuery.RecipientProperties)
		{
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00026A70 File Offset: 0x00024C70
		internal IList<RecipientData> Query(string recipientSmtpAddress, ADObjectId recipientAdObjectId)
		{
			if (!string.IsNullOrEmpty(recipientSmtpAddress) && SmtpAddress.IsValidSmtpAddress(recipientSmtpAddress))
			{
				RecipientQueryResults recipientQueryResults = base.Query(new EmailAddress[]
				{
					new EmailAddress(string.Empty, recipientSmtpAddress)
				});
				if (recipientQueryResults != null && recipientQueryResults.Count > 0 && recipientQueryResults[0].Exception == null && recipientQueryResults[0].ProviderError == null)
				{
					return recipientQueryResults;
				}
			}
			if (recipientAdObjectId != null)
			{
				ADRecipient adrecipient = base.ADRecipientSession.Read(recipientAdObjectId);
				if (adrecipient != null)
				{
					EmailAddress emailAddress = RecipientQuery.CreateEmailAddressFromADRecipient(adrecipient);
					return new RecipientData[]
					{
						RecipientData.Create(emailAddress, adrecipient, UserPhotoRecipientQuery.RecipientProperties)
					};
				}
			}
			this.tracer.TraceDebug<string, ADObjectId>((long)this.GetHashCode(), "Target user not found in directory.  Search params were SMTP-address='{0}' OR ADObjectId='{1}'", recipientSmtpAddress, recipientAdObjectId);
			return Array<RecipientData>.Empty;
		}

		// Token: 0x040006EC RID: 1772
		internal static readonly PropertyDefinition[] RecipientProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.ExternalEmailAddress,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.LegacyExchangeDN,
			ADMailboxRecipientSchema.Database,
			ADRecipientSchema.MasterAccountSid,
			ADMailboxRecipientSchema.ServerLegacyDN,
			ADMailboxRecipientSchema.ExchangeGuid,
			ADMailboxRecipientSchema.Database,
			ADObjectSchema.ExchangeVersion,
			ADRecipientSchema.ThumbnailPhoto
		};

		// Token: 0x040006ED RID: 1773
		private readonly ITracer tracer;
	}
}
