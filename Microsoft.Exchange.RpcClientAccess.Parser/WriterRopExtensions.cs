using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000394 RID: 916
	internal static class WriterRopExtensions
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x00039654 File Offset: 0x00037854
		public static void WriteSizedRestriction(this Writer writer, Restriction restriction, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			long position = writer.Position;
			writer.WriteUInt16(0);
			if (restriction == null)
			{
				return;
			}
			restriction.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.UpdateSize(position, writer.Position);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0003968C File Offset: 0x0003788C
		public static void WriteCountedStoreIdPairs(this Writer writer, StoreIdPair[] pairs)
		{
			if (pairs != null)
			{
				writer.WriteUInt32((uint)pairs.Length);
				uint num = 0U;
				while ((ulong)num < (ulong)((long)pairs.Length))
				{
					pairs[(int)((UIntPtr)num)].Serialize(writer);
					num += 1U;
				}
				return;
			}
			writer.WriteUInt32(0U);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000396CB File Offset: 0x000378CB
		public static void WriteCountedStoreIds(this Writer writer, StoreId[] folderIds)
		{
			if (folderIds != null)
			{
				writer.WriteUInt16((ushort)folderIds.Length);
				writer.WriteStoreIds(folderIds);
				return;
			}
			writer.WriteUInt16(0);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x000396EC File Offset: 0x000378EC
		public static void WriteStoreIds(this Writer writer, StoreId[] elements)
		{
			if (elements != null)
			{
				for (int i = 0; i < elements.Length; i++)
				{
					elements[i].Serialize(writer);
				}
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00039718 File Offset: 0x00037918
		public static void WriteUInt64Array(this Writer writer, ulong[] elements)
		{
			if (elements != null)
			{
				for (int i = 0; i < elements.Length; i++)
				{
					writer.WriteUInt64(elements[i]);
				}
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00039740 File Offset: 0x00037940
		public static void WriteCountedStoreLongTermIds(this Writer writer, StoreLongTermId[] folderIds)
		{
			if (folderIds == null)
			{
				throw new ArgumentNullException("folderIds");
			}
			writer.WriteUInt16((ushort)folderIds.Length);
			foreach (StoreLongTermId storeLongTermId in folderIds)
			{
				storeLongTermId.Serialize(writer);
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0003978C File Offset: 0x0003798C
		public static void WriteCountedNamedProperties(this Writer writer, NamedProperty[] namedProperties)
		{
			if (namedProperties != null)
			{
				writer.WriteUInt16((ushort)namedProperties.Length);
				for (int i = 0; i < namedProperties.Length; i++)
				{
					namedProperties[i].Serialize(writer);
				}
				return;
			}
			writer.WriteUInt16(0);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000397C8 File Offset: 0x000379C8
		public static void WriteCountAndPropertyTagArray(this Writer writer, PropertyTag[] propertyTags, FieldLength fieldLength)
		{
			if (propertyTags == null)
			{
				propertyTags = Array<PropertyTag>.Empty;
			}
			writer.WriteCountOrSize(propertyTags.Length, fieldLength);
			for (int i = 0; i < propertyTags.Length; i++)
			{
				writer.WritePropertyTag(propertyTags[i]);
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0003980C File Offset: 0x00037A0C
		public static void WriteCountAndUnicodeStringList(this Writer writer, string[] values, StringFlags flags, FieldLength fieldLength)
		{
			if (values == null)
			{
				values = Array<string>.Empty;
			}
			writer.WriteCountOrSize(values.Length, fieldLength);
			for (int i = 0; i < values.Length; i++)
			{
				writer.WriteUnicodeString(values[i], flags);
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00039848 File Offset: 0x00037A48
		public static void WriteCountAndString8List(this Writer writer, string[] values, Encoding encoding, StringFlags flags, FieldLength fieldLength)
		{
			if (values == null)
			{
				values = Array<string>.Empty;
			}
			writer.WriteCountOrSize(values.Length, fieldLength);
			for (int i = 0; i < values.Length; i++)
			{
				writer.WriteString8(values[i], encoding, flags);
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00039884 File Offset: 0x00037A84
		public static void WriteSizedMessageReadStates(this Writer writer, MessageReadState[] messageReadStates)
		{
			long position = writer.Position;
			writer.WriteUInt16(0);
			for (int i = 0; i < messageReadStates.Length; i++)
			{
				messageReadStates[i].Serialize(writer);
			}
			writer.UpdateSize(position, writer.Position);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000398C8 File Offset: 0x00037AC8
		public static void WriteSizedLongTermIdRanges(this Writer writer, LongTermIdRange[] longTermIdRanges)
		{
			long position = writer.Position;
			writer.WriteUInt16(0);
			writer.WriteUInt32((uint)longTermIdRanges.Length);
			for (int i = 0; i < longTermIdRanges.Length; i++)
			{
				longTermIdRanges[i].Serialize(writer);
			}
			writer.UpdateSize(position, writer.Position);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00039914 File Offset: 0x00037B14
		public static void WriteFormattedString(this Writer writer, string value, bool useUnicode, Encoding string8Encoding)
		{
			if (value == null)
			{
				writer.WriteByte(0);
				return;
			}
			if (value.Length == 0)
			{
				writer.WriteByte(1);
				return;
			}
			if (!useUnicode)
			{
				writer.WriteByte(2);
				writer.WriteString8(value, string8Encoding, StringFlags.IncludeNull);
				return;
			}
			bool flag = ReducedUnicodeEncoding.IsStringConvertible(value);
			if (flag)
			{
				writer.WriteByte(3);
				writer.WriteString8(value, String8Encodings.ReducedUnicode, StringFlags.IncludeNull);
				return;
			}
			writer.WriteByte(4);
			writer.WriteUnicodeString(value, StringFlags.IncludeNull);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00039980 File Offset: 0x00037B80
		public static void WriteCountedPropertyIds(this Writer writer, PropertyId[] propertyIds)
		{
			if (propertyIds != null)
			{
				writer.WriteUInt16((ushort)propertyIds.Length);
				for (int i = 0; i < propertyIds.Length; i++)
				{
					writer.WriteUInt16((ushort)propertyIds[i]);
				}
				return;
			}
			writer.WriteUInt16(0);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000399BC File Offset: 0x00037BBC
		public static void WriteCountedPropertyProblems(this Writer writer, PropertyProblem[] propertyProblems)
		{
			if (propertyProblems != null)
			{
				writer.WriteUInt16((ushort)propertyProblems.Length);
				for (int i = 0; i < propertyProblems.Length; i++)
				{
					propertyProblems[i].Serialize(writer);
				}
				return;
			}
			writer.WriteUInt16(0);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000399FC File Offset: 0x00037BFC
		public static void WriteSizeAndPropertyRowList(this Writer writer, IList<PropertyRow> propertyRows, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (propertyRows == null)
			{
				propertyRows = Array<PropertyRow>.Empty;
			}
			writer.WriteCountOrSize(propertyRows.Count, wireFormatStyle);
			foreach (PropertyRow propertyRow in propertyRows)
			{
				propertyRow.Serialize(writer, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00039A60 File Offset: 0x00037C60
		public static void WriteSizedModifyTableRows(this Writer writer, ICollection<ModifyTableRow> elements, Encoding string8Encoding)
		{
			if (elements == null)
			{
				writer.WriteInt16(0);
				return;
			}
			writer.WriteInt16((short)elements.Count);
			foreach (ModifyTableRow modifyTableRow in elements)
			{
				modifyTableRow.Serialize(writer, string8Encoding);
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00039AC4 File Offset: 0x00037CC4
		public static void WriteCountAndPropertyValueList(this Writer writer, PropertyValue[] propertyValues, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (propertyValues == null)
			{
				propertyValues = Array<PropertyValue>.Empty;
			}
			writer.WriteCountOrSize(propertyValues.Length, wireFormatStyle);
			for (int i = 0; i < propertyValues.Length; i++)
			{
				writer.WritePropertyValue(propertyValues[i], string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00039B08 File Offset: 0x00037D08
		public static void WriteCountAndPropertyValueListList(this Writer writer, PropertyValue[][] propertyValues, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (propertyValues == null)
			{
				propertyValues = Array<PropertyValue[]>.Empty;
			}
			writer.WriteCountOrSize(propertyValues.Length, wireFormatStyle);
			for (int i = 0; i < propertyValues.Length; i++)
			{
				writer.WriteCountAndPropertyValueList(propertyValues[i], string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00039B44 File Offset: 0x00037D44
		public static void WriteSizeAndIntegerArray(this Writer writer, int[] integers, FieldLength fieldLength)
		{
			uint num = 0U;
			if (integers != null)
			{
				num = (uint)integers.Length;
			}
			if (fieldLength == FieldLength.WordSize)
			{
				writer.WriteUInt16((ushort)num);
			}
			else
			{
				writer.WriteUInt32(num);
			}
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				writer.WriteInt32(integers[num2]);
				num2++;
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00039B88 File Offset: 0x00037D88
		public static void WriteSizedRuleAction(this Writer writer, RuleAction action, Encoding string8Encoding)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			long position = writer.Position;
			writer.WriteUInt16(0);
			action.Serialize(writer, string8Encoding);
			writer.UpdateSize(position, writer.Position);
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00039BC8 File Offset: 0x00037DC8
		public static void WriteSizedRuleActions(this Writer writer, RuleAction[] actions, Encoding string8Encoding)
		{
			writer.WriteUInt16((ushort)actions.Length);
			for (int i = 0; i < actions.Length; i++)
			{
				writer.WriteSizedRuleAction(actions[i], string8Encoding);
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00039BF8 File Offset: 0x00037DF8
		public static uint WriteSizedBlock(this Writer writer, Action writeMethod)
		{
			long position = writer.Position;
			writer.WriteUInt32(0U);
			writeMethod();
			uint num = (uint)(writer.Position - position - 4L);
			long position2 = writer.Position;
			uint result;
			try
			{
				writer.Position = position;
				writer.WriteUInt32(num);
				result = num;
			}
			finally
			{
				writer.Position = position2;
			}
			return result;
		}
	}
}
