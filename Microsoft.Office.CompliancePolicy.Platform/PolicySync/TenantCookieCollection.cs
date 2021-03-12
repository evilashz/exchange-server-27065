using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x02000117 RID: 279
	[DataContract]
	public sealed class TenantCookieCollection : IEnumerable<TenantCookie>, IEnumerable
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x000176A5 File Offset: 0x000158A5
		public TenantCookieCollection(Workload workload, ConfigurationObjectType objectType)
		{
			this.Workload = workload;
			this.ObjectType = objectType;
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x000176C6 File Offset: 0x000158C6
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x000176CE File Offset: 0x000158CE
		[DataMember]
		public Workload Workload { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x000176D7 File Offset: 0x000158D7
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x000176DF File Offset: 0x000158DF
		[DataMember]
		public ConfigurationObjectType ObjectType { get; private set; }

		// Token: 0x1700021A RID: 538
		public TenantCookie this[Guid tenantId]
		{
			get
			{
				return this.tenantCookies[tenantId];
			}
			set
			{
				this.tenantCookies[tenantId] = value;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00017705 File Offset: 0x00015905
		public bool TryGetCookie(Guid tenantId, out TenantCookie tenantCookie)
		{
			return this.tenantCookies.TryGetValue(tenantId, out tenantCookie);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00017714 File Offset: 0x00015914
		IEnumerator<TenantCookie> IEnumerable<TenantCookie>.GetEnumerator()
		{
			return this.tenantCookies.Values.GetEnumerator();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001772B File Offset: 0x0001592B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TenantCookie>)this).GetEnumerator();
		}

		// Token: 0x04000434 RID: 1076
		[DataMember]
		private Dictionary<Guid, TenantCookie> tenantCookies = new Dictionary<Guid, TenantCookie>();
	}
}
