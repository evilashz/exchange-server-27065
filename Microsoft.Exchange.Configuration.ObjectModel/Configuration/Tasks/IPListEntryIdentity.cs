using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class IPListEntryIdentity : ObjectId, IIdentityParameter
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x00027C49 File Offset: 0x00025E49
		public IPListEntryIdentity(int index)
		{
			this.index = index;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00027C58 File Offset: 0x00025E58
		public IPListEntryIdentity(string index)
		{
			this.index = int.Parse(index);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00027C6C File Offset: 0x00025E6C
		public IPListEntryIdentity(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00027C7A File Offset: 0x00025E7A
		internal int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00027C82 File Offset: 0x00025E82
		public override string ToString()
		{
			return this.index.ToString();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00027C8F File Offset: 0x00025E8F
		public override byte[] GetBytes()
		{
			return BitConverter.GetBytes(this.index);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00027C9C File Offset: 0x00025E9C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00027CAC File Offset: 0x00025EAC
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00027CC4 File Offset: 0x00025EC4
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00027CD0 File Offset: 0x00025ED0
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			notFoundReason = null;
			T t = (T)((object)session.Read<T>(this));
			T[] result;
			if (t != null)
			{
				result = new T[]
				{
					t
				};
			}
			else
			{
				result = new T[0];
			}
			return result;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00027D2B File Offset: 0x00025F2B
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00027D34 File Offset: 0x00025F34
		internal void Initialize(ObjectId objectId)
		{
			this.index = ((IPListEntryIdentity)objectId).index;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00027D47 File Offset: 0x00025F47
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00027D4F File Offset: 0x00025F4F
		internal string RawIdentity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x040002E4 RID: 740
		private int index;
	}
}
