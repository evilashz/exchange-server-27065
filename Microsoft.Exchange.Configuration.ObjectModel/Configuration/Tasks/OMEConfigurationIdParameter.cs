using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	public class OMEConfigurationIdParameter : IIdentityParameter
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00026EB2 File Offset: 0x000250B2
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return "OME Configuration";
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00026EB9 File Offset: 0x000250B9
		public OMEConfigurationIdParameter()
		{
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00026EC1 File Offset: 0x000250C1
		public OMEConfigurationIdParameter(string identity)
		{
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00026ECC File Offset: 0x000250CC
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00026EE4 File Offset: 0x000250E4
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IConfigurable[] array = session.Find<T>(null, null, false, null);
			if (array == null || array.Length == 0)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			T[] array2 = new T[array.Length];
			int num = 0;
			foreach (IConfigurable configurable in array)
			{
				array2[num++] = (T)((object)configurable);
			}
			return array2;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00026F71 File Offset: 0x00025171
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
		}
	}
}
