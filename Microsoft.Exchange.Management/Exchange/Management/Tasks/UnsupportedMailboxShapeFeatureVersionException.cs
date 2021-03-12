using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF7 RID: 3831
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedMailboxShapeFeatureVersionException : LocalizedException
	{
		// Token: 0x0600A9BD RID: 43453 RVA: 0x0028C42E File Offset: 0x0028A62E
		public UnsupportedMailboxShapeFeatureVersionException(string identity) : base(Strings.ErrorUnsupportedMailboxShapeFeatureVersionException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A9BE RID: 43454 RVA: 0x0028C443 File Offset: 0x0028A643
		public UnsupportedMailboxShapeFeatureVersionException(string identity, Exception innerException) : base(Strings.ErrorUnsupportedMailboxShapeFeatureVersionException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A9BF RID: 43455 RVA: 0x0028C459 File Offset: 0x0028A659
		protected UnsupportedMailboxShapeFeatureVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A9C0 RID: 43456 RVA: 0x0028C483 File Offset: 0x0028A683
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036FE RID: 14078
		// (get) Token: 0x0600A9C1 RID: 43457 RVA: 0x0028C49E File Offset: 0x0028A69E
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006064 RID: 24676
		private readonly string identity;
	}
}
