using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200018D RID: 397
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidObjectSyncTypeException : InvalidConfigurationObjectTypeException
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00036732 File Offset: 0x00034932
		public InvalidObjectSyncTypeException(string types) : base(Strings.ErrorInvalidObjectSyncType(types))
		{
			this.types = types;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00036747 File Offset: 0x00034947
		public InvalidObjectSyncTypeException(string types, Exception innerException) : base(Strings.ErrorInvalidObjectSyncType(types), innerException)
		{
			this.types = types;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003675D File Offset: 0x0003495D
		protected InvalidObjectSyncTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.types = (string)info.GetValue("types", typeof(string));
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00036787 File Offset: 0x00034987
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("types", this.types);
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000367A2 File Offset: 0x000349A2
		public string Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x04000684 RID: 1668
		private readonly string types;
	}
}
