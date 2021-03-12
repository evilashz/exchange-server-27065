using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009EF RID: 2543
	public static class IdentityExtensions
	{
		// Token: 0x060047CE RID: 18382 RVA: 0x0010081C File Offset: 0x000FEA1C
		public static Identity ToIdentity(this ADObjectId source)
		{
			if (source != null)
			{
				return source.ToIdentity(source.Name);
			}
			return null;
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0010082F File Offset: 0x000FEA2F
		public static Identity ToIdentity(this ADObjectId source, string displayName)
		{
			if (source != null)
			{
				return new Identity(source, displayName);
			}
			return null;
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0010083D File Offset: 0x000FEA3D
		public static Identity ToIdentity(this AggregationSubscriptionIdentity source, string displayName)
		{
			if (source != null)
			{
				return new Identity(source.GuidIdentityString, displayName);
			}
			return null;
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x00100850 File Offset: 0x000FEA50
		public static Identity ToIdentity(this MailboxFolder source)
		{
			if (source == null)
			{
				return null;
			}
			string displayName = source.Name;
			if (source.DefaultFolderType == DefaultFolderType.Root)
			{
				MailboxFolderId mailboxFolderId = (MailboxFolderId)source.Identity;
				if (ADObjectId.Equals(mailboxFolderId.MailboxOwnerId, CallContext.Current.AccessingADUser.Id))
				{
					displayName = CallContext.Current.AccessingPrincipal.MailboxInfo.DisplayName;
				}
				else
				{
					displayName = mailboxFolderId.MailboxOwnerId.Name;
				}
			}
			return new Identity(source.Identity.ToString(), displayName);
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x001008EC File Offset: 0x000FEAEC
		public static Identity[] ToIdentityArray(this IEnumerable<ADObjectId> source)
		{
			if (source == null || !source.Any<ADObjectId>())
			{
				return null;
			}
			IEnumerable<Identity> source2 = from e in source
			select e.ToIdentity();
			return source2.ToArray<Identity>();
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0010095C File Offset: 0x000FEB5C
		public static Identity[] ToIdentityArray(this IEnumerable<MessageClassification> classifications)
		{
			if (classifications == null || !classifications.Any<MessageClassification>())
			{
				return null;
			}
			IEnumerable<Identity> source = from e in classifications
			select new Identity(e.Guid.ToString(), e.DisplayName);
			return source.ToArray<Identity>();
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x001009A0 File Offset: 0x000FEBA0
		public static Identity[] ToIdentityArray(this AggregationSubscriptionIdentity[] subscriptionIds, InboxRule ruleObj)
		{
			IList<string> list = null;
			if (!subscriptionIds.IsNullOrEmpty<AggregationSubscriptionIdentity>())
			{
				list = ruleObj.GetSubscriptionEmailAddresses(subscriptionIds);
			}
			if (list == null || list.Count == 0)
			{
				return null;
			}
			List<Identity> list2 = new List<Identity>();
			for (int i = 0; i < subscriptionIds.Length; i++)
			{
				list2.Add(new Identity(subscriptionIds[i].ToString(), list[i]));
			}
			return list2.ToArray();
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x00100A00 File Offset: 0x000FEC00
		public static T ToIdParameter<T>(this Identity identity) where T : ADIdParameter
		{
			if (!(null == identity))
			{
				return (T)((object)Activator.CreateInstance(typeof(T), new object[]
				{
					identity
				}));
			}
			return default(T);
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x00100A58 File Offset: 0x000FEC58
		public static IEnumerable<AggregationSubscriptionIdentity> ToIdParameters(this IEnumerable<Identity> identities)
		{
			if (identities == null || !identities.Any<Identity>())
			{
				return null;
			}
			return from identity in identities
			where identity != null
			select new AggregationSubscriptionIdentity(identity.RawIdentity);
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x00100AC8 File Offset: 0x000FECC8
		public static IEnumerable<T> ToIdParameters<T>(this IEnumerable<Identity> identities) where T : ADIdParameter
		{
			if (identities == null || !identities.Any<Identity>())
			{
				return null;
			}
			return from identity in identities
			where identity != null
			select identity.ToIdParameter<T>();
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x00100AFC File Offset: 0x000FECFC
		public static MailboxFolderIdParameter ToMailboxFolderIdParameter(this Identity identity)
		{
			if (!(null != identity))
			{
				return null;
			}
			if (identity.RawIdentity.IndexOf(':') < 0)
			{
				string arg = IdConverter.EwsIdToMessageStoreObjectId(identity.RawIdentity).ToString();
				return new MailboxFolderIdParameter(new Identity(':' + arg, identity.DisplayName));
			}
			return new MailboxFolderIdParameter(identity);
		}
	}
}
