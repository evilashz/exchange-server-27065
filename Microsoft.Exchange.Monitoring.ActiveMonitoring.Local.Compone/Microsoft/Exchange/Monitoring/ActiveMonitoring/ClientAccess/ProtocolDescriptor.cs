using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess
{
	// Token: 0x02000047 RID: 71
	internal class ProtocolDescriptor
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public HttpProtocol HttpProtocol { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00010AB9 File Offset: 0x0000ECB9
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00010AC1 File Offset: 0x0000ECC1
		public string AppPool { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00010ACA File Offset: 0x0000ECCA
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		public string VirtualDirectory { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00010ADB File Offset: 0x0000ECDB
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00010AE3 File Offset: 0x0000ECE3
		public bool IsRedirectOK { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00010AEC File Offset: 0x0000ECEC
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		public Component HealthSet { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00010AFD File Offset: 0x0000ECFD
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00010B05 File Offset: 0x0000ED05
		public bool DeferAlertOnCafeWideFailure { get; set; }

		// Token: 0x0600024A RID: 586 RVA: 0x00010B0E File Offset: 0x0000ED0E
		public override string ToString()
		{
			return this.HttpProtocol.ToString();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00010B20 File Offset: 0x0000ED20
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00010B28 File Offset: 0x0000ED28
		public AuthenticationMethod[] AuthPreferenceOrderDatacenter { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00010B31 File Offset: 0x0000ED31
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00010B39 File Offset: 0x0000ED39
		public AuthenticationMethod[] AuthPreferenceOrderEnterprise { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00010B42 File Offset: 0x0000ED42
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00010B4A File Offset: 0x0000ED4A
		public int ProtocolPriority { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00010B53 File Offset: 0x0000ED53
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00010B79 File Offset: 0x0000ED79
		public string LogFolderName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.logFolderName))
				{
					return this.logFolderName;
				}
				return this.HttpProtocol.ToString();
			}
			set
			{
				this.logFolderName = value;
			}
		}

		// Token: 0x040001A1 RID: 417
		private string logFolderName;
	}
}
