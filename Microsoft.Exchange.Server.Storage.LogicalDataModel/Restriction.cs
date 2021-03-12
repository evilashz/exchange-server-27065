using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000B5 RID: 181
	public abstract class Restriction
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x00051CD8 File Offset: 0x0004FED8
		internal static void CheckRelationOperator(RelationOperator op)
		{
			switch (op)
			{
			case RelationOperator.LessThan:
			case RelationOperator.LessThanEqual:
			case RelationOperator.GreaterThan:
			case RelationOperator.GreaterThanEqual:
			case RelationOperator.Equal:
			case RelationOperator.NotEqual:
				return;
			default:
				throw new StoreException((LID)61944U, ErrorCodeValue.TooComplex);
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00051D1C File Offset: 0x0004FF1C
		internal static SearchCriteriaCompare.SearchRelOp GetSearchRelOp(RelationOperator op)
		{
			switch (op)
			{
			case RelationOperator.LessThan:
				return SearchCriteriaCompare.SearchRelOp.LessThan;
			case RelationOperator.LessThanEqual:
				return SearchCriteriaCompare.SearchRelOp.LessThanEqual;
			case RelationOperator.GreaterThan:
				return SearchCriteriaCompare.SearchRelOp.GreaterThan;
			case RelationOperator.GreaterThanEqual:
				return SearchCriteriaCompare.SearchRelOp.GreaterThanEqual;
			case RelationOperator.Equal:
				return SearchCriteriaCompare.SearchRelOp.Equal;
			case RelationOperator.NotEqual:
				return SearchCriteriaCompare.SearchRelOp.NotEqual;
			default:
				return SearchCriteriaCompare.SearchRelOp.Equal;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00051D58 File Offset: 0x0004FF58
		public static bool FSamePropType(PropertyType propType1, PropertyType propType2)
		{
			if ((ushort)(propType1 & (PropertyType)12288) == 8192 || (ushort)(propType2 & (PropertyType)12288) == 8192)
			{
				DiagnosticContext.TraceLocation((LID)41960U);
				return false;
			}
			if ((ushort)(propType1 & (PropertyType)12288) == 4096 && (ushort)(propType2 & (PropertyType)12288) == 4096)
			{
				DiagnosticContext.TraceLocation((LID)54248U);
				return false;
			}
			return (ushort)(propType1 & (PropertyType)53247) == (ushort)(propType2 & (PropertyType)53247);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00051DD8 File Offset: 0x0004FFD8
		internal static void SerializeValue(byte[] byteForm, ref int position, StorePropTag propTag, object value)
		{
			PropertyType externalType = propTag.ExternalType;
			if (externalType <= PropertyType.Binary)
			{
				if (externalType <= PropertyType.SysTime)
				{
					switch (externalType)
					{
					case PropertyType.Null:
						return;
					case PropertyType.Int16:
						ParseSerialize.SetWord(byteForm, ref position, (short)value);
						return;
					case PropertyType.Int32:
						ParseSerialize.SetDword(byteForm, ref position, (int)value);
						return;
					case PropertyType.Real32:
						ParseSerialize.SetFloat(byteForm, ref position, (float)value);
						return;
					case PropertyType.Real64:
					case PropertyType.AppTime:
						ParseSerialize.SetDouble(byteForm, ref position, (double)value);
						return;
					case PropertyType.Currency:
					case PropertyType.Int64:
						ParseSerialize.SetQword(byteForm, ref position, (long)value);
						return;
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)12:
					case (PropertyType)14:
					case (PropertyType)15:
					case (PropertyType)16:
					case (PropertyType)17:
					case (PropertyType)18:
					case (PropertyType)19:
						goto IL_255;
					case PropertyType.Error:
						ParseSerialize.SetDword(byteForm, ref position, (int)((ErrorCodeValue)value));
						return;
					case PropertyType.Boolean:
						ParseSerialize.SetBoolean(byteForm, ref position, (bool)value);
						return;
					case PropertyType.Object:
						break;
					default:
						switch (externalType)
						{
						case PropertyType.String8:
							ParseSerialize.SetASCIIString(byteForm, ref position, (string)value);
							return;
						case PropertyType.Unicode:
							ParseSerialize.SetUnicodeString(byteForm, ref position, (string)value);
							return;
						default:
							if (externalType != PropertyType.SysTime)
							{
								goto IL_255;
							}
							ParseSerialize.SetSysTime(byteForm, ref position, (DateTime)value);
							return;
						}
						break;
					}
				}
				else
				{
					if (externalType == PropertyType.Guid)
					{
						ParseSerialize.SetGuid(byteForm, ref position, (Guid)value);
						return;
					}
					switch (externalType)
					{
					case PropertyType.SvrEid:
					case PropertyType.SRestriction:
					case PropertyType.Actions:
						break;
					case (PropertyType)252:
						goto IL_255;
					default:
						if (externalType != PropertyType.Binary)
						{
							goto IL_255;
						}
						break;
					}
				}
				ParseSerialize.SetByteArray(byteForm, ref position, (byte[])value);
				return;
			}
			if (externalType <= PropertyType.MVUnicode)
			{
				switch (externalType)
				{
				case PropertyType.MVInt16:
					ParseSerialize.SetMVInt16(byteForm, ref position, (short[])value);
					return;
				case PropertyType.MVInt32:
					ParseSerialize.SetMVInt32(byteForm, ref position, (int[])value);
					return;
				case PropertyType.MVReal32:
					ParseSerialize.SetMVReal32(byteForm, ref position, (float[])value);
					return;
				case PropertyType.MVReal64:
				case PropertyType.MVAppTime:
					ParseSerialize.SetMVReal64(byteForm, ref position, (double[])value);
					return;
				case PropertyType.MVCurrency:
					break;
				default:
					if (externalType != PropertyType.MVInt64)
					{
						if (externalType != PropertyType.MVUnicode)
						{
							goto IL_255;
						}
						ParseSerialize.SetMVUnicode(byteForm, ref position, (string[])value);
						return;
					}
					break;
				}
				ParseSerialize.SetMVInt64(byteForm, ref position, (long[])value);
				return;
			}
			if (externalType == PropertyType.MVSysTime)
			{
				ParseSerialize.SetMVSystime(byteForm, ref position, (DateTime[])value);
				return;
			}
			if (externalType == PropertyType.MVGuid)
			{
				ParseSerialize.SetMVGuid(byteForm, ref position, (Guid[])value);
				return;
			}
			if (externalType == PropertyType.MVBinary)
			{
				ParseSerialize.SetMVBinary(byteForm, ref position, (byte[][])value);
				return;
			}
			IL_255:
			throw new StoreException((LID)37368U, ErrorCodeValue.TooComplex, "Unsupported propType : " + PropertyTypeHelper.PropertyTypeToString(propTag.PropType));
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00052068 File Offset: 0x00050268
		internal static object DeserializeValue(byte[] byteForm, ref int position, StorePropTag propTag)
		{
			PropertyType externalType = propTag.ExternalType;
			if (externalType <= PropertyType.Binary)
			{
				if (externalType <= PropertyType.SysTime)
				{
					switch (externalType)
					{
					case PropertyType.Null:
						return null;
					case PropertyType.Int16:
						return (short)ParseSerialize.GetWord(byteForm, ref position, byteForm.Length);
					case PropertyType.Int32:
						return (int)ParseSerialize.GetDword(byteForm, ref position, byteForm.Length);
					case PropertyType.Real32:
						return ParseSerialize.GetFloat(byteForm, ref position, byteForm.Length);
					case PropertyType.Real64:
					case PropertyType.AppTime:
						return ParseSerialize.GetDouble(byteForm, ref position, byteForm.Length);
					case PropertyType.Currency:
					case PropertyType.Int64:
						return (long)ParseSerialize.GetQword(byteForm, ref position, byteForm.Length);
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)12:
					case (PropertyType)14:
					case (PropertyType)15:
					case (PropertyType)16:
					case (PropertyType)17:
					case (PropertyType)18:
					case (PropertyType)19:
						goto IL_262;
					case PropertyType.Error:
						return (ErrorCodeValue)ParseSerialize.GetDword(byteForm, ref position, byteForm.Length);
					case PropertyType.Boolean:
						if (!ParseSerialize.GetBoolean(byteForm, ref position, byteForm.Length))
						{
							return SerializedValue.BoxedFalse;
						}
						return SerializedValue.BoxedTrue;
					case PropertyType.Object:
						break;
					default:
						switch (externalType)
						{
						case PropertyType.String8:
							return ParseSerialize.GetStringFromASCII(byteForm, ref position, byteForm.Length);
						case PropertyType.Unicode:
							return ParseSerialize.GetStringFromUnicode(byteForm, ref position, byteForm.Length);
						default:
							if (externalType != PropertyType.SysTime)
							{
								goto IL_262;
							}
							return ParseSerialize.GetSysTime(byteForm, ref position, byteForm.Length);
						}
						break;
					}
				}
				else
				{
					if (externalType == PropertyType.Guid)
					{
						return ParseSerialize.GetGuid(byteForm, ref position, byteForm.Length);
					}
					switch (externalType)
					{
					case PropertyType.SvrEid:
					case PropertyType.SRestriction:
					case PropertyType.Actions:
						break;
					case (PropertyType)252:
						goto IL_262;
					default:
						if (externalType != PropertyType.Binary)
						{
							goto IL_262;
						}
						break;
					}
				}
				return ParseSerialize.GetByteArray(byteForm, ref position, byteForm.Length);
			}
			if (externalType <= PropertyType.MVUnicode)
			{
				switch (externalType)
				{
				case PropertyType.MVInt16:
					return ParseSerialize.GetMVInt16(byteForm, ref position, byteForm.Length);
				case PropertyType.MVInt32:
					return ParseSerialize.GetMVInt32(byteForm, ref position, byteForm.Length);
				case PropertyType.MVReal32:
					return ParseSerialize.GetMVReal32(byteForm, ref position, byteForm.Length);
				case PropertyType.MVReal64:
				case PropertyType.MVAppTime:
					return ParseSerialize.GetMVR8(byteForm, ref position, byteForm.Length);
				case PropertyType.MVCurrency:
					break;
				default:
					if (externalType != PropertyType.MVInt64)
					{
						switch (externalType)
						{
						case PropertyType.MVString8:
							return ParseSerialize.GetMVString8(byteForm, ref position, byteForm.Length);
						case PropertyType.MVUnicode:
							return ParseSerialize.GetMVUnicode(byteForm, ref position, byteForm.Length);
						default:
							goto IL_262;
						}
					}
					break;
				}
				return ParseSerialize.GetMVInt64(byteForm, ref position, byteForm.Length);
			}
			if (externalType == PropertyType.MVSysTime)
			{
				return ParseSerialize.GetMVSysTime(byteForm, ref position, byteForm.Length);
			}
			if (externalType == PropertyType.MVGuid)
			{
				return ParseSerialize.GetMVGuid(byteForm, ref position, byteForm.Length);
			}
			if (externalType == PropertyType.MVBinary)
			{
				return ParseSerialize.GetMVBinary(byteForm, ref position, byteForm.Length);
			}
			IL_262:
			throw new StoreException((LID)53752U, ErrorCodeValue.TooComplex, "Unsupported propType : " + PropertyTypeHelper.PropertyTypeToString(propTag.PropType));
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00052304 File Offset: 0x00050504
		public static Restriction Deserialize(Context context, byte[] byteForm, ref int position, int posMax, Mailbox mailbox, ObjectType objectType)
		{
			Restriction.RestrictionType restrictionType = (Restriction.RestrictionType)ParseSerialize.PeekByte(byteForm, position, posMax);
			Restriction.RestrictionType restrictionType2 = restrictionType;
			switch (restrictionType2)
			{
			case Restriction.RestrictionType.And:
				return new RestrictionAND(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Or:
				return new RestrictionOR(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Not:
				return new RestrictionNOT(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Text:
				return new RestrictionTextProperty(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Property:
				return new RestrictionProperty(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.CompareProps:
				return new RestrictionCompareProps(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Bitmask:
				return new RestrictionBitmask(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Size:
				return new RestrictionSize(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Exists:
				return new RestrictionExists(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Sub:
				return new RestrictionSub(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Comment:
				return new RestrictionComment(context, byteForm, ref position, posMax, mailbox, objectType);
			case Restriction.RestrictionType.Count:
				return new RestrictionCount(context, byteForm, ref position, posMax, mailbox, objectType);
			case (Restriction.RestrictionType)12:
				break;
			case Restriction.RestrictionType.Near:
				return new RestrictionNEAR(context, byteForm, ref position, posMax, mailbox, objectType);
			default:
				switch (restrictionType2)
				{
				case Restriction.RestrictionType.True:
					return new RestrictionTrue(byteForm, ref position, posMax, mailbox, objectType);
				case Restriction.RestrictionType.False:
					return new RestrictionFalse(byteForm, ref position, posMax, mailbox, objectType);
				}
				break;
			}
			throw new StoreException((LID)41464U, ErrorCodeValue.CorruptStore, "Unknown restriction type");
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00052498 File Offset: 0x00050698
		public static Restriction Deserialize(Context context, byte[] byteForm, Mailbox mailbox, ObjectType objectType)
		{
			int num = 0;
			return Restriction.Deserialize(context, byteForm, ref num, byteForm.Length, mailbox, objectType);
		}

		// Token: 0x06000A35 RID: 2613
		public abstract void Serialize(byte[] byteForm, ref int position);

		// Token: 0x06000A36 RID: 2614 RVA: 0x000524B8 File Offset: 0x000506B8
		public byte[] Serialize()
		{
			byte[] array = new byte[this.GetSerializedLength()];
			int num = 0;
			this.Serialize(array, ref num);
			return array;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000524E0 File Offset: 0x000506E0
		public int GetSerializedLength()
		{
			int result = 0;
			this.Serialize(null, ref result);
			return result;
		}

		// Token: 0x06000A38 RID: 2616
		public abstract SearchCriteria ToSearchCriteria(StoreDatabase database, ObjectType objectType);

		// Token: 0x06000A39 RID: 2617
		internal abstract void AppendToString(StringBuilder sb);

		// Token: 0x06000A3A RID: 2618 RVA: 0x000524F9 File Offset: 0x000506F9
		public virtual bool HasClauseMeetingPredicate(Predicate<Restriction> predicate)
		{
			return predicate(this);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00052504 File Offset: 0x00050704
		public Restriction InspectAndFix(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			if (callback != null)
			{
				Restriction restriction = callback(this);
				if (restriction == null)
				{
					return this;
				}
				if (!object.ReferenceEquals(restriction, this))
				{
					return restriction;
				}
			}
			return this.InspectAndFixChildren(callback);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00052533 File Offset: 0x00050733
		protected virtual Restriction InspectAndFixChildren(Restriction.InspectAndFixRestrictionDelegate callback)
		{
			return this;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00052536 File Offset: 0x00050736
		public virtual bool RefersToProperty(StorePropTag propTag)
		{
			return false;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0005253C File Offset: 0x0005073C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x040004CE RID: 1230
		private const byte ColumnType = 0;

		// Token: 0x040004CF RID: 1231
		private const byte ExtendedColumnType = 1;

		// Token: 0x020000B6 RID: 182
		internal enum RestrictionType : byte
		{
			// Token: 0x040004D1 RID: 1233
			And,
			// Token: 0x040004D2 RID: 1234
			Or,
			// Token: 0x040004D3 RID: 1235
			Not,
			// Token: 0x040004D4 RID: 1236
			Text,
			// Token: 0x040004D5 RID: 1237
			Property,
			// Token: 0x040004D6 RID: 1238
			CompareProps,
			// Token: 0x040004D7 RID: 1239
			Bitmask,
			// Token: 0x040004D8 RID: 1240
			Size,
			// Token: 0x040004D9 RID: 1241
			Exists,
			// Token: 0x040004DA RID: 1242
			Sub,
			// Token: 0x040004DB RID: 1243
			Comment,
			// Token: 0x040004DC RID: 1244
			Count,
			// Token: 0x040004DD RID: 1245
			Near = 13,
			// Token: 0x040004DE RID: 1246
			True = 131,
			// Token: 0x040004DF RID: 1247
			False
		}

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x06000A41 RID: 2625
		public delegate Restriction InspectAndFixRestrictionDelegate(Restriction restriction);
	}
}
