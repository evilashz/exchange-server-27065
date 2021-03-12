using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000199 RID: 409
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotRemoveEndpointWithAssociatedBatchesException : MigrationTransientException
	{
		// Token: 0x0600174F RID: 5967 RVA: 0x000707BE File Offset: 0x0006E9BE
		public CannotRemoveEndpointWithAssociatedBatchesException(string endpointId, string batches) : base(Strings.ErrorCannotRemoveEndpointWithAssociatedBatches(endpointId, batches))
		{
			this.endpointId = endpointId;
			this.batches = batches;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000707DB File Offset: 0x0006E9DB
		public CannotRemoveEndpointWithAssociatedBatchesException(string endpointId, string batches, Exception innerException) : base(Strings.ErrorCannotRemoveEndpointWithAssociatedBatches(endpointId, batches), innerException)
		{
			this.endpointId = endpointId;
			this.batches = batches;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000707FC File Offset: 0x0006E9FC
		protected CannotRemoveEndpointWithAssociatedBatchesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointId = (string)info.GetValue("endpointId", typeof(string));
			this.batches = (string)info.GetValue("batches", typeof(string));
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00070851 File Offset: 0x0006EA51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointId", this.endpointId);
			info.AddValue("batches", this.batches);
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x0007087D File Offset: 0x0006EA7D
		public string EndpointId
		{
			get
			{
				return this.endpointId;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00070885 File Offset: 0x0006EA85
		public string Batches
		{
			get
			{
				return this.batches;
			}
		}

		// Token: 0x04000B13 RID: 2835
		private readonly string endpointId;

		// Token: 0x04000B14 RID: 2836
		private readonly string batches;
	}
}
