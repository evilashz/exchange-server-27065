using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Configuration
{
	// Token: 0x02000466 RID: 1126
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class OwaOrgConfigData : SerializableDataBase, IEquatable<OwaOrgConfigData>
	{
		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000CDC21 File Offset: 0x000CBE21
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x000CDC29 File Offset: 0x000CBE29
		[DataMember]
		public uint MailTipsLargeAudienceThreshold { get; set; }

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000CDC32 File Offset: 0x000CBE32
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000CDC3A File Offset: 0x000CBE3A
		[DataMember]
		public bool PublicComputersDetectionEnabled { get; set; }

		// Token: 0x06003279 RID: 12921 RVA: 0x000CDC44 File Offset: 0x000CBE44
		public bool Equals(OwaOrgConfigData other)
		{
			return !object.ReferenceEquals(other, null) && (object.ReferenceEquals(other, this) || (this.MailTipsLargeAudienceThreshold.Equals(other.MailTipsLargeAudienceThreshold) && this.PublicComputersDetectionEnabled.Equals(other.PublicComputersDetectionEnabled)));
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000CDC93 File Offset: 0x000CBE93
		protected override bool InternalEquals(object other)
		{
			return this.Equals(other as OwaOrgConfigData);
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000CDCA4 File Offset: 0x000CBEA4
		protected override int InternalGetHashCode()
		{
			int num = 17;
			num = (num * 397 ^ this.MailTipsLargeAudienceThreshold.GetHashCode());
			return num * 397 ^ this.PublicComputersDetectionEnabled.GetHashCode();
		}
	}
}
