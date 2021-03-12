using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000171 RID: 369
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedTargetRecipientTypeException : MigrationPermanentException
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x0006FA79 File Offset: 0x0006DC79
		public UnsupportedTargetRecipientTypeException(string type) : base(Strings.UnsupportedTargetRecipientTypeError(type))
		{
			this.type = type;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0006FA8E File Offset: 0x0006DC8E
		public UnsupportedTargetRecipientTypeException(string type, Exception innerException) : base(Strings.UnsupportedTargetRecipientTypeError(type), innerException)
		{
			this.type = type;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0006FAA4 File Offset: 0x0006DCA4
		protected UnsupportedTargetRecipientTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0006FACE File Offset: 0x0006DCCE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0006FAE9 File Offset: 0x0006DCE9
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000AFF RID: 2815
		private readonly string type;
	}
}
