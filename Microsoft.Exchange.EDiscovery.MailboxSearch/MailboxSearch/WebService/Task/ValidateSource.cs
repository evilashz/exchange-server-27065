using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x0200005C RID: 92
	internal class ValidateSource : SearchTask<SearchSource>
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00018738 File Offset: 0x00016938
		internal ValidateSource.ValidateSourceContext TaskContext
		{
			get
			{
				return base.Context.TaskContext as ValidateSource.ValidateSourceContext;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00018770 File Offset: 0x00016970
		public override void Process(SearchSource item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "ValidateSource.Process Item:", item);
			if (item.Recipient == null || item.Recipient.ADEntry == null)
			{
				Recorder.Trace(4L, TraceType.ErrorTrace, "ValidateSource.Process Failed ADEntry Missing Item:", item);
				base.Executor.Fail(new SearchException(KnownError.ErrorRecipientTypeNotSupported)
				{
					ErrorSource = item
				});
				return;
			}
			if (this.TaskContext != null)
			{
				if (this.TaskContext.AllowedRecipientTypeDetails != null)
				{
					RecipientTypeDetails typeDetails = (RecipientTypeDetails)item.GetProperty(ADRecipientSchema.RecipientTypeDetails);
					if (!this.TaskContext.AllowedRecipientTypeDetails.Any((RecipientTypeDetails t) => t == typeDetails))
					{
						Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
						{
							"ValidateSource.Process Failed RecipientTypeDetails TypeDetials:",
							typeDetails,
							"Allowed:",
							this.TaskContext.AllowedRecipientTypeDetails
						});
						base.Executor.Fail(new SearchException(KnownError.ErrorRecipientTypeNotSupported)
						{
							ErrorSource = item
						});
						return;
					}
				}
				if (this.TaskContext.AllowedRecipientTypes != null)
				{
					RecipientType type = (RecipientType)item.GetProperty(ADRecipientSchema.RecipientType);
					if (!this.TaskContext.AllowedRecipientTypes.Any((RecipientType t) => t == type))
					{
						Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
						{
							"ValidateSource.Process Failed RecipientTypes Type:",
							type,
							"Allowed:",
							this.TaskContext.AllowedRecipientTypes
						});
						base.Executor.Fail(new SearchException(KnownError.ErrorRecipientTypeNotSupported)
						{
							ErrorSource = item
						});
						return;
					}
				}
				if (this.TaskContext.MinimumVersion != null && item.Recipient.ADEntry.ExchangeVersion.IsOlderThan(this.TaskContext.MinimumVersion))
				{
					Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
					{
						"ValidateSource.Process Failed Version Version:",
						item.Recipient.ADEntry.ExchangeVersion,
						"Allowed:",
						this.TaskContext.MinimumVersion
					});
					base.Executor.Fail(new SearchException(KnownError.ErrorMailboxVersionNotSupported)
					{
						ErrorSource = item
					});
					return;
				}
				RecipientSoftDeletedStatusFlags recipientSoftDeletedStatusFlags = (RecipientSoftDeletedStatusFlags)item.GetProperty(ADRecipientSchema.RecipientSoftDeletedStatus);
				if (recipientSoftDeletedStatusFlags != RecipientSoftDeletedStatusFlags.None && !recipientSoftDeletedStatusFlags.HasFlag(RecipientSoftDeletedStatusFlags.Inactive))
				{
					Recorder.Trace(4L, TraceType.ErrorTrace, "Skipping non-inactive, soft deleted item:", recipientSoftDeletedStatusFlags);
					return;
				}
				if (item.SourceType != SourceType.PublicFolder && !string.IsNullOrEmpty(this.TaskContext.RequiredCmdlet) && !string.IsNullOrEmpty(this.TaskContext.RequiredCmdletParameters) && !base.Policy.CallerInfo.IsOpenAsAdmin && base.Policy.RunspaceConfiguration != null && !base.Executor.Policy.RunspaceConfiguration.IsCmdletAllowedInScope(this.TaskContext.RequiredCmdlet, this.TaskContext.RequiredCmdletParameters.Split(new char[]
				{
					','
				}), item.Recipient.ADEntry, ScopeLocation.RecipientWrite))
				{
					Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
					{
						"ValidateSource.Process Failed Permission Entry:",
						item.Recipient.ADEntry,
						"Allowed:",
						this.TaskContext.RequiredCmdlet,
						this.TaskContext.RequiredCmdletParameters
					});
					base.Executor.Fail(new SearchException(KnownError.ErrorNoPermissionToSearchOrHoldMailbox)
					{
						ErrorSource = item
					});
					return;
				}
			}
			base.Executor.EnqueueNext(item);
		}

		// Token: 0x0200005D RID: 93
		internal class ValidateSourceContext
		{
			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x00018B33 File Offset: 0x00016D33
			// (set) Token: 0x060003B9 RID: 953 RVA: 0x00018B3B File Offset: 0x00016D3B
			public ExchangeObjectVersion MinimumVersion { get; set; }

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060003BA RID: 954 RVA: 0x00018B44 File Offset: 0x00016D44
			// (set) Token: 0x060003BB RID: 955 RVA: 0x00018B4C File Offset: 0x00016D4C
			public IEnumerable<RecipientTypeDetails> AllowedRecipientTypeDetails { get; set; }

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060003BC RID: 956 RVA: 0x00018B55 File Offset: 0x00016D55
			// (set) Token: 0x060003BD RID: 957 RVA: 0x00018B5D File Offset: 0x00016D5D
			public IEnumerable<RecipientType> AllowedRecipientTypes { get; set; }

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060003BE RID: 958 RVA: 0x00018B66 File Offset: 0x00016D66
			// (set) Token: 0x060003BF RID: 959 RVA: 0x00018B6E File Offset: 0x00016D6E
			public string RequiredCmdlet { get; set; }

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x00018B77 File Offset: 0x00016D77
			// (set) Token: 0x060003C1 RID: 961 RVA: 0x00018B7F File Offset: 0x00016D7F
			public string RequiredCmdletParameters { get; set; }
		}
	}
}
