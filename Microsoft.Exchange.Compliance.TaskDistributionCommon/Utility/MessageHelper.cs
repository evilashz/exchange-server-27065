using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Client;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Utility
{
	// Token: 0x02000066 RID: 102
	internal static class MessageHelper
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public static string GetRoutingKey(ComplianceMessage message)
		{
			if (message == null || message.MessageTarget == null)
			{
				return null;
			}
			if (message.MessageTarget.TargetType == Target.Type.Driver)
			{
				return string.Format("DRIVERROUTE", new object[0]);
			}
			return string.Format("DATABASEROUTE:{0}", message.MessageTarget.Database);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public static ClientType GetClientType(ComplianceMessage message)
		{
			if (message != null && message.MessageTarget != null && message.MessageTarget.TargetType == Target.Type.Driver)
			{
				return ClientType.DriverClient;
			}
			return ClientType.ExchangeWorkloadClient;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000ECF7 File Offset: 0x0000CEF7
		public static ClientType GetClientType(IEnumerable<ComplianceMessage> messages)
		{
			return MessageHelper.GetClientType(messages.First<ComplianceMessage>());
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000ED04 File Offset: 0x0000CF04
		public static bool IsFromDriver(ComplianceMessage message)
		{
			return message.MessageSource != null && message.MessageSource.TargetType == Target.Type.Driver;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000ED1F File Offset: 0x0000CF1F
		public static bool IsToDriver(ComplianceMessage message)
		{
			return message.MessageSource != null && message.MessageTarget.TargetType == Target.Type.Driver;
		}
	}
}
