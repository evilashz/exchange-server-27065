using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x02000004 RID: 4
	public class ExchangeResourceManager : ResourceManager
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000023C8 File Offset: 0x000005C8
		public static ExchangeResourceManager GetResourceManager(string baseName, Assembly assembly)
		{
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			string key = baseName + assembly.GetName().Name;
			ExchangeResourceManager result;
			lock (ExchangeResourceManager.resourceManagers)
			{
				ExchangeResourceManager exchangeResourceManager = null;
				if (!ExchangeResourceManager.resourceManagers.TryGetValue(key, out exchangeResourceManager))
				{
					exchangeResourceManager = new ExchangeResourceManager(baseName, assembly);
					ExchangeResourceManager.resourceManagers.Add(key, exchangeResourceManager);
				}
				result = exchangeResourceManager;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002454 File Offset: 0x00000654
		private ExchangeResourceManager(string baseName, Assembly assembly) : base(baseName, assembly)
		{
			this.resourceReleaseStopwatch.Start();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002493 File Offset: 0x00000693
		public override string BaseName
		{
			get
			{
				return base.BaseName;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000249B File Offset: 0x0000069B
		public string AssemblyName
		{
			get
			{
				return this.MainAssembly.GetName().FullName;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024AD File Offset: 0x000006AD
		public override string GetString(string name)
		{
			return this.GetString(name, CultureInfo.CurrentUICulture);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024BC File Offset: 0x000006BC
		public override string GetString(string name, CultureInfo culture)
		{
			CultureInfo cultureInfo = culture ?? CultureInfo.CurrentUICulture;
			SipCultureInfoBase sipCultureInfoBase = cultureInfo as SipCultureInfoBase;
			if (sipCultureInfoBase != null)
			{
				bool useSipName = sipCultureInfoBase.UseSipName;
				try
				{
					sipCultureInfoBase.UseSipName = true;
					return this.GetStringInternal(name, sipCultureInfoBase);
				}
				finally
				{
					sipCultureInfoBase.UseSipName = useSipName;
				}
			}
			return this.GetStringInternal(name, cultureInfo);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000251C File Offset: 0x0000071C
		protected virtual string GetStringInternal(string name, CultureInfo culture)
		{
			string text = null;
			try
			{
				this.readerWriterLock.EnterReadLock();
				text = base.GetString(name, culture);
			}
			finally
			{
				this.readerWriterLock.ExitReadLock();
			}
			if (text == null)
			{
				try
				{
					this.readerWriterLock.EnterWriteLock();
					if (this.resourceReleaseStopwatch.Elapsed > this.resourceReleaseInterval)
					{
						base.ReleaseAllResources();
						this.resourceReleaseStopwatch.Restart();
					}
					text = base.GetString(name, culture);
				}
				finally
				{
					this.readerWriterLock.ExitWriteLock();
				}
			}
			return text;
		}

		// Token: 0x04000006 RID: 6
		private static Dictionary<string, ExchangeResourceManager> resourceManagers = new Dictionary<string, ExchangeResourceManager>();

		// Token: 0x04000007 RID: 7
		private readonly TimeSpan resourceReleaseInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000008 RID: 8
		private Stopwatch resourceReleaseStopwatch = new Stopwatch();

		// Token: 0x04000009 RID: 9
		private ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();

		// Token: 0x02000005 RID: 5
		public class Concurrent : ExchangeResourceManager
		{
			// Token: 0x0600000E RID: 14 RVA: 0x000025C4 File Offset: 0x000007C4
			public Concurrent(ExchangeResourceManager resourceManager) : base(resourceManager.BaseName, resourceManager.MainAssembly)
			{
				this.resourceManager = resourceManager;
			}

			// Token: 0x0600000F RID: 15 RVA: 0x000025EC File Offset: 0x000007EC
			protected override string GetStringInternal(string name, CultureInfo culture)
			{
				Tuple<string, string> key = new Tuple<string, string>(name, culture.Name);
				string stringInternal;
				if (this.cache.TryGetValue(key, out stringInternal))
				{
					return stringInternal;
				}
				stringInternal = this.resourceManager.GetStringInternal(name, culture);
				if (stringInternal != null)
				{
					this.cache[key] = stringInternal;
				}
				return stringInternal;
			}

			// Token: 0x0400000A RID: 10
			private readonly ExchangeResourceManager resourceManager;

			// Token: 0x0400000B RID: 11
			private readonly ConcurrentDictionary<Tuple<string, string>, string> cache = new ConcurrentDictionary<Tuple<string, string>, string>();
		}
	}
}
