using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003CD RID: 973
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckEseutilErrorException : FileCheckException
	{
		// Token: 0x06002867 RID: 10343 RVA: 0x000B7D3B File Offset: 0x000B5F3B
		public FileCheckEseutilErrorException(string errorMessage) : base(ReplayStrings.FileCheckEseutilError(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000B7D55 File Offset: 0x000B5F55
		public FileCheckEseutilErrorException(string errorMessage, Exception innerException) : base(ReplayStrings.FileCheckEseutilError(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000B7D70 File Offset: 0x000B5F70
		protected FileCheckEseutilErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000B7D9A File Offset: 0x000B5F9A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000B7DB5 File Offset: 0x000B5FB5
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040013DA RID: 5082
		private readonly string errorMessage;
	}
}
