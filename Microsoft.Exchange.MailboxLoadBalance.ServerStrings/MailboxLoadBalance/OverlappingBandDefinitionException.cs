using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OverlappingBandDefinitionException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002DE5 File Offset: 0x00000FE5
		public OverlappingBandDefinitionException(string newBand, string existingBand) : base(MigrationWorkflowServiceStrings.ErrorOverlappingBandDefinition(newBand, existingBand))
		{
			this.newBand = newBand;
			this.existingBand = existingBand;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E02 File Offset: 0x00001002
		public OverlappingBandDefinitionException(string newBand, string existingBand, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorOverlappingBandDefinition(newBand, existingBand), innerException)
		{
			this.newBand = newBand;
			this.existingBand = existingBand;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002E20 File Offset: 0x00001020
		protected OverlappingBandDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.newBand = (string)info.GetValue("newBand", typeof(string));
			this.existingBand = (string)info.GetValue("existingBand", typeof(string));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002E75 File Offset: 0x00001075
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("newBand", this.newBand);
			info.AddValue("existingBand", this.existingBand);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002EA1 File Offset: 0x000010A1
		public string NewBand
		{
			get
			{
				return this.newBand;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002EA9 File Offset: 0x000010A9
		public string ExistingBand
		{
			get
			{
				return this.existingBand;
			}
		}

		// Token: 0x04000030 RID: 48
		private readonly string newBand;

		// Token: 0x04000031 RID: 49
		private readonly string existingBand;
	}
}
