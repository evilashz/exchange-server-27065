using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x0200002A RID: 42
	internal static class InterceptorAgentStrings
	{
		// Token: 0x06000172 RID: 370 RVA: 0x000090AC File Offset: 0x000072AC
		static InterceptorAgentStrings()
		{
			InterceptorAgentStrings.stringIDs.Add(658946777U, "ConditionTypeValueCannotBeNullOrEmpty");
			InterceptorAgentStrings.stringIDs.Add(2490668003U, "ConditionTypeValueInvalidTenantIdGuid");
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009110 File Offset: 0x00007310
		public static LocalizedString ConditionTypeValueInvalidDirectionalityType(string values)
		{
			return new LocalizedString("ConditionTypeValueInvalidDirectionalityType", InterceptorAgentStrings.ResourceManager, new object[]
			{
				values
			});
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00009138 File Offset: 0x00007338
		public static LocalizedString ConditionTypeValueCannotBeNullOrEmpty
		{
			get
			{
				return new LocalizedString("ConditionTypeValueCannotBeNullOrEmpty", InterceptorAgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000914F File Offset: 0x0000734F
		public static LocalizedString ConditionTypeValueInvalidTenantIdGuid
		{
			get
			{
				return new LocalizedString("ConditionTypeValueInvalidTenantIdGuid", InterceptorAgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00009166 File Offset: 0x00007366
		public static LocalizedString GetLocalizedString(InterceptorAgentStrings.IDs key)
		{
			return new LocalizedString(InterceptorAgentStrings.stringIDs[(uint)key], InterceptorAgentStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000112 RID: 274
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x04000113 RID: 275
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Agent.InterceptorAgent.InterceptorAgentStrings", typeof(InterceptorAgentStrings).GetTypeInfo().Assembly);

		// Token: 0x0200002B RID: 43
		public enum IDs : uint
		{
			// Token: 0x04000115 RID: 277
			ConditionTypeValueCannotBeNullOrEmpty = 658946777U,
			// Token: 0x04000116 RID: 278
			ConditionTypeValueInvalidTenantIdGuid = 2490668003U
		}

		// Token: 0x0200002C RID: 44
		private enum ParamIDs
		{
			// Token: 0x04000118 RID: 280
			ConditionTypeValueInvalidDirectionalityType
		}
	}
}
