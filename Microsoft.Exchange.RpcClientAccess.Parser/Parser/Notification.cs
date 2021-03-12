using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000063 RID: 99
	internal abstract class Notification
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000A8DF File Offset: 0x00008ADF
		protected Notification(Notification.NotificationModifiers notificationType)
		{
			if ((ushort)(notificationType & ~(Notification.NotificationModifiers.CriticalError | Notification.NotificationModifiers.NewMail | Notification.NotificationModifiers.ObjectCreated | Notification.NotificationModifiers.ObjectDeleted | Notification.NotificationModifiers.ObjectModified | Notification.NotificationModifiers.ObjectMoved | Notification.NotificationModifiers.ObjectCopied | Notification.NotificationModifiers.SearchComplete | Notification.NotificationModifiers.TableModified | Notification.NotificationModifiers.StatusObject | Notification.NotificationModifiers.Extended)) != 0)
			{
				throw new ArgumentException("Invalid flag value set", "notificationType");
			}
			this.notificationType = notificationType;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A908 File Offset: 0x00008B08
		public static Notification Parse(Reader reader, PropertyTag[] originalPropertyTags, Encoding string8Encoding)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			Notification.NotificationModifiers notificationModifiers = (Notification.NotificationModifiers)reader.ReadUInt16();
			Notification.NotificationModifiers notificationModifiers2 = notificationModifiers & Notification.NotificationModifiers.NotificationTypeMask;
			if (notificationModifiers2 <= Notification.NotificationModifiers.ObjectModified)
			{
				switch (notificationModifiers2)
				{
				case Notification.NotificationModifiers.NewMail:
					return new Notification.NewMailNotification(reader, notificationModifiers);
				case Notification.NotificationModifiers.CriticalError | Notification.NotificationModifiers.NewMail:
					break;
				case Notification.NotificationModifiers.ObjectCreated:
					return new Notification.ObjectCreatedNotification(reader, notificationModifiers);
				default:
					if (notificationModifiers2 == Notification.NotificationModifiers.ObjectDeleted)
					{
						return new Notification.ObjectDeletedNotification(reader, notificationModifiers);
					}
					if (notificationModifiers2 == Notification.NotificationModifiers.ObjectModified)
					{
						return new Notification.ObjectModifiedNotification(reader, notificationModifiers);
					}
					break;
				}
			}
			else if (notificationModifiers2 <= Notification.NotificationModifiers.ObjectCopied)
			{
				if (notificationModifiers2 == Notification.NotificationModifiers.ObjectMoved)
				{
					return new Notification.ObjectMovedNotification(reader, notificationModifiers);
				}
				if (notificationModifiers2 == Notification.NotificationModifiers.ObjectCopied)
				{
					return new Notification.ObjectCopiedNotification(reader, notificationModifiers);
				}
			}
			else
			{
				if (notificationModifiers2 == Notification.NotificationModifiers.SearchComplete)
				{
					return new Notification.SearchCompleteNotification(reader, notificationModifiers);
				}
				if (notificationModifiers2 == Notification.NotificationModifiers.TableModified)
				{
					return Notification.TableModifiedNotificationFactory.Create(reader, notificationModifiers, originalPropertyTags, string8Encoding);
				}
			}
			throw new BufferParseException(string.Format("Notification type not supported: {0}", notificationModifiers & Notification.NotificationModifiers.NotificationTypeMask));
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public void Serialize(Writer writer, Encoding string8Encoding)
		{
			Notification.NotificationModifiers modifiers = this.GetModifiers();
			writer.WriteUInt16((ushort)modifiers);
			this.InternalSerialize(writer, modifiers, string8Encoding);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			stringBuilder.Append("[");
			this.AppendToString(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AA39 File Offset: 0x00008C39
		protected virtual Notification.NotificationModifiers GetModifiers()
		{
			return this.notificationType;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AA41 File Offset: 0x00008C41
		protected virtual void AppendToString(StringBuilder sb)
		{
			sb.AppendFormat("Modifiers [{0}]", this.GetModifiers().ToString());
		}

		// Token: 0x060002B7 RID: 695
		protected abstract void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding);

		// Token: 0x04000147 RID: 327
		private readonly Notification.NotificationModifiers notificationType;

		// Token: 0x02000064 RID: 100
		[Flags]
		internal enum NotificationModifiers : ushort
		{
			// Token: 0x04000149 RID: 329
			CriticalError = 1,
			// Token: 0x0400014A RID: 330
			NewMail = 2,
			// Token: 0x0400014B RID: 331
			ObjectCreated = 4,
			// Token: 0x0400014C RID: 332
			ObjectDeleted = 8,
			// Token: 0x0400014D RID: 333
			ObjectModified = 16,
			// Token: 0x0400014E RID: 334
			ObjectMoved = 32,
			// Token: 0x0400014F RID: 335
			ObjectCopied = 64,
			// Token: 0x04000150 RID: 336
			SearchComplete = 128,
			// Token: 0x04000151 RID: 337
			TableModified = 256,
			// Token: 0x04000152 RID: 338
			StatusObject = 512,
			// Token: 0x04000153 RID: 339
			Extended = 1024,
			// Token: 0x04000154 RID: 340
			TotalItemsChanged = 4096,
			// Token: 0x04000155 RID: 341
			UnreadItemsChanged = 8192,
			// Token: 0x04000156 RID: 342
			SearchFolder = 16384,
			// Token: 0x04000157 RID: 343
			Message = 32768,
			// Token: 0x04000158 RID: 344
			NotificationTypeMask = 2047
		}

		// Token: 0x02000065 RID: 101
		internal enum TableModifiedNotificationType : ushort
		{
			// Token: 0x0400015A RID: 346
			TableChanged = 1,
			// Token: 0x0400015B RID: 347
			TableError,
			// Token: 0x0400015C RID: 348
			TableRowAdded,
			// Token: 0x0400015D RID: 349
			TableRowDeleted,
			// Token: 0x0400015E RID: 350
			TableRowModified,
			// Token: 0x0400015F RID: 351
			TableSortDone,
			// Token: 0x04000160 RID: 352
			TableRestrictDone,
			// Token: 0x04000161 RID: 353
			TableSetColumnDone,
			// Token: 0x04000162 RID: 354
			TableReload,
			// Token: 0x04000163 RID: 355
			TableRowDeletedExtended
		}

		// Token: 0x02000066 RID: 102
		internal static class TableModifiedNotificationFactory
		{
			// Token: 0x060002B8 RID: 696 RVA: 0x0000AA60 File Offset: 0x00008C60
			public static Notification.TableModifiedNotification Create(Reader reader, Notification.NotificationModifiers modifiers, PropertyTag[] originalPropertyTags, Encoding string8Encoding)
			{
				Notification.TableModifiedNotificationType tableModifiedNotificationType = (Notification.TableModifiedNotificationType)reader.ReadUInt16();
				switch (tableModifiedNotificationType)
				{
				case Notification.TableModifiedNotificationType.TableChanged:
				case Notification.TableModifiedNotificationType.TableRestrictDone:
				case Notification.TableModifiedNotificationType.TableReload:
					return new Notification.TableModifiedNotification(reader, modifiers, tableModifiedNotificationType);
				case Notification.TableModifiedNotificationType.TableRowAdded:
				case Notification.TableModifiedNotificationType.TableRowModified:
					return new Notification.TableModifiedNotification.TableRowAddModifiedNotification(reader, modifiers, tableModifiedNotificationType, originalPropertyTags, string8Encoding);
				case Notification.TableModifiedNotificationType.TableRowDeleted:
					return new Notification.TableModifiedNotification.TableRowDeletedModifiedNotification(reader, modifiers, tableModifiedNotificationType);
				case Notification.TableModifiedNotificationType.TableRowDeletedExtended:
					return new Notification.TableModifiedNotification.TableRowDeletedExtendedNotification(reader, modifiers, tableModifiedNotificationType, string8Encoding);
				}
				throw new BufferParseException(string.Format("TableModifiedNotificationType not supported: {0}", tableModifiedNotificationType));
			}
		}

		// Token: 0x02000067 RID: 103
		internal class TableModifiedNotification : Notification
		{
			// Token: 0x060002B9 RID: 697 RVA: 0x0000AAE4 File Offset: 0x00008CE4
			public TableModifiedNotification(Notification.TableModifiedNotificationType tableModifiedNotificationType) : base(Notification.NotificationModifiers.TableModified)
			{
				this.tableModifiedNotificationType = tableModifiedNotificationType;
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000AAF8 File Offset: 0x00008CF8
			internal TableModifiedNotification(Reader reader, Notification.NotificationModifiers modifiers, Notification.TableModifiedNotificationType tableModifiedNotificationType) : base(modifiers & Notification.NotificationModifiers.NotificationTypeMask)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 256)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
				this.tableModifiedNotificationType = tableModifiedNotificationType;
			}

			// Token: 0x060002BB RID: 699 RVA: 0x0000AB2E File Offset: 0x00008D2E
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				writer.WriteUInt16((ushort)this.tableModifiedNotificationType);
			}

			// Token: 0x060002BC RID: 700 RVA: 0x0000AB3C File Offset: 0x00008D3C
			protected override void AppendToString(StringBuilder sb)
			{
				base.AppendToString(sb);
				sb.AppendFormat(" Type [{0}]", this.tableModifiedNotificationType);
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AB5C File Offset: 0x00008D5C
			public Notification.TableModifiedNotificationType NotificationType
			{
				get
				{
					return this.tableModifiedNotificationType;
				}
			}

			// Token: 0x04000164 RID: 356
			private Notification.TableModifiedNotificationType tableModifiedNotificationType;

			// Token: 0x02000068 RID: 104
			private class TableModifiedNotificationContext
			{
				// Token: 0x060002BE RID: 702 RVA: 0x0000AB64 File Offset: 0x00008D64
				internal TableModifiedNotificationContext(StoreId folderId)
				{
					this.folderId = folderId;
				}

				// Token: 0x060002BF RID: 703 RVA: 0x0000AB73 File Offset: 0x00008D73
				internal TableModifiedNotificationContext(StoreId folderId, StoreId messageId, uint instance) : this(folderId)
				{
					this.messageId = new StoreId?(messageId);
					this.instance = new uint?(instance);
				}

				// Token: 0x060002C0 RID: 704 RVA: 0x0000AB94 File Offset: 0x00008D94
				internal TableModifiedNotificationContext(Reader reader, Notification.NotificationModifiers modifiers)
				{
					this.folderId = StoreId.Parse(reader);
					if ((ushort)(modifiers & Notification.NotificationModifiers.Message) != 0)
					{
						this.messageId = new StoreId?(StoreId.Parse(reader));
						this.instance = new uint?(reader.ReadUInt32());
					}
				}

				// Token: 0x060002C1 RID: 705 RVA: 0x0000ABD4 File Offset: 0x00008DD4
				internal void Serialize(Writer writer)
				{
					this.folderId.Serialize(writer);
					if (this.messageId != null)
					{
						this.messageId.Value.Serialize(writer);
						writer.WriteUInt32(this.instance.Value);
					}
				}

				// Token: 0x060002C2 RID: 706 RVA: 0x0000AC2C File Offset: 0x00008E2C
				internal Notification.NotificationModifiers GetModifiers(Notification.NotificationModifiers modifiers, bool isInSearchFolder)
				{
					if (this.MessageId != null)
					{
						modifiers |= Notification.NotificationModifiers.Message;
					}
					if (isInSearchFolder)
					{
						modifiers |= Notification.NotificationModifiers.SearchFolder;
					}
					return modifiers;
				}

				// Token: 0x060002C3 RID: 707 RVA: 0x0000AC64 File Offset: 0x00008E64
				internal void AppendToString(StringBuilder sb)
				{
					sb.AppendFormat(" [FID [{0}]", this.FolderId);
					if (this.MessageId != null)
					{
						sb.AppendFormat(" MID [{0}] Inst [{1}]", this.MessageId.Value, this.Instance);
					}
					sb.Append("]");
				}

				// Token: 0x1700009F RID: 159
				// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000ACCE File Offset: 0x00008ECE
				public StoreId FolderId
				{
					get
					{
						return this.folderId;
					}
				}

				// Token: 0x170000A0 RID: 160
				// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000ACD6 File Offset: 0x00008ED6
				public StoreId? MessageId
				{
					get
					{
						return this.messageId;
					}
				}

				// Token: 0x170000A1 RID: 161
				// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000ACDE File Offset: 0x00008EDE
				public uint? Instance
				{
					get
					{
						return this.instance;
					}
				}

				// Token: 0x04000165 RID: 357
				private readonly StoreId folderId;

				// Token: 0x04000166 RID: 358
				private readonly StoreId? messageId;

				// Token: 0x04000167 RID: 359
				private readonly uint? instance;
			}

			// Token: 0x02000069 RID: 105
			internal class TableRowDeletedModifiedNotification : Notification.TableModifiedNotification
			{
				// Token: 0x060002C7 RID: 711 RVA: 0x0000ACE6 File Offset: 0x00008EE6
				internal TableRowDeletedModifiedNotification(Notification.TableModifiedNotificationType tableModifiedNotificationType, StoreId folderId, StoreId messageId, uint instance, bool isInSearchFolder) : base(tableModifiedNotificationType)
				{
					this.tableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(folderId, messageId, instance);
					this.IsInSearchFolder = isInSearchFolder;
				}

				// Token: 0x060002C8 RID: 712 RVA: 0x0000AD06 File Offset: 0x00008F06
				internal TableRowDeletedModifiedNotification(Notification.TableModifiedNotificationType tableModifiedNotificationType, StoreId folderId) : base(tableModifiedNotificationType)
				{
					this.tableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(folderId);
				}

				// Token: 0x060002C9 RID: 713 RVA: 0x0000AD1B File Offset: 0x00008F1B
				internal TableRowDeletedModifiedNotification(Reader reader, Notification.NotificationModifiers modifiers, Notification.TableModifiedNotificationType tableModifiedNotificationType) : base(reader, modifiers, tableModifiedNotificationType)
				{
					this.tableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(reader, modifiers);
					this.IsInSearchFolder = ((ushort)(modifiers & Notification.NotificationModifiers.SearchFolder) != 0);
				}

				// Token: 0x170000A2 RID: 162
				// (get) Token: 0x060002CA RID: 714 RVA: 0x0000AD47 File Offset: 0x00008F47
				public StoreId FolderId
				{
					get
					{
						return this.tableModifiedNotificationContext.FolderId;
					}
				}

				// Token: 0x170000A3 RID: 163
				// (get) Token: 0x060002CB RID: 715 RVA: 0x0000AD54 File Offset: 0x00008F54
				public StoreId? MessageId
				{
					get
					{
						return this.tableModifiedNotificationContext.MessageId;
					}
				}

				// Token: 0x170000A4 RID: 164
				// (get) Token: 0x060002CC RID: 716 RVA: 0x0000AD61 File Offset: 0x00008F61
				// (set) Token: 0x060002CD RID: 717 RVA: 0x0000AD69 File Offset: 0x00008F69
				internal bool IsInSearchFolder { get; set; }

				// Token: 0x060002CE RID: 718 RVA: 0x0000AD72 File Offset: 0x00008F72
				protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
				{
					base.InternalSerialize(writer, modifiers, string8Encoding);
					this.tableModifiedNotificationContext.Serialize(writer);
				}

				// Token: 0x060002CF RID: 719 RVA: 0x0000AD89 File Offset: 0x00008F89
				protected override Notification.NotificationModifiers GetModifiers()
				{
					return this.tableModifiedNotificationContext.GetModifiers(base.GetModifiers(), this.IsInSearchFolder);
				}

				// Token: 0x060002D0 RID: 720 RVA: 0x0000ADA2 File Offset: 0x00008FA2
				protected override void AppendToString(StringBuilder sb)
				{
					base.AppendToString(sb);
					sb.Append(" Row");
					this.tableModifiedNotificationContext.AppendToString(sb);
				}

				// Token: 0x04000168 RID: 360
				private readonly Notification.TableModifiedNotification.TableModifiedNotificationContext tableModifiedNotificationContext;
			}

			// Token: 0x0200006A RID: 106
			internal sealed class TableRowDeletedExtendedNotification : Notification.TableModifiedNotification.TableRowDeletedModifiedNotification
			{
				// Token: 0x060002D1 RID: 721 RVA: 0x0000ADC3 File Offset: 0x00008FC3
				internal TableRowDeletedExtendedNotification(Notification.TableModifiedNotificationType tableModifiedNotificationType, StoreId folderId, StoreId messageId, uint instance, PropertyValue[] propertyValues, bool isInSearchFolder) : base(tableModifiedNotificationType, folderId, messageId, instance, isInSearchFolder)
				{
					this.propertyValues = propertyValues;
				}

				// Token: 0x060002D2 RID: 722 RVA: 0x0000ADDA File Offset: 0x00008FDA
				internal TableRowDeletedExtendedNotification(Reader reader, Notification.NotificationModifiers modifiers, Notification.TableModifiedNotificationType tableModifiedNotificationType, Encoding string8Encoding) : base(reader, modifiers, tableModifiedNotificationType)
				{
					this.propertyValues = reader.ReadCountAndPropertyValueList(string8Encoding, WireFormatStyle.Rop);
				}

				// Token: 0x170000A5 RID: 165
				// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000ADF4 File Offset: 0x00008FF4
				internal PropertyValue[] PropertyValues
				{
					get
					{
						return this.propertyValues;
					}
				}

				// Token: 0x060002D4 RID: 724 RVA: 0x0000ADFC File Offset: 0x00008FFC
				protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
				{
					base.InternalSerialize(writer, modifiers, string8Encoding);
					writer.WriteCountAndPropertyValueList(this.propertyValues, string8Encoding, WireFormatStyle.Rop);
				}

				// Token: 0x060002D5 RID: 725 RVA: 0x0000AE18 File Offset: 0x00009018
				protected override void AppendToString(StringBuilder sb)
				{
					base.AppendToString(sb);
					sb.Append(" Properties: ");
					for (int i = 0; i < this.propertyValues.Length; i++)
					{
						if (i != 0)
						{
							sb.Append(" ");
						}
						sb.Append("[");
						this.propertyValues[i].AppendToString(sb);
						sb.Append("]");
					}
				}

				// Token: 0x0400016A RID: 362
				private PropertyValue[] propertyValues;
			}

			// Token: 0x0200006B RID: 107
			internal sealed class TableRowAddModifiedNotification : Notification.TableModifiedNotification
			{
				// Token: 0x060002D6 RID: 726 RVA: 0x0000AE84 File Offset: 0x00009084
				public TableRowAddModifiedNotification(Notification.TableModifiedNotificationType tableModifiedNotificationType, StoreId folderId, StoreId messageId, uint instance, StoreId insertAfterFolderId, StoreId insertAfterMessageId, uint insertAfterInstance, PropertyTag[] originalPropertyTags, PropertyValue[] propertyValues, bool isInSearchFolder) : base(tableModifiedNotificationType)
				{
					this.tableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(folderId, messageId, instance);
					this.insertAfterTableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(insertAfterFolderId, insertAfterMessageId, insertAfterInstance);
					this.SetPropertyRow(originalPropertyTags, propertyValues);
					this.IsInSearchFolder = isInSearchFolder;
				}

				// Token: 0x060002D7 RID: 727 RVA: 0x0000AEBF File Offset: 0x000090BF
				public TableRowAddModifiedNotification(Notification.TableModifiedNotificationType tableModifiedNotificationType, StoreId folderId, StoreId insertAfterFolderId, PropertyTag[] originalPropertyTags, PropertyValue[] propertyValues) : base(tableModifiedNotificationType)
				{
					this.tableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(folderId);
					this.insertAfterTableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(insertAfterFolderId);
					this.SetPropertyRow(originalPropertyTags, propertyValues);
				}

				// Token: 0x060002D8 RID: 728 RVA: 0x0000AEEA File Offset: 0x000090EA
				private void SetPropertyRow(PropertyTag[] originalPropertyTags, PropertyValue[] propertyValues)
				{
					if (originalPropertyTags == null)
					{
						throw new ArgumentNullException("originalPropertyTags cannot be null.");
					}
					if (propertyValues == null)
					{
						throw new ArgumentNullException("propertyValues cannot be null.");
					}
					this.propertyTags = originalPropertyTags;
					this.propertyRow = new PropertyRow(originalPropertyTags, propertyValues);
				}

				// Token: 0x060002D9 RID: 729 RVA: 0x0000AF1C File Offset: 0x0000911C
				internal TableRowAddModifiedNotification(Reader reader, Notification.NotificationModifiers modifiers, Notification.TableModifiedNotificationType tableModifiedNotificationType, PropertyTag[] propertyTags, Encoding string8Encoding) : base(reader, modifiers, tableModifiedNotificationType)
				{
					this.tableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(reader, modifiers);
					this.insertAfterTableModifiedNotificationContext = new Notification.TableModifiedNotification.TableModifiedNotificationContext(reader, modifiers);
					ushort count = reader.ReadUInt16();
					this.IsInSearchFolder = ((ushort)(modifiers & Notification.NotificationModifiers.SearchFolder) != 0);
					this.propertyTags = propertyTags;
					if (this.propertyTags == null)
					{
						reader.ReadArraySegment((uint)count);
						this.propertyRow = PropertyRow.Empty;
						return;
					}
					this.propertyRow = PropertyRow.Parse(reader, propertyTags, WireFormatStyle.Rop);
					this.propertyRow.ResolveString8Values(string8Encoding);
				}

				// Token: 0x170000A6 RID: 166
				// (get) Token: 0x060002DA RID: 730 RVA: 0x0000AFA7 File Offset: 0x000091A7
				internal PropertyTag[] PropertyTags
				{
					get
					{
						return this.propertyTags;
					}
				}

				// Token: 0x170000A7 RID: 167
				// (get) Token: 0x060002DB RID: 731 RVA: 0x0000AFAF File Offset: 0x000091AF
				internal PropertyRow PropertyRow
				{
					get
					{
						return this.propertyRow;
					}
				}

				// Token: 0x170000A8 RID: 168
				// (get) Token: 0x060002DC RID: 732 RVA: 0x0000AFB7 File Offset: 0x000091B7
				// (set) Token: 0x060002DD RID: 733 RVA: 0x0000AFBF File Offset: 0x000091BF
				internal bool IsInSearchFolder { get; set; }

				// Token: 0x060002DE RID: 734 RVA: 0x0000AFC8 File Offset: 0x000091C8
				protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
				{
					base.InternalSerialize(writer, modifiers, string8Encoding);
					this.tableModifiedNotificationContext.Serialize(writer);
					this.insertAfterTableModifiedNotificationContext.Serialize(writer);
					long position = writer.Position;
					writer.WriteUInt16(0);
					long position2 = writer.Position;
					this.propertyRow.Serialize(writer, string8Encoding, WireFormatStyle.Rop);
					long position3 = writer.Position;
					writer.Position = position;
					writer.WriteUInt16((ushort)(position3 - position2));
					writer.Position = position3;
				}

				// Token: 0x060002DF RID: 735 RVA: 0x0000B038 File Offset: 0x00009238
				protected override Notification.NotificationModifiers GetModifiers()
				{
					return this.tableModifiedNotificationContext.GetModifiers(base.GetModifiers(), this.IsInSearchFolder);
				}

				// Token: 0x060002E0 RID: 736 RVA: 0x0000B054 File Offset: 0x00009254
				protected override void AppendToString(StringBuilder sb)
				{
					base.AppendToString(sb);
					sb.Append(" Row");
					this.tableModifiedNotificationContext.AppendToString(sb);
					sb.Append(" Prev Row");
					this.insertAfterTableModifiedNotificationContext.AppendToString(sb);
					sb.AppendFormat(" Properties [{0}]", this.propertyRow.ToString());
				}

				// Token: 0x0400016B RID: 363
				private readonly Notification.TableModifiedNotification.TableModifiedNotificationContext tableModifiedNotificationContext;

				// Token: 0x0400016C RID: 364
				private readonly Notification.TableModifiedNotification.TableModifiedNotificationContext insertAfterTableModifiedNotificationContext;

				// Token: 0x0400016D RID: 365
				private PropertyTag[] propertyTags;

				// Token: 0x0400016E RID: 366
				private PropertyRow propertyRow;
			}
		}

		// Token: 0x0200006C RID: 108
		internal abstract class ObjectNotification : Notification
		{
			// Token: 0x060002E1 RID: 737 RVA: 0x0000B0B5 File Offset: 0x000092B5
			protected ObjectNotification(Notification.NotificationModifiers notificationType, StoreId folderId, StoreId? messageId) : base(notificationType)
			{
				this.folderId = folderId;
				this.messageId = messageId;
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000B0CC File Offset: 0x000092CC
			protected ObjectNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(modifiers & Notification.NotificationModifiers.NotificationTypeMask)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}
				this.folderId = StoreId.Parse(reader);
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) != 0)
				{
					this.messageId = new StoreId?(StoreId.Parse(reader));
				}
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000B11C File Offset: 0x0000931C
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				this.folderId.Serialize(writer);
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) != 0)
				{
					this.messageId.Value.Serialize(writer);
				}
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000B15C File Offset: 0x0000935C
			protected override Notification.NotificationModifiers GetModifiers()
			{
				Notification.NotificationModifiers notificationModifiers = base.GetModifiers();
				if (this.messageId != null)
				{
					notificationModifiers |= Notification.NotificationModifiers.Message;
				}
				return notificationModifiers;
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000B18C File Offset: 0x0000938C
			protected override void AppendToString(StringBuilder sb)
			{
				base.AppendToString(sb);
				sb.AppendFormat(" FID [{0}]", this.FolderId);
				if (this.MessageId != null)
				{
					sb.AppendFormat(" MID [{0}]", this.MessageId);
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000B1DE File Offset: 0x000093DE
			public StoreId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B1E6 File Offset: 0x000093E6
			public StoreId? MessageId
			{
				get
				{
					return this.messageId;
				}
			}

			// Token: 0x04000170 RID: 368
			private readonly StoreId folderId;

			// Token: 0x04000171 RID: 369
			private readonly StoreId? messageId;
		}

		// Token: 0x0200006D RID: 109
		internal sealed class SearchCompleteNotification : Notification.ObjectNotification
		{
			// Token: 0x060002E8 RID: 744 RVA: 0x0000B1F0 File Offset: 0x000093F0
			public SearchCompleteNotification(StoreId folderId) : base(Notification.NotificationModifiers.SearchComplete, folderId, null)
			{
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000B212 File Offset: 0x00009412
			internal SearchCompleteNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 128)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
			}
		}

		// Token: 0x0200006E RID: 110
		internal sealed class NewMailNotification : Notification.ObjectNotification
		{
			// Token: 0x060002EA RID: 746 RVA: 0x0000B23B File Offset: 0x0000943B
			public NewMailNotification(StoreId folderId, StoreId messageId, uint messageFlags, string messageClass) : base(Notification.NotificationModifiers.NewMail, folderId, new StoreId?(messageId))
			{
				if (messageClass == null)
				{
					throw new ArgumentNullException("Message class cannot be null.", "messageClass");
				}
				this.messageFlags = messageFlags;
				this.messageClass = messageClass;
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000B270 File Offset: 0x00009470
			internal NewMailNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 2)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) == 0 || base.MessageId == null)
				{
					throw new BufferParseException("Message ID is not present in NewMail notification.");
				}
				this.messageFlags = reader.ReadUInt32();
				bool flag = reader.ReadBool();
				if (flag)
				{
					this.messageClass = reader.ReadUnicodeString(StringFlags.IncludeNull);
					return;
				}
				this.messageClass = reader.ReadAsciiString(StringFlags.IncludeNull);
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000B2F6 File Offset: 0x000094F6
			protected override Notification.NotificationModifiers GetModifiers()
			{
				return base.GetModifiers() | Notification.NotificationModifiers.Message;
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000B305 File Offset: 0x00009505
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				base.InternalSerialize(writer, modifiers, string8Encoding);
				writer.WriteUInt32(this.messageFlags);
				writer.WriteBool(false);
				writer.WriteAsciiString(this.messageClass, StringFlags.IncludeNull);
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000B330 File Offset: 0x00009530
			protected override void AppendToString(StringBuilder sb)
			{
				base.AppendToString(sb);
				sb.AppendFormat(" MsgFlags [{0}] MsgClass [{1}]", this.messageFlags, this.messageClass);
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B356 File Offset: 0x00009556
			public string MessageClass
			{
				get
				{
					return this.messageClass;
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000B35E File Offset: 0x0000955E
			public MessageFlags MessageFlags
			{
				get
				{
					return (MessageFlags)this.messageFlags;
				}
			}

			// Token: 0x04000172 RID: 370
			private readonly uint messageFlags;

			// Token: 0x04000173 RID: 371
			private readonly string messageClass;
		}

		// Token: 0x0200006F RID: 111
		internal abstract class WithParentFolderNotification : Notification.ObjectNotification
		{
			// Token: 0x060002F1 RID: 753 RVA: 0x0000B366 File Offset: 0x00009566
			protected WithParentFolderNotification(Notification.NotificationModifiers notificationType, StoreId folderId, StoreId? messageId, StoreId parentFolderId) : base(notificationType, folderId, messageId)
			{
				this.parentFolderId = parentFolderId;
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000B379 File Offset: 0x00009579
			protected WithParentFolderNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) == 0 || (ushort)(modifiers & Notification.NotificationModifiers.SearchFolder) != 0)
				{
					this.parentFolderId = StoreId.Parse(reader);
					return;
				}
				this.parentFolderId = base.FolderId;
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000B3B0 File Offset: 0x000095B0
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				base.InternalSerialize(writer, modifiers, string8Encoding);
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) == 0 || (ushort)(modifiers & Notification.NotificationModifiers.SearchFolder) != 0)
				{
					this.parentFolderId.Serialize(writer);
				}
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x0000B3EC File Offset: 0x000095EC
			protected override Notification.NotificationModifiers GetModifiers()
			{
				Notification.NotificationModifiers notificationModifiers = base.GetModifiers();
				if ((ushort)(notificationModifiers & Notification.NotificationModifiers.Message) != 0 && !this.parentFolderId.Equals(base.FolderId))
				{
					notificationModifiers |= Notification.NotificationModifiers.SearchFolder;
				}
				return notificationModifiers;
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000B42C File Offset: 0x0000962C
			protected override void AppendToString(StringBuilder sb)
			{
				Notification.NotificationModifiers modifiers = this.GetModifiers();
				base.AppendToString(sb);
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) == 0 || (ushort)(modifiers & Notification.NotificationModifiers.SearchFolder) != 0)
				{
					sb.AppendFormat(" FID [{0}]", this.parentFolderId);
				}
			}

			// Token: 0x04000174 RID: 372
			private readonly StoreId parentFolderId;
		}

		// Token: 0x02000070 RID: 112
		internal sealed class ObjectCreatedNotification : Notification.WithParentFolderNotification
		{
			// Token: 0x060002F6 RID: 758 RVA: 0x0000B472 File Offset: 0x00009672
			public ObjectCreatedNotification(StoreId folderId, StoreId? messageId, StoreId parentFolderId, PropertyTag[] propertyTags) : base(Notification.NotificationModifiers.ObjectCreated, folderId, messageId, parentFolderId)
			{
				if (propertyTags == null)
				{
					throw new ArgumentNullException("propertyTags");
				}
				this.propertyTags = propertyTags;
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000B495 File Offset: 0x00009695
			internal ObjectCreatedNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 4)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
				this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000B4C7 File Offset: 0x000096C7
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				base.InternalSerialize(writer, modifiers, string8Encoding);
				writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000B4E0 File Offset: 0x000096E0
			protected override void AppendToString(StringBuilder sb)
			{
				base.AppendToString(sb);
				sb.Append(" PropTags [");
				for (int i = 0; i < this.propertyTags.Length; i++)
				{
					sb.AppendFormat(" {0}", this.propertyTags[i].ToString());
				}
				sb.Append(" ]");
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060002FA RID: 762 RVA: 0x0000B542 File Offset: 0x00009742
			internal PropertyTag[] PropertyTags
			{
				get
				{
					return this.propertyTags;
				}
			}

			// Token: 0x04000175 RID: 373
			private readonly PropertyTag[] propertyTags;
		}

		// Token: 0x02000071 RID: 113
		internal sealed class ObjectDeletedNotification : Notification.WithParentFolderNotification
		{
			// Token: 0x060002FB RID: 763 RVA: 0x0000B54A File Offset: 0x0000974A
			public ObjectDeletedNotification(StoreId folderId, StoreId? messageId, StoreId parentFolderId) : base(Notification.NotificationModifiers.ObjectDeleted, folderId, messageId, parentFolderId)
			{
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000B556 File Offset: 0x00009756
			internal ObjectDeletedNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 8)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
			}
		}

		// Token: 0x02000072 RID: 114
		internal sealed class ObjectModifiedNotification : Notification.ObjectNotification
		{
			// Token: 0x060002FD RID: 765 RVA: 0x0000B57C File Offset: 0x0000977C
			public ObjectModifiedNotification(StoreId folderId, StoreId? messageId, PropertyTag[] propertyTags, int totalItemsChanged, int unreadItemsChanged) : base(Notification.NotificationModifiers.ObjectModified, folderId, messageId)
			{
				if (propertyTags == null)
				{
					throw new ArgumentNullException("propertyTags");
				}
				if (messageId != null && totalItemsChanged != -1)
				{
					throw new ArgumentException("The value of totalItemsChanged must be -1 for message notifications", "totalItemsChanged");
				}
				if (messageId != null && unreadItemsChanged != -1)
				{
					throw new ArgumentException("The value of unreadItemsChanged must be -1 for message notifications", "unreadItemsChanged");
				}
				this.propertyTags = propertyTags;
				this.totalItemsChanged = totalItemsChanged;
				this.unreadItemsChanged = unreadItemsChanged;
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0000B5F4 File Offset: 0x000097F4
			internal ObjectModifiedNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 16)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
				this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
				this.totalItemsChanged = (((ushort)(modifiers & Notification.NotificationModifiers.TotalItemsChanged) != 0) ? reader.ReadInt32() : -1);
				this.unreadItemsChanged = (((ushort)(modifiers & Notification.NotificationModifiers.UnreadItemsChanged) != 0) ? reader.ReadInt32() : -1);
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000B664 File Offset: 0x00009864
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				base.InternalSerialize(writer, modifiers, string8Encoding);
				writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
				if ((ushort)(modifiers & Notification.NotificationModifiers.TotalItemsChanged) != 0)
				{
					writer.WriteInt32(this.totalItemsChanged);
				}
				if ((ushort)(modifiers & Notification.NotificationModifiers.UnreadItemsChanged) != 0)
				{
					writer.WriteInt32(this.unreadItemsChanged);
				}
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000B6B4 File Offset: 0x000098B4
			protected override Notification.NotificationModifiers GetModifiers()
			{
				Notification.NotificationModifiers notificationModifiers = base.GetModifiers();
				if (this.totalItemsChanged != -1)
				{
					notificationModifiers |= Notification.NotificationModifiers.TotalItemsChanged;
				}
				if (this.unreadItemsChanged != -1)
				{
					notificationModifiers |= Notification.NotificationModifiers.UnreadItemsChanged;
				}
				return notificationModifiers;
			}

			// Token: 0x06000301 RID: 769 RVA: 0x0000B6F0 File Offset: 0x000098F0
			protected override void AppendToString(StringBuilder sb)
			{
				base.AppendToString(sb);
				sb.Append(" PropTags [");
				for (int i = 0; i < this.propertyTags.Length; i++)
				{
					sb.AppendFormat(" {0}", this.propertyTags[i].ToString());
				}
				sb.Append(" ]");
				if (this.totalItemsChanged != -1)
				{
					sb.AppendFormat(" Changed [{0}]", this.totalItemsChanged);
				}
				if (this.unreadItemsChanged != -1)
				{
					sb.AppendFormat(" Unread [{0}]", this.unreadItemsChanged);
				}
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B792 File Offset: 0x00009992
			public PropertyTag[] PropertyTags
			{
				get
				{
					return this.propertyTags;
				}
			}

			// Token: 0x04000176 RID: 374
			private readonly PropertyTag[] propertyTags;

			// Token: 0x04000177 RID: 375
			private readonly int totalItemsChanged;

			// Token: 0x04000178 RID: 376
			private readonly int unreadItemsChanged;
		}

		// Token: 0x02000073 RID: 115
		internal abstract class WithOldItemIdNotification : Notification.WithParentFolderNotification
		{
			// Token: 0x06000303 RID: 771 RVA: 0x0000B79C File Offset: 0x0000999C
			protected WithOldItemIdNotification(Notification.NotificationModifiers notificationType, StoreId folderId, StoreId? messageId, StoreId parentFolderId, StoreId oldFolderId, StoreId? oldMessageId, StoreId oldParentFolderId) : base(notificationType, folderId, messageId, parentFolderId)
			{
				if (messageId != null != (oldMessageId != null))
				{
					throw new ArgumentException("Message ID or old message ID parameters are invalid. The parameters must be either both null or both non-null.");
				}
				if (oldMessageId != null && !oldParentFolderId.Equals(oldFolderId))
				{
					throw new ArgumentException("The value of oldParentFolderId must be the same as the value of oldFolderId.", "oldParentFolderId");
				}
				this.oldFolderId = oldFolderId;
				this.oldMessageId = oldMessageId;
				this.oldParentFolderId = oldParentFolderId;
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000B80C File Offset: 0x00009A0C
			protected WithOldItemIdNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				this.oldFolderId = StoreId.Parse(reader);
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) != 0)
				{
					this.oldMessageId = new StoreId?(StoreId.Parse(reader));
					this.oldParentFolderId = this.oldFolderId;
					return;
				}
				this.oldParentFolderId = StoreId.Parse(reader);
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000305 RID: 773 RVA: 0x0000B861 File Offset: 0x00009A61
			public StoreId OldFolderId
			{
				get
				{
					return this.oldFolderId;
				}
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000306 RID: 774 RVA: 0x0000B869 File Offset: 0x00009A69
			public StoreId? OldMessageId
			{
				get
				{
					return this.oldMessageId;
				}
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000B874 File Offset: 0x00009A74
			protected override void InternalSerialize(Writer writer, Notification.NotificationModifiers modifiers, Encoding string8Encoding)
			{
				base.InternalSerialize(writer, modifiers, string8Encoding);
				this.oldFolderId.Serialize(writer);
				if ((ushort)(modifiers & Notification.NotificationModifiers.Message) != 0)
				{
					this.oldMessageId.Value.Serialize(writer);
					return;
				}
				this.oldParentFolderId.Serialize(writer);
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000B8CC File Offset: 0x00009ACC
			protected override void AppendToString(StringBuilder sb)
			{
				base.AppendToString(sb);
				sb.AppendFormat(" Old FID [{0}]", this.oldFolderId);
				if (this.oldMessageId != null)
				{
					sb.AppendFormat(" Old MID [{0}]", this.oldMessageId.Value);
					return;
				}
				sb.AppendFormat(" Old Parent FID [{0}]", this.oldParentFolderId.ToString());
			}

			// Token: 0x04000179 RID: 377
			private readonly StoreId oldFolderId;

			// Token: 0x0400017A RID: 378
			private readonly StoreId? oldMessageId;

			// Token: 0x0400017B RID: 379
			private readonly StoreId oldParentFolderId;
		}

		// Token: 0x02000074 RID: 116
		internal sealed class ObjectMovedNotification : Notification.WithOldItemIdNotification
		{
			// Token: 0x06000309 RID: 777 RVA: 0x0000B947 File Offset: 0x00009B47
			public ObjectMovedNotification(StoreId folderId, StoreId? messageId, StoreId parentFolderId, StoreId oldFolderId, StoreId? oldMessageId, StoreId oldParentFolderId) : base(Notification.NotificationModifiers.ObjectMoved, folderId, messageId, parentFolderId, oldFolderId, oldMessageId, oldParentFolderId)
			{
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000B95A File Offset: 0x00009B5A
			internal ObjectMovedNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 32)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
			}
		}

		// Token: 0x02000075 RID: 117
		internal sealed class ObjectCopiedNotification : Notification.WithOldItemIdNotification
		{
			// Token: 0x0600030B RID: 779 RVA: 0x0000B980 File Offset: 0x00009B80
			public ObjectCopiedNotification(StoreId folderId, StoreId? messageId, StoreId parentFolderId, StoreId oldFolderId, StoreId? oldMessageId, StoreId oldParentFolderId) : base(Notification.NotificationModifiers.ObjectCopied, folderId, messageId, parentFolderId, oldFolderId, oldMessageId, oldParentFolderId)
			{
			}

			// Token: 0x0600030C RID: 780 RVA: 0x0000B993 File Offset: 0x00009B93
			internal ObjectCopiedNotification(Reader reader, Notification.NotificationModifiers modifiers) : base(reader, modifiers)
			{
				if ((ushort)(modifiers & Notification.NotificationModifiers.NotificationTypeMask) != 64)
				{
					throw new ArgumentException("Invalid notification type.", "modifiers");
				}
			}
		}
	}
}
