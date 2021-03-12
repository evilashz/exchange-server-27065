using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001B5 RID: 437
	internal sealed class ServerLocation : NotificationLocation
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x0003C6E8 File Offset: 0x0003A8E8
		public ServerLocation(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentException("Server address cannot be null or empty string.", "address");
			}
			this.address = address;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003C70F File Offset: 0x0003A90F
		public override KeyValuePair<string, object> GetEventData()
		{
			return new KeyValuePair<string, object>("ServerAddress", this.address);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003C721 File Offset: 0x0003A921
		public override int GetHashCode()
		{
			return ServerLocation.TypeHashCode ^ this.address.GetHashCode();
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003C734 File Offset: 0x0003A934
		public override bool Equals(object obj)
		{
			ServerLocation serverLocation = obj as ServerLocation;
			return serverLocation != null && this.address.Equals(serverLocation.address);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003C75E File Offset: 0x0003A95E
		public override string ToString()
		{
			return this.address;
		}

		// Token: 0x04000950 RID: 2384
		private const string EventKey = "ServerAddress";

		// Token: 0x04000951 RID: 2385
		private static readonly int TypeHashCode = typeof(ServerLocation).GetHashCode();

		// Token: 0x04000952 RID: 2386
		private readonly string address;
	}
}
