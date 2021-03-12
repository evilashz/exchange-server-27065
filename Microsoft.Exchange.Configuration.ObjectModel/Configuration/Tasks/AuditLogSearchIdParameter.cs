using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public class AuditLogSearchIdParameter : IIdentityParameter
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x0001E92D File Offset: 0x0001CB2D
		public AuditLogSearchIdParameter()
		{
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001E935 File Offset: 0x0001CB35
		public AuditLogSearchId GetId()
		{
			return this.Identity;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001E940 File Offset: 0x0001CB40
		public AuditLogSearchIdParameter(string identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("Identity");
			}
			Guid requestId;
			if (!Guid.TryParse(identity, out requestId))
			{
				throw new ArgumentException("Identity should be a valid Guid", "Identity");
			}
			this.Identity = new AuditLogSearchId(requestId);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public AuditLogSearchIdParameter(ObjectId id)
		{
			this.Initialize(id);
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001E99B File Offset: 0x0001CB9B
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001E9A3 File Offset: 0x0001CBA3
		internal string RawIdentity
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001E9AB File Offset: 0x0001CBAB
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		public static AuditLogSearchIdParameter Parse(string identity)
		{
			return new AuditLogSearchIdParameter(identity);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return null;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001E9D3 File Offset: 0x0001CBD3
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			return this.GetObjects<T>(rootId, session);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001E9DD File Offset: 0x0001CBDD
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			return this.GetObjects<T>(rootId, session, optionalData, out notFoundReason);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001E9EA File Offset: 0x0001CBEA
		internal virtual void Initialize(ObjectId id)
		{
			this.Identity = (id as AuditLogSearchId);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
			return session.FindPaged<T>((optionalData == null) ? null : optionalData.AdditionalFilter, rootId, false, null, 0);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001EA28 File Offset: 0x0001CC28
		internal IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x04000258 RID: 600
		private AuditLogSearchId Identity;
	}
}
