using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000FC RID: 252
	internal abstract class RequestDetailsLoggerBase<T> : DisposeTrackableBase where T : RequestDetailsLoggerBase<T>, new()
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001C88E File Offset: 0x0001AA8E
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001C89A File Offset: 0x0001AA9A
		public static T Current
		{
			get
			{
				return RequestDetailsLoggerBase<T>.GetCurrent(HttpContext.Current);
			}
			private set
			{
				RequestDetailsLoggerBase<T>.SetCurrent(HttpContext.Current, value);
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		public static T GetCurrent(HttpContext httpContext)
		{
			if (httpContext != null && httpContext.Items != null)
			{
				return (T)((object)httpContext.Items[RequestDetailsLoggerBase<T>.ContextItemKey]);
			}
			if (RequestDetailsLoggerBase<T>.AdditionalLoggerGetterForGetCurrent != null)
			{
				return RequestDetailsLoggerBase<T>.AdditionalLoggerGetterForGetCurrent();
			}
			return default(T);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001C8F1 File Offset: 0x0001AAF1
		public static void SetCurrent(HttpContext httpContext, T logger)
		{
			if (httpContext != null && httpContext.Items != null)
			{
				httpContext.Items[RequestDetailsLoggerBase<T>.ContextItemKey] = logger;
				return;
			}
			if (RequestDetailsLoggerBase<T>.AdditionalLoggerSetterForSetCurrent != null)
			{
				RequestDetailsLoggerBase<T>.AdditionalLoggerSetterForSetCurrent(logger);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001C927 File Offset: 0x0001AB27
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001C92E File Offset: 0x0001AB2E
		public static bool IsInitialized { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001C936 File Offset: 0x0001AB36
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001C93E File Offset: 0x0001AB3E
		public bool SkipLogging { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001C947 File Offset: 0x0001AB47
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001C94F File Offset: 0x0001AB4F
		public bool EndActivityContext { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001C958 File Offset: 0x0001AB58
		public Guid ActivityId
		{
			get
			{
				if (this.ActivityScope != null)
				{
					return this.ActivityScope.ActivityId;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001C973 File Offset: 0x0001AB73
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001C97B File Offset: 0x0001AB7B
		public IActivityScope ActivityScope { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001C984 File Offset: 0x0001AB84
		public object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001C98C File Offset: 0x0001AB8C
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001C993 File Offset: 0x0001AB93
		protected static int? ErrorMessageLengthThreshold { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001C99B File Offset: 0x0001AB9B
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001C9A2 File Offset: 0x0001ABA2
		protected static bool ProcessExceptionMessage { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001C9AA File Offset: 0x0001ABAA
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001C9B1 File Offset: 0x0001ABB1
		protected static Func<T> AdditionalLoggerGetterForGetCurrent { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001C9B9 File Offset: 0x0001ABB9
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		protected static Action<T> AdditionalLoggerSetterForSetCurrent { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		internal static bool IsDebugBuild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001C9CB File Offset: 0x0001ABCB
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		internal static RequestLoggerConfig RequestLoggerConfig { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001C9DA File Offset: 0x0001ABDA
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001C9E1 File Offset: 0x0001ABE1
		protected internal static Log Log { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001C9E9 File Offset: 0x0001ABE9
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		private protected static LogSchema LogSchema { protected get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001CA00 File Offset: 0x0001AC00
		private protected LogRowFormatter Row { protected get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		protected Dictionary<Enum, long> Latencies
		{
			get
			{
				if (this.latencies == null)
				{
					lock (this.latenciesLock)
					{
						if (this.latencies == null)
						{
							this.latencies = new Dictionary<Enum, long>(RequestDetailsLoggerBase<T>.RequestLoggerConfig.DefaultLatencyDictionarySize);
						}
					}
				}
				return this.latencies;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001CA74 File Offset: 0x0001AC74
		public static RequestLoggerConfig GetConfig()
		{
			RequestLoggerConfig requestLoggerConfig;
			using (T t = Activator.CreateInstance<T>())
			{
				requestLoggerConfig = t.GetRequestLoggerConfig();
			}
			return requestLoggerConfig;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001CAC0 File Offset: 0x0001ACC0
		public static T InitializeRequestLogger()
		{
			return RequestDetailsLoggerBase<T>.InitializeRequestLogger(null);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		public static T InitializeRequestLogger(IActivityScope activityScope)
		{
			T t = RequestDetailsLoggerBase<T>.Current;
			if (t == null)
			{
				lock (RequestDetailsLoggerBase<T>.staticSyncRoot)
				{
					t = RequestDetailsLoggerBase<T>.Current;
					if (t == null)
					{
						t = Activator.CreateInstance<T>();
						t.EndActivityContext = true;
						t.httpContext = HttpContext.Current;
						RequestDetailsLoggerBase<T>.Current = t;
					}
				}
			}
			t.InitializeRequest(activityScope);
			return t;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001CB58 File Offset: 0x0001AD58
		public static string[] GetColumnNames()
		{
			RequestLoggerConfig config = RequestDetailsLoggerBase<T>.GetConfig();
			string[] array = new string[config.Columns.Count];
			int num = 0;
			foreach (KeyValuePair<string, Enum> keyValuePair in config.Columns)
			{
				array[num++] = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001CBD7 File Offset: 0x0001ADD7
		public static void SafeSetLogger(RequestDetailsLoggerBase<T> requestDetailsLogger, Enum key, object value)
		{
			RequestDetailsLoggerBase<T>.SafeLogOperation<Enum, object>(requestDetailsLogger, key, value, delegate(RequestDetailsLoggerBase<T> logger, Enum k, object v)
			{
				logger.Set(k, v);
			});
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001CBFE File Offset: 0x0001ADFE
		public static void SafeAppendDetailedExchangePrincipalLatency(RequestDetailsLoggerBase<T> requestDetailsLogger, string key, string value)
		{
			RequestDetailsLoggerBase<T>.SafeAppendColumn(requestDetailsLogger, ServiceLatencyMetadata.DetailedExchangePrincipalLatency, key, value);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001CC34 File Offset: 0x0001AE34
		public static void SafeAppendColumn(RequestDetailsLoggerBase<T> requestDetailsLogger, Enum columnName, string key, string value)
		{
			RequestDetailsLoggerBase<T>.SafeLogOperation<string, string>(requestDetailsLogger, key, value, delegate(RequestDetailsLoggerBase<T> logger, string k, string v)
			{
				logger.SafeAppend(columnName, key, value);
			});
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001CC7A File Offset: 0x0001AE7A
		protected static void SafeAppendBackEndGenericInfo(RequestDetailsLoggerBase<T> requestDetailsLogger, string key, string value)
		{
			RequestDetailsLoggerBase<T>.SafeAppendColumn(requestDetailsLogger, ServiceCommonMetadata.BackEndGenericInfo, key, value);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001CC8B File Offset: 0x0001AE8B
		public static void SafeAppendAuthenticationError(RequestDetailsLoggerBase<T> requestDetailsLogger, string key, string value)
		{
			RequestDetailsLoggerBase<T>.SafeAppendColumn(requestDetailsLogger, ServiceCommonMetadata.AuthenticationErrors, key, value);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		public static void SafeAppendGenericInfo(RequestDetailsLoggerBase<T> requestDetailsLogger, string key, object value)
		{
			if (value != null && RequestDetailsLoggerBase<T>.RequestLoggerConfig != null)
			{
				RequestDetailsLoggerBase<T>.SafeAppendColumn(requestDetailsLogger, RequestDetailsLoggerBase<T>.RequestLoggerConfig.GenericInfoColumn, key, value.ToString());
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001CCBF File Offset: 0x0001AEBF
		public static void SafeAppendGenericError(RequestDetailsLoggerBase<T> requestDetailsLogger, string key, string value)
		{
			RequestDetailsLoggerBase<T>.SafeAppendColumn(requestDetailsLogger, ServiceCommonMetadata.GenericErrors, key, value);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		private static void SafeLogOperation<TKey, TValue>(RequestDetailsLoggerBase<T> requestDetailsLogger, TKey key, TValue value, RequestDetailsLoggerBase<T>.LogOperation<TKey, TValue> logOperation)
		{
			if (requestDetailsLogger == null || requestDetailsLogger.IsDisposed || key == null || value == null)
			{
				return;
			}
			try
			{
				if (Monitor.TryEnter(requestDetailsLogger.SyncRoot))
				{
					if (!requestDetailsLogger.IsDisposed)
					{
						logOperation(requestDetailsLogger, key, value);
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(requestDetailsLogger.SyncRoot))
				{
					Monitor.Exit(requestDetailsLogger.SyncRoot);
				}
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001CD4E File Offset: 0x0001AF4E
		public static void SafeLogRequestException(RequestDetailsLoggerBase<T> requestDetailsLogger, Exception ex, string keyPrefix)
		{
			RequestDetailsLoggerBase<T>.SafeLogOperation<Exception, string>(requestDetailsLogger, ex, keyPrefix, delegate(RequestDetailsLoggerBase<T> logger, Exception k, string v)
			{
				logger.LogExceptionToGenericError(k, v);
			});
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001CD78 File Offset: 0x0001AF78
		internal static string GetCorrectServerNameFromExceptionIfNecessary(string exceptionName, Exception ex)
		{
			if (exceptionName.Equals("Microsoft.Exchange.Data.Storage.WrongServerException", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					MethodInfo method = ex.GetType().GetMethod("RightServerToString");
					if (method != null)
					{
						return method.Invoke(ex, null) as string;
					}
					ExWatson.SendReport(new MissingMethodException("Microsoft.Exchange.Data.Storage.WrongServerException", "RightServerToString"), ReportOptions.DoNotFreezeThreads, null);
				}
				catch (Exception exception)
				{
					ExWatson.SendReport(exception, ReportOptions.DoNotFreezeThreads, null);
				}
			}
			return null;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		private static void SafeInitializeLogger(RequestDetailsLoggerBase<T> requestDetailsLoggerBase)
		{
			if (!RequestDetailsLoggerBase<T>.IsInitialized)
			{
				lock (RequestDetailsLoggerBase<T>.staticSyncRoot)
				{
					if (!RequestDetailsLoggerBase<T>.IsInitialized)
					{
						requestDetailsLoggerBase.InitializeLogger();
						RequestDetailsLoggerBase<T>.IsInitialized = true;
					}
				}
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001CE4C File Offset: 0x0001B04C
		private static void GlobalActivityLogger(object sender, ActivityEventArgs args)
		{
			IActivityScope activityScope = sender as IActivityScope;
			if (activityScope.ActivityType == ActivityType.Global && (args.ActivityEventType == ActivityEventType.EndActivity || args.ActivityEventType == ActivityEventType.SuspendActivity))
			{
				RequestDetailsLoggerBase<T> requestDetailsLoggerBase = RequestDetailsLoggerBase<T>.InitializeRequestLogger(activityScope);
				ServiceCommonMetadataPublisher.PublishServerInfo(requestDetailsLoggerBase.ActivityScope);
				requestDetailsLoggerBase.Commit();
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001CE98 File Offset: 0x0001B098
		private static string FormatForCsv(string value)
		{
			if (value.Contains(","))
			{
				value = value.Replace(',', ' ');
			}
			if (value.Contains("\r\n"))
			{
				value = value.Replace("\r\n", " ");
			}
			return value;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001CED3 File Offset: 0x0001B0D3
		public virtual void Commit()
		{
			this.Dispose();
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		public virtual string Set(Enum property, object value)
		{
			string text = null;
			try
			{
				if (this.VerifyIsDisposed())
				{
					return null;
				}
				if (value != null)
				{
					text = LogRowFormatter.Format(value);
				}
				this.ActivityScope.SetProperty(property, text);
			}
			catch (ActivityContextException exception)
			{
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, false), ReportOptions.None);
			}
			return text;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001CF34 File Offset: 0x0001B134
		public virtual string Get(Enum property)
		{
			string result = null;
			try
			{
				if (base.IsDisposed)
				{
					return null;
				}
				result = this.ActivityScope.GetProperty(property);
			}
			catch (ActivityContextException exception)
			{
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, false), ReportOptions.None);
			}
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001CF84 File Offset: 0x0001B184
		public void AppendGenericInfo(string key, object value)
		{
			RequestDetailsLoggerBase<T>.SafeAppendGenericInfo(this, key, value);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001CF8E File Offset: 0x0001B18E
		public void ExcludeLogEntry()
		{
			this.excludeLogEntry = true;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001CF98 File Offset: 0x0001B198
		public void TrackLatency(Enum latencyMetadata, Action method)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				method();
			}
			finally
			{
				stopwatch.Stop();
				this.UpdateLatency(latencyMetadata, (double)stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		public TResult TrackLatency<TResult>(Enum latencyMetadata, Func<TResult> method)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			TResult result;
			try
			{
				result = method();
			}
			finally
			{
				stopwatch.Stop();
				this.UpdateLatency(latencyMetadata, (double)stopwatch.ElapsedMilliseconds);
			}
			return result;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001D028 File Offset: 0x0001B228
		public TResult TrackLatency<TResult>(Enum latencyMetadata, Func<TResult> method, out double latencyValue)
		{
			TResult result;
			try
			{
				result = this.TrackLatency<TResult>(latencyMetadata, method);
			}
			finally
			{
				long num = 0L;
				this.TryGetLatency(latencyMetadata, out num);
				latencyValue = (double)num;
			}
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001D064 File Offset: 0x0001B264
		public bool TryGetLatency(Enum latencyMetadata, out long valueInMilliseconds)
		{
			valueInMilliseconds = 0L;
			if (base.IsDisposed || latencyMetadata == null)
			{
				return false;
			}
			try
			{
				if (Monitor.TryEnter(this.SyncRoot))
				{
					if (base.IsDisposed)
					{
						return false;
					}
					lock (this.latenciesLock)
					{
						if (!this.Latencies.ContainsKey(latencyMetadata))
						{
							return false;
						}
						valueInMilliseconds = this.Latencies[latencyMetadata];
						return true;
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.SyncRoot))
				{
					Monitor.Exit(this.SyncRoot);
				}
			}
			return false;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001D114 File Offset: 0x0001B314
		public void UpdateLatency(Enum latencyMetadata, double latencyInMilliseconds)
		{
			if (base.IsDisposed || latencyMetadata == null)
			{
				return;
			}
			try
			{
				if (Monitor.TryEnter(this.SyncRoot))
				{
					if (!base.IsDisposed)
					{
						lock (this.latenciesLock)
						{
							if (!this.Latencies.ContainsKey(latencyMetadata))
							{
								this.Latencies.Add(latencyMetadata, 0L);
							}
							Dictionary<Enum, long> dictionary;
							(dictionary = this.Latencies)[latencyMetadata] = dictionary[latencyMetadata] + (long)latencyInMilliseconds;
						}
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.SyncRoot))
				{
					Monitor.Exit(this.SyncRoot);
				}
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		protected virtual void InitializeLogger()
		{
			RequestDetailsLoggerBase<T>.RequestLoggerConfig = this.GetRequestLoggerConfig();
			string[] array = new string[RequestDetailsLoggerBase<T>.RequestLoggerConfig.Columns.Count];
			ReadOnlyCollection<KeyValuePair<string, Enum>> columns = RequestDetailsLoggerBase<T>.RequestLoggerConfig.Columns;
			int num = 0;
			foreach (KeyValuePair<string, Enum> keyValuePair in columns)
			{
				array[num] = keyValuePair.Key;
				RequestDetailsLoggerBase<T>.enumToIndexMap.Add(keyValuePair.Value, num);
				num++;
			}
			RequestDetailsLoggerBase<T>.LogSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", RequestDetailsLoggerBase<T>.RequestLoggerConfig.LogType, array);
			RequestDetailsLoggerBase<T>.Log = new Log(RequestDetailsLoggerBase<T>.RequestLoggerConfig.LogFilePrefix, new LogHeaderFormatter(RequestDetailsLoggerBase<T>.LogSchema, true), RequestDetailsLoggerBase<T>.RequestLoggerConfig.LogComponent);
			string text = ConfigurationManager.AppSettings[RequestDetailsLoggerBase<T>.RequestLoggerConfig.FolderPathAppSettingsKey];
			if (string.IsNullOrEmpty(text))
			{
				text = RequestDetailsLoggerBase<T>.RequestLoggerConfig.FallbackLogFolderPath;
			}
			RequestDetailsLoggerBase<T>.Log.Configure(text, RequestDetailsLoggerBase<T>.RequestLoggerConfig.MaxAge, RequestDetailsLoggerBase<T>.RequestLoggerConfig.MaxDirectorySize, RequestDetailsLoggerBase<T>.RequestLoggerConfig.MaxLogFileSize, true);
			ActivityContext.OnActivityEvent += RequestDetailsLoggerBase<T>.GlobalActivityLogger;
			this.SetPerLogLineSizeLimit();
		}

		// Token: 0x0600074A RID: 1866
		protected abstract RequestLoggerConfig GetRequestLoggerConfig();

		// Token: 0x0600074B RID: 1867 RVA: 0x0001D318 File Offset: 0x0001B518
		protected virtual void PreCommitTasks()
		{
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001D31A File Offset: 0x0001B51A
		protected virtual bool IsIgnorableException(Exception ex)
		{
			return false;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001D31D File Offset: 0x0001B51D
		protected virtual bool LogFullException(Exception ex)
		{
			return true;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001D320 File Offset: 0x0001B520
		protected virtual void SetPerLogLineSizeLimit()
		{
			ActivityScopeImpl.MaxAppendableColumnLength = new int?(16384);
			RequestDetailsLoggerBase<T>.ErrorMessageLengthThreshold = new int?(250);
			RequestDetailsLoggerBase<T>.ProcessExceptionMessage = true;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001D346 File Offset: 0x0001B546
		public virtual bool ShouldSendDebugResponseHeaders()
		{
			return false;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001D349 File Offset: 0x0001B549
		protected virtual string GetDebugHeaderSource()
		{
			return "BE";
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001D350 File Offset: 0x0001B550
		private void SafeAppend(Enum property, string key, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				value = value.Replace(",", " ").Replace(";", " ").Replace("=", " ").Replace(Environment.NewLine, " ");
				this.Append(property, key, value);
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		protected void Append(Enum property, string key, string value)
		{
			StringBuilder stringBuilder = new StringBuilder(key.Length + value.Length + 2);
			stringBuilder.Append(key);
			stringBuilder.Append("=");
			stringBuilder.Append(value);
			stringBuilder.Append(";");
			try
			{
				if (!this.VerifyIsDisposed())
				{
					this.ActivityScope.AppendToProperty(property, stringBuilder.ToString());
				}
			}
			catch (ActivityContextException exception)
			{
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, false), ReportOptions.None);
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001D43C File Offset: 0x0001B63C
		protected void LogExceptionToGenericError(Exception ex, string keyPrefix)
		{
			if (!RequestDetailsLoggerBase<T>.ProcessExceptionMessage)
			{
				this.ActivityScope.SetProperty(ServiceCommonMetadata.GenericErrors, ex.ToString());
				string fullName = ex.GetType().FullName;
				this.ActivityScope.SetProperty(ServiceCommonMetadata.ExceptionName, fullName);
				string correctServerNameFromExceptionIfNecessary = RequestDetailsLoggerBase<T>.GetCorrectServerNameFromExceptionIfNecessary(fullName, ex);
				if (!string.IsNullOrWhiteSpace(correctServerNameFromExceptionIfNecessary))
				{
					this.ActivityScope.SetProperty(ServiceCommonMetadata.CorrectBEServerToUse, correctServerNameFromExceptionIfNecessary);
				}
				return;
			}
			if (this.LogFullException(ex))
			{
				this.AppendGenericError(keyPrefix, ex.ToString());
				return;
			}
			this.AppendGenericError(keyPrefix + "_Type", ex.GetType().Name);
			this.AppendGenericError(keyPrefix + "_Message", ex.Message);
			this.AppendGenericError(keyPrefix + "_OuterStackTrace", ex.StackTrace);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001D50C File Offset: 0x0001B70C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RequestDetailsLoggerBase<T>>(this);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001D55C File Offset: 0x0001B75C
		protected override void InternalDispose(bool disposing)
		{
			if (!this.isDisposeAlreadyCalled && disposing && this.ActivityScope != null)
			{
				lock (this.SyncRoot)
				{
					if (!this.isDisposeAlreadyCalled)
					{
						this.excludeLogEntry |= this.SkipLogging;
						try
						{
							if (this.ActivityScope.ActivityType == ActivityType.Request)
							{
								this.FetchLatencyData();
								if (!this.excludeLogEntry)
								{
									this.PreCommitTasks();
								}
							}
							if (this.EndActivityContext && this.ActivityScope.Status == ActivityContextStatus.ActivityStarted)
							{
								this.ActivityScope.End();
							}
							if (!this.excludeLogEntry)
							{
								List<KeyValuePair<string, object>> formattableMetadata = this.ActivityScope.GetFormattableMetadata();
								formattableMetadata.RemoveAll(delegate(KeyValuePair<string, object> pair)
								{
									Enum @enum = ActivityContext.LookupEnum(pair.Key);
									return object.Equals(@enum, RequestDetailsLoggerBase<T>.RequestLoggerConfig.GenericInfoColumn) || object.Equals(@enum, ActivityStandardMetadata.ReturnClientRequestId) || RequestDetailsLoggerBase<T>.enumToIndexMap.ContainsKey(@enum);
								});
								List<KeyValuePair<string, object>> collection = WorkloadManagementLogger.FormatWlmActivity(this.ActivityScope, false);
								formattableMetadata.AddRange(collection);
								foreach (KeyValuePair<Enum, int> keyValuePair in RequestDetailsLoggerBase<T>.enumToIndexMap)
								{
									string text = this.Get(keyValuePair.Key);
									if (object.Equals(RequestDetailsLoggerBase<T>.RequestLoggerConfig.GenericInfoColumn, keyValuePair.Key))
									{
										text += LogRowFormatter.FormatCollection(formattableMetadata);
									}
									if (!string.IsNullOrEmpty(text))
									{
										text = RequestDetailsLoggerBase<T>.FormatForCsv(text);
										this.Row[keyValuePair.Value] = text;
									}
								}
								RequestDetailsLoggerBase<T>.Log.Append(this.Row, -1);
								this.UploadDataToStreamInsight();
							}
						}
						finally
						{
							if (this.ActivityScope != null && this.EndActivityContext && this.ActivityScope.Status == ActivityContextStatus.ActivityStarted)
							{
								this.ActivityScope.End();
							}
							this.isDisposeAlreadyCalled = true;
						}
					}
				}
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001D770 File Offset: 0x0001B970
		protected bool TryGetLogColumnValueByEnum(Enum columnName, out string value)
		{
			value = string.Empty;
			if (columnName == null)
			{
				return false;
			}
			try
			{
				int index;
				if (RequestDetailsLoggerBase<T>.enumToIndexMap.TryGetValue(columnName, out index) && this.Row[index] != null)
				{
					value = this.Row[index].ToString();
					return true;
				}
			}
			catch (Exception ex)
			{
				Log.EventLog.LogEvent(CommonEventLogConstants.Tuple_StreamInsightsDataUploaderGetValueFailed, columnName.ToString(), new object[]
				{
					RequestDetailsLoggerBase<T>.RequestLoggerConfig.LogComponent,
					ex.Message
				});
			}
			return false;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001D80C File Offset: 0x0001BA0C
		protected static bool TryGetLogColumnIndexByEnum(Enum columnName, out int keyIndex)
		{
			return RequestDetailsLoggerBase<T>.enumToIndexMap.TryGetValue(columnName, out keyIndex);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001D81A File Offset: 0x0001BA1A
		protected virtual void UploadDataToStreamInsight()
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001D81C File Offset: 0x0001BA1C
		private void InitializeRequest(IActivityScope activityScope)
		{
			RequestDetailsLoggerBase<T>.SafeInitializeLogger(this);
			lock (this.SyncRoot)
			{
				if (this.ActivityScope == null)
				{
					if (activityScope == null)
					{
						this.ActivityScope = ActivityContext.Start(null);
					}
					else
					{
						this.ActivityScope = activityScope;
					}
					this.Row = new LogRowFormatter(RequestDetailsLoggerBase<T>.LogSchema);
				}
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001D88C File Offset: 0x0001BA8C
		private void FetchLatencyData()
		{
			if (this.latencies != null)
			{
				foreach (KeyValuePair<Enum, long> keyValuePair in this.latencies)
				{
					this.Set(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		public void AppendGenericError(string key, string value)
		{
			RequestDetailsLoggerBase<T>.SafeAppendGenericError(this, key, value);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001D906 File Offset: 0x0001BB06
		public void PushDebugInfoToResponseHeaders()
		{
			if (this.httpContext != null)
			{
				this.PushDebugInfoToResponseHeaders(this.httpContext);
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		public void PushDebugInfoToResponseHeaders(HttpContext httpContext)
		{
			if (httpContext != null && this.ShouldSendDebugResponseHeaders())
			{
				RequestDetailsLoggerBase<T>.SafeLogOperation<string, string>(this, string.Empty, string.Empty, delegate(RequestDetailsLoggerBase<T> logger, string k, string v)
				{
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.LiveIdBasicError);
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.LiveIdBasicLog);
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.LiveIdNegotiateError);
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.OAuthError);
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.GenericInfo);
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.AuthenticationErrors);
					logger.AddToResponseHeadersIfAvailable(httpContext, ServiceCommonMetadata.GenericErrors);
				});
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001DA05 File Offset: 0x0001BC05
		public void PushDebugInfoToResponseHeaders(string headerSuffix, string value)
		{
			if (base.IsDisposed)
			{
				return;
			}
			if (this.httpContext != null && this.ShouldSendDebugResponseHeaders())
			{
				this.AddToResponseHeadersIfAvailable(this.httpContext, headerSuffix, value);
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001DA30 File Offset: 0x0001BC30
		private void AddToResponseHeadersIfAvailable(HttpContext httpContext, Enum property)
		{
			if (base.IsDisposed)
			{
				return;
			}
			string value = this.Get(property);
			this.AddToResponseHeadersIfAvailable(httpContext, property.ToString(), value);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		private void AddToResponseHeadersIfAvailable(HttpContext httpContext, string headerSuffix, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (value.Length > 1000)
				{
					value = value.Substring(0, 1000);
				}
				value = AntiXssEncoder.HtmlEncode(value, false);
				httpContext.SetResponseHeader(this.GetDebugHeaderSource(), headerSuffix, value);
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001DA98 File Offset: 0x0001BC98
		private bool VerifyIsDisposed()
		{
			return base.IsDisposed;
		}

		// Token: 0x0400048E RID: 1166
		public const string KeyValueSeparator = "=";

		// Token: 0x0400048F RID: 1167
		public const string PairSeparator = ";";

		// Token: 0x04000490 RID: 1168
		public const string ColumnSeparator = ",";

		// Token: 0x04000491 RID: 1169
		public const string SingleSplace = " ";

		// Token: 0x04000492 RID: 1170
		public const int ResponseHeaderValueLengthLimit = 1000;

		// Token: 0x04000493 RID: 1171
		private static readonly string ContextItemKey = string.Format("{0}-Logger", typeof(T).FullName);

		// Token: 0x04000494 RID: 1172
		private static Dictionary<Enum, int> enumToIndexMap = new Dictionary<Enum, int>();

		// Token: 0x04000495 RID: 1173
		private static object staticSyncRoot = new object();

		// Token: 0x04000496 RID: 1174
		protected HttpContext httpContext;

		// Token: 0x04000497 RID: 1175
		private bool isDisposeAlreadyCalled;

		// Token: 0x04000498 RID: 1176
		private bool excludeLogEntry;

		// Token: 0x04000499 RID: 1177
		private Dictionary<Enum, long> latencies;

		// Token: 0x0400049A RID: 1178
		private object latenciesLock = new object();

		// Token: 0x0400049B RID: 1179
		private object syncRoot = new object();

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000768 RID: 1896
		protected delegate void LogOperation<TKey, TValue>(RequestDetailsLoggerBase<T> logger, TKey key, TValue value);
	}
}
