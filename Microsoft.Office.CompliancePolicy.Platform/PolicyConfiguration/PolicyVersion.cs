using System;
using System.Data.SqlTypes;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x020000A2 RID: 162
	[DataContract]
	[Serializable]
	public sealed class PolicyVersion : IComparable
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0000D34E File Offset: 0x0000B54E
		private PolicyVersion()
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000D356 File Offset: 0x0000B556
		internal Guid InternalStorage
		{
			get
			{
				return this.internalStorage;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000D35E File Offset: 0x0000B55E
		public static bool operator ==(PolicyVersion lhs, PolicyVersion rhs)
		{
			return object.ReferenceEquals(lhs, rhs) || (lhs != null && rhs != null && lhs.Equals(rhs));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D37A File Offset: 0x0000B57A
		public static bool operator !=(PolicyVersion lhs, PolicyVersion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000D386 File Offset: 0x0000B586
		public static implicit operator Guid(PolicyVersion version)
		{
			if (version == null)
			{
				return Guid.Empty;
			}
			return version.InternalStorage;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000D39D File Offset: 0x0000B59D
		public static implicit operator PolicyVersion(Guid version)
		{
			return PolicyVersion.Create(version);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public int CompareTo(object obj)
		{
			PolicyVersion policyVersion = obj as PolicyVersion;
			if (policyVersion == null)
			{
				throw new ArgumentNullException("obj");
			}
			return new SqlGuid(this.internalStorage).CompareTo(new SqlGuid(policyVersion.InternalStorage));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyVersion policyVersion = obj as PolicyVersion;
			return !(policyVersion == null) && this.internalStorage.Equals(policyVersion.internalStorage);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000D425 File Offset: 0x0000B625
		public override int GetHashCode()
		{
			return this.internalStorage.GetHashCode();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000D438 File Offset: 0x0000B638
		internal static PolicyVersion Create(Guid combGuid)
		{
			return new PolicyVersion
			{
				internalStorage = combGuid
			};
		}

		// Token: 0x040002AA RID: 682
		public static readonly PolicyVersion Empty = PolicyVersion.Create(Guid.Empty);

		// Token: 0x040002AB RID: 683
		[DataMember]
		private Guid internalStorage;
	}
}
