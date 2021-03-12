using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000190 RID: 400
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NextHopSolutionKeyPropertyBag : PropertyBag
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x00047690 File Offset: 0x00045890
		public NextHopSolutionKeyPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0004769A File Offset: 0x0004589A
		public NextHopSolutionKeyPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000476A5 File Offset: 0x000458A5
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return NextHopSolutionKeyObjectSchema.Version;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x000476AC File Offset: 0x000458AC
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return NextHopSolutionKeyObjectSchema.ObjectState;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000476B3 File Offset: 0x000458B3
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return NextHopSolutionKeyObjectSchema.Id;
			}
		}
	}
}
