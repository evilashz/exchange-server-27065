using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public class ADRawEntryIdParameter : ADIdParameter
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x0001E263 File Offset: 0x0001C463
		public ADRawEntryIdParameter()
		{
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001E26B File Offset: 0x0001C46B
		public ADRawEntryIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001E274 File Offset: 0x0001C474
		public ADRawEntryIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001E27D File Offset: 0x0001C47D
		protected ADRawEntryIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001E286 File Offset: 0x0001C486
		public static ADRawEntryIdParameter Parse(string identity)
		{
			return new ADRawEntryIdParameter(identity);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001E28E File Offset: 0x0001C48E
		public static explicit operator string(ADRawEntryIdParameter rawEntryIdParameter)
		{
			if (rawEntryIdParameter != null)
			{
				return rawEntryIdParameter.ToString();
			}
			return null;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001E29C File Offset: 0x0001C49C
		internal override IEnumerable<T> PerformSearch<T>(QueryFilter filter, ADObjectId rootId, IDirectorySession session, bool deepSearch)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (typeof(T) != typeof(ADRawEntry))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return (IEnumerable<T>)session.FindPagedADRawEntry(rootId, deepSearch ? QueryScope.SubTree : QueryScope.OneLevel, filter, null, 0, new PropertyDefinition[]
			{
				ADObjectSchema.Name,
				ADObjectSchema.Id,
				ADObjectSchema.ObjectClass
			});
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001E330 File Offset: 0x0001C530
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (notFoundReason == null && !wrapper.HasElements() && ADRawEntryIdParameter.IsSingleOrDoubleAsterisks(base.RawIdentity))
			{
				notFoundReason = new LocalizedString?(Strings.ErrorNotSupportSingletonWildcard);
			}
			return wrapper;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001E380 File Offset: 0x0001C580
		protected override bool IsWildcardDefined(string name)
		{
			return name != null && !ADRawEntryIdParameter.IsSingleOrDoubleAsterisks(name) && (name.StartsWith("*") || name.EndsWith("*"));
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001E3AB File Offset: 0x0001C5AB
		private static bool IsSingleOrDoubleAsterisks(string str)
		{
			return "*" == str || "**" == str;
		}
	}
}
