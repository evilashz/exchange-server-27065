using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.TextProcessing;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003E RID: 62
	public class Value : Argument
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00006E5D File Offset: 0x0000505D
		private Value(object value, ShortList<string> rawValues) : base(value.GetType())
		{
			this.value = value;
			this.rawValues = rawValues;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006E79 File Offset: 0x00005079
		private Value(object value) : base(value.GetType())
		{
			this.value = value;
			this.rawValues = new ShortList<string>();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006E99 File Offset: 0x00005099
		public Value(string value) : base(typeof(string))
		{
			this.value = value;
			this.rawValues = new ShortList<string>();
			this.rawValues.Add(value);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006EC9 File Offset: 0x000050C9
		internal ShortList<string> RawValues
		{
			get
			{
				return this.rawValues;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006ED1 File Offset: 0x000050D1
		public object ParsedValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006ED9 File Offset: 0x000050D9
		public static Value CreateValue(IMatch parsedObject, IList<string> rawValues)
		{
			return new Value(parsedObject, new ShortList<string>(rawValues));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006EE7 File Offset: 0x000050E7
		internal static Value CreateValue(IMatch parsedObject, ShortList<string> rawValues)
		{
			return new Value(parsedObject, rawValues);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal static Value CreateValue(Type type, ShortList<string> rawValues)
		{
			if (rawValues.Count == 0)
			{
				throw new RulesValidationException(RulesStrings.MissingValue);
			}
			if (rawValues.Count <= 1)
			{
				object obj = Value.ParseSingleValue(type, rawValues[0]);
				return new Value(obj, rawValues);
			}
			if (!Argument.IsStringType(type))
			{
				throw new RulesValidationException(RulesStrings.StringArrayPropertyRequiredForMultiValue);
			}
			List<string> list = new List<string>(rawValues);
			return new Value(list, rawValues);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006F50 File Offset: 0x00005150
		public static Value CreateValue(ShortList<ShortList<KeyValuePair<string, string>>> rawValues)
		{
			if (rawValues.Count == 0)
			{
				throw new RulesValidationException(RulesStrings.MissingValue);
			}
			return new Value(rawValues);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006F6B File Offset: 0x0000516B
		public static Value CreateValue(object value)
		{
			if (value == null)
			{
				throw new RulesValidationException(RulesStrings.MissingValue);
			}
			return new Value(value);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006F84 File Offset: 0x00005184
		internal static object ParseSingleValue(Type type, string unparsedValue)
		{
			object result = null;
			try
			{
				if (type.Equals(typeof(int)))
				{
					result = Convert.ToInt32(unparsedValue);
				}
				else if (type.Equals(typeof(ulong)))
				{
					result = Convert.ToUInt64(unparsedValue);
				}
				else if (Argument.IsStringType(type))
				{
					result = unparsedValue;
				}
				else
				{
					if (!type.Equals(typeof(IPAddress)))
					{
						throw new InvalidOperationException("Invalid Property Type.");
					}
					result = IPAddress.Parse(unparsedValue);
				}
			}
			catch (FormatException)
			{
				throw new RulesValidationException(RulesStrings.InvalidArgumentForType(unparsedValue, type.ToString()));
			}
			catch (OverflowException)
			{
				throw new RulesValidationException(RulesStrings.InvalidArgumentForType(unparsedValue, type.ToString()));
			}
			catch (ArgumentException)
			{
				throw new RulesValidationException(RulesStrings.InvalidArgumentForType(unparsedValue, type.ToString()));
			}
			return result;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000706C File Offset: 0x0000526C
		public override object GetValue(RulesEvaluationContext context)
		{
			return this.value;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007074 File Offset: 0x00005274
		public override int GetEstimatedSize()
		{
			int num = 0;
			if (this.value != null)
			{
				string text = this.value as string;
				if (text != null)
				{
					num += 18;
					num += text.Length * 2;
				}
				else
				{
					ICollection<string> collection = this.value as ICollection<string>;
					if (collection != null)
					{
						num += 18;
						using (IEnumerator<string> enumerator = collection.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text2 = enumerator.Current;
								num += 18;
								num += text2.Length * 2;
							}
							goto IL_7D;
						}
					}
					num += 18;
				}
			}
			IL_7D:
			if (this.rawValues != null)
			{
				num += 18;
				foreach (string text3 in this.rawValues)
				{
					num += text3.Length * 2;
					num += 18;
				}
			}
			return num + base.GetEstimatedSize();
		}

		// Token: 0x040000B3 RID: 179
		private object value;

		// Token: 0x040000B4 RID: 180
		private ShortList<string> rawValues;

		// Token: 0x040000B5 RID: 181
		public static Value Empty = new Value(new object(), new ShortList<string>());
	}
}
