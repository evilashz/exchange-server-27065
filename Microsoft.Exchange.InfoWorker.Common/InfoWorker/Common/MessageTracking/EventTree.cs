using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000312 RID: 786
	internal class EventTree : TreeManager
	{
		// Token: 0x0600173C RID: 5948 RVA: 0x0006BACC File Offset: 0x00069CCC
		private static Dictionary<MessageTrackingEvent, int> InitEventPrioritiesForBestResultLeafHa()
		{
			return new Dictionary<MessageTrackingEvent, int>(EventTree.EventPrioritiesForBestResultLeaf)
			{
				{
					MessageTrackingEvent.HAREDIRECT,
					6
				}
			};
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0006BAF8 File Offset: 0x00069CF8
		public override ICollection<Node> GetPathToLeaf(string leafId)
		{
			this.deprioritizeHa = false;
			ICollection<Node> pathToLeaf = base.GetPathToLeaf(leafId);
			if (pathToLeaf.Count == 0)
			{
				return pathToLeaf;
			}
			MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)pathToLeaf.Last<Node>().Value;
			if (messageTrackingLogEntry.EventId != MessageTrackingEvent.HAREDIRECT)
			{
				return pathToLeaf;
			}
			this.deprioritizeHa = true;
			ICollection<Node> pathToLeaf2 = base.GetPathToLeaf(leafId);
			if (pathToLeaf2.Count == 0)
			{
				MessageTrackingLogEntry messageTrackingLogEntry2 = (MessageTrackingLogEntry)pathToLeaf2.Last<Node>().Value;
				if (messageTrackingLogEntry2 != messageTrackingLogEntry)
				{
					pathToLeaf2.Add(pathToLeaf.Last<Node>());
				}
			}
			return pathToLeaf2;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0006BB74 File Offset: 0x00069D74
		internal static int GetSecondaryPriority(bool bccRecipient, bool hiddenRecipient, bool recipientMatchesRoot)
		{
			int num = 0;
			if (bccRecipient)
			{
				num++;
			}
			if (hiddenRecipient)
			{
				num += 2;
			}
			if (!recipientMatchesRoot)
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0006BB9C File Offset: 0x00069D9C
		protected override Node DisambiguateParentRecord(LinkedList<Node> possibleParents, Node node)
		{
			MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)node.Value;
			if (possibleParents == null || possibleParents.Count == 0)
			{
				return null;
			}
			MessageTrackingEvent eventId = messageTrackingLogEntry.EventId;
			if (eventId <= MessageTrackingEvent.REDIRECT)
			{
				switch (eventId)
				{
				case MessageTrackingEvent.RECEIVE:
					return this.FindParentMatchingParameters(messageTrackingLogEntry, null, EventTree.receiveEventMatchingTypes, EventTree.EventMatchingCondition.MustMatch, null, possibleParents);
				case MessageTrackingEvent.SEND:
					break;
				case MessageTrackingEvent.FAIL:
					return this.FindParentMatchingParameters(messageTrackingLogEntry, new long?(messageTrackingLogEntry.InternalMessageId), EventTree.beginModerationEventType, EventTree.EventMatchingCondition.MustNotMatch, null, possibleParents);
				default:
					if (eventId == MessageTrackingEvent.REDIRECT)
					{
						if (messageTrackingLogEntry.RecipientAddresses == null || messageTrackingLogEntry.RecipientAddresses.Length == 0)
						{
							return null;
						}
						foreach (Node node2 in possibleParents)
						{
							MessageTrackingLogEntry messageTrackingLogEntry2 = (MessageTrackingLogEntry)node2.Value;
							if (messageTrackingLogEntry2.EventId == MessageTrackingEvent.REDIRECT && messageTrackingLogEntry2.RelatedRecipientAddress != null && !messageTrackingLogEntry2.RelatedRecipientAddress.Equals(messageTrackingLogEntry2.RecipientAddresses[0]))
							{
								return node2;
							}
						}
						return this.FindParentMatchingParameters(messageTrackingLogEntry, null, EventTree.redirectEventType, EventTree.EventMatchingCondition.MustNotMatch, null, possibleParents);
					}
					break;
				}
			}
			else
			{
				switch (eventId)
				{
				case MessageTrackingEvent.DUPLICATEDELIVER:
					return this.FindParentMatchingParameters(messageTrackingLogEntry, new long?(messageTrackingLogEntry.ServerLogKeyMailItemId), EventTree.deliveryEventTypes, EventTree.EventMatchingCondition.MustNotMatch, null, possibleParents);
				case MessageTrackingEvent.RESUBMIT:
					if (messageTrackingLogEntry.Source == MessageTrackingSource.REDUNDANCY)
					{
						return this.FindParentMatchingParameters(messageTrackingLogEntry, null, EventTree.haResubmitEventMatchingTypes, EventTree.EventMatchingCondition.MustMatch, null, possibleParents);
					}
					break;
				case MessageTrackingEvent.INITMESSAGECREATED:
					break;
				case MessageTrackingEvent.MODERATORREJECT:
					return this.FindParentMatchingParameters(messageTrackingLogEntry, null, EventTree.beginModerationEventType, EventTree.EventMatchingCondition.MustMatch, null, possibleParents);
				case MessageTrackingEvent.MODERATORAPPROVE:
					return this.FindParentMatchingParameters(messageTrackingLogEntry, null, EventTree.beginModerationEventType, EventTree.EventMatchingCondition.MustMatch, null, possibleParents);
				default:
					if (eventId == MessageTrackingEvent.HAREDIRECT)
					{
						return null;
					}
					if (eventId == MessageTrackingEvent.HARECEIVE)
					{
						return this.FindParentMatchingParameters(messageTrackingLogEntry, null, EventTree.haReceiveEventMatchingTypes, EventTree.EventMatchingCondition.MustMatch, null, possibleParents);
					}
					break;
				}
			}
			return this.FindParentMatchingParameters(messageTrackingLogEntry, new long?(messageTrackingLogEntry.ServerLogKeyMailItemId), null, EventTree.EventMatchingCondition.MustMatch, null, possibleParents);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0006BDF4 File Offset: 0x00069FF4
		private static int GetEventPriorityForBestResultLeaf(MessageTrackingLogEntry logEntry, bool deprioritizeHa)
		{
			int result;
			bool flag;
			if (!deprioritizeHa)
			{
				flag = EventTree.EventPrioritiesForBestResultLeafHa.TryGetValue(logEntry.EventId, out result);
			}
			else
			{
				flag = EventTree.EventPrioritiesForBestResultLeaf.TryGetValue(logEntry.EventId, out result);
			}
			if (!flag)
			{
				return int.MaxValue;
			}
			return result;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0006BE38 File Offset: 0x0006A038
		private static int GetSecondaryPriority(Node node, bool forPathPicking)
		{
			MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)node.Value;
			bool recipientMatchesRoot = !forPathPicking || string.Equals(node.Key, messageTrackingLogEntry.RootAddress, StringComparison.OrdinalIgnoreCase);
			return EventTree.GetSecondaryPriority(messageTrackingLogEntry.BccRecipient == null || messageTrackingLogEntry.BccRecipient.Value, messageTrackingLogEntry.HiddenRecipient, recipientMatchesRoot);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0006BE97 File Offset: 0x0006A097
		internal bool InsertAllChildrenForOneNode(string parentKey, IList<Node> nodes)
		{
			return base.InsertAllChildrenForOneNode(parentKey, nodes, null, new TreeManager.DoPostInsertionProcessingDelegate(this.DoPostInsertionProcessing));
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0006BEAE File Offset: 0x0006A0AE
		internal bool Insert(Node node)
		{
			return base.Insert(node, null, new TreeManager.DoPostInsertionProcessingDelegate(this.DoPostInsertionProcessing));
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0006BEC4 File Offset: 0x0006A0C4
		protected override int GetNodePriorities(Node node, out int secondaryPriority)
		{
			MessageTrackingLogEntry logEntry = (MessageTrackingLogEntry)node.Value;
			secondaryPriority = EventTree.GetSecondaryPriority(node, true);
			return EventTree.GetEventPriorityForBestResultLeaf(logEntry, this.deprioritizeHa);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0006BEF4 File Offset: 0x0006A0F4
		private Node DoPostInsertionProcessing(Node parent, Node child)
		{
			MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)child.Value;
			if (parent == base.Root)
			{
				messageTrackingLogEntry.RootAddress = child.Key;
				return child;
			}
			MessageTrackingLogEntry messageTrackingLogEntry2 = (MessageTrackingLogEntry)parent.Value;
			messageTrackingLogEntry.RootAddress = messageTrackingLogEntry2.RootAddress;
			if (messageTrackingLogEntry2.HiddenRecipient)
			{
				messageTrackingLogEntry.HiddenRecipient = true;
			}
			if (messageTrackingLogEntry2.BccRecipient != null && messageTrackingLogEntry.BccRecipient == null)
			{
				messageTrackingLogEntry.BccRecipient = messageTrackingLogEntry2.BccRecipient;
			}
			return child;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0006BF78 File Offset: 0x0006A178
		protected override bool IsNodeRootChildCandidate(Node node)
		{
			MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)node.Value;
			return messageTrackingLogEntry.EventId == MessageTrackingEvent.SUBMIT || messageTrackingLogEntry.EventId == MessageTrackingEvent.RECEIVE || (messageTrackingLogEntry.EventId == MessageTrackingEvent.FAIL && messageTrackingLogEntry.Source == MessageTrackingSource.SMTP) || (messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORAPPROVE && messageTrackingLogEntry.Source == MessageTrackingSource.APPROVAL) || (messageTrackingLogEntry.EventId == MessageTrackingEvent.MODERATORREJECT && messageTrackingLogEntry.Source == MessageTrackingSource.APPROVAL) || messageTrackingLogEntry.EventId == MessageTrackingEvent.REDIRECT || messageTrackingLogEntry.EventId == MessageTrackingEvent.HAREDIRECT || messageTrackingLogEntry.EventId == MessageTrackingEvent.HARECEIVE || messageTrackingLogEntry.EventId == MessageTrackingEvent.DELIVER || messageTrackingLogEntry.EventId == MessageTrackingEvent.DUPLICATEDELIVER;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0006C010 File Offset: 0x0006A210
		private Node FindParentMatchingParameters(MessageTrackingLogEntry childEntryValue, long? mailItemId, MessageTrackingEvent[] eventTypes, EventTree.EventMatchingCondition eventMatchingCondition, MessageTrackingSource? source, LinkedList<Node> possibleParents)
		{
			Node node = null;
			foreach (Node node2 in possibleParents)
			{
				MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)node2.Value;
				if (messageTrackingLogEntry != childEntryValue && !messageTrackingLogEntry.SharesRowDataWithEntry(childEntryValue) && string.IsNullOrEmpty(messageTrackingLogEntry.FederatedDeliveryAddress))
				{
					if (mailItemId != null)
					{
						long num;
						if (messageTrackingLogEntry.EventId == MessageTrackingEvent.TRANSFER || messageTrackingLogEntry.EventId == MessageTrackingEvent.RESUBMIT)
						{
							num = messageTrackingLogEntry.InternalMessageId;
						}
						else
						{
							num = messageTrackingLogEntry.ServerLogKeyMailItemId;
						}
						if (mailItemId != num)
						{
							continue;
						}
					}
					if (eventTypes != null)
					{
						bool flag = eventMatchingCondition == EventTree.EventMatchingCondition.MustMatch;
						bool flag2 = !flag;
						foreach (MessageTrackingEvent messageTrackingEvent in eventTypes)
						{
							if (messageTrackingEvent == messageTrackingLogEntry.EventId)
							{
								flag2 = flag;
								break;
							}
						}
						if (!flag2)
						{
							continue;
						}
					}
					if ((source == null || source.Value == messageTrackingLogEntry.Source) && (node == null || EventTree.GetSecondaryPriority(node, false) >= EventTree.GetSecondaryPriority(node2, false)))
					{
						node = node2;
					}
				}
			}
			return node;
		}

		// Token: 0x04000EC3 RID: 3779
		private static readonly Dictionary<MessageTrackingEvent, int> EventPrioritiesForBestResultLeaf = new Dictionary<MessageTrackingEvent, int>
		{
			{
				MessageTrackingEvent.DELIVER,
				0
			},
			{
				MessageTrackingEvent.DUPLICATEDELIVER,
				0
			},
			{
				MessageTrackingEvent.MODERATORAPPROVE,
				1
			},
			{
				MessageTrackingEvent.MODERATORREJECT,
				2
			},
			{
				MessageTrackingEvent.FAIL,
				3
			},
			{
				MessageTrackingEvent.PROCESS,
				4
			},
			{
				MessageTrackingEvent.SEND,
				5
			}
		};

		// Token: 0x04000EC4 RID: 3780
		private static readonly Dictionary<MessageTrackingEvent, int> EventPrioritiesForBestResultLeafHa = EventTree.InitEventPrioritiesForBestResultLeafHa();

		// Token: 0x04000EC5 RID: 3781
		private static MessageTrackingEvent[] deliveryEventTypes = new MessageTrackingEvent[]
		{
			MessageTrackingEvent.DELIVER,
			MessageTrackingEvent.DUPLICATEDELIVER
		};

		// Token: 0x04000EC6 RID: 3782
		private static MessageTrackingEvent[] beginModerationEventType = new MessageTrackingEvent[]
		{
			MessageTrackingEvent.INITMESSAGECREATED
		};

		// Token: 0x04000EC7 RID: 3783
		private static MessageTrackingEvent[] receiveEventMatchingTypes = new MessageTrackingEvent[]
		{
			MessageTrackingEvent.MODERATORAPPROVE,
			MessageTrackingEvent.SEND,
			MessageTrackingEvent.SUBMIT
		};

		// Token: 0x04000EC8 RID: 3784
		private static MessageTrackingEvent[] redirectEventType = new MessageTrackingEvent[]
		{
			MessageTrackingEvent.REDIRECT
		};

		// Token: 0x04000EC9 RID: 3785
		private static MessageTrackingEvent[] haReceiveEventMatchingTypes = new MessageTrackingEvent[]
		{
			MessageTrackingEvent.HAREDIRECT
		};

		// Token: 0x04000ECA RID: 3786
		private static MessageTrackingEvent[] haResubmitEventMatchingTypes = new MessageTrackingEvent[]
		{
			MessageTrackingEvent.HARECEIVE
		};

		// Token: 0x04000ECB RID: 3787
		private bool deprioritizeHa;

		// Token: 0x02000313 RID: 787
		private enum EventMatchingCondition
		{
			// Token: 0x04000ECD RID: 3789
			MustMatch,
			// Token: 0x04000ECE RID: 3790
			MustNotMatch
		}
	}
}
