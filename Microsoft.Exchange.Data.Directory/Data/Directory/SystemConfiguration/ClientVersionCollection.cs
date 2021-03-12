using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000534 RID: 1332
	[XmlType(TypeName = "ClientVersionCollection")]
	[Serializable]
	public sealed class ClientVersionCollection : XMLSerializableBase, ICollection<ClientVersion>, IEnumerable<ClientVersion>, IEnumerable
	{
		// Token: 0x06003B3B RID: 15163 RVA: 0x000E21BD File Offset: 0x000E03BD
		public void Add(ClientVersion item)
		{
			this.list.Add(item);
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x000E21CB File Offset: 0x000E03CB
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000E21D8 File Offset: 0x000E03D8
		public bool Contains(ClientVersion item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x000E21E6 File Offset: 0x000E03E6
		public void CopyTo(ClientVersion[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000E21F5 File Offset: 0x000E03F5
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000E2202 File Offset: 0x000E0402
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000E2205 File Offset: 0x000E0405
		public bool Remove(ClientVersion item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000E2213 File Offset: 0x000E0413
		public IEnumerator<ClientVersion> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000E2228 File Offset: 0x000E0428
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable enumerable = this.list;
			return enumerable.GetEnumerator();
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000E2244 File Offset: 0x000E0444
		public ClientVersion GetRequiredClientVersion(Version requestClientVersion)
		{
			ClientVersion clientVersion = null;
			foreach (ClientVersion clientVersion2 in this.list)
			{
				if (requestClientVersion.Major == clientVersion2.Version.Major && requestClientVersion < clientVersion2.Version)
				{
					if (clientVersion == null)
					{
						clientVersion = new ClientVersion
						{
							Version = clientVersion2.Version,
							ExpirationDate = clientVersion2.ExpirationDate
						};
					}
					else if (clientVersion2.Version > clientVersion.Version)
					{
						clientVersion.Version = clientVersion2.Version;
					}
					else if (clientVersion2.ExpirationDate < clientVersion.ExpirationDate)
					{
						clientVersion.ExpirationDate = clientVersion2.ExpirationDate;
					}
				}
			}
			return clientVersion;
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x000E2320 File Offset: 0x000E0520
		public bool IsClientVersionSufficient(Version requestedVersion)
		{
			foreach (ClientVersion clientVersion in this.list)
			{
				if (requestedVersion.Major == clientVersion.Version.Major && requestedVersion < clientVersion.Version && clientVersion.ExpirationDate < DateTime.UtcNow)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400281F RID: 10271
		private List<ClientVersion> list = new List<ClientVersion>();
	}
}
