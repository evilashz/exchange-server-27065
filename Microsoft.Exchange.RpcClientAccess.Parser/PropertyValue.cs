using System;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020001FC RID: 508
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct PropertyValue : IFormattable
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0002292C File Offset: 0x00020B2C
		public bool IsError
		{
			get
			{
				return this.propertyTag.PropertyType == PropertyType.Error;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002294B File Offset: 0x00020B4B
		public bool IsNullValue
		{
			get
			{
				return !this.IsError && this.unionVal.Value == null;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00022965 File Offset: 0x00020B65
		public bool IsNotFound
		{
			get
			{
				return this.IsError && this.unionVal.ErrorCode == (ErrorCode)2147746063U;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00022983 File Offset: 0x00020B83
		public PropertyTag PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002298B File Offset: 0x00020B8B
		internal ErrorCode ErrorCodeValue
		{
			get
			{
				if (!this.IsError)
				{
					throw new InvalidOperationException("Property is not an error type");
				}
				return this.unionVal.ErrorCode;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000229B8 File Offset: 0x00020BB8
		public object Value
		{
			get
			{
				if (this.IsError)
				{
					return this.unionVal.ErrorCode;
				}
				String8 @string = this.unionVal.Value as String8;
				if (@string != null)
				{
					return @string.StringValue;
				}
				String8[] array = this.unionVal.Value as String8[];
				if (array != null)
				{
					return array.Select(delegate(String8 x)
					{
						if (x == null)
						{
							return null;
						}
						return x.StringValue;
					}).ToArray<string>();
				}
				return this.unionVal.Value;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00022A41 File Offset: 0x00020C41
		public bool CanGetValue<T>()
		{
			return !this.IsError && this.Value is T;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00022A5B File Offset: 0x00020C5B
		public T GetServerValue<T>()
		{
			if (this.CanGetValue<T>())
			{
				return (T)((object)this.Value);
			}
			throw new RopExecutionException(string.Format("RpcClientAccess expects a value convertible to {0}, but the Store returned {1} instead", typeof(T), this), (ErrorCode)2147746075U);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00022A9A File Offset: 0x00020C9A
		public T GetValue<T>()
		{
			if (this.CanGetValue<T>())
			{
				return (T)((object)this.Value);
			}
			throw new UnexpectedPropertyTypeException(typeof(T), this);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00022AC8 File Offset: 0x00020CC8
		public T GetValueAssert<T>()
		{
			if (!this.CanGetValue<T>())
			{
				ExAssert.RetailAssert(false, "Unable to cast value to {0}", new object[]
				{
					typeof(T)
				});
			}
			return (T)((object)this.Value);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00022B08 File Offset: 0x00020D08
		public PropertyValue(PropertyTag propertyTag, object value)
		{
			this = new PropertyValue(propertyTag, value, false);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00022B20 File Offset: 0x00020D20
		private PropertyValue(PropertyTag propertyTag, object value, bool allowNullValue)
		{
			if (!allowNullValue && propertyTag.PropertyType != PropertyType.Null && value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.propertyTag = propertyTag;
			this.unionVal.ErrorCode = ErrorCode.None;
			this.unionVal.Value = value;
			PropertyType propertyType = propertyTag.PropertyType;
			if (propertyType <= PropertyType.Binary)
			{
				if (propertyType <= PropertyType.SysTime)
				{
					switch (propertyType)
					{
					case PropertyType.Unspecified:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)12:
					case (PropertyType)14:
					case (PropertyType)15:
					case (PropertyType)16:
					case (PropertyType)17:
					case (PropertyType)18:
					case (PropertyType)19:
						break;
					case PropertyType.Null:
						this.CheckValueTypeOnConstruction(value == null);
						return;
					case PropertyType.Int16:
						this.CheckValueTypeOnConstruction(value is short);
						return;
					case PropertyType.Int32:
						this.CheckValueTypeOnConstruction(value is int);
						return;
					case PropertyType.Float:
						this.CheckValueTypeOnConstruction(value is float);
						return;
					case PropertyType.Double:
					case PropertyType.AppTime:
						this.CheckValueTypeOnConstruction(value is double);
						return;
					case PropertyType.Currency:
					case PropertyType.Int64:
						this.CheckValueTypeOnConstruction(value is long);
						return;
					case PropertyType.Error:
						this.CheckValueTypeOnConstruction(value is ErrorCode || value is uint);
						this.unionVal.Value = null;
						this.unionVal.ErrorCode = (ErrorCode)value;
						return;
					case PropertyType.Bool:
						this.CheckValueTypeOnConstruction(value is bool);
						return;
					case PropertyType.Object:
						return;
					default:
						switch (propertyType)
						{
						case PropertyType.String8:
						{
							this.CheckValueTypeOnConstruction(value is String8 || value is string || (allowNullValue && value == null));
							string text = value as string;
							if (text != null)
							{
								this.unionVal.Value = new String8(text);
								return;
							}
							return;
						}
						case PropertyType.Unicode:
							this.CheckValueTypeOnConstruction(value is string || (allowNullValue && value == null));
							return;
						default:
							if (propertyType == PropertyType.SysTime)
							{
								this.CheckValueTypeOnConstruction(value is ExDateTime);
								return;
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (propertyType != PropertyType.Guid)
					{
						switch (propertyType)
						{
						case PropertyType.ServerId:
							break;
						case (PropertyType)252:
							goto IL_455;
						case PropertyType.Restriction:
							this.CheckValueTypeOnConstruction(value is Restriction || (allowNullValue && value == null));
							return;
						case PropertyType.Actions:
							this.CheckValueTypeOnConstruction(value is RuleAction[] || (allowNullValue && value == null));
							return;
						default:
							if (propertyType != PropertyType.Binary)
							{
								goto IL_455;
							}
							break;
						}
						this.CheckValueTypeOnConstruction(value is byte[] || (allowNullValue && value == null));
						return;
					}
					this.CheckValueTypeOnConstruction(value is Guid || (allowNullValue && value == null));
					return;
				}
			}
			else
			{
				if (propertyType <= PropertyType.MultiValueUnicode)
				{
					switch (propertyType)
					{
					case PropertyType.MultiValueInt16:
						this.CheckValueTypeOnConstruction(value is short[] || (allowNullValue && value == null));
						return;
					case PropertyType.MultiValueInt32:
						this.CheckValueTypeOnConstruction(value is int[] || (allowNullValue && value == null));
						return;
					case PropertyType.MultiValueFloat:
						this.CheckValueTypeOnConstruction(value is float[] || (allowNullValue && value == null));
						return;
					case PropertyType.MultiValueDouble:
					case PropertyType.MultiValueAppTime:
						this.CheckValueTypeOnConstruction(value is double[] || (allowNullValue && value == null));
						return;
					case PropertyType.MultiValueCurrency:
						break;
					default:
						if (propertyType != PropertyType.MultiValueInt64)
						{
							switch (propertyType)
							{
							case PropertyType.MultiValueString8:
							{
								this.CheckValueTypeOnConstruction(value is String8[] || value is string[] || (allowNullValue && value == null));
								string[] array = value as string[];
								if (array != null)
								{
									this.unionVal.Value = array.Select(delegate(string x)
									{
										if (x == null)
										{
											return null;
										}
										return new String8(x);
									}).ToArray<String8>();
									return;
								}
								return;
							}
							case PropertyType.MultiValueUnicode:
								this.CheckValueTypeOnConstruction(value is string[] || (allowNullValue && value == null));
								return;
							default:
								goto IL_455;
							}
						}
						break;
					}
					this.CheckValueTypeOnConstruction(value is long[] || (allowNullValue && value == null));
					return;
				}
				if (propertyType == PropertyType.MultiValueSysTime)
				{
					this.CheckValueTypeOnConstruction(value is ExDateTime[] || (allowNullValue && value == null));
					return;
				}
				if (propertyType == PropertyType.MultiValueGuid)
				{
					this.CheckValueTypeOnConstruction(value is Guid[] || (allowNullValue && value == null));
					return;
				}
				if (propertyType == PropertyType.MultiValueBinary)
				{
					this.CheckValueTypeOnConstruction(value is byte[][] || (allowNullValue && value == null));
					return;
				}
			}
			IL_455:
			throw new NotSupportedException(string.Format("Property type not supported: {0}.", propertyTag.PropertyType));
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00022F9E File Offset: 0x0002119E
		private PropertyValue(PropertyId propertyId, ErrorCode value)
		{
			this.propertyTag = new PropertyTag(propertyId, PropertyType.Error);
			this.unionVal.Value = null;
			this.unionVal.ErrorCode = value;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00022FC6 File Offset: 0x000211C6
		public PropertyValue CloneAs(PropertyTag propertyTag)
		{
			if (this.IsNullValue)
			{
				return PropertyValue.NullValue(propertyTag);
			}
			return new PropertyValue(propertyTag, this.Value);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00022FE4 File Offset: 0x000211E4
		public PropertyValue CloneAs(PropertyId propertyId)
		{
			return this.CloneAs(new PropertyTag(propertyId, this.PropertyTag.PropertyType));
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002300B File Offset: 0x0002120B
		public static PropertyValue Error(PropertyId propertyId, ErrorCode errorCode)
		{
			return new PropertyValue(propertyId, errorCode);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00023014 File Offset: 0x00021214
		public static PropertyValue NullValue(PropertyTag propertyTag)
		{
			return new PropertyValue(propertyTag, null, true);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002301E File Offset: 0x0002121E
		internal static PropertyValue CreateNotEnoughMemory(PropertyId propertyId)
		{
			return new PropertyValue(PropertyTag.CreateError(propertyId), (ErrorCode)2147942414U);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00023035 File Offset: 0x00021235
		internal static PropertyValue CreateNotFound(PropertyId propertyId)
		{
			return new PropertyValue(PropertyTag.CreateError(propertyId), (ErrorCode)2147746063U);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002304C File Offset: 0x0002124C
		internal static bool IsSupportedPropertyType(PropertyTag propertyTag)
		{
			return EnumValidator.IsValidValue<PropertyType>(propertyTag.PropertyType);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002305A File Offset: 0x0002125A
		public override string ToString()
		{
			return this.ToString("B", null);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00023068 File Offset: 0x00021268
		internal void AppendToString(StringBuilder stringBuilder)
		{
			this.AppendToString(stringBuilder, "G", null);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00023078 File Offset: 0x00021278
		internal void AppendToString(StringBuilder stringBuilder, string format, IFormatProvider fp)
		{
			if (format != null)
			{
				if (!(format == "G"))
				{
					if (!(format == "B"))
					{
					}
				}
				else
				{
					stringBuilder.Append("Tag=").Append(this.propertyTag.ToString());
					if (this.IsError)
					{
						stringBuilder.Append(" Error=");
						stringBuilder.Append(this.unionVal.ErrorCode.ToString());
						return;
					}
					stringBuilder.Append(" Value=");
					Util.AppendToString(stringBuilder, this.unionVal.Value);
					return;
				}
			}
			stringBuilder.AppendFormat("[{0} : ", this.PropertyTag);
			if (this.IsError)
			{
				stringBuilder.Append(this.unionVal.ErrorCode.ToString());
			}
			else
			{
				Util.AppendToString(stringBuilder, this.unionVal.Value);
			}
			stringBuilder.Append("]");
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00023178 File Offset: 0x00021378
		internal void ResolveString8Values(Encoding string8Encoding)
		{
			if (!this.IsNullValue)
			{
				if (this.propertyTag.PropertyType == PropertyType.String8)
				{
					((String8)this.unionVal.Value).ResolveString8Values(string8Encoding);
					return;
				}
				if (this.propertyTag.PropertyType == PropertyType.MultiValueString8)
				{
					foreach (String8 @string in (String8[])this.unionVal.Value)
					{
						if (@string != null)
						{
							@string.ResolveString8Values(string8Encoding);
						}
					}
					return;
				}
				if (this.propertyTag.PropertyType == PropertyType.Restriction)
				{
					((Restriction)this.unionVal.Value).ResolveString8Values(string8Encoding);
					return;
				}
				if (this.propertyTag.PropertyType == PropertyType.Actions)
				{
					foreach (RuleAction ruleAction in (RuleAction[])this.unionVal.Value)
					{
						ruleAction.ResolveString8Values(string8Encoding);
					}
				}
			}
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002327C File Offset: 0x0002147C
		public string ToString(string format, IFormatProvider fp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.AppendToString(stringBuilder, format, fp);
			return stringBuilder.ToString();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000232A0 File Offset: 0x000214A0
		internal static ExDateTime ExDateTimeFromFileTimeUtc(long fileTimeAsInt64)
		{
			if (fileTimeAsInt64 == 0L)
			{
				return ExDateTime.MinValue;
			}
			if (fileTimeAsInt64 < 0L || fileTimeAsInt64 >= PropertyValue.ExDateTimeUtcMaxValueAsFileTime)
			{
				return ExDateTime.MaxValue;
			}
			ExDateTime result;
			try
			{
				result = ExDateTime.FromFileTimeUtc(fileTimeAsInt64);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = ExDateTime.MaxValue;
			}
			return result;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000232F0 File Offset: 0x000214F0
		internal static long ExDateTimeToFileTimeUtc(ExDateTime dateTime)
		{
			if (dateTime == ExDateTime.MaxValue)
			{
				return long.MaxValue;
			}
			if (dateTime <= PropertyValue.FileTimeMinValueAsExDateTimeUtc)
			{
				return 0L;
			}
			long result;
			try
			{
				result = dateTime.ToFileTimeUtc();
			}
			catch (ArgumentOutOfRangeException)
			{
				result = 0L;
			}
			return result;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00023348 File Offset: 0x00021548
		private void CheckValueTypeOnConstruction(bool isTypeAcceptable)
		{
			if (!isTypeAcceptable)
			{
				throw new InvalidPropertyValueTypeException(string.Format("Value for property {0} of type {1} is of incorrect CLR type {2}", this.propertyTag, this.propertyTag.PropertyType, (this.unionVal.Value != null) ? this.unionVal.Value.GetType().Name : "(null)"), "value");
			}
		}

		// Token: 0x04000645 RID: 1605
		private readonly PropertyTag propertyTag;

		// Token: 0x04000646 RID: 1606
		private readonly PropertyValue.Union unionVal;

		// Token: 0x04000647 RID: 1607
		internal static readonly long ExDateTimeUtcMaxValueAsFileTime = ExDateTime.MaxValue.ToFileTimeUtc();

		// Token: 0x04000648 RID: 1608
		internal static readonly ExDateTime FileTimeMinValueAsExDateTimeUtc = ExDateTime.FromFileTimeUtc(0L);

		// Token: 0x020001FD RID: 509
		private struct Union
		{
			// Token: 0x0400064B RID: 1611
			public object Value;

			// Token: 0x0400064C RID: 1612
			public ErrorCode ErrorCode;
		}
	}
}
