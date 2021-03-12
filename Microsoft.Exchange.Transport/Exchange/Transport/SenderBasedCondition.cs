using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200033F RID: 831
	internal class SenderBasedCondition : WaitCondition
	{
		// Token: 0x060023EF RID: 9199 RVA: 0x00088DDB File Offset: 0x00086FDB
		public SenderBasedCondition(string sender)
		{
			if (string.IsNullOrEmpty(sender))
			{
				throw new ArgumentNullException("sender");
			}
			this.value = sender;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00088E00 File Offset: 0x00087000
		public override int CompareTo(object obj)
		{
			SenderBasedCondition senderBasedCondition = obj as SenderBasedCondition;
			if (senderBasedCondition == null)
			{
				throw new ArgumentException();
			}
			return this.value.CompareTo(senderBasedCondition.value);
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00088E30 File Offset: 0x00087030
		public override bool Equals(object obj)
		{
			SenderBasedCondition senderBasedCondition = obj as SenderBasedCondition;
			return senderBasedCondition != null && this.Equals(senderBasedCondition);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00088E50 File Offset: 0x00087050
		public bool Equals(SenderBasedCondition condition)
		{
			return condition != null && this.value.Equals(condition.value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00088E69 File Offset: 0x00087069
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00088E76 File Offset: 0x00087076
		public override string ToString()
		{
			return "SenderBasedCondition-" + this.value.ToString();
		}

		// Token: 0x04001293 RID: 4755
		private readonly string value;
	}
}
