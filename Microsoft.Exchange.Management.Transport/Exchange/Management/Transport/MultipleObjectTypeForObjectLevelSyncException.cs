using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200018F RID: 399
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultipleObjectTypeForObjectLevelSyncException : InvalidConfigurationObjectTypeException
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00036822 File Offset: 0x00034A22
		public MultipleObjectTypeForObjectLevelSyncException(string types) : base(Strings.ErrorMultipleObjectTypeForObjectLevelSync(types))
		{
			this.types = types;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00036837 File Offset: 0x00034A37
		public MultipleObjectTypeForObjectLevelSyncException(string types, Exception innerException) : base(Strings.ErrorMultipleObjectTypeForObjectLevelSync(types), innerException)
		{
			this.types = types;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003684D File Offset: 0x00034A4D
		protected MultipleObjectTypeForObjectLevelSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.types = (string)info.GetValue("types", typeof(string));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00036877 File Offset: 0x00034A77
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("types", this.types);
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00036892 File Offset: 0x00034A92
		public string Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x04000686 RID: 1670
		private readonly string types;
	}
}
