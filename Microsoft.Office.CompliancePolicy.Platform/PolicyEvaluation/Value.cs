using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000E4 RID: 228
	public class Value : Argument
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x00012ED0 File Offset: 0x000110D0
		public Value(string value) : base(typeof(string))
		{
			this.value = value;
			this.rawValues = new List<string>();
			this.rawValues.Add(value);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012F00 File Offset: 0x00011100
		public Value(List<string> rawValues) : base(typeof(List<string>))
		{
			this.value = rawValues;
			this.rawValues = rawValues;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00012F20 File Offset: 0x00011120
		private Value(object value, List<string> rawValues) : base(value.GetType())
		{
			this.value = value;
			this.rawValues = rawValues;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00012F3C File Offset: 0x0001113C
		private Value(object value) : base(value.GetType())
		{
			this.value = value;
			this.rawValues = new List<string>();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00012F5C File Offset: 0x0001115C
		public static Value Empty
		{
			get
			{
				return Value.emptyValue;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00012F63 File Offset: 0x00011163
		public object ParsedValue
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00012F6B File Offset: 0x0001116B
		internal List<string> RawValues
		{
			get
			{
				return this.rawValues;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00012F73 File Offset: 0x00011173
		public static Value CreateValue(List<List<KeyValuePair<string, string>>> rawValues)
		{
			if (rawValues.Count == 0)
			{
				throw new CompliancePolicyValidationException("Value is not set");
			}
			return new Value(rawValues);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00012F8E File Offset: 0x0001118E
		public static Value CreateValue(object value)
		{
			if (value == null)
			{
				throw new CompliancePolicyValidationException("Value is not set");
			}
			return new Value(value);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00012FA4 File Offset: 0x000111A4
		public override object GetValue(PolicyEvaluationContext context)
		{
			return this.value;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00012FAC File Offset: 0x000111AC
		internal static Value CreateValue(Type type, List<string> rawValues)
		{
			if (rawValues.Count == 0)
			{
				throw new CompliancePolicyValidationException("Value is not set");
			}
			if (rawValues.Count > 1 || PolicyUtils.IsTypeCollection(type))
			{
				IList list = (IList)Value.ConstructCollection(type);
				foreach (string unparsedValue in rawValues)
				{
					object obj = Value.ParseSingleValue(type, unparsedValue);
					list.Add(obj);
				}
				return new Value(list, rawValues);
			}
			object obj2 = Value.ParseSingleValue(type, rawValues[0]);
			return new Value(obj2, rawValues);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00013054 File Offset: 0x00011254
		internal static object ParseSingleValue(Type type, string unparsedValue)
		{
			object result = null;
			try
			{
				if (type == typeof(int))
				{
					result = Convert.ToInt32(unparsedValue);
				}
				else if (type == typeof(ulong))
				{
					result = Convert.ToUInt64(unparsedValue);
				}
				else if (type == typeof(string) || Argument.IsTypeCollectionOfType(type, typeof(string)))
				{
					result = unparsedValue;
				}
				else if (type == typeof(IPAddress))
				{
					result = IPAddress.Parse(unparsedValue);
				}
				else if (type == typeof(DateTime))
				{
					result = DateTime.Parse(unparsedValue);
				}
				else if (type == typeof(Guid) || Argument.IsTypeCollectionOfType(type, typeof(Guid)))
				{
					result = Guid.Parse(unparsedValue);
				}
				else if (type == typeof(AccessScope))
				{
					result = Enum.Parse(typeof(AccessScope), unparsedValue);
				}
				else if (type == typeof(bool))
				{
					result = bool.Parse(unparsedValue);
				}
				else
				{
					if (!(type == typeof(long)))
					{
						throw new CompliancePolicyValidationException("Invalid Property Type.");
					}
					result = long.Parse(unparsedValue);
				}
			}
			catch (FormatException)
			{
				throw new CompliancePolicyValidationException("Invalid argument type '{0}' for value '{1}'", new object[]
				{
					type.ToString(),
					unparsedValue
				});
			}
			catch (OverflowException)
			{
				throw new CompliancePolicyValidationException("Invalid argument type '{0}' for value '{1}'", new object[]
				{
					type.ToString(),
					unparsedValue
				});
			}
			catch (ArgumentException)
			{
				throw new CompliancePolicyValidationException("Invalid argument type '{0}' for value '{1}'", new object[]
				{
					type.ToString(),
					unparsedValue
				});
			}
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001326C File Offset: 0x0001146C
		internal static object ConstructCollection(Type type)
		{
			if (type == typeof(int))
			{
				return new List<int>();
			}
			if (type == typeof(ulong))
			{
				return new List<ulong>();
			}
			if (type == typeof(string) || Argument.IsTypeCollectionOfType(type, typeof(string)))
			{
				return new List<string>();
			}
			if (type == typeof(IPAddress))
			{
				return new List<IPAddress>();
			}
			if (type == typeof(DateTime))
			{
				return new List<DateTime>();
			}
			if (type == typeof(Guid) || Argument.IsTypeCollectionOfType(type, typeof(Guid)))
			{
				return new List<Guid>();
			}
			if (type == typeof(AccessScope))
			{
				return new List<AccessScope>();
			}
			if (type == typeof(bool))
			{
				return new List<bool>();
			}
			if (type == typeof(long))
			{
				return new List<long>();
			}
			throw new CompliancePolicyValidationException("Invalid Property Type.");
		}

		// Token: 0x0400039F RID: 927
		private const string ValueNotSet = "Value is not set";

		// Token: 0x040003A0 RID: 928
		private const string InvalidArgumentTypeFormat = "Invalid argument type '{0}' for value '{1}'";

		// Token: 0x040003A1 RID: 929
		private static readonly Value emptyValue = new Value(new object(), new List<string>());

		// Token: 0x040003A2 RID: 930
		private readonly object value;

		// Token: 0x040003A3 RID: 931
		private readonly List<string> rawValues;
	}
}
