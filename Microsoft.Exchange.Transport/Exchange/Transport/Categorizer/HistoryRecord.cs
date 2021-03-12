using System;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001DB RID: 475
	internal class HistoryRecord : IEquatable<HistoryRecord>, IComparable<HistoryRecord>, IHistoryRecordFacade
	{
		// Token: 0x0600158D RID: 5517 RVA: 0x000572B2 File Offset: 0x000554B2
		internal HistoryRecord(HistoryType type, RoutingAddress address)
		{
			this.type = type;
			this.address = address;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000572C8 File Offset: 0x000554C8
		public HistoryType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x000572D0 File Offset: 0x000554D0
		public RoutingAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x000572D8 File Offset: 0x000554D8
		public override string ToString()
		{
			if (this.serializedString != null)
			{
				return this.serializedString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(HistoryRecord.historyTypeStrings[(int)this.type]);
			stringBuilder.Append(": ");
			stringBuilder.Append(this.address.ToString());
			this.serializedString = stringBuilder.ToString();
			return this.serializedString;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00057344 File Offset: 0x00055544
		internal static HistoryRecord Parse(string serializedRecord)
		{
			int num = serializedRecord.IndexOf(':');
			if (num == -1)
			{
				throw new FormatException("History record missing ':' delimiter");
			}
			HistoryType historyType = HistoryRecord.ParseType(serializedRecord.Substring(0, num));
			string text = serializedRecord.Substring(num + 1).Trim();
			RoutingAddress routingAddress = new RoutingAddress(text);
			if (!routingAddress.IsValid)
			{
				throw new FormatException("Address is invalid");
			}
			return new HistoryRecord(historyType, routingAddress)
			{
				serializedString = serializedRecord
			};
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x000573B8 File Offset: 0x000555B8
		private static HistoryType ParseType(string serializedType)
		{
			for (int i = 0; i < HistoryRecord.historyTypeStrings.Length; i++)
			{
				if (serializedType.Equals(HistoryRecord.historyTypeStrings[i]))
				{
					return (HistoryType)i;
				}
			}
			throw new FormatException("Unrecognized HistoryType");
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x000573F2 File Offset: 0x000555F2
		public bool Equals(HistoryRecord other)
		{
			return this.type == other.type && this.address == other.address;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00057418 File Offset: 0x00055618
		public int CompareTo(HistoryRecord other)
		{
			if (this.type != other.type)
			{
				return this.type - other.type;
			}
			return string.Compare(this.address.ToString(), other.address.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000AB4 RID: 2740
		private static string[] historyTypeStrings = new string[]
		{
			HistoryType.Expanded.ToString(),
			HistoryType.Forwarded.ToString(),
			HistoryType.DeliveredAndForwarded.ToString()
		};

		// Token: 0x04000AB5 RID: 2741
		private HistoryType type;

		// Token: 0x04000AB6 RID: 2742
		private RoutingAddress address;

		// Token: 0x04000AB7 RID: 2743
		private string serializedString;
	}
}
