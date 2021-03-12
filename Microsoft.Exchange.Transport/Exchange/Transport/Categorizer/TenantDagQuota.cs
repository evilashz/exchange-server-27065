using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200026D RID: 621
	internal class TenantDagQuota : ITenantDagQuota
	{
		// Token: 0x06001B26 RID: 6950 RVA: 0x0006F5F0 File Offset: 0x0006D7F0
		public TenantDagQuota(int dagsPerTenant, int messagesPerDag, double historyWeight, bool logDiagnosticInfo)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("dagsPerTenant", dagsPerTenant, 1, int.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<int>("messagesPerDag", messagesPerDag, 1, int.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<double>("historyWeight", historyWeight, 0.0, 1.0);
			this.defaultDagsPerTenant = dagsPerTenant;
			this.messagesPerDag = messagesPerDag;
			this.historyWeight = historyWeight;
			this.logDiagnosticInfo = logDiagnosticInfo;
			this.tenantDataDictionary = new ConcurrentDictionary<Guid, TenantDagQuota.TenantData>();
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0006F66C File Offset: 0x0006D86C
		public void RefreshDagCount(int dagsAvailable)
		{
			this.LogDiagnosticInfo();
			List<Guid> list = new List<Guid>();
			foreach (KeyValuePair<Guid, TenantDagQuota.TenantData> keyValuePair in this.tenantDataDictionary)
			{
				TenantDagQuota.TenantData value = keyValuePair.Value;
				if (value.DagsPerTenant <= this.defaultDagsPerTenant && value.MessageCount == 0)
				{
					list.Add(keyValuePair.Key);
				}
				int num = this.ComputeDagCount(value);
				value.DagsPerTenant = ((num > dagsAvailable) ? dagsAvailable : num);
				value.ResetMessageCount();
			}
			foreach (Guid key in list)
			{
				TenantDagQuota.TenantData tenantData;
				if (this.tenantDataDictionary.TryGetValue(key, out tenantData) && tenantData.MessageCount == 0)
				{
					this.tenantDataDictionary.TryRemove(key, out tenantData);
				}
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0006F76C File Offset: 0x0006D96C
		public int GetDagCountForTenant(Guid tenantId)
		{
			TenantDagQuota.TenantData tenantData;
			if (this.tenantDataDictionary.TryGetValue(tenantId, out tenantData))
			{
				return tenantData.DagsPerTenant;
			}
			return this.defaultDagsPerTenant;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0006F798 File Offset: 0x0006D998
		public void IncrementMessagesDeliveredToTenant(Guid tenantId)
		{
			TenantDagQuota.TenantData tenantData;
			if (!this.tenantDataDictionary.TryGetValue(tenantId, out tenantData))
			{
				this.tenantDataDictionary.TryAdd(tenantId, new TenantDagQuota.TenantData(this.defaultDagsPerTenant, 1));
				return;
			}
			tenantData.IncrementMessageCount();
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0006F7D8 File Offset: 0x0006D9D8
		public bool TryGetDiagnosticInfo(bool verbose, DiagnosableParameters parameters, out XElement diagnosticInfo)
		{
			bool flag = verbose;
			if (!flag)
			{
				flag = parameters.Argument.Equals("TenantDagQuota", StringComparison.InvariantCultureIgnoreCase);
			}
			if (flag)
			{
				diagnosticInfo = this.GetDiagnosticInfo();
				return true;
			}
			if (parameters.Argument.IndexOf("tenant:", StringComparison.OrdinalIgnoreCase) != -1)
			{
				diagnosticInfo = this.GetTenantDiagnosticInfo(parameters.Argument.Substring(7));
				return true;
			}
			diagnosticInfo = null;
			return false;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0006F83C File Offset: 0x0006DA3C
		private static XElement GetTenantDiagnosticInfo(Guid tenantId, TenantDagQuota.TenantData tenantData)
		{
			XElement xelement = new XElement("Tenant");
			xelement.SetAttributeValue("Id", tenantId);
			xelement.SetAttributeValue("DagsForTenant", tenantData.DagsPerTenant);
			xelement.SetAttributeValue("MessageCount", tenantData.MessageCount);
			return xelement;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0006F8A8 File Offset: 0x0006DAA8
		private XElement GetDiagnosticInfo()
		{
			XElement xelement;
			XElement tenantDagQuotaDiagnosticInfo = this.GetTenantDagQuotaDiagnosticInfo(out xelement);
			foreach (KeyValuePair<Guid, TenantDagQuota.TenantData> keyValuePair in this.tenantDataDictionary)
			{
				xelement.Add(TenantDagQuota.GetTenantDiagnosticInfo(keyValuePair.Key, keyValuePair.Value));
			}
			return tenantDagQuotaDiagnosticInfo;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0006F914 File Offset: 0x0006DB14
		private void LogDiagnosticInfo()
		{
			if (this.logDiagnosticInfo)
			{
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_TenantDagQuotaDiagnosticInfo, null, new object[]
				{
					this.GetDiagnosticInfo()
				});
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0006F94C File Offset: 0x0006DB4C
		private XElement GetTenantDagQuotaDiagnosticInfo(out XElement tenantsElement)
		{
			XElement xelement = new XElement("TenantDagQuota");
			xelement.SetAttributeValue("DefaultDagsPerTenant", this.defaultDagsPerTenant);
			xelement.SetAttributeValue("MessagesPerDag", this.messagesPerDag);
			xelement.SetAttributeValue("WeightForHistory", this.historyWeight);
			tenantsElement = new XElement("TenantData");
			xelement.Add(tenantsElement);
			return xelement;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0006F9D4 File Offset: 0x0006DBD4
		private XElement GetTenantDiagnosticInfo(string tenantIdString)
		{
			Guid guid;
			if (!Guid.TryParse(tenantIdString, out guid))
			{
				return new XElement("Error", string.Format("Invalid tenant id {0} passed as argument. A Guid is expected.", tenantIdString));
			}
			TenantDagQuota.TenantData tenantData;
			if (this.tenantDataDictionary.TryGetValue(guid, out tenantData))
			{
				XElement xelement;
				XElement tenantDagQuotaDiagnosticInfo = this.GetTenantDagQuotaDiagnosticInfo(out xelement);
				xelement.Add(TenantDagQuota.GetTenantDiagnosticInfo(guid, tenantData));
				return tenantDagQuotaDiagnosticInfo;
			}
			return new XElement("Error", string.Format("Tenant with id {0} not present in TenantDagQuota.", guid));
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006FA50 File Offset: 0x0006DC50
		private int ComputeDagCount(TenantDagQuota.TenantData tenantData)
		{
			double num = (double)tenantData.DagsPerTenant * this.historyWeight;
			double num2 = (double)tenantData.MessageCount * (1.0 - this.historyWeight) / (double)this.messagesPerDag;
			return Math.Max(1, (int)Math.Floor(num + num2));
		}

		// Token: 0x04000CD3 RID: 3283
		private readonly int defaultDagsPerTenant;

		// Token: 0x04000CD4 RID: 3284
		private readonly int messagesPerDag;

		// Token: 0x04000CD5 RID: 3285
		private readonly double historyWeight;

		// Token: 0x04000CD6 RID: 3286
		private readonly bool logDiagnosticInfo;

		// Token: 0x04000CD7 RID: 3287
		private ConcurrentDictionary<Guid, TenantDagQuota.TenantData> tenantDataDictionary;

		// Token: 0x0200026E RID: 622
		private class TenantData
		{
			// Token: 0x06001B31 RID: 6961 RVA: 0x0006FA9C File Offset: 0x0006DC9C
			public TenantData(int dagsPerTenant, int messageCount)
			{
				this.DagsPerTenant = dagsPerTenant;
				this.messageCount = messageCount;
			}

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0006FAB2 File Offset: 0x0006DCB2
			// (set) Token: 0x06001B33 RID: 6963 RVA: 0x0006FABA File Offset: 0x0006DCBA
			public int DagsPerTenant { get; set; }

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0006FAC3 File Offset: 0x0006DCC3
			public int MessageCount
			{
				get
				{
					return this.messageCount;
				}
			}

			// Token: 0x06001B35 RID: 6965 RVA: 0x0006FACB File Offset: 0x0006DCCB
			public void IncrementMessageCount()
			{
				Interlocked.Increment(ref this.messageCount);
			}

			// Token: 0x06001B36 RID: 6966 RVA: 0x0006FAD9 File Offset: 0x0006DCD9
			public void ResetMessageCount()
			{
				Interlocked.Exchange(ref this.messageCount, 0);
			}

			// Token: 0x04000CD8 RID: 3288
			private int messageCount;
		}
	}
}
