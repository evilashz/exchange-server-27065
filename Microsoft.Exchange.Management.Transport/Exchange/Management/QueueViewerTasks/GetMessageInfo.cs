using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000072 RID: 114
	[Cmdlet("Get", "Message", DefaultParameterSetName = "Filter")]
	public sealed class GetMessageInfo : RpcPagedGetObjectTask<ExtensibleMessageInfo>
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public GetMessageInfo()
		{
			this.IncludeRecipientInfo = new SwitchParameter(true);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000FF80 File Offset: 0x0000E180
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000FF97 File Offset: 0x0000E197
		[Parameter(ParameterSetName = "Identity", Position = 0)]
		[ValidateNotNullOrEmpty]
		public MessageIdentity Identity
		{
			get
			{
				return (MessageIdentity)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000FFAA File Offset: 0x0000E1AA
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000FFC1 File Offset: 0x0000E1C1
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "Queue", ValueFromPipeline = true)]
		public QueueIdentity Queue
		{
			get
			{
				return (QueueIdentity)base.Fields["Queue"];
			}
			set
			{
				base.Fields["Queue"] = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeRecipientInfo
		{
			get
			{
				return base.IncludeDetails;
			}
			set
			{
				base.IncludeDetails = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000FFED File Offset: 0x0000E1ED
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeComponentLatencyInfo
		{
			get
			{
				return base.IncludeLatencyInfo;
			}
			set
			{
				base.IncludeLatencyInfo = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000FFF6 File Offset: 0x0000E1F6
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ExtensibleMessageInfoSchema>();
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FFFD File Offset: 0x0000E1FD
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.NeedSuppressingPiiData && base.ExchangeRunspaceConfig != null)
			{
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010021 File Offset: 0x0000E221
		protected override void InternalValidate()
		{
			this.InitializeInnerFilter<ExtensibleMessageInfo>(ExtensibleMessageInfoSchema.Identity, ExtensibleMessageInfoSchema.Queue);
			base.InternalValidate();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001003C File Offset: 0x0000E23C
		internal override void InitializeInnerFilter<Object>(QueueViewerPropertyDefinition<Object> messageIdentity, QueueViewerPropertyDefinition<Object> queueIdentity)
		{
			if (this.Identity != null)
			{
				if (this.Identity.IsFullySpecified)
				{
					this.innerFilter = new ComparisonFilter(ComparisonOperator.Equal, messageIdentity, this.Identity);
				}
				else
				{
					this.innerFilter = new TextFilter(messageIdentity, this.Identity.ToString(), MatchOptions.FullString, MatchFlags.Default);
				}
				base.Server = ServerIdParameter.Parse(this.Identity.QueueIdentity.Server);
				return;
			}
			if (this.Queue != null)
			{
				if (this.Queue.IsFullySpecified)
				{
					this.innerFilter = new ComparisonFilter(ComparisonOperator.Equal, queueIdentity, this.Queue);
				}
				else
				{
					this.innerFilter = new TextFilter(queueIdentity, this.Queue.ToString(), MatchOptions.FullString, MatchFlags.Default);
				}
				base.Server = ServerIdParameter.Parse(this.Queue.Server);
			}
		}
	}
}
