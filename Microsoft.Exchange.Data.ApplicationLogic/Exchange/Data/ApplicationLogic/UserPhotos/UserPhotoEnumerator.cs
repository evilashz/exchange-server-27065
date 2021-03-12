using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x0200021C RID: 540
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UserPhotoEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x00050614 File Offset: 0x0004E814
		internal UserPhotoEnumerator(IMailboxSession session, StoreObjectId folder, string photoItemClass, IXSOFactory xsoFactory, ITracer upstreamTracer)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			if (string.IsNullOrEmpty(photoItemClass))
			{
				throw new ArgumentNullException("photoItemClass");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.session = session;
			this.folder = folder;
			this.photoItemClass = photoItemClass;
			this.filter = this.GetFilterForItemClass(photoItemClass);
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000509F4 File Offset: 0x0004EBF4
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			using (IFolder folder = this.xsoFactory.BindToFolder(this.session, this.folder))
			{
				using (IQueryResult query = folder.IItemQuery(ItemQueryType.None, null, UserPhotoEnumerator.SortByItemClassAscending, UserPhotoEnumerator.PropertiesToLoad))
				{
					if (!query.SeekToCondition(SeekReference.OriginBeginning, this.filter))
					{
						this.tracer.TraceDebug((long)this.GetHashCode(), "UserPhoto enumerator: no photos in this folder.");
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
								this.tracer.TraceDebug((long)this.GetHashCode(), "UserPhoto enumerator: skipping message with blank item class.");
							}
							else
							{
								if (!itemClass.Equals(this.photoItemClass, StringComparison.OrdinalIgnoreCase))
								{
									this.tracer.TraceDebug((long)this.GetHashCode(), "UserPhoto enumerator: no further photos in this folder.");
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

		// Token: 0x06001372 RID: 4978 RVA: 0x00050A10 File Offset: 0x0004EC10
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00050A1C File Offset: 0x0004EC1C
		private QueryFilter GetFilterForItemClass(string itemClass)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, itemClass);
		}

		// Token: 0x04000AD9 RID: 2777
		private static readonly SortBy[] SortByItemClassAscending = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04000ADA RID: 2778
		private static readonly PropertyDefinition[] PropertiesToLoad = new StorePropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04000ADB RID: 2779
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04000ADC RID: 2780
		private readonly IMailboxSession session;

		// Token: 0x04000ADD RID: 2781
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04000ADE RID: 2782
		private readonly StoreObjectId folder;

		// Token: 0x04000ADF RID: 2783
		private readonly string photoItemClass;

		// Token: 0x04000AE0 RID: 2784
		private readonly QueryFilter filter;
	}
}
