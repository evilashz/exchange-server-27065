using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ABSession : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003696 File Offset: 0x00001896
		protected ABSession(Trace tracer) : this(tracer, CultureInfo.CurrentCulture.LCID)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000036A9 File Offset: 0x000018A9
		protected ABSession(Trace tracer, int lcid)
		{
			if (tracer == null)
			{
				throw new ArgumentNullException("tracer");
			}
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
			this.tracer = tracer;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006F RID: 111
		public abstract ABProviderCapabilities ProviderCapabilities { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000036D2 File Offset: 0x000018D2
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000036E0 File Offset: 0x000018E0
		public TimeSpan? Timeout
		{
			get
			{
				this.ThrowIfDisposed();
				return this.timeout;
			}
			set
			{
				this.ThrowIfDisposed();
				this.timeout = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000036EF File Offset: 0x000018EF
		public string ProviderName
		{
			get
			{
				this.ThrowIfDisposed();
				return this.InternalProviderName;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000036FD File Offset: 0x000018FD
		protected Trace Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000074 RID: 116
		protected abstract string InternalProviderName { get; }

		// Token: 0x06000075 RID: 117 RVA: 0x00003708 File Offset: 0x00001908
		public ABObject FindById(ABObjectId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<ABObjectId>(0L, "FindById ({0})", id);
			ABObject abobject = this.InternalFindById(id);
			this.TraceForObjectAfterFind(abobject);
			return abobject;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000374C File Offset: 0x0000194C
		public ABRawEntry FindById(ABObjectId id, ABPropertyDefinitionCollection properties)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			ABSession.ValidateProperties(properties);
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<ABObjectId, ABPropertyDefinitionCollection>(0L, "FindById(id={0}, properties={1})", id, properties);
			ABObjectId[] ids = new ABObjectId[]
			{
				id
			};
			IList<ABRawEntry> list = this.InternalFindByIds(ids, properties);
			this.Tracer.TraceDebug<int>(0L, "Provider returned '{0}' results.", list.Count);
			if (list.Count > 1)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Provider returned {0} results for FindById({1}).", new object[]
				{
					list.Count,
					id
				}));
			}
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003800 File Offset: 0x00001A00
		public IList<ABRawEntry> FindByIds(ICollection<ABObjectId> ids, ABPropertyDefinitionCollection properties)
		{
			if (ids == null)
			{
				throw new ArgumentNullException("ids");
			}
			if (ids.Count == 0)
			{
				throw new ArgumentException("ids collection can't be empty.");
			}
			ABSession.ValidateProperties(properties);
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<int, ABPropertyDefinitionCollection>(0L, "FindByIds({0} ids, properties={1})", ids.Count, properties);
			IList<ABRawEntry> list = this.InternalFindByIds(ids, properties);
			this.Tracer.TraceDebug<int>(0L, "Provider returned '{0}' results.", list.Count);
			return list;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003878 File Offset: 0x00001A78
		public IList<ABRawEntry> FindByANR(string anrMatch, int maxResults, ABPropertyDefinitionCollection properties)
		{
			if (string.IsNullOrEmpty(anrMatch))
			{
				throw new ArgumentNullException("anrMatch");
			}
			if (maxResults < 1)
			{
				throw new ArgumentOutOfRangeException("maxResults", "maxResults must be greater than 0 and smaller than or equal to " + 1000.ToString());
			}
			if (maxResults > 1000)
			{
				maxResults = 1000;
			}
			ABSession.ValidateProperties(properties);
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<string, int, ABPropertyDefinitionCollection>(0L, "FindByAnr({0}, {1}, {2})", anrMatch, maxResults, properties);
			IList<ABRawEntry> list = this.InternalFindByANR(anrMatch, maxResults, properties);
			if (list == null)
			{
				list = ABSession.emptyRawEntriesReadOnlyList;
			}
			else if (list.Count > maxResults)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "Provider '{0}' returned '{1}' rather than '{2}' maximum results.", new object[]
				{
					this.ProviderName,
					list.Count,
					maxResults
				});
				this.Tracer.TraceError(0L, message);
				throw new InvalidOperationException(message);
			}
			this.Tracer.TraceDebug<int>(0L, "Provider returned '{0}' results.", list.Count);
			return list;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003974 File Offset: 0x00001B74
		public IList<ABObject> FindByANR(string anrMatch, int maxResults)
		{
			if (string.IsNullOrEmpty(anrMatch))
			{
				throw new ArgumentNullException("anrMatch");
			}
			if (maxResults < 1)
			{
				throw new ArgumentOutOfRangeException("maxResults", "maxResults must be greater than 0 and smaller than or equal to " + 1000.ToString());
			}
			if (maxResults > 1000)
			{
				maxResults = 1000;
			}
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<string, int>(0L, "FindByAnr({0}, {1})", anrMatch, maxResults);
			IList<ABObject> list = this.InternalFindByANR(anrMatch, maxResults);
			if (list == null)
			{
				list = ABSession.emptyReadOnlyList;
			}
			else if (list.Count > maxResults)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "Provider '{0}' returned '{1}' rather than '{2}' maximum results.", new object[]
				{
					this.ProviderName,
					list.Count,
					maxResults
				});
				this.Tracer.TraceError(0L, message);
				throw new InvalidOperationException(message);
			}
			this.Tracer.TraceDebug<int>(0L, "Provider returned '{0}' results.", list.Count);
			return list;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003A68 File Offset: 0x00001C68
		public ABObject FindByProxyAddress(ProxyAddress proxyAddress)
		{
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<ProxyAddress>(0L, "FindByProxyAddress('{0}')", proxyAddress);
			ABObject abobject = this.InternalFindByProxyAddress(proxyAddress);
			this.TraceForObjectAfterFind(abobject);
			return abobject;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public ABObject FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			if (string.IsNullOrEmpty(legacyExchangeDN))
			{
				throw new ArgumentNullException("legacyExchangeDN");
			}
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<string>(0L, "FindByLegacyExchangeDN('{0}')", legacyExchangeDN);
			ABObject abobject = this.InternalFindByLegacyExchangeDN(legacyExchangeDN);
			this.TraceForObjectAfterFind(abobject);
			return abobject;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003B00 File Offset: 0x00001D00
		public ABRawEntry FindByLegacyExchangeDN(string legacyDN, ABPropertyDefinitionCollection properties)
		{
			if (string.IsNullOrEmpty(legacyDN))
			{
				throw new ArgumentNullException("legacyDN");
			}
			ABSession.ValidateProperties(properties);
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<string, ABPropertyDefinitionCollection>(0L, "FindByLegacyExchangeDN(id={0}, properties={1})", legacyDN, properties);
			string[] legacyDNs = new string[]
			{
				legacyDN
			};
			IList<ABRawEntry> list = this.InternalFindByLegacyExchangeDNs(legacyDNs, properties);
			this.Tracer.TraceDebug<int>(0L, "Provider returned '{0}' results.", list.Count);
			if (list.Count > 1)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Provider returned {0} results for FindByLegacyDN({1}).", new object[]
				{
					list.Count,
					legacyDN
				}));
			}
			if (list.Count != 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public IList<ABRawEntry> FindByLegacyExchangeDNs(ICollection<string> legacyDNs, ABPropertyDefinitionCollection properties)
		{
			if (legacyDNs == null)
			{
				throw new ArgumentNullException("legacyDNs");
			}
			if (legacyDNs.Count == 0)
			{
				throw new ArgumentException("LegacyDNs collection can't be empty.");
			}
			ABSession.ValidateProperties(properties);
			this.ThrowIfDisposed();
			this.Tracer.TraceDebug<int, ABPropertyDefinitionCollection>(0L, "FindByLegacyExchangeDNs({0} legacyDNs, properties={1})", legacyDNs.Count, properties);
			IList<ABRawEntry> list = this.InternalFindByLegacyExchangeDNs(legacyDNs, properties);
			this.Tracer.TraceDebug<int>(0L, "Provider returned '{0}' results.", list.Count);
			return list;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003C2D File Offset: 0x00001E2D
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.InternalDispose();
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003C5E File Offset: 0x00001E5E
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ABSession>(this);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003C66 File Offset: 0x00001E66
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000081 RID: 129
		protected abstract ABObject InternalFindById(ABObjectId id);

		// Token: 0x06000082 RID: 130 RVA: 0x00003C7C File Offset: 0x00001E7C
		protected virtual ABRawEntry InternalFindById(ABObjectId id, ABPropertyDefinitionCollection properties)
		{
			ABObject abobject = this.InternalFindById(id);
			if (abobject != null)
			{
				abobject.PropertyDefinitionCollection = properties;
			}
			return abobject;
		}

		// Token: 0x06000083 RID: 131
		protected abstract List<ABObject> InternalFindByANR(string anrMatch, int maxResults);

		// Token: 0x06000084 RID: 132 RVA: 0x00003C9C File Offset: 0x00001E9C
		protected virtual List<ABRawEntry> InternalFindByANR(string anrMatch, int maxResults, ABPropertyDefinitionCollection properties)
		{
			List<ABObject> list = this.InternalFindByANR(anrMatch, maxResults);
			List<ABRawEntry> list2 = new List<ABRawEntry>(list.Count);
			foreach (ABObject abobject in list)
			{
				if (abobject != null)
				{
					abobject.PropertyDefinitionCollection = properties;
					list2.Add(abobject);
				}
			}
			return list2;
		}

		// Token: 0x06000085 RID: 133
		protected abstract ABObject InternalFindByProxyAddress(ProxyAddress proxyAddress);

		// Token: 0x06000086 RID: 134
		protected abstract ABObject InternalFindByLegacyExchangeDN(string legacyExchangeDN);

		// Token: 0x06000087 RID: 135 RVA: 0x00003D0C File Offset: 0x00001F0C
		protected virtual ABRawEntry InternalFindByLegacyExchangeDN(string legacyExchangeDN, ABPropertyDefinitionCollection properties)
		{
			ABObject abobject = this.InternalFindByLegacyExchangeDN(legacyExchangeDN);
			if (abobject != null)
			{
				abobject.PropertyDefinitionCollection = properties;
			}
			return abobject;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003D2C File Offset: 0x00001F2C
		protected virtual void InternalDispose()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003D30 File Offset: 0x00001F30
		protected virtual IList<ABRawEntry> InternalFindByLegacyExchangeDNs(ICollection<string> legacyDNs, ABPropertyDefinitionCollection properties)
		{
			IList<ABRawEntry> list = new List<ABRawEntry>(legacyDNs.Count);
			foreach (string legacyExchangeDN in legacyDNs)
			{
				list.Add(this.InternalFindByLegacyExchangeDN(legacyExchangeDN, properties));
			}
			return list;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003D8C File Offset: 0x00001F8C
		protected virtual IList<ABRawEntry> InternalFindByIds(ICollection<ABObjectId> ids, ABPropertyDefinitionCollection properties)
		{
			IList<ABRawEntry> list = new List<ABRawEntry>(ids.Count);
			foreach (ABObjectId id in ids)
			{
				list.Add(this.InternalFindById(id, properties));
			}
			return list;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003DE8 File Offset: 0x00001FE8
		private static void ValidateProperties(ABPropertyDefinitionCollection properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003DF8 File Offset: 0x00001FF8
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("ABSession");
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003E0D File Offset: 0x0000200D
		private void TraceForObjectAfterFind(ABObject objectFound)
		{
			if (objectFound == null)
			{
				this.Tracer.TraceDebug(0L, "Provider didn't find object.");
				return;
			}
			this.Tracer.TraceDebug<string>(0L, "Provider found object with display name '{0}'.", objectFound.DisplayName);
		}

		// Token: 0x0400002F RID: 47
		public const int MaxSupportedAnrResult = 1000;

		// Token: 0x04000030 RID: 48
		private static IList<ABObject> emptyReadOnlyList = new ReadOnlyCollection<ABObject>(new List<ABObject>(0));

		// Token: 0x04000031 RID: 49
		private static IList<ABRawEntry> emptyRawEntriesReadOnlyList = new ReadOnlyCollection<ABRawEntry>(new List<ABRawEntry>(0));

		// Token: 0x04000032 RID: 50
		private DisposeTracker disposeTracker;

		// Token: 0x04000033 RID: 51
		private Trace tracer;

		// Token: 0x04000034 RID: 52
		private TimeSpan? timeout;

		// Token: 0x04000035 RID: 53
		private bool disposed;
	}
}
