using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x0200007A RID: 122
	[LocDescription(QueueViewerStrings.IDs.SetMessageTask)]
	[Cmdlet("Set", "Message", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetMessage : MessageActionWithFilter
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00010674 File Offset: 0x0000E874
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001068B File Offset: 0x0000E88B
		[Parameter(Mandatory = true)]
		public int OutboundIPPool
		{
			get
			{
				return (int)base.Fields["OutboundIPPool"];
			}
			set
			{
				base.Fields["OutboundIPPool"] = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000106A3 File Offset: 0x0000E8A3
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x000106BA File Offset: 0x0000E8BA
		[Parameter(Mandatory = false)]
		public bool Resubmit
		{
			get
			{
				return (bool)base.Fields["Resubmit"];
			}
			set
			{
				base.Fields["Resubmit"] = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000106D2 File Offset: 0x0000E8D2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Filter" == base.ParameterSetName)
				{
					return QueueViewerStrings.ConfirmationMessageSetMessageFilter(base.Filter.ToString());
				}
				return QueueViewerStrings.ConfirmationMessageSetMessageIdentity(base.Identity.ToString());
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00010708 File Offset: 0x0000E908
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.OutboundIPPool < 0 || this.OutboundIPPool > 65535)
			{
				base.WriteError(new LocalizedException(QueueViewerStrings.SetMessageOutboundPoolOutsideRange(this.OutboundIPPool, 0, 65535)), ErrorCategory.InvalidData, this.OutboundIPPool);
			}
			if (base.Fields.IsModified("OutboundIPPool") && (!base.Fields.IsModified("Resubmit") || !this.Resubmit))
			{
				base.WriteError(new LocalizedException(QueueViewerStrings.SetMessageResubmitMustBeTrue), ErrorCategory.InvalidData, "Resubmit");
			}
			long identity = 1L;
			QueueIdentity queueIdentity = QueueIdentity.Empty;
			if (base.Identity != null)
			{
				identity = base.Identity.InternalId;
				queueIdentity = base.Identity.QueueIdentity;
			}
			this.properties = new PropertyBagBasedMessageInfo(identity, queueIdentity);
			this.properties.OutboundIPPool = this.OutboundIPPool;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000107E8 File Offset: 0x0000E9E8
		protected override void RunAction()
		{
			using (QueueViewerClient<ExtensibleMessageInfo> queueViewerClient = new QueueViewerClient<ExtensibleMessageInfo>((string)base.Server))
			{
				queueViewerClient.SetMessage(base.Identity, this.innerFilter, this.properties, base.Fields.IsModified("Resubmit") && this.Resubmit);
			}
		}

		// Token: 0x04000180 RID: 384
		private const string ResubmitProperty = "Resubmit";

		// Token: 0x04000181 RID: 385
		private const string OutboundIPPoolProperty = "OutboundIPPool";

		// Token: 0x04000182 RID: 386
		private ExtensibleMessageInfo properties;
	}
}
