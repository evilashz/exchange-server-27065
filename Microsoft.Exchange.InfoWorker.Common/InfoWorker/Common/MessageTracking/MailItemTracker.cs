﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002CB RID: 715
	internal class MailItemTracker
	{
		// Token: 0x060013FD RID: 5117 RVA: 0x0005D955 File Offset: 0x0005BB55
		public MailItemTracker(List<MessageTrackingLogEntry> entries, EventTree tree)
		{
			this.tree = tree;
			this.entriesForMailItem = entries;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0005D978 File Offset: 0x0005BB78
		public List<MessageTrackingLogEntry> ProcessEntries()
		{
			List<MessageTrackingLogEntry> list = new List<MessageTrackingLogEntry>();
			if (this.entriesForMailItem == null || this.entriesForMailItem.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No entries for tracker, finishing the tree at this node", new object[0]);
				return list;
			}
			this.FixupFederatedDeliveryEvents();
			this.entriesForMailItem.Sort((MessageTrackingLogEntry left, MessageTrackingLogEntry right) => this.LogEntryComparer(left, right));
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in this.entriesForMailItem)
			{
				if (!this.ShouldSkipEvent(messageTrackingLogEntry))
				{
					messageTrackingLogEntry.ProcessedBy = this.tree;
					MessageTrackingEvent eventId = messageTrackingLogEntry.EventId;
					if (messageTrackingLogEntry.EventId == MessageTrackingEvent.SUBMIT)
					{
						if (messageTrackingLogEntry.RecipientAddresses == null || messageTrackingLogEntry.RecipientAddresses.Length == 0)
						{
							messageTrackingLogEntry.RecipientAddresses = MailItemTracker.emptyRecipientList;
						}
						if (this.tree.Root != null)
						{
							list.Add(messageTrackingLogEntry);
							return list;
						}
					}
					bool flag;
					if (eventId == MessageTrackingEvent.EXPAND || eventId == MessageTrackingEvent.RESOLVE)
					{
						string text = messageTrackingLogEntry.RelatedRecipientAddress;
						if (messageTrackingLogEntry.RecipientAddresses == null || messageTrackingLogEntry.RecipientAddresses.Length == 0)
						{
							TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No recipients in EXPAND event.  Removing parent from leaf node dictionary.", new object[0]);
							this.tree.RemoveKeyFromLeafSet(text);
							continue;
						}
						int num = messageTrackingLogEntry.RecipientAddresses.Length;
						Node[] array = new Node[num];
						for (int i = 0; i < num; i++)
						{
							string key = messageTrackingLogEntry.RecipientAddresses[i];
							Node node = new Node(key, text, messageTrackingLogEntry.Clone());
							array[i] = node;
						}
						flag = this.tree.InsertAllChildrenForOneNode(text, array);
						if (flag && eventId == MessageTrackingEvent.EXPAND && string.IsNullOrEmpty(messageTrackingLogEntry.FederatedDeliveryAddress))
						{
							this.tree.DropRecipientDueToPotentialDuplication(text);
						}
					}
					else
					{
						if (messageTrackingLogEntry.RecipientAddresses == null || messageTrackingLogEntry.RecipientAddresses.Length == 0)
						{
							TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingLogEntry>(this.GetHashCode(), "A Message Tracking Log Entry of type other than SUBMIT or EXPAND had no recipients: {0}.\n", messageTrackingLogEntry);
							continue;
						}
						bool entryIsStoreDriverReceiveEvent = eventId == MessageTrackingEvent.RECEIVE && messageTrackingLogEntry.Source == MessageTrackingSource.STOREDRIVER;
						bool entryIsAgentReceiveEvent = eventId == MessageTrackingEvent.RECEIVE && messageTrackingLogEntry.Source == MessageTrackingSource.AGENT;
						int num2 = 0;
						HashSet<string> hashSet = null;
						if (eventId == MessageTrackingEvent.RECEIVE)
						{
							hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
						}
						for (int j = 0; j < messageTrackingLogEntry.RecipientAddresses.Length; j++)
						{
							if (hashSet == null || hashSet.Add(messageTrackingLogEntry.RecipientAddresses[j]))
							{
								string text = messageTrackingLogEntry.RecipientAddresses[j];
								Node node2;
								if (eventId == MessageTrackingEvent.REDIRECT)
								{
									node2 = new Node(messageTrackingLogEntry.RelatedRecipientAddress, text, messageTrackingLogEntry.Clone());
								}
								else
								{
									node2 = new Node(messageTrackingLogEntry.RecipientAddresses[j], text, messageTrackingLogEntry.Clone());
								}
								MessageTrackingLogEntry messageTrackingLogEntry2 = (MessageTrackingLogEntry)node2.Value;
								this.SetPerRecipientProperties(node2, j, entryIsStoreDriverReceiveEvent, entryIsAgentReceiveEvent);
								if (this.tree.Insert(node2))
								{
									num2++;
								}
							}
						}
						flag = (num2 == messageTrackingLogEntry.RecipientAddresses.Length);
					}
					if (!flag && ServerCache.Instance.ReportNonFatalBugs)
					{
						string extraData = string.Format(CultureInfo.InvariantCulture, "Not all entries can be inserted into the tree. total number of entries is {0}, Message id = {1}, server = {2}", new object[]
						{
							this.entriesForMailItem.Count,
							this.entriesForMailItem[0].MessageId,
							this.entriesForMailItem[0].Server
						});
						DiagnosticWatson.SendWatsonWithoutCrash(new InvalidOperationException(), "RecipInsertFailure", TimeSpan.FromHours(3.0), extraData);
					}
					if (messageTrackingLogEntry.IsTerminalEvent)
					{
						list.Add(messageTrackingLogEntry);
					}
				}
			}
			return list;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0005DD18 File Offset: 0x0005BF18
		private void SetHideRecipientAndBccPropertiesForAgentEvent(MessageTrackingLogEntry logEntry)
		{
			bool flag = true;
			bool flag2 = false;
			KeyValuePair<string, object>[] customData = logEntry.CustomData;
			if (customData == null || customData.Length == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Agent added recipient does not have value in CustomData field, hide recipient.", new object[0]);
			}
			else
			{
				foreach (KeyValuePair<string, object> keyValuePair in customData)
				{
					if ("RecipientType".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						string text = keyValuePair.Value as string;
						if (!string.IsNullOrEmpty(text))
						{
							flag = (!"To".Equals(text, StringComparison.OrdinalIgnoreCase) && !"Cc".Equals(text, StringComparison.OrdinalIgnoreCase) && !"Redirect".Equals(text, StringComparison.OrdinalIgnoreCase));
							if ("Bcc".Equals(text, StringComparison.OrdinalIgnoreCase) || "Unknown".Equals(text, StringComparison.OrdinalIgnoreCase))
							{
								flag2 = true;
							}
							TraceWrapper.SearchLibraryTracer.TraceDebug<string, bool, bool>(this.GetHashCode(), "Tracking log entry contained '{0}' for RecipientType property. hiddenRecip={1}, bccRecip={2}", text, flag, flag2);
							break;
						}
						TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Tracking log entry contained null or invalid value for RecipientTypePropertyName property: {0}.", new object[]
						{
							keyValuePair.Value
						});
					}
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<bool, bool>(this.GetHashCode(), "Hidden recipient is {0} and bccRecip is {1}", flag, flag2);
			logEntry.BccRecipient = new bool?(flag2);
			logEntry.HiddenRecipient = flag;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0005DE74 File Offset: 0x0005C074
		private void SetPerRecipientProperties(Node newNode, int recipIndex, bool entryIsStoreDriverReceiveEvent, bool entryIsAgentReceiveEvent)
		{
			MessageTrackingLogEntry messageTrackingLogEntry = (MessageTrackingLogEntry)newNode.Value;
			if (messageTrackingLogEntry.RecipientStatuses != null && messageTrackingLogEntry.RecipientStatuses.Length > recipIndex)
			{
				messageTrackingLogEntry.RecipientStatus = messageTrackingLogEntry.RecipientStatuses[recipIndex];
			}
			if (!entryIsStoreDriverReceiveEvent)
			{
				if (entryIsAgentReceiveEvent)
				{
					this.SetHideRecipientAndBccPropertiesForAgentEvent(messageTrackingLogEntry);
				}
				return;
			}
			MimeRecipientType mimeRecipientType;
			if (!MessageTrackingLogEntry.RecipientAddressTypeGetter.TryGetValue(messageTrackingLogEntry.RecipientStatus, out mimeRecipientType))
			{
				mimeRecipientType = MimeRecipientType.Unknown;
			}
			if (mimeRecipientType == MimeRecipientType.Unknown || mimeRecipientType == MimeRecipientType.Bcc)
			{
				messageTrackingLogEntry.BccRecipient = new bool?(true);
				return;
			}
			messageTrackingLogEntry.BccRecipient = new bool?(false);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0005DEF4 File Offset: 0x0005C0F4
		private bool ShouldSkipEvent(MessageTrackingLogEntry entry)
		{
			if (this.tree == entry.ProcessedBy)
			{
				return true;
			}
			string text = entry.SourceContext;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			if ("Federated Delivery Encryption Agent".Equals(text, StringComparison.OrdinalIgnoreCase) && entry.EventId == MessageTrackingEvent.DEFER)
			{
				return true;
			}
			if (entry.EventId == MessageTrackingEvent.REDIRECT && MessageTrackingSource.AGENT == entry.Source)
			{
				string relatedRecipientAddress = entry.RelatedRecipientAddress;
				if (string.IsNullOrEmpty(relatedRecipientAddress) || !SmtpAddress.IsValidSmtpAddress(relatedRecipientAddress))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Ignoring REDIRECT event with non-SMTP RelatedRecipientAddress: {0}. Probably EHF agent.", relatedRecipientAddress);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0005DF84 File Offset: 0x0005C184
		private void FixupFederatedDeliveryEvents()
		{
			string text = null;
			List<MessageTrackingLogEntry> list = null;
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in this.entriesForMailItem)
			{
				if (messageTrackingLogEntry.SourceContext.Equals("Federated Delivery Encryption Agent", StringComparison.OrdinalIgnoreCase) && messageTrackingLogEntry.EventId == MessageTrackingEvent.EXPAND)
				{
					if (messageTrackingLogEntry.RecipientAddresses != null && messageTrackingLogEntry.RecipientAddresses.Length != 0)
					{
						if (messageTrackingLogEntry.RecipientAddresses.Length != 1)
						{
							TraceWrapper.SearchLibraryTracer.TraceError<int>(this.GetHashCode(), "Federated delivery EXPAND event expected to have only 0 or 1 recipients. Found: {0}", messageTrackingLogEntry.RecipientAddresses.Length);
							TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Federated Delivery Expand event had multiple recipients", new object[0]);
						}
						text = messageTrackingLogEntry.RecipientAddresses[0];
					}
					messageTrackingLogEntry.RecipientAddresses = new string[]
					{
						messageTrackingLogEntry.RelatedRecipientAddress
					};
					if (list == null)
					{
						list = new List<MessageTrackingLogEntry>(this.entriesForMailItem.Count);
					}
					list.Add(messageTrackingLogEntry);
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				foreach (MessageTrackingLogEntry messageTrackingLogEntry2 in list)
				{
					messageTrackingLogEntry2.FederatedDeliveryAddress = text;
				}
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0005E0D0 File Offset: 0x0005C2D0
		private int LogEntryComparer(MessageTrackingLogEntry left, MessageTrackingLogEntry right)
		{
			if (left == null)
			{
				if (right == null)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (right == null)
				{
					return -1;
				}
				if ((MailItemTracker.InitialSubmitEvents.Contains(left.EventId) && !MailItemTracker.InitialSubmitEvents.Contains(right.EventId)) || (!MailItemTracker.TerminalDeliveryEvents.Contains(left.EventId) && MailItemTracker.TerminalDeliveryEvents.Contains(right.EventId)))
				{
					return -1;
				}
				if ((MailItemTracker.InitialSubmitEvents.Contains(right.EventId) && !MailItemTracker.InitialSubmitEvents.Contains(left.EventId)) || (!MailItemTracker.TerminalDeliveryEvents.Contains(right.EventId) && MailItemTracker.TerminalDeliveryEvents.Contains(left.EventId)))
				{
					return 1;
				}
				if (left.EventId == MessageTrackingEvent.TRANSFER && right.EventId == MessageTrackingEvent.EXPAND)
				{
					return 1;
				}
				if (left.EventId == MessageTrackingEvent.EXPAND && right.EventId == MessageTrackingEvent.TRANSFER)
				{
					return -1;
				}
				return left.Time.CompareTo(right.Time);
			}
		}

		// Token: 0x04000D2E RID: 3374
		private const string RecipientTypePropertyName = "RecipientType";

		// Token: 0x04000D2F RID: 3375
		private const string ToRecipientTypeValue = "To";

		// Token: 0x04000D30 RID: 3376
		private const string CcRecipientTypeValue = "Cc";

		// Token: 0x04000D31 RID: 3377
		private const string RedirectRecipientTypeValue = "Redirect";

		// Token: 0x04000D32 RID: 3378
		private const string BccRecipientTypeValue = "Bcc";

		// Token: 0x04000D33 RID: 3379
		private const string UnknownRecipientTypeValue = "Unknown";

		// Token: 0x04000D34 RID: 3380
		private const int FirstPrecedes = -1;

		// Token: 0x04000D35 RID: 3381
		private const int SecondPrecedes = 1;

		// Token: 0x04000D36 RID: 3382
		private static readonly string[] emptyRecipientList = new string[]
		{
			string.Empty
		};

		// Token: 0x04000D37 RID: 3383
		private static readonly HashSet<MessageTrackingEvent> TerminalDeliveryEvents = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.DELIVER,
			MessageTrackingEvent.DUPLICATEDELIVER,
			MessageTrackingEvent.PROCESS
		};

		// Token: 0x04000D38 RID: 3384
		private static readonly HashSet<MessageTrackingEvent> InitialSubmitEvents = new HashSet<MessageTrackingEvent>
		{
			MessageTrackingEvent.SUBMIT
		};

		// Token: 0x04000D39 RID: 3385
		private EventTree tree;

		// Token: 0x04000D3A RID: 3386
		private List<MessageTrackingLogEntry> entriesForMailItem;
	}
}
