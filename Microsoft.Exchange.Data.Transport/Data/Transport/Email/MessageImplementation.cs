using System;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000E5 RID: 229
	internal abstract class MessageImplementation : IDisposable
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000522 RID: 1314
		internal abstract ObjectThreadAccessToken AccessToken { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000523 RID: 1315
		// (set) Token: 0x06000524 RID: 1316
		public abstract EmailRecipient From { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000525 RID: 1317
		public abstract EmailRecipientCollection To { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000526 RID: 1318
		public abstract EmailRecipientCollection Cc { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000527 RID: 1319
		public abstract EmailRecipientCollection Bcc { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000528 RID: 1320
		public abstract EmailRecipientCollection ReplyTo { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000529 RID: 1321
		// (set) Token: 0x0600052A RID: 1322
		public abstract EmailRecipient DispositionNotificationTo { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600052B RID: 1323
		// (set) Token: 0x0600052C RID: 1324
		public abstract EmailRecipient Sender { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600052D RID: 1325
		// (set) Token: 0x0600052E RID: 1326
		public abstract DateTime Date { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600052F RID: 1327
		// (set) Token: 0x06000530 RID: 1328
		public abstract DateTime Expires { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000531 RID: 1329
		// (set) Token: 0x06000532 RID: 1330
		public abstract DateTime ReplyBy { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000533 RID: 1331
		// (set) Token: 0x06000534 RID: 1332
		public abstract string Subject { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000535 RID: 1333
		// (set) Token: 0x06000536 RID: 1334
		public abstract string MessageId { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000537 RID: 1335
		// (set) Token: 0x06000538 RID: 1336
		public abstract Importance Importance { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000539 RID: 1337
		// (set) Token: 0x0600053A RID: 1338
		public abstract Priority Priority { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600053B RID: 1339
		// (set) Token: 0x0600053C RID: 1340
		public abstract Sensitivity Sensitivity { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600053D RID: 1341
		public abstract string MapiMessageClass { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000CD02 File Offset: 0x0000AF02
		public virtual MimeDocument MimeDocument
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000CD05 File Offset: 0x0000AF05
		public virtual MimePart RootPart
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000CD08 File Offset: 0x0000AF08
		public virtual MimePart CalendarPart
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000CD0B File Offset: 0x0000AF0B
		public virtual MimePart TnefPart
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000542 RID: 1346
		public abstract bool IsInterpersonalMessage { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000543 RID: 1347
		public abstract bool IsPublicFolderReplicationMessage { get; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000544 RID: 1348
		public abstract bool IsSystemMessage { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000545 RID: 1349
		public abstract bool IsOpaqueMessage { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000546 RID: 1350
		public abstract MessageSecurityType MessageSecurityType { get; }

		// Token: 0x06000547 RID: 1351 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000CD1D File Offset: 0x0000AF1D
		internal virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000549 RID: 1353
		public abstract void Normalize(bool allowUTF8);

		// Token: 0x0600054A RID: 1354
		internal abstract void Normalize(NormalizeOptions normalizeOptions, bool allowUTF8);

		// Token: 0x0600054B RID: 1355
		internal abstract void Synchronize();

		// Token: 0x0600054C RID: 1356
		internal abstract void SetReadOnly(bool makeReadOnly);

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600054D RID: 1357
		internal abstract IMapiPropertyAccess MapiProperties { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600054E RID: 1358
		internal abstract int Version { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600054F RID: 1359
		internal abstract EmailRecipientCollection BccFromOrgHeader { get; }

		// Token: 0x06000550 RID: 1360
		internal abstract void AddRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient newRecipient);

		// Token: 0x06000551 RID: 1361
		internal abstract void RemoveRecipient(RecipientType recipientType, ref object cachedHeader, EmailRecipient oldRecipient);

		// Token: 0x06000552 RID: 1362
		internal abstract void ClearRecipients(RecipientType recipientType, ref object cachedHeader);

		// Token: 0x06000553 RID: 1363
		internal abstract IBody GetBody();

		// Token: 0x06000554 RID: 1364
		internal abstract AttachmentCookie AttachmentCollection_AddAttachment(Attachment attachment);

		// Token: 0x06000555 RID: 1365
		internal abstract bool AttachmentCollection_RemoveAttachment(AttachmentCookie cookie);

		// Token: 0x06000556 RID: 1366
		internal abstract void AttachmentCollection_ClearAttachments();

		// Token: 0x06000557 RID: 1367
		internal abstract int AttachmentCollection_Count();

		// Token: 0x06000558 RID: 1368
		internal abstract object AttachmentCollection_Indexer(int index);

		// Token: 0x06000559 RID: 1369
		internal abstract AttachmentCookie AttachmentCollection_CacheAttachment(int publicIndex, object attachment);

		// Token: 0x0600055A RID: 1370
		internal abstract MimePart Attachment_GetMimePart(AttachmentCookie cookie);

		// Token: 0x0600055B RID: 1371
		internal abstract string Attachment_GetContentType(AttachmentCookie cookie);

		// Token: 0x0600055C RID: 1372
		internal abstract void Attachment_SetContentType(AttachmentCookie cookie, string contentType);

		// Token: 0x0600055D RID: 1373
		internal abstract AttachmentMethod Attachment_GetAttachmentMethod(AttachmentCookie cookie);

		// Token: 0x0600055E RID: 1374
		internal abstract InternalAttachmentType Attachment_GetAttachmentType(AttachmentCookie cookie);

		// Token: 0x0600055F RID: 1375
		internal abstract void Attachment_SetAttachmentType(AttachmentCookie cookie, InternalAttachmentType attachmentType);

		// Token: 0x06000560 RID: 1376
		internal abstract EmailMessage Attachment_GetEmbeddedMessage(AttachmentCookie cookie);

		// Token: 0x06000561 RID: 1377
		internal abstract void Attachment_SetEmbeddedMessage(AttachmentCookie cookie, EmailMessage embeddedMessage);

		// Token: 0x06000562 RID: 1378
		internal abstract string Attachment_GetFileName(AttachmentCookie cookie, ref int sequenceNumber);

		// Token: 0x06000563 RID: 1379
		internal abstract void Attachment_SetFileName(AttachmentCookie cookie, string fileName);

		// Token: 0x06000564 RID: 1380
		internal abstract string Attachment_GetContentDisposition(AttachmentCookie cookie);

		// Token: 0x06000565 RID: 1381
		internal abstract void Attachment_SetContentDisposition(AttachmentCookie cookie, string contentDisposition);

		// Token: 0x06000566 RID: 1382
		internal abstract bool Attachment_IsAppleDouble(AttachmentCookie cookie);

		// Token: 0x06000567 RID: 1383
		internal abstract Stream Attachment_GetContentReadStream(AttachmentCookie cookie);

		// Token: 0x06000568 RID: 1384
		internal abstract bool Attachment_TryGetContentReadStream(AttachmentCookie cookie, out Stream result);

		// Token: 0x06000569 RID: 1385
		internal abstract Stream Attachment_GetContentWriteStream(AttachmentCookie cookie);

		// Token: 0x0600056A RID: 1386
		internal abstract int Attachment_GetRenderingPosition(AttachmentCookie cookie);

		// Token: 0x0600056B RID: 1387
		internal abstract string Attachment_GetAttachContentID(AttachmentCookie cookie);

		// Token: 0x0600056C RID: 1388
		internal abstract string Attachment_GetAttachContentLocation(AttachmentCookie cookie);

		// Token: 0x0600056D RID: 1389
		internal abstract byte[] Attachment_GetAttachRendering(AttachmentCookie cookie);

		// Token: 0x0600056E RID: 1390
		internal abstract int Attachment_GetAttachmentFlags(AttachmentCookie cookie);

		// Token: 0x0600056F RID: 1391
		internal abstract bool Attachment_GetAttachHidden(AttachmentCookie cookie);

		// Token: 0x06000570 RID: 1392
		internal abstract int Attachment_GetHashCode(AttachmentCookie cookie);

		// Token: 0x06000571 RID: 1393
		internal abstract void Attachment_Dispose(AttachmentCookie cookie);

		// Token: 0x06000572 RID: 1394 RVA: 0x0000CD1F File Offset: 0x0000AF1F
		internal virtual void CopyTo(MessageImplementation destination)
		{
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000CD21 File Offset: 0x0000AF21
		internal virtual object GetMapiProperty(TnefPropertyTag tag)
		{
			return null;
		}
	}
}
