using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	public class ServiceInstanceIdParameter : ADIdParameter
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002505F File Offset: 0x0002325F
		public ServiceInstanceIdParameter()
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00025067 File Offset: 0x00023267
		public ServiceInstanceIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00025070 File Offset: 0x00023270
		protected ServiceInstanceIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00025079 File Offset: 0x00023279
		public ServiceInstanceIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00025082 File Offset: 0x00023282
		public ServiceInstanceIdParameter(ServiceInstanceId serviceInstanceId) : base(serviceInstanceId.InstanceId)
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00025090 File Offset: 0x00023290
		public ServiceInstanceIdParameter(SyncServiceInstance syncServiceInstance) : base(syncServiceInstance.Name)
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002509E File Offset: 0x0002329E
		public static ServiceInstanceIdParameter Parse(string identity)
		{
			return new ServiceInstanceIdParameter(identity);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000250A8 File Offset: 0x000232A8
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(SyncServiceInstance))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
