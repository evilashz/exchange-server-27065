using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C6 RID: 454
	internal abstract class IsMemberOfResolverADAdapter<GroupKeyType>
	{
		// Token: 0x06001286 RID: 4742 RVA: 0x00059574 File Offset: 0x00057774
		public IsMemberOfResolverADAdapter(bool disableDynamicGroups)
		{
			this.disableDynamicGroups = disableDynamicGroups;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000595D8 File Offset: 0x000577D8
		public virtual ResolvedGroup ResolveGroup(IRecipientSession session, GroupKeyType group, out int ldapQueries)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			ResolvedGroup resolvedGroup = null;
			int numQueries = 0;
			this.RunADOperation(group, delegate
			{
				Guid groupGuid;
				Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType;
				if (this.TryGetGroupInfo(session, group, out groupGuid, out recipientType, out numQueries) && this.IsGroup(recipientType))
				{
					resolvedGroup = new ResolvedGroup(groupGuid);
				}
			});
			ldapQueries = numQueries;
			return resolvedGroup;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00059770 File Offset: 0x00057970
		public virtual ExpandedGroup ExpandGroup(IRecipientSession session, ADObjectId groupId, out int ldapQueries)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			ExpandedGroup expandedGroup = null;
			int numQueries = 0;
			this.RunADOperation(groupId, delegate
			{
				IADDistributionList iaddistributionList = session.Read(groupId) as IADDistributionList;
				numQueries++;
				if (iaddistributionList != null)
				{
					if (this.disableDynamicGroups && iaddistributionList is ADDynamicGroup)
					{
						expandedGroup = null;
						return;
					}
					List<Guid> list = new List<Guid>();
					List<Guid> list2 = new List<Guid>();
					ADPagedReader<ADRawEntry> adpagedReader = iaddistributionList.Expand(1000, IsMemberOfResolverADAdapter<GroupKeyType>.properties);
					numQueries++;
					foreach (ADRawEntry adrawEntry in adpagedReader)
					{
						Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType = (Microsoft.Exchange.Data.Directory.Recipient.RecipientType)adrawEntry[ADRecipientSchema.RecipientType];
						if (this.IsGroup(recipientType))
						{
							list.Add(adrawEntry.Id.ObjectGuid);
						}
						else
						{
							list2.Add(adrawEntry.Id.ObjectGuid);
						}
					}
					expandedGroup = this.CreateExpandedGroup((ADObject)iaddistributionList, list, list2);
				}
			});
			ldapQueries = numQueries;
			return expandedGroup;
		}

		// Token: 0x06001289 RID: 4745
		protected abstract bool TryGetGroupInfo(IRecipientSession session, GroupKeyType group, out Guid objectGuid, out Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType, out int ldapQueries);

		// Token: 0x0600128A RID: 4746 RVA: 0x000597DF File Offset: 0x000579DF
		protected virtual ExpandedGroup CreateExpandedGroup(ADObject group, List<Guid> memberGroups, List<Guid> memberRecipients)
		{
			return new ExpandedGroup(memberGroups, memberRecipients);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000597E8 File Offset: 0x000579E8
		private bool IsGroup(Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType)
		{
			return recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Group || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalDistributionGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalSecurityGroup || recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailNonUniversalGroup || (!this.disableDynamicGroups && recipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.DynamicDistributionGroup);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00059810 File Offset: 0x00057A10
		private void RunADOperation(object group, ADOperation adOperation)
		{
			try
			{
				ADNotificationAdapter.RunADOperation(adOperation);
			}
			catch (TransientException e)
			{
				this.RethrowException(group, e);
			}
			catch (DataValidationException e2)
			{
				this.RethrowException(group, e2);
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00059858 File Offset: 0x00057A58
		private void RethrowException(object group, Exception e)
		{
			string text = string.Empty;
			if (group != null)
			{
				text = group.ToString();
			}
			IsMemberOfResolver<GroupKeyType>.Tracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "AD query for group {0} failed with exception: {1}", text, e);
			throw new AddressBookTransientException(DirectoryStrings.IsMemberOfQueryFailed(text), e);
		}

		// Token: 0x04000AB4 RID: 2740
		private const int PageSize = 1000;

		// Token: 0x04000AB5 RID: 2741
		private static PropertyDefinition[] properties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.RecipientType
		};

		// Token: 0x04000AB6 RID: 2742
		protected bool disableDynamicGroups;

		// Token: 0x020001C7 RID: 455
		public class RoutingAddressResolver : IsMemberOfResolverADAdapter<RoutingAddress>
		{
			// Token: 0x0600128F RID: 4751 RVA: 0x000598C6 File Offset: 0x00057AC6
			public RoutingAddressResolver(bool disableDynamicGroups) : base(disableDynamicGroups)
			{
			}

			// Token: 0x06001290 RID: 4752 RVA: 0x000598D0 File Offset: 0x00057AD0
			protected override bool TryGetGroupInfo(IRecipientSession session, RoutingAddress group, out Guid objectGuid, out Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType, out int ldapQueries)
			{
				ldapQueries = 0;
				if (group.IsValid)
				{
					ADRawEntry adrawEntry = session.FindByProxyAddress(new SmtpProxyAddress((string)group, false), IsMemberOfResolverADAdapter<RoutingAddress>.properties);
					ldapQueries = 1;
					if (adrawEntry != null)
					{
						objectGuid = adrawEntry.Id.ObjectGuid;
						recipientType = (Microsoft.Exchange.Data.Directory.Recipient.RecipientType)adrawEntry[ADRecipientSchema.RecipientType];
						return true;
					}
				}
				objectGuid = Guid.Empty;
				recipientType = Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Invalid;
				return false;
			}
		}

		// Token: 0x020001C8 RID: 456
		public class LegacyDNResolver : IsMemberOfResolverADAdapter<string>
		{
			// Token: 0x06001291 RID: 4753 RVA: 0x0005993F File Offset: 0x00057B3F
			public LegacyDNResolver(bool disableDynamicGroups) : base(disableDynamicGroups)
			{
			}

			// Token: 0x06001292 RID: 4754 RVA: 0x00059948 File Offset: 0x00057B48
			protected override bool TryGetGroupInfo(IRecipientSession session, string group, out Guid objectGuid, out Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType, out int ldapQueries)
			{
				ldapQueries = 0;
				if (!string.IsNullOrEmpty(group))
				{
					Result<ADRawEntry>[] array = session.FindByLegacyExchangeDNs(new string[]
					{
						group
					}, IsMemberOfResolverADAdapter<RoutingAddress>.properties);
					ldapQueries = 1;
					if (array != null && array.Length > 0 && array[0].Data != null)
					{
						ADRawEntry data = array[0].Data;
						objectGuid = data.Id.ObjectGuid;
						recipientType = (Microsoft.Exchange.Data.Directory.Recipient.RecipientType)data[ADRecipientSchema.RecipientType];
						return true;
					}
				}
				objectGuid = Guid.Empty;
				recipientType = Microsoft.Exchange.Data.Directory.Recipient.RecipientType.Invalid;
				return false;
			}
		}
	}
}
