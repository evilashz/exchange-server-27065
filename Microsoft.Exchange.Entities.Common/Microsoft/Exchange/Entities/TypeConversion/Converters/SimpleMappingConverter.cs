using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x02000062 RID: 98
	public struct SimpleMappingConverter<TLeft, TRight> : IConverter<TLeft, TRight>
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00007A60 File Offset: 0x00005C60
		private SimpleMappingConverter(ICollection<Tuple<TLeft, TRight>> mappings, SimpleMappingConverter<TLeft, TRight>.MappingBehavior behavior = SimpleMappingConverter<TLeft, TRight>.MappingBehavior.None, TRight defaultLeftToRight = default(TRight), TLeft defaultRightToLeft = default(TLeft))
		{
			this.defaultLeftToRight = defaultLeftToRight;
			this.defaultRightToLeft = defaultRightToLeft;
			int count = mappings.Count;
			this.forwardMappingDictionary = new Dictionary<TLeft, TRight>(count);
			this.backwardMappingDictionary = new Dictionary<TRight, TLeft>(count);
			foreach (Tuple<TLeft, TRight> tuple in mappings)
			{
				SimpleMappingConverter<TLeft, TRight>.AddMapping<TLeft, TRight>(tuple.Item1, tuple.Item2, this.forwardMappingDictionary);
				SimpleMappingConverter<TLeft, TRight>.AddMapping<TRight, TLeft>(tuple.Item2, tuple.Item1, this.backwardMappingDictionary);
			}
			this.throwOnNullLeftValue = SimpleMappingConverter<TLeft, TRight>.CheckBehavior(behavior, SimpleMappingConverter<TLeft, TRight>.MappingBehavior.ThrowOnNullLeftValue);
			this.throwOnNullRightValue = SimpleMappingConverter<TLeft, TRight>.CheckBehavior(behavior, SimpleMappingConverter<TLeft, TRight>.MappingBehavior.ThrowOnNullRightValue);
			this.throwOnMissingLeftToRightMapping = SimpleMappingConverter<TLeft, TRight>.CheckBehavior(behavior, SimpleMappingConverter<TLeft, TRight>.MappingBehavior.ThrowOnMissingLeftToRightMapping);
			this.throwOnMissingRightToLeftMapping = SimpleMappingConverter<TLeft, TRight>.CheckBehavior(behavior, SimpleMappingConverter<TLeft, TRight>.MappingBehavior.ThrowOnMissingRightToLeftMapping);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007B34 File Offset: 0x00005D34
		public ICollection<TLeft> LeftKeyCollection
		{
			get
			{
				return this.forwardMappingDictionary.Keys;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007B41 File Offset: 0x00005D41
		public ICollection<TRight> RightKeyCollection
		{
			get
			{
				return this.backwardMappingDictionary.Keys;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007B50 File Offset: 0x00005D50
		public static SimpleMappingConverter<TLeft, TRight> CreateStrictConverter(ICollection<Tuple<TLeft, TRight>> mappings)
		{
			return new SimpleMappingConverter<TLeft, TRight>(mappings, SimpleMappingConverter<TLeft, TRight>.MappingBehavior.Strict, default(TRight), default(TLeft));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007B78 File Offset: 0x00005D78
		public static SimpleMappingConverter<TLeft, TRight> CreateRelaxedConverter(ICollection<Tuple<TLeft, TRight>> mappings)
		{
			return new SimpleMappingConverter<TLeft, TRight>(mappings, SimpleMappingConverter<TLeft, TRight>.MappingBehavior.None, default(TRight), default(TLeft));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007B9E File Offset: 0x00005D9E
		public TRight Convert(TLeft value)
		{
			return SimpleMappingConverter<TLeft, TRight>.Convert<TLeft, TRight>(value, this.forwardMappingDictionary, this.throwOnNullLeftValue, this.throwOnMissingLeftToRightMapping, this.defaultLeftToRight);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007BBE File Offset: 0x00005DBE
		public TLeft Reverse(TRight value)
		{
			return SimpleMappingConverter<TLeft, TRight>.Convert<TRight, TLeft>(value, this.backwardMappingDictionary, this.throwOnNullRightValue, this.throwOnMissingRightToLeftMapping, this.defaultRightToLeft);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007BDE File Offset: 0x00005DDE
		private static void AddMapping<TKey, TValue>(TKey key, TValue value, IDictionary<TKey, TValue> mappingTable)
		{
			if (mappingTable.ContainsKey(key))
			{
				throw new ArgumentException(string.Format("Mapping duplicate: {0}", key));
			}
			mappingTable.Add(key, value);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007C08 File Offset: 0x00005E08
		private static TDestination Convert<TSource, TDestination>(TSource value, IReadOnlyDictionary<TSource, TDestination> mappingDictionary, bool throwOnNullValue, bool throwOnMissingMapping, TDestination defaultDestination)
		{
			if (value == null)
			{
				if (throwOnNullValue)
				{
					throw new ExArgumentNullException("value");
				}
			}
			else
			{
				TDestination result;
				if (mappingDictionary.TryGetValue(value, out result))
				{
					return result;
				}
				if (throwOnMissingMapping)
				{
					throw new ExArgumentOutOfRangeException("value", value, "There's no mapping provided for the specified value.");
				}
			}
			return defaultDestination;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007C53 File Offset: 0x00005E53
		private static bool CheckBehavior(SimpleMappingConverter<TLeft, TRight>.MappingBehavior behavior, SimpleMappingConverter<TLeft, TRight>.MappingBehavior valueToCheck)
		{
			return (behavior & valueToCheck) == valueToCheck;
		}

		// Token: 0x040000A9 RID: 169
		private readonly Dictionary<TRight, TLeft> backwardMappingDictionary;

		// Token: 0x040000AA RID: 170
		private readonly TRight defaultLeftToRight;

		// Token: 0x040000AB RID: 171
		private readonly TLeft defaultRightToLeft;

		// Token: 0x040000AC RID: 172
		private readonly Dictionary<TLeft, TRight> forwardMappingDictionary;

		// Token: 0x040000AD RID: 173
		private readonly bool throwOnMissingLeftToRightMapping;

		// Token: 0x040000AE RID: 174
		private readonly bool throwOnMissingRightToLeftMapping;

		// Token: 0x040000AF RID: 175
		private readonly bool throwOnNullLeftValue;

		// Token: 0x040000B0 RID: 176
		private readonly bool throwOnNullRightValue;

		// Token: 0x02000063 RID: 99
		[Flags]
		public enum MappingBehavior
		{
			// Token: 0x040000B2 RID: 178
			None = 0,
			// Token: 0x040000B3 RID: 179
			ThrowOnNullLeftValue = 1,
			// Token: 0x040000B4 RID: 180
			ThrowOnNullRightValue = 2,
			// Token: 0x040000B5 RID: 181
			ThrowOnNullValue = 3,
			// Token: 0x040000B6 RID: 182
			ThrowOnMissingLeftToRightMapping = 4,
			// Token: 0x040000B7 RID: 183
			ThrowOnMissingRightToLeftMapping = 8,
			// Token: 0x040000B8 RID: 184
			ThrowOnMissingAnyMapping = 12,
			// Token: 0x040000B9 RID: 185
			Strict = 15
		}
	}
}
