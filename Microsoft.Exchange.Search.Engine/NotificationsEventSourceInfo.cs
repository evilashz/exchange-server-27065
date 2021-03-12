using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000007 RID: 7
	internal class NotificationsEventSourceInfo
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public NotificationsEventSourceInfo(IWatermarkStorage watermarkStorage, INotificationsEventSource eventSource, IDiagnosticsSession diagnosticsSession, MdbInfo mdbInfo)
		{
			this.FirstEvent = eventSource.ReadFirstEventCounter();
			this.NotificationsWatermark = watermarkStorage.GetNotificationsWatermark();
			long num = Math.Max(0L, this.NotificationsWatermark);
			this.LastEvent = eventSource.ReadLastEvent().EventCounter;
			this.WatermarkDelta = this.LastEvent - num;
			long num2;
			MapiEvent[] array = eventSource.ReadEvents(num, 1, ReadEventsFlags.IncludeMoveDestinationEvents, out num2);
			DateTime d = (array.Length > 0) ? array[0].CreateTime : DateTime.UtcNow;
			this.CurrentEventAge = ((this.WatermarkDelta > 0L) ? TimeSpan.FromSeconds((double)Math.Max(0, (int)(DateTime.UtcNow - d).TotalSeconds)) : TimeSpan.Zero);
			if (array.Length == 0)
			{
				diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Watermark events not found for Mdb [{0}] - Setting current event time to now", new object[]
				{
					mdbInfo
				});
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002EA3 File Offset: 0x000010A3
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002EAB File Offset: 0x000010AB
		public long FirstEvent { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002EB4 File Offset: 0x000010B4
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002EBC File Offset: 0x000010BC
		public long LastEvent { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002EC5 File Offset: 0x000010C5
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002ECD File Offset: 0x000010CD
		public long WatermarkDelta { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002ED6 File Offset: 0x000010D6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002EDE File Offset: 0x000010DE
		public TimeSpan CurrentEventAge { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002EE7 File Offset: 0x000010E7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002EEF File Offset: 0x000010EF
		public long NotificationsWatermark { get; private set; }
	}
}
