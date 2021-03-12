using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000032 RID: 50
	internal abstract class SynchronizedThreadMap<K>
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00009524 File Offset: 0x00007724
		public SynchronizedThreadMap(int capacity, Trace tracer, string keyDisplayName, int estimatedEntrySize, int threadLimit, SmtpResponse exceededResponse, bool shouldRemoveEntryOnZero) : this(capacity, null, tracer, keyDisplayName, estimatedEntrySize, threadLimit, exceededResponse, null, shouldRemoveEntryOnZero)
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009544 File Offset: 0x00007744
		public SynchronizedThreadMap(int capacity, Trace tracer, string keyDisplayName, int estimatedEntrySize, int threadLimit, string exceptionMessage, bool shouldRemoveEntryOnZero) : this(capacity, null, tracer, keyDisplayName, estimatedEntrySize, threadLimit, SmtpResponse.Empty, exceptionMessage, shouldRemoveEntryOnZero)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009568 File Offset: 0x00007768
		public SynchronizedThreadMap(int capacity, IEqualityComparer<K> comparer, Trace tracer, string keyDisplayName, int estimatedEntrySize, int threadLimit, SmtpResponse exceededResponse, bool shouldRemoveEntryOnZero) : this(capacity, comparer, tracer, keyDisplayName, estimatedEntrySize, threadLimit, exceededResponse, null, shouldRemoveEntryOnZero)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000958C File Offset: 0x0000778C
		public SynchronizedThreadMap(int capacity, IEqualityComparer<K> comparer, Trace tracer, string keyDisplayName, int estimatedEntrySize, int threadLimit, string exceptionMessage, bool shouldRemoveEntryOnZero) : this(capacity, comparer, tracer, keyDisplayName, estimatedEntrySize, threadLimit, SmtpResponse.Empty, exceptionMessage, shouldRemoveEntryOnZero)
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000095B4 File Offset: 0x000077B4
		public SynchronizedThreadMap(int capacity, IEqualityComparer<K> comparer, Trace tracer, string keyDisplayName, int estimatedEntrySize, int threadLimit, SmtpResponse exceededResponse, string exceptionMessage, bool shouldRemoveEntryOnZero)
		{
			this.map = new Dictionary<K, int>(capacity, comparer);
			this.SyncRoot = new object();
			this.Tracer = tracer;
			this.keyDisplayName = keyDisplayName;
			this.estimatedEntrySize = estimatedEntrySize;
			this.exceededResponse = exceededResponse;
			this.exceptionMessage = exceptionMessage;
			this.shouldRemoveEntryOnZero = shouldRemoveEntryOnZero;
			this.threadLimit = threadLimit;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009618 File Offset: 0x00007818
		protected SynchronizedThreadMap(int capacity, IEqualityComparer<K> comparer, Trace tracer, string keyDisplayName, int estimatedEntrySize, Dictionary<K, int> threadLimitsForDiagnostics, SmtpResponse exceededResponse, bool shouldRemoveEntryOnZero) : this(capacity, comparer, tracer, keyDisplayName, estimatedEntrySize, 0, exceededResponse, null, shouldRemoveEntryOnZero)
		{
			this.threadLimitsForDiagnostics = threadLimitsForDiagnostics;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009640 File Offset: 0x00007840
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00009648 File Offset: 0x00007848
		public int ThreadLimit
		{
			get
			{
				return this.threadLimit;
			}
			set
			{
				this.threadLimit = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009651 File Offset: 0x00007851
		protected Dictionary<K, int> ThreadMap
		{
			get
			{
				return this.map;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009659 File Offset: 0x00007859
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009661 File Offset: 0x00007861
		private protected Trace Tracer { protected get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000966A File Offset: 0x0000786A
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00009672 File Offset: 0x00007872
		private protected object SyncRoot { protected get; private set; }

		// Token: 0x0600019E RID: 414 RVA: 0x0000967C File Offset: 0x0000787C
		public static int CalculateAdaptiveThreadLimit(Trace tracer, string entityType, int availableThreads, int perEntityThreadLimit, int entityCount)
		{
			TraceHelper.TracePass(tracer, 0L, "CalculateAdaptiveThreadLimit: perEntityThreadLimit: {0}, availableThreads: {1}, entityCount: {2}, entityType: {3}", new object[]
			{
				perEntityThreadLimit,
				availableThreads,
				entityCount,
				entityType
			});
			if (entityCount == 0)
			{
				return availableThreads;
			}
			int num = perEntityThreadLimit * entityCount;
			if (num < availableThreads)
			{
				perEntityThreadLimit = (int)Math.Ceiling((double)availableThreads / (double)entityCount);
				TraceHelper.TracePass(tracer, 0L, "CalculateAdaptiveThreadLimit: thread limit changed to {0} to ensure all available threads are used", new object[]
				{
					perEntityThreadLimit
				});
			}
			return perEntityThreadLimit;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000096FA File Offset: 0x000078FA
		public void CheckAndIncrement(K key, ulong sessionId, string mdb)
		{
			this.CheckThreadLimit(key, this.threadLimit, sessionId, mdb, true);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000970C File Offset: 0x0000790C
		public bool TryCheckAndIncrement(K key, ulong sessionId, string mdb)
		{
			return this.TryCheckThreadLimit(key, this.threadLimit, sessionId, mdb, true);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009720 File Offset: 0x00007920
		public void Decrement(K key)
		{
			lock (this.SyncRoot)
			{
				int num;
				if (!this.map.TryGetValue(key, out num))
				{
					string message = string.Format(CultureInfo.InvariantCulture, "Trying to decrement a non-existent thread map entry {0}. Current map content is {1}", new object[]
					{
						key,
						this.Dump()
					});
					TraceHelper.TraceFail(this.Tracer, 0L, message);
					throw new InvalidOperationException(message);
				}
				if (1 > num)
				{
					string message2 = string.Format(CultureInfo.InvariantCulture, "Trying to decrement a thread map entry {0} that is already 0. Current map content is {1}", new object[]
					{
						key,
						this.Dump()
					});
					this.Tracer.TraceError(0L, message2);
					throw new InvalidOperationException(message2);
				}
				if (this.shouldRemoveEntryOnZero && 1 == num)
				{
					this.map.Remove(key);
					if (this.threadLimitsForDiagnostics != null)
					{
						this.threadLimitsForDiagnostics.Remove(key);
					}
					TraceHelper.TracePass(this.Tracer, 0L, "Removed thread map entry {0} since it's the last thread being used.", new object[]
					{
						key
					});
				}
				else
				{
					num = (this.map[key] = num - 1);
					TraceHelper.TracePass(this.Tracer, 0L, "Decremented thread map entry {0} to {1}.", new object[]
					{
						key,
						num
					});
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000098A4 File Offset: 0x00007AA4
		public string Dump()
		{
			string result;
			lock (this.SyncRoot)
			{
				StringBuilder stringBuilder = new StringBuilder(this.estimatedEntrySize * this.map.Count);
				foreach (KeyValuePair<K, int> keyValuePair in this.map)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}:{1},{2}", new object[]
					{
						keyValuePair.Key,
						keyValuePair.Value,
						Environment.NewLine
					});
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009980 File Offset: 0x00007B80
		public virtual XElement GetDiagnosticInfo(XElement parentElement)
		{
			lock (this.SyncRoot)
			{
				parentElement.Add(new XElement("count", this.map.Count));
				if (this.threadLimitsForDiagnostics == null)
				{
					parentElement.Add(new XElement("limit", this.threadLimit));
				}
				if (this.map.Count > 0)
				{
					foreach (KeyValuePair<K, int> keyValuePair in this.map)
					{
						XElement xelement = new XElement("item");
						xelement.Add(new XElement("key", keyValuePair.Key));
						xelement.Add(new XElement("value", keyValuePair.Value));
						if (this.threadLimitsForDiagnostics != null)
						{
							int num;
							if (this.threadLimitsForDiagnostics.TryGetValue(keyValuePair.Key, out num))
							{
								xelement.Add(new XElement("limit", num));
							}
							else
							{
								xelement.Add(new XElement("limit", "not set"));
							}
						}
						parentElement.Add(xelement);
					}
				}
			}
			return parentElement;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009B24 File Offset: 0x00007D24
		protected void CheckThreadLimit(K key, int threadLimit, ulong sessionId, string mdb, bool shouldIncrement)
		{
			if (this.TryCheckThreadLimit(key, threadLimit, sessionId, mdb, shouldIncrement))
			{
				return;
			}
			if (this.exceptionMessage != null)
			{
				string message = string.Format(this.exceptionMessage, key, threadLimit);
				throw new ThreadLimitExceededException(message);
			}
			throw new ThreadLimitExceededException(this.exceededResponse);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009B74 File Offset: 0x00007D74
		protected bool TryCheckThreadLimit(K key, int threadLimit, ulong sessionId, string mdb, bool shouldIncrement)
		{
			bool result;
			lock (this.SyncRoot)
			{
				int num;
				if (!this.map.TryGetValue(key, out num))
				{
					num = 0;
				}
				if (this.threadLimitsForDiagnostics != null)
				{
					this.threadLimitsForDiagnostics[key] = threadLimit;
				}
				if (shouldIncrement)
				{
					num++;
				}
				if (num > threadLimit)
				{
					TraceHelper.TraceFail(this.Tracer, 0L, "{0} {1} has reached thread limit {2}.", new object[]
					{
						this.keyDisplayName,
						key,
						threadLimit
					});
					this.LogLimitExceeded(key, threadLimit, sessionId, mdb);
					result = false;
				}
				else
				{
					this.map[key] = num;
					TraceHelper.TracePass(this.Tracer, 0L, "{0} {1} has been incremented to {2}.", new object[]
					{
						this.keyDisplayName,
						key,
						num
					});
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009C74 File Offset: 0x00007E74
		protected virtual void LogLimitExceeded(K key, int threadLimit, ulong sessionId, string mdb)
		{
		}

		// Token: 0x040000B0 RID: 176
		private Dictionary<K, int> map;

		// Token: 0x040000B1 RID: 177
		private string keyDisplayName;

		// Token: 0x040000B2 RID: 178
		private int estimatedEntrySize;

		// Token: 0x040000B3 RID: 179
		private SmtpResponse exceededResponse;

		// Token: 0x040000B4 RID: 180
		private string exceptionMessage;

		// Token: 0x040000B5 RID: 181
		private bool shouldRemoveEntryOnZero;

		// Token: 0x040000B6 RID: 182
		private int threadLimit;

		// Token: 0x040000B7 RID: 183
		private Dictionary<K, int> threadLimitsForDiagnostics;
	}
}
