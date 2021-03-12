using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000FA RID: 250
	[Serializable]
	public class ContainerIdParameter : ADIdParameter, IIdentityParameter
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0001F99F File Offset: 0x0001DB9F
		public ContainerIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		public ContainerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001F9B1 File Offset: 0x0001DBB1
		public ContainerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001F9BA File Offset: 0x0001DBBA
		public ContainerIdParameter()
		{
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001F9C2 File Offset: 0x0001DBC2
		public static ContainerIdParameter Parse(string rawString)
		{
			return new ContainerIdParameter(rawString);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (!typeof(Container).IsAssignableFrom(typeof(T)) && !typeof(ADContainer).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (!wrapper.HasElements())
			{
				notFoundReason = null;
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.RawIdentity);
				wrapper = EnumerableWrapper<T>.GetWrapper(base.PerformPrimarySearch<T>(filter, ((IConfigurationSession)session).GetOrgContainerId(), session, false, optionalData));
			}
			return wrapper;
		}
	}
}
