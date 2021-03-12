using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.UnifiedContent;
using Microsoft.Filtering;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000836 RID: 2102
	internal sealed class FileFilteringServiceInvokerRequest : FilteringServiceInvokerRequest, IDisposeTrackable, IDisposable
	{
		// Token: 0x060048F2 RID: 18674 RVA: 0x0012BC96 File Offset: 0x00129E96
		private FileFilteringServiceInvokerRequest(string organizationId, TimeSpan scanTimeout, int textScanLimit, FileFipsDataStreamFilteringRequest fileFipsDataStreamFilteringRequest) : base(organizationId, scanTimeout, textScanLimit, fileFipsDataStreamFilteringRequest)
		{
			this.contentManager = fileFipsDataStreamFilteringRequest.ContentManager;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x0012BCD0 File Offset: 0x00129ED0
		public static FileFilteringServiceInvokerRequest CreateInstance(string fileName, Stream fileStream, int textScanLimit, string organizationId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("fileName", fileName);
			ArgumentValidator.ThrowIfInvalidValue<Stream>("fileStream", fileStream, (Stream stream) => stream != null || stream.Length > 0L);
			ArgumentValidator.ThrowIfNullOrEmpty("organizationId", organizationId);
			TimeSpan scanTimeout = FileFilteringServiceInvokerRequest.GetScanTimeout(fileStream);
			ContentManager contentManager = new ContentManager(Path.GetTempPath());
			FileFipsDataStreamFilteringRequest fileFipsDataStreamFilteringRequest = FileFipsDataStreamFilteringRequest.CreateInstance(fileName, fileStream, contentManager);
			return new FileFilteringServiceInvokerRequest(organizationId, scanTimeout, textScanLimit, fileFipsDataStreamFilteringRequest);
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x0012BD3F File Offset: 0x00129F3F
		internal static TimeSpan GetScanTimeout(Stream fileStream)
		{
			return FileFilteringServiceInvokerRequest.rulesScanTimeout.GetTimeout(fileStream, null);
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x0012BD4D File Offset: 0x00129F4D
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FileFilteringServiceInvokerRequest>(this);
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x0012BD55 File Offset: 0x00129F55
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x0012BD6A File Offset: 0x00129F6A
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.contentManager != null)
			{
				this.contentManager.Dispose();
				this.contentManager = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04002C24 RID: 11300
		private DisposeTracker disposeTracker;

		// Token: 0x04002C25 RID: 11301
		private ContentManager contentManager;

		// Token: 0x04002C26 RID: 11302
		private static readonly RulesScanTimeout rulesScanTimeout = new RulesScanTimeout(Components.TransportAppConfig.TransportRuleConfig.ScanVelocities, Components.TransportAppConfig.TransportRuleConfig.TransportRuleMinFipsTimeoutInMilliseconds);
	}
}
