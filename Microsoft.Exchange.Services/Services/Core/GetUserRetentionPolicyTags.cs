using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200033E RID: 830
	internal sealed class GetUserRetentionPolicyTags : SingleStepServiceCommand<GetUserRetentionPolicyTagsRequest, Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag[]>, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600175C RID: 5980 RVA: 0x0007CEDB File Offset: 0x0007B0DB
		public GetUserRetentionPolicyTags(CallContext callContext, GetUserRetentionPolicyTagsRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0007CEF1 File Offset: 0x0007B0F1
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<GetUserRetentionPolicyTags>(this);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0007CEF9 File Offset: 0x0007B0F9
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0007CF0E File Offset: 0x0007B10E
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0007CF30 File Offset: 0x0007B130
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetUserRetentionPolicyTagsResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0007CF58 File Offset: 0x0007B158
		internal override ServiceResult<Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag[]> Execute()
		{
			Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag[] value = this.FetchRetentionPolicyTags();
			return new ServiceResult<Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag[]>(value);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0007CF72 File Offset: 0x0007B172
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0007CF98 File Offset: 0x0007B198
		private Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag[] FetchRetentionPolicyTags()
		{
			List<Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag> list = new List<Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag>();
			if (base.MailboxIdentityMailboxSession != null)
			{
				PolicyTagList policyTagList = base.MailboxIdentityMailboxSession.GetPolicyTagList((Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType)0);
				if (policyTagList != null)
				{
					foreach (PolicyTag policyTag in policyTagList.Values)
					{
						list.Add(new Microsoft.Exchange.Services.Core.Types.RetentionPolicyTag(policyTag));
					}
				}
			}
			base.CallContext.ProtocolLog.AppendGenericInfo("UserRetentionPolicyTagsCount", list.Count);
			ExTraceGlobals.ELCTracer.TraceDebug<int>((long)this.GetHashCode(), "[GetUserRetentionPolicyTags::FetchRetentionPolicyTags] retrieved {0} retention policy tags", list.Count);
			return list.ToArray();
		}

		// Token: 0x04000FC9 RID: 4041
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000FCA RID: 4042
		private bool disposed;
	}
}
