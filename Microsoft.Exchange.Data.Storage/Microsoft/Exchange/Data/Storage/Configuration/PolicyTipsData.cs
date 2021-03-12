using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000469 RID: 1129
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyTipsData : SerializableDataBase, IEquatable<PolicyTipsData>
	{
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000CDE2B File Offset: 0x000CC02B
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x000CDE33 File Offset: 0x000CC033
		[DataMember]
		public bool IsPolicyTipsEnabled { get; set; }

		// Token: 0x0600328D RID: 12941 RVA: 0x000CDE3C File Offset: 0x000CC03C
		public bool Equals(PolicyTipsData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || this.IsPolicyTipsEnabled.Equals(other.IsPolicyTipsEnabled));
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000CDE73 File Offset: 0x000CC073
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as PolicyTipsData);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000CDE84 File Offset: 0x000CC084
		protected override int InternalGetHashCode()
		{
			return this.IsPolicyTipsEnabled.GetHashCode();
		}
	}
}
