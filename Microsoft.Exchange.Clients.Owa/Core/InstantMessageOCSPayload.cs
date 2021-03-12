using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InstantMessaging;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000142 RID: 322
	internal sealed class InstantMessageOCSPayload : InstantMessagePayload
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0004D84F File Offset: 0x0004BA4F
		internal InstantMessageOCSPayload(UserContext userContext) : base(userContext)
		{
			this.deliverySuccessNotifications = new List<InstantMessageOCSPayload.DeliverySuccessNotification>();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0004D864 File Offset: 0x0004BA64
		internal void RegisterDeliverySuccessNotification(IIMModality context, int messageId)
		{
			lock (this.deliverySuccessNotifications)
			{
				this.deliverySuccessNotifications.Add(new InstantMessageOCSPayload.DeliverySuccessNotification(context, messageId));
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
		public override string ReadDataAndResetState()
		{
			string result = base.ReadDataAndResetState();
			lock (this.deliverySuccessNotifications)
			{
				foreach (InstantMessageOCSPayload.DeliverySuccessNotification deliverySuccessNotification in this.deliverySuccessNotifications)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug<int>((long)this.GetHashCode(), "InstantMessageOCSPayload.ReadDataAndResetState. BeginNotifyDeliverySuccess Message Id: {0}", deliverySuccessNotification.MessageId);
					deliverySuccessNotification.Context.BeginNotifyDeliverySuccess(deliverySuccessNotification.MessageId, new AsyncCallback(this.NotifyDeliverySuccessCallback), deliverySuccessNotification.Context);
				}
				this.deliverySuccessNotifications.Clear();
			}
			return result;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0004D980 File Offset: 0x0004BB80
		protected override void Cancel()
		{
			base.Cancel();
			lock (this.deliverySuccessNotifications)
			{
				this.deliverySuccessNotifications.Clear();
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0004D9CC File Offset: 0x0004BBCC
		private void NotifyDeliverySuccessCallback(IAsyncResult result)
		{
			IIMModality iimmodality = null;
			try
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug((long)this.GetHashCode(), "InstantMessageOCSPayload.NotifyDeliverySuccessCallback.");
				iimmodality = (result.AsyncState as IIMModality);
				if (iimmodality == null)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceError((long)this.GetHashCode(), "InstantMessageOCSPayload.NotifyDeliverySuccessCallback. instantMessaging is null.");
				}
				else
				{
					iimmodality.EndNotifyDeliverySuccess(result);
				}
			}
			catch (InstantMessagingException ex)
			{
				if (!iimmodality.IsConnected)
				{
					ExTraceGlobals.InstantMessagingTracer.TraceDebug<InstantMessagingException>((long)this.GetHashCode(), "InstantMessageOCSPayload.NotifyDeliverySuccessCallback. Ignoring exception because IM conversation is not connected : {0}.", ex);
				}
				else
				{
					InstantMessagingError code = ex.Code;
					switch (code)
					{
					case 18102:
						if (ex.SubCode == 9)
						{
							ExTraceGlobals.InstantMessagingTracer.TraceError<InstantMessagingException>((long)this.GetHashCode(), "InstantMessageOCSPayload.NotifyDeliverySuccessCallback. OcsFailureResponse. {0}", ex);
							goto IL_113;
						}
						InstantMessageUtilities.SendWatsonReport("InstantMessageOCSPayload.NotifyDeliverySuccessCallback", this.userContext, ex);
						goto IL_113;
					case 18103:
						break;
					case 18104:
						ExTraceGlobals.InstantMessagingTracer.TraceError<InstantMessagingException>((long)this.GetHashCode(), "InstantMessageOCSPayload.NotifyDeliverySuccessCallback. Failed to send delivery success notification. OcsFailureResponse. {0}", ex);
						goto IL_113;
					default:
						if (code == 18201)
						{
							ExTraceGlobals.InstantMessagingTracer.TraceError<InstantMessagingException>((long)this.GetHashCode(), "InstantMessageOCSPayload.NotifyDeliverySuccessCallback. OcsFailureResponse. {0}", ex);
							goto IL_113;
						}
						break;
					}
					InstantMessageUtilities.SendWatsonReport("InstantMessageOCSPayload.NotifyDeliverySuccessCallback", this.userContext, ex);
				}
				IL_113:;
			}
			catch (Exception exception)
			{
				InstantMessageUtilities.SendWatsonReport("InstantMessageOCSPayload.NotifyDeliverySuccessCallback", this.userContext, exception);
			}
		}

		// Token: 0x040007DE RID: 2014
		private List<InstantMessageOCSPayload.DeliverySuccessNotification> deliverySuccessNotifications;

		// Token: 0x02000143 RID: 323
		private struct DeliverySuccessNotification
		{
			// Token: 0x06000AFA RID: 2810 RVA: 0x0004DB20 File Offset: 0x0004BD20
			internal DeliverySuccessNotification(IIMModality context, int messageId)
			{
				this.Context = context;
				this.MessageId = messageId;
			}

			// Token: 0x040007DF RID: 2015
			internal IIMModality Context;

			// Token: 0x040007E0 RID: 2016
			internal int MessageId;
		}
	}
}
