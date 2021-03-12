using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C3 RID: 195
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IndexStatusRegistryNotFoundException : IndexStatusException
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00012F28 File Offset: 0x00011128
		public IndexStatusRegistryNotFoundException() : base(Strings.IndexStatusRegistryNotFound)
		{
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00012F3A File Offset: 0x0001113A
		public IndexStatusRegistryNotFoundException(Exception innerException) : base(Strings.IndexStatusRegistryNotFound, innerException)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00012F4D File Offset: 0x0001114D
		protected IndexStatusRegistryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00012F57 File Offset: 0x00011157
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
