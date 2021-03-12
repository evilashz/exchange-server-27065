using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public class ExchangeCertificateIdParameter : IIdentityParameter
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x00021087 File Offset: 0x0001F287
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002108E File Offset: 0x0001F28E
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00021095 File Offset: 0x0001F295
		public void Initialize(ObjectId objectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002109C File Offset: 0x0001F29C
		public string RawIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000210A3 File Offset: 0x0001F2A3
		public ExchangeCertificateIdParameter(INamedIdentity id) : this(id.Identity)
		{
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000210B4 File Offset: 0x0001F2B4
		public ExchangeCertificateIdParameter(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(base.GetType().ToString()), "identity");
			}
			if (!id.Contains("\\"))
			{
				this.Thumbprint = id;
				return;
			}
			this.Thumbprint = id.Remove(0, id.Split(new char[]
			{
				'\\'
			})[0].Length + 1);
			this.ServerIdParameter = ServerIdParameter.Parse(id.Split(new char[]
			{
				'\\'
			})[0]);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002115B File Offset: 0x0001F35B
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00021163 File Offset: 0x0001F363
		public ServerIdParameter ServerIdParameter { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002116C File Offset: 0x0001F36C
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00021174 File Offset: 0x0001F374
		public string Thumbprint { get; private set; }

		// Token: 0x060009BC RID: 2492 RVA: 0x0002117D File Offset: 0x0001F37D
		public static ExchangeCertificateIdParameter Parse(string id)
		{
			return new ExchangeCertificateIdParameter(id);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00021185 File Offset: 0x0001F385
		public override string ToString()
		{
			if (this.ServerIdParameter != null)
			{
				return this.ServerIdParameter.ToString() + "\\" + this.Thumbprint;
			}
			return this.Thumbprint;
		}
	}
}
