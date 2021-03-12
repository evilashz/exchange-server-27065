using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BF0 RID: 3056
	internal class DnsCache
	{
		// Token: 0x060042ED RID: 17133 RVA: 0x000B23AE File Offset: 0x000B05AE
		public DnsCache() : this(10000)
		{
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000B23BB File Offset: 0x000B05BB
		public DnsCache(int cacheSize)
		{
			this.cacheSize = cacheSize;
			this.data = new Dictionary<DnsQuery, DnsResult>(cacheSize);
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060042EF RID: 17135 RVA: 0x000B23EC File Offset: 0x000B05EC
		public int MaxCacheSize
		{
			get
			{
				return this.cacheSize;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x000B23F4 File Offset: 0x000B05F4
		public int Count
		{
			get
			{
				int count;
				try
				{
					this.syncRoot.EnterReadLock();
					count = this.data.Count;
				}
				finally
				{
					try
					{
						this.syncRoot.ExitReadLock();
					}
					catch (ApplicationException)
					{
					}
				}
				return count;
			}
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000B2448 File Offset: 0x000B0648
		public static DnsCache CreateFromSystem()
		{
			return DnsCache.CreateFromFile(DnsCache.HostsFile);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000B2454 File Offset: 0x000B0654
		public static DnsCache CreateFromFile(string path)
		{
			DnsCache dnsCache = new DnsCache();
			dnsCache.UpdateFromFile(path);
			return dnsCache;
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000B2470 File Offset: 0x000B0670
		public DnsResult Find(DnsQuery query)
		{
			try
			{
				this.syncRoot.EnterReadLock();
				DnsResult dnsResult;
				if (this.data.TryGetValue(query, out dnsResult))
				{
					dnsResult.UpdateLastAccess();
					return dnsResult;
				}
			}
			finally
			{
				try
				{
					this.syncRoot.ExitReadLock();
				}
				catch (ApplicationException)
				{
				}
			}
			return null;
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000B24D4 File Offset: 0x000B06D4
		public void Add(DnsQuery query, DnsResult results)
		{
			try
			{
				this.syncRoot.EnterWriteLock();
				this.data[query] = results;
				if (this.data.Count >= this.MaxCacheSize + this.hostsFileEntries)
				{
					this.ExpireCache(10);
				}
			}
			finally
			{
				try
				{
					this.syncRoot.ExitWriteLock();
				}
				catch (ApplicationException)
				{
				}
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000B254C File Offset: 0x000B074C
		public void FlushCache()
		{
			try
			{
				this.syncRoot.EnterWriteLock();
				this.ExpireCache(100);
			}
			finally
			{
				try
				{
					this.syncRoot.ExitWriteLock();
				}
				catch (ApplicationException)
				{
				}
			}
			this.quadATimeOuts.Clear();
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000B25A8 File Offset: 0x000B07A8
		public void UpdateFromFile(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			this.pathToHostsFile = path;
			string directoryName = Path.GetDirectoryName(path);
			string fileName = Path.GetFileName(path);
			FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(directoryName, fileName);
			fileSystemWatcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime);
			fileSystemWatcher.Deleted += this.UpdateCache;
			fileSystemWatcher.Changed += this.UpdateCache;
			fileSystemWatcher.Created += this.UpdateCache;
			fileSystemWatcher.Renamed += new RenamedEventHandler(this.UpdateCache);
			fileSystemWatcher.EnableRaisingEvents = true;
			fileSystemWatcher = Interlocked.Exchange<FileSystemWatcher>(ref this.fileSystemWatcher, fileSystemWatcher);
			if (fileSystemWatcher != null)
			{
				fileSystemWatcher.EnableRaisingEvents = false;
				fileSystemWatcher.Dispose();
			}
			this.UpdateCache(null, null);
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000B2664 File Offset: 0x000B0864
		public void Close()
		{
			FileSystemWatcher fileSystemWatcher = Interlocked.Exchange<FileSystemWatcher>(ref this.fileSystemWatcher, null);
			if (fileSystemWatcher != null)
			{
				fileSystemWatcher.EnableRaisingEvents = false;
				fileSystemWatcher.Dispose();
			}
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000B2690 File Offset: 0x000B0890
		private void UpdateCache(object sender, FileSystemEventArgs eventArgs)
		{
			int num = 3;
			bool flag;
			do
			{
				flag = false;
				num--;
				try
				{
					this.syncRoot.EnterWriteLock();
					this.hostsFileEntries = 0;
					this.data.Clear();
					this.LoadPermanentEntries();
					Stream stream = new FileStream(this.pathToHostsFile, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete, 4096, FileOptions.SequentialScan);
					using (StreamReader streamReader = new StreamReader(stream, Encoding.ASCII, false))
					{
						this.LoadFromStream(streamReader);
						this.hostsFileEntries = this.data.Count;
					}
				}
				catch (FileNotFoundException arg)
				{
					ExTraceGlobals.DNSTracer.TraceError<string, FileNotFoundException>(0L, "DNS address cache failed to find file \"{0}\". {1}", this.pathToHostsFile, arg);
				}
				catch (DirectoryNotFoundException arg2)
				{
					ExTraceGlobals.DNSTracer.TraceError<string, DirectoryNotFoundException>(0L, "DNS address cache failed to find directory \"{0}\". {1}", this.pathToHostsFile, arg2);
				}
				catch (IOException arg3)
				{
					ExTraceGlobals.DNSTracer.TraceError<string, IOException>(0L, "DNS address cache failed to read file \"{0}\". {1}", this.pathToHostsFile, arg3);
					flag = true;
				}
				catch (UnauthorizedAccessException arg4)
				{
					ExTraceGlobals.DNSTracer.TraceError<string, UnauthorizedAccessException>(0L, "DNS address cache was not authorized to access file \"{0}\". {1}", this.pathToHostsFile, arg4);
				}
				finally
				{
					try
					{
						this.syncRoot.ExitWriteLock();
					}
					catch (ApplicationException)
					{
					}
				}
				if (num > 0 && flag)
				{
					Thread.Sleep(1000);
				}
			}
			while (num > 0 && flag);
			if (flag)
			{
				ExTraceGlobals.DNSTracer.TraceError<string>(0L, "DNS address cache failed to read file \"{0}\". Retry Count exceeded.", this.pathToHostsFile);
			}
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000B282C File Offset: 0x000B0A2C
		private void ExpireCache(int purgePercentage)
		{
			ExTraceGlobals.DNSTracer.TraceDebug((long)this.GetHashCode(), "Expiring cache");
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan t = TimeSpan.FromDays(365.0);
			List<DnsCache.DnsExpireEntry> list = new List<DnsCache.DnsExpireEntry>(this.data.Count);
			foreach (KeyValuePair<DnsQuery, DnsResult> keyValuePair in this.data)
			{
				if (!keyValuePair.Value.IsPermanentEntry)
				{
					DateTime dateTime = keyValuePair.Value.LastAccess;
					if (keyValuePair.Value.Expires < utcNow)
					{
						dateTime -= t;
					}
					DnsCache.DnsExpireEntry dnsExpireEntry = new DnsCache.DnsExpireEntry(keyValuePair.Key, dateTime);
					int num = list.BinarySearch(dnsExpireEntry, dnsExpireEntry);
					if (num < 0)
					{
						num = ~num;
					}
					list.Insert(num, dnsExpireEntry);
				}
			}
			ExTraceGlobals.DNSTracer.TraceDebug<int>((long)this.GetHashCode(), "Expiration list contains {0} candidates", list.Count);
			int num2 = this.data.Count * purgePercentage / 100;
			foreach (DnsCache.DnsExpireEntry dnsExpireEntry2 in list)
			{
				if (num2-- <= 0)
				{
					break;
				}
				ExTraceGlobals.DNSTracer.TraceDebug<DnsQuery, DateTime>((long)this.GetHashCode(), "Purging {0}, lastAccess {1}", dnsExpireEntry2.Query, dnsExpireEntry2.LastAccessDate);
				this.data.Remove(dnsExpireEntry2.Query);
			}
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000B29C8 File Offset: 0x000B0BC8
		private void LoadFromStream(StreamReader sr)
		{
			string text;
			while ((text = sr.ReadLine()) != null)
			{
				text = text.TrimStart(null);
				if (!string.IsNullOrEmpty(text) && text[0] != '#')
				{
					int num = text.IndexOf('#');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					string[] array = text.Split(DnsCache.Dividers, StringSplitOptions.RemoveEmptyEntries);
					IPAddress address;
					if (array.Length >= 2 && IPAddress.TryParse(array[0], out address))
					{
						string text2 = Dns.TrimTrailingDot(array[1]);
						this.AddAddressRecord(address, text2);
						this.AddPtrRecord(address, text2);
						for (int i = 2; i < array.Length; i++)
						{
							this.AddCnameRecord(Dns.TrimTrailingDot(array[i]), text2);
						}
					}
				}
			}
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000B2A78 File Offset: 0x000B0C78
		private void LoadPermanentEntries()
		{
			DnsQuery key = new DnsQuery(DnsRecordType.MX, "localhost");
			DnsResult value = new DnsResult(DnsStatus.InfoDomainNonexistent, IPAddress.None, DnsResult.NoExpiration);
			this.data[key] = value;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000B2AB0 File Offset: 0x000B0CB0
		private void AddAddressRecord(IPAddress address, string host)
		{
			DnsQuery dnsQuery;
			DnsRecord dnsRecord;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				dnsQuery = new DnsQuery(DnsRecordType.A, host);
				dnsRecord = new DnsARecord(dnsQuery.Question, address);
			}
			else
			{
				dnsQuery = new DnsQuery(DnsRecordType.AAAA, host);
				dnsRecord = new DnsAAAARecord(dnsQuery.Question, address);
			}
			dnsRecord.Section = DnsResponseSection.Answer;
			dnsRecord.TimeToLive = TimeSpan.MaxValue;
			DnsResult dnsResult;
			DnsRecordList dnsRecordList;
			if (this.data.TryGetValue(dnsQuery, out dnsResult))
			{
				dnsRecordList = dnsResult.List;
				using (IEnumerator<DnsRecord> enumerator = DnsRecordList.EnumerateAddresses(dnsRecordList).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DnsRecord dnsRecord2 = enumerator.Current;
						DnsAddressRecord dnsAddressRecord = (DnsAddressRecord)dnsRecord2;
						if (address.Equals(dnsAddressRecord.IPAddress))
						{
							return;
						}
					}
					goto IL_C9;
				}
			}
			dnsRecordList = new DnsRecordList();
			dnsResult = new DnsResult(DnsStatus.Success, IPAddress.None, dnsRecordList, DnsResult.NoExpiration);
			this.data[dnsQuery] = dnsResult;
			IL_C9:
			dnsRecordList.Add(dnsRecord);
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000B2BA0 File Offset: 0x000B0DA0
		private void AddPtrRecord(IPAddress address, string host)
		{
			DnsQuery dnsQuery = new DnsQuery(DnsRecordType.PTR, PtrRequest.ConstructPTRQuery(address));
			DnsResult dnsResult;
			DnsRecordList dnsRecordList;
			if (this.data.TryGetValue(dnsQuery, out dnsResult))
			{
				dnsRecordList = dnsResult.List;
				using (IEnumerator<DnsRecord> enumerator = dnsRecordList.EnumerateAnswers(DnsRecordType.PTR).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DnsRecord dnsRecord = enumerator.Current;
						DnsPtrRecord dnsPtrRecord = (DnsPtrRecord)dnsRecord;
						if (dnsPtrRecord.Host.Equals(host, StringComparison.OrdinalIgnoreCase))
						{
							return;
						}
					}
					goto IL_90;
				}
			}
			dnsRecordList = new DnsRecordList();
			dnsResult = new DnsResult(DnsStatus.Success, IPAddress.None, dnsRecordList, DnsResult.NoExpiration);
			this.data[dnsQuery] = dnsResult;
			IL_90:
			dnsRecordList.Add(new DnsPtrRecord(dnsQuery.Question, host)
			{
				Section = DnsResponseSection.Answer,
				TimeToLive = TimeSpan.MaxValue
			});
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000B2C78 File Offset: 0x000B0E78
		private void AddCnameRecord(string host, string alias)
		{
			DnsQuery dnsQuery = new DnsQuery(DnsRecordType.CNAME, host);
			DnsResult dnsResult;
			DnsRecordList dnsRecordList;
			if (this.data.TryGetValue(dnsQuery, out dnsResult))
			{
				dnsRecordList = dnsResult.List;
				using (IEnumerator<DnsRecord> enumerator = dnsRecordList.EnumerateAnswers(DnsRecordType.PTR).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DnsRecord dnsRecord = enumerator.Current;
						DnsPtrRecord dnsPtrRecord = (DnsPtrRecord)dnsRecord;
						if (dnsPtrRecord.Host.Equals(alias, StringComparison.OrdinalIgnoreCase))
						{
							return;
						}
					}
					goto IL_8A;
				}
			}
			dnsRecordList = new DnsRecordList();
			dnsResult = new DnsResult(DnsStatus.Success, IPAddress.None, dnsRecordList, DnsResult.NoExpiration);
			this.data[dnsQuery] = dnsResult;
			IL_8A:
			dnsRecordList.Add(new DnsCNameRecord(dnsQuery.Question, alias)
			{
				Section = DnsResponseSection.Answer,
				TimeToLive = TimeSpan.MaxValue
			});
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000B2D51 File Offset: 0x000B0F51
		public void AddAaaaQueryTimeOut(string domain)
		{
			this.quadATimeOuts.AddOrUpdate(domain, 0, (string k, int v) => v + 1);
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000B2D80 File Offset: 0x000B0F80
		public void RemoveAaaaTimeOutEntry(string domainName)
		{
			int num;
			this.quadATimeOuts.TryRemove(domainName, out num);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000B2D9C File Offset: 0x000B0F9C
		public int GetAaaaTimeOutEntry(string domainName)
		{
			int result;
			if (this.quadATimeOuts.TryGetValue(domainName, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x000B2DBC File Offset: 0x000B0FBC
		public void AddDiagnosticInfoTo(XElement dnsCacheElement)
		{
			dnsCacheElement.SetAttributeValue("CacheSize", this.cacheSize);
			XElement xelement = new XElement("Cache");
			XElement xelement2 = new XElement("TimeOuts");
			dnsCacheElement.Add(xelement);
			dnsCacheElement.Add(xelement2);
			xelement.SetAttributeValue("Count", this.data.Count);
			foreach (KeyValuePair<DnsQuery, DnsResult> keyValuePair in this.data)
			{
				XElement xelement3 = new XElement("Entry");
				XElement content = new XElement("Query", keyValuePair.Key);
				XElement content2 = new XElement("Result", keyValuePair.Value);
				xelement3.Add(content);
				xelement3.Add(content2);
				xelement.Add(xelement3);
			}
			xelement2.SetAttributeValue("Count", this.quadATimeOuts.Count);
			foreach (KeyValuePair<string, int> keyValuePair2 in this.quadATimeOuts)
			{
				XElement xelement4 = new XElement("Entry");
				XElement content3 = new XElement("domain", keyValuePair2.Key);
				XElement content4 = new XElement("timeoutCount", keyValuePair2.Value);
				xelement4.Add(content3);
				xelement4.Add(content4);
				xelement2.Add(xelement4);
			}
		}

		// Token: 0x0400390A RID: 14602
		private const int DefaultCacheSize = 10000;

		// Token: 0x0400390B RID: 14603
		private const int PurgePercentage = 10;

		// Token: 0x0400390C RID: 14604
		public const int QuadATimeoutNotFound = -1;

		// Token: 0x0400390D RID: 14605
		private static readonly string HostsFile = Path.Combine(Environment.SystemDirectory, "drivers\\etc\\HOSTS");

		// Token: 0x0400390E RID: 14606
		private static readonly char[] Dividers = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x0400390F RID: 14607
		private Dictionary<DnsQuery, DnsResult> data;

		// Token: 0x04003910 RID: 14608
		private int cacheSize;

		// Token: 0x04003911 RID: 14609
		private int hostsFileEntries;

		// Token: 0x04003912 RID: 14610
		private ReaderWriterLockSlim syncRoot = new ReaderWriterLockSlim();

		// Token: 0x04003913 RID: 14611
		private string pathToHostsFile;

		// Token: 0x04003914 RID: 14612
		private FileSystemWatcher fileSystemWatcher;

		// Token: 0x04003915 RID: 14613
		private ConcurrentDictionary<string, int> quadATimeOuts = new ConcurrentDictionary<string, int>();

		// Token: 0x02000BF1 RID: 3057
		private class DnsExpireEntry : IComparer<DnsCache.DnsExpireEntry>
		{
			// Token: 0x06004305 RID: 17157 RVA: 0x000B2FBC File Offset: 0x000B11BC
			internal DnsExpireEntry(DnsQuery query, DateTime lastAccessDate)
			{
				this.query = query;
				this.lastAccessDate = lastAccessDate;
			}

			// Token: 0x170010C5 RID: 4293
			// (get) Token: 0x06004306 RID: 17158 RVA: 0x000B2FD2 File Offset: 0x000B11D2
			internal DnsQuery Query
			{
				get
				{
					return this.query;
				}
			}

			// Token: 0x170010C6 RID: 4294
			// (get) Token: 0x06004307 RID: 17159 RVA: 0x000B2FDA File Offset: 0x000B11DA
			internal DateTime LastAccessDate
			{
				get
				{
					return this.lastAccessDate;
				}
			}

			// Token: 0x06004308 RID: 17160 RVA: 0x000B2FE2 File Offset: 0x000B11E2
			public int Compare(DnsCache.DnsExpireEntry x, DnsCache.DnsExpireEntry y)
			{
				if (x == y)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				if (x.lastAccessDate < y.lastAccessDate)
				{
					return -1;
				}
				if (!(x.lastAccessDate > y.lastAccessDate))
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x04003917 RID: 14615
			private DnsQuery query;

			// Token: 0x04003918 RID: 14616
			private DateTime lastAccessDate;
		}
	}
}
