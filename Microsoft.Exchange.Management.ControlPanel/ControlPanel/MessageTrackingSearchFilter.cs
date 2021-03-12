using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D1 RID: 721
	[DataContract]
	public class MessageTrackingSearchFilter : ResultSizeFilter
	{
		// Token: 0x06002C89 RID: 11401 RVA: 0x000895E0 File Offset: 0x000877E0
		public MessageTrackingSearchFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x00089602 File Offset: 0x00087802
		public override string AssociatedCmdlet
		{
			get
			{
				return "Search-MessageTrackingReport";
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x00089609 File Offset: 0x00087809
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x00089610 File Offset: 0x00087810
		// (set) Token: 0x06002C8D RID: 11405 RVA: 0x00089622 File Offset: 0x00087822
		[DataMember]
		public string Subject
		{
			get
			{
				return (string)base["Subject"];
			}
			set
			{
				base["Subject"] = value;
			}
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x00089630 File Offset: 0x00087830
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x00089642 File Offset: 0x00087842
		[DataMember]
		public string Sender
		{
			get
			{
				return (string)base["Sender"];
			}
			set
			{
				base["Sender"] = value.Trim();
			}
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x00089655 File Offset: 0x00087855
		// (set) Token: 0x06002C91 RID: 11409 RVA: 0x00089667 File Offset: 0x00087867
		[DataMember]
		public string MessageEntryId
		{
			get
			{
				return (string)base["MessageEntryId"];
			}
			set
			{
				base["MessageEntryId"] = value;
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x00089675 File Offset: 0x00087875
		// (set) Token: 0x06002C93 RID: 11411 RVA: 0x00089687 File Offset: 0x00087887
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x00089698 File Offset: 0x00087898
		// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000896BC File Offset: 0x000878BC
		[DataMember]
		public string Recipients
		{
			get
			{
				SmtpAddress[] addresses = base["Recipients"] as SmtpAddress[];
				return addresses.ToSmtpAddressesString();
			}
			set
			{
				base["Recipients"] = value.ToSmtpAddressArray();
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000896CF File Offset: 0x000878CF
		[OnDeserializing]
		private void OnDeserializing(StreamingContext streamingContext)
		{
			this.Identity = ((RbacPrincipal.Current.ExecutingUserId != null) ? Identity.FromExecutingUserId() : null);
			if (base.CanSetParameter("ByPassDelegateChecking"))
			{
				base["BypassDelegateChecking"] = true;
			}
		}

		// Token: 0x04002201 RID: 8705
		public new const string RbacParameters = "?ResultSize&Identity&MessageId&Subject&Sender&Recipients";
	}
}
