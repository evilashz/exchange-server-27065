using System;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002CC RID: 716
	public class GetMessageTrackingReportParameters : WebServiceParameters
	{
		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x0008934E File Offset: 0x0008754E
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MessageTrackingReport";
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x00089355 File Offset: 0x00087555
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x0008935C File Offset: 0x0008755C
		// (set) Token: 0x06002C6B RID: 11371 RVA: 0x0008936E File Offset: 0x0008756E
		public string Identity
		{
			get
			{
				return (string)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x0008937C File Offset: 0x0008757C
		// (set) Token: 0x06002C6D RID: 11373 RVA: 0x0008938E File Offset: 0x0008758E
		public int ResultSize
		{
			get
			{
				return (int)base["ResultSize"];
			}
			set
			{
				base["ResultSize"] = value;
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x06002C6E RID: 11374 RVA: 0x000893A1 File Offset: 0x000875A1
		// (set) Token: 0x06002C6F RID: 11375 RVA: 0x000893B8 File Offset: 0x000875B8
		public string Recipients
		{
			get
			{
				return base["Recipients"].StringArrayJoin(",");
			}
			set
			{
				base["Recipients"] = value.ToArrayOfStrings();
			}
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x000893CC File Offset: 0x000875CC
		// (set) Token: 0x06002C71 RID: 11377 RVA: 0x000893FC File Offset: 0x000875FC
		public bool ByPassDelegateChecking
		{
			get
			{
				return (bool?)base["ByPassDelegateChecking"] == true;
			}
			set
			{
				if (base.CanSetParameter("ByPassDelegateChecking"))
				{
					base["ByPassDelegateChecking"] = value;
				}
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x0008941C File Offset: 0x0008761C
		// (set) Token: 0x06002C73 RID: 11379 RVA: 0x0008942E File Offset: 0x0008762E
		public MessageTrackingDetailLevel DetailLevel
		{
			get
			{
				return (MessageTrackingDetailLevel)base["DetailLevel"];
			}
			set
			{
				if (base.CanSetParameter("DetailLevel"))
				{
					base["DetailLevel"] = value;
				}
			}
		}

		// Token: 0x040021F9 RID: 8697
		public const string RbacParameters = "?ResultSize&Identity&Recipients";
	}
}
