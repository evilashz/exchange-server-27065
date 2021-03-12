using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mapi;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	public abstract class MapiIdParameter : IIdentityParameter
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002306E File Offset: 0x0002126E
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002307B File Offset: 0x0002127B
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00023088 File Offset: 0x00021288
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000230A0 File Offset: 0x000212A0
		internal virtual IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (!typeof(MapiObject).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			if (!(session is MapiSession))
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("session", typeof(MapiSession).Name), "session");
			}
			notFoundReason = null;
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			if (rootId == null)
			{
				return new List<T>
				{
					(T)((object)session.Read<T>(this.mapiObjectId))
				};
			}
			return session.FindPaged<T>(null, rootId, true, null, 0);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00023168 File Offset: 0x00021368
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00023171 File Offset: 0x00021371
		internal virtual void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (null != this.mapiObjectId)
			{
				throw new InvalidOperationException(Strings.ErrorChangeImmutableType);
			}
			this.MapiObjectId = (objectId as MapiObjectId);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000231AB File Offset: 0x000213AB
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000231B3 File Offset: 0x000213B3
		internal virtual string RawIdentity
		{
			get
			{
				if (!(this.mapiObjectId != null))
				{
					return null;
				}
				return this.MapiObjectId.ToString();
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000231D0 File Offset: 0x000213D0
		public MapiIdParameter()
		{
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000231D8 File Offset: 0x000213D8
		public MapiIdParameter(MapiObjectId mapiObjectId)
		{
			this.Initialize(mapiObjectId);
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000231E7 File Offset: 0x000213E7
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x000231EF File Offset: 0x000213EF
		protected MapiObjectId MapiObjectId
		{
			get
			{
				return this.mapiObjectId;
			}
			set
			{
				if (null == value)
				{
					throw new ArgumentNullException("MapiObjectId");
				}
				this.mapiObjectId = value;
			}
		}

		// Token: 0x0400028A RID: 650
		private MapiObjectId mapiObjectId;
	}
}
