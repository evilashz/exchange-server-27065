using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedRecipientTypeForProtocolException : MigrationPermanentException
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x0006F9AD File Offset: 0x0006DBAD
		public UnsupportedRecipientTypeForProtocolException(string type, string protocol) : base(Strings.ErrorUnsupportedRecipientTypeForProtocol(type, protocol))
		{
			this.type = type;
			this.protocol = protocol;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0006F9CA File Offset: 0x0006DBCA
		public UnsupportedRecipientTypeForProtocolException(string type, string protocol, Exception innerException) : base(Strings.ErrorUnsupportedRecipientTypeForProtocol(type, protocol), innerException)
		{
			this.type = type;
			this.protocol = protocol;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0006F9E8 File Offset: 0x0006DBE8
		protected UnsupportedRecipientTypeForProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.type = (string)info.GetValue("type", typeof(string));
			this.protocol = (string)info.GetValue("protocol", typeof(string));
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0006FA3D File Offset: 0x0006DC3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.type);
			info.AddValue("protocol", this.protocol);
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0006FA69 File Offset: 0x0006DC69
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0006FA71 File Offset: 0x0006DC71
		public string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x04000AFD RID: 2813
		private readonly string type;

		// Token: 0x04000AFE RID: 2814
		private readonly string protocol;
	}
}
