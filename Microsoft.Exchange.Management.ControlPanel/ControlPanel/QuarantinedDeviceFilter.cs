using System;
using System.Management.Automation;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200030D RID: 781
	[DataContract]
	public class QuarantinedDeviceFilter : ResultSizeFilter
	{
		// Token: 0x06002E6B RID: 11883 RVA: 0x0008CCBC File Offset: 0x0008AEBC
		public QuarantinedDeviceFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x0008CCDE File Offset: 0x0008AEDE
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MobileDevice";
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x0008CCE5 File Offset: 0x0008AEE5
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x0008CCEC File Offset: 0x0008AEEC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base.ResultSize = 500;
			base["Filter"] = "DeviceAccessState -eq 'Quarantined'";
			base["ActiveSync"] = new SwitchParameter(true);
		}

		// Token: 0x04002298 RID: 8856
		public new const string RbacParameters = "?ResultSize&Filter&ActiveSync";
	}
}
