using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000028 RID: 40
	internal class ApnsFeedbackManager : IApnsFeedbackProvider
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000667C File Offset: 0x0000487C
		public ApnsFeedbackManager(ApnsFeedbackManagerSettings settings = null) : this(settings ?? new ApnsFeedbackManagerSettings(), ApnsFeedbackScheduler.DefaultScheduler, ApnsFeedbackFileIO.DefaultFileIO, ExTraceGlobals.PublisherManagerTracer)
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000066A0 File Offset: 0x000048A0
		protected ApnsFeedbackManager(ApnsFeedbackManagerSettings settings, ApnsFeedbackScheduler scheduler, ApnsFeedbackFileIO fileIO, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			ArgumentValidator.ThrowIfNegative("settings.UpdateIntervalInMilliseconds", settings.UpdateIntervalInMilliseconds);
			ArgumentValidator.ThrowIfNull("scheduler", scheduler);
			ArgumentValidator.ThrowIfNull("fileIO", fileIO);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.Settings = settings;
			this.Scheduler = scheduler;
			this.FileIO = fileIO;
			this.Tracer = tracer;
			this.CurrentFeedback = new Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile>();
			this.ExpirationThreshold = TimeSpan.FromMilliseconds((double)this.Settings.ExpirationThresholdInMilliseconds);
			this.Scheduler.ScheduleOnce(new Action(this.UpdateFeedback), 10);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006748 File Offset: 0x00004948
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00006750 File Offset: 0x00004950
		private protected Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile> CurrentFeedback { protected get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006759 File Offset: 0x00004959
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00006761 File Offset: 0x00004961
		private ApnsFeedbackManagerSettings Settings { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000676A File Offset: 0x0000496A
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00006772 File Offset: 0x00004972
		private TimeSpan ExpirationThreshold { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000677B File Offset: 0x0000497B
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00006783 File Offset: 0x00004983
		private ApnsFeedbackScheduler Scheduler { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000678C File Offset: 0x0000498C
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00006794 File Offset: 0x00004994
		private ApnsFeedbackFileIO FileIO { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000679D File Offset: 0x0000499D
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x000067A5 File Offset: 0x000049A5
		private ITracer Tracer { get; set; }

		// Token: 0x060001A2 RID: 418 RVA: 0x000067B0 File Offset: 0x000049B0
		public ApnsFeedbackValidationResult ValidateNotification(ApnsNotification notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile> currentFeedback = this.CurrentFeedback;
			if (currentFeedback.Count == 0)
			{
				this.Tracer.TraceDebug<ApnsNotification>((long)this.GetHashCode(), "[ValidateNotification] Feedback is not available, defaulting to Uncertain for '{0}'", notification);
				return ApnsFeedbackValidationResult.Uncertain;
			}
			ApnsFeedbackValidationResult apnsFeedbackValidationResult = ApnsFeedbackValidationResult.Valid;
			foreach (IApnsFeedbackFile apnsFeedbackFile in currentFeedback.Values)
			{
				if (!apnsFeedbackFile.HasExpired(this.ExpirationThreshold))
				{
					ApnsFeedbackValidationResult apnsFeedbackValidationResult2 = apnsFeedbackFile.ValidateNotification(notification);
					if (apnsFeedbackValidationResult2 == ApnsFeedbackValidationResult.Expired)
					{
						return apnsFeedbackValidationResult2;
					}
					if (apnsFeedbackValidationResult != apnsFeedbackValidationResult2 && apnsFeedbackValidationResult == ApnsFeedbackValidationResult.Valid)
					{
						apnsFeedbackValidationResult = ApnsFeedbackValidationResult.Uncertain;
					}
				}
			}
			return apnsFeedbackValidationResult;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006860 File Offset: 0x00004A60
		protected void UpdateFeedback()
		{
			try
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "[UpdateFeedback] Updating feedback");
				PushNotificationsCrimsonEvents.ApnsFeedbackManagerUpdating.Log<string>(string.Empty);
				if (!this.FileIO.Exists(ApnsFeedbackManager.FeedbackPath))
				{
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[UpdateFeedback] Feedback folder not found: '{0}'.", ApnsFeedbackManager.FeedbackPath);
					this.CurrentFeedback = new Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile>();
				}
				else
				{
					this.CurrentFeedback = this.BuildFeedbackFromFilesystem();
				}
			}
			catch (Exception ex)
			{
				string text = ex.ToTraceString();
				this.Tracer.TraceError<string>((long)this.GetHashCode(), "[UpdateFeedback] An error occurred trying to update the feedback: {0}", text);
				PushNotificationsCrimsonEvents.ApnsFeedbackManagerUpdateError.Log<string>(text);
				this.CurrentFeedback = this.ExpireInMemoryFeedback();
				if (!(ex is ApnsFeedbackException))
				{
					throw;
				}
			}
			finally
			{
				ExDateTime arg = ExDateTime.UtcNow.AddMilliseconds((double)this.Settings.UpdateIntervalInMilliseconds);
				this.Tracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "[UpdateFeedback] Scheduling next feedback update for '{0}'.", arg);
				PushNotificationsCrimsonEvents.ApnsFeedbackManagerUpdateDone.Log<string>(arg.ToString());
				this.Scheduler.ScheduleOnce(new Action(this.UpdateFeedback), this.Settings.UpdateIntervalInMilliseconds);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000069AC File Offset: 0x00004BAC
		private Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile> BuildFeedbackFromFilesystem()
		{
			Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile> dictionary = new Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile>();
			this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[UpdateFeedback] Searching for all feedback packages under: '{0}'.", ApnsFeedbackManager.FeedbackPath);
			List<IApnsFeedbackFile> list = ApnsFeedbackPackage.FindAll(ApnsFeedbackManager.FeedbackPath, this.FileIO, this.Tracer);
			foreach (IApnsFeedbackFile apnsFeedbackFile in list)
			{
				try
				{
					if (apnsFeedbackFile.HasExpired(this.ExpirationThreshold))
					{
						this.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[UpdateFeedback] Removing package: '{0}'.", apnsFeedbackFile.Identifier);
						apnsFeedbackFile.Remove();
					}
					else
					{
						IApnsFeedbackFile apnsFeedbackFile2;
						if (!this.CurrentFeedback.TryGetValue(apnsFeedbackFile.Identifier, out apnsFeedbackFile2))
						{
							apnsFeedbackFile2 = apnsFeedbackFile;
							apnsFeedbackFile2.Load();
						}
						this.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[UpdateFeedback] Adding package: '{0}'.", apnsFeedbackFile2.Identifier);
						dictionary.Add(apnsFeedbackFile2.Identifier, apnsFeedbackFile2);
					}
				}
				catch (ApnsFeedbackException exception)
				{
					string text = exception.ToTraceString();
					this.Tracer.TraceError<ApnsFeedbackFileId, string>((long)this.GetHashCode(), "[UpdateFeedback] An error occurred trying to update the feedback for '{0}': {1}", apnsFeedbackFile.Identifier, text);
					PushNotificationsCrimsonEvents.ApnsFeedbackManagerPackageError.Log<ApnsFeedbackFileId, string>(apnsFeedbackFile.Identifier, text);
				}
			}
			return dictionary;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006AFC File Offset: 0x00004CFC
		private Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile> ExpireInMemoryFeedback()
		{
			this.Tracer.TraceDebug((long)this.GetHashCode(), "[UpdateFeedback] Removing all expired packages from the in-memory collection.");
			Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile> dictionary = new Dictionary<ApnsFeedbackFileId, IApnsFeedbackFile>();
			foreach (IApnsFeedbackFile apnsFeedbackFile in this.CurrentFeedback.Values)
			{
				if (apnsFeedbackFile.HasExpired(this.ExpirationThreshold))
				{
					this.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[UpdateFeedback] Removing expired package '{0}'.", apnsFeedbackFile.Identifier);
				}
				else
				{
					this.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[UpdateFeedback] Keeping current package '{0}'.", apnsFeedbackFile.Identifier);
					dictionary.Add(apnsFeedbackFile.Identifier, apnsFeedbackFile);
				}
			}
			return dictionary;
		}

		// Token: 0x0400009B RID: 155
		private const string FeedbackRelativePath = "ClientAccess\\PushNotifications\\Feedback";

		// Token: 0x0400009C RID: 156
		internal static readonly string FeedbackPath = Path.Combine(ExchangeSetupContext.InstallPath, "ClientAccess\\PushNotifications\\Feedback");
	}
}
