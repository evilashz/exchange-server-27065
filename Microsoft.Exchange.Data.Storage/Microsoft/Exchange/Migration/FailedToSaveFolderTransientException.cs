using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014A RID: 330
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToSaveFolderTransientException : MigrationTransientException
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x0006E934 File Offset: 0x0006CB34
		public FailedToSaveFolderTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0006E93D File Offset: 0x0006CB3D
		public FailedToSaveFolderTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0006E947 File Offset: 0x0006CB47
		protected FailedToSaveFolderTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0006E951 File Offset: 0x0006CB51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
