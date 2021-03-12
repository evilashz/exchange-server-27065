using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000343 RID: 835
	internal class ProcessingQuotaComponent : IProcessingQuotaComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00088F3D File Offset: 0x0008713D
		private PerformanceCounter ProcessorTimeCounter
		{
			get
			{
				if (this.processorTimeCounter == null)
				{
					this.processorTimeCounter = new PerformanceCounter("Process", "% Processor Time", "EdgeTransport");
				}
				return this.processorTimeCounter;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x00088F68 File Offset: 0x00087168
		private Type SessionType
		{
			get
			{
				if (this.sessionType == null)
				{
					Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.Data");
					this.sessionType = assembly.GetType("Microsoft.Exchange.Hygiene.Data.Reporting.ReportingSession");
				}
				return this.sessionType;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00088FA5 File Offset: 0x000871A5
		private ITenantThrottlingSession TenantSession
		{
			get
			{
				if (this.tenantSession == null)
				{
					this.tenantSession = (ITenantThrottlingSession)Activator.CreateInstance(this.SessionType);
				}
				return this.tenantSession;
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x00088FCC File Offset: 0x000871CC
		public ProcessingQuotaComponent.ProcessingData GetQuotaOverride(Guid externalOrgId)
		{
			ProcessingQuotaComponent.ProcessingData processingData;
			if (this.tenantOverrides.TryGetValue(externalOrgId, out processingData) && (processingData.IsAdmin || this.cpuTriggered))
			{
				return processingData;
			}
			return null;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00088FFC File Offset: 0x000871FC
		public ProcessingQuotaComponent.ProcessingData GetQuotaOverride(WaitCondition condition)
		{
			TenantBasedCondition tenantBasedCondition = condition as TenantBasedCondition;
			if (tenantBasedCondition != null)
			{
				return this.GetQuotaOverride(tenantBasedCondition.TenantId);
			}
			return null;
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00089024 File Offset: 0x00087224
		public void SetLoadTimeDependencies(TransportAppConfig.IProcessingQuotaConfig processingQuota)
		{
			this.config = processingQuota;
			this.enabled = (this.config.EnforceProcessingQuota || this.config.TestProcessingQuota);
			this.lastUpdateTime = DateTime.UtcNow - this.config.UpdateThrottlingDataInterval;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00089074 File Offset: 0x00087274
		public void TimedUpdate()
		{
			if (!this.enabled)
			{
				return;
			}
			bool flag = this.processorTimeCounter != null;
			this.currentProcessorTimeValue = this.ProcessorTimeCounter.NextValue() / (float)Environment.ProcessorCount;
			if (flag && (this.currentProcessorTimeValue >= (float)this.config.HighWatermarkCpuPercentage || (this.cpuTriggered && this.currentProcessorTimeValue >= (float)this.config.LowWatermarkCpuPercentage)))
			{
				this.cpuTriggered = true;
			}
			else
			{
				this.cpuTriggered = false;
			}
			if (this.lastUpdateTime.Add(this.config.UpdateThrottlingDataInterval) < DateTime.UtcNow && Interlocked.CompareExchange(ref this.updateInProgress, 1, 0) == 0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.FetchTenantOverrides));
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00089138 File Offset: 0x00087338
		private void FetchTenantOverrides(object state)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.config.ThrottleDataFilePath))
				{
					this.ReadTestOverrideData();
				}
				else
				{
					try
					{
						IEnumerable<TenantThrottleInfo> tenantThrottlingDigest = this.TenantSession.GetTenantThrottlingDigest(0, null, false, 5000, true);
						this.ParseTenantOverrideData(tenantThrottlingDigest);
					}
					catch (TransientException ex)
					{
						ExTraceGlobals.GeneralTracer.TraceError<TransientException>((long)this.GetHashCode(), "Transient exception when fetching override data: {0}", ex);
						ProcessingQuotaComponent.logger.LogEvent(TransportEventLogConstants.Tuple_CategorizerErrorRetrievingTenantOverride, this.GetDiagnosticComponentName(), new object[]
						{
							ex
						});
					}
					catch (DataSourceOperationException ex2)
					{
						ExTraceGlobals.GeneralTracer.TraceError<DataSourceOperationException>((long)this.GetHashCode(), "Permanent exception when fetching override data: {0}", ex2);
						ProcessingQuotaComponent.logger.LogEvent(TransportEventLogConstants.Tuple_CategorizerErrorRetrievingTenantOverride, this.GetDiagnosticComponentName(), new object[]
						{
							ex2
						});
					}
					catch (DataValidationException ex3)
					{
						ExTraceGlobals.GeneralTracer.TraceError<DataValidationException>((long)this.GetHashCode(), "Data exception when fetching override data: {0}", ex3);
						ProcessingQuotaComponent.logger.LogEvent(TransportEventLogConstants.Tuple_CategorizerErrorRetrievingTenantOverride, this.GetDiagnosticComponentName(), new object[]
						{
							ex3
						});
					}
				}
			}
			finally
			{
				Interlocked.CompareExchange(ref this.updateInProgress, 0, 1);
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000892C0 File Offset: 0x000874C0
		private void ParseTenantOverrideData(IEnumerable<TenantThrottleInfo> tenants)
		{
			if (tenants == null)
			{
				return;
			}
			Dictionary<Guid, ProcessingQuotaComponent.ProcessingData> currentEntries = new Dictionary<Guid, ProcessingQuotaComponent.ProcessingData>();
			foreach (TenantThrottleInfo tenantThrottleInfo in tenants)
			{
				ProcessingQuotaComponent.ProcessingData processingData = new ProcessingQuotaComponent.ProcessingData();
				switch (tenantThrottleInfo.ThrottleState)
				{
				case TenantThrottleState.Auto:
					if (tenantThrottleInfo.ThrottlingFactor < 0.0 || tenantThrottleInfo.ThrottlingFactor > 1.0)
					{
						ExTraceGlobals.GeneralTracer.TraceError<double, Guid>((long)this.GetHashCode(), "ThrottlingFactor of {0} for tenant {1} is outside allowed range [0,1]", tenantThrottleInfo.ThrottlingFactor, tenantThrottleInfo.TenantId);
						continue;
					}
					processingData.ThrottlingFactor = tenantThrottleInfo.ThrottlingFactor;
					break;
				case TenantThrottleState.Throttled:
					processingData.ThrottlingFactor = 0.0;
					processingData.IsAdmin = true;
					break;
				case TenantThrottleState.Unthrottled:
					processingData.ThrottlingFactor = 1.0;
					processingData.IsAdmin = true;
					break;
				default:
					ExTraceGlobals.GeneralTracer.TraceError<TenantThrottleState, Guid>((long)this.GetHashCode(), "ThrottleState of {0} for tenant {1} unrecognized", tenantThrottleInfo.ThrottleState, tenantThrottleInfo.TenantId);
					continue;
				}
				TransportHelpers.AttemptAddToDictionary<Guid, ProcessingQuotaComponent.ProcessingData>(currentEntries, tenantThrottleInfo.TenantId, processingData, null);
			}
			this.tenantOverrides = currentEntries;
			this.lastUpdateTime = DateTime.UtcNow;
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x00089404 File Offset: 0x00087604
		private void ReadTestOverrideData()
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(this.config.ThrottleDataFilePath, FileMode.Open, FileAccess.Read);
				StreamReader streamReader = new StreamReader(fileStream);
				Dictionary<Guid, ProcessingQuotaComponent.ProcessingData> currentEntries = new Dictionary<Guid, ProcessingQuotaComponent.ProcessingData>();
				while (!streamReader.EndOfStream)
				{
					string[] array = streamReader.ReadLine().Split(new char[]
					{
						','
					});
					Guid keyToAdd;
					double num;
					if (array.Length != 2)
					{
						ExTraceGlobals.GeneralTracer.TraceError<string>((long)this.GetHashCode(), "skipping malformed line {0}", string.Join("", array));
					}
					else if (!Guid.TryParse(array[0], out keyToAdd))
					{
						ExTraceGlobals.GeneralTracer.TraceError<string>((long)this.GetHashCode(), "skipping malformed tenant {0}", array[0]);
					}
					else if (!double.TryParse(array[1], out num) || num < 0.0 || num > 1.0)
					{
						ExTraceGlobals.GeneralTracer.TraceError<string, string>((long)this.GetHashCode(), "skipping tenant {0} because of malformed throttling factor {1} ", array[0], array[1]);
					}
					else
					{
						ProcessingQuotaComponent.ProcessingData processingData = new ProcessingQuotaComponent.ProcessingData();
						processingData.ThrottlingFactor = num;
						processingData.IsAdmin = (processingData.ThrottlingFactor == 1.0 || processingData.ThrottlingFactor == 0.0);
						TransportHelpers.AttemptAddToDictionary<Guid, ProcessingQuotaComponent.ProcessingData>(currentEntries, keyToAdd, processingData, null);
					}
				}
				this.tenantOverrides = currentEntries;
				this.lastUpdateTime = DateTime.UtcNow;
			}
			catch (IOException arg)
			{
				ExTraceGlobals.GeneralTracer.TraceError<IOException>((long)this.GetHashCode(), "ioexception when reading file {0}", arg);
			}
			catch (SecurityException arg2)
			{
				ExTraceGlobals.GeneralTracer.TraceError<SecurityException>((long)this.GetHashCode(), "securityException when reading file {0}", arg2);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000895E8 File Offset: 0x000877E8
		public string GetDiagnosticComponentName()
		{
			return "ProcessingQuota";
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000895F0 File Offset: 0x000877F0
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag2)
			{
				xelement.Add(TransportAppConfig.GetDiagnosticInfoForType(this.config));
			}
			xelement.Add(new XElement("LastProcessorMeasurement", this.currentProcessorTimeValue));
			xelement.Add(new XElement("LastLoadTime", this.lastUpdateTime));
			xelement.Add(new XElement("ThrottlingTriggered", this.cpuTriggered));
			xelement.Add(new XElement("OverrideCount", this.tenantOverrides.Count));
			if (flag)
			{
				Dictionary<Guid, ProcessingQuotaComponent.ProcessingData> dictionary = this.tenantOverrides;
				XElement xelement2 = new XElement("tenantOverrides");
				foreach (KeyValuePair<Guid, ProcessingQuotaComponent.ProcessingData> keyValuePair in dictionary)
				{
					XElement xelement3 = new XElement("ProcessingOverride");
					xelement3.SetAttributeValue("Tenant", keyValuePair.Key);
					xelement3.SetAttributeValue("ThrottleFactor", keyValuePair.Value.ThrottlingFactor);
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			if (flag3)
			{
				xelement.Add(new XElement("help", "Supported arguments: verbose, config, help"));
			}
			return xelement;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000897D0 File Offset: 0x000879D0
		public void Load()
		{
			this.TimedUpdate();
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000897D8 File Offset: 0x000879D8
		public void Unload()
		{
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000897DA File Offset: 0x000879DA
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x040012AF RID: 4783
		private const string DiagnosticsComponentName = "ProcessingQuota";

		// Token: 0x040012B0 RID: 4784
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x040012B1 RID: 4785
		private TransportAppConfig.IProcessingQuotaConfig config;

		// Token: 0x040012B2 RID: 4786
		private bool enabled;

		// Token: 0x040012B3 RID: 4787
		private DateTime lastUpdateTime;

		// Token: 0x040012B4 RID: 4788
		private int updateInProgress;

		// Token: 0x040012B5 RID: 4789
		private Dictionary<Guid, ProcessingQuotaComponent.ProcessingData> tenantOverrides = new Dictionary<Guid, ProcessingQuotaComponent.ProcessingData>();

		// Token: 0x040012B6 RID: 4790
		private Type sessionType;

		// Token: 0x040012B7 RID: 4791
		private ITenantThrottlingSession tenantSession;

		// Token: 0x040012B8 RID: 4792
		private PerformanceCounter processorTimeCounter;

		// Token: 0x040012B9 RID: 4793
		private bool cpuTriggered;

		// Token: 0x040012BA RID: 4794
		private float currentProcessorTimeValue;

		// Token: 0x02000344 RID: 836
		internal class ProcessingData
		{
			// Token: 0x06002410 RID: 9232 RVA: 0x0008980B File Offset: 0x00087A0B
			public ProcessingData()
			{
			}

			// Token: 0x06002411 RID: 9233 RVA: 0x00089813 File Offset: 0x00087A13
			public ProcessingData(double throttleFactor)
			{
				this.throttlingFactor = throttleFactor;
			}

			// Token: 0x17000B37 RID: 2871
			// (get) Token: 0x06002412 RID: 9234 RVA: 0x00089822 File Offset: 0x00087A22
			// (set) Token: 0x06002413 RID: 9235 RVA: 0x0008982A File Offset: 0x00087A2A
			public double ThrottlingFactor
			{
				get
				{
					return this.throttlingFactor;
				}
				set
				{
					this.throttlingFactor = value;
				}
			}

			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x06002414 RID: 9236 RVA: 0x00089833 File Offset: 0x00087A33
			// (set) Token: 0x06002415 RID: 9237 RVA: 0x0008983B File Offset: 0x00087A3B
			public bool IsAdmin
			{
				get
				{
					return this.isAdmin;
				}
				set
				{
					this.isAdmin = value;
				}
			}

			// Token: 0x17000B39 RID: 2873
			// (get) Token: 0x06002416 RID: 9238 RVA: 0x00089844 File Offset: 0x00087A44
			public bool IsAllowListed
			{
				get
				{
					return this.throttlingFactor == 1.0;
				}
			}

			// Token: 0x17000B3A RID: 2874
			// (get) Token: 0x06002417 RID: 9239 RVA: 0x00089857 File Offset: 0x00087A57
			public bool IsBlocked
			{
				get
				{
					return this.throttlingFactor == 0.0;
				}
			}

			// Token: 0x040012BB RID: 4795
			internal const int AllowThrottlingFactor = 1;

			// Token: 0x040012BC RID: 4796
			internal const int BlockThrottlingFactor = 0;

			// Token: 0x040012BD RID: 4797
			private double throttlingFactor;

			// Token: 0x040012BE RID: 4798
			private bool isAdmin;
		}
	}
}
