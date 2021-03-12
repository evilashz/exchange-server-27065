using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200026D RID: 621
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserOofSettings
	{
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00027430 File Offset: 0x00025630
		// (set) Token: 0x06001746 RID: 5958 RVA: 0x00027438 File Offset: 0x00025638
		public OofState OofState
		{
			get
			{
				return this.oofStateField;
			}
			set
			{
				this.oofStateField = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00027441 File Offset: 0x00025641
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x00027449 File Offset: 0x00025649
		public ExternalAudience ExternalAudience
		{
			get
			{
				return this.externalAudienceField;
			}
			set
			{
				this.externalAudienceField = value;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00027452 File Offset: 0x00025652
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x0002745A File Offset: 0x0002565A
		public Duration Duration
		{
			get
			{
				return this.durationField;
			}
			set
			{
				this.durationField = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00027463 File Offset: 0x00025663
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x0002746B File Offset: 0x0002566B
		public ReplyBody InternalReply
		{
			get
			{
				return this.internalReplyField;
			}
			set
			{
				this.internalReplyField = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00027474 File Offset: 0x00025674
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x0002747C File Offset: 0x0002567C
		public ReplyBody ExternalReply
		{
			get
			{
				return this.externalReplyField;
			}
			set
			{
				this.externalReplyField = value;
			}
		}

		// Token: 0x04000FB8 RID: 4024
		private OofState oofStateField;

		// Token: 0x04000FB9 RID: 4025
		private ExternalAudience externalAudienceField;

		// Token: 0x04000FBA RID: 4026
		private Duration durationField;

		// Token: 0x04000FBB RID: 4027
		private ReplyBody internalReplyField;

		// Token: 0x04000FBC RID: 4028
		private ReplyBody externalReplyField;
	}
}
