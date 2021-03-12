using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000500 RID: 1280
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OscContactSyncFAIEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x06003783 RID: 14211 RVA: 0x000DF6C0 File Offset: 0x000DD8C0
		internal OscContactSyncFAIEnumerator(IMailboxSession session, StoreObjectId folder, IXSOFactory xsoFactory)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(folder, "folder");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			this.session = session;
			this.folder = folder;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000DFA1C File Offset: 0x000DDC1C
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			using (IFolder folder = this.xsoFactory.BindToFolder(this.session, this.folder))
			{
				using (IQueryResult query = folder.IItemQuery(ItemQueryType.Associated, null, OscContactSyncFAIEnumerator.SortByItemClassAscending, OscContactSyncFAIEnumerator.PropertiesToLoadFromFAI))
				{
					if (!query.SeekToCondition(SeekReference.OriginBeginning, OscContactSyncFAIEnumerator.ItemClassEqualsContactSync))
					{
						OscContactSyncFAIEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ContactSync FAI enumerator: SeekToCondition = false.  No ContactSync FAI exists in this folder.");
						yield break;
					}
					IStorePropertyBag[] messages = query.GetPropertyBags(10);
					while (messages.Length > 0)
					{
						foreach (IStorePropertyBag message in messages)
						{
							string itemClass = message.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
							if (string.IsNullOrEmpty(itemClass))
							{
								OscContactSyncFAIEnumerator.Tracer.TraceError((long)this.GetHashCode(), "ContactSync FAI enumerator: skipping message with blank item class.");
							}
							else
							{
								if (!ObjectClass.IsOfClass(itemClass, "IPM.Microsoft.OSC.ContactSync"))
								{
									OscContactSyncFAIEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ContactSync FAI enumerator: no further ContactSync FAIs in this folder.");
									yield break;
								}
								yield return message;
							}
						}
						messages = query.GetPropertyBags(10);
					}
				}
			}
			yield break;
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000DFA38 File Offset: 0x000DDC38
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001D77 RID: 7543
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04001D78 RID: 7544
		private static readonly SortBy[] SortByItemClassAscending = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04001D79 RID: 7545
		private static readonly PropertyDefinition[] PropertiesToLoadFromFAI = new StorePropertyDefinition[]
		{
			MessageItemSchema.OscContactSources,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04001D7A RID: 7546
		private static readonly ComparisonFilter ItemClassEqualsContactSync = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Microsoft.OSC.ContactSync");

		// Token: 0x04001D7B RID: 7547
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001D7C RID: 7548
		private readonly IMailboxSession session;

		// Token: 0x04001D7D RID: 7549
		private readonly StoreObjectId folder;
	}
}
