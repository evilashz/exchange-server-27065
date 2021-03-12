using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000025 RID: 37
	internal class ApnsFeedbackAppFile : ApnsFeedbackFileBase
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00005F80 File Offset: 0x00004180
		internal ApnsFeedbackAppFile(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO) : this(identifier, fileIO, ExTraceGlobals.PublisherManagerTracer)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005F8F File Offset: 0x0000418F
		internal ApnsFeedbackAppFile(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO, ITracer tracer) : base(identifier, fileIO, tracer)
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005F9A File Offset: 0x0000419A
		public override bool IsLoaded
		{
			get
			{
				return this.Feedback != null;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005FA8 File Offset: 0x000041A8
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005FB0 File Offset: 0x000041B0
		private Dictionary<string, ApnsFeedbackResponse> Feedback { get; set; }

		// Token: 0x06000177 RID: 375 RVA: 0x00005FBC File Offset: 0x000041BC
		public override ApnsFeedbackValidationResult ValidateNotification(ApnsNotification notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (!this.IsLoaded)
			{
				base.Tracer.TraceDebug<ApnsNotification, ApnsFeedbackFileId>((long)this.GetHashCode(), "[ValidateNotification] Feedback app file not loaded, defaulting to Uncertain for {0}, {1}.", notification, base.Identifier);
				return ApnsFeedbackValidationResult.Uncertain;
			}
			ApnsFeedbackResponse apnsFeedbackResponse;
			if (this.Feedback.TryGetValue(notification.DeviceToken, out apnsFeedbackResponse))
			{
				if (apnsFeedbackResponse.TimeStamp > (ExDateTime)notification.LastSubscriptionUpdate)
				{
					return ApnsFeedbackValidationResult.Expired;
				}
				base.Tracer.TraceDebug((long)this.GetHashCode(), "[ValidateNotification] '{0}' is valid because its last subscription update '{1}' is higher than the timestamp '{3}' for feedback '{4}'", new object[]
				{
					notification,
					notification.LastSubscriptionUpdate,
					apnsFeedbackResponse.TimeStamp,
					base.Identifier
				});
			}
			else
			{
				base.Tracer.TraceDebug<ApnsNotification, ApnsFeedbackFileId>((long)this.GetHashCode(), "[ValidateNotification] '{0}' is valid because its device token is not part of the feedback '{1}'", notification, base.Identifier);
			}
			return ApnsFeedbackValidationResult.Valid;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006094 File Offset: 0x00004294
		public override void Load()
		{
			if (this.IsLoaded)
			{
				return;
			}
			Dictionary<string, ApnsFeedbackResponse> dictionary = new Dictionary<string, ApnsFeedbackResponse>();
			Exception ex = null;
			base.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[Load] Loading APNs Feedback Responses from '{0}'", base.Identifier);
			try
			{
				using (StreamReader fileReader = base.FileIO.GetFileReader(base.Identifier.ToString()))
				{
					string text;
					while ((text = fileReader.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(text))
						{
							base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[Load] Parsing APNs Feedback Response '{0}'", text);
							ApnsFeedbackResponse apnsFeedbackResponse = ApnsFeedbackResponse.FromFeedbackFileEntry(text);
							dictionary.Add(apnsFeedbackResponse.Token, apnsFeedbackResponse);
						}
						else
						{
							base.Tracer.TraceWarning((long)this.GetHashCode(), "[Load] Unexpected empty line");
						}
					}
					base.Tracer.TraceDebug((long)this.GetHashCode(), "[Load] Done loading.");
				}
				this.Feedback = dictionary;
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackFileLoadError(base.Identifier.ToString(), ex.Message), ex);
			}
		}
	}
}
