using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x0200007C RID: 124
	[Cmdlet("Get", "Queue", DefaultParameterSetName = "Filter")]
	public sealed class GetQueueInfo : RpcPagedGetObjectTask<ExtensibleQueueInfo>
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00010A3E File Offset: 0x0000EC3E
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00010A55 File Offset: 0x0000EC55
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public QueueIdentity Identity
		{
			get
			{
				return (QueueIdentity)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00010A68 File Offset: 0x0000EC68
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00010A7F File Offset: 0x0000EC7F
		[Parameter(Mandatory = false)]
		public QueueViewerIncludesAndExcludes Include
		{
			get
			{
				return (QueueViewerIncludesAndExcludes)base.Fields["Include"];
			}
			set
			{
				base.Fields["Include"] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00010A92 File Offset: 0x0000EC92
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00010AA9 File Offset: 0x0000ECA9
		[Parameter(Mandatory = false)]
		public QueueViewerIncludesAndExcludes Exclude
		{
			get
			{
				return (QueueViewerIncludesAndExcludes)base.Fields["Exclude"];
			}
			set
			{
				base.Fields["Exclude"] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00010ABC File Offset: 0x0000ECBC
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>();
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00010AC3 File Offset: 0x0000ECC3
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.InitializeInnerFilter<ExtensibleQueueInfo>(null, ExtensibleQueueInfoSchema.Identity);
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			TaskLogger.LogExit();
			this.ValidateIncludeExclude();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		internal override void InitializeInnerFilter<Object>(QueueViewerPropertyDefinition<Object> messageIdentity, QueueViewerPropertyDefinition<Object> queueIdentity)
		{
			if (this.Identity != null)
			{
				if (this.Identity.IsFullySpecified)
				{
					this.innerFilter = new ComparisonFilter(ComparisonOperator.Equal, queueIdentity, this.Identity);
				}
				else
				{
					this.innerFilter = new TextFilter(queueIdentity, this.Identity.ToString(), MatchOptions.FullString, MatchFlags.Default);
				}
				base.Server = ServerIdParameter.Parse(this.Identity.Server);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00010B64 File Offset: 0x0000ED64
		private void ValidateIncludeExclude()
		{
			QueueViewerIncludesAndExcludes exclude = this.Exclude;
			QueueViewerIncludesAndExcludes include = this.Include;
			if (exclude == null && include == null)
			{
				return;
			}
			if (this.Identity != null)
			{
				base.Server = ServerIdParameter.Parse(this.Identity.Server);
			}
			string filter;
			LocalizedString localizedString;
			if (QueueViewerIncludesAndExcludes.ComposeFilterString(base.Filter, exclude, include, out filter, out localizedString))
			{
				base.Filter = filter;
				return;
			}
			base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidData, base.Filter);
		}
	}
}
