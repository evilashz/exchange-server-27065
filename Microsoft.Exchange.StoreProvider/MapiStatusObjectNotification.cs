using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001E2 RID: 482
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiStatusObjectNotification : MapiNotification
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001BCC9 File Offset: 0x00019EC9
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001BCD1 File Offset: 0x00019ED1
		public PropValue[] PropValues
		{
			get
			{
				return this.propValues;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001BCDC File Offset: 0x00019EDC
		internal unsafe MapiStatusObjectNotification(NOTIFICATION* notification) : base(notification)
		{
			if (notification->info.statobj.cbEntryID > 0)
			{
				this.entryId = new byte[notification->info.statobj.cbEntryID];
				Marshal.Copy(notification->info.statobj.lpEntryID, this.entryId, 0, this.entryId.Length);
			}
			this.propValues = new PropValue[notification->info.statobj.cValues];
			for (int i = 0; i < this.propValues.Length; i++)
			{
				this.propValues[i] = new PropValue(notification->info.statobj.lpPropVals + i);
			}
		}

		// Token: 0x0400067C RID: 1660
		private readonly byte[] entryId;

		// Token: 0x0400067D RID: 1661
		private readonly PropValue[] propValues;
	}
}
