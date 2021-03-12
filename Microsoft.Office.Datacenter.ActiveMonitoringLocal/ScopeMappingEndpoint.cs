using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200005F RID: 95
	internal abstract class ScopeMappingEndpoint : IDisposable
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00019317 File Offset: 0x00017517
		internal static bool IsDCEnvironment
		{
			get
			{
				return ScopeMappingEndpoint.isDCEnvironment;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001931E File Offset: 0x0001751E
		internal static bool IsSystemMonitoringEnvironment
		{
			get
			{
				return ScopeMappingEndpoint.isSystemMonitoringEnvironment;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00019325 File Offset: 0x00017525
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001932D File Offset: 0x0001752D
		public ConcurrentDictionary<string, ScopeMapping> ScopeMappings { get; protected set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00019336 File Offset: 0x00017536
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0001933E File Offset: 0x0001753E
		public ConcurrentDictionary<string, SystemMonitoringMapping> SystemMonitoringMappings { get; protected set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00019347 File Offset: 0x00017547
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0001934F File Offset: 0x0001754F
		public ConcurrentDictionary<string, ScopeMapping> DefaultScopes { get; protected set; }

		// Token: 0x060005EA RID: 1514 RVA: 0x00019358 File Offset: 0x00017558
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005EB RID: 1515
		internal abstract void InitializeScopeAndSystemMonitoringMappings();

		// Token: 0x060005EC RID: 1516
		internal abstract void InitializeDefaultScopes();

		// Token: 0x060005ED RID: 1517 RVA: 0x000193E8 File Offset: 0x000175E8
		protected void InitializeScopeAndSystemMonitoringMappingsFromXml(XmlNode scope, ConcurrentDictionary<string, ScopeMapping> scopeMappings, ConcurrentDictionary<string, SystemMonitoringMapping> smMappings, ScopeMapping parentScopeMapping = null)
		{
			if (scope.Attributes["Name"] == null)
			{
				return;
			}
			if (scope.Attributes["Type"] == null)
			{
				return;
			}
			ScopeMapping scopeMapping = new ScopeMapping
			{
				ScopeName = scope.Attributes["Name"].Value,
				ScopeType = scope.Attributes["Type"].Value,
				SystemMonitoringInstances = ((parentScopeMapping != null) ? parentScopeMapping.SystemMonitoringInstances : null),
				Parent = parentScopeMapping
			};
			XmlNodeList xmlNodeList = scope.SelectNodes("SystemMonitoring/Instance");
			if (xmlNodeList != null && xmlNodeList.Count > 0)
			{
				scopeMapping.SystemMonitoringInstances = new List<SystemMonitoringMapping>();
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text = (!string.IsNullOrWhiteSpace(ScopeMappingEndpoint.systemMonitoringInstance)) ? ScopeMappingEndpoint.systemMonitoringInstance : xmlNode.Attributes["Name"].Value;
					SystemMonitoringMapping smMapping = null;
					if (!smMappings.TryGetValue(text, out smMapping))
					{
						smMapping = new SystemMonitoringMapping
						{
							InstanceName = text,
							Scopes = null,
							Uploader = this.GetBatchingUploader(text)
						};
						smMappings[text] = smMapping;
					}
					if (!scopeMapping.SystemMonitoringInstances.Exists((SystemMonitoringMapping i) => i.InstanceName.Equals(smMapping.InstanceName, StringComparison.InvariantCultureIgnoreCase)))
					{
						scopeMapping.SystemMonitoringInstances.Add(smMapping);
					}
				}
			}
			if (scope.Attributes["Exclude"] == null || !scope.Attributes["Exclude"].Value.Equals("True", StringComparison.InvariantCultureIgnoreCase))
			{
				scopeMappings[scopeMapping.ScopeName] = scopeMapping;
				if (scopeMapping.SystemMonitoringInstances != null)
				{
					scopeMapping.SystemMonitoringInstances.ForEach(delegate(SystemMonitoringMapping sm)
					{
						if (sm.Scopes == null)
						{
							sm.Scopes = new List<ScopeMapping>();
						}
						if (!sm.Scopes.Exists((ScopeMapping s) => s.ScopeName.Equals(scopeMapping.ScopeName, StringComparison.InvariantCultureIgnoreCase)))
						{
							sm.Scopes.Add(scopeMapping);
						}
					});
				}
			}
			foreach (object obj2 in scope.SelectNodes("Scope"))
			{
				XmlNode scope2 = (XmlNode)obj2;
				this.InitializeScopeAndSystemMonitoringMappingsFromXml(scope2, scopeMappings, smMappings, scopeMapping);
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000196A8 File Offset: 0x000178A8
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.batchingUploaders != null)
				{
					foreach (KeyValuePair<string, BatchingUploader<ScopeNotificationRawData>> keyValuePair in this.batchingUploaders)
					{
						if (keyValuePair.Value != null)
						{
							keyValuePair.Value.Dispose();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00019720 File Offset: 0x00017920
		private BatchingUploader<ScopeNotificationRawData> GetBatchingUploader(string uploadUri)
		{
			BatchingUploader<ScopeNotificationRawData> batchingUploader;
			if (!this.batchingUploaders.TryGetValue(uploadUri, out batchingUploader))
			{
				batchingUploader = new BatchingUploader<ScopeNotificationRawData>(this.encoder, uploadUri, 20000, TimeSpan.FromSeconds(10.0), 10, 1, 3, false, "", false, null, null, null, null, false);
				this.batchingUploaders[uploadUri] = batchingUploader;
			}
			return batchingUploader;
		}

		// Token: 0x040003EA RID: 1002
		internal const int RecurrenceIntervalSeconds = 0;

		// Token: 0x040003EB RID: 1003
		private static bool isDCEnvironment = string.Equals(Settings.Environment, "DC", StringComparison.InvariantCultureIgnoreCase);

		// Token: 0x040003EC RID: 1004
		private static bool isSystemMonitoringEnvironment = string.Equals(Settings.Environment, "SM", StringComparison.InvariantCultureIgnoreCase);

		// Token: 0x040003ED RID: 1005
		private static string systemMonitoringInstance = Settings.SystemMonitoringInstance;

		// Token: 0x040003EE RID: 1006
		private DataContractSerializerEncoder<ScopeNotificationRawData> encoder = new DataContractSerializerEncoder<ScopeNotificationRawData>();

		// Token: 0x040003EF RID: 1007
		private ConcurrentDictionary<string, BatchingUploader<ScopeNotificationRawData>> batchingUploaders = new ConcurrentDictionary<string, BatchingUploader<ScopeNotificationRawData>>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040003F0 RID: 1008
		private bool disposed;

		// Token: 0x02000060 RID: 96
		protected enum ServiceEnvironment
		{
			// Token: 0x040003F5 RID: 1013
			Prod,
			// Token: 0x040003F6 RID: 1014
			Sdf,
			// Token: 0x040003F7 RID: 1015
			Pdt,
			// Token: 0x040003F8 RID: 1016
			Gallatin,
			// Token: 0x040003F9 RID: 1017
			Test
		}
	}
}
