using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F2 RID: 242
	public abstract class PropertyMapping
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x0002C1F8 File Offset: 0x0002A3F8
		internal PropertyMapping(PropertyMappingKind kind, StorePropTag propertyTag, Column column, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, StreamGetterDelegate readStreamGetter, StreamGetterDelegate writeStreamGetter, bool primary, bool reservedPropId, bool list)
		{
			this.propertyTag = propertyTag;
			this.mappingKind = kind;
			this.column = column;
			this.valueSetter = valueSetter;
			this.readStreamGetter = readStreamGetter;
			this.writeStreamGetter = writeStreamGetter;
			this.primary = primary;
			this.reservedPropId = reservedPropId;
			this.list = list;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0002C250 File Offset: 0x0002A450
		public StorePropTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0002C258 File Offset: 0x0002A458
		public PropertyMappingKind MappingKind
		{
			get
			{
				return this.mappingKind;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0002C260 File Offset: 0x0002A460
		public Column Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0002C268 File Offset: 0x0002A468
		public Func<Context, ISimplePropertyBag, object, ErrorCode> ValueSetter
		{
			get
			{
				return this.valueSetter;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002C270 File Offset: 0x0002A470
		public StreamGetterDelegate ReadStreamGetter
		{
			get
			{
				return this.readStreamGetter;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x0002C278 File Offset: 0x0002A478
		public StreamGetterDelegate WriteStreamGetter
		{
			get
			{
				return this.writeStreamGetter;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002C280 File Offset: 0x0002A480
		public bool IsPrimary
		{
			get
			{
				return this.primary;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0002C288 File Offset: 0x0002A488
		public bool IsReservedPropId
		{
			get
			{
				return this.reservedPropId;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002C290 File Offset: 0x0002A490
		public bool ShouldBeListed
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000995 RID: 2453
		public abstract bool CanBeSet { get; }

		// Token: 0x06000996 RID: 2454
		public abstract object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag);

		// Token: 0x06000997 RID: 2455
		public abstract ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value);

		// Token: 0x06000998 RID: 2456
		public abstract bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag);

		// Token: 0x06000999 RID: 2457 RVA: 0x0002C298 File Offset: 0x0002A498
		public virtual ErrorCode OpenPropertyReadStream(Context context, ISimplePropertyBag bag, out Stream readStream)
		{
			if (this.ReadStreamGetter == null)
			{
				readStream = null;
				return ErrorCode.CreateNotSupported((LID)47896U, this.PropertyTag.PropTag);
			}
			return this.ReadStreamGetter(context, bag, out readStream);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002C2DC File Offset: 0x0002A4DC
		public virtual ErrorCode OpenPropertyWriteStream(Context context, ISimplePropertyBag bag, out Stream writeStream)
		{
			if (this.WriteStreamGetter == null)
			{
				writeStream = null;
				return ErrorCode.CreateNotSupported((LID)64280U, this.PropertyTag.PropTag);
			}
			return this.WriteStreamGetter(context, bag, out writeStream);
		}

		// Token: 0x0400055B RID: 1371
		private readonly PropertyMappingKind mappingKind;

		// Token: 0x0400055C RID: 1372
		private readonly StorePropTag propertyTag;

		// Token: 0x0400055D RID: 1373
		private readonly Column column;

		// Token: 0x0400055E RID: 1374
		private readonly Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter;

		// Token: 0x0400055F RID: 1375
		private StreamGetterDelegate readStreamGetter;

		// Token: 0x04000560 RID: 1376
		private StreamGetterDelegate writeStreamGetter;

		// Token: 0x04000561 RID: 1377
		private bool primary;

		// Token: 0x04000562 RID: 1378
		private bool reservedPropId;

		// Token: 0x04000563 RID: 1379
		private bool list;
	}
}
