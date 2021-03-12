using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200019B RID: 411
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NspiNotSupportedForEndpointTypeException : MigrationPermanentException
	{
		// Token: 0x0600175A RID: 5978 RVA: 0x0007090A File Offset: 0x0006EB0A
		public NspiNotSupportedForEndpointTypeException(MigrationType type) : base(Strings.ErrorNspiNotSupportedForEndpointType(type))
		{
			this.type = type;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0007091F File Offset: 0x0006EB1F
		public NspiNotSupportedForEndpointTypeException(MigrationType type, Exception innerException) : base(Strings.ErrorNspiNotSupportedForEndpointType(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00070935 File Offset: 0x0006EB35
		protected NspiNotSupportedForEndpointTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (MigrationType)info.GetValue("type", typeof(MigrationType));
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0007095F File Offset: 0x0006EB5F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0007097F File Offset: 0x0006EB7F
		public MigrationType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000B16 RID: 2838
		private readonly MigrationType type;
	}
}
