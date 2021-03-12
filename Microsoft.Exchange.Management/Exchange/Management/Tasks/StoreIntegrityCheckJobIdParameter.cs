using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DA2 RID: 3490
	[Serializable]
	public class StoreIntegrityCheckJobIdParameter : IIdentityParameter
	{
		// Token: 0x060085C5 RID: 34245 RVA: 0x002238C7 File Offset: 0x00221AC7
		public StoreIntegrityCheckJobIdParameter()
		{
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x002238CF File Offset: 0x00221ACF
		public StoreIntegrityCheckJobIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.Length == 0)
			{
				throw new ArgumentException("identity");
			}
			this.rawIdentity = identity;
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x002238FF File Offset: 0x00221AFF
		public StoreIntegrityCheckJobIdParameter(StoreIntegrityCheckJobIdentity objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			this.rawIdentity = objectId.ToString();
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x00223921 File Offset: 0x00221B21
		public StoreIntegrityCheckJobIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.Identity;
		}

		// Token: 0x170029A2 RID: 10658
		// (get) Token: 0x060085C9 RID: 34249 RVA: 0x0022393B File Offset: 0x00221B3B
		// (set) Token: 0x060085CA RID: 34250 RVA: 0x00223943 File Offset: 0x00221B43
		public string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
			private set
			{
				this.rawIdentity = value;
			}
		}

		// Token: 0x170029A3 RID: 10659
		// (get) Token: 0x060085CB RID: 34251 RVA: 0x0022394C File Offset: 0x00221B4C
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x060085CC RID: 34252 RVA: 0x00223954 File Offset: 0x00221B54
		public override string ToString()
		{
			return this.RawIdentity;
		}

		// Token: 0x060085CD RID: 34253 RVA: 0x0022395C File Offset: 0x00221B5C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x060085CE RID: 34254 RVA: 0x00223974 File Offset: 0x00221B74
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			notFoundReason = null;
			return session.FindPaged<T>(null, rootId, false, null, 0);
		}

		// Token: 0x060085CF RID: 34255 RVA: 0x0022398C File Offset: 0x00221B8C
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			StoreIntegrityCheckJobIdentity storeIntegrityCheckJobIdentity = objectId as StoreIntegrityCheckJobIdentity;
			if (storeIntegrityCheckJobIdentity == null)
			{
				throw new ArgumentException("objectId");
			}
			this.rawIdentity = storeIntegrityCheckJobIdentity.ToString();
		}

		// Token: 0x040040E7 RID: 16615
		private string rawIdentity;
	}
}
