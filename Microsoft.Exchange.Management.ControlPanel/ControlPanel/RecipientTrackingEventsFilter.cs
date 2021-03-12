using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Management.Tracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002CA RID: 714
	[DataContract]
	public class RecipientTrackingEventsFilter : ResultSizeFilter
	{
		// Token: 0x06002C59 RID: 11353 RVA: 0x00089138 File Offset: 0x00087338
		public RecipientTrackingEventsFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x0008915A File Offset: 0x0008735A
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MessageTrackingReport";
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x00089161 File Offset: 0x00087361
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x00089168 File Offset: 0x00087368
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x0008917A File Offset: 0x0008737A
		[DataMember]
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

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x00089188 File Offset: 0x00087388
		// (set) Token: 0x06002C5F RID: 11359 RVA: 0x000891AA File Offset: 0x000873AA
		[DataMember]
		public RecipientDeliveryStatus RecipientStatus
		{
			get
			{
				if (base["Status"] != null)
				{
					return (RecipientDeliveryStatus)base["Status"];
				}
				return RecipientDeliveryStatus.All;
			}
			set
			{
				if (value != RecipientDeliveryStatus.All)
				{
					base["Status"] = (_DeliveryStatus)value;
				}
			}
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000891C2 File Offset: 0x000873C2
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000891D9 File Offset: 0x000873D9
		[DataMember]
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

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000891EC File Offset: 0x000873EC
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x000891F4 File Offset: 0x000873F4
		[DataMember]
		public string SearchText
		{
			get
			{
				return this.Recipients;
			}
			set
			{
				this.Recipients = value;
			}
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x00089200 File Offset: 0x00087400
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			if (base.CanSetParameter("ByPassDelegateChecking"))
			{
				base["BypassDelegateChecking"] = true;
			}
			if (base.CanSetParameter("DetailLevel"))
			{
				base["DetailLevel"] = MessageTrackingDetailLevel.Verbose;
			}
			this.RecipientStatus = RecipientDeliveryStatus.All;
			base.ResultSize = 30;
		}

		// Token: 0x040021F3 RID: 8691
		public new const string RbacParameters = "?ResultSize&Identity&Status&Recipients";
	}
}
