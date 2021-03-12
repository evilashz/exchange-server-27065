using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004CB RID: 1227
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactsByGALLinkIdEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060035DC RID: 13788 RVA: 0x000D8E88 File Offset: 0x000D7088
		public ContactsByGALLinkIdEnumerator(MailboxSession session, DefaultFolderType defaultFolderType, Guid galLinkId, ICollection<PropertyDefinition> requestedProperties)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfEmpty("galLinkId", galLinkId);
			ArgumentValidator.ThrowIfNull("columns", requestedProperties);
			this.session = session;
			this.defaultFolderType = defaultFolderType;
			this.galLinkId = galLinkId;
			this.requestedProperties = requestedProperties;
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000D92E4 File Offset: 0x000D74E4
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			ContactsByGALLinkIdEnumerator.Tracer.TraceDebug<Guid>((long)this.galLinkId.GetHashCode(), "ContactsByGALLinkIdEnumerator.GetEnumerator: this.galLinkId = {0}", this.galLinkId);
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.GALLinkID, this.galLinkId);
			PropertyDefinition[] allProperties = PropertyDefinitionCollection.Merge<PropertyDefinition>(new IEnumerable<PropertyDefinition>[]
			{
				ContactsByGALLinkIdEnumerator.RequiredProperties,
				this.requestedProperties
			});
			Folder folder = Folder.Bind(this.session, this.defaultFolderType, Array<PropertyDefinition>.Empty);
			QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, ContactsByGALLinkIdEnumerator.SortByGalLinkId, allProperties);
			if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, filter))
			{
				ContactsByGALLinkIdEnumerator.Tracer.TraceDebug<Guid>((long)this.galLinkId.GetHashCode(), "ContactsByGALLinkIdEnumerator.GetEnumerator: SeekToCondition = false.  No contacts with GALLinkID={0}.", this.galLinkId);
				yield break;
			}
			for (;;)
			{
				ContactsByGALLinkIdEnumerator.Tracer.TraceDebug<int>((long)this.galLinkId.GetHashCode(), "ContactsByGALLinkIdEnumerator.GetEnumerator: querying for {0} more property bags", 100);
				IStorePropertyBag[] contacts = queryResult.GetPropertyBags(100);
				if (contacts == null || contacts.Length == 0)
				{
					break;
				}
				foreach (IStorePropertyBag contact in contacts)
				{
					Guid contactGalLinkId = contact.GetValueOrDefault<Guid>(InternalSchema.GALLinkID, Guid.Empty);
					if (contactGalLinkId != this.galLinkId)
					{
						goto Block_3;
					}
					ContactsByGALLinkIdEnumerator.Tracer.TraceDebug((long)this.galLinkId.GetHashCode(), "ContactsByGALLinkIdEnumerator.GetEnumerator: found property bag");
					yield return contact;
				}
			}
			ContactsByGALLinkIdEnumerator.Tracer.TraceDebug((long)this.galLinkId.GetHashCode(), "ContactsByGALLinkIdEnumerator.GetEnumerator: no more property bags");
			yield break;
			Block_3:
			ContactsByGALLinkIdEnumerator.Tracer.TraceDebug((long)this.galLinkId.GetHashCode(), "ContactsByGALLinkIdEnumerator.GetEnumerator: no more property bags");
			yield break;
			yield break;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000D9300 File Offset: 0x000D7500
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x04001CE2 RID: 7394
		private const int ContactBatchSize = 100;

		// Token: 0x04001CE3 RID: 7395
		private static readonly Trace Tracer = ExTraceGlobals.PersonTracer;

		// Token: 0x04001CE4 RID: 7396
		private static readonly SortBy[] SortByGalLinkId = new SortBy[]
		{
			new SortBy(InternalSchema.GALLinkID, SortOrder.Ascending)
		};

		// Token: 0x04001CE5 RID: 7397
		private static readonly PropertyDefinition[] RequiredProperties = new PropertyDefinition[]
		{
			InternalSchema.GALLinkID
		};

		// Token: 0x04001CE6 RID: 7398
		private readonly MailboxSession session;

		// Token: 0x04001CE7 RID: 7399
		private readonly ICollection<PropertyDefinition> requestedProperties;

		// Token: 0x04001CE8 RID: 7400
		private readonly DefaultFolderType defaultFolderType;

		// Token: 0x04001CE9 RID: 7401
		private readonly Guid galLinkId;
	}
}
