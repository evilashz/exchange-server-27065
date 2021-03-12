using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001B5 RID: 437
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiExtendedNotification : MapiNotification
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x000147F0 File Offset: 0x000129F0
		public int EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000147F8 File Offset: 0x000129F8
		public byte[] EventParameters
		{
			get
			{
				return this.eventParameters;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00014800 File Offset: 0x00012A00
		internal unsafe MapiExtendedNotification(NOTIFICATION* notification) : base(notification)
		{
			this.eventType = notification->info.ext.ulEvent;
			this.eventParameters = new byte[notification->info.ext.cb];
			if (this.eventParameters.Length > 0)
			{
				Marshal.Copy(notification->info.ext.pbEventParameters, this.eventParameters, 0, this.eventParameters.Length);
			}
		}

		// Token: 0x040005A1 RID: 1441
		private readonly int eventType;

		// Token: 0x040005A2 RID: 1442
		private readonly byte[] eventParameters;
	}
}
