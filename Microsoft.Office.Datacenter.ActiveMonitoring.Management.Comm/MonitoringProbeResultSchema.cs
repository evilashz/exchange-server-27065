using System;
using Microsoft.Exchange.Data;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x0200000E RID: 14
	internal class MonitoringProbeResultSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000045 RID: 69
		public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000046 RID: 70
		public static readonly SimpleProviderPropertyDefinition MonitorIdentity = new SimpleProviderPropertyDefinition("MonitorIdentity", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000047 RID: 71
		public static readonly SimpleProviderPropertyDefinition RequestId = new SimpleProviderPropertyDefinition("RequestId", ExchangeObjectVersion.Exchange2010, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000048 RID: 72
		public static readonly SimpleProviderPropertyDefinition ExecutionStartTime = new SimpleProviderPropertyDefinition("PropertyName", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000049 RID: 73
		public static readonly SimpleProviderPropertyDefinition ExecutionEndTime = new SimpleProviderPropertyDefinition("PropertyName", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400004A RID: 74
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400004B RID: 75
		public static readonly SimpleProviderPropertyDefinition Exception = new SimpleProviderPropertyDefinition("Exception", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400004C RID: 76
		public static readonly SimpleProviderPropertyDefinition PoisonedCount = new SimpleProviderPropertyDefinition("PoisonedCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400004D RID: 77
		public static readonly SimpleProviderPropertyDefinition ExecutionId = new SimpleProviderPropertyDefinition("ExecutionId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400004E RID: 78
		public static readonly SimpleProviderPropertyDefinition SampleValue = new SimpleProviderPropertyDefinition("SampleValue", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400004F RID: 79
		public static readonly SimpleProviderPropertyDefinition ExecutionContext = new SimpleProviderPropertyDefinition("PropertyName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000050 RID: 80
		public static readonly SimpleProviderPropertyDefinition FailureContext = new SimpleProviderPropertyDefinition("FailureContext", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000051 RID: 81
		public static readonly SimpleProviderPropertyDefinition ExtensionXml = new SimpleProviderPropertyDefinition("ExtensionXml", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000052 RID: 82
		public static readonly SimpleProviderPropertyDefinition ResultType = new SimpleProviderPropertyDefinition("ResultType", ExchangeObjectVersion.Exchange2010, typeof(ResultType), PropertyDefinitionFlags.None, Microsoft.Office.Datacenter.WorkerTaskFramework.ResultType.Failed, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000053 RID: 83
		public static readonly SimpleProviderPropertyDefinition RetryCount = new SimpleProviderPropertyDefinition("RetryCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000054 RID: 84
		public static readonly SimpleProviderPropertyDefinition ResultName = new SimpleProviderPropertyDefinition("ResultName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000055 RID: 85
		public static readonly SimpleProviderPropertyDefinition IsNotified = new SimpleProviderPropertyDefinition("IsNotified", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000056 RID: 86
		public static readonly SimpleProviderPropertyDefinition ResultId = new SimpleProviderPropertyDefinition("ResultId", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000057 RID: 87
		public static readonly SimpleProviderPropertyDefinition ServiceName = new SimpleProviderPropertyDefinition("ServiceName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000058 RID: 88
		public static readonly SimpleProviderPropertyDefinition StateAttribute1 = new SimpleProviderPropertyDefinition("StateAttribute1", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000059 RID: 89
		public static readonly SimpleProviderPropertyDefinition StateAttribute2 = new SimpleProviderPropertyDefinition("StateAttribute2", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400005A RID: 90
		public static readonly SimpleProviderPropertyDefinition StateAttribute3 = new SimpleProviderPropertyDefinition("StateAttribute3", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400005B RID: 91
		public static readonly SimpleProviderPropertyDefinition StateAttribute4 = new SimpleProviderPropertyDefinition("StateAttribute4", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400005C RID: 92
		public static readonly SimpleProviderPropertyDefinition StateAttribute5 = new SimpleProviderPropertyDefinition("StateAttribute5", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400005D RID: 93
		public static readonly SimpleProviderPropertyDefinition StateAttribute6 = new SimpleProviderPropertyDefinition("StateAttribute6", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400005E RID: 94
		public static readonly SimpleProviderPropertyDefinition StateAttribute7 = new SimpleProviderPropertyDefinition("StateAttribute7", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400005F RID: 95
		public static readonly SimpleProviderPropertyDefinition StateAttribute8 = new SimpleProviderPropertyDefinition("StateAttribute8", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000060 RID: 96
		public static readonly SimpleProviderPropertyDefinition StateAttribute9 = new SimpleProviderPropertyDefinition("StateAttribute9", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000061 RID: 97
		public static readonly SimpleProviderPropertyDefinition StateAttribute10 = new SimpleProviderPropertyDefinition("StateAttribute10", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000062 RID: 98
		public static readonly SimpleProviderPropertyDefinition StateAttribute11 = new SimpleProviderPropertyDefinition("StateAttribute11", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000063 RID: 99
		public static readonly SimpleProviderPropertyDefinition StateAttribute12 = new SimpleProviderPropertyDefinition("StateAttribute12", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000064 RID: 100
		public static readonly SimpleProviderPropertyDefinition StateAttribute13 = new SimpleProviderPropertyDefinition("StateAttribute13", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000065 RID: 101
		public static readonly SimpleProviderPropertyDefinition StateAttribute14 = new SimpleProviderPropertyDefinition("StateAttribute14", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000066 RID: 102
		public static readonly SimpleProviderPropertyDefinition StateAttribute15 = new SimpleProviderPropertyDefinition("StateAttribute15", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000067 RID: 103
		public static readonly SimpleProviderPropertyDefinition StateAttribute16 = new SimpleProviderPropertyDefinition("StateAttribute16", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000068 RID: 104
		public static readonly SimpleProviderPropertyDefinition StateAttribute17 = new SimpleProviderPropertyDefinition("StateAttribute17", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000069 RID: 105
		public static readonly SimpleProviderPropertyDefinition StateAttribute18 = new SimpleProviderPropertyDefinition("StateAttribute18", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006A RID: 106
		public static readonly SimpleProviderPropertyDefinition StateAttribute19 = new SimpleProviderPropertyDefinition("StateAttribute19", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006B RID: 107
		public static readonly SimpleProviderPropertyDefinition StateAttribute20 = new SimpleProviderPropertyDefinition("StateAttribute20", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006C RID: 108
		public static readonly SimpleProviderPropertyDefinition StateAttribute21 = new SimpleProviderPropertyDefinition("StateAttribute21", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006D RID: 109
		public static readonly SimpleProviderPropertyDefinition StateAttribute22 = new SimpleProviderPropertyDefinition("StateAttribute22", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006E RID: 110
		public static readonly SimpleProviderPropertyDefinition StateAttribute23 = new SimpleProviderPropertyDefinition("StateAttribute23", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400006F RID: 111
		public static readonly SimpleProviderPropertyDefinition StateAttribute24 = new SimpleProviderPropertyDefinition("StateAttribute24", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000070 RID: 112
		public static readonly SimpleProviderPropertyDefinition StateAttribute25 = new SimpleProviderPropertyDefinition("StateAttribute25", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
