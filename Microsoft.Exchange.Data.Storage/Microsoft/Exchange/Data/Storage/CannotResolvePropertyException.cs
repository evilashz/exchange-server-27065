using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000122 RID: 290
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotResolvePropertyException : StoragePermanentException
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0006A638 File Offset: 0x00068838
		public CannotResolvePropertyException(string propertySchema) : base(ServerStrings.CannotResolvePropertyException(propertySchema))
		{
			this.propertySchema = propertySchema;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0006A64D File Offset: 0x0006884D
		public CannotResolvePropertyException(string propertySchema, Exception innerException) : base(ServerStrings.CannotResolvePropertyException(propertySchema), innerException)
		{
			this.propertySchema = propertySchema;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0006A663 File Offset: 0x00068863
		protected CannotResolvePropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertySchema = (string)info.GetValue("propertySchema", typeof(string));
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0006A68D File Offset: 0x0006888D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertySchema", this.propertySchema);
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0006A6A8 File Offset: 0x000688A8
		public string PropertySchema
		{
			get
			{
				return this.propertySchema;
			}
		}

		// Token: 0x040009AE RID: 2478
		private readonly string propertySchema;
	}
}
