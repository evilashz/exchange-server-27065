using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DefaultFolderLocalizationException : CorruptDataException
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x000673FA File Offset: 0x000655FA
		public DefaultFolderLocalizationException() : base(ServerStrings.idDefaultFoldersNotLocalizedException)
		{
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00067407 File Offset: 0x00065607
		public DefaultFolderLocalizationException(Exception innerException) : base(ServerStrings.idDefaultFoldersNotLocalizedException, innerException)
		{
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00067415 File Offset: 0x00065615
		protected DefaultFolderLocalizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0006741F File Offset: 0x0006561F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
