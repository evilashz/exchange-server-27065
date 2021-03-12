using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000340 RID: 832
	internal class ResubmitterBasedCondition : WaitCondition
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x00088E8D File Offset: 0x0008708D
		public ResubmitterBasedCondition(string resubmitter)
		{
			if (string.IsNullOrEmpty(resubmitter))
			{
				throw new ArgumentNullException("resubmitter");
			}
			this.value = resubmitter;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00088EB0 File Offset: 0x000870B0
		public override int CompareTo(object obj)
		{
			ResubmitterBasedCondition resubmitterBasedCondition = obj as ResubmitterBasedCondition;
			if (resubmitterBasedCondition == null)
			{
				throw new ArgumentException();
			}
			return this.value.CompareTo(resubmitterBasedCondition.value);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00088EE0 File Offset: 0x000870E0
		public override bool Equals(object obj)
		{
			ResubmitterBasedCondition resubmitterBasedCondition = obj as ResubmitterBasedCondition;
			return resubmitterBasedCondition != null && this.Equals(resubmitterBasedCondition);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00088F00 File Offset: 0x00087100
		public bool Equals(ResubmitterBasedCondition condition)
		{
			return condition != null && this.value.Equals(condition.value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x00088F19 File Offset: 0x00087119
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x00088F26 File Offset: 0x00087126
		public override string ToString()
		{
			return "ResubmitterBasedCondition-" + this.value.ToString();
		}

		// Token: 0x04001294 RID: 4756
		private readonly string value;
	}
}
