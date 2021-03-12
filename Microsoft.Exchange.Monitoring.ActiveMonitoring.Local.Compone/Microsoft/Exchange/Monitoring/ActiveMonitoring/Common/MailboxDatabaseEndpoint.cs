using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000540 RID: 1344
	internal class MailboxDatabaseEndpoint : IEndpoint
	{
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000C902C File Offset: 0x000C722C
		public ICollection<MailboxDatabaseInfo> MailboxDatabaseInfoCollectionForBackend
		{
			get
			{
				return this.validMailboxDatabasesForBackend;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x000C9034 File Offset: 0x000C7234
		public ICollection<MailboxDatabaseInfo> MailboxDatabaseInfoCollectionForCafe
		{
			get
			{
				return this.validMailboxDatabasesForCafe;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x000C903C File Offset: 0x000C723C
		public ICollection<MailboxDatabaseInfo> UnverifiedMailboxDatabaseInfoCollectionForBackendLiveIdAuthenticationProbe
		{
			get
			{
				return this.unverifiedMailboxDatabasesForBackend;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000C9044 File Offset: 0x000C7244
		public ICollection<MailboxDatabaseInfo> UnverifiedMailboxDatabaseInfoCollectionForCafeLiveIdAuthenticationProbe
		{
			get
			{
				return this.unverifiedMailboxDatabasesForCafe;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x000C904C File Offset: 0x000C724C
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000C904F File Offset: 0x000C724F
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x000C9057 File Offset: 0x000C7257
		public Exception Exception { get; set; }

		// Token: 0x06002109 RID: 8457 RVA: 0x000C9060 File Offset: 0x000C7260
		public bool DetectChange()
		{
			this.changeModulus++;
			if (this.changeModulus >= 5)
			{
				this.changeModulus = 0;
			}
			if (DirectoryAccessor.Instance.Server != null)
			{
				if (this.changeModulus % 5 == 0 && DirectoryAccessor.Instance.Server.IsMailboxServer)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Detecting local mailbox database endpoint changes", null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseEndpoint.cs", 170);
					if (this.backendDelegate.DetectChange(this.unverifiedMailboxDatabasesForBackend))
					{
						return true;
					}
				}
				if (DirectoryAccessor.Instance.Server.IsCafeServer)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Detecting remote mailbox database endpoint changes", null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseEndpoint.cs", 182);
					if (this.cafeDelegate.DetectChange(this.unverifiedMailboxDatabasesForCafe))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000C913C File Offset: 0x000C733C
		public virtual void Initialize()
		{
			if (DirectoryAccessor.Instance.Server != null)
			{
				string text = IPGlobalProperties.GetIPGlobalProperties().DomainName.ToLower();
				if (text.Contains("prdmgt01.prod.exchangelabs.com") || text.Contains("sdfmgt02.sdf.exchangelabs.com") || text.Contains("chnmgt03.partner.outlook.cn"))
				{
					return;
				}
				if (DirectoryAccessor.Instance.Server.IsMailboxServer)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox role installed, initializing local mailbox database list", null, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseEndpoint.cs", 215);
					this.backendDelegate.Initialize(this.validMailboxDatabasesForBackend, this.unverifiedMailboxDatabasesForBackend);
				}
				if (DirectoryAccessor.Instance.Server.IsCafeServer)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Cafe role installed, initializing remote mailbox database list", null, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MailboxDatabaseEndpoint.cs", 224);
					this.cafeDelegate.Initialize(this.validMailboxDatabasesForCafe, this.unverifiedMailboxDatabasesForCafe);
				}
			}
		}

		// Token: 0x04001828 RID: 6184
		private const int backendMultiple = 5;

		// Token: 0x04001829 RID: 6185
		private List<MailboxDatabaseInfo> validMailboxDatabasesForBackend = new List<MailboxDatabaseInfo>();

		// Token: 0x0400182A RID: 6186
		private List<MailboxDatabaseInfo> validMailboxDatabasesForCafe = new List<MailboxDatabaseInfo>();

		// Token: 0x0400182B RID: 6187
		private List<MailboxDatabaseInfo> unverifiedMailboxDatabasesForBackend = new List<MailboxDatabaseInfo>();

		// Token: 0x0400182C RID: 6188
		private List<MailboxDatabaseInfo> unverifiedMailboxDatabasesForCafe = new List<MailboxDatabaseInfo>();

		// Token: 0x0400182D RID: 6189
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x0400182E RID: 6190
		private MailboxDatabaseEndpointDelegate backendDelegate = new BackendMailboxDatabaseEndpointDelegate();

		// Token: 0x0400182F RID: 6191
		private MailboxDatabaseEndpointDelegate cafeDelegate = new CafeMailboxDatabaseEndpointDelegate();

		// Token: 0x04001830 RID: 6192
		private int changeModulus;
	}
}
