using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000315 RID: 789
	internal class ReferralTree : TreeManager
	{
		// Token: 0x06001753 RID: 5971 RVA: 0x0006C315 File Offset: 0x0006A515
		public ReferralTree(ReferralTree.PathInsertedHandler pathInsertedHandler)
		{
			this.pathInsertedHandler = pathInsertedHandler;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0006C360 File Offset: 0x0006A560
		internal void InsertPaths(Node referralNode, TrackingAuthorityKind authorityKind, IEnumerable<List<RecipientTrackingEvent>> paths)
		{
			Dictionary<string, List<Node>> dictionary = new Dictionary<string, List<Node>>();
			foreach (IList<RecipientTrackingEvent> list in paths)
			{
				Node[] array = new Node[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					string text = list[i].RecipientAddress.ToString();
					string parentKey = (i == 0) ? list[i].RootAddress : text;
					array[i] = new Node(text, parentKey, list[i].Clone());
					if (i != 0)
					{
						array[i - 1].AddChild(array[i]);
					}
				}
				List<Node> list2;
				if (!dictionary.TryGetValue(array[0].ParentKey, out list2))
				{
					list2 = new List<Node>();
					dictionary.Add(array[0].ParentKey, list2);
				}
				list2.Add(array[0]);
			}
			foreach (string text2 in dictionary.Keys)
			{
				base.InsertAllChildrenForOneNode(text2, dictionary[text2], (Node parentNode, Node childNode) => this.Root == parentNode || parentNode == referralNode, (Node parent, Node child) => this.DoPostInsertionProcessing(parent, child, authorityKind));
			}
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0006C4FC File Offset: 0x0006A6FC
		protected override Node DisambiguateParentRecord(LinkedList<Node> possibleParents, Node node)
		{
			if (possibleParents == null || possibleParents.Count == 0)
			{
				return null;
			}
			return possibleParents.First.Value;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0006C518 File Offset: 0x0006A718
		protected override int GetNodePriorities(Node node, out int secondaryPriority)
		{
			RecipientTrackingEvent recipientTrackingEvent = (RecipientTrackingEvent)node.Value;
			EventDescriptionInformation eventDescriptionInformation;
			if (!EnumAttributeInfo<EventDescription, EventDescriptionInformation>.TryGetValue((int)recipientTrackingEvent.EventDescription, out eventDescriptionInformation))
			{
				throw new InvalidOperationException(string.Format("Value {0} was not annotated", Names<EventDescription>.Map[(int)recipientTrackingEvent.EventDescription]));
			}
			secondaryPriority = EventTree.GetSecondaryPriority(recipientTrackingEvent.BccRecipient, recipientTrackingEvent.HiddenRecipient, string.Equals(recipientTrackingEvent.RootAddress, (string)recipientTrackingEvent.RecipientAddress, StringComparison.OrdinalIgnoreCase));
			return eventDescriptionInformation.EventPriority;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0006C58C File Offset: 0x0006A78C
		protected override bool IsNodeRootChildCandidate(Node node)
		{
			return true;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0006C590 File Offset: 0x0006A790
		private Node DoPostInsertionProcessing(Node parent, Node child, TrackingAuthorityKind authorityKind)
		{
			this.ApplyInheritance(parent, child);
			Node node = child;
			while (node.HasChildren)
			{
				if (node.Children.Count != 1)
				{
					throw new InvalidOperationException(string.Format("Unexpected number of child nodes ({0}) in referral tree", node.Children.Count));
				}
				this.ApplyInheritance(node, node.Children[0]);
				node = node.Children[0];
			}
			this.pathInsertedHandler(node, authorityKind);
			return node;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0006C610 File Offset: 0x0006A810
		private void ApplyInheritance(Node parent, Node child)
		{
			RecipientTrackingEvent recipientTrackingEvent = (RecipientTrackingEvent)child.Value;
			if (parent == base.Root)
			{
				return;
			}
			RecipientTrackingEvent recipientTrackingEvent2 = (RecipientTrackingEvent)parent.Value;
			if (recipientTrackingEvent2.HiddenRecipient)
			{
				recipientTrackingEvent.HiddenRecipient = true;
			}
			recipientTrackingEvent.BccRecipient = recipientTrackingEvent2.BccRecipient;
			recipientTrackingEvent.RootAddress = recipientTrackingEvent2.RootAddress;
		}

		// Token: 0x04000ED4 RID: 3796
		private ReferralTree.PathInsertedHandler pathInsertedHandler;

		// Token: 0x02000316 RID: 790
		// (Invoke) Token: 0x0600175B RID: 5979
		public delegate void PathInsertedHandler(Node lastNodeInPath, TrackingAuthorityKind authorityKind);

		// Token: 0x02000317 RID: 791
		// (Invoke) Token: 0x0600175F RID: 5983
		public delegate bool IsNodePendingReferral(Node parent);
	}
}
