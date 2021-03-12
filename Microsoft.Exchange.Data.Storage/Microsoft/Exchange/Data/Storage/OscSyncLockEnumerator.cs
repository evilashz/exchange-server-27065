using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000512 RID: 1298
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OscSyncLockEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060037DF RID: 14303 RVA: 0x000E1CC4 File Offset: 0x000DFEC4
		internal OscSyncLockEnumerator(IMailboxSession session, StoreObjectId folder, IXSOFactory xsoFactory)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(folder, "folder");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			this.session = session;
			this.folder = folder;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000E2020 File Offset: 0x000E0220
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			using (IFolder folder = this.xsoFactory.BindToFolder(this.session, this.folder))
			{
				using (IQueryResult query = folder.IItemQuery(ItemQueryType.Associated, null, OscSyncLockEnumerator.SortByItemClassAscending, OscSyncLockEnumerator.PropertiesToLoadFromFAI))
				{
					if (!query.SeekToCondition(SeekReference.OriginBeginning, OscSyncLockEnumerator.ItemClassStartsWithOscSyncLockPrefix, SeekToConditionFlags.AllowExtendedFilters))
					{
						OscSyncLockEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "SyncLock enumerator: no SyncLock in this folder.");
						yield break;
					}
					IStorePropertyBag[] messages = query.GetPropertyBags(50);
					while (messages.Length > 0)
					{
						foreach (IStorePropertyBag message in messages)
						{
							string itemClass = message.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
							if (string.IsNullOrEmpty(itemClass))
							{
								OscSyncLockEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "SyncLock enumerator: skipping message with blank item class.");
							}
							else
							{
								if (!itemClass.StartsWith("IPM.Microsoft.OSC.SyncLock.", StringComparison.OrdinalIgnoreCase))
								{
									OscSyncLockEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "SyncLock enumerator: no further SyncLocks in this folder.");
									yield break;
								}
								yield return message;
							}
						}
						messages = query.GetPropertyBags(50);
					}
				}
			}
			yield break;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000E203C File Offset: 0x000E023C
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001DC0 RID: 7616
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04001DC1 RID: 7617
		private static readonly SortBy[] SortByItemClassAscending = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04001DC2 RID: 7618
		private static readonly PropertyDefinition[] PropertiesToLoadFromFAI = new StorePropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			MessageItemSchema.OscSyncEnabledOnServer
		};

		// Token: 0x04001DC3 RID: 7619
		private static readonly TextFilter ItemClassStartsWithOscSyncLockPrefix = new TextFilter(StoreObjectSchema.ItemClass, "IPM.Microsoft.OSC.SyncLock.", MatchOptions.Prefix, MatchFlags.IgnoreCase);

		// Token: 0x04001DC4 RID: 7620
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001DC5 RID: 7621
		private readonly IMailboxSession session;

		// Token: 0x04001DC6 RID: 7622
		private readonly StoreObjectId folder;
	}
}
