using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001DA RID: 474
	internal class History : IEquatable<History>, IComparable<History>, IHistoryFacade
	{
		// Token: 0x0600156E RID: 5486 RVA: 0x00056A84 File Offset: 0x00054C84
		private History(List<HistoryRecord> records, RecipientP2Type recipientP2Type)
		{
			this.records = records;
			this.recipientP2Type = recipientP2Type;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00056A9A File Offset: 0x00054C9A
		public List<HistoryRecord> Records
		{
			get
			{
				return this.records;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00056AA2 File Offset: 0x00054CA2
		public RecipientP2Type RecipientType
		{
			get
			{
				return this.recipientP2Type;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x00056AAD File Offset: 0x00054CAD
		List<IHistoryRecordFacade> IHistoryFacade.Records
		{
			get
			{
				if (this.records == null)
				{
					return null;
				}
				return this.records.ConvertAll<IHistoryRecordFacade>((HistoryRecord record) => record);
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00056AE4 File Offset: 0x00054CE4
		public static bool operator ==(History a, History b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.recipientP2Type != b.recipientP2Type)
			{
				return false;
			}
			if (a.records == null || b.records == null)
			{
				return false;
			}
			if (a.records.Count != b.records.Count)
			{
				return false;
			}
			for (int i = 0; i < a.records.Count; i++)
			{
				if (!a.records[i].Equals(b.records[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00056B73 File Offset: 0x00054D73
		public static bool operator !=(History a, History b)
		{
			return !(a == b);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00056B80 File Offset: 0x00054D80
		public static History Derive(History parent, HistoryType type, RoutingAddress address, RecipientP2Type parentP2Type)
		{
			if (parent != null && parent.recipientP2Type != parentP2Type)
			{
				throw new InvalidOperationException("Parent history must match recipientP2Type passed in during Derive");
			}
			List<HistoryRecord> list = new List<HistoryRecord>();
			if (parent != null && parent.records != null && parent.records.Count != 0)
			{
				list.AddRange(parent.records);
			}
			HistoryRecord item = new HistoryRecord(type, address);
			list.Add(item);
			return new History(list, parentP2Type);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00056BF0 File Offset: 0x00054DF0
		private static bool CheckValidRecipientP2Type(int p2Type)
		{
			return p2Type >= 0 && p2Type <= 3;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00056C00 File Offset: 0x00054E00
		private static History ReadFrom(IExtendedPropertyCollection extendedProperties)
		{
			ReadOnlyCollection<string> readOnlyCollection;
			if (!extendedProperties.TryGetListValue<string>("Microsoft.Exchange.Transport.History", out readOnlyCollection))
			{
				readOnlyCollection = null;
			}
			RecipientP2Type recipientP2Type = RecipientP2Type.Unknown;
			int num;
			if (extendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", out num) && History.CheckValidRecipientP2Type(num))
			{
				recipientP2Type = (RecipientP2Type)num;
			}
			if (recipientP2Type == RecipientP2Type.Unknown && (readOnlyCollection == null || readOnlyCollection.Count == 0))
			{
				return null;
			}
			History result;
			try
			{
				List<HistoryRecord> list = History.ParseSerializedHistory(readOnlyCollection);
				result = new History(list, recipientP2Type);
			}
			catch (FormatException)
			{
				ExTraceGlobals.ResolverTracer.TraceError(0L, "Could not parse recipient history from extended properties");
				result = null;
			}
			return result;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00056C84 File Offset: 0x00054E84
		internal static History ReadFrom(TransportMailItem mailItem)
		{
			History history = History.ReadFrom(mailItem.ExtendedProperties);
			if (history != null)
			{
				return history;
			}
			return History.ReadFrom(mailItem.Message.MimeDocument.RootPart.Headers);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00056CC2 File Offset: 0x00054EC2
		internal static History ReadFrom(MailRecipient recipient)
		{
			return History.ReadFrom(recipient.ExtendedProperties);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00056CD0 File Offset: 0x00054ED0
		public static History ReadFrom(HeaderList headers)
		{
			RecipientP2Type recipientP2Type = RecipientP2Type.Unknown;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Recipient-P2-Type");
			if (header != null)
			{
				try
				{
					recipientP2Type = History.ParseRecipientP2Type(header.Value.Trim());
				}
				catch (ExchangeDataException)
				{
					ExTraceGlobals.ResolverTracer.TraceError(0L, "Invalid MIME for RecipientP2Type header, treating as if no history");
					return null;
				}
				catch (FormatException)
				{
					ExTraceGlobals.ResolverTracer.TraceError(0L, "Corrupt recipient-type header, treating as if recipient type was \"Unknown\"");
				}
			}
			Header[] array = headers.FindAll("X-MS-Exchange-Organization-History");
			List<HistoryRecord> list = null;
			List<string> list2 = new List<string>();
			try
			{
				foreach (Header header2 in array)
				{
					list2.Add(header2.Value);
				}
			}
			catch (ExchangeDataException)
			{
				return null;
			}
			try
			{
				list = History.ParseSerializedHistory(list2);
			}
			catch (FormatException)
			{
				return null;
			}
			if (list == null && recipientP2Type == RecipientP2Type.Unknown)
			{
				return null;
			}
			return new History(list, recipientP2Type);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00056DCC File Offset: 0x00054FCC
		internal static IHistoryFacade ReadFromMailItemByAgent(ITransportMailItemFacade mailItem)
		{
			return History.ReadFrom((TransportMailItem)mailItem);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00056DD9 File Offset: 0x00054FD9
		internal static IHistoryFacade ReadFromRecipientByAgent(IMailRecipientFacade recipient)
		{
			return History.ReadFrom((MailRecipient)recipient);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00056DE8 File Offset: 0x00054FE8
		private static List<HistoryRecord> ParseSerializedHistory(IList<string> serializedHistory)
		{
			if (serializedHistory == null || serializedHistory.Count == 0)
			{
				return null;
			}
			List<HistoryRecord> list = new List<HistoryRecord>(serializedHistory.Count);
			foreach (string serializedRecord in serializedHistory)
			{
				HistoryRecord item = HistoryRecord.Parse(serializedRecord);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00056E54 File Offset: 0x00055054
		public static void Clear(TransportMailItem mailItem)
		{
			HeaderList headers = mailItem.Message.MimeDocument.RootPart.Headers;
			History history = History.ReadFrom(headers);
			if (history != null)
			{
				history.WriteTo(mailItem.ExtendedProperties);
			}
			headers.RemoveAll("X-MS-Exchange-Organization-History");
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00056E9E File Offset: 0x0005509E
		public void WriteTo(MailRecipient recipient)
		{
			this.WriteTo(recipient.ExtendedProperties);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00056EAC File Offset: 0x000550AC
		public void WriteTo(IExtendedPropertyCollection extendedProperties)
		{
			List<string> serializedHistory;
			if (this.cachedSerializedHistory != null)
			{
				serializedHistory = this.cachedSerializedHistory;
			}
			else
			{
				serializedHistory = this.GetSerializedHistory();
			}
			extendedProperties.SetValue<List<string>>("Microsoft.Exchange.Transport.History", serializedHistory);
			extendedProperties.SetValue<int>("Microsoft.Exchange.Transport.RecipientP2Type", (int)this.recipientP2Type);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00056EF0 File Offset: 0x000550F0
		public void WriteTo(HeaderList headers)
		{
			MimeNode mimeNode = null;
			MimeNode mimeNode2 = null;
			foreach (Header header in headers)
			{
				bool flag = string.Equals(header.Name, "X-MS-Exchange-Organization-History", StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					if (mimeNode == null)
					{
						mimeNode = header;
					}
					mimeNode2 = header;
				}
				else if (mimeNode != null)
				{
					break;
				}
			}
			if (mimeNode2 == null)
			{
				mimeNode2 = headers.LastChild;
			}
			if (this.records != null && this.records.Count != 0)
			{
				foreach (HistoryRecord historyRecord in this.records)
				{
					Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-History", historyRecord.ToString());
					mimeNode2 = headers.InsertAfter(newChild, mimeNode2);
				}
			}
			if (this.recipientP2Type != RecipientP2Type.Unknown && headers.FindFirst("X-MS-Exchange-Organization-Recipient-P2-Type") == null)
			{
				Header newChild2 = new AsciiTextHeader("X-MS-Exchange-Organization-Recipient-P2-Type", History.RecipientP2TypeToString(this.recipientP2Type));
				headers.AppendChild(newChild2);
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00057010 File Offset: 0x00055210
		private static string RecipientP2TypeToString(RecipientP2Type p2Type)
		{
			return History.recipientP2TypeNames[(int)p2Type];
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0005701C File Offset: 0x0005521C
		private static RecipientP2Type ParseRecipientP2Type(string serializedP2Type)
		{
			for (int i = 0; i < History.recipientP2TypeNames.Length; i++)
			{
				if (serializedP2Type.Equals(History.recipientP2TypeNames[i], StringComparison.OrdinalIgnoreCase))
				{
					return (RecipientP2Type)i;
				}
			}
			ExTraceGlobals.ResolverTracer.TraceError<string>(0L, "Could not parse {0} as a RecipientP2Type", serializedP2Type);
			throw new FormatException("Unrecognized recipient P2 type");
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0005706C File Offset: 0x0005526C
		public bool Contains(RoutingAddress searchAddress, out bool reportable)
		{
			reportable = true;
			if (this.records == null || this.records.Count == 0)
			{
				return false;
			}
			foreach (HistoryRecord historyRecord in this.records)
			{
				reportable = (reportable && historyRecord.Type == HistoryType.Forwarded);
				if (historyRecord.Address == searchAddress)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000570FC File Offset: 0x000552FC
		public bool Equals(History other)
		{
			return this == other;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00057108 File Offset: 0x00055308
		public int CompareTo(History other)
		{
			int num = 0;
			RecipientP2Type recipientP2Type = (other == null) ? RecipientP2Type.Unknown : other.recipientP2Type;
			if (this.recipientP2Type != recipientP2Type)
			{
				return this.recipientP2Type - recipientP2Type;
			}
			if (other != null && other.records != null)
			{
				num = other.records.Count;
			}
			int num2 = (this.records == null) ? 0 : this.records.Count;
			int num3 = (num2 < num) ? num2 : num;
			for (int i = 0; i < num3; i++)
			{
				int num4 = this.records[i].CompareTo(other.records[i]);
				if (num4 != 0)
				{
					return num4;
				}
			}
			return num2 - num;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000571B4 File Offset: 0x000553B4
		public override bool Equals(object obj)
		{
			return this == obj as History;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000571C2 File Offset: 0x000553C2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000571CA File Offset: 0x000553CA
		public void CacheSerializedHistory()
		{
			this.cachedSerializedHistory = this.GetSerializedHistory();
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000571D8 File Offset: 0x000553D8
		public void ClearSerializedHistory()
		{
			this.cachedSerializedHistory = null;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x000571E4 File Offset: 0x000553E4
		private List<string> GetSerializedHistory()
		{
			List<string> list = null;
			if (this.records != null && this.records.Count > 0)
			{
				list = new List<string>();
				foreach (HistoryRecord historyRecord in this.records)
				{
					string item = historyRecord.ToString();
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x04000AAB RID: 2731
		private const string HistoryHeader = "X-MS-Exchange-Organization-History";

		// Token: 0x04000AAC RID: 2732
		private const string HistoryProperty = "Microsoft.Exchange.Transport.History";

		// Token: 0x04000AAD RID: 2733
		public const string RecipientP2TypeProperty = "Microsoft.Exchange.Transport.RecipientP2Type";

		// Token: 0x04000AAE RID: 2734
		private const string RecipientP2TypeHeader = "X-MS-Exchange-Organization-Recipient-P2-Type";

		// Token: 0x04000AAF RID: 2735
		private static string[] recipientP2TypeNames = new string[]
		{
			RecipientP2Type.Unknown.ToString(),
			RecipientP2Type.To.ToString(),
			RecipientP2Type.Cc.ToString(),
			RecipientP2Type.Bcc.ToString()
		};

		// Token: 0x04000AB0 RID: 2736
		private RecipientP2Type recipientP2Type;

		// Token: 0x04000AB1 RID: 2737
		private List<HistoryRecord> records;

		// Token: 0x04000AB2 RID: 2738
		private List<string> cachedSerializedHistory;
	}
}
