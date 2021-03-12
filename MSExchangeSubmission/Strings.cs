using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x0200000F RID: 15
	internal static class Strings
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00004E3C File Offset: 0x0000303C
		static Strings()
		{
			Strings.stringIDs.Add(3130811674U, "MailboxTransportSubmissionAssistantName");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004E8C File Offset: 0x0000308C
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004EB4 File Offset: 0x000030B4
		public static LocalizedString MailboxTransportSubmissionAssistantName
		{
			get
			{
				return new LocalizedString("MailboxTransportSubmissionAssistantName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004ECB File Offset: 0x000030CB
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000074 RID: 116
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000075 RID: 117
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxTransport.Submission.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000010 RID: 16
		public enum IDs : uint
		{
			// Token: 0x04000077 RID: 119
			MailboxTransportSubmissionAssistantName = 3130811674U
		}

		// Token: 0x02000011 RID: 17
		private enum ParamIDs
		{
			// Token: 0x04000079 RID: 121
			UsageText
		}
	}
}
