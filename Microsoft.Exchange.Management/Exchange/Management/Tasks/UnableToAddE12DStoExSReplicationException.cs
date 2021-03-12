using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DEF RID: 3567
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToAddE12DStoExSReplicationException : LocalizedException
	{
		// Token: 0x0600A49F RID: 42143 RVA: 0x00284914 File Offset: 0x00282B14
		public UnableToAddE12DStoExSReplicationException(string dom) : base(Strings.UnableToAddE12DStoExSReplicationException(dom))
		{
			this.dom = dom;
		}

		// Token: 0x0600A4A0 RID: 42144 RVA: 0x00284929 File Offset: 0x00282B29
		public UnableToAddE12DStoExSReplicationException(string dom, Exception innerException) : base(Strings.UnableToAddE12DStoExSReplicationException(dom), innerException)
		{
			this.dom = dom;
		}

		// Token: 0x0600A4A1 RID: 42145 RVA: 0x0028493F File Offset: 0x00282B3F
		protected UnableToAddE12DStoExSReplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dom = (string)info.GetValue("dom", typeof(string));
		}

		// Token: 0x0600A4A2 RID: 42146 RVA: 0x00284969 File Offset: 0x00282B69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dom", this.dom);
		}

		// Token: 0x17003600 RID: 13824
		// (get) Token: 0x0600A4A3 RID: 42147 RVA: 0x00284984 File Offset: 0x00282B84
		public string Dom
		{
			get
			{
				return this.dom;
			}
		}

		// Token: 0x04005F66 RID: 24422
		private readonly string dom;
	}
}
