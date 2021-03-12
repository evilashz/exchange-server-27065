using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200018E RID: 398
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidDeltaSyncAndFullSyncTypeException : InvalidConfigurationObjectTypeException
	{
		// Token: 0x06000FB1 RID: 4017 RVA: 0x000367AA File Offset: 0x000349AA
		public InvalidDeltaSyncAndFullSyncTypeException(string types) : base(Strings.ErrorInvalidDeltaSyncAndFullSyncType(types))
		{
			this.types = types;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000367BF File Offset: 0x000349BF
		public InvalidDeltaSyncAndFullSyncTypeException(string types, Exception innerException) : base(Strings.ErrorInvalidDeltaSyncAndFullSyncType(types), innerException)
		{
			this.types = types;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000367D5 File Offset: 0x000349D5
		protected InvalidDeltaSyncAndFullSyncTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.types = (string)info.GetValue("types", typeof(string));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000367FF File Offset: 0x000349FF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("types", this.types);
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0003681A File Offset: 0x00034A1A
		public string Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x04000685 RID: 1669
		private readonly string types;
	}
}
