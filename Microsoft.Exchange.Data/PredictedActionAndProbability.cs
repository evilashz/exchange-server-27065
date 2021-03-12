using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000090 RID: 144
	internal class PredictedActionAndProbability
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000E683 File Offset: 0x0000C883
		public PredictedActionAndProbability(PredictedMessageAction action, short probability)
		{
			this.Action = action;
			this.Probability = (int)probability;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000E699 File Offset: 0x0000C899
		public PredictedActionAndProbability(short rawActionAndProbability)
		{
			this.RawActionAndProbability = rawActionAndProbability;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public PredictedMessageAction Action { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000E6B9 File Offset: 0x0000C8B9
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000E6C1 File Offset: 0x0000C8C1
		public int Probability
		{
			get
			{
				return this.probability;
			}
			private set
			{
				if (value > 100 || value < 0)
				{
					throw new ArgumentOutOfRangeException("probability");
				}
				this.probability = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000E6E6 File Offset: 0x0000C8E6
		public bool Completed { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000E728 File Offset: 0x0000C928
		public short RawActionAndProbability
		{
			get
			{
				short num = (short)this.Action;
				num = (short)(num << 8);
				num += (short)this.Probability;
				if (this.Completed)
				{
					num |= short.MinValue;
				}
				return num;
			}
			set
			{
				this.Completed = ((value & short.MinValue) != 0);
				this.Action = (PredictedMessageAction)(value >> 8 & 127);
				this.Probability = (int)(value & 255);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000E757 File Offset: 0x0000C957
		public override bool Equals(object other)
		{
			return this.Equals(other as PredictedActionAndProbability);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000E768 File Offset: 0x0000C968
		public override int GetHashCode()
		{
			return this.Action.GetHashCode() + this.Probability.GetHashCode();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000E794 File Offset: 0x0000C994
		private bool Equals(PredictedActionAndProbability other)
		{
			return other != null && (object.ReferenceEquals(other, this) || (this.Action.Equals(other.Action) && this.Probability.Equals(other.Probability)));
		}

		// Token: 0x04000209 RID: 521
		private int probability;
	}
}
