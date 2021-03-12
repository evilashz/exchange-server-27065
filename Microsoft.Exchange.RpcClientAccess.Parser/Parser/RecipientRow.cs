using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000084 RID: 132
	internal sealed class RecipientRow
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000C030 File Offset: 0x0000A230
		public RecipientRow(uint recipientRowId, RecipientType recipientType, Encoding string8Encoding, RecipientAddress recipientAddress, RecipientFlags recipientFlags, bool useUnicode, string emailAddress, string displayName, string simpleDisplayName, string transmittableDisplayName, PropertyValue[] extraPropertyValues, PropertyValue[] extraUnicodePropertyValues) : this(recipientRowId, recipientType, new ushort?((ushort)CodePageMap.GetCodePage(string8Encoding)), recipientAddress, recipientFlags, useUnicode, emailAddress, displayName, simpleDisplayName, transmittableDisplayName, extraPropertyValues, extraUnicodePropertyValues)
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C064 File Offset: 0x0000A264
		internal RecipientRow(uint recipientRowId, RecipientType recipientType, ushort? codePageId, RecipientAddress recipientAddress, RecipientFlags recipientFlags, bool useUnicode, string emailAddress, string displayName, string simpleDisplayName, string transmittableDisplayName, PropertyValue[] extraPropertyValues, PropertyValue[] extraUnicodePropertyValues)
		{
			Util.ThrowOnNullArgument(recipientAddress, "recipientAddress");
			Util.ThrowOnNullArgument(extraPropertyValues, "extraPropertyValues");
			Util.ThrowOnNullArgument(extraUnicodePropertyValues, "extraUnicodePropertyValues");
			this.recipientRowId = recipientRowId;
			this.codePageId = codePageId;
			this.recipientType = recipientType;
			this.recipientData = new RecipientRow.RecipientData(recipientAddress, recipientFlags, useUnicode, String8.Create(emailAddress), String8.Create(displayName), String8.Create(simpleDisplayName), String8.Create(transmittableDisplayName), extraPropertyValues, extraUnicodePropertyValues);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C0E1 File Offset: 0x0000A2E1
		internal RecipientRow(uint recipientRowId, RecipientType recipientType)
		{
			this.recipientRowId = recipientRowId;
			this.recipientType = recipientType;
			this.recipientData = null;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C100 File Offset: 0x0000A300
		internal RecipientRow(Reader reader, PropertyTag[] extraPropertyTags, RecipientSerializationFlags serializationFlags)
		{
			ushort num = 0;
			if ((serializationFlags & RecipientSerializationFlags.RecipientRowId) != (RecipientSerializationFlags)0)
			{
				this.recipientRowId = reader.ReadUInt32();
			}
			this.recipientType = (RecipientType)reader.ReadByte();
			if ((serializationFlags & RecipientSerializationFlags.CodePageId) != (RecipientSerializationFlags)0)
			{
				this.codePageId = new ushort?(reader.ReadUInt16());
			}
			if ((serializationFlags & RecipientSerializationFlags.ExtraUnicodeProperties) != (RecipientSerializationFlags)0)
			{
				num = reader.ReadUInt16();
			}
			ushort num2 = reader.ReadUInt16();
			long position = reader.Position;
			if (num2 > 0)
			{
				ushort num3 = reader.ReadUInt16();
				bool useUnicode = (num3 & 512) != 0;
				RecipientFlags recipientFlags = (RecipientFlags)(num3 & 448);
				RecipientAddressType recipientAddressType = (RecipientAddressType)(num3 & 32775);
				RecipientAddress recipientAddress = RecipientAddress.Parse(reader, recipientAddressType);
				String8 emailAddress = null;
				if ((num3 & 8) != 0)
				{
					emailAddress = String8.Parse(reader, useUnicode, StringFlags.IncludeNull);
				}
				String8 displayName = null;
				if ((num3 & 16) != 0)
				{
					displayName = String8.Parse(reader, useUnicode, StringFlags.IncludeNull);
				}
				String8 simpleDisplayName = null;
				if ((num3 & 1024) != 0)
				{
					simpleDisplayName = String8.Parse(reader, useUnicode, StringFlags.IncludeNull);
				}
				String8 transmittableDisplayName = null;
				if ((num3 & 32) != 0)
				{
					transmittableDisplayName = String8.Parse(reader, useUnicode, StringFlags.IncludeNull);
				}
				ushort num4 = reader.ReadUInt16();
				if ((int)num4 > extraPropertyTags.Length)
				{
					string message = string.Format("Recipient expects more extra properties than available: Expected = {0}; Available = {1}", num4, extraPropertyTags.Length);
					throw new BufferParseException(message);
				}
				PropertyTag[] array = new PropertyTag[(int)num4];
				Array.ConstrainedCopy(extraPropertyTags, 0, array, 0, (int)num4);
				PropertyValue[] propertyValues = PropertyRow.Parse(reader, array, WireFormatStyle.Rop).PropertyValues;
				List<PropertyValue> list = new List<PropertyValue>((int)num);
				int num5 = 0;
				while (num5 < (int)num && reader.Position < position + (long)((ulong)num2))
				{
					list.Add(reader.ReadPropertyValue(WireFormatStyle.Rop));
					num5++;
				}
				PropertyValue[] extraUnicodePropertyValues = list.ToArray();
				if ((ulong)num2 != (ulong)(reader.Position - position))
				{
					string message2 = string.Format("Did not read entire recipient buffer:  Size={0} Read={1}", num2, reader.Position - position);
					throw new BufferParseException(message2);
				}
				this.recipientData = new RecipientRow.RecipientData(recipientAddress, recipientFlags, useUnicode, emailAddress, displayName, simpleDisplayName, transmittableDisplayName, propertyValues, extraUnicodePropertyValues);
				if (this.String8Encoding != null)
				{
					this.recipientData.ResolveString8Values(this.String8Encoding);
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000C2EE File Offset: 0x0000A4EE
		private RecipientRow.RecipientData Data
		{
			get
			{
				return this.recipientData;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C2F6 File Offset: 0x0000A4F6
		internal bool IsEmpty
		{
			get
			{
				return this.recipientData == null;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C301 File Offset: 0x0000A501
		internal RecipientFlags Flags
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.RecipientFlags;
				}
				return RecipientFlags.None;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000C318 File Offset: 0x0000A518
		internal ushort? CodePageId
		{
			get
			{
				return this.codePageId;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C320 File Offset: 0x0000A520
		internal Encoding String8Encoding
		{
			get
			{
				if (this.codePageId == null)
				{
					return null;
				}
				return CodePageMap.GetEncoding((int)this.codePageId.Value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000C352 File Offset: 0x0000A552
		internal string DisplayName
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.DisplayName;
				}
				return null;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000C369 File Offset: 0x0000A569
		internal bool UseUnicode
		{
			get
			{
				return this.IsEmpty || this.Data.UseUnicode;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C380 File Offset: 0x0000A580
		internal string EmailAddress
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.EmailAddress;
				}
				return null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000C397 File Offset: 0x0000A597
		internal string SimpleDisplayName
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.SimpleDisplayName;
				}
				return null;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000C3AE File Offset: 0x0000A5AE
		internal string TransmittableDisplayName
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.TransmittableDisplayName;
				}
				return null;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		internal RecipientAddress RecipientAddress
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.RecipientAddress;
				}
				return null;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		internal uint RecipientRowId
		{
			get
			{
				return this.recipientRowId;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		internal RecipientType RecipientType
		{
			get
			{
				return this.recipientType;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		internal PropertyValue[] ExtraPropertyValues
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.ExtraPropertyValues;
				}
				return Array<PropertyValue>.Empty;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000C407 File Offset: 0x0000A607
		internal PropertyValue[] ExtraUnicodePropertyValues
		{
			get
			{
				if (!this.IsEmpty)
				{
					return this.Data.ExtraUnicodePropertyValues;
				}
				return Array<PropertyValue>.Empty;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C422 File Offset: 0x0000A622
		internal void ResolveString8Values(Encoding string8Encoding)
		{
			if (this.Data != null)
			{
				this.Data.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000C438 File Offset: 0x0000A638
		private ushort CombinedRecipientPropertyFlags
		{
			get
			{
				ushort num = 0;
				if (!this.IsEmpty)
				{
					num = (ushort)this.Flags;
					num |= (ushort)this.RecipientAddress.RecipientAddressType;
					if (this.UseUnicode)
					{
						num |= 512;
					}
					if (this.EmailAddress != null)
					{
						num |= 8;
					}
					if (this.DisplayName != null)
					{
						num |= 16;
					}
					if (this.SimpleDisplayName != null)
					{
						num |= 1024;
					}
					if (this.TransmittableDisplayName != null)
					{
						num |= 32;
					}
				}
				return num;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C4B1 File Offset: 0x0000A6B1
		internal void Serialize(Writer writer, PropertyTag[] extraPropertyTags, RecipientSerializationFlags serializationFlags)
		{
			if (this.String8Encoding == null)
			{
				throw new InvalidOperationException("Cannot use this method without a code page");
			}
			this.Serialize(writer, extraPropertyTags, serializationFlags, this.String8Encoding);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		internal void Serialize(Writer writer, PropertyTag[] extraPropertyTags, RecipientSerializationFlags serializationFlags, Encoding string8Encoding)
		{
			if ((serializationFlags & RecipientSerializationFlags.RecipientRowId) != (RecipientSerializationFlags)0)
			{
				writer.WriteUInt32(this.recipientRowId);
			}
			writer.WriteByte((byte)this.recipientType);
			if ((serializationFlags & RecipientSerializationFlags.CodePageId) != (RecipientSerializationFlags)0)
			{
				writer.WriteUInt16(this.CodePageId.Value);
			}
			if ((serializationFlags & RecipientSerializationFlags.ExtraUnicodeProperties) != (RecipientSerializationFlags)0)
			{
				writer.WriteUInt16((ushort)this.ExtraUnicodePropertyValues.Length);
			}
			long position = writer.Position;
			writer.WriteUInt16(0);
			long position2 = writer.Position;
			if (!this.IsEmpty)
			{
				writer.WriteUInt16(this.CombinedRecipientPropertyFlags);
				this.RecipientAddress.Serialize(writer);
				RecipientRow.SerializeStringIfNonNull(writer, this.EmailAddress, this.UseUnicode, string8Encoding);
				RecipientRow.SerializeStringIfNonNull(writer, this.DisplayName, this.UseUnicode, string8Encoding);
				RecipientRow.SerializeStringIfNonNull(writer, this.SimpleDisplayName, this.UseUnicode, string8Encoding);
				RecipientRow.SerializeStringIfNonNull(writer, this.TransmittableDisplayName, this.UseUnicode, string8Encoding);
				PropertyTag[] array = new PropertyTag[this.ExtraPropertyValues.Length];
				Array.ConstrainedCopy(extraPropertyTags, 0, array, 0, this.ExtraPropertyValues.Length);
				PropertyRow propertyRow = new PropertyRow(array, this.ExtraPropertyValues);
				writer.WriteUInt16((ushort)this.ExtraPropertyValues.Length);
				propertyRow.Serialize(writer, string8Encoding, WireFormatStyle.Rop);
				if ((serializationFlags & RecipientSerializationFlags.ExtraUnicodeProperties) != (RecipientSerializationFlags)0)
				{
					foreach (PropertyValue value in this.ExtraUnicodePropertyValues)
					{
						writer.WritePropertyValue(value, string8Encoding, WireFormatStyle.Rop);
					}
				}
				long position3 = writer.Position;
				writer.Position = position;
				writer.WriteUInt16((ushort)(position3 - position2));
				writer.Position = position3;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C65C File Offset: 0x0000A85C
		private static void SerializeStringIfNonNull(Writer writer, string stringValue, bool useUnicode, Encoding string8Encoding)
		{
			if (stringValue != null)
			{
				if (useUnicode)
				{
					writer.WriteUnicodeString(stringValue, StringFlags.IncludeNull);
					return;
				}
				writer.WriteString8(stringValue, string8Encoding, StringFlags.IncludeNull);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C678 File Offset: 0x0000A878
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C69C File Offset: 0x0000A89C
		internal void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append("[RowId=").Append(this.RecipientRowId);
			stringBuilder.Append(" Type=").Append(this.RecipientType);
			stringBuilder.Append(" CodePage=").Append(this.CodePageId);
			if (this.Data != null)
			{
				stringBuilder.Append(" DisplayName=[").Append(this.Data.DisplayNameAsString8).Append("]");
			}
			stringBuilder.Append("]");
		}

		// Token: 0x040001BE RID: 446
		private readonly uint recipientRowId;

		// Token: 0x040001BF RID: 447
		private readonly ushort? codePageId;

		// Token: 0x040001C0 RID: 448
		private readonly RecipientType recipientType;

		// Token: 0x040001C1 RID: 449
		private readonly RecipientRow.RecipientData recipientData;

		// Token: 0x02000085 RID: 133
		private class RecipientData
		{
			// Token: 0x06000349 RID: 841 RVA: 0x0000C734 File Offset: 0x0000A934
			internal RecipientData(RecipientAddress recipientAddress, RecipientFlags recipientFlags, bool useUnicode, String8 emailAddress, String8 displayName, String8 simpleDisplayName, String8 transmittableDisplayName, PropertyValue[] extraPropertyValues, PropertyValue[] extraUnicodePropertyValues)
			{
				EnumValidator.ThrowIfInvalid<RecipientFlags>(recipientFlags, "recipientFlags");
				this.recipientAddress = recipientAddress;
				this.recipientFlags = recipientFlags;
				this.useUnicode = useUnicode;
				this.emailAddress = emailAddress;
				this.displayName = displayName;
				this.simpleDisplayName = simpleDisplayName;
				this.transmittableDisplayName = transmittableDisplayName;
				this.extraPropertyValues = extraPropertyValues;
				this.extraUnicodePropertyValues = extraUnicodePropertyValues;
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600034A RID: 842 RVA: 0x0000C797 File Offset: 0x0000A997
			internal RecipientAddress RecipientAddress
			{
				get
				{
					return this.recipientAddress;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600034B RID: 843 RVA: 0x0000C79F File Offset: 0x0000A99F
			internal RecipientFlags RecipientFlags
			{
				get
				{
					return this.recipientFlags;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600034C RID: 844 RVA: 0x0000C7A7 File Offset: 0x0000A9A7
			internal bool UseUnicode
			{
				get
				{
					return this.useUnicode;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x0600034D RID: 845 RVA: 0x0000C7AF File Offset: 0x0000A9AF
			internal string EmailAddress
			{
				get
				{
					if (this.emailAddress != null)
					{
						return this.emailAddress.StringValue;
					}
					return null;
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600034E RID: 846 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
			internal string DisplayName
			{
				get
				{
					if (this.displayName != null)
					{
						return this.displayName.StringValue;
					}
					return null;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C7DD File Offset: 0x0000A9DD
			internal String8 DisplayNameAsString8
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000350 RID: 848 RVA: 0x0000C7E5 File Offset: 0x0000A9E5
			internal string SimpleDisplayName
			{
				get
				{
					if (this.simpleDisplayName != null)
					{
						return this.simpleDisplayName.StringValue;
					}
					return null;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000351 RID: 849 RVA: 0x0000C7FC File Offset: 0x0000A9FC
			internal string TransmittableDisplayName
			{
				get
				{
					if (this.transmittableDisplayName != null)
					{
						return this.transmittableDisplayName.StringValue;
					}
					return null;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000352 RID: 850 RVA: 0x0000C813 File Offset: 0x0000AA13
			internal PropertyValue[] ExtraPropertyValues
			{
				get
				{
					return this.extraPropertyValues;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000353 RID: 851 RVA: 0x0000C81B File Offset: 0x0000AA1B
			internal PropertyValue[] ExtraUnicodePropertyValues
			{
				get
				{
					return this.extraUnicodePropertyValues;
				}
			}

			// Token: 0x06000354 RID: 852 RVA: 0x0000C824 File Offset: 0x0000AA24
			internal void ResolveString8Values(Encoding string8Encoding)
			{
				RecipientRow.RecipientData.ResolveString8IfPresent(this.emailAddress, string8Encoding);
				RecipientRow.RecipientData.ResolveString8IfPresent(this.displayName, string8Encoding);
				RecipientRow.RecipientData.ResolveString8IfPresent(this.simpleDisplayName, string8Encoding);
				RecipientRow.RecipientData.ResolveString8IfPresent(this.transmittableDisplayName, string8Encoding);
				foreach (PropertyValue propertyValue in this.extraPropertyValues)
				{
					propertyValue.ResolveString8Values(string8Encoding);
				}
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0000C88B File Offset: 0x0000AA8B
			private static void ResolveString8IfPresent(String8 string8, Encoding encoding)
			{
				if (string8 != null)
				{
					string8.ResolveString8Values(encoding);
				}
			}

			// Token: 0x040001C2 RID: 450
			private readonly RecipientAddress recipientAddress;

			// Token: 0x040001C3 RID: 451
			private readonly RecipientFlags recipientFlags;

			// Token: 0x040001C4 RID: 452
			private readonly bool useUnicode;

			// Token: 0x040001C5 RID: 453
			private readonly String8 emailAddress;

			// Token: 0x040001C6 RID: 454
			private readonly String8 displayName;

			// Token: 0x040001C7 RID: 455
			private readonly String8 simpleDisplayName;

			// Token: 0x040001C8 RID: 456
			private readonly String8 transmittableDisplayName;

			// Token: 0x040001C9 RID: 457
			private readonly PropertyValue[] extraPropertyValues;

			// Token: 0x040001CA RID: 458
			private readonly PropertyValue[] extraUnicodePropertyValues;
		}
	}
}
