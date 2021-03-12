using System;
using System.Collections;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001EA RID: 490
	internal class SubmissionHelper
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x00040E9C File Offset: 0x0003F09C
		internal SubmissionHelper() : this(null, PhoneNumber.Empty, Guid.Empty, null, null, null, null, Guid.Empty)
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00040EC4 File Offset: 0x0003F0C4
		internal SubmissionHelper(string callId, PhoneNumber callerId, Guid recipientObjectGuid, string recipientName, string cultureInfo, string callerAddress, string callerName, Guid tenantGuid) : this(callId, callerId, recipientObjectGuid, recipientName, cultureInfo, callerAddress, callerName, new Hashtable(), null, tenantGuid)
		{
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00040EEC File Offset: 0x0003F0EC
		internal SubmissionHelper(string callId, PhoneNumber callerId, Guid recipientObjectGuid, string recipientName, string cultureInfo, string callerAddress, string callerName, string callerIdDisplayName, Guid tenantGuid) : this(callId, callerId, recipientObjectGuid, recipientName, cultureInfo, callerAddress, callerName, new Hashtable(), callerIdDisplayName, tenantGuid)
		{
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00040F14 File Offset: 0x0003F114
		private SubmissionHelper(string callId, PhoneNumber callerId, Guid recipientObjectGuid, string recipientName, string cultureInfo, string callerAddress, string callerName, Hashtable customHeaders, string callerIdDisplayName, Guid tenantGuid)
		{
			this.callId = callId;
			this.callerId = callerId;
			this.callerName = callerName;
			this.customHeaders = customHeaders;
			this.recipientObjectGuid = recipientObjectGuid;
			this.cultureInfo = cultureInfo;
			this.recipientName = recipientName;
			this.callerAddress = callerAddress;
			this.callerIdDisplayName = callerIdDisplayName;
			this.TenantGuid = tenantGuid;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00040F74 File Offset: 0x0003F174
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00040F7C File Offset: 0x0003F17C
		public Guid TenantGuid { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00040F85 File Offset: 0x0003F185
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00040F8D File Offset: 0x0003F18D
		internal string CallId
		{
			get
			{
				return this.callId;
			}
			set
			{
				this.callId = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00040F96 File Offset: 0x0003F196
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00040F9E File Offset: 0x0003F19E
		internal PhoneNumber CallerId
		{
			get
			{
				return this.callerId;
			}
			set
			{
				this.callerId = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00040FA7 File Offset: 0x0003F1A7
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x00040FAF File Offset: 0x0003F1AF
		internal string CallerName
		{
			get
			{
				return this.callerName;
			}
			set
			{
				this.callerName = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00040FB8 File Offset: 0x0003F1B8
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00040FC0 File Offset: 0x0003F1C0
		internal Hashtable CustomHeaders
		{
			get
			{
				return this.customHeaders;
			}
			set
			{
				this.customHeaders = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00040FC9 File Offset: 0x0003F1C9
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x00040FD1 File Offset: 0x0003F1D1
		internal Guid RecipientObjectGuid
		{
			get
			{
				return this.recipientObjectGuid;
			}
			set
			{
				this.recipientObjectGuid = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00040FDA File Offset: 0x0003F1DA
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x00040FE2 File Offset: 0x0003F1E2
		internal string CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
			set
			{
				this.cultureInfo = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00040FEB File Offset: 0x0003F1EB
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x00040FF3 File Offset: 0x0003F1F3
		internal string RecipientName
		{
			get
			{
				return this.recipientName;
			}
			set
			{
				this.recipientName = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00040FFC File Offset: 0x0003F1FC
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x00041004 File Offset: 0x0003F204
		internal string CallerAddress
		{
			get
			{
				return this.callerAddress;
			}
			set
			{
				this.callerAddress = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0004100D File Offset: 0x0003F20D
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x00041015 File Offset: 0x0003F215
		internal string CallerIdDisplayName
		{
			get
			{
				return this.callerIdDisplayName;
			}
			set
			{
				this.callerIdDisplayName = value;
			}
		}

		// Token: 0x04000AEC RID: 2796
		private string callId;

		// Token: 0x04000AED RID: 2797
		private PhoneNumber callerId;

		// Token: 0x04000AEE RID: 2798
		private string callerIdDisplayName;

		// Token: 0x04000AEF RID: 2799
		private Guid recipientObjectGuid;

		// Token: 0x04000AF0 RID: 2800
		private string recipientName;

		// Token: 0x04000AF1 RID: 2801
		private string cultureInfo;

		// Token: 0x04000AF2 RID: 2802
		private string callerAddress;

		// Token: 0x04000AF3 RID: 2803
		private string callerName;

		// Token: 0x04000AF4 RID: 2804
		private Hashtable customHeaders;
	}
}
