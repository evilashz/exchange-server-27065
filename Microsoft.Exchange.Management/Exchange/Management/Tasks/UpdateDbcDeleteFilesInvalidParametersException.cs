using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200109A RID: 4250
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UpdateDbcDeleteFilesInvalidParametersException : LocalizedException
	{
		// Token: 0x0600B202 RID: 45570 RVA: 0x002993FD File Offset: 0x002975FD
		public UpdateDbcDeleteFilesInvalidParametersException() : base(Strings.UpdateDbcDeleteFilesInvalidParametersException)
		{
		}

		// Token: 0x0600B203 RID: 45571 RVA: 0x0029940A File Offset: 0x0029760A
		public UpdateDbcDeleteFilesInvalidParametersException(Exception innerException) : base(Strings.UpdateDbcDeleteFilesInvalidParametersException, innerException)
		{
		}

		// Token: 0x0600B204 RID: 45572 RVA: 0x00299418 File Offset: 0x00297618
		protected UpdateDbcDeleteFilesInvalidParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B205 RID: 45573 RVA: 0x00299422 File Offset: 0x00297622
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
