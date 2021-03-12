using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000003 RID: 3
	internal class ResourceObserverOperation
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002458 File Offset: 0x00000658
		public ResourceObserverOperation(IResourceLevelObserver resourceLevelObserver, IExecutionInfo executionInfo, int maxTransientExceptions = 5)
		{
			ArgumentValidator.ThrowIfNull("resourceLevelObserver", resourceLevelObserver);
			ArgumentValidator.ThrowIfNull("executionInfo", executionInfo);
			ArgumentValidator.ThrowIfZeroOrNegative("maxTransientExceptions", maxTransientExceptions);
			this.resourceLevelObserver = resourceLevelObserver;
			Action action = delegate()
			{
				resourceLevelObserver.HandleResourceChange(ResourceObserverOperation.allResourceUses, ResourceObserverOperation.changedResourceUses, ResourceObserverOperation.rawResourceUses);
			};
			this.operation = new Operation("HandleResourceChange method for " + resourceLevelObserver.Name, action, executionInfo, maxTransientExceptions);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000024E1 File Offset: 0x000006E1
		public IResourceLevelObserver ResourceLevelObserver
		{
			get
			{
				return this.resourceLevelObserver;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000024E9 File Offset: 0x000006E9
		public IExecutionInfo ExecutionInfo
		{
			get
			{
				return this.operation.ExecutionInfo;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002500 File Offset: 0x00000700
		public static Task InvokeResourceObserverOperations(IEnumerable<ResourceObserverOperation> resourceObserverOperations, IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses, TimeSpan timeout)
		{
			ResourceObserverOperation.allResourceUses = allResourceUses;
			ResourceObserverOperation.changedResourceUses = changedResourceUses;
			ResourceObserverOperation.rawResourceUses = rawResourceUses;
			IEnumerable<Operation> operations = from resourceObserverOperation in resourceObserverOperations
			select resourceObserverOperation.operation;
			return Operation.InvokeOperationsAsync(operations, timeout);
		}

		// Token: 0x04000007 RID: 7
		private const string DebugInfoPrefix = "HandleResourceChange method for ";

		// Token: 0x04000008 RID: 8
		private static IEnumerable<ResourceUse> allResourceUses;

		// Token: 0x04000009 RID: 9
		private static IEnumerable<ResourceUse> changedResourceUses;

		// Token: 0x0400000A RID: 10
		private static IEnumerable<ResourceUse> rawResourceUses;

		// Token: 0x0400000B RID: 11
		private readonly Operation operation;

		// Token: 0x0400000C RID: 12
		private readonly IResourceLevelObserver resourceLevelObserver;
	}
}
