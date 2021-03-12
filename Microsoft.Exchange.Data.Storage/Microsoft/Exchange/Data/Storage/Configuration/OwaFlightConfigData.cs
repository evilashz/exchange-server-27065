using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000467 RID: 1127
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OwaFlightConfigData : SerializableDataBase, IEquatable<OwaFlightConfigData>
	{
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000CDCEB File Offset: 0x000CBEEB
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000CDCF3 File Offset: 0x000CBEF3
		[DataMember]
		public string RampId { get; set; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000CDCFC File Offset: 0x000CBEFC
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000CDD04 File Offset: 0x000CBF04
		[DataMember]
		public bool IsFirstRelease { get; set; }

		// Token: 0x06003281 RID: 12929 RVA: 0x000CDD0D File Offset: 0x000CBF0D
		public bool Equals(OwaFlightConfigData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || (this.RampId == other.RampId && this.IsFirstRelease == other.IsFirstRelease));
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000CDD48 File Offset: 0x000CBF48
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as OwaFlightConfigData);
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000CDD58 File Offset: 0x000CBF58
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ this.IsFirstRelease.GetHashCode());
			return num * 397 ^ (this.RampId ?? string.Empty).GetHashCode();
		}
	}
}
