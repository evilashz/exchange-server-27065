using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010B8 RID: 4280
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MetadataMustBeAbsoluteUrlException : FederationException
	{
		// Token: 0x0600B286 RID: 45702 RVA: 0x00299CEC File Offset: 0x00297EEC
		public MetadataMustBeAbsoluteUrlException() : base(Strings.ErrorMetadataMustBeAbsoluteUrl)
		{
		}

		// Token: 0x0600B287 RID: 45703 RVA: 0x00299CF9 File Offset: 0x00297EF9
		public MetadataMustBeAbsoluteUrlException(Exception innerException) : base(Strings.ErrorMetadataMustBeAbsoluteUrl, innerException)
		{
		}

		// Token: 0x0600B288 RID: 45704 RVA: 0x00299D07 File Offset: 0x00297F07
		protected MetadataMustBeAbsoluteUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B289 RID: 45705 RVA: 0x00299D11 File Offset: 0x00297F11
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
