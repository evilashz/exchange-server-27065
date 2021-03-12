using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020000DC RID: 220
	internal interface IActivityScope
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000614 RID: 1556
		Guid ActivityId { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000615 RID: 1557
		Guid LocalId { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000616 RID: 1558
		ActivityContextStatus Status { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000617 RID: 1559
		ActivityType ActivityType { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000618 RID: 1560
		DateTime StartTime { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000619 RID: 1561
		DateTime? EndTime { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600061A RID: 1562
		double TotalMilliseconds { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600061B RID: 1563
		// (set) Token: 0x0600061C RID: 1564
		object UserState { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600061D RID: 1565
		// (set) Token: 0x0600061E RID: 1566
		string UserId { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600061F RID: 1567
		// (set) Token: 0x06000620 RID: 1568
		string Puid { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000621 RID: 1569
		// (set) Token: 0x06000622 RID: 1570
		string UserEmail { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000623 RID: 1571
		// (set) Token: 0x06000624 RID: 1572
		string AuthenticationType { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000625 RID: 1573
		// (set) Token: 0x06000626 RID: 1574
		string AuthenticationToken { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000627 RID: 1575
		// (set) Token: 0x06000628 RID: 1576
		string TenantId { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000629 RID: 1577
		// (set) Token: 0x0600062A RID: 1578
		string TenantType { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600062B RID: 1579
		// (set) Token: 0x0600062C RID: 1580
		string Component { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600062D RID: 1581
		// (set) Token: 0x0600062E RID: 1582
		string ComponentInstance { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600062F RID: 1583
		// (set) Token: 0x06000630 RID: 1584
		string Feature { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000631 RID: 1585
		// (set) Token: 0x06000632 RID: 1586
		string Protocol { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000633 RID: 1587
		// (set) Token: 0x06000634 RID: 1588
		string ClientInfo { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000635 RID: 1589
		// (set) Token: 0x06000636 RID: 1590
		string ClientRequestId { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000637 RID: 1591
		// (set) Token: 0x06000638 RID: 1592
		string ReturnClientRequestId { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000639 RID: 1593
		// (set) Token: 0x0600063A RID: 1594
		string Action { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600063B RID: 1595
		IEnumerable<KeyValuePair<Enum, object>> Metadata { get; }

		// Token: 0x0600063C RID: 1596
		AggregatedOperationStatistics TakeStatisticsSnapshot(AggregatedOperationType type);

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600063D RID: 1597
		IEnumerable<KeyValuePair<OperationKey, OperationStatistics>> Statistics { get; }

		// Token: 0x0600063E RID: 1598
		ActivityContextState Suspend();

		// Token: 0x0600063F RID: 1599
		void End();

		// Token: 0x06000640 RID: 1600
		bool AddOperation(ActivityOperationType operation, string instance, float value = 0f, int count = 1);

		// Token: 0x06000641 RID: 1601
		void SetProperty(Enum property, string value);

		// Token: 0x06000642 RID: 1602
		bool AppendToProperty(Enum property, string value);

		// Token: 0x06000643 RID: 1603
		string GetProperty(Enum property);

		// Token: 0x06000644 RID: 1604
		List<KeyValuePair<string, object>> GetFormattableMetadata();

		// Token: 0x06000645 RID: 1605
		IEnumerable<KeyValuePair<string, object>> GetFormattableMetadata(IEnumerable<Enum> properties);

		// Token: 0x06000646 RID: 1606
		List<KeyValuePair<string, object>> GetFormattableStatistics();

		// Token: 0x06000647 RID: 1607
		void UpdateFromMessage(HttpRequestMessageProperty wcfMessage);

		// Token: 0x06000648 RID: 1608
		void UpdateFromMessage(OperationContext wcfOperationContext);

		// Token: 0x06000649 RID: 1609
		void UpdateFromMessage(HttpRequest httpRequest);

		// Token: 0x0600064A RID: 1610
		void UpdateFromMessage(HttpRequestBase httpRequestBase);

		// Token: 0x0600064B RID: 1611
		void SerializeTo(OperationContext wcfOperationContext);

		// Token: 0x0600064C RID: 1612
		void SerializeTo(HttpRequestMessageProperty wcfMessage);

		// Token: 0x0600064D RID: 1613
		void SerializeTo(HttpWebRequest httpWebRequest);

		// Token: 0x0600064E RID: 1614
		void SerializeTo(HttpResponse httpResponse);

		// Token: 0x0600064F RID: 1615
		void SerializeMinimalTo(HttpWebRequest httpWebRequest);

		// Token: 0x06000650 RID: 1616
		void SerializeMinimalTo(HttpRequestBase httpRequest);
	}
}
