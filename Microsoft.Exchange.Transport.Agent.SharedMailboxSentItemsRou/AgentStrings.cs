using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent
{
	// Token: 0x0200000C RID: 12
	internal static class AgentStrings
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002990 File Offset: 0x00000B90
		static AgentStrings()
		{
			AgentStrings.stringIDs.Add(2339319564U, "WrapperMessageBody");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static LocalizedString WrapperMessageSubjectFormat(string originalSubject)
		{
			return new LocalizedString("WrapperMessageSubjectFormat", AgentStrings.ResourceManager, new object[]
			{
				originalSubject
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A08 File Offset: 0x00000C08
		public static LocalizedString WrapperMessageBody
		{
			get
			{
				return new LocalizedString("WrapperMessageBody", AgentStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A1F File Offset: 0x00000C1F
		public static LocalizedString GetLocalizedString(AgentStrings.IDs key)
		{
			return new LocalizedString(AgentStrings.stringIDs[(uint)key], AgentStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000014 RID: 20
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000015 RID: 21
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Agent.SharedMailboxSentItemsRoutingAgent.AgentStrings", typeof(AgentStrings).GetTypeInfo().Assembly);

		// Token: 0x0200000D RID: 13
		public enum IDs : uint
		{
			// Token: 0x04000017 RID: 23
			WrapperMessageBody = 2339319564U
		}

		// Token: 0x0200000E RID: 14
		private enum ParamIDs
		{
			// Token: 0x04000019 RID: 25
			WrapperMessageSubjectFormat
		}
	}
}
