using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F3 RID: 243
	internal sealed class FreeBusyRecipientQuery : RecipientQuery
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x0001CC79 File Offset: 0x0001AE79
		internal FreeBusyRecipientQuery(ClientContext clientContext, ADObjectId queryBaseDn, OrganizationId organizationId, DateTime queryPrepareDeadline) : base(clientContext, queryBaseDn, organizationId, queryPrepareDeadline, FreeBusyRecipientQuery.RecipientProperties)
		{
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		internal RecipientQueryResults Query(MailboxData[] mailboxDataArray)
		{
			EmailAddress[] array = new EmailAddress[mailboxDataArray.Length];
			for (int i = 0; i < mailboxDataArray.Length; i++)
			{
				array[i] = FreeBusyRecipientQuery.ConvertMailboxDataToEmailAddress(mailboxDataArray[i]);
			}
			RecipientQueryResults recipientQueryResults = base.Query(array);
			for (int j = 0; j < mailboxDataArray.Length; j++)
			{
				recipientQueryResults[j].AssociatedFolderId = mailboxDataArray[j].AssociatedFolderId;
			}
			return recipientQueryResults;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		internal List<RecipientData> ExpandDistributionGroup(RecipientData recipientData, DateTime queryPrepareDeadline, out bool groupCapped)
		{
			List<RecipientData> list = new List<RecipientData>(Configuration.MaximumGroupMemberCount);
			groupCapped = false;
			int num = 0;
			Queue<RecipientData> queue = new Queue<RecipientData>(Configuration.MaximumGroupMemberCount);
			HashSet<Guid> hashSet = new HashSet<Guid>(Configuration.MaximumGroupMemberCount);
			queue.Enqueue(recipientData);
			hashSet.Add(recipientData.Guid);
			while (queue.Count > 0 && !groupCapped)
			{
				RecipientData recipientData2 = queue.Dequeue();
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, recipientData2.Id);
				ADPagedReader<ADRecipient> adpagedReader = base.ADRecipientSession.FindPaged(null, QueryScope.SubTree, filter, null, Configuration.MaximumGroupMemberCount);
				ExTraceGlobals.DistributionListHandlingTracer.TraceDebug<object, SmtpAddress, int>((long)this.GetHashCode(), "{0}: Expanding group {1}, which has {2} members.", TraceContext.Get(), recipientData.PrimarySmtpAddress, recipientData.DistributionGroupMembersCount);
				foreach (ADRecipient adrecipient in adpagedReader)
				{
					if (DateTime.UtcNow > queryPrepareDeadline)
					{
						groupCapped = true;
						break;
					}
					if (!hashSet.Contains(adrecipient.Guid))
					{
						hashSet.Add(adrecipient.Guid);
						if (adrecipient is ADUser || adrecipient is ADContact)
						{
							if (list.Count >= Configuration.MaximumGroupMemberCount)
							{
								groupCapped = true;
								break;
							}
							EmailAddress emailAddress = RecipientQuery.CreateEmailAddressFromADRecipient(adrecipient);
							list.Add(RecipientData.Create(emailAddress, adrecipient, base.PropertyDefinitionArray));
						}
						else if (adrecipient is ADGroup)
						{
							EmailAddress emailAddress2 = RecipientQuery.CreateEmailAddressFromADRecipient(adrecipient);
							ADGroup configurableObject = (ADGroup)adrecipient;
							queue.Enqueue(RecipientData.Create(emailAddress2, configurableObject, base.PropertyDefinitionArray));
						}
						else
						{
							ExTraceGlobals.DistributionListHandlingTracer.TraceDebug<object, ADRecipient>((long)this.GetHashCode(), "{0}: Group member {1} is not a ADUser, ADContact or ADGroup. This member is not being processed.", TraceContext.Get(), adrecipient);
						}
					}
					else
					{
						ExTraceGlobals.DistributionListHandlingTracer.TraceDebug<object, ADRecipient>((long)this.GetHashCode(), "{0}: Group member {1} has been found to be a duplicate and not being processed.", TraceContext.Get(), adrecipient);
						num++;
					}
				}
			}
			return list;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001CED8 File Offset: 0x0001B0D8
		private static EmailAddress ConvertMailboxDataToEmailAddress(MailboxData mailboxData)
		{
			return mailboxData.Email;
		}

		// Token: 0x040003E6 RID: 998
		private static readonly PropertyDefinition[] RecipientProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.ExternalEmailAddress,
			ADMailboxRecipientSchema.ServerLegacyDN,
			ADMailboxRecipientSchema.ExchangeGuid,
			ADMailboxRecipientSchema.Database,
			ADObjectSchema.ExchangeVersion,
			ADObjectSchema.Guid,
			ADObjectSchema.Id,
			ADGroupSchema.Members,
			ADMailboxRecipientSchema.Sid,
			ADRecipientSchema.MasterAccountSid,
			ADMailboxRecipientSchema.ExchangeSecurityDescriptor,
			ADObjectSchema.OrganizationId
		};
	}
}
