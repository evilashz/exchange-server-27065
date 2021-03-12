using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000002 RID: 2
	public sealed class ClassificationConfiguration
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ClassificationConfiguration()
		{
			this.properties = new Dictionary<string, object>();
			this.propertyBag = new ClassificationConfiguration.ReadOnlyClassificationPropertyBag(this.properties);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002106 File Offset: 0x00000306
		public int? MaxRulePackageCacheCount
		{
			get
			{
				return (int?)this.RetrievePropertyHelper("MaxRulePackageCacheCount");
			}
			set
			{
				this.SetPropertyHelper("MaxRulePackageCacheCount", value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000212B File Offset: 0x0000032B
		public int? MaxRulePackageCacheStorage
		{
			get
			{
				return (int?)this.RetrievePropertyHelper("MaxRulePackageCacheStorage");
			}
			set
			{
				this.SetPropertyHelper("MaxRulePackageCacheStorage", value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000213E File Offset: 0x0000033E
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public int? PerformanceMonitorPollPeriod
		{
			get
			{
				return (int?)this.RetrievePropertyHelper("PerformanceMonitorPollPeriod");
			}
			set
			{
				this.SetPropertyHelper("PerformanceMonitorPollPeriod", value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002163 File Offset: 0x00000363
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002175 File Offset: 0x00000375
		public int? RulePackageCachePollPeriod
		{
			get
			{
				return (int?)this.RetrievePropertyHelper("RulePackageCachePollPeriod");
			}
			set
			{
				this.SetPropertyHelper("RulePackageCachePollPeriod", value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000219A File Offset: 0x0000039A
		public int? RulePackageCacheRetrievalWaitTime
		{
			get
			{
				return (int?)this.RetrievePropertyHelper("RulePackageCacheRetrievalWaitTime");
			}
			set
			{
				this.SetPropertyHelper("RulePackageCacheRetrievalWaitTime", value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021AD File Offset: 0x000003AD
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021BF File Offset: 0x000003BF
		public string TraceSessionId
		{
			get
			{
				return (string)this.RetrievePropertyHelper("TraceSessionId");
			}
			set
			{
				this.SetPropertyHelper("TraceSessionId", value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021CD File Offset: 0x000003CD
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021DF File Offset: 0x000003DF
		public int? TraceSessionLevel
		{
			get
			{
				return (int?)this.RetrievePropertyHelper("TraceSessionLevel");
			}
			set
			{
				this.SetPropertyHelper("TraceSessionLevel", value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F2 File Offset: 0x000003F2
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002204 File Offset: 0x00000404
		public bool? UseLazyRegexCompilation
		{
			get
			{
				return (bool?)this.RetrievePropertyHelper("UseLazyRegexCompilation");
			}
			set
			{
				this.SetPropertyHelper("UseLazyRegexCompilation", value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002217 File Offset: 0x00000417
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002229 File Offset: 0x00000429
		public bool? UseMemoryToImprovePerformance
		{
			get
			{
				return (bool?)this.RetrievePropertyHelper("UseMemoryToImprovePerformance");
			}
			set
			{
				this.SetPropertyHelper("UseMemoryToImprovePerformance", value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000223C File Offset: 0x0000043C
		internal IPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002244 File Offset: 0x00000444
		private object RetrievePropertyHelper(string propertyName)
		{
			if (this.properties.ContainsKey(propertyName))
			{
				return this.properties[propertyName];
			}
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002262 File Offset: 0x00000462
		private void SetPropertyHelper(string propertyName, object value)
		{
			if (value == null && this.properties.ContainsKey(propertyName))
			{
				this.properties.Remove(propertyName);
				return;
			}
			this.properties[propertyName] = value;
		}

		// Token: 0x04000001 RID: 1
		private Dictionary<string, object> properties;

		// Token: 0x04000002 RID: 2
		private IPropertyBag propertyBag;

		// Token: 0x02000004 RID: 4
		private class ReadOnlyClassificationPropertyBag : IPropertyBag
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00002290 File Offset: 0x00000490
			internal ReadOnlyClassificationPropertyBag(Dictionary<string, object> properties)
			{
				if (properties == null)
				{
					throw new ArgumentNullException("properties");
				}
				this.properties = properties;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x000022AD File Offset: 0x000004AD
			public int Read(string propertyName, ref object propertyValue, IErrorLog errorLog)
			{
				if (this.properties.ContainsKey(propertyName))
				{
					propertyValue = this.properties[propertyName];
					return 0;
				}
				return -2147467259;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x000022D2 File Offset: 0x000004D2
			public int Write(string propertyName, ref object propertyValue)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000003 RID: 3
			private const int ComOK = 0;

			// Token: 0x04000004 RID: 4
			private const int ComFAIL = -2147467259;

			// Token: 0x04000005 RID: 5
			private Dictionary<string, object> properties;
		}
	}
}
