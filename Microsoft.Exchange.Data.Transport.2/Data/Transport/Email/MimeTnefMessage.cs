using System;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000ED RID: 237
	internal class MimeTnefMessage : MessageImplementation, IBody
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x0000E44C File Offset: 0x0000C64C
		internal MimeTnefMessage(BodyFormat bodyFormat, bool createAlternative, string charsetName)
		{
			this.mimeMessage = new PureMimeMessage(bodyFormat, createAlternative, charsetName);
			this.accessToken = new MimeTnefMessage.MimeTnefMessageThreadAccessToken(this);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000E47C File Offset: 0x0000C67C
		internal MimeTnefMessage(MimeDocument mimeDocument)
		{
			this.mimeMessage = new PureMimeMessage(mimeDocument);
			bool hasTnef = this.HasTnef;
			if (mimeDocument.IsReadOnly)
			{
				this.Synchronize();
			}
			this.accessToken = new MimeTnefMessage.MimeTnefMessageThreadAccessToken(this);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000E4CA File Offset: 0x0000C6CA
		internal MimeTnefMessage(MimePart rootPart)
		{
			this.mimeMessage = new PureMimeMessage(rootPart);
			bool hasTnef = this.HasTnef;
			this.Synchronize();
			this.accessToken = new MimeTnefMessage.MimeTnefMessageThreadAccessToken(this);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000E505 File Offset: 0x0000C705
		internal override ObjectThreadAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000E510 File Offset: 0x0000C710
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000E558 File Offset: 0x0000C758
		public override EmailRecipient From
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipient from;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					from = this.mimeMessage.From;
				}
				return from;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.From = value;
					this.AdjustVersions(versions);
				}
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		public override EmailRecipientCollection To
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection to;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					to = this.mimeMessage.To;
				}
				return to;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		public override EmailRecipientCollection Cc
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection cc;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					cc = this.mimeMessage.Cc;
				}
				return cc;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000E640 File Offset: 0x0000C840
		public override EmailRecipientCollection Bcc
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection bcc;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bcc = this.mimeMessage.Bcc;
				}
				return bcc;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000E688 File Offset: 0x0000C888
		public override EmailRecipientCollection ReplyTo
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection replyTo;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					replyTo = this.mimeMessage.ReplyTo;
				}
				return replyTo;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000E718 File Offset: 0x0000C918
		public override EmailRecipient DispositionNotificationTo
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipient dispositionNotificationTo;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					dispositionNotificationTo = this.mimeMessage.DispositionNotificationTo;
				}
				return dispositionNotificationTo;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.DispositionNotificationTo = value;
					this.AdjustVersions(versions);
				}
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000E770 File Offset: 0x0000C970
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public override EmailRecipient Sender
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipient sender;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					sender = this.mimeMessage.Sender;
				}
				return sender;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Sender = value;
					this.AdjustVersions(versions);
				}
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000E810 File Offset: 0x0000CA10
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000E884 File Offset: 0x0000CA84
		public override DateTime Date
		{
			get
			{
				this.ThrowIfDisposed();
				DateTime date;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						DateTime? property = this.tnefMessage.GetProperty<DateTime>(TnefPropertyTag.ClientSubmitTime);
						if (property != null)
						{
							return property.Value;
						}
					}
					date = this.mimeMessage.Date;
				}
				return date;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Date = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.Date = ((DateTime.MinValue != value) ? value.ToUniversalTime() : value);
					}
				}
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000E904 File Offset: 0x0000CB04
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000E978 File Offset: 0x0000CB78
		public override DateTime Expires
		{
			get
			{
				this.ThrowIfDisposed();
				DateTime expires;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						DateTime? property = this.tnefMessage.GetProperty<DateTime>(TnefPropertyTag.ExpiryTime);
						if (property != null)
						{
							return property.Value;
						}
					}
					expires = this.mimeMessage.Expires;
				}
				return expires;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Expires = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.Expires = ((DateTime.MinValue != value) ? value.ToUniversalTime() : value);
					}
				}
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		public override DateTime ReplyBy
		{
			get
			{
				this.ThrowIfDisposed();
				DateTime replyBy;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						DateTime? property = this.tnefMessage.GetProperty<DateTime>(TnefPropertyTag.ReplyTime);
						if (property != null)
						{
							return property.Value;
						}
					}
					replyBy = this.mimeMessage.ReplyBy;
				}
				return replyBy;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.ReplyBy = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.ReplyBy = ((DateTime.MinValue != value) ? value.ToUniversalTime() : value);
					}
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public override string Subject
		{
			get
			{
				this.ThrowIfDisposed();
				string subject2;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						string subject = this.tnefMessage.Subject;
						if (!string.IsNullOrEmpty(subject))
						{
							return subject;
						}
					}
					subject2 = this.mimeMessage.Subject;
				}
				return subject2;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Subject = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.Subject = value;
					}
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000EC28 File Offset: 0x0000CE28
		public override string MessageId
		{
			get
			{
				this.ThrowIfDisposed();
				string messageId2;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						string messageId = this.tnefMessage.MessageId;
						if (!string.IsNullOrEmpty(messageId))
						{
							return messageId;
						}
					}
					messageId2 = this.mimeMessage.MessageId;
				}
				return messageId2;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.MessageId = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.MessageId = value;
					}
				}
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000EC94 File Offset: 0x0000CE94
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		public override Importance Importance
		{
			get
			{
				this.ThrowIfDisposed();
				Importance result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					Importance importance;
					if (this.HasTnef && this.tnefMessage.TryGetImportance(out importance))
					{
						result = importance;
					}
					else
					{
						result = this.mimeMessage.Importance;
					}
				}
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Importance = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.Importance = value;
					}
				}
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000ED64 File Offset: 0x0000CF64
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public override Priority Priority
		{
			get
			{
				this.ThrowIfDisposed();
				Priority result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					Priority priority;
					if (this.HasTnef && this.tnefMessage.TryGetPriority(out priority))
					{
						result = priority;
					}
					else
					{
						result = this.mimeMessage.Priority;
					}
				}
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Priority = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.Priority = value;
					}
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000EE34 File Offset: 0x0000D034
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0000EE98 File Offset: 0x0000D098
		public override Sensitivity Sensitivity
		{
			get
			{
				this.ThrowIfDisposed();
				Sensitivity result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					Sensitivity sensitivity;
					if (this.HasTnef && this.tnefMessage.TryGetSensitivity(out sensitivity))
					{
						result = sensitivity;
					}
					else
					{
						result = this.mimeMessage.Sensitivity;
					}
				}
				return result;
			}
			set
			{
				this.ThrowIfDisposed();
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Sensitivity = value;
					this.AdjustVersions(versions);
					if (this.HasTnef)
					{
						this.tnefMessage.Sensitivity = value;
					}
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000EF04 File Offset: 0x0000D104
		public override string MapiMessageClass
		{
			get
			{
				this.ThrowIfDisposed();
				string mapiMessageClass;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						mapiMessageClass = this.tnefMessage.MapiMessageClass;
					}
					else
					{
						mapiMessageClass = this.mimeMessage.MapiMessageClass;
					}
				}
				return mapiMessageClass;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000EF64 File Offset: 0x0000D164
		public override MimeDocument MimeDocument
		{
			get
			{
				this.ThrowIfDisposed();
				MimeDocument result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = ((this.mimeMessage == null) ? null : this.mimeMessage.MimeDocument);
				}
				return result;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		public override MimePart RootPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart rootPart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					rootPart = this.mimeMessage.RootPart;
				}
				return rootPart;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000F000 File Offset: 0x0000D200
		public override MimePart CalendarPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart calendarPart;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					calendarPart = this.mimeMessage.CalendarPart;
				}
				return calendarPart;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000F048 File Offset: 0x0000D248
		public override MimePart TnefPart
		{
			get
			{
				this.ThrowIfDisposed();
				MimePart result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (!this.HasTnef)
					{
						result = null;
					}
					else
					{
						result = this.tnefPart;
					}
				}
				return result;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000F098 File Offset: 0x0000D298
		internal override void Synchronize()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.mimeMessage.Synchronize();
				this.TnefCheck();
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		internal override int Version
		{
			get
			{
				return this.mimeMessage.Version;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		internal override EmailRecipientCollection BccFromOrgHeader
		{
			get
			{
				this.ThrowIfDisposed();
				EmailRecipientCollection bccFromOrgHeader;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bccFromOrgHeader = this.mimeMessage.BccFromOrgHeader;
				}
				return bccFromOrgHeader;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000F138 File Offset: 0x0000D338
		public override bool IsInterpersonalMessage
		{
			get
			{
				bool isInterpersonalMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						isInterpersonalMessage = this.tnefMessage.IsInterpersonalMessage;
					}
					else
					{
						isInterpersonalMessage = this.mimeMessage.IsInterpersonalMessage;
					}
				}
				return isInterpersonalMessage;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000F190 File Offset: 0x0000D390
		public override bool IsSystemMessage
		{
			get
			{
				bool isSystemMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						isSystemMessage = this.tnefMessage.IsSystemMessage;
					}
					else
					{
						isSystemMessage = this.mimeMessage.IsSystemMessage;
					}
				}
				return isSystemMessage;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		public override bool IsPublicFolderReplicationMessage
		{
			get
			{
				bool isPublicFolderReplicationMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						isPublicFolderReplicationMessage = this.tnefMessage.IsPublicFolderReplicationMessage;
					}
					else
					{
						isPublicFolderReplicationMessage = this.mimeMessage.IsPublicFolderReplicationMessage;
					}
				}
				return isPublicFolderReplicationMessage;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000F240 File Offset: 0x0000D440
		public override bool IsOpaqueMessage
		{
			get
			{
				bool isOpaqueMessage;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						isOpaqueMessage = this.tnefMessage.IsOpaqueMessage;
					}
					else
					{
						isOpaqueMessage = this.mimeMessage.IsOpaqueMessage;
					}
				}
				return isOpaqueMessage;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000F298 File Offset: 0x0000D498
		public override MessageSecurityType MessageSecurityType
		{
			get
			{
				MessageSecurityType messageSecurityType;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					if (this.HasTnef)
					{
						messageSecurityType = this.tnefMessage.MessageSecurityType;
					}
					else
					{
						messageSecurityType = this.mimeMessage.MessageSecurityType;
					}
				}
				return messageSecurityType;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		internal PureMimeMessage PureMimeMessage
		{
			get
			{
				this.ThrowIfDisposed();
				PureMimeMessage result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.mimeMessage;
				}
				return result;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000F334 File Offset: 0x0000D534
		internal PureTnefMessage PureTnefMessage
		{
			get
			{
				this.ThrowIfDisposed();
				PureTnefMessage result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.tnefMessage;
				}
				return result;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000F378 File Offset: 0x0000D578
		internal bool HasTnef
		{
			get
			{
				this.ThrowIfDisposed();
				bool result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					bool flag = this.TnefCheck();
					if (!flag && this.tnefMessage != null)
					{
						this.tnefMessage.Dispose();
						this.tnefMessage = null;
					}
					result = flag;
				}
				return result;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		internal override void Dispose(bool disposing)
		{
			if (disposing && this.mimeMessage != null)
			{
				this.mimeMessage.Dispose(disposing);
				this.mimeMessage = null;
				this.DisposeTnef();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000F40C File Offset: 0x0000D60C
		public override void Normalize(bool allowUTF8 = false)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				this.mimeMessage.NormalizeStructure(false);
				MimeTnefVersions versions = this.SnapshotVersions();
				this.mimeMessage.Normalize((NormalizeOptions)65534, allowUTF8);
				this.AdjustVersions(versions);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000F474 File Offset: 0x0000D674
		internal override void Normalize(NormalizeOptions normalizeOptions, bool allowUTF8)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if ((normalizeOptions & NormalizeOptions.NormalizeMimeStructure) != (NormalizeOptions)0)
				{
					this.mimeMessage.NormalizeStructure(false);
					normalizeOptions &= ~NormalizeOptions.NormalizeMimeStructure;
				}
				if ((normalizeOptions & NormalizeOptions.NormalizeMime) != (NormalizeOptions)0)
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					this.mimeMessage.Normalize(normalizeOptions, allowUTF8);
					this.AdjustVersions(versions);
				}
				if ((normalizeOptions & NormalizeOptions.NormalizeTnef) != (NormalizeOptions)0 && this.HasTnef)
				{
					this.tnefMessage.Normalize(normalizeOptions, allowUTF8);
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000F508 File Offset: 0x0000D708
		internal override IMapiPropertyAccess MapiProperties
		{
			get
			{
				IMapiPropertyAccess result;
				using (ThreadAccessGuard.EnterPublic(this.accessToken))
				{
					result = this.tnefMessage;
				}
				return result;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000F548 File Offset: 0x0000D748
		internal override void AddRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient newRecipient)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				this.mimeMessage.AddRecipient(recipientType, ref cachedHeader, newRecipient);
				this.AdjustVersions(versions);
				if (this.HasTnef)
				{
					this.tnefMessage.AddRecipient(recipientType, ref cachedHeader, newRecipient);
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000F5B0 File Offset: 0x0000D7B0
		internal override void RemoveRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient oldRecipient)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				this.mimeMessage.RemoveRecipient(recipientType, ref cachedHeader, oldRecipient);
				this.AdjustVersions(versions);
				if (this.HasTnef)
				{
					this.tnefMessage.RemoveRecipient(recipientType, ref cachedHeader, oldRecipient);
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000F618 File Offset: 0x0000D818
		internal override void ClearRecipients(RecipientType recipientType, ref object cachedHeader)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				this.mimeMessage.ClearRecipients(recipientType, ref cachedHeader);
				this.AdjustVersions(versions);
				if (this.HasTnef)
				{
					this.tnefMessage.ClearRecipients(recipientType, ref cachedHeader);
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0000F680 File Offset: 0x0000D880
		internal override IBody GetBody()
		{
			return this;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000F684 File Offset: 0x0000D884
		internal IBody InternalGetBody()
		{
			IBody result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body;
				if (this.HasTnef)
				{
					body = this.tnefMessage;
					BodyFormat bodyFormat = body.GetBodyFormat();
					if (bodyFormat != BodyFormat.None)
					{
						return body;
					}
				}
				body = this.mimeMessage.GetBody();
				result = body;
			}
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		bool IBody.ConversionNeeded(int[] validCodepages)
		{
			return this.InternalGetBody().ConversionNeeded(validCodepages);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		BodyFormat IBody.GetBodyFormat()
		{
			BodyFormat bodyFormat;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bodyFormat = this.InternalGetBody().GetBodyFormat();
			}
			return bodyFormat;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000F73C File Offset: 0x0000D93C
		string IBody.GetCharsetName()
		{
			string charsetName;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				charsetName = this.InternalGetBody().GetCharsetName();
			}
			return charsetName;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000F780 File Offset: 0x0000D980
		MimePart IBody.GetMimePart()
		{
			MimePart mimePart;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				mimePart = this.InternalGetBody().GetMimePart();
			}
			return mimePart;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		Stream IBody.GetContentReadStream()
		{
			Stream contentReadStream;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				contentReadStream = this.InternalGetBody().GetContentReadStream();
			}
			return contentReadStream;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000F808 File Offset: 0x0000DA08
		bool IBody.TryGetContentReadStream(out Stream stream)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = this.InternalGetBody().TryGetContentReadStream(out stream);
			}
			return result;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000F84C File Offset: 0x0000DA4C
		Stream IBody.GetContentWriteStream(Charset charset)
		{
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				IBody body = this.InternalGetBody();
				MimeTnefVersions versions = this.SnapshotVersions();
				Stream contentWriteStream = body.GetContentWriteStream(charset);
				this.AdjustVersions(versions);
				result = contentWriteStream;
			}
			return result;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		void IBody.SetNewContent(DataStorage storage, long start, long end)
		{
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		internal override AttachmentCookie AttachmentCollection_AddAttachment(Attachment attachment)
		{
			AttachmentCookie result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				AttachmentCookie attachmentCookie;
				if (this.HasTnef)
				{
					attachmentCookie = this.tnefMessage.AttachmentCollection_AddAttachment(attachment);
				}
				else
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					attachmentCookie = this.mimeMessage.AttachmentCollection_AddAttachment(attachment);
					this.AdjustVersions(versions);
				}
				result = attachmentCookie;
			}
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000F918 File Offset: 0x0000DB18
		internal override bool AttachmentCollection_RemoveAttachment(AttachmentCookie cookie)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				bool flag;
				if (cookie.MessageImplementation is PureTnefMessage)
				{
					flag = this.tnefMessage.AttachmentCollection_RemoveAttachment(cookie);
				}
				else
				{
					MimeTnefVersions versions = this.SnapshotVersions();
					flag = this.mimeMessage.AttachmentCollection_RemoveAttachment(cookie);
					this.AdjustVersions(versions);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000F98C File Offset: 0x0000DB8C
		internal override void AttachmentCollection_ClearAttachments()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				this.mimeMessage.AttachmentCollection_ClearAttachments();
				this.AdjustVersions(versions);
				if (this.HasTnef)
				{
					this.tnefMessage.AttachmentCollection_ClearAttachments();
				}
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		internal override int AttachmentCollection_Count()
		{
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				int num = this.mimeMessage.AttachmentCollection_Count();
				if (this.HasTnef)
				{
					int num2 = this.tnefMessage.AttachmentCollection_Count();
					num += num2;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		internal override object AttachmentCollection_Indexer(int publicIndex)
		{
			object result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				int num = this.mimeMessage.AttachmentCollection_Count();
				if (publicIndex < 0)
				{
					result = null;
				}
				else if (publicIndex < num)
				{
					object obj = this.mimeMessage.AttachmentCollection_Indexer(publicIndex);
					result = obj;
				}
				else
				{
					int num2 = this.tnefMessage.AttachmentCollection_Count();
					publicIndex -= num;
					object obj2 = this.tnefMessage.AttachmentCollection_Indexer(publicIndex);
					result = obj2;
				}
			}
			return result;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		internal override AttachmentCookie AttachmentCollection_CacheAttachment(int publicIndex, object attachment)
		{
			AttachmentCookie result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				int num = this.mimeMessage.AttachmentCollection_Count();
				if (publicIndex < 0)
				{
					result = new AttachmentCookie(0, null);
				}
				else if (publicIndex < num)
				{
					AttachmentCookie attachmentCookie = this.mimeMessage.AttachmentCollection_CacheAttachment(publicIndex, attachment);
					result = attachmentCookie;
				}
				else
				{
					int num2 = this.tnefMessage.AttachmentCollection_Count();
					publicIndex -= num;
					if (publicIndex >= num2)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					AttachmentCookie attachmentCookie2 = this.tnefMessage.AttachmentCollection_CacheAttachment(publicIndex, attachment);
					result = attachmentCookie2;
				}
			}
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000FB78 File Offset: 0x0000DD78
		internal override string Attachment_GetContentType(AttachmentCookie cookie)
		{
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetContentType(cookie);
			}
			return result;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		internal override void Attachment_SetContentType(AttachmentCookie cookie, string contentType)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				cookie.MessageImplementation.Attachment_SetContentType(cookie, contentType);
				this.AdjustVersions(versions);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000FC10 File Offset: 0x0000DE10
		internal override AttachmentMethod Attachment_GetAttachmentMethod(AttachmentCookie cookie)
		{
			AttachmentMethod result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachmentMethod(cookie);
			}
			return result;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000FC54 File Offset: 0x0000DE54
		internal override InternalAttachmentType Attachment_GetAttachmentType(AttachmentCookie cookie)
		{
			InternalAttachmentType result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachmentType(cookie);
			}
			return result;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000FC98 File Offset: 0x0000DE98
		internal override void Attachment_SetAttachmentType(AttachmentCookie cookie, InternalAttachmentType attachmentType)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				cookie.MessageImplementation.Attachment_SetAttachmentType(cookie, attachmentType);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		internal override EmailMessage Attachment_GetEmbeddedMessage(AttachmentCookie cookie)
		{
			EmailMessage result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetEmbeddedMessage(cookie);
			}
			return result;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000FD20 File Offset: 0x0000DF20
		internal override void Attachment_SetEmbeddedMessage(AttachmentCookie cookie, EmailMessage embeddedMessage)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				cookie.MessageImplementation.Attachment_SetEmbeddedMessage(cookie, embeddedMessage);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000FD64 File Offset: 0x0000DF64
		internal override MimePart Attachment_GetMimePart(AttachmentCookie cookie)
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetMimePart(cookie);
			}
			return result;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000FDA8 File Offset: 0x0000DFA8
		internal override string Attachment_GetFileName(AttachmentCookie cookie, ref int sequenceNumber)
		{
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetFileName(cookie, ref sequenceNumber);
			}
			return result;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		internal override void Attachment_SetFileName(AttachmentCookie cookie, string fileName)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				cookie.MessageImplementation.Attachment_SetFileName(cookie, fileName);
				this.AdjustVersions(versions);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000FE44 File Offset: 0x0000E044
		internal override string Attachment_GetContentDisposition(AttachmentCookie cookie)
		{
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetContentDisposition(cookie);
			}
			return result;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000FE88 File Offset: 0x0000E088
		internal override void Attachment_SetContentDisposition(AttachmentCookie cookie, string value)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				cookie.MessageImplementation.Attachment_SetContentDisposition(cookie, value);
				this.AdjustVersions(versions);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		internal override bool Attachment_IsAppleDouble(AttachmentCookie cookie)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_IsAppleDouble(cookie);
			}
			return result;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000FF20 File Offset: 0x0000E120
		internal override Stream Attachment_GetContentReadStream(AttachmentCookie cookie)
		{
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetContentReadStream(cookie);
			}
			return result;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000FF64 File Offset: 0x0000E164
		internal override bool Attachment_TryGetContentReadStream(AttachmentCookie cookie, out Stream result)
		{
			bool result2;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result2 = cookie.MessageImplementation.Attachment_TryGetContentReadStream(cookie, out result);
			}
			return result2;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		internal override Stream Attachment_GetContentWriteStream(AttachmentCookie cookie)
		{
			Stream result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefVersions versions = this.SnapshotVersions();
				Stream stream = cookie.MessageImplementation.Attachment_GetContentWriteStream(cookie);
				this.AdjustVersions(versions);
				result = stream;
			}
			return result;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00010000 File Offset: 0x0000E200
		internal override int Attachment_GetRenderingPosition(AttachmentCookie cookie)
		{
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetRenderingPosition(cookie);
			}
			return result;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00010044 File Offset: 0x0000E244
		internal override string Attachment_GetAttachContentID(AttachmentCookie cookie)
		{
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachContentID(cookie);
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00010088 File Offset: 0x0000E288
		internal override string Attachment_GetAttachContentLocation(AttachmentCookie cookie)
		{
			string result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachContentLocation(cookie);
			}
			return result;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000100CC File Offset: 0x0000E2CC
		internal override byte[] Attachment_GetAttachRendering(AttachmentCookie cookie)
		{
			byte[] result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachRendering(cookie);
			}
			return result;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00010110 File Offset: 0x0000E310
		internal override int Attachment_GetAttachmentFlags(AttachmentCookie cookie)
		{
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachmentFlags(cookie);
			}
			return result;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00010154 File Offset: 0x0000E354
		internal override bool Attachment_GetAttachHidden(AttachmentCookie cookie)
		{
			bool result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetAttachHidden(cookie);
			}
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00010198 File Offset: 0x0000E398
		internal override int Attachment_GetHashCode(AttachmentCookie cookie)
		{
			int result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				result = cookie.MessageImplementation.Attachment_GetHashCode(cookie);
			}
			return result;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000101DC File Offset: 0x0000E3DC
		internal override void Attachment_Dispose(AttachmentCookie cookie)
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				cookie.MessageImplementation.Attachment_Dispose(cookie);
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00010220 File Offset: 0x0000E420
		internal override void SetReadOnly(bool makeReadOnly)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.tnefRelayStorage != null)
				{
					this.tnefRelayStorage.SetReadOnly(makeReadOnly);
				}
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00010270 File Offset: 0x0000E470
		internal override void CopyTo(MessageImplementation destination)
		{
			this.ThrowIfDisposed();
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimeTnefMessage mimeTnefMessage = (MimeTnefMessage)destination;
				using (ThreadAccessGuard.EnterPublic(mimeTnefMessage.accessToken))
				{
					if (mimeTnefMessage != this)
					{
						base.CopyTo(mimeTnefMessage);
						this.mimeMessage.CopyTo(mimeTnefMessage.mimeMessage);
						if (mimeTnefMessage.HasTnef)
						{
							mimeTnefMessage.DisposeTnef();
						}
						mimeTnefMessage.tnefCheckRootPartVersion = -1;
						mimeTnefMessage.tnefCheckTnefPartVersion = -1;
						mimeTnefMessage.tnefState = MimeTnefMessage.TnefState.NoTnef;
						mimeTnefMessage.tnefPart = null;
					}
				}
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00010320 File Offset: 0x0000E520
		internal void InvalidateTnefContent()
		{
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				if (this.tnefRelayStorage == null)
				{
					this.tnefRelayStorage = new RelayStorage(this.tnefMessage);
				}
				else
				{
					this.tnefRelayStorage.Invalidate();
				}
				this.SetTnefPartContent(this.tnefRelayStorage, 0L, long.MaxValue);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00010394 File Offset: 0x0000E594
		internal Charset GetMessageCharsetFromMime()
		{
			Charset result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				string name = null;
				Charset charset = null;
				if (this.mimeMessage.RootPart.ContentType == "multipart/mixed")
				{
					MimePart mimePart = this.mimeMessage.RootPart.FirstChild as MimePart;
					if (mimePart != null && mimePart.ContentType == "text/plain")
					{
						name = Utility.GetParameterValue(mimePart, HeaderId.ContentType, "charset");
						charset = (Charset.TryGetCharset(name, out charset) ? charset : Charset.DefaultMimeCharset);
						return charset;
					}
				}
				foreach (Header header in this.mimeMessage.RootPart.Headers)
				{
					TextHeader textHeader = header as TextHeader;
					if (textHeader != null)
					{
						if (Utility.Get2047CharsetName(textHeader, out name))
						{
							charset = (Charset.TryGetCharset(name, out charset) ? charset : Charset.DefaultMimeCharset);
							return charset;
						}
					}
					else
					{
						AddressHeader addressHeader = header as AddressHeader;
						if (addressHeader != null)
						{
							foreach (AddressItem addressItem in addressHeader)
							{
								if (Utility.Get2047CharsetName(addressItem, out name))
								{
									charset = (Charset.TryGetCharset(name, out charset) ? charset : Charset.DefaultMimeCharset);
									return charset;
								}
								MimeGroup mimeGroup = addressItem as MimeGroup;
								if (mimeGroup != null)
								{
									foreach (MimeRecipient addressItem2 in mimeGroup)
									{
										if (Utility.Get2047CharsetName(addressItem2, out name))
										{
											charset = (Charset.TryGetCharset(name, out charset) ? charset : Charset.DefaultMimeCharset);
											return charset;
										}
									}
								}
							}
						}
					}
				}
				if (this.mimeMessage.MimeDocument != null && this.mimeMessage.MimeDocument.EffectiveHeaderDecodingOptions.Charset != null)
				{
					result = this.mimeMessage.MimeDocument.EffectiveHeaderDecodingOptions.Charset;
				}
				else
				{
					result = Charset.DefaultMimeCharset;
				}
			}
			return result;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00010620 File Offset: 0x0000E820
		internal MimePart GetLegacyPlainTextBody()
		{
			MimePart result;
			using (ThreadAccessGuard.EnterPublic(this.accessToken))
			{
				MimePart mimePart = this.mimeMessage.RootPart.FirstChild as MimePart;
				if (mimePart.ContentType == "text/plain")
				{
					result = mimePart;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00010684 File Offset: 0x0000E884
		private void SetTnefPartContent(DataStorage newContent, long start, long end)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				bool contentDirty = this.tnefPart.ContentDirty;
				MimeTnefVersions versions = this.SnapshotVersions();
				bool isSynchronized = this.mimeMessage.IsSynchronized;
				ContentTransferEncoding contentTransferEncoding = this.tnefPart.ContentTransferEncoding;
				if (ContentTransferEncoding.Binary != contentTransferEncoding && ContentTransferEncoding.Base64 != contentTransferEncoding)
				{
					Header header = this.tnefPart.Headers.FindFirst(HeaderId.ContentTransferEncoding);
					if (header == null)
					{
						header = Header.Create(HeaderId.ContentTransferEncoding);
						this.tnefPart.Headers.AppendChild(header);
					}
					header.Value = "base64";
				}
				this.tnefPart.SetStorage(newContent, start, end);
				if (isSynchronized)
				{
					this.mimeMessage.UpdateMimeVersion();
				}
				this.AdjustVersions(versions);
				this.tnefPart.ContentDirty = contentDirty;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010760 File Offset: 0x0000E960
		private bool TnefCheck()
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (this.tnefCheckRootPartVersion != this.mimeMessage.Version)
				{
					MimePart rootPart = this.mimeMessage.RootPart;
					if (rootPart == null)
					{
						result = false;
					}
					else
					{
						this.tnefCheckRootPartVersion = this.mimeMessage.Version;
						if (this.TnefCheck(rootPart, true))
						{
							result = true;
						}
						else if (this.tnefState == MimeTnefMessage.TnefState.Invalid)
						{
							result = false;
						}
						else
						{
							if (rootPart.ContentType == "multipart/mixed")
							{
								for (MimePart mimePart = rootPart.FirstChild as MimePart; mimePart != null; mimePart = (mimePart.NextSibling as MimePart))
								{
									if (this.TnefCheck(mimePart, false))
									{
										return true;
									}
									if (this.tnefState == MimeTnefMessage.TnefState.Invalid)
									{
										return false;
									}
								}
							}
							this.tnefState = MimeTnefMessage.TnefState.NoTnef;
							result = false;
						}
					}
				}
				else
				{
					result = (null != this.tnefMessage);
				}
			}
			return result;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010850 File Offset: 0x0000EA50
		private bool TnefCheck(MimePart candidate, bool isRoot)
		{
			bool result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				if (candidate == this.tnefPart && candidate.Version == this.tnefCheckTnefPartVersion)
				{
					result = (this.tnefState == MimeTnefMessage.TnefState.Valid);
				}
				else
				{
					ContentTypeHeader contentTypeHeader = candidate.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
					if (contentTypeHeader == null)
					{
						result = false;
					}
					else
					{
						string headerValue = Utility.GetHeaderValue(contentTypeHeader);
						if (string.IsNullOrEmpty(headerValue))
						{
							result = false;
						}
						else
						{
							if (headerValue != "application/ms-tnef")
							{
								if (headerValue != "application/octet-stream" && !headerValue.StartsWith("application/x-openmail", StringComparison.Ordinal))
								{
									return false;
								}
								DecodingOptions decodingOptions = new DecodingOptions((DecodingFlags)131071);
								MimeParameter mimeParameter = contentTypeHeader["name"];
								DecodingResults decodingResults;
								string a;
								if (mimeParameter == null || !mimeParameter.TryGetValue(decodingOptions, out decodingResults, out a) || !string.Equals(a, "winmail.dat", StringComparison.OrdinalIgnoreCase))
								{
									return false;
								}
							}
							if (this.tnefRelayStorage != null)
							{
								if (candidate == this.tnefPart && !candidate.ContentDirty)
								{
									return MimeTnefMessage.TnefState.Valid == this.tnefState;
								}
								this.tnefRelayStorage.PermanentlyRelay();
								this.tnefRelayStorage.Release();
								this.tnefRelayStorage = null;
							}
							if (this.tnefMessage != null)
							{
								this.tnefMessage.Dispose();
								this.tnefMessage = null;
							}
							if (this.tnefPart != null && this.tnefPart != candidate && this.tnefState == MimeTnefMessage.TnefState.Valid)
							{
								this.mimeMessage.SetTnefPart(null);
								this.Normalize(false);
								this.tnefCheckRootPartVersion = this.mimeMessage.Version;
							}
							this.tnefPart = candidate;
							this.tnefCheckTnefPartVersion = candidate.Version;
							Stream stream;
							if (!candidate.TryGetContentReadStream(out stream))
							{
								this.tnefState = MimeTnefMessage.TnefState.Invalid;
								result = false;
							}
							else
							{
								Stream stream2 = stream;
								DataStorage storage;
								long tnefStart;
								long tnefEnd;
								if (candidate.BodyCte == ContentTransferEncoding.Binary)
								{
									storage = candidate.Storage;
									tnefStart = candidate.DataStart + candidate.BodyOffset;
									tnefEnd = candidate.DataEnd;
								}
								else
								{
									ForkToTempStorageReadStream forkToTempStorageReadStream = new ForkToTempStorageReadStream(stream);
									storage = forkToTempStorageReadStream.Storage;
									tnefStart = 0L;
									tnefEnd = long.MaxValue;
									stream2 = forkToTempStorageReadStream;
								}
								if (storage == null)
								{
									this.tnefState = MimeTnefMessage.TnefState.Invalid;
									if (stream2 != null)
									{
										stream2.Dispose();
									}
									result = false;
								}
								else
								{
									PureTnefMessage pureTnefMessage = new PureTnefMessage(this, candidate, storage, tnefStart, tnefEnd);
									if (pureTnefMessage.Load(stream2))
									{
										string correlator = pureTnefMessage.Correlator;
										string tnefCorrelator = this.mimeMessage.TnefCorrelator;
										if (tnefCorrelator == null)
										{
											tnefCorrelator = Utility.GetTnefCorrelator(candidate);
										}
										if (EmailMessage.TestabilityEnableBetterFuzzing || (string.IsNullOrEmpty(tnefCorrelator) && string.IsNullOrEmpty(correlator)) || string.Equals(tnefCorrelator, correlator, StringComparison.OrdinalIgnoreCase))
										{
											this.tnefMessage = pureTnefMessage;
											this.mimeMessage.SetTnefPart(this.tnefPart);
											this.tnefMessage.Stnef = isRoot;
											candidate.ContentDirty = false;
											this.tnefState = MimeTnefMessage.TnefState.Valid;
											return true;
										}
									}
									this.tnefState = MimeTnefMessage.TnefState.Invalid;
									result = false;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00010B44 File Offset: 0x0000ED44
		private void DisposeTnef()
		{
			if (this.tnefMessage != null)
			{
				if (this.tnefRelayStorage != null)
				{
					this.tnefRelayStorage.Release();
					this.tnefRelayStorage = null;
				}
				this.tnefMessage.Dispose();
				this.tnefMessage = null;
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00010B7C File Offset: 0x0000ED7C
		private MimeTnefVersions SnapshotVersions()
		{
			MimeTnefVersions result;
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				result = new MimeTnefVersions(this.mimeMessage, this.tnefPart);
			}
			return result;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		private void AdjustVersions(MimeTnefVersions versions)
		{
			using (ThreadAccessGuard.EnterPrivate(this.accessToken))
			{
				this.tnefCheckRootPartVersion += this.mimeMessage.Version - versions.RootPartVersion;
				if (-1 != versions.TnefPartVersion)
				{
					this.tnefCheckTnefPartVersion += this.tnefPart.Version - versions.TnefPartVersion;
				}
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00010C44 File Offset: 0x0000EE44
		private void ThrowIfDisposed()
		{
			if (this.mimeMessage == null)
			{
				throw new ObjectDisposedException("EmailMessage");
			}
		}

		// Token: 0x040003B1 RID: 945
		private MimeTnefMessage.MimeTnefMessageThreadAccessToken accessToken;

		// Token: 0x040003B2 RID: 946
		private PureMimeMessage mimeMessage;

		// Token: 0x040003B3 RID: 947
		private PureTnefMessage tnefMessage;

		// Token: 0x040003B4 RID: 948
		private MimePart tnefPart;

		// Token: 0x040003B5 RID: 949
		private RelayStorage tnefRelayStorage;

		// Token: 0x040003B6 RID: 950
		private int tnefCheckRootPartVersion = -1;

		// Token: 0x040003B7 RID: 951
		private int tnefCheckTnefPartVersion = -1;

		// Token: 0x040003B8 RID: 952
		private MimeTnefMessage.TnefState tnefState;

		// Token: 0x020000EE RID: 238
		private class MimeTnefMessageThreadAccessToken : ObjectThreadAccessToken
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x00010C59 File Offset: 0x0000EE59
			internal MimeTnefMessageThreadAccessToken(MimeTnefMessage parent)
			{
			}
		}

		// Token: 0x020000EF RID: 239
		private enum TnefState
		{
			// Token: 0x040003BA RID: 954
			NoTnef,
			// Token: 0x040003BB RID: 955
			Valid,
			// Token: 0x040003BC RID: 956
			Invalid
		}
	}
}
