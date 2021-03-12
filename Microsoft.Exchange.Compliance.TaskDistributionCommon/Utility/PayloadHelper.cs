using System;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Utility
{
	// Token: 0x02000069 RID: 105
	internal static class PayloadHelper
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		internal static PayloadReference GetPayloadReference(Guid jobRunId, int taskId = -1)
		{
			PayloadIdentifier payloadIdentifier = new PayloadIdentifier
			{
				JobRunId = jobRunId,
				TaskId = taskId
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return new PayloadReference
			{
				PayloadId = javaScriptSerializer.Serialize(payloadIdentifier)
			};
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		internal static bool TryReadFromPayloadReference(PayloadReference payloadReference, out Guid jobRunId, out int taskId, out PayloadLevel payloadLevel)
		{
			jobRunId = Guid.Empty;
			taskId = -1;
			payloadLevel = PayloadLevel.Job;
			bool result;
			try
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				PayloadIdentifier payloadIdentifier = javaScriptSerializer.Deserialize<PayloadIdentifier>(payloadReference.PayloadId);
				jobRunId = payloadIdentifier.JobRunId;
				if (payloadIdentifier.TaskId > -1)
				{
					taskId = payloadIdentifier.TaskId;
					payloadLevel = PayloadLevel.Task;
				}
				else
				{
					payloadLevel = PayloadLevel.Job;
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}
	}
}
