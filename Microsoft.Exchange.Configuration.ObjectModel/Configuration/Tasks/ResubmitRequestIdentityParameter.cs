using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public class ResubmitRequestIdentityParameter : IIdentityParameter
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x000244BA File Offset: 0x000226BA
		public ResubmitRequestIdentityParameter()
		{
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000244C2 File Offset: 0x000226C2
		public ResubmitRequestIdentityParameter(ResubmitRequestId identity)
		{
			this.identity = identity;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000244D1 File Offset: 0x000226D1
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			return session.FindPaged<T>(null, rootId, false, null, 0);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000244E0 File Offset: 0x000226E0
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData notUsed2, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = null;
			T[] array = session.FindPaged<T>(null, rootId, false, null, 0).ToArray<T>();
			if (array == null || array.Length == 0)
			{
				notFoundReason = new LocalizedString?(Strings.ResubmitRequestDoesNotExist((this.identity == null) ? 0L : this.identity.ResubmitRequestRowId));
			}
			return array;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00024537 File Offset: 0x00022737
		public void Initialize(ObjectId objectId)
		{
			this.identity = (ResubmitRequestId)objectId;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00024545 File Offset: 0x00022745
		public string RawIdentity
		{
			get
			{
				if (this.identity == null)
				{
					return string.Empty;
				}
				return this.identity.ToString();
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00024560 File Offset: 0x00022760
		public ResubmitRequestId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00024568 File Offset: 0x00022768
		public static ResubmitRequestIdentityParameter Parse(string identity)
		{
			return new ResubmitRequestIdentityParameter(ResubmitRequestId.Parse(identity));
		}

		// Token: 0x040002A0 RID: 672
		private ResubmitRequestId identity;
	}
}
