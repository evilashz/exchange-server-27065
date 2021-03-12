using System;
using System.Collections;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000202 RID: 514
	internal abstract class Restriction
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00023CC8 File Offset: 0x00021EC8
		public static Restriction Parse(Reader reader, WireFormatStyle wireFormatStyle)
		{
			return Restriction.InternalParse(reader, wireFormatStyle, 0U);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00023CD4 File Offset: 0x00021ED4
		internal static Restriction InternalParse(Reader reader, WireFormatStyle wireFormatStyle, uint depth)
		{
			if (depth++ >= 256U)
			{
				throw new BufferParseException("Restriction structure exceeds maximal depth.");
			}
			RestrictionType restrictionType = (RestrictionType)reader.ReadByte();
			RestrictionType restrictionType2 = restrictionType;
			switch (restrictionType2)
			{
			case RestrictionType.And:
				return AndRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Or:
				return OrRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Not:
				return NotRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Content:
				return ContentRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Property:
				return PropertyRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.CompareProps:
				return ComparePropsRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.BitMask:
				return BitMaskRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Size:
				return SizeRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Exists:
				return ExistsRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.SubRestriction:
				return SubRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Comment:
				return CommentRestriction.InternalParse(reader, wireFormatStyle, depth);
			case RestrictionType.Count:
				return CountRestriction.InternalParse(reader, wireFormatStyle, depth);
			case (RestrictionType)12U:
				break;
			case RestrictionType.Near:
				return NearRestriction.InternalParse(reader, wireFormatStyle, depth);
			default:
				switch (restrictionType2)
				{
				case RestrictionType.True:
					return TrueRestriction.InternalParse(reader, wireFormatStyle, depth);
				case RestrictionType.False:
					return FalseRestriction.InternalParse(reader, wireFormatStyle, depth);
				default:
					if (restrictionType2 == RestrictionType.Null)
					{
						return NullRestriction.InternalParse(reader, wireFormatStyle, depth);
					}
					break;
				}
				break;
			}
			throw new BufferParseException(string.Format("The restriction type is not supported. Type = {0}.", restrictionType));
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00023E0C File Offset: 0x0002200C
		internal static PropertyValue? ReadNullablePropertyValue(Reader reader, WireFormatStyle wireFormatStyle)
		{
			if (wireFormatStyle == WireFormatStyle.Nspi && !reader.ReadBool())
			{
				return null;
			}
			return new PropertyValue?(reader.ReadPropertyValue(wireFormatStyle));
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00023E3B File Offset: 0x0002203B
		internal static void WriteNullablePropertyValue(Writer writer, PropertyValue? propertyValue, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (wireFormatStyle == WireFormatStyle.Nspi)
			{
				writer.WriteNullablePropertyValue(propertyValue, string8Encoding, wireFormatStyle);
				return;
			}
			writer.WritePropertyValue(propertyValue.Value, string8Encoding, wireFormatStyle);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00023E5A File Offset: 0x0002205A
		public virtual void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			writer.WriteByte((byte)this.RestrictionType);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B29 RID: 2857
		internal abstract RestrictionType RestrictionType { get; }

		// Token: 0x06000B2A RID: 2858 RVA: 0x00023E69 File Offset: 0x00022069
		internal virtual void ResolveString8Values(Encoding string8Encoding)
		{
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00023E6B File Offset: 0x0002206B
		public virtual ErrorCode Validate()
		{
			return ErrorCode.None;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00023E70 File Offset: 0x00022070
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00023E94 File Offset: 0x00022094
		public override bool Equals(object other)
		{
			Restriction restriction = other as Restriction;
			if (restriction == null)
			{
				return false;
			}
			if (this.GetHashCode() != other.GetHashCode())
			{
				return false;
			}
			byte[] x = this.Serialize();
			byte[] y = restriction.Serialize();
			return StructuralComparisons.StructuralEqualityComparer.Equals(x, y);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00023ED8 File Offset: 0x000220D8
		public override int GetHashCode()
		{
			if (this.hashCode == null)
			{
				byte[] obj = this.Serialize();
				this.hashCode = new int?(StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj));
			}
			return this.hashCode.Value;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00023F29 File Offset: 0x00022129
		private byte[] Serialize()
		{
			return BufferWriter.Serialize(delegate(Writer writer)
			{
				this.Serialize(writer, CTSGlobals.AsciiEncoding, WireFormatStyle.Rop);
			});
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00023F3C File Offset: 0x0002213C
		internal virtual void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append(this.RestrictionType);
		}

		// Token: 0x04000667 RID: 1639
		public const int MaximalDepth = 256;

		// Token: 0x04000668 RID: 1640
		private int? hashCode;
	}
}
