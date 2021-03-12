using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200031F RID: 799
	internal class QueueDiversity
	{
		// Token: 0x06002299 RID: 8857 RVA: 0x00082DB8 File Offset: 0x00080FB8
		private QueueDiversity()
		{
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x00082DE4 File Offset: 0x00080FE4
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x00082DEC File Offset: 0x00080FEC
		public QueueIdentity QueueId
		{
			get
			{
				return this.queueIdentity;
			}
			private set
			{
				this.queueIdentity = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x00082DF5 File Offset: 0x00080FF5
		// (set) Token: 0x0600229D RID: 8861 RVA: 0x00082DFD File Offset: 0x00080FFD
		public int TopCount
		{
			get
			{
				return this.topCount;
			}
			private set
			{
				this.topCount = value;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00082E06 File Offset: 0x00081006
		// (set) Token: 0x0600229F RID: 8863 RVA: 0x00082E0E File Offset: 0x0008100E
		public bool IncludeDeferred
		{
			get
			{
				return this.includeDeferred;
			}
			private set
			{
				this.includeDeferred = value;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x00082E17 File Offset: 0x00081017
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x00082E1F File Offset: 0x0008101F
		public int MaxDiversity
		{
			get
			{
				return this.maxDiversity;
			}
			private set
			{
				this.maxDiversity = value;
			}
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00082E28 File Offset: 0x00081028
		public static bool TryParse(string requestArgument, bool implicitShadow, out QueueDiversity queueDiversity, out string parseError)
		{
			QueueIdentity queueIdentity = QueueIdentity.SubmissionQueueIdentity;
			int num = 3;
			int num2 = 10000;
			bool flag = true;
			queueDiversity = null;
			if (!string.IsNullOrEmpty(requestArgument))
			{
				Match match = QueueDiversity.RegexParser.Match(requestArgument);
				if (!match.Success || match.Value != requestArgument)
				{
					parseError = string.Format("Invalid argument format or options detected in '{0}'.", requestArgument);
					return false;
				}
				Group group = match.Groups["queueId"];
				if (group.Success && !string.IsNullOrEmpty(group.Value))
				{
					queueIdentity = QueueIdentity.Parse(group.Value, implicitShadow);
					if (!queueIdentity.IsLocal)
					{
						parseError = string.Format("Invalid use of server name in the QueueID: {0}. Please use -Server parameter to specify server name.", queueIdentity.Server);
						return false;
					}
					if (queueIdentity.Type == QueueType.Poison)
					{
						parseError = "Queue Diversity does not support retrieving information from Poison Queue at this point.";
						return false;
					}
				}
				Group group2 = match.Groups["top"];
				if (group2.Success && !int.TryParse(group2.Value, out num))
				{
					parseError = string.Format("Invalid option topCount '{0}' specified.", group2.Value);
					return false;
				}
				Group group3 = match.Groups["max"];
				if (group3.Success && !string.IsNullOrEmpty(group3.Value) && !int.TryParse(group3.Value, out num2))
				{
					parseError = string.Format("Invalid option maxDiversity '{0}' specified.", group3.Value);
					return false;
				}
				Group group4 = match.Groups["skipDeferred"];
				if (group4.Success && !string.IsNullOrEmpty(group4.Value))
				{
					flag = false;
				}
			}
			parseError = string.Empty;
			queueDiversity = new QueueDiversity
			{
				IncludeDeferred = flag,
				QueueId = queueIdentity,
				TopCount = num,
				MaxDiversity = num2
			};
			return true;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00082FDC File Offset: 0x000811DC
		public XElement GetDiagnosticInfo(MessageQueue messageQueue)
		{
			XElement xelement = new XElement("QueueDiversity");
			if (this.QueueId.Type != QueueType.Delivery && this.QueueId.Type != QueueType.Submission && this.QueueId.Type != QueueType.Unreachable)
			{
				xelement.Add(new XElement("Mismatch", string.Format("Type Mismatch usage between message queue and QueueIdentity. Current QueueId is {0}", this.QueueId)));
			}
			else if (messageQueue != null && messageQueue.TotalCount > 0)
			{
				this.AddQueueInfo(xelement, messageQueue.TotalCount, messageQueue.DeferredCount);
				this.ReportDiversity(xelement, this.GetDiversityInformation<MessageQueue>(messageQueue));
			}
			else
			{
				this.AddQueueInfo(xelement, 0, 0);
			}
			return xelement;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00083084 File Offset: 0x00081284
		public XElement GetDiagnosticInfo(ShadowMessageQueue messageQueue)
		{
			XElement xelement = new XElement("QueueDiversity");
			if (this.QueueId.Type != QueueType.Shadow)
			{
				xelement.Add(new XElement("Mismatch", string.Format("Type Mismatch usage between message queue and QueueIdentity. Current QueueId is {0}", this.QueueId)));
			}
			else if (messageQueue != null && messageQueue.Count > 0)
			{
				this.AddQueueInfo(xelement, messageQueue.Count, 0);
				this.ReportDiversity(xelement, this.GetDiversityInformation<ShadowMessageQueue>(messageQueue));
			}
			else
			{
				this.AddQueueInfo(xelement, 0, 0);
			}
			return xelement;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0008310C File Offset: 0x0008130C
		public XElement GetComponentAdvice()
		{
			string componentName = QueueDiversity.GetComponentName(this.QueueId.Type);
			string content;
			if (!string.IsNullOrEmpty(componentName))
			{
				content = string.Format("Please use '-component {0}' parameter to retrieve diversity information of QueueId:'{1}'", QueueDiversity.GetComponentName(this.QueueId.Type), this.QueueId.ToString());
			}
			else
			{
				content = string.Format("Queue Diversity can't handle queue identity of QueueId:'{0}'.", this.QueueId.ToString());
			}
			return new XElement("ComponentError", content);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00083180 File Offset: 0x00081380
		private static IList<KeyValuePair<string, int>> TakeTop(ICollection<KeyValuePair<string, int>> items, int topCount)
		{
			if (items.Count <= 0)
			{
				return null;
			}
			QueueDiversity.ReverseCompare reverseCompare = new QueueDiversity.ReverseCompare();
			SortedList<KeyValuePair<string, int>, int> sortedList = new SortedList<KeyValuePair<string, int>, int>(reverseCompare);
			foreach (KeyValuePair<string, int> keyValuePair in items)
			{
				if (sortedList.Count < topCount)
				{
					sortedList.Add(keyValuePair, keyValuePair.Value);
				}
				else if (reverseCompare.Compare(keyValuePair, sortedList.Keys[topCount - 1]) < 0)
				{
					sortedList.Add(keyValuePair, keyValuePair.Value);
					sortedList.RemoveAt(topCount);
				}
			}
			return sortedList.Keys;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x00083228 File Offset: 0x00081428
		private static XElement GetDiagnosticInfo(IEnumerable<KeyValuePair<string, int>> items, string rootName, string itemName, bool resultCompacted)
		{
			if (items == null)
			{
				return null;
			}
			XElement xelement = new XElement(rootName);
			if (resultCompacted)
			{
				string content = string.Format("Count may not be accurate. Use option '{0}:' to set a larger memory allowance for more accurate result.", "max");
				xelement.Add(new XElement("Warning", content));
			}
			foreach (KeyValuePair<string, int> keyValuePair in items)
			{
				XElement xelement2 = new XElement(itemName);
				xelement2.SetAttributeValue("Id", keyValuePair.Key);
				xelement2.SetAttributeValue("Count", keyValuePair.Value);
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000832F0 File Offset: 0x000814F0
		private static string GetComponentName(QueueType queueType)
		{
			switch (queueType)
			{
			case QueueType.Delivery:
			case QueueType.Unreachable:
				return "RemoteDelivery";
			case QueueType.Submission:
				return "Categorizer";
			case QueueType.Shadow:
				return "ShadowRedundancy";
			}
			return null;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00083354 File Offset: 0x00081554
		private void AddOrUpdatePropertyCount(IDictionary<string, int> items, string property, ref bool compacted)
		{
			if (items.ContainsKey(property))
			{
				items[property]++;
				return;
			}
			if (items.Count == this.MaxDiversity)
			{
				int smallestCount = items.Min((KeyValuePair<string, int> item) => item.Value);
				IList<KeyValuePair<string, int>> list = (from item in items
				where item.Value == smallestCount
				select item).ToList<KeyValuePair<string, int>>();
				foreach (KeyValuePair<string, int> item2 in list)
				{
					items.Remove(item2);
				}
				compacted = true;
			}
			items.Add(property, 1);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00083418 File Offset: 0x00081618
		private void AddOrUpdateRecipientCount(IDictionary<string, int> items, IReadOnlyMailItem mailItem, ref bool compacted)
		{
			IEnumerable<MailRecipient> all = mailItem.Recipients.All;
			foreach (MailRecipient mailRecipient in all)
			{
				this.AddOrUpdatePropertyCount(items, mailRecipient.ToString(), ref compacted);
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00083474 File Offset: 0x00081674
		private void AddQueueInfo(XElement diversity, int totalCount, int deferredCount)
		{
			XElement xelement = new XElement("Queue");
			xelement.SetAttributeValue("Id", this.QueueId.ToString());
			xelement.SetAttributeValue("IncludeDeferred", this.IncludeDeferred.ToString());
			xelement.SetAttributeValue("TotalCount", totalCount);
			xelement.SetAttributeValue("DeferredCount", deferredCount);
			diversity.Add(xelement);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x00083500 File Offset: 0x00081700
		private void ReportDiversity(XElement diversity, IList<KeyValuePair<string, int>>[] topRepeatingTypes)
		{
			diversity.Add(QueueDiversity.GetDiagnosticInfo(topRepeatingTypes[0], "Organizations", "Organization", this.orgsCompacted));
			diversity.Add(QueueDiversity.GetDiagnosticInfo(topRepeatingTypes[1], "Senders", "Sender", this.sendersCompacted));
			diversity.Add(QueueDiversity.GetDiagnosticInfo(topRepeatingTypes[2], "Recipients", "Recipient", this.recipientsCompacted));
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x00083608 File Offset: 0x00081808
		private IList<KeyValuePair<string, int>>[] GetDiversityInformation<T>(T queue) where T : IQueueVisitor
		{
			Dictionary<string, int>[] groups = new Dictionary<string, int>[]
			{
				new Dictionary<string, int>(),
				new Dictionary<string, int>(),
				new Dictionary<string, int>()
			};
			this.orgsCompacted = false;
			this.sendersCompacted = false;
			this.recipientsCompacted = false;
			queue.ForEach(delegate(IQueueItem mailItem)
			{
				this.AddOrUpdatePropertyCount(groups[0], ((IReadOnlyMailItem)mailItem).ExternalOrganizationId.ToString(), ref this.orgsCompacted);
				this.AddOrUpdatePropertyCount(groups[1], (string)((IReadOnlyMailItem)mailItem).From, ref this.sendersCompacted);
				this.AddOrUpdateRecipientCount(groups[2], (IReadOnlyMailItem)mailItem, ref this.recipientsCompacted);
			}, this.IncludeDeferred);
			return new IList<KeyValuePair<string, int>>[]
			{
				QueueDiversity.TakeTop(groups[0], this.TopCount),
				QueueDiversity.TakeTop(groups[1], this.TopCount),
				QueueDiversity.TakeTop(groups[2], this.TopCount)
			};
		}

		// Token: 0x04001208 RID: 4616
		public const string DiversityArgumentName = "diversity";

		// Token: 0x04001209 RID: 4617
		private const string SkipDeferredOptionName = "skipDeferred";

		// Token: 0x0400120A RID: 4618
		private const string QueueIdOptionName = "queueId";

		// Token: 0x0400120B RID: 4619
		private const string TopCountOptionName = "top";

		// Token: 0x0400120C RID: 4620
		private const string MaxDiversityOptionName = "max";

		// Token: 0x0400120D RID: 4621
		private const int DefaultTopCount = 3;

		// Token: 0x0400120E RID: 4622
		private const int DefaultMaxDiversity = 10000;

		// Token: 0x0400120F RID: 4623
		public static readonly string UsageString = string.Format("[queueId][/[[{0}:]{{topCount}},][{1}:{{maxDiversity}},][{2}]]", "top", "max", "skipDeferred");

		// Token: 0x04001210 RID: 4624
		private static readonly string RegexQueueIdPart = string.Format("\\:*\\s*(?<{0}>[^/\\s]*)", "queueId");

		// Token: 0x04001211 RID: 4625
		private static readonly string RegexMaxDiversityPart = string.Format("({0}\\:(?<{0}>\\d+))", "max");

		// Token: 0x04001212 RID: 4626
		private static readonly string RegexTopCountPart = string.Format("(({0}\\:)*(?<{0}>\\d+))", "top");

		// Token: 0x04001213 RID: 4627
		private static readonly string RegexSkipDeferredPart = string.Format("(?<{0}>{0})", "skipDeferred");

		// Token: 0x04001214 RID: 4628
		private static readonly string RegexString = string.Format("\\s*{0}\\s*(/{{1}}\\s*(({1}|{2}|{3})[,\\s]*){{0,3}})?", new object[]
		{
			QueueDiversity.RegexQueueIdPart,
			QueueDiversity.RegexMaxDiversityPart,
			QueueDiversity.RegexTopCountPart,
			QueueDiversity.RegexSkipDeferredPart
		});

		// Token: 0x04001215 RID: 4629
		private static readonly Regex RegexParser = new Regex(QueueDiversity.RegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04001216 RID: 4630
		private bool includeDeferred = true;

		// Token: 0x04001217 RID: 4631
		private QueueIdentity queueIdentity = QueueIdentity.SubmissionQueueIdentity;

		// Token: 0x04001218 RID: 4632
		private int topCount = 3;

		// Token: 0x04001219 RID: 4633
		private int maxDiversity = 10000;

		// Token: 0x0400121A RID: 4634
		private bool orgsCompacted;

		// Token: 0x0400121B RID: 4635
		private bool sendersCompacted;

		// Token: 0x0400121C RID: 4636
		private bool recipientsCompacted;

		// Token: 0x02000320 RID: 800
		private enum GroupBy
		{
			// Token: 0x0400121F RID: 4639
			Tenants,
			// Token: 0x04001220 RID: 4640
			Senders,
			// Token: 0x04001221 RID: 4641
			Recipients
		}

		// Token: 0x02000321 RID: 801
		private class ReverseCompare : IComparer<KeyValuePair<string, int>>
		{
			// Token: 0x060022B0 RID: 8880 RVA: 0x0008378B File Offset: 0x0008198B
			public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
			{
				if (x.Value != y.Value)
				{
					return y.Value - x.Value;
				}
				return string.Compare(y.Key, x.Key, StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
