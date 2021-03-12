using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000757 RID: 1879
	[Serializable]
	public class NotAllowedPublishingByPolicyException : StoragePermanentException
	{
		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06004854 RID: 18516 RVA: 0x00130DD9 File Offset: 0x0012EFD9
		// (set) Token: 0x06004855 RID: 18517 RVA: 0x00130DE1 File Offset: 0x0012EFE1
		internal int MaxAllowedDetailLevel { get; private set; }

		// Token: 0x06004856 RID: 18518 RVA: 0x00130DEA File Offset: 0x0012EFEA
		public NotAllowedPublishingByPolicyException() : base(ServerStrings.NotAllowedAnonymousSharingByPolicy)
		{
			this.MaxAllowedDetailLevel = 0;
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x00130DFE File Offset: 0x0012EFFE
		public NotAllowedPublishingByPolicyException(DetailLevelEnumType detailLevel, DetailLevelEnumType maxAllowedDetailLevel) : base(ServerStrings.DetailLevelNotAllowedByPolicy(LocalizedDescriptionAttribute.FromEnum(typeof(DetailLevelEnumType), detailLevel)))
		{
			EnumValidator.ThrowIfInvalid<DetailLevelEnumType>(detailLevel, "detailLevel");
			EnumValidator.ThrowIfInvalid<DetailLevelEnumType>(maxAllowedDetailLevel, "maxAllowedDetailLevel");
			this.MaxAllowedDetailLevel = (int)maxAllowedDetailLevel;
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x00130E3D File Offset: 0x0012F03D
		protected NotAllowedPublishingByPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
