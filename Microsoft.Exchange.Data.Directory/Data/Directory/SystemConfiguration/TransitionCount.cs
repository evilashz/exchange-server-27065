using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D8 RID: 1496
	public class TransitionCount : IComparable<TransitionCount>
	{
		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x00100DCF File Offset: 0x000FEFCF
		// (set) Token: 0x06004503 RID: 17667 RVA: 0x00100DD7 File Offset: 0x000FEFD7
		public TenantRelocationTransition Transition { get; internal set; }

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x00100DE0 File Offset: 0x000FEFE0
		// (set) Token: 0x06004505 RID: 17669 RVA: 0x00100DE8 File Offset: 0x000FEFE8
		public ushort Count { get; internal set; }

		// Token: 0x06004506 RID: 17670 RVA: 0x00100DF1 File Offset: 0x000FEFF1
		internal TransitionCount(TenantRelocationTransition transition, ushort count)
		{
			this.Transition = transition;
			this.Count = count;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00100E08 File Offset: 0x000FF008
		int IComparable<TransitionCount>.CompareTo(TransitionCount other)
		{
			if (other == null)
			{
				return 1;
			}
			return ((byte)this.Transition).CompareTo((byte)other.Transition);
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00100E2E File Offset: 0x000FF02E
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.Transition, this.Count);
		}
	}
}
