using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.Serialization;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000003 RID: 3
	internal class AddressFinderDiagnostics : IAddressFinderDiagnostics
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public AddressFinderDiagnostics(HttpContextBase httpContext)
		{
			this.logger = RequestLogger.GetLogger(httpContext);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020EF File Offset: 0x000002EF
		public void AddErrorInfo(object value)
		{
			this.logger.AppendErrorInfo("AddressFinder", value);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002102 File Offset: 0x00000302
		public void AddRoutingkey(IRoutingKey routingKey, string routingHint)
		{
			this.routingKeyLogs.Add(new Tuple<IRoutingKey, string>(routingKey, routingHint));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public void LogRoutingKeys()
		{
			if (this.routingKeyLogs.Count == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (Tuple<IRoutingKey, string> tuple in this.routingKeyLogs)
			{
				stringBuilder.Append(RoutingEntryHeaderSerializer.RoutingTypeToString(tuple.Item1.RoutingItemType) + "-" + tuple.Item1.Value + "|");
				stringBuilder2.Append(tuple.Item2 + "|");
			}
			this.logger.LogField(LogKey.AnchorMailbox, stringBuilder.ToString());
			this.logger.LogField(LogKey.RoutingHint, stringBuilder2.ToString());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021EC File Offset: 0x000003EC
		public void LogUnhandledException(Exception ex)
		{
			this.logger.LastChanceExceptionHandler(ex);
		}

		// Token: 0x04000001 RID: 1
		private const string EntrySeparator = "|";

		// Token: 0x04000002 RID: 2
		private const string RoutingKeyValueSeparator = "-";

		// Token: 0x04000003 RID: 3
		private RequestLogger logger;

		// Token: 0x04000004 RID: 4
		private List<Tuple<IRoutingKey, string>> routingKeyLogs = new List<Tuple<IRoutingKey, string>>();
	}
}
