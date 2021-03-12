using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B4 RID: 2484
	[Cmdlet("Get", "MailboxTransportService", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxTransportService : GetSystemConfigurationObjectTask<MailboxTransportServerIdParameter, MailboxTransportServer>
	{
		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x060058A1 RID: 22689 RVA: 0x00171FF4 File Offset: 0x001701F4
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new BitMaskOrFilter(MailboxTransportServerSchema.CurrentServerRole, 2UL);
			}
		}

		// Token: 0x17001A72 RID: 6770
		// (get) Token: 0x060058A2 RID: 22690 RVA: 0x00172002 File Offset: 0x00170202
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060058A3 RID: 22691 RVA: 0x00172008 File Offset: 0x00170208
		protected override void WriteResult(IConfigurable dataObject)
		{
			MailboxTransportServer dataObject2 = (MailboxTransportServer)dataObject;
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(new MailboxTransportServerPresentationObject(dataObject2));
			TaskLogger.LogExit();
		}

		// Token: 0x060058A4 RID: 22692 RVA: 0x00172047 File Offset: 0x00170247
		protected override void InternalValidate()
		{
			if (this.Identity != null)
			{
				this.Identity = MailboxTransportServerIdParameter.CreateIdentity(this.Identity);
			}
			base.InternalValidate();
		}
	}
}
