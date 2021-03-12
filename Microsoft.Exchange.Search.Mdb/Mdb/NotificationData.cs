using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000025 RID: 37
	internal sealed class NotificationData : IEquatable<NotificationData>
	{
		// Token: 0x060000FD RID: 253 RVA: 0x000079BE File Offset: 0x00005BBE
		internal NotificationData(MapiEvent mapiEvent)
		{
			this.MapiEvent = mapiEvent;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000079D8 File Offset: 0x00005BD8
		internal NotificationData(MapiEvent mapiEvent, bool interesting, DocumentOperation operation, MdbItemIdentity identity)
		{
			this.MapiEvent = mapiEvent;
			this.Type = (interesting ? NotificationType.Insert : NotificationType.Uninteresting);
			this.Operation = operation;
			this.Identity = identity;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007A0E File Offset: 0x00005C0E
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00007A16 File Offset: 0x00005C16
		internal MapiEvent MapiEvent { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00007A1F File Offset: 0x00005C1F
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007A27 File Offset: 0x00005C27
		internal DocumentOperation Operation { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00007A30 File Offset: 0x00005C30
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00007A38 File Offset: 0x00005C38
		internal MdbItemIdentity Identity { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007A41 File Offset: 0x00005C41
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00007A49 File Offset: 0x00005C49
		internal NotificationType Type { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00007A52 File Offset: 0x00005C52
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00007A5A File Offset: 0x00005C5A
		internal List<NotificationData> MergedEvents { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007A63 File Offset: 0x00005C63
		internal bool IsMoveDestination
		{
			get
			{
				return (this.MapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.MoveDestination) != MapiExtendedEventFlags.None;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007A7C File Offset: 0x00005C7C
		public override string ToString()
		{
			return string.Format("Notification (EventId={0}, Type={1}, Operation={2}, DocumentId={3}, Identity={4})", new object[]
			{
				this.MapiEvent.EventCounter,
				this.Type,
				this.Operation,
				this.MapiEvent.DocumentId,
				this.Identity
			});
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public string ToMergeDebuggingString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Notification (MapiEvent={0}, Type={1}, Operation={2}, Identity={3}, Merged Events: [", new object[]
			{
				this.MapiEvent,
				this.Type,
				this.Operation,
				this.Identity
			});
			lock (this.lockObject)
			{
				if (this.MergedEvents != null && this.MergedEvents.Count > 0)
				{
					using (List<NotificationData>.Enumerator enumerator = this.MergedEvents.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NotificationData notificationData = enumerator.Current;
							stringBuilder.AppendFormat("{0} ", notificationData.ToString());
						}
						goto IL_BE;
					}
				}
				stringBuilder.Append("None");
				IL_BE:;
			}
			stringBuilder.Append("])");
			return stringBuilder.ToString();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public bool Equals(NotificationData other)
		{
			return other != null && this.Identity.Equals(other.Identity);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007C08 File Offset: 0x00005E08
		public override int GetHashCode()
		{
			return this.Identity.GetHashCode();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007C18 File Offset: 0x00005E18
		public void TrackMergeWith(NotificationData other)
		{
			if (this.MergedEvents == null)
			{
				if (other.MergedEvents != null)
				{
					this.MergedEvents = new List<NotificationData>(other.MergedEvents.Capacity + 1);
					this.MergedEvents.AddRange(other.MergedEvents);
				}
				else
				{
					this.MergedEvents = new List<NotificationData>(1);
				}
			}
			this.MergedEvents.Add(other);
		}

		// Token: 0x040000B1 RID: 177
		private readonly object lockObject = new object();
	}
}
