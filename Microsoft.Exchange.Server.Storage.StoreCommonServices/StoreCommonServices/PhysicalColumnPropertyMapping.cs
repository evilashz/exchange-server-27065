using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000F4 RID: 244
	internal sealed class PhysicalColumnPropertyMapping : PropertyMapping
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x0002C3BC File Offset: 0x0002A5BC
		internal PhysicalColumnPropertyMapping(StorePropTag propertyTag, Column column, Func<Context, ISimplePropertyBag, object, ErrorCode> valueSetter, StreamGetterDelegate readStreamGetter, StreamGetterDelegate writeStreamGetter, PhysicalColumn physicalColumn, bool canBeSet, bool primary, bool reservedPropId, bool list, bool tailSet) : base(PropertyMappingKind.PhysicalColumn, propertyTag, column, valueSetter, readStreamGetter, writeStreamGetter, primary, reservedPropId, list)
		{
			this.physicalColumn = physicalColumn;
			this.canBeSet = canBeSet;
			this.tailSet = tailSet;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002C3F5 File Offset: 0x0002A5F5
		public override bool CanBeSet
		{
			get
			{
				return this.canBeSet || base.ValueSetter != null;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002C40D File Offset: 0x0002A60D
		public PhysicalColumn PhysicalColumn
		{
			get
			{
				return this.physicalColumn;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0002C415 File Offset: 0x0002A615
		public override object GetPropertyValue(Context context, ISimpleReadOnlyPropertyBag bag)
		{
			return bag.GetPhysicalColumnValue(context, this.physicalColumn);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0002C424 File Offset: 0x0002A624
		public override ErrorCode SetPropertyValue(Context context, ISimplePropertyBag bag, object value)
		{
			if (!this.CanBeSet)
			{
				return ErrorCode.CreateNoAccess((LID)39704U, base.PropertyTag.PropTag);
			}
			if (base.ValueSetter != null)
			{
				ErrorCode first = ErrorCode.NoError;
				first = base.ValueSetter(context, bag, value);
				if (first != ErrorCode.NoError || !this.tailSet)
				{
					return first.Propagate((LID)39744U);
				}
			}
			if (value == null && !this.physicalColumn.IsNullable)
			{
				return ErrorCode.CreateNoAccess((LID)56088U, base.PropertyTag.PropTag);
			}
			int num;
			if (value != null && (this.physicalColumn.Type == typeof(string) || this.physicalColumn.Type == typeof(byte[])) && this.physicalColumn.TryGetColumnMaxSize(out num))
			{
				if (this.physicalColumn.Type == typeof(string))
				{
					string text = (string)value;
					if (text.Length > num)
					{
						return ErrorCode.CreateTooBig((LID)59472U);
					}
				}
				else
				{
					byte[] array = (byte[])value;
					if (array.Length > num)
					{
						return ErrorCode.CreateTooBig((LID)34896U);
					}
				}
			}
			bag.SetPhysicalColumn(context, this.physicalColumn, value);
			return ErrorCode.NoError;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002C584 File Offset: 0x0002A784
		public override bool IsPropertyChanged(Context context, ISimplePropertyBagWithChangeTracking bag)
		{
			return bag.IsPhysicalColumnChanged(context, this.physicalColumn);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0002C594 File Offset: 0x0002A794
		public override ErrorCode OpenPropertyReadStream(Context context, ISimplePropertyBag bag, out Stream readStream)
		{
			if (base.ReadStreamGetter != null)
			{
				return base.ReadStreamGetter(context, bag, out readStream);
			}
			if (!this.physicalColumn.StreamSupport)
			{
				readStream = null;
				return ErrorCode.CreateNotSupported((LID)60184U, base.PropertyTag.PropTag);
			}
			return bag.OpenPhysicalColumnReadStream(context, this.physicalColumn, out readStream);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0002C5F4 File Offset: 0x0002A7F4
		public override ErrorCode OpenPropertyWriteStream(Context context, ISimplePropertyBag bag, out Stream writeStream)
		{
			if (base.WriteStreamGetter != null)
			{
				return base.WriteStreamGetter(context, bag, out writeStream);
			}
			if (!this.physicalColumn.StreamSupport)
			{
				writeStream = null;
				return ErrorCode.CreateNotSupported((LID)45848U, base.PropertyTag.PropTag);
			}
			return bag.OpenPhysicalColumnWriteStream(context, this.physicalColumn, out writeStream);
		}

		// Token: 0x04000565 RID: 1381
		private readonly bool canBeSet;

		// Token: 0x04000566 RID: 1382
		private readonly bool tailSet;

		// Token: 0x04000567 RID: 1383
		private PhysicalColumn physicalColumn;
	}
}
