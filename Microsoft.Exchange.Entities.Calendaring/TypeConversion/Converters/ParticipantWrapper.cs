using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x02000074 RID: 116
	internal abstract class ParticipantWrapper<TOriginal> : IParticipantWrapper
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000B3D7 File Offset: 0x000095D7
		protected ParticipantWrapper(TOriginal original)
		{
			this.Original = original;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000B3E6 File Offset: 0x000095E6
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000B3EE File Offset: 0x000095EE
		public TOriginal Original { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600030A RID: 778
		public abstract Participant Participant { get; }

		// Token: 0x0600030B RID: 779 RVA: 0x0000B3F7 File Offset: 0x000095F7
		public static implicit operator Participant(ParticipantWrapper<TOriginal> wrapper)
		{
			return wrapper.Participant;
		}
	}
}
