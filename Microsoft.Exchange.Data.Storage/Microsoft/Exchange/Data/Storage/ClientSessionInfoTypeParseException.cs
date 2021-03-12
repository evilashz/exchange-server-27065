using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000102 RID: 258
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ClientSessionInfoTypeParseException : StoragePermanentException
	{
		// Token: 0x06001393 RID: 5011 RVA: 0x00069760 File Offset: 0x00067960
		public ClientSessionInfoTypeParseException(string typeName, string assemblyName) : base(ServerStrings.idClientSessionInfoTypeParseException(typeName, assemblyName))
		{
			this.typeName = typeName;
			this.assemblyName = assemblyName;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0006977D File Offset: 0x0006797D
		public ClientSessionInfoTypeParseException(string typeName, string assemblyName, Exception innerException) : base(ServerStrings.idClientSessionInfoTypeParseException(typeName, assemblyName), innerException)
		{
			this.typeName = typeName;
			this.assemblyName = assemblyName;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0006979C File Offset: 0x0006799C
		protected ClientSessionInfoTypeParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.typeName = (string)info.GetValue("typeName", typeof(string));
			this.assemblyName = (string)info.GetValue("assemblyName", typeof(string));
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000697F1 File Offset: 0x000679F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("typeName", this.typeName);
			info.AddValue("assemblyName", this.assemblyName);
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0006981D File Offset: 0x00067A1D
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00069825 File Offset: 0x00067A25
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
		}

		// Token: 0x04000992 RID: 2450
		private readonly string typeName;

		// Token: 0x04000993 RID: 2451
		private readonly string assemblyName;
	}
}
