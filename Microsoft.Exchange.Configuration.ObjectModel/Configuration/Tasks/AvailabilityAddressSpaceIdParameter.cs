using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F1 RID: 241
	[Serializable]
	public class AvailabilityAddressSpaceIdParameter : ADIdParameter
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x0001EA99 File Offset: 0x0001CC99
		public AvailabilityAddressSpaceIdParameter()
		{
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001EAA1 File Offset: 0x0001CCA1
		public AvailabilityAddressSpaceIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001EAAA File Offset: 0x0001CCAA
		public AvailabilityAddressSpaceIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001EAB3 File Offset: 0x0001CCB3
		protected AvailabilityAddressSpaceIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001EABC File Offset: 0x0001CCBC
		public static AvailabilityAddressSpaceIdParameter Parse(string identity)
		{
			return new AvailabilityAddressSpaceIdParameter(identity);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			TaskLogger.LogEnter();
			EnumerableWrapper<T> wrapper;
			try
			{
				if (typeof(T) != typeof(AvailabilityAddressSpace))
				{
					throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
				}
				if (string.IsNullOrEmpty(base.RawIdentity))
				{
					throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
				}
				wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
				if (!wrapper.HasElements())
				{
					notFoundReason = null;
					wrapper = EnumerableWrapper<T>.GetWrapper(base.PerformPrimarySearch<T>(base.CreateWildcardOrEqualFilter(AvailabilityAddressSpaceSchema.ForestName, base.RawIdentity), rootId, session, true, optionalData));
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return wrapper;
		}
	}
}
