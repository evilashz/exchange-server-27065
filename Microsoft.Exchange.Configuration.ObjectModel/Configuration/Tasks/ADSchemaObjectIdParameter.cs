using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public class ADSchemaObjectIdParameter : ADIdParameter
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0001E3C7 File Offset: 0x0001C5C7
		public ADSchemaObjectIdParameter()
		{
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001E3CF File Offset: 0x0001C5CF
		public ADSchemaObjectIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001E3D8 File Offset: 0x0001C5D8
		public ADSchemaObjectIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001E3E1 File Offset: 0x0001C5E1
		protected ADSchemaObjectIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001E3EA File Offset: 0x0001C5EA
		public static ADSchemaObjectIdParameter Parse(string identity)
		{
			return new ADSchemaObjectIdParameter(identity);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ADSchemaClassObject) && typeof(T) != typeof(ADSchemaAttributeObject))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IEnumerable<T> enumerable = base.GetObjects<T>(session.GetSchemaNamingContext(), session, subTreeSession, optionalData, out notFoundReason);
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(enumerable);
			if (!wrapper.HasElements() && base.InternalADObjectId != null && base.InternalADObjectId.ObjectGuid != Guid.Empty)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADSchemaObjectSchema.SchemaIDGuid, base.InternalADObjectId.ObjectGuid);
				enumerable = base.PerformPrimarySearch<T>(filter, ADSession.GetSchemaNamingContextForLocalForest(), session, true, optionalData);
				wrapper = EnumerableWrapper<T>.GetWrapper(enumerable);
			}
			if (!wrapper.HasElements() && !string.IsNullOrEmpty(base.RawIdentity))
			{
				QueryFilter filter2 = new ComparisonFilter(ComparisonOperator.Equal, ADSchemaObjectSchema.DisplayName, base.RawIdentity);
				enumerable = base.PerformPrimarySearch<T>(filter2, ADSession.GetSchemaNamingContextForLocalForest(), session, true, optionalData);
				wrapper = EnumerableWrapper<T>.GetWrapper(enumerable);
			}
			return wrapper;
		}
	}
}
