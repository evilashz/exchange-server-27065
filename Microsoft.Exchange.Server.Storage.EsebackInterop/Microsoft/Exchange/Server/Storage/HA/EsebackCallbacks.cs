using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200000E RID: 14
	internal static class EsebackCallbacks
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00001178 File Offset: 0x00000578
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000118C File Offset: 0x0000058C
		public static IEsebackCallbacks ManagedCallbacks
		{
			get
			{
				return EsebackCallbacks.<backing_store>ManagedCallbacks;
			}
			set
			{
				EsebackCallbacks.<backing_store>ManagedCallbacks = value;
			}
		}

		// Token: 0x04000095 RID: 149
		private static IEsebackCallbacks <backing_store>ManagedCallbacks;
	}
}
