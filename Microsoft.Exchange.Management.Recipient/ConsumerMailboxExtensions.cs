using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200000F RID: 15
	internal static class ConsumerMailboxExtensions
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000538E File Offset: 0x0000358E
		public static PrimaryMailboxSourceType PrimaryMailboxSource(this ADUser user)
		{
			if (user[ADUserSchema.PrimaryMailboxSource] == null)
			{
				return PrimaryMailboxSourceType.None;
			}
			return (PrimaryMailboxSourceType)user[ADUserSchema.PrimaryMailboxSource];
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000053AF File Offset: 0x000035AF
		public static string SatchmoDGroup(this ADUser user)
		{
			if (user[ADUserSchema.SatchmoDGroup] == null)
			{
				return null;
			}
			return (string)user[ADUserSchema.SatchmoDGroup];
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000053D0 File Offset: 0x000035D0
		public static IPAddress SatchmoClusterIp(this ADUser user)
		{
			if (user[ADUserSchema.SatchmoClusterIp] == null)
			{
				return null;
			}
			return (IPAddress)user[ADUserSchema.SatchmoClusterIp];
		}
	}
}
