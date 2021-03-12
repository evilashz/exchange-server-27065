using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002CF RID: 719
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class MailboxData
	{
		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x00027DD7 File Offset: 0x00025FD7
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x00027DDF File Offset: 0x00025FDF
		public EmailAddress Email
		{
			get
			{
				return this.emailField;
			}
			set
			{
				this.emailField = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00027DE8 File Offset: 0x00025FE8
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x00027DF0 File Offset: 0x00025FF0
		public MeetingAttendeeType AttendeeType
		{
			get
			{
				return this.attendeeTypeField;
			}
			set
			{
				this.attendeeTypeField = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x00027DF9 File Offset: 0x00025FF9
		// (set) Token: 0x06001872 RID: 6258 RVA: 0x00027E01 File Offset: 0x00026001
		public bool ExcludeConflicts
		{
			get
			{
				return this.excludeConflictsField;
			}
			set
			{
				this.excludeConflictsField = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x00027E0A File Offset: 0x0002600A
		// (set) Token: 0x06001874 RID: 6260 RVA: 0x00027E12 File Offset: 0x00026012
		[XmlIgnore]
		public bool ExcludeConflictsSpecified
		{
			get
			{
				return this.excludeConflictsFieldSpecified;
			}
			set
			{
				this.excludeConflictsFieldSpecified = value;
			}
		}

		// Token: 0x04001087 RID: 4231
		private EmailAddress emailField;

		// Token: 0x04001088 RID: 4232
		private MeetingAttendeeType attendeeTypeField;

		// Token: 0x04001089 RID: 4233
		private bool excludeConflictsField;

		// Token: 0x0400108A RID: 4234
		private bool excludeConflictsFieldSpecified;
	}
}
