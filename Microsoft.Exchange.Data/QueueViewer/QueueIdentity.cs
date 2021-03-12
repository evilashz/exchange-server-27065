using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Data;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000286 RID: 646
	[Serializable]
	public class QueueIdentity : ObjectId, IComparable
	{
		// Token: 0x0600172A RID: 5930 RVA: 0x000482F3 File Offset: 0x000464F3
		private QueueIdentity()
		{
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00048314 File Offset: 0x00046514
		private QueueIdentity(QueueType queueType)
		{
			if (queueType == QueueType.Delivery || queueType == QueueType.Shadow)
			{
				throw new InvalidOperationException("Cannot create Delivery or Shadow queue using this constructor");
			}
			this.server = QueueIdentity.LocalHostName;
			this.queueType = queueType;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00048364 File Offset: 0x00046564
		public QueueIdentity(QueueType queueType, long queueRowId, string nextHopDomain)
		{
			if (queueRowId <= 0L)
			{
				throw new ExArgumentNullException("queueRowId");
			}
			if (queueType != QueueType.Shadow && queueType != QueueType.Delivery)
			{
				throw new ArgumentException(string.Format("QueueType '{0}' not allowed in this constructor", queueType));
			}
			this.server = QueueIdentity.LocalHostName;
			this.queueType = queueType;
			this.queueRowId = queueRowId;
			this.nextHopDomain = nextHopDomain;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000483DC File Offset: 0x000465DC
		public QueueIdentity(ExtensibleQueueInfo queue) : this()
		{
			QueueIdentity queueIdentity = (QueueIdentity)queue.Identity;
			this.server = queueIdentity.server;
			this.queueType = queueIdentity.queueType;
			this.queueRowId = queueIdentity.queueRowId;
			this.nextHopDomain = queueIdentity.nextHopDomain;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0004842C File Offset: 0x0004662C
		private QueueIdentity(PropertyStreamReader reader)
		{
			KeyValuePair<string, object> item;
			reader.Read(out item);
			if (!string.Equals("NumProperties", item.Key, StringComparison.OrdinalIgnoreCase))
			{
				throw new SerializationException(string.Format("Cannot deserialize QueueIdentity. Expected property NumProperties, but found property '{0}'", item.Key));
			}
			int value = PropertyStreamReader.GetValue<int>(item);
			for (int i = 0; i < value; i++)
			{
				reader.Read(out item);
				if (string.Equals("RowId", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.queueRowId = PropertyStreamReader.GetValue<long>(item);
				}
				else if (string.Equals("Type", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.queueType = (QueueType)PropertyStreamReader.GetValue<int>(item);
				}
				else if (string.Equals("NextHopDomain", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.nextHopDomain = PropertyStreamReader.GetValue<string>(item);
				}
				else if (string.Equals("Server", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.server = PropertyStreamReader.GetValue<string>(item);
				}
				else
				{
					ExTraceGlobals.SerializationTracer.TraceWarning<string>(0L, "Ignoring unknown property '{0} in queueIdentity", item.Key);
				}
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0004854B File Offset: 0x0004674B
		public static QueueIdentity Empty
		{
			get
			{
				return QueueIdentity.empty;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00048552 File Offset: 0x00046752
		public static QueueIdentity PoisonQueueIdentity
		{
			get
			{
				return QueueIdentity.poisonQueueIdentity;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00048559 File Offset: 0x00046759
		public static QueueIdentity SubmissionQueueIdentity
		{
			get
			{
				return QueueIdentity.submissionQueueIdentity;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00048560 File Offset: 0x00046760
		public static QueueIdentity UnreachableQueueIdentity
		{
			get
			{
				return QueueIdentity.unreachableQueueIdentity;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00048567 File Offset: 0x00046767
		public QueueType Type
		{
			get
			{
				return this.queueType;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0004856F File Offset: 0x0004676F
		public long RowId
		{
			get
			{
				return this.queueRowId;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00048578 File Offset: 0x00046778
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x000485DF File Offset: 0x000467DF
		public string NextHopDomain
		{
			get
			{
				if (!string.IsNullOrEmpty(this.nextHopDomain))
				{
					return this.nextHopDomain;
				}
				if (this.queueType == QueueType.Submission)
				{
					return DataStrings.SubmissionQueueNextHopDomain;
				}
				if (this.queueType == QueueType.Poison)
				{
					return DataStrings.PoisonQueueNextHopDomain;
				}
				if (this.queueType == QueueType.Unreachable)
				{
					return DataStrings.UnreachableQueueNextHopDomain;
				}
				throw new Exception("Unexpected queue type");
			}
			set
			{
				this.nextHopDomain = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x000485E8 File Offset: 0x000467E8
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x00048603 File Offset: 0x00046803
		public string Server
		{
			get
			{
				if (string.IsNullOrEmpty(this.server))
				{
					return QueueIdentity.LocalHostName;
				}
				return this.server;
			}
			private set
			{
				if (RoutingAddress.IsValidDomain(value))
				{
					this.server = value;
					return;
				}
				throw new ArgumentException(DataStrings.ExceptionInvalidServerName(value), "Identity");
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0004862A File Offset: 0x0004682A
		public bool IsLocal
		{
			get
			{
				return string.IsNullOrEmpty(this.server) || string.Compare(this.server, QueueIdentity.LocalHostName, StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0004864F File Offset: 0x0004684F
		public bool IsFullySpecified
		{
			get
			{
				return this.queueType != QueueType.Undefined;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0004865D File Offset: 0x0004685D
		public bool IsEmpty
		{
			get
			{
				return this.queueType == QueueType.Undefined && string.IsNullOrEmpty(this.nextHopDomain) && this.queueRowId == 0L;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00048680 File Offset: 0x00046880
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			if (!string.IsNullOrEmpty(this.server))
			{
				stringBuilder.AppendFormat("{0}\\", this.server);
			}
			switch (this.queueType)
			{
			case QueueType.Poison:
				stringBuilder.Append(QueueIdentity.PoisonIdentityString);
				break;
			case QueueType.Submission:
				stringBuilder.Append(QueueIdentity.SubmissionIdentityString);
				break;
			case QueueType.Unreachable:
				stringBuilder.Append(QueueIdentity.UnreachableIdentityString);
				break;
			case QueueType.Shadow:
				if (this.queueRowId > 0L)
				{
					stringBuilder.AppendFormat("{0}\\{1}", QueueIdentity.ShadowIdentityString, this.queueRowId.ToString(NumberFormatInfo.InvariantInfo));
				}
				else
				{
					stringBuilder.AppendFormat("{0}\\{1}", QueueIdentity.ShadowIdentityString, this.nextHopDomain);
				}
				break;
			default:
				if (this.queueRowId > 0L)
				{
					stringBuilder.Append(this.queueRowId.ToString(NumberFormatInfo.InvariantInfo));
				}
				else
				{
					stringBuilder.Append(this.nextHopDomain);
				}
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00048781 File Offset: 0x00046981
		public override bool Equals(object obj)
		{
			return this == obj as QueueIdentity;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00048790 File Offset: 0x00046990
		public override int GetHashCode()
		{
			return this.queueType.GetHashCode() ^ ((this.queueRowId != 0L) ? this.queueRowId.GetHashCode() : ((!string.IsNullOrEmpty(this.nextHopDomain)) ? this.nextHopDomain.GetHashCode() : 0));
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000487E0 File Offset: 0x000469E0
		public override byte[] GetBytes()
		{
			return new byte[0];
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000487E8 File Offset: 0x000469E8
		internal void ToByteArray(ref byte[] bytes, ref int offset)
		{
			int num = 0;
			PropertyStreamWriter.WritePropertyKeyValue("NumProperties", StreamPropertyType.Int32, 4, ref bytes, ref offset);
			PropertyStreamWriter.WritePropertyKeyValue("RowId", StreamPropertyType.Int64, this.queueRowId, ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("Type", StreamPropertyType.Int32, (int)this.queueType, ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("NextHopDomain", StreamPropertyType.String, this.nextHopDomain, ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("Server", StreamPropertyType.String, this.server, ref bytes, ref offset);
			num++;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00048873 File Offset: 0x00046A73
		internal static QueueIdentity Create(PropertyStreamReader reader)
		{
			return new QueueIdentity(reader);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0004887B File Offset: 0x00046A7B
		public static QueueIdentity Parse(string identity)
		{
			return QueueIdentity.InternalParse(identity, false, false);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00048885 File Offset: 0x00046A85
		public static QueueIdentity Parse(string identity, bool implicitShadow)
		{
			return QueueIdentity.InternalParse(identity, false, implicitShadow);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00048890 File Offset: 0x00046A90
		internal static QueueIdentity InternalParse(string identity, bool queuePartAlwaysAsDomain, bool implicitShadow)
		{
			int num = identity.IndexOf('\\');
			string text = null;
			string text2;
			if (num == -1)
			{
				text2 = identity;
			}
			else
			{
				text = identity.Substring(0, num);
				if (string.Equals(text, QueueIdentity.ShadowIdentityString, StringComparison.OrdinalIgnoreCase))
				{
					text = null;
					text2 = identity;
				}
				else
				{
					text2 = identity.Substring(num + 1);
				}
			}
			if (string.IsNullOrEmpty(text2))
			{
				throw new ExArgumentNullException("QueueIdentity");
			}
			string text3 = QueueIdentity.ShadowIdentityString + "\\";
			bool flag = text2.StartsWith(text3, StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				text2 = text2.Substring(text3.Length);
			}
			QueueIdentity queueIdentity = new QueueIdentity();
			int num2;
			if (!queuePartAlwaysAsDomain && int.TryParse(text2, out num2))
			{
				queueIdentity.queueType = ((flag || implicitShadow) ? QueueType.Shadow : QueueType.Delivery);
				queueIdentity.queueRowId = (long)num2;
			}
			else if (!queuePartAlwaysAsDomain && string.Compare(text2, QueueIdentity.PoisonIdentityString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				queueIdentity.queueType = QueueType.Poison;
			}
			else if (!queuePartAlwaysAsDomain && string.Compare(text2, QueueIdentity.SubmissionIdentityString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				queueIdentity.queueType = QueueType.Submission;
			}
			else if (!queuePartAlwaysAsDomain && string.Compare(text2, QueueIdentity.UnreachableIdentityString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				queueIdentity.queueType = QueueType.Unreachable;
			}
			else
			{
				if (!text2.StartsWith("*", StringComparison.OrdinalIgnoreCase) && !text2.EndsWith("*", StringComparison.OrdinalIgnoreCase))
				{
					queueIdentity.queueType = (flag ? QueueType.Shadow : QueueType.Delivery);
				}
				queueIdentity.nextHopDomain = text2;
			}
			if (!string.IsNullOrEmpty(text))
			{
				queueIdentity.Server = text;
			}
			return queueIdentity;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000489E4 File Offset: 0x00046BE4
		public static QueueIdentity ParsePattern(string identity, ref MatchOptions matchOptions)
		{
			QueueIdentity queueIdentity = QueueIdentity.InternalParse(identity, true, false);
			queueIdentity.ParseDomain(ref matchOptions);
			return queueIdentity;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00048A04 File Offset: 0x00046C04
		public void ParseDomain(ref MatchOptions matchOptions)
		{
			matchOptions = MatchOptions.FullString;
			if (!string.IsNullOrEmpty(this.nextHopDomain))
			{
				if (this.nextHopDomain.StartsWith("*", StringComparison.Ordinal))
				{
					matchOptions = MatchOptions.Suffix;
					this.nextHopDomain = this.nextHopDomain.Substring(1, this.nextHopDomain.Length - 1);
				}
				if (this.nextHopDomain.EndsWith("*", StringComparison.Ordinal))
				{
					if (matchOptions == MatchOptions.Suffix)
					{
						matchOptions = MatchOptions.SubString;
					}
					else
					{
						matchOptions = MatchOptions.Prefix;
					}
					this.nextHopDomain = this.nextHopDomain.Substring(0, this.nextHopDomain.Length - 1);
				}
			}
			if (string.IsNullOrEmpty(this.nextHopDomain))
			{
				matchOptions = MatchOptions.SubString;
				this.nextHopDomain = string.Empty;
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00048AB0 File Offset: 0x00046CB0
		public bool Match(QueueIdentity matchPattern, MatchOptions matchOptions)
		{
			if (!PagedObjectSchema.MatchString(this.NextHopDomain, matchPattern.nextHopDomain, matchOptions))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.server))
			{
				throw new InvalidOperationException();
			}
			return string.IsNullOrEmpty(matchPattern.server) || this.server.Equals(matchPattern.server, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00048B08 File Offset: 0x00046D08
		public static int Compare(ObjectId a, ObjectId b)
		{
			QueueIdentity queueIdentity = (QueueIdentity)a;
			QueueIdentity queueIdentity2 = (QueueIdentity)b;
			if (queueIdentity == queueIdentity2)
			{
				return 0;
			}
			if (queueIdentity == null && queueIdentity2 != null)
			{
				return -1;
			}
			if (queueIdentity != null && queueIdentity2 == null)
			{
				return 1;
			}
			if (queueIdentity.IsEmpty || queueIdentity2.IsEmpty)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			int num2;
			if (queueIdentity.queueType != QueueType.Undefined && queueIdentity2.queueType != QueueType.Undefined)
			{
				int num = (int)queueIdentity.queueType;
				num2 = num.CompareTo((int)queueIdentity2.queueType);
				if (num2 != 0)
				{
					return num2;
				}
			}
			if (queueIdentity.queueRowId != 0L && queueIdentity2.queueRowId != 0L)
			{
				num2 = queueIdentity.queueRowId.CompareTo(queueIdentity2.queueRowId);
			}
			else
			{
				num2 = string.Compare(queueIdentity.nextHopDomain, queueIdentity2.nextHopDomain, StringComparison.OrdinalIgnoreCase);
			}
			if (num2 != 0)
			{
				return num2;
			}
			if (string.IsNullOrEmpty(queueIdentity.server) || string.IsNullOrEmpty(queueIdentity2.server))
			{
				return 0;
			}
			return string.Compare(queueIdentity.server, queueIdentity2.server, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00048BEE File Offset: 0x00046DEE
		public static bool operator ==(QueueIdentity v1, QueueIdentity v2)
		{
			return QueueIdentity.Compare(v1, v2) == 0;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00048BFA File Offset: 0x00046DFA
		public static bool operator !=(QueueIdentity a, QueueIdentity b)
		{
			return !(a == b);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00048C08 File Offset: 0x00046E08
		public int CompareTo(object obj)
		{
			QueueIdentity queueIdentity = obj as QueueIdentity;
			if (queueIdentity == null)
			{
				throw new ArgumentException(DataStrings.ExceptionQueueIdentityCompare(obj.GetType().FullName));
			}
			return QueueIdentity.Compare(this, queueIdentity);
		}

		// Token: 0x04000D41 RID: 3393
		private const string NumPropertiesKey = "NumProperties";

		// Token: 0x04000D42 RID: 3394
		private const string RowIdKey = "RowId";

		// Token: 0x04000D43 RID: 3395
		private const string TypeKey = "Type";

		// Token: 0x04000D44 RID: 3396
		private const string NextHopDomainKey = "NextHopDomain";

		// Token: 0x04000D45 RID: 3397
		private const string ServerKey = "Server";

		// Token: 0x04000D46 RID: 3398
		public static readonly string LocalHostName = Dns.GetHostName();

		// Token: 0x04000D47 RID: 3399
		private string server = string.Empty;

		// Token: 0x04000D48 RID: 3400
		private QueueType queueType;

		// Token: 0x04000D49 RID: 3401
		private long queueRowId;

		// Token: 0x04000D4A RID: 3402
		private string nextHopDomain = string.Empty;

		// Token: 0x04000D4B RID: 3403
		private static readonly QueueIdentity empty = new QueueIdentity();

		// Token: 0x04000D4C RID: 3404
		private static readonly QueueIdentity poisonQueueIdentity = new QueueIdentity(QueueType.Poison);

		// Token: 0x04000D4D RID: 3405
		private static readonly QueueIdentity submissionQueueIdentity = new QueueIdentity(QueueType.Submission);

		// Token: 0x04000D4E RID: 3406
		private static readonly QueueIdentity unreachableQueueIdentity = new QueueIdentity(QueueType.Unreachable);

		// Token: 0x04000D4F RID: 3407
		private static readonly string PoisonIdentityString = QueueType.Poison.ToString();

		// Token: 0x04000D50 RID: 3408
		private static readonly string SubmissionIdentityString = QueueType.Submission.ToString();

		// Token: 0x04000D51 RID: 3409
		private static readonly string UnreachableIdentityString = QueueType.Unreachable.ToString();

		// Token: 0x04000D52 RID: 3410
		private static readonly string ShadowIdentityString = QueueType.Shadow.ToString();
	}
}
