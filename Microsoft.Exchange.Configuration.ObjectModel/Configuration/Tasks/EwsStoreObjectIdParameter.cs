using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class EwsStoreObjectIdParameter : IIdentityParameter
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x000256D8 File Offset: 0x000238D8
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000256F0 File Offset: 0x000238F0
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (!(session is EwsStoreDataProvider))
			{
				throw new NotSupportedException("session: " + session.GetType().FullName);
			}
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				throw new NotSupportedException("Supplying Additional Filters is not currently supported by this IdParameter.");
			}
			T t = (this.ewsStoreObjectId != null) ? ((T)((object)session.Read<T>(this.ewsStoreObjectId))) : ((EwsStoreDataProvider)session).FindByAlternativeId<T>(this.rawIdentity);
			if (t == null)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
				return new T[0];
			}
			notFoundReason = null;
			return new T[]
			{
				t
			};
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000257B0 File Offset: 0x000239B0
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			EwsStoreObjectId ewsStoreObjectId = objectId as EwsStoreObjectId;
			if (ewsStoreObjectId == null)
			{
				throw new NotSupportedException("objectId: " + objectId.GetType().FullName);
			}
			this.ewsStoreObjectId = ewsStoreObjectId;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x000257F7 File Offset: 0x000239F7
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000257FF File Offset: 0x000239FF
		public EwsStoreObjectIdParameter()
		{
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00025807 File Offset: 0x00023A07
		public EwsStoreObjectIdParameter(EwsStoreObject ewsObject) : this(ewsObject.Identity)
		{
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00025815 File Offset: 0x00023A15
		public EwsStoreObjectIdParameter(EwsStoreObjectId storeObjectId)
		{
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			this.rawIdentity = storeObjectId.ToString();
			((IIdentityParameter)this).Initialize(storeObjectId);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00025840 File Offset: 0x00023A40
		public EwsStoreObjectIdParameter(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			this.rawIdentity = id;
			EwsStoreObjectId objectId;
			if (EwsStoreObjectId.TryParse(id, out objectId))
			{
				((IIdentityParameter)this).Initialize(objectId);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002587E File Offset: 0x00023A7E
		public EwsStoreObjectIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.displayName = namedIdentity.DisplayName;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00025898 File Offset: 0x00023A98
		public override string ToString()
		{
			return this.displayName ?? this.rawIdentity;
		}

		// Token: 0x040002B1 RID: 689
		private readonly string displayName;

		// Token: 0x040002B2 RID: 690
		private readonly string rawIdentity;

		// Token: 0x040002B3 RID: 691
		[NonSerialized]
		private EwsStoreObjectId ewsStoreObjectId;
	}
}
