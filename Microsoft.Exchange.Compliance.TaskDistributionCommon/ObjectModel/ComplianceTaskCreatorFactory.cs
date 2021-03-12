using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000030 RID: 48
	internal static class ComplianceTaskCreatorFactory
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00007A08 File Offset: 0x00005C08
		public static IComplianceTaskCreator GetInstance(ComplianceBindingType bindingType)
		{
			if (bindingType == ComplianceBindingType.ExchangeBinding)
			{
				return ComplianceTaskCreatorFactory.exchangeBindingTaskCreatorInstance.Value;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007A2C File Offset: 0x00005C2C
		public static IDisposable SetTestHook(ComplianceBindingType bindingType, IComplianceTaskCreator testHook)
		{
			if (bindingType == ComplianceBindingType.ExchangeBinding)
			{
				return ComplianceTaskCreatorFactory.exchangeBindingTaskCreatorInstance.SetTestHook(testHook);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007A50 File Offset: 0x00005C50
		private static IComplianceTaskCreator CreateInstance(string assemblyName, string typeName, params object[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName);
			Type type = assembly.GetType(typeName);
			return (IComplianceTaskCreator)Activator.CreateInstance(type, args);
		}

		// Token: 0x040000D5 RID: 213
		private const int MaxUserCountForExchangeBindingTask = 1000;

		// Token: 0x040000D6 RID: 214
		private static readonly Hookable<IComplianceTaskCreator> exchangeBindingTaskCreatorInstance = Hookable<IComplianceTaskCreator>.Create(true, ComplianceTaskCreatorFactory.CreateInstance("Microsoft.Exchange.Compliance.TaskCreator", "Microsoft.Exchange.Compliance.TaskCreator.ExchangeBindingTaskCreator", new object[]
		{
			1000
		}));
	}
}
