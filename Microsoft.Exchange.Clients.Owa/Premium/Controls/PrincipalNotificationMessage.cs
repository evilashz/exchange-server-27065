using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003E2 RID: 994
	internal sealed class PrincipalNotificationMessage
	{
		// Token: 0x06002475 RID: 9333 RVA: 0x000D3D64 File Offset: 0x000D1F64
		internal PrincipalNotificationMessage(string itemId, OwaStoreObjectId folderId, UserContext userContext, HttpContext httpContext, PrincipalNotificationMessage.ActionType actionType, bool isNewItem, bool isMeeting)
		{
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			this.itemId = itemId;
			this.folderId = folderId;
			this.userContext = userContext;
			this.httpContext = httpContext;
			this.actionType = actionType;
			this.isNewItem = isNewItem;
			this.isMeeting = isMeeting;
			this.GetMessageAndTitleTypes();
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000D3DEC File Offset: 0x000D1FEC
		internal void SendNotificationMessage()
		{
			string text = this.CapturePrintMeetingMarkup();
			text = string.Format("<div style=\"font-size: 70%; font-family:'{0}'\">{1}</div><br>{2}", Utilities.GetDefaultFontName(), LocalizedStrings.GetHtmlEncoded(PrincipalNotificationMessage.NotificationMessages[(int)this.messageType]), text);
			using (MessageItem messageItem = MessageItem.Create(this.userContext.MailboxSession, this.userContext.DraftsFolderId))
			{
				messageItem.Subject = LocalizedStrings.GetNonEncoded(PrincipalNotificationMessage.NotificationMessageTitles[(int)this.titleType]);
				BodyConversionUtilities.SetBody(messageItem, text, Markup.Html, StoreObjectType.Message, this.userContext);
				messageItem[ItemSchema.ConversationIndexTracking] = true;
				IExchangePrincipal folderOwnerExchangePrincipal = Utilities.GetFolderOwnerExchangePrincipal(this.folderId, this.userContext);
				Participant participant = new Participant(folderOwnerExchangePrincipal);
				messageItem.Recipients.Add(participant, RecipientItemType.To);
				try
				{
					messageItem.SendWithoutSavingMessage();
				}
				catch
				{
					ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Error sending principal notification message.");
					throw;
				}
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsCreated.Increment();
				OwaSingleCounters.MessagesSent.Increment();
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000D3F04 File Offset: 0x000D2104
		private void GetMessageAndTitleTypes()
		{
			switch (this.actionType)
			{
			case PrincipalNotificationMessage.ActionType.Send:
				if (this.isNewItem)
				{
					this.titleType = PrincipalNotificationMessage.TitleType.MeetingCreatedNotification;
					this.messageType = PrincipalNotificationMessage.MessageType.SendNewMeetingRequest;
					return;
				}
				this.titleType = PrincipalNotificationMessage.TitleType.MeetingChangedNotification;
				this.messageType = PrincipalNotificationMessage.MessageType.SendUpdateForExistingMeetingRequest;
				return;
			case PrincipalNotificationMessage.ActionType.Save:
				if (this.isNewItem)
				{
					if (this.isMeeting)
					{
						this.titleType = PrincipalNotificationMessage.TitleType.MeetingCreatedNotification;
						this.messageType = PrincipalNotificationMessage.MessageType.SaveNewMeetingRequest;
						return;
					}
					this.titleType = PrincipalNotificationMessage.TitleType.AppointmentCreatedNotification;
					this.messageType = PrincipalNotificationMessage.MessageType.SaveNewAppointment;
					return;
				}
				else
				{
					if (this.isMeeting)
					{
						this.titleType = PrincipalNotificationMessage.TitleType.MeetingChangedNotification;
						this.messageType = PrincipalNotificationMessage.MessageType.SaveExistingMeetingRequest;
						return;
					}
					this.titleType = PrincipalNotificationMessage.TitleType.AppointmentChangedNotification;
					this.messageType = PrincipalNotificationMessage.MessageType.SaveExistingAppointment;
					return;
				}
				break;
			case PrincipalNotificationMessage.ActionType.Delete:
				if (!this.isMeeting)
				{
					this.titleType = PrincipalNotificationMessage.TitleType.AppointmentCancellationNotification;
					this.messageType = PrincipalNotificationMessage.MessageType.DeleteAppointment;
					return;
				}
				break;
			case PrincipalNotificationMessage.ActionType.Cancel:
				this.titleType = PrincipalNotificationMessage.TitleType.MeetingCancellationNotification;
				this.messageType = PrincipalNotificationMessage.MessageType.CancelMeetingRequest;
				return;
			case PrincipalNotificationMessage.ActionType.Move:
				if (this.isMeeting)
				{
					this.titleType = PrincipalNotificationMessage.TitleType.MeetingChangedNotification;
					this.messageType = PrincipalNotificationMessage.MessageType.MoveMeetingRequest;
					return;
				}
				this.titleType = PrincipalNotificationMessage.TitleType.AppointmentChangedNotification;
				this.messageType = PrincipalNotificationMessage.MessageType.MoveAppointment;
				break;
			default:
				return;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000D3FFC File Offset: 0x000D21FC
		private string CapturePrintMeetingMarkup()
		{
			string path = string.Format(CultureInfo.InvariantCulture, "forms/premium/PrintMeetingPage.aspx?ae=Item&t=IPM.Appointment&id={0}", new object[]
			{
				Utilities.UrlEncode(this.itemId)
			});
			StringBuilder sb = new StringBuilder();
			string result;
			using (StringWriter stringWriter = new StringWriter(sb, CultureInfo.InvariantCulture))
			{
				this.httpContext.Server.Execute(path, stringWriter);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x04001944 RID: 6468
		private static readonly Strings.IDs[] NotificationMessageTitles = new Strings.IDs[]
		{
			-103948802,
			1718903530,
			1474482155,
			904403258,
			1104105618,
			900208633
		};

		// Token: 0x04001945 RID: 6469
		private static readonly Strings.IDs[] NotificationMessages = new Strings.IDs[]
		{
			1759358345,
			-737562370,
			887581567,
			-1622812136,
			1378908170,
			-1597738953,
			-516697337,
			-653709769,
			987522774,
			-1700473406
		};

		// Token: 0x04001946 RID: 6470
		private string itemId;

		// Token: 0x04001947 RID: 6471
		private OwaStoreObjectId folderId;

		// Token: 0x04001948 RID: 6472
		private UserContext userContext;

		// Token: 0x04001949 RID: 6473
		private HttpContext httpContext;

		// Token: 0x0400194A RID: 6474
		private PrincipalNotificationMessage.ActionType actionType;

		// Token: 0x0400194B RID: 6475
		private bool isNewItem;

		// Token: 0x0400194C RID: 6476
		private bool isMeeting;

		// Token: 0x0400194D RID: 6477
		private PrincipalNotificationMessage.TitleType titleType;

		// Token: 0x0400194E RID: 6478
		private PrincipalNotificationMessage.MessageType messageType;

		// Token: 0x020003E3 RID: 995
		public enum TitleType
		{
			// Token: 0x04001950 RID: 6480
			MeetingChangedNotification,
			// Token: 0x04001951 RID: 6481
			MeetingCreatedNotification,
			// Token: 0x04001952 RID: 6482
			MeetingCancellationNotification,
			// Token: 0x04001953 RID: 6483
			AppointmentChangedNotification,
			// Token: 0x04001954 RID: 6484
			AppointmentCreatedNotification,
			// Token: 0x04001955 RID: 6485
			AppointmentCancellationNotification
		}

		// Token: 0x020003E4 RID: 996
		public enum MessageType
		{
			// Token: 0x04001957 RID: 6487
			SendNewMeetingRequest,
			// Token: 0x04001958 RID: 6488
			SendUpdateForExistingMeetingRequest,
			// Token: 0x04001959 RID: 6489
			SaveNewAppointment,
			// Token: 0x0400195A RID: 6490
			SaveNewMeetingRequest,
			// Token: 0x0400195B RID: 6491
			SaveExistingAppointment,
			// Token: 0x0400195C RID: 6492
			SaveExistingMeetingRequest,
			// Token: 0x0400195D RID: 6493
			DeleteAppointment,
			// Token: 0x0400195E RID: 6494
			CancelMeetingRequest,
			// Token: 0x0400195F RID: 6495
			MoveMeetingRequest,
			// Token: 0x04001960 RID: 6496
			MoveAppointment
		}

		// Token: 0x020003E5 RID: 997
		public enum ActionType
		{
			// Token: 0x04001962 RID: 6498
			Send,
			// Token: 0x04001963 RID: 6499
			Save,
			// Token: 0x04001964 RID: 6500
			Delete,
			// Token: 0x04001965 RID: 6501
			Cancel,
			// Token: 0x04001966 RID: 6502
			Move
		}
	}
}
