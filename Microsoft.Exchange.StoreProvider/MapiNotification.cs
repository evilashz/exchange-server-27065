using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200007F RID: 127
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiNotification
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000EDFA File Offset: 0x0000CFFA
		public static void AbandonNotificationsDuringShutdown(bool abandon)
		{
			NativeMethods.AbandonNotificationsDuringShutdown(abandon);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000EE02 File Offset: 0x0000D002
		public AdviseFlags NotificationType
		{
			get
			{
				return this.notificationType;
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000EE0A File Offset: 0x0000D00A
		internal unsafe MapiNotification(NOTIFICATION* notification)
		{
			this.notificationType = (AdviseFlags)notification->ulEventType;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000EE20 File Offset: 0x0000D020
		internal unsafe static MapiNotification Create(NOTIFICATION* notification)
		{
			AdviseFlags ulEventType = (AdviseFlags)notification->ulEventType;
			if (ulEventType <= AdviseFlags.ObjectMoved)
			{
				if (ulEventType <= AdviseFlags.ObjectCreated)
				{
					if (ulEventType == AdviseFlags.Extended)
					{
						return new MapiExtendedNotification(notification);
					}
					switch (ulEventType)
					{
					case AdviseFlags.CriticalError:
						return new MapiErrorNotification(notification);
					case AdviseFlags.NewMail:
						return new MapiNewMailNotification(notification);
					case AdviseFlags.ObjectCreated:
						return new MapiObjectNotification(notification);
					}
				}
				else
				{
					if (ulEventType == AdviseFlags.ObjectDeleted)
					{
						return new MapiObjectNotification(notification);
					}
					if (ulEventType == AdviseFlags.ObjectModified)
					{
						return new MapiObjectNotification(notification);
					}
					if (ulEventType == AdviseFlags.ObjectMoved)
					{
						return new MapiObjectNotification(notification);
					}
				}
			}
			else if (ulEventType <= AdviseFlags.SearchComplete)
			{
				if (ulEventType == AdviseFlags.ObjectCopied)
				{
					return new MapiObjectNotification(notification);
				}
				if (ulEventType == AdviseFlags.SearchComplete)
				{
					return new MapiNotification(notification);
				}
			}
			else
			{
				if (ulEventType == AdviseFlags.TableModified)
				{
					return new MapiTableNotification(notification);
				}
				if (ulEventType == AdviseFlags.StatusObjectModified)
				{
					return new MapiStatusObjectNotification(notification);
				}
				if (ulEventType == AdviseFlags.ConnectionDropped)
				{
					return new MapiConnectionDroppedNotification(notification);
				}
			}
			return new MapiNotification(notification);
		}

		// Token: 0x040004DC RID: 1244
		private readonly AdviseFlags notificationType;
	}
}
