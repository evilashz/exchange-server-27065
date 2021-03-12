using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000024 RID: 36
	internal abstract class ApnsFeedbackFileBase : IApnsFeedbackFile, IApnsFeedbackProvider, IEquatable<IApnsFeedbackFile>
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00005D87 File Offset: 0x00003F87
		protected ApnsFeedbackFileBase(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("identifier", identifier);
			ArgumentValidator.ThrowIfNull("fileIO", fileIO);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.identifier = identifier;
			this.fileIO = fileIO;
			this.tracer = tracer;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005DC5 File Offset: 0x00003FC5
		public ApnsFeedbackFileId Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005DCD File Offset: 0x00003FCD
		public ApnsFeedbackFileIO FileIO
		{
			get
			{
				return this.fileIO;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005DD5 File Offset: 0x00003FD5
		public ITracer Tracer
		{
			get
			{
				return this.tracer;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005DDD File Offset: 0x00003FDD
		public virtual bool IsLoaded
		{
			get
			{
				throw new NotImplementedException("ApnsFeedbackFileBase.IsLoaded");
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005DE9 File Offset: 0x00003FE9
		public virtual void Load()
		{
			throw new NotImplementedException("ApnsFeedbackFileBase.Load");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005DF8 File Offset: 0x00003FF8
		public virtual bool HasExpired(TimeSpan expirationThreshold)
		{
			return this.Identifier.Date < ExDateTime.UtcNow.Subtract(expirationThreshold);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005E23 File Offset: 0x00004023
		public virtual void Remove()
		{
			throw new NotImplementedException("ApnsFeedbackFileBase.Remove");
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005E2F File Offset: 0x0000402F
		public virtual ApnsFeedbackValidationResult ValidateNotification(ApnsNotification notification)
		{
			throw new NotImplementedException("ApnsFeedbackFileBase.ValidateNotification");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005E3B File Offset: 0x0000403B
		public bool Equals(IApnsFeedbackFile other)
		{
			return other != null && this.Identifier.Equals(other.Identifier);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005E54 File Offset: 0x00004054
		public override bool Equals(object obj)
		{
			IApnsFeedbackFile apnsFeedbackFile = obj as IApnsFeedbackFile;
			return apnsFeedbackFile != null && this.Equals(apnsFeedbackFile);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005E74 File Offset: 0x00004074
		public override int GetHashCode()
		{
			return this.Identifier.ToString().GetHashCode();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005E86 File Offset: 0x00004086
		public override string ToString()
		{
			return this.Identifier.ToString();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005E93 File Offset: 0x00004093
		protected static List<T> FindFeedbackFiles<T>(string root, string searchPattern, ApnsFeedbackFileIO fileIO, Func<ApnsFeedbackFileId, T> constructor, ITracer tracer) where T : ApnsFeedbackFileBase
		{
			return ApnsFeedbackFileBase.FindFeedbackFiles<T>(root, searchPattern, SearchOption.TopDirectoryOnly, fileIO, constructor, tracer);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005EA4 File Offset: 0x000040A4
		protected static List<T> FindFeedbackFiles<T>(string root, string searchPattern, SearchOption searchOption, ApnsFeedbackFileIO fileIO, Func<ApnsFeedbackFileId, T> constructor, ITracer tracer) where T : ApnsFeedbackFileBase
		{
			ArgumentValidator.ThrowIfNullOrEmpty("root", root);
			ArgumentValidator.ThrowIfNullOrEmpty("searchPattern", searchPattern);
			ArgumentValidator.ThrowIfNull("fileIO", fileIO);
			tracer.TraceDebug<string, string, SearchOption>(0L, "[FindFeedbackFiles] Searching for files under '{0}' with pattern '{1}' and option '{2}'.", root, searchPattern, searchOption);
			List<T> list = new List<T>();
			Exception ex = null;
			try
			{
				string[] files = fileIO.GetFiles(root, searchPattern, searchOption);
				foreach (string text in files)
				{
					tracer.TraceDebug<string>(0L, "[FindFeedbackFiles] File name found: {0}.", text);
					ApnsFeedbackFileId arg = ApnsFeedbackFileId.Parse(text);
					T item = constructor(arg);
					list.Add(item);
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
				throw new ApnsFeedbackException(Strings.ApnsFeedbackFileGetFilesError(searchPattern, root, ex.Message), ex);
			}
			return list;
		}

		// Token: 0x04000085 RID: 133
		private ApnsFeedbackFileId identifier;

		// Token: 0x04000086 RID: 134
		private ApnsFeedbackFileIO fileIO;

		// Token: 0x04000087 RID: 135
		private ITracer tracer;
	}
}
