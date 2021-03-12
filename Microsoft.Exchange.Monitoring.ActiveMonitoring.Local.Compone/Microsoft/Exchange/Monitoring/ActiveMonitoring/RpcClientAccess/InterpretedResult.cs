using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x020001F3 RID: 499
	public class InterpretedResult
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0005E507 File Offset: 0x0005C707
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0005E50F File Offset: 0x0005C70F
		public ProbeResult RawResult { get; internal set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0005E518 File Offset: 0x0005C718
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0005E525 File Offset: 0x0005C725
		public string ActivityContext
		{
			get
			{
				return this.RawResult.StateAttribute15;
			}
			set
			{
				this.RawResult.StateAttribute15 = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0005E533 File Offset: 0x0005C733
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0005E540 File Offset: 0x0005C740
		public string UserLegacyDN
		{
			get
			{
				return this.RawResult.StateAttribute21;
			}
			set
			{
				this.RawResult.StateAttribute21 = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0005E54E File Offset: 0x0005C74E
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0005E55B File Offset: 0x0005C75B
		public string RequestUrl
		{
			get
			{
				return this.RawResult.StateAttribute23;
			}
			set
			{
				this.RawResult.StateAttribute23 = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0005E569 File Offset: 0x0005C769
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0005E576 File Offset: 0x0005C776
		public string AuthType
		{
			get
			{
				return this.RawResult.StateAttribute24;
			}
			set
			{
				this.RawResult.StateAttribute24 = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0005E584 File Offset: 0x0005C784
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0005E591 File Offset: 0x0005C791
		public string RespondingHttpServer
		{
			get
			{
				return this.RawResult.StateAttribute3;
			}
			set
			{
				this.RawResult.StateAttribute3 = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0005E59F File Offset: 0x0005C79F
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0005E5AC File Offset: 0x0005C7AC
		public string RespondingRpcProxyServer
		{
			get
			{
				return this.RawResult.StateAttribute4;
			}
			set
			{
				this.RawResult.StateAttribute4 = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0005E5BA File Offset: 0x0005C7BA
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0005E5C7 File Offset: 0x0005C7C7
		public string MonitoringAccount
		{
			get
			{
				return this.RawResult.StateAttribute13;
			}
			set
			{
				this.RawResult.StateAttribute13 = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0005E5D5 File Offset: 0x0005C7D5
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0005E5E2 File Offset: 0x0005C7E2
		public string OutlookSessionCookie
		{
			get
			{
				return this.RawResult.StateAttribute5;
			}
			set
			{
				this.RawResult.StateAttribute5 = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0005E5F0 File Offset: 0x0005C7F0
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0005E602 File Offset: 0x0005C802
		public TimeSpan TotalLatency
		{
			get
			{
				return TimeSpan.FromMilliseconds(this.RawResult.SampleValue);
			}
			set
			{
				this.RawResult.SampleValue = value.TotalMilliseconds;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0005E616 File Offset: 0x0005C816
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0005E623 File Offset: 0x0005C823
		public string FirstFailedTaskName
		{
			get
			{
				return this.RawResult.StateAttribute22;
			}
			set
			{
				this.RawResult.StateAttribute22 = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0005E631 File Offset: 0x0005C831
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0005E63E File Offset: 0x0005C83E
		public string ExecutionOutline
		{
			get
			{
				return this.RawResult.StateAttribute25;
			}
			set
			{
				this.RawResult.StateAttribute25 = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0005E64C File Offset: 0x0005C84C
		public string RootCause
		{
			get
			{
				return this.RawResult.StateAttribute2;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0005E659 File Offset: 0x0005C859
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0005E666 File Offset: 0x0005C866
		public string OspUrl
		{
			get
			{
				return this.RawResult.StateAttribute14;
			}
			set
			{
				this.RawResult.StateAttribute14 = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0005E674 File Offset: 0x0005C874
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0005E681 File Offset: 0x0005C881
		public virtual string VerboseLog
		{
			get
			{
				return this.RawResult.ExecutionContext;
			}
			set
			{
				this.RawResult.ExecutionContext = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0005E68F File Offset: 0x0005C88F
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x0005E69C File Offset: 0x0005C89C
		public string ErrorDetails
		{
			get
			{
				return this.RawResult.FailureContext;
			}
			set
			{
				this.RawResult.FailureContext = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0005E6AA File Offset: 0x0005C8AA
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0005E6BC File Offset: 0x0005C8BC
		public TimeSpan InitialLatency
		{
			get
			{
				return TimeSpan.FromMilliseconds(this.RawResult.StateAttribute18);
			}
			set
			{
				this.RawResult.StateAttribute18 = value.TotalMilliseconds;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0005E6D0 File Offset: 0x0005C8D0
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0005E6DD File Offset: 0x0005C8DD
		public string InitialException
		{
			get
			{
				return this.RawResult.StateAttribute12;
			}
			set
			{
				this.RawResult.StateAttribute12 = value;
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		public void SetRootCause(string rootCause, FailingComponent failingComponent)
		{
			this.RawResult.FailureCategory = (int)failingComponent;
			this.RawResult.StateAttribute1 = failingComponent.ToString();
			this.RawResult.StateAttribute2 = rootCause;
			this.RawResult.StateAttribute11 = FailingComponent.Momt.ToString();
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0005E73D File Offset: 0x0005C93D
		protected internal virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x04000A77 RID: 2679
		public const string UnknownRPCProxyServer = "Unknown";
	}
}
