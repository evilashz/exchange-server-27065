using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Xml.Linq;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000A5 RID: 165
	public class PerformanceEntry
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x00016988 File Offset: 0x00014B88
		public PerformanceEntry(StoreKind storeKind, bool isPrimary)
		{
			this.StoreKind = storeKind;
			this.IsPrimary = isPrimary;
			this.LatencyMap = new Dictionary<string, PerformanceEntry.LatencyMeasure>();
			this.ExceptionCountMap = new Dictionary<string, int>();
			this.ApiMap = new Dictionary<OperationCategory, PerformanceEntry.ApiMeasure>();
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000169BF File Offset: 0x00014BBF
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x000169C7 File Offset: 0x00014BC7
		public StoreKind StoreKind { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000169D0 File Offset: 0x00014BD0
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x000169D8 File Offset: 0x00014BD8
		public bool IsPrimary { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000169E1 File Offset: 0x00014BE1
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000169E9 File Offset: 0x00014BE9
		public int CountTotal { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000169F2 File Offset: 0x00014BF2
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x000169FA File Offset: 0x00014BFA
		public int CountInProgress { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00016A03 File Offset: 0x00014C03
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00016A0B File Offset: 0x00014C0B
		public int CountReadRequestsSkipped { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00016A14 File Offset: 0x00014C14
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00016A1C File Offset: 0x00014C1C
		public int CountWriteRequestsSkipped { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00016A25 File Offset: 0x00014C25
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00016A2D File Offset: 0x00014C2D
		public int CountStale { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00016A36 File Offset: 0x00014C36
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00016A3E File Offset: 0x00014C3E
		public int CountNotReady { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00016A47 File Offset: 0x00014C47
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x00016A4F File Offset: 0x00014C4F
		public int CountFailedConstraints { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00016A58 File Offset: 0x00014C58
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00016A60 File Offset: 0x00014C60
		public int CountFailedServerTimeout { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00016A69 File Offset: 0x00014C69
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00016A71 File Offset: 0x00014C71
		public int CountFailedClientTimeout { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00016A7A File Offset: 0x00014C7A
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00016A82 File Offset: 0x00014C82
		public Dictionary<string, PerformanceEntry.LatencyMeasure> LatencyMap { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00016A8B File Offset: 0x00014C8B
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00016A93 File Offset: 0x00014C93
		public Dictionary<string, int> ExceptionCountMap { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00016A9C File Offset: 0x00014C9C
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00016AA4 File Offset: 0x00014CA4
		public Dictionary<OperationCategory, PerformanceEntry.ApiMeasure> ApiMap { get; set; }

		// Token: 0x06000626 RID: 1574 RVA: 0x00016AB0 File Offset: 0x00014CB0
		public void RecordStart()
		{
			lock (this)
			{
				this.CountTotal++;
				this.CountInProgress++;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00016B04 File Offset: 0x00014D04
		public void RecordFinish(RequestInfo req, long latencyInMs, Exception exception, bool isSkipped = false)
		{
			lock (this)
			{
				this.CountInProgress--;
				if (isSkipped)
				{
					if (req.OperationType == OperationType.Write)
					{
						this.CountWriteRequestsSkipped++;
					}
					else
					{
						this.CountReadRequestsSkipped++;
					}
				}
				else
				{
					bool isSuccess = exception == null;
					this.TrackLatency(req.OperationType, latencyInMs, isSuccess);
					this.RecordApiResult(req.OperationCategory, isSuccess, latencyInMs);
					if (exception != null)
					{
						this.TrackExceptions(exception);
					}
				}
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00016BB4 File Offset: 0x00014DB4
		public string GetApiStatsAsXml()
		{
			XElement xelement = new XElement("ApiStats");
			IOrderedEnumerable<KeyValuePair<OperationCategory, PerformanceEntry.ApiMeasure>> orderedEnumerable = from kvp in this.ApiMap
			orderby kvp.Value.Latency.Max descending
			select kvp;
			foreach (KeyValuePair<OperationCategory, PerformanceEntry.ApiMeasure> keyValuePair in orderedEnumerable)
			{
				OperationCategory key = keyValuePair.Key;
				PerformanceEntry.ApiMeasure value = keyValuePair.Value;
				XElement content = new XElement("Api", new object[]
				{
					new XAttribute("Name", key),
					new XAttribute("Succeeded", value.Succeeded),
					new XAttribute("Failed", value.Failed),
					new XAttribute("Average", value.Latency.Average.ToString(".00")),
					new XAttribute("Max", value.Latency.Max),
					new XAttribute("MaxMinusOne", value.Latency.MaxMinusOne),
					new XAttribute("Min", value.Latency.Min),
					new XAttribute("MinPlusOne", value.Latency.MinPlusOne)
				});
				xelement.Add(content);
			}
			return xelement.ToString();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00016D94 File Offset: 0x00014F94
		public string GetLatencyStatsAsXml()
		{
			XElement xelement = new XElement("LatencyStats");
			foreach (KeyValuePair<string, PerformanceEntry.LatencyMeasure> keyValuePair in this.LatencyMap)
			{
				string key = keyValuePair.Key;
				PerformanceEntry.LatencyMeasure value = keyValuePair.Value;
				XElement content = new XElement("Item", new object[]
				{
					new XAttribute("Name", key),
					new XAttribute("Count", value.Count),
					new XAttribute("Average", value.Average.ToString(".00")),
					new XAttribute("Max", value.Max),
					new XAttribute("MaxMinusOne", value.MaxMinusOne),
					new XAttribute("Min", value.Min),
					new XAttribute("MinPlusOne", value.MinPlusOne)
				});
				xelement.Add(content);
			}
			return xelement.ToString();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00016F1C File Offset: 0x0001511C
		public string GetExceptionStatsAsXml()
		{
			XElement xelement = new XElement("ExceptionStats");
			IEnumerable<KeyValuePair<string, int>> enumerable = (from kvp in this.ExceptionCountMap
			orderby kvp.Value descending
			select kvp).Take(10);
			foreach (KeyValuePair<string, int> keyValuePair in enumerable)
			{
				string key = keyValuePair.Key;
				int value = keyValuePair.Value;
				XElement content = new XElement("Exception", new object[]
				{
					new XAttribute("Name", key),
					new XAttribute("Count", value)
				});
				xelement.Add(content);
			}
			return xelement.ToString();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001700C File Offset: 0x0001520C
		public void PublishEvent(string currentProcessName)
		{
			string latencyStatsAsXml = this.GetLatencyStatsAsXml();
			string exceptionStatsAsXml = this.GetExceptionStatsAsXml();
			string apiStatsAsXml = this.GetApiStatsAsXml();
			DxStoreHACrimsonEvents.ApiPerfStats.Log<bool, StoreKind, int, int, int, int, int, int, int, int, string, string, string, string>(this.IsPrimary, this.StoreKind, this.CountTotal, this.CountInProgress, this.CountWriteRequestsSkipped, this.CountReadRequestsSkipped, this.CountStale, this.CountNotReady, this.CountFailedConstraints, this.CountFailedServerTimeout, latencyStatsAsXml, exceptionStatsAsXml, apiStatsAsXml, currentProcessName);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00017078 File Offset: 0x00015278
		private void RecordApiResult(OperationCategory category, bool isSuccess, long latencyInMs)
		{
			PerformanceEntry.ApiMeasure apiMeasure;
			if (!this.ApiMap.TryGetValue(category, out apiMeasure) || apiMeasure == null)
			{
				apiMeasure = new PerformanceEntry.ApiMeasure();
				this.ApiMap[category] = apiMeasure;
			}
			if (isSuccess)
			{
				apiMeasure.Succeeded++;
			}
			else
			{
				apiMeasure.Failed++;
			}
			apiMeasure.Latency.Update(latencyInMs);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000170D8 File Offset: 0x000152D8
		private void TrackLatency(OperationType operationType, long latencyInMs, bool isSuccess)
		{
			string latencyItemName = this.GetLatencyItemName(operationType, isSuccess);
			PerformanceEntry.LatencyMeasure latencyMeasure;
			if (!this.LatencyMap.TryGetValue(latencyItemName, out latencyMeasure) || latencyMeasure == null)
			{
				latencyMeasure = new PerformanceEntry.LatencyMeasure();
				this.LatencyMap[latencyItemName] = latencyMeasure;
			}
			latencyMeasure.Update(latencyInMs);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001711B File Offset: 0x0001531B
		private string GetLatencyItemName(OperationType operationType, bool isSucceeded)
		{
			return string.Format("{0}_{1}", operationType, isSucceeded ? "Succeeded" : "Failed");
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001713C File Offset: 0x0001533C
		private void TrackExceptions(Exception exception)
		{
			int num = 0;
			string name = exception.GetType().Name;
			this.ExceptionCountMap.TryGetValue(name, out num);
			num = (this.ExceptionCountMap[name] = num + 1);
			FaultException<DxStoreServerFault> faultException = exception as FaultException<DxStoreServerFault>;
			if (faultException != null)
			{
				DxStoreServerFault detail = faultException.Detail;
				if (detail != null)
				{
					switch (detail.FaultCode)
					{
					case DxStoreFaultCode.Stale:
						this.CountStale++;
						return;
					case DxStoreFaultCode.InstanceNotReady:
						this.CountNotReady++;
						return;
					case DxStoreFaultCode.ServerTimeout:
						this.CountFailedServerTimeout++;
						break;
					case DxStoreFaultCode.ConstraintNotSatisfied:
						this.CountFailedConstraints++;
						return;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x020000A6 RID: 166
		public class ApiMeasure
		{
			// Token: 0x06000632 RID: 1586 RVA: 0x000171E8 File Offset: 0x000153E8
			public ApiMeasure()
			{
				this.Latency = new PerformanceEntry.LatencyMeasure();
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000633 RID: 1587 RVA: 0x000171FB File Offset: 0x000153FB
			// (set) Token: 0x06000634 RID: 1588 RVA: 0x00017203 File Offset: 0x00015403
			public int Succeeded { get; set; }

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001720C File Offset: 0x0001540C
			// (set) Token: 0x06000636 RID: 1590 RVA: 0x00017214 File Offset: 0x00015414
			public int Failed { get; set; }

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001721D File Offset: 0x0001541D
			// (set) Token: 0x06000638 RID: 1592 RVA: 0x00017225 File Offset: 0x00015425
			public PerformanceEntry.LatencyMeasure Latency { get; set; }
		}

		// Token: 0x020000A7 RID: 167
		public class LatencyMeasure
		{
			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001722E File Offset: 0x0001542E
			// (set) Token: 0x0600063A RID: 1594 RVA: 0x00017236 File Offset: 0x00015436
			public long Count { get; set; }

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001723F File Offset: 0x0001543F
			// (set) Token: 0x0600063C RID: 1596 RVA: 0x00017247 File Offset: 0x00015447
			public long Total { get; set; }

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x0600063D RID: 1597 RVA: 0x00017250 File Offset: 0x00015450
			public double Average
			{
				get
				{
					if (this.Count > 0L)
					{
						return (double)this.Total / (double)this.Count;
					}
					return 0.0;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x00017275 File Offset: 0x00015475
			// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001727D File Offset: 0x0001547D
			public long Max { get; set; }

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000640 RID: 1600 RVA: 0x00017286 File Offset: 0x00015486
			// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001728E File Offset: 0x0001548E
			public long MaxMinusOne { get; set; }

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x00017297 File Offset: 0x00015497
			// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001729F File Offset: 0x0001549F
			public long Min { get; set; }

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x000172A8 File Offset: 0x000154A8
			// (set) Token: 0x06000645 RID: 1605 RVA: 0x000172B0 File Offset: 0x000154B0
			public long MinPlusOne { get; set; }

			// Token: 0x06000646 RID: 1606 RVA: 0x000172BC File Offset: 0x000154BC
			public void Update(long latency)
			{
				this.Count += 1L;
				if (this.Count == 1L)
				{
					this.Max = latency;
					this.Min = latency;
				}
				this.Total += latency;
				if (latency > this.Max)
				{
					this.MaxMinusOne = this.Max;
					this.Max = latency;
				}
				else if (latency < this.Max && latency > this.MaxMinusOne)
				{
					this.MaxMinusOne = latency;
				}
				if (latency < this.Min)
				{
					this.MinPlusOne = this.Min;
					this.Min = latency;
					return;
				}
				if (latency > this.Min && latency < this.MinPlusOne)
				{
					this.MinPlusOne = latency;
				}
			}
		}
	}
}
