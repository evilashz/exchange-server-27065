using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x0200027B RID: 635
	[Serializable]
	public class MessageIdentity : ObjectId
	{
		// Token: 0x060015A2 RID: 5538 RVA: 0x00044953 File Offset: 0x00042B53
		public MessageIdentity(long identity)
		{
			this.internalId = identity;
			if (identity <= 0L)
			{
				throw new ArgumentNullException("Identity");
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0004497D File Offset: 0x00042B7D
		public MessageIdentity(long identity, QueueIdentity queueIdentity) : this(identity)
		{
			if (queueIdentity == null)
			{
				throw new ArgumentNullException("QueueIdentity");
			}
			this.queueIdentity = queueIdentity;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000449A1 File Offset: 0x00042BA1
		public MessageIdentity(QueueIdentity queueId)
		{
			this.queueIdentity = queueId;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000449BB File Offset: 0x00042BBB
		private MessageIdentity()
		{
			this.queueIdentity = QueueIdentity.Empty;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x000449DC File Offset: 0x00042BDC
		private MessageIdentity(PropertyStreamReader reader)
		{
			KeyValuePair<string, object> item;
			reader.Read(out item);
			if (!string.Equals("NumProperties", item.Key, StringComparison.OrdinalIgnoreCase))
			{
				throw new SerializationException(string.Format("Cannot deserialize MessageIdentity. Expected property NumProperties, but found property '{0}'", item.Key));
			}
			int value = PropertyStreamReader.GetValue<int>(item);
			for (int i = 0; i < value; i++)
			{
				reader.Read(out item);
				if (string.Equals("InternalId", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.internalId = PropertyStreamReader.GetValue<long>(item);
				}
				else if (string.Equals("QueueIdentity", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.queueIdentity = QueueIdentity.Create(reader);
				}
				else
				{
					ExTraceGlobals.SerializationTracer.TraceWarning<string>(0L, "Ignoring unknown property '{0} in messageIdentity", item.Key);
				}
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00044AA6 File Offset: 0x00042CA6
		public static MessageIdentity Empty
		{
			get
			{
				return MessageIdentity.empty;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00044AAD File Offset: 0x00042CAD
		public long InternalId
		{
			get
			{
				return this.internalId;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00044AB5 File Offset: 0x00042CB5
		public QueueIdentity QueueIdentity
		{
			get
			{
				return this.queueIdentity;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x00044ABD File Offset: 0x00042CBD
		public bool IsFullySpecified
		{
			get
			{
				return this.internalId != 0L && this.queueIdentity.IsFullySpecified;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00044AD6 File Offset: 0x00042CD6
		public override string ToString()
		{
			return this.queueIdentity.ToString() + "\\" + this.internalId.ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00044AFD File Offset: 0x00042CFD
		public override bool Equals(object obj)
		{
			return this == obj as MessageIdentity;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00044B0B File Offset: 0x00042D0B
		public override int GetHashCode()
		{
			return this.internalId.GetHashCode() ^ this.queueIdentity.GetHashCode();
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00044B24 File Offset: 0x00042D24
		public override byte[] GetBytes()
		{
			if (this.internalId != 0L)
			{
				return BitConverter.GetBytes(this.internalId);
			}
			return new byte[0];
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00044B42 File Offset: 0x00042D42
		public bool Match(MessageIdentity matchPattern, MatchOptions matchOptions)
		{
			return this.internalId == matchPattern.internalId && this.queueIdentity.Match(matchPattern.queueIdentity, matchOptions);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00044B68 File Offset: 0x00042D68
		internal void ToByteArray(Version targetVersion, ref byte[] bytes, ref int offset)
		{
			if (targetVersion <= MessageIdentity.messageIdAsTextVersion)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Identity.Name, StreamPropertyType.String, this.ToString(), ref bytes, ref offset);
				return;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Identity.Name, StreamPropertyType.Int32, 1, ref bytes, ref offset);
			this.ToByteArray(ref bytes, ref offset);
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00044BBC File Offset: 0x00042DBC
		internal void ToByteArray(ref byte[] bytes, ref int offset)
		{
			int num = 0;
			PropertyStreamWriter.WritePropertyKeyValue("NumProperties", StreamPropertyType.Int32, 2, ref bytes, ref offset);
			PropertyStreamWriter.WritePropertyKeyValue("InternalId", StreamPropertyType.Int64, this.internalId, ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("QueueIdentity", StreamPropertyType.Int32, 1, ref bytes, ref offset);
			this.queueIdentity.ToByteArray(ref bytes, ref offset);
			num++;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00044C1F File Offset: 0x00042E1F
		internal static MessageIdentity Create(Version sourceVersion, KeyValuePair<string, object> pair, PropertyStreamReader reader)
		{
			if (!(sourceVersion <= MessageIdentity.messageIdAsTextVersion))
			{
				return MessageIdentity.Create(reader);
			}
			return MessageIdentity.Parse(PropertyStreamReader.GetValue<string>(pair));
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00044C40 File Offset: 0x00042E40
		internal static MessageIdentity Create(PropertyStreamReader reader)
		{
			return new MessageIdentity(reader);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00044C48 File Offset: 0x00042E48
		public static MessageIdentity Parse(string identity)
		{
			return MessageIdentity.InternalParse(identity, false);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00044C54 File Offset: 0x00042E54
		internal static MessageIdentity InternalParse(string identity, bool queuePartAlwaysAsDomain)
		{
			int num = identity.LastIndexOf('\\');
			string text = null;
			string text2;
			if (num == -1)
			{
				text2 = identity;
			}
			else
			{
				text2 = identity.Substring(num + 1);
				text = identity.Substring(0, num);
			}
			if (string.IsNullOrEmpty(text2))
			{
				throw new ArgumentNullException("Identity");
			}
			QueueIdentity queueIdentity;
			if (!string.IsNullOrEmpty(text))
			{
				queueIdentity = QueueIdentity.InternalParse(text, queuePartAlwaysAsDomain, false);
			}
			else
			{
				queueIdentity = QueueIdentity.Empty;
			}
			long identity2;
			if (long.TryParse(text2, out identity2))
			{
				return new MessageIdentity(identity2, queueIdentity);
			}
			throw new ArgumentException(DataStrings.ExceptionParseInternalMessageId, "Identity");
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00044CE0 File Offset: 0x00042EE0
		public static MessageIdentity ParsePattern(string identity, ref MatchOptions matchOptions)
		{
			MessageIdentity messageIdentity = MessageIdentity.InternalParse(identity, true);
			messageIdentity.QueueIdentity.ParseDomain(ref matchOptions);
			return messageIdentity;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00044D02 File Offset: 0x00042F02
		public static implicit operator long(MessageIdentity messageIdentity)
		{
			return messageIdentity.internalId;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00044D0C File Offset: 0x00042F0C
		public static int Compare(ObjectId a, ObjectId b)
		{
			MessageIdentity messageIdentity = (MessageIdentity)a;
			MessageIdentity messageIdentity2 = (MessageIdentity)b;
			if (messageIdentity == messageIdentity2)
			{
				return 0;
			}
			if (messageIdentity == null && messageIdentity2 != null)
			{
				return -1;
			}
			if (messageIdentity != null && messageIdentity2 == null)
			{
				return 1;
			}
			int num = messageIdentity.internalId.CompareTo(messageIdentity2.internalId);
			if (num != 0)
			{
				return num;
			}
			return QueueIdentity.Compare(messageIdentity.queueIdentity, messageIdentity2.queueIdentity);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00044D65 File Offset: 0x00042F65
		public static bool operator ==(MessageIdentity v1, MessageIdentity v2)
		{
			return MessageIdentity.Compare(v1, v2) == 0;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00044D71 File Offset: 0x00042F71
		public static bool operator !=(MessageIdentity v1, MessageIdentity v2)
		{
			return !(v1 == v2);
		}

		// Token: 0x04000CB1 RID: 3249
		private const string NumPropertiesKey = "NumProperties";

		// Token: 0x04000CB2 RID: 3250
		private const string InternalIdKey = "InternalId";

		// Token: 0x04000CB3 RID: 3251
		private const string QueueIdentityKey = "QueueIdentity";

		// Token: 0x04000CB4 RID: 3252
		private static readonly Version messageIdAsTextVersion = new Version(1, 0);

		// Token: 0x04000CB5 RID: 3253
		private long internalId;

		// Token: 0x04000CB6 RID: 3254
		private QueueIdentity queueIdentity = QueueIdentity.Empty;

		// Token: 0x04000CB7 RID: 3255
		private static readonly MessageIdentity empty = new MessageIdentity();
	}
}
