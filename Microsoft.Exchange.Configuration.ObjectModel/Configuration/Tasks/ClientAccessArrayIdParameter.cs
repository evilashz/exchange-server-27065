using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class ClientAccessArrayIdParameter : ADIdParameter
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x0001EBBB File Offset: 0x0001CDBB
		public ClientAccessArrayIdParameter(Fqdn fqdn) : this(fqdn.ToString())
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001EBC9 File Offset: 0x0001CDC9
		public ClientAccessArrayIdParameter(ClientAccessArray clientAccessArray) : this(clientAccessArray.Id)
		{
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001EBD7 File Offset: 0x0001CDD7
		public ClientAccessArrayIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
		public ClientAccessArrayIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001EBE9 File Offset: 0x0001CDE9
		public ClientAccessArrayIdParameter()
		{
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001EBF1 File Offset: 0x0001CDF1
		protected ClientAccessArrayIdParameter(string identity) : base(identity)
		{
			if (base.InternalADObjectId != null)
			{
				return;
			}
			this.fqdn = ServerIdParameter.Parse(identity).Fqdn;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001EC14 File Offset: 0x0001CE14
		public static ClientAccessArrayIdParameter Parse(string identity)
		{
			return new ClientAccessArrayIdParameter(identity);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001EC1C File Offset: 0x0001CE1C
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ClientAccessArray))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (!wrapper.HasElements() && this.fqdn != null)
			{
				QueryFilter filter = new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ClientAccessArraySchema.Fqdn, this.fqdn),
					new ComparisonFilter(ComparisonOperator.Equal, ClientAccessArraySchema.ExchangeLegacyDN, this.fqdn)
				});
				wrapper = EnumerableWrapper<T>.GetWrapper(base.PerformPrimarySearch<T>(filter, rootId, session, true, optionalData));
			}
			return wrapper;
		}

		// Token: 0x04000259 RID: 601
		private string fqdn;
	}
}
