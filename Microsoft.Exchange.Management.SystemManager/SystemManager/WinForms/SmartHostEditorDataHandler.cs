using System;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200020D RID: 525
	public class SmartHostEditorDataHandler : StrongTypeEditorDataHandler<SmartHost>
	{
		// Token: 0x060017CE RID: 6094 RVA: 0x0006460C File Offset: 0x0006280C
		public SmartHostEditorDataHandler(bool isUMSmartHost, SmartHostEditor control, bool isCloudOrganizationMode) : base(control, "SmartHost")
		{
			this.isUMSmartHost = isUMSmartHost;
			base.Table.Rows[0]["IsCloudOrganizationMode"] = isCloudOrganizationMode;
			base.Table.Rows[0]["IsIpAddress"] = !isCloudOrganizationMode;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00064671 File Offset: 0x00062871
		public SmartHostEditorDataHandler(bool isUMSmartHost, SmartHostEditor control) : this(isUMSmartHost, control, false)
		{
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0006467C File Offset: 0x0006287C
		protected override void UpdateStrongType()
		{
			Hostname hostname = (!DBNull.Value.Equals(base.Table.Rows[0]["Domain"])) ? ((Hostname)base.Table.Rows[0]["Domain"]) : null;
			bool flag = (bool)base.Table.Rows[0]["IsIpAddress"];
			IPAddress ipaddress = (IPAddress)base.Table.Rows[0]["Address"];
			string address = string.Empty;
			if (flag)
			{
				address = ipaddress.ToString();
			}
			else if (hostname != null)
			{
				address = hostname.HostnameString;
			}
			base.StrongType = (this.isUMSmartHost ? UMSmartHost.Parse(address) : SmartHost.Parse(address));
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00064750 File Offset: 0x00062950
		protected override void UpdateTable()
		{
			SmartHost strongType = base.StrongType;
			if (strongType.IsIPAddress)
			{
				base.Table.Rows[0]["Address"] = strongType.Address;
			}
			else
			{
				base.Table.Rows[0]["Domain"] = strongType.Domain;
			}
			base.Table.Rows[0]["IsIpAddress"] = (strongType.IsIPAddress && false.Equals(base.Table.Rows[0]["IsCloudOrganizationMode"]));
		}

		// Token: 0x040008E9 RID: 2281
		private bool isUMSmartHost;
	}
}
