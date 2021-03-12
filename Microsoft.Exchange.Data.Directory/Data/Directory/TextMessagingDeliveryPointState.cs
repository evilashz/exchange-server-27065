using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001AA RID: 426
	internal class TextMessagingDeliveryPointState : TextMessagingStateBase
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x00057545 File Offset: 0x00055745
		public TextMessagingDeliveryPointState(int rawValue) : base(rawValue)
		{
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0005754E File Offset: 0x0005574E
		public TextMessagingDeliveryPointState(bool shared, bool p2pEnabled, bool m2pEnabled, DeliveryPointType type, byte id, byte p2pPriority, byte m2pPriority) : base(0)
		{
			this.Shared = shared;
			this.PersonToPersonMessagingEnabled = p2pEnabled;
			this.MachineToPersonMessagingEnabled = m2pEnabled;
			this.Type = type;
			this.Identity = id;
			this.PersonToPersonMessagingPriority = p2pPriority;
			this.MachineToPersonMessagingPriority = m2pPriority;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0005758C File Offset: 0x0005578C
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x000575A0 File Offset: 0x000557A0
		public bool Shared
		{
			get
			{
				return 0 != (1073741824 & base.RawValue);
			}
			private set
			{
				base.RawValue &= -1073741825;
				base.RawValue ^= (value ? 1073741824 : 0);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x000575CC File Offset: 0x000557CC
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x000575E0 File Offset: 0x000557E0
		public bool PersonToPersonMessagingEnabled
		{
			get
			{
				return 0 != (536870912 & base.RawValue);
			}
			private set
			{
				base.RawValue &= -536870913;
				base.RawValue ^= (value ? 536870912 : 0);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0005760C File Offset: 0x0005580C
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00057620 File Offset: 0x00055820
		public bool MachineToPersonMessagingEnabled
		{
			get
			{
				return 0 != (268435456 & base.RawValue);
			}
			private set
			{
				base.RawValue &= -268435457;
				base.RawValue ^= (value ? 268435456 : 0);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0005764C File Offset: 0x0005584C
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00057683 File Offset: 0x00055883
		public DeliveryPointType Type
		{
			get
			{
				int num = (251658240 & base.RawValue) >> 24;
				if (!Enum.IsDefined(typeof(DeliveryPointType), num))
				{
					return DeliveryPointType.Unknown;
				}
				return (DeliveryPointType)num;
			}
			private set
			{
				base.RawValue &= -251658241;
				base.RawValue ^= (int)((int)value << 24);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000576A8 File Offset: 0x000558A8
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x000576BA File Offset: 0x000558BA
		public byte Identity
		{
			get
			{
				return (byte)((16711680 & base.RawValue) >> 16);
			}
			private set
			{
				base.RawValue &= -16711681;
				base.RawValue ^= (int)value << 16;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x000576DF File Offset: 0x000558DF
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000576F0 File Offset: 0x000558F0
		public byte PersonToPersonMessagingPriority
		{
			get
			{
				return (byte)((65280 & base.RawValue) >> 8);
			}
			private set
			{
				base.RawValue &= -65281;
				base.RawValue ^= (int)value << 8;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00057714 File Offset: 0x00055914
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x00057723 File Offset: 0x00055923
		public byte MachineToPersonMessagingPriority
		{
			get
			{
				return (byte)(255 & base.RawValue);
			}
			private set
			{
				base.RawValue &= -256;
				base.RawValue ^= (int)value;
			}
		}

		// Token: 0x04000A60 RID: 2656
		internal const int StartBitShared = 30;

		// Token: 0x04000A61 RID: 2657
		internal const int StartBitP2pEnabled = 29;

		// Token: 0x04000A62 RID: 2658
		internal const int StartBitM2pEnabled = 28;

		// Token: 0x04000A63 RID: 2659
		internal const int StartBitType = 24;

		// Token: 0x04000A64 RID: 2660
		internal const int StartBitIdentity = 16;

		// Token: 0x04000A65 RID: 2661
		internal const int StartBitP2pPriority = 8;

		// Token: 0x04000A66 RID: 2662
		internal const int StartBitM2pPriority = 0;

		// Token: 0x04000A67 RID: 2663
		internal const int MaskShared = 1073741824;

		// Token: 0x04000A68 RID: 2664
		internal const int MaskP2pEnabled = 536870912;

		// Token: 0x04000A69 RID: 2665
		internal const int MaskM2pEnabled = 268435456;

		// Token: 0x04000A6A RID: 2666
		internal const int MaskType = 251658240;

		// Token: 0x04000A6B RID: 2667
		internal const int MaskIdentity = 16711680;

		// Token: 0x04000A6C RID: 2668
		internal const int MaskP2pPriority = 65280;

		// Token: 0x04000A6D RID: 2669
		internal const int MaskM2pPriority = 255;
	}
}
