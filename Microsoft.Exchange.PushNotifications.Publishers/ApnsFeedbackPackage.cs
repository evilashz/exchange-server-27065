using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200002C RID: 44
	internal class ApnsFeedbackPackage : ApnsFeedbackFileBase
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00006D07 File Offset: 0x00004F07
		internal ApnsFeedbackPackage(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO) : this(identifier, fileIO, ExTraceGlobals.PublisherManagerTracer)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006D16 File Offset: 0x00004F16
		internal ApnsFeedbackPackage(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO, ITracer tracer) : base(identifier, fileIO, tracer)
		{
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006D21 File Offset: 0x00004F21
		public override bool IsLoaded
		{
			get
			{
				return this.HasLoadedMetadata && this.Feedback != null;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006D39 File Offset: 0x00004F39
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00006D41 File Offset: 0x00004F41
		public bool IsExtracted { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006D4A File Offset: 0x00004F4A
		public bool HasLoadedMetadata
		{
			get
			{
				return this.IsExtracted && this.Metadata != null && this.Metadata.IsLoaded;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006D69 File Offset: 0x00004F69
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00006D71 File Offset: 0x00004F71
		protected ApnsFeedbackMetadata Metadata { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006D7A File Offset: 0x00004F7A
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006D82 File Offset: 0x00004F82
		protected Dictionary<string, ApnsFeedbackAppFile> Feedback { get; set; }

		// Token: 0x060001C0 RID: 448 RVA: 0x00006D8C File Offset: 0x00004F8C
		public override ApnsFeedbackValidationResult ValidateNotification(ApnsNotification notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (!this.IsLoaded)
			{
				base.Tracer.TraceDebug<ApnsNotification, ApnsFeedbackFileId>((long)this.GetHashCode(), "[ValidateNotification] Feedback package not loaded, defaulting to Uncertain for '{0}', '{1}'.", notification, base.Identifier);
				return ApnsFeedbackValidationResult.Uncertain;
			}
			ApnsFeedbackAppFile apnsFeedbackAppFile;
			if (this.Feedback.TryGetValue(notification.AppId, out apnsFeedbackAppFile))
			{
				return apnsFeedbackAppFile.ValidateNotification(notification);
			}
			base.Tracer.TraceDebug<ApnsNotification, string, ApnsFeedbackFileId>((long)this.GetHashCode(), "[ValidateNotification] Unable to find feedback for '{0}', '{1}', '{2}'.", notification, notification.AppId, base.Identifier);
			return ApnsFeedbackValidationResult.Uncertain;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006E10 File Offset: 0x00005010
		public override void Remove()
		{
			Exception ex = null;
			base.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[Remove] Removing feedback package '{0}'", base.Identifier);
			try
			{
				if (this.IsExtracted)
				{
					base.FileIO.DeleteFolder(base.Identifier.GetPackageExtractionFolder());
				}
				else
				{
					base.FileIO.DeleteFile(base.Identifier.ToString());
				}
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackPackageRemovalFailed(base.Identifier.ToString(), ex.Message), ex);
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006EBC File Offset: 0x000050BC
		public override void Load()
		{
			if (this.IsLoaded)
			{
				return;
			}
			if (!this.IsExtracted)
			{
				this.Extract();
			}
			if (!this.HasLoadedMetadata)
			{
				this.LoadMetadata();
			}
			this.LoadFeedback();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006EEC File Offset: 0x000050EC
		internal static ApnsFeedbackPackage CreateFromMetadata(ApnsFeedbackMetadata metadata)
		{
			ArgumentValidator.ThrowIfNull("metadata", metadata);
			return new ApnsFeedbackPackage(metadata.Identifier.GetPackageId(), metadata.FileIO, metadata.Tracer)
			{
				Metadata = metadata,
				IsExtracted = true
			};
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006F5C File Offset: 0x0000515C
		internal static List<IApnsFeedbackFile> FindAll(string path, ApnsFeedbackFileIO fileIO, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("path", path);
			ArgumentValidator.ThrowIfNull("fileIO", fileIO);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			List<ApnsFeedbackPackage> list = ApnsFeedbackFileBase.FindFeedbackFiles<ApnsFeedbackPackage>(path, "*.zip", fileIO, (ApnsFeedbackFileId id) => new ApnsFeedbackPackage(id, fileIO), tracer);
			List<IApnsFeedbackFile> list2 = new List<IApnsFeedbackFile>(list.Count);
			foreach (ApnsFeedbackPackage item in list)
			{
				list2.Add(item);
			}
			List<ApnsFeedbackPackage> list3 = ApnsFeedbackFileBase.FindFeedbackFiles<ApnsFeedbackPackage>(path, "*.metadata", SearchOption.AllDirectories, fileIO, (ApnsFeedbackFileId id) => ApnsFeedbackPackage.CreateFromMetadata(new ApnsFeedbackMetadata(id, fileIO)), tracer);
			foreach (ApnsFeedbackPackage apnsFeedbackPackage in list3)
			{
				if (list2.Contains(apnsFeedbackPackage))
				{
					tracer.TraceWarning<ApnsFeedbackFileId>(0L, "[FindAll] Skipping extracted package '{0}' because we found also the compressed version", apnsFeedbackPackage.Identifier);
				}
				else
				{
					list2.Add(apnsFeedbackPackage);
				}
			}
			return list2;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007094 File Offset: 0x00005294
		private void Extract()
		{
			Exception ex = null;
			try
			{
				if (base.FileIO.Exists(base.Identifier.GetPackageExtractionFolder()))
				{
					base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[Extract] Removing existing folder '{0}'", base.Identifier.GetPackageExtractionFolder());
					base.FileIO.DeleteFolder(base.Identifier.GetPackageExtractionFolder());
				}
				base.Tracer.TraceDebug<ApnsFeedbackFileId, string>((long)this.GetHashCode(), "[Extract] Extracting '{0}' on '{1}'", base.Identifier, base.Identifier.GetPackageExtractionFolder());
				base.FileIO.ExtractFileToDirectory(base.Identifier.ToString(), base.Identifier.GetPackageExtractionFolder());
				this.IsExtracted = true;
				base.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[Extract] Deleting '{0}'", base.Identifier);
				base.FileIO.DeleteFile(base.Identifier.ToString());
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackPackageExtractionFailed(base.Identifier.ToString(), ex.Message), ex);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000071D0 File Offset: 0x000053D0
		private void LoadMetadata()
		{
			if (this.Metadata == null)
			{
				base.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[LoadMetadata] Loading metadata for '{0}'", base.Identifier);
				List<ApnsFeedbackMetadata> list = ApnsFeedbackFileBase.FindFeedbackFiles<ApnsFeedbackMetadata>(base.Identifier.GetPackageExtractionFolder(), "*.metadata", base.FileIO, (ApnsFeedbackFileId id) => new ApnsFeedbackMetadata(id, base.FileIO), base.Tracer);
				if (list.Count != 1)
				{
					if (list.Count > 1)
					{
						base.Tracer.TraceWarning(0L, string.Format("[FindInPackage] Found at least two metadata files for the same package: '{0}'; '{1}'", list[0], list[1]));
					}
					throw new ApnsFeedbackException(Strings.ApnsFeedbackPackageUnexpectedMetadataResult(base.Identifier.GetPackageExtractionFolder(), list.Count));
				}
				this.Metadata = list[0];
			}
			this.Metadata.Load();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000072B4 File Offset: 0x000054B4
		private void LoadFeedback()
		{
			base.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[LoadMetadata] Loading feedback for '{0}''", base.Identifier);
			List<ApnsFeedbackAppFile> list = ApnsFeedbackFileBase.FindFeedbackFiles<ApnsFeedbackAppFile>(base.Identifier.GetPackageExtractionFolder(), base.Identifier.GetFeedbackFileSearchPattern(), base.FileIO, (ApnsFeedbackFileId id) => new ApnsFeedbackAppFile(id, base.FileIO), base.Tracer);
			if (list.Count <= 0)
			{
				throw new ApnsFeedbackException(Strings.ApnsFeedbackPackageFeedbackNotFound(base.Identifier.GetPackageExtractionFolder()));
			}
			Dictionary<string, ApnsFeedbackAppFile> dictionary = new Dictionary<string, ApnsFeedbackAppFile>();
			foreach (ApnsFeedbackAppFile apnsFeedbackAppFile in list)
			{
				apnsFeedbackAppFile.Load();
				dictionary.Add(apnsFeedbackAppFile.Identifier.AppId, apnsFeedbackAppFile);
			}
			this.Feedback = dictionary;
		}
	}
}
