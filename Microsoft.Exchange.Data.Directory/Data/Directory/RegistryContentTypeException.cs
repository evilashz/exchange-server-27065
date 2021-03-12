using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA6 RID: 2726
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryContentTypeException : DataSourceOperationException
	{
		// Token: 0x06008007 RID: 32775 RVA: 0x001A4C49 File Offset: 0x001A2E49
		public RegistryContentTypeException() : base(DirectoryStrings.RegistryContentTypeException)
		{
		}

		// Token: 0x06008008 RID: 32776 RVA: 0x001A4C56 File Offset: 0x001A2E56
		public RegistryContentTypeException(Exception innerException) : base(DirectoryStrings.RegistryContentTypeException, innerException)
		{
		}

		// Token: 0x06008009 RID: 32777 RVA: 0x001A4C64 File Offset: 0x001A2E64
		protected RegistryContentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x001A4C6E File Offset: 0x001A2E6E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
