using System;
using System.IO;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000214 RID: 532
	internal class XsoEmailBodyContentProperty : XsoContentProperty, IBodyContentProperty, IBodyProperty, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x0007631A File Offset: 0x0007451A
		public XsoEmailBodyContentProperty(PropertyType propertyType = PropertyType.ReadOnly) : base(propertyType)
		{
			this.xsoBodyProperty = new XsoBodyProperty(propertyType);
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0007632F File Offset: 0x0007452F
		public Stream RtfData
		{
			get
			{
				return this.xsoBodyProperty.RtfData;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0007633C File Offset: 0x0007453C
		public int RtfSize
		{
			get
			{
				return this.xsoBodyProperty.RtfSize;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00076349 File Offset: 0x00074549
		public Stream TextData
		{
			get
			{
				return this.xsoBodyProperty.TextData;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00076356 File Offset: 0x00074556
		public bool TextPresent
		{
			get
			{
				return this.xsoBodyProperty.TextPresent;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00076363 File Offset: 0x00074563
		public int TextSize
		{
			get
			{
				return this.xsoBodyProperty.TextSize;
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00076370 File Offset: 0x00074570
		public Stream GetTextData(int length)
		{
			return this.xsoBodyProperty.GetTextData(length);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0007637E File Offset: 0x0007457E
		public override void Bind(StoreObject item)
		{
			base.Bind(item);
			if (this.xsoBodyProperty != null)
			{
				this.xsoBodyProperty.Bind(item);
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0007639B File Offset: 0x0007459B
		public override void Unbind()
		{
			if (this.xsoBodyProperty != null)
			{
				this.xsoBodyProperty.Unbind();
			}
			base.Unbind();
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000763B8 File Offset: 0x000745B8
		public override void PreProcessProperty()
		{
			if (!this.IsItemDelegated())
			{
				base.PreProcessProperty();
				return;
			}
			this.originalItem = null;
			MessageItem messageItem = this.CreateSubstituteDelegatedMeetingRequestMessage();
			if (messageItem != null)
			{
				this.originalItem = (Item)base.XsoItem;
				base.XsoItem = messageItem;
				this.actualBody = messageItem.Body;
				if (this.xsoBodyProperty != null)
				{
					this.xsoBodyProperty.Unbind();
				}
				this.xsoBodyProperty.Bind(messageItem);
				return;
			}
			throw new ConversionException("Delegated meeting request body could not be converted");
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00076433 File Offset: 0x00074633
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			if (Command.CurrentCommand.Request.Version < 160)
			{
				throw new ConversionException("Email body is a read-only property and should not be set!");
			}
			base.InternalCopyFromModified(srcProperty);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00076460 File Offset: 0x00074660
		private MessageItem CreateSubstituteDelegatedMeetingRequestMessage()
		{
			MessageItem messageItem = null;
			bool flag = false;
			try
			{
				MeetingMessage meetingMessage = base.XsoItem as MeetingMessage;
				messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
				if (messageItem == null)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.XsoTracer, null, "CreateSubstituteDelegatedMeetingRequestMessage failed to create in memory message item");
					return null;
				}
				Item.CopyItemContent(meetingMessage, messageItem);
				messageItem.ClassName = "IPM.Note";
				messageItem.AttachmentCollection.RemoveAll();
				int num = meetingMessage.Body.PreviewText.IndexOf("*~*~*~*~*~*~*~*~*~*");
				string value;
				if (num == -1)
				{
					ExTimeZone promotedTimeZoneFromItem = TimeZoneHelper.GetPromotedTimeZoneFromItem(meetingMessage);
					value = CalendarItemBase.CreateWhenStringForBodyPrefix(meetingMessage, promotedTimeZoneFromItem);
				}
				else
				{
					value = meetingMessage.Body.PreviewText.Substring(0, num + "*~*~*~*~*~*~*~*~*~*".Length);
				}
				using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextPlain))
				{
					textWriter.Write(value);
				}
			}
			catch (Exception ex)
			{
				AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.XsoTracer, null, "CreateSubstituteDelegatedMeetingRequestMessage: exception thrown: {0}", ex.Message);
				if (messageItem != null)
				{
					flag = true;
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
			return messageItem;
		}

		// Token: 0x04000C67 RID: 3175
		private XsoBodyProperty xsoBodyProperty;
	}
}
