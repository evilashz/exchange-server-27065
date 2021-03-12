using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DDB RID: 3547
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IISNotInstalledException : DataSourceOperationException
	{
		// Token: 0x0600A433 RID: 42035 RVA: 0x00283D00 File Offset: 0x00281F00
		public IISNotInstalledException() : base(Strings.IISNotInstalledException)
		{
		}

		// Token: 0x0600A434 RID: 42036 RVA: 0x00283D0D File Offset: 0x00281F0D
		public IISNotInstalledException(Exception innerException) : base(Strings.IISNotInstalledException, innerException)
		{
		}

		// Token: 0x0600A435 RID: 42037 RVA: 0x00283D1B File Offset: 0x00281F1B
		protected IISNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A436 RID: 42038 RVA: 0x00283D25 File Offset: 0x00281F25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
