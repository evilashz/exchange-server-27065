using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications.Server.LocStrings
{
	// Token: 0x0200002F RID: 47
	internal static class Strings
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00004980 File Offset: 0x00002B80
		public static LocalizedString FailedToAcquireBudget(string windowsIdentity, string principal)
		{
			return new LocalizedString("FailedToAcquireBudget", Strings.ResourceManager, new object[]
			{
				windowsIdentity,
				principal
			});
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000049AC File Offset: 0x00002BAC
		public static LocalizedString OperationCancelled(string command)
		{
			return new LocalizedString("OperationCancelled", Strings.ResourceManager, new object[]
			{
				command
			});
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000049D4 File Offset: 0x00002BD4
		public static LocalizedString ServiceCommandTransientExceptionMessage(string message)
		{
			return new LocalizedString("ServiceCommandTransientExceptionMessage", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000049FC File Offset: 0x00002BFC
		public static LocalizedString ServiceBusy(string command)
		{
			return new LocalizedString("ServiceBusy", Strings.ResourceManager, new object[]
			{
				command
			});
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004A24 File Offset: 0x00002C24
		public static LocalizedString ServiceCommandExceptionMessage(string message)
		{
			return new LocalizedString("ServiceCommandExceptionMessage", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x04000064 RID: 100
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.PushNotifications.Server.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000030 RID: 48
		private enum ParamIDs
		{
			// Token: 0x04000066 RID: 102
			FailedToAcquireBudget,
			// Token: 0x04000067 RID: 103
			OperationCancelled,
			// Token: 0x04000068 RID: 104
			ServiceCommandTransientExceptionMessage,
			// Token: 0x04000069 RID: 105
			ServiceBusy,
			// Token: 0x0400006A RID: 106
			ServiceCommandExceptionMessage
		}
	}
}
