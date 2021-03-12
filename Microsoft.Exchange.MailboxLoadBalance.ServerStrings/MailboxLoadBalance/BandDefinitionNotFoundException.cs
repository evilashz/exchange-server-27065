using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BandDefinitionNotFoundException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00002D6D File Offset: 0x00000F6D
		public BandDefinitionNotFoundException(string band) : base(MigrationWorkflowServiceStrings.ErrorBandDefinitionNotFound(band))
		{
			this.band = band;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D82 File Offset: 0x00000F82
		public BandDefinitionNotFoundException(string band, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorBandDefinitionNotFound(band), innerException)
		{
			this.band = band;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D98 File Offset: 0x00000F98
		protected BandDefinitionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.band = (string)info.GetValue("band", typeof(string));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DC2 File Offset: 0x00000FC2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("band", this.band);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002DDD File Offset: 0x00000FDD
		public string Band
		{
			get
			{
				return this.band;
			}
		}

		// Token: 0x0400002F RID: 47
		private readonly string band;
	}
}
