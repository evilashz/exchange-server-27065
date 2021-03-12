using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class ClientAccessServerOrArrayIdParameter : ServerIdParameter
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001F314 File Offset: 0x0001D514
		public ClientAccessServerOrArrayIdParameter(Fqdn fqdn) : this(fqdn.ToString())
		{
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001F322 File Offset: 0x0001D522
		public ClientAccessServerOrArrayIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001F32B File Offset: 0x0001D52B
		public ClientAccessServerOrArrayIdParameter(ExchangeServer exServer) : base(exServer.Id)
		{
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001F339 File Offset: 0x0001D539
		public ClientAccessServerOrArrayIdParameter(ClientAccessServer caServer) : this(caServer.Id)
		{
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001F347 File Offset: 0x0001D547
		public ClientAccessServerOrArrayIdParameter(ClientAccessArray caArray) : this(caArray.Id)
		{
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001F355 File Offset: 0x0001D555
		public ClientAccessServerOrArrayIdParameter(ExchangeRpcClientAccess rpcClientAccess) : this(rpcClientAccess.Server)
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001F363 File Offset: 0x0001D563
		public ClientAccessServerOrArrayIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001F36C File Offset: 0x0001D56C
		public ClientAccessServerOrArrayIdParameter()
		{
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001F374 File Offset: 0x0001D574
		protected ClientAccessServerOrArrayIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001F37D File Offset: 0x0001D57D
		public new static ClientAccessServerOrArrayIdParameter Parse(string identity)
		{
			return new ClientAccessServerOrArrayIdParameter(identity);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001F388 File Offset: 0x0001D588
		internal override IEnumerableFilter<T> GetEnumerableFilter<T>()
		{
			if (typeof(T) != typeof(MiniClientAccessServerOrArray))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return ClientAccessServerOrArrayIdParameter.Filter.Instance as IEnumerableFilter<T>;
		}

		// Token: 0x020000F7 RID: 247
		private sealed class Filter : IEnumerableFilter<MiniClientAccessServerOrArray>
		{
			// Token: 0x060008EB RID: 2283 RVA: 0x0001F3DE File Offset: 0x0001D5DE
			private Filter()
			{
			}

			// Token: 0x060008EC RID: 2284 RVA: 0x0001F3E6 File Offset: 0x0001D5E6
			public bool AcceptElement(MiniClientAccessServerOrArray element)
			{
				return element.IsClientAccessArray || element.IsClientAccessServer;
			}

			// Token: 0x0400025D RID: 605
			public static readonly ClientAccessServerOrArrayIdParameter.Filter Instance = new ClientAccessServerOrArrayIdParameter.Filter();
		}
	}
}
