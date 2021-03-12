using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000193 RID: 403
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationEndpointDuplicatedException : MigrationPermanentException
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x00070398 File Offset: 0x0006E598
		public MigrationEndpointDuplicatedException(string endpointIdentity) : base(Strings.MigrationEndpointAlreadyExistsError(endpointIdentity))
		{
			this.endpointIdentity = endpointIdentity;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000703AD File Offset: 0x0006E5AD
		public MigrationEndpointDuplicatedException(string endpointIdentity, Exception innerException) : base(Strings.MigrationEndpointAlreadyExistsError(endpointIdentity), innerException)
		{
			this.endpointIdentity = endpointIdentity;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000703C3 File Offset: 0x0006E5C3
		protected MigrationEndpointDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointIdentity = (string)info.GetValue("endpointIdentity", typeof(string));
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000703ED File Offset: 0x0006E5ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointIdentity", this.endpointIdentity);
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00070408 File Offset: 0x0006E608
		public string EndpointIdentity
		{
			get
			{
				return this.endpointIdentity;
			}
		}

		// Token: 0x04000B09 RID: 2825
		private readonly string endpointIdentity;
	}
}
