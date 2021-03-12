using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000086 RID: 134
	internal struct ReplicaServerInfo : IEquatable<ReplicaServerInfo>
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000C897 File Offset: 0x0000AA97
		public ReplicaServerInfo(ushort cheapReplicaCount, string[] replicas)
		{
			Util.ThrowOnNullArgument(replicas, "replicas");
			this.cheapReplicaCount = cheapReplicaCount;
			this.replicas = replicas;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public ReplicaServerInfo(string mailboxLegacyDistinguishedName)
		{
			Util.ThrowOnNullOrEmptyArgument(mailboxLegacyDistinguishedName, "mailboxLegacyDistinguishedName");
			this.cheapReplicaCount = 1;
			this.replicas = new string[]
			{
				mailboxLegacyDistinguishedName
			};
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000C8E5 File Offset: 0x0000AAE5
		public ushort CheapReplicaCount
		{
			get
			{
				return this.cheapReplicaCount;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000C8ED File Offset: 0x0000AAED
		public string[] Replicas
		{
			get
			{
				return this.replicas ?? Array<string>.Empty;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C8FE File Offset: 0x0000AAFE
		public override bool Equals(object obj)
		{
			return obj is ReplicaServerInfo && this.Equals((ReplicaServerInfo)obj);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C918 File Offset: 0x0000AB18
		public override int GetHashCode()
		{
			return this.cheapReplicaCount.GetHashCode() ^ ArrayComparer<string>.Comparer.GetHashCode(this.replicas);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C944 File Offset: 0x0000AB44
		public override string ToString()
		{
			return string.Format("ServerCount: {0}, CheapReplicaCount: {1}, Replicas: {{{2}}}", this.Replicas.Length, this.CheapReplicaCount, string.Join(",", this.Replicas));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C978 File Offset: 0x0000AB78
		public bool Equals(ReplicaServerInfo other)
		{
			return this.CheapReplicaCount == other.CheapReplicaCount && ArrayComparer<string>.Comparer.Equals(this.Replicas, other.Replicas);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		internal static ReplicaServerInfo Parse(Reader reader)
		{
			ushort num = reader.ReadUInt16();
			ushort num2 = reader.ReadUInt16();
			string[] array = new string[(int)num];
			uint num3 = 0U;
			while ((ulong)num3 < (ulong)((long)array.Length))
			{
				array[(int)((UIntPtr)num3)] = reader.ReadAsciiString(StringFlags.IncludeNull);
				num3 += 1U;
			}
			return new ReplicaServerInfo(num2, array);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		internal void Serialize(Writer writer)
		{
			writer.WriteUInt16((ushort)this.replicas.Length);
			writer.WriteUInt16(this.cheapReplicaCount);
			foreach (string value in this.replicas)
			{
				writer.WriteAsciiString(value, StringFlags.IncludeNull);
			}
		}

		// Token: 0x040001CB RID: 459
		private readonly ushort cheapReplicaCount;

		// Token: 0x040001CC RID: 460
		private readonly string[] replicas;
	}
}
