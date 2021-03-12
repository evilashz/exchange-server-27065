using System;
using System.Text;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000CE RID: 206
	internal abstract class MwiFailureEventLogStrategy
	{
		// Token: 0x060006D8 RID: 1752
		internal abstract void LogFailure(MwiMessage message, Exception ex);

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001AA38 File Offset: 0x00018C38
		protected virtual string ConstructErrorMessage(MwiMessage message, Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(ex.Message);
			foreach (MwiDeliveryException ex2 in message.DeliveryErrors)
			{
				stringBuilder.AppendLine(ex2.Message);
			}
			return stringBuilder.ToString();
		}
	}
}
