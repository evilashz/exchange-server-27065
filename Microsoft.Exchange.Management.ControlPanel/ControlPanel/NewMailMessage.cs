using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Providers;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C2 RID: 706
	[DataContract]
	public class NewMailMessage : WebServiceParameters
	{
		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x00088D39 File Offset: 0x00086F39
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-MailMessage";
			}
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x00088D40 File Offset: 0x00086F40
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x00088D47 File Offset: 0x00086F47
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x00088D59 File Offset: 0x00086F59
		[DataMember]
		public string Subject
		{
			get
			{
				return (string)base[MailMessageSchema.Subject];
			}
			set
			{
				base[MailMessageSchema.Subject] = value;
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x00088D67 File Offset: 0x00086F67
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x00088D79 File Offset: 0x00086F79
		[DataMember]
		public string Body
		{
			get
			{
				return (string)base[MailMessageSchema.Body];
			}
			set
			{
				base[MailMessageSchema.Body] = value;
			}
		}

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x00088D87 File Offset: 0x00086F87
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x00088D99 File Offset: 0x00086F99
		[DataMember]
		public BodyFormat BodyFormat
		{
			get
			{
				return (BodyFormat)base[MailMessageSchema.BodyFormat];
			}
			set
			{
				base[MailMessageSchema.BodyFormat] = (MailBodyFormat)value;
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x00088DAC File Offset: 0x00086FAC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.BodyFormat = BodyFormat.Html;
		}

		// Token: 0x040021DE RID: 8670
		public const string RbacParameters = "?Subject&Body&BodyFormat";
	}
}
